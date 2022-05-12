using System;
using System.IO;

namespace ET
{
    [Timer(TimerType.ActorMessageSenderChecker)]
    public class ActorMessageSenderChecker: ATimer<ActorMessageSenderComponent>
    {
        public override void Run(ActorMessageSenderComponent me)
        {
            try
            {
                me.Check();
            }
            catch (Exception e)
            {
                Log.Error($"move timer error: {me.Id}\n{e}");
            }
        }
    }
    
    [ObjectSystem]
    public class ActorMessageSenderComponentAwakeSystem: AwakeSystem<ActorMessageSenderComponent>
    {
        public override void Awake(ActorMessageSenderComponent me)
        {
            ActorMessageSenderComponent.Instance = me;

            me.TimeoutCheckTimer = TimerComponent.Instance.NewRepeatedTimer(1000, TimerType.ActorMessageSenderChecker, me);
        }
    }

    [ObjectSystem]
    public class ActorMessageSenderComponentDestroySystem: DestroySystem<ActorMessageSenderComponent>
    {
        public override void Destroy(ActorMessageSenderComponent me)
        {
            ActorMessageSenderComponent.Instance = null;
            TimerComponent.Instance.Remove(ref me.TimeoutCheckTimer);
            me.TimeoutCheckTimer = 0;
            me.TimeoutActorMessageSenders.Clear();
        }
    }

    [FriendClass(typeof(ActorMessageSenderComponent))]
    public static class ActorMessageSenderComponentSystem
    {
        public static void Run(ActorMessageSender me, IActorResponse response)
        {
            if (response.Error == ErrorCore.ERR_ActorTimeout)
            {
                me.Tcs.SetException(new Exception($"Rpc error: request, 注意Actor消息超时，请注意查看是否死锁或者没有reply: actorId: {me.ActorId} {me.MemoryStream.ToActorMessage()}, response: {response}"));
                return;
            }

            if (me.NeedException && ErrorCore.IsRpcNeedThrowException(response.Error))
            {
                me.Tcs.SetException(new Exception($"Rpc error: actorId: {me.ActorId} request: {me.MemoryStream.ToActorMessage()}, response: {response}"));
                return;
            }

            me.Tcs.SetResult(response);
        }
        
        public static void Check(this ActorMessageSenderComponent me)
        {
            long timeNow = TimeHelper.ServerNow();
            foreach ((int key, ActorMessageSender value) in me.requestCallback)
            {
                // 因为是顺序发送的，所以，检测到第一个不超时的就退出
                if (timeNow < value.CreateTime + ActorMessageSenderComponent.TIMEOUT_TIME)
                {
                    break;
                }

                me.TimeoutActorMessageSenders.Add(key);
            }

            foreach (int rpcId in me.TimeoutActorMessageSenders)
            {
                ActorMessageSender actorMessageSender = me.requestCallback[rpcId];
                me.requestCallback.Remove(rpcId);
                try
                {
                    IActorResponse response = ActorHelper.CreateResponse((IActorRequest)actorMessageSender.MemoryStream.ToActorMessage(), ErrorCore.ERR_ActorTimeout);
                    Run(actorMessageSender, response);
                }
                catch (Exception e)
                {
                    Log.Error(e.ToString());
                }
            }

            me.TimeoutActorMessageSenders.Clear();
        }

        public static void Send(this ActorMessageSenderComponent me, long actorId, IMessage message)
        {
            if (actorId == 0)
            {
                throw new Exception($"actor id is 0: {message}");
            }
            
            ProcessActorId processActorId = new ProcessActorId(actorId);
            Session session = NetInnerComponent.Instance.Get(processActorId.Process);
            session.Send(processActorId.ActorId, message);
        }
        
        public static void Send(this ActorMessageSenderComponent me, long actorId, MemoryStream memoryStream)
        {
            if (actorId == 0)
            {
                throw new Exception($"actor id is 0: {memoryStream.ToActorMessage()}");
            }
            
            ProcessActorId processActorId = new ProcessActorId(actorId);
            Session session = NetInnerComponent.Instance.Get(processActorId.Process);
            session.Send(processActorId.ActorId, memoryStream);
        }


        public static int GetRpcId(this ActorMessageSenderComponent me)
        {
            return ++me.RpcId;
        }

        public static async ETTask<IActorResponse> Call(
                this ActorMessageSenderComponent me,
                long actorId,
                IActorRequest request,
                bool needException = true
        )
        {
            request.RpcId = me.GetRpcId();
            
            if (actorId == 0)
            {
                throw new Exception($"actor id is 0: {request}");
            }

            (ushort _, MemoryStream stream) = MessageSerializeHelper.MessageToStream(request);

            return await me.Call(actorId, request.RpcId, stream, needException);
        }
        
        public static async ETTask<IActorResponse> Call(
                this ActorMessageSenderComponent me,
                long actorId,
                int rpcId,
                MemoryStream memoryStream,
                bool needException = true
        )
        {
            if (actorId == 0)
            {
                throw new Exception($"actor id is 0: {memoryStream.ToActorMessage()}");
            }

            var tcs = ETTask<IActorResponse>.Create(true);
            
            me.requestCallback.Add(rpcId, new ActorMessageSender(actorId, memoryStream, tcs, needException));
            
            me.Send(actorId, memoryStream);

            long beginTime = TimeHelper.ServerFrameTime();
            IActorResponse response = await tcs;
            long endTime = TimeHelper.ServerFrameTime();

            long costTime = endTime - beginTime;
            if (costTime > 200)
            {
                Log.Warning("actor rpc time > 200: {0} {1}", costTime, memoryStream.ToActorMessage());
            }
            
            return response;
        }

        public static void RunMessage(this ActorMessageSenderComponent me, long actorId, IActorResponse response)
        {
            ActorMessageSender actorMessageSender;
            if (!me.requestCallback.TryGetValue(response.RpcId, out actorMessageSender))
            {
                return;
            }

            me.requestCallback.Remove(response.RpcId);
            
            Run(actorMessageSender, response);
        }
    }
}