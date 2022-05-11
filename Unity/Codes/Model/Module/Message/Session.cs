using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace ET
{
    public readonly struct RpcInfo
    {
        public readonly IRequest Request;
        public readonly ETTask<IResponse> Tcs;

        public RpcInfo(IRequest request)
        {
            this.Request = request;
            this.Tcs = ETTask<IResponse>.Create(true);
        }
    }
    
    [FriendClass(typeof(Session))]
    public static class SessionSystem
    {
        [ObjectSystem]
        public class SessionAwakeSystem: AwakeSystem<Session, AService>
        {
            public override void Awake(Session me, AService aService)
            {
                me.AService = aService;
                long timeNow = TimeHelper.ClientNow();
                me.LastRecvTime = timeNow;
                me.LastSendTime = timeNow;

                me.requestCallbacks.Clear();
            
                Log.Info($"session create: zone: {me.DomainZone()} id: {me.Id} {timeNow} ");
            }
        }
        
        [ObjectSystem]
        public class SessionDestroySystem: DestroySystem<Session>
        {
            public override void Destroy(Session me)
            {
                me.AService.RemoveChannel(me.Id);
            
                foreach (RpcInfo responseCallback in me.requestCallbacks.Values.ToArray())
                {
                    responseCallback.Tcs.SetException(new RpcException(me.Error, $"session dispose: {me.Id} {me.RemoteAddress}"));
                }

                Log.Info($"session dispose: {me.RemoteAddress} id: {me.Id} ErrorCode: {me.Error}, please see ErrorCode.cs! {TimeHelper.ClientNow()}");
            
                me.requestCallbacks.Clear();
            }
        }
        
        public static void OnRead(this Session me, ushort opcode, IResponse response)
        {
            OpcodeHelper.LogMsg(me.DomainZone(), opcode, response);
            
            if (!me.requestCallbacks.TryGetValue(response.RpcId, out var action))
            {
                return;
            }

            me.requestCallbacks.Remove(response.RpcId);
            if (ErrorCore.IsRpcNeedThrowException(response.Error))
            {
                action.Tcs.SetException(new Exception($"Rpc error, request: {action.Request} response: {response}"));
                return;
            }
            action.Tcs.SetResult(response);
        }
        
        public static async ETTask<IResponse> Call(this Session me, IRequest request, ETCancellationToken cancellationToken)
        {
            int rpcId = ++Session.RpcId;
            RpcInfo rpcInfo = new RpcInfo(request);
            me.requestCallbacks[rpcId] = rpcInfo;
            request.RpcId = rpcId;

            me.Send(request);
            
            void CancelAction()
            {
                if (!me.requestCallbacks.TryGetValue(rpcId, out RpcInfo action))
                {
                    return;
                }

                me.requestCallbacks.Remove(rpcId);
                Type responseType = OpcodeTypeComponent.Instance.GetResponseType(action.Request.GetType());
                IResponse response = (IResponse) Activator.CreateInstance(responseType);
                response.Error = ErrorCore.ERR_Cancel;
                action.Tcs.SetResult(response);
            }

            IResponse ret;
            try
            {
                cancellationToken?.Add(CancelAction);
                ret = await rpcInfo.Tcs;
            }
            finally
            {
                cancellationToken?.Remove(CancelAction);
            }
            return ret;
        }

        public static async ETTask<IResponse> Call(this Session me, IRequest request)
        {
            int rpcId = ++Session.RpcId;
            RpcInfo rpcInfo = new RpcInfo(request);
            me.requestCallbacks[rpcId] = rpcInfo;
            request.RpcId = rpcId;
            me.Send(request);
            return await rpcInfo.Tcs;
        }

        public static void Reply(this Session me, IResponse message)
        {
            me.Send(0, message);
        }

        public static void Send(this Session me, IMessage message)
        {
            me.Send(0, message);
        }
        
        public static void Send(this Session me, long actorId, IMessage message)
        {
            (ushort opcode, MemoryStream stream) = MessageSerializeHelper.MessageToStream(message);
            OpcodeHelper.LogMsg(me.DomainZone(), opcode, message);
            me.Send(actorId, stream);
        }
        
        public static void Send(this Session me, long actorId, MemoryStream memoryStream)
        {
            me.LastSendTime = TimeHelper.ClientNow();
            me.AService.SendStream(me.Id, actorId, memoryStream);
        }
    }

    public sealed class Session: Entity, IAwake<AService>, IDestroy
    {
        public AService AService;
        
        public static int RpcId
        {
            get;
            set;
        }

        public readonly Dictionary<int, RpcInfo> requestCallbacks = new Dictionary<int, RpcInfo>();
        
        public long LastRecvTime
        {
            get;
            set;
        }

        public long LastSendTime
        {
            get;
            set;
        }

        public int Error
        {
            get;
            set;
        }

        public IPEndPoint RemoteAddress
        {
            get;
            set;
        }
    }
}