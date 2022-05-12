using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;

namespace ET
{
    [FriendClass(typeof(NetThreadComponent))]
    [ObjectSystem]
    public class NetInnerComponentAwakeSystem: AwakeSystem<NetInnerComponent, int>
    {
        public override void Awake(NetInnerComponent me, int sessionStreamDispatcherType)
        {
            NetInnerComponent.Instance = me;
            me.SessionStreamDispatcherType = sessionStreamDispatcherType;
            
            me.Service = new TService(NetThreadComponent.Instance.ThreadSynchronizationContext, ServiceType.Inner);
            me.Service.ErrorCallback += me.OnError;
            me.Service.ReadCallback += me.OnRead;

            NetThreadComponent.Instance.Add(me.Service);
        }
    }

    [FriendClass(typeof(NetThreadComponent))]
    [ObjectSystem]
    public class NetInnerComponentAwake1System: AwakeSystem<NetInnerComponent, IPEndPoint, int>
    {
        public override void Awake(NetInnerComponent me, IPEndPoint address, int sessionStreamDispatcherType)
        {
            NetInnerComponent.Instance = me;
            me.SessionStreamDispatcherType = sessionStreamDispatcherType;

            me.Service = new TService(NetThreadComponent.Instance.ThreadSynchronizationContext, address, ServiceType.Inner);
            me.Service.ErrorCallback += me.OnError;
            me.Service.ReadCallback += me.OnRead;
            me.Service.AcceptCallback += me.OnAccept;

            NetThreadComponent.Instance.Add(me.Service);
        }
    }

    [ObjectSystem]
    public class NetInnerComponentDestroySystem: DestroySystem<NetInnerComponent>
    {
        public override void Destroy(NetInnerComponent me)
        {
            NetThreadComponent.Instance.Remove(me.Service);
            me.Service.Destroy();
        }
    }

    [FriendClass(typeof(NetInnerComponent))]
    public static class NetInnerComponentSystem
    {
        public static void OnRead(this NetInnerComponent me, long channelId, MemoryStream memoryStream)
        {
            Session session = me.GetChild<Session>(channelId);
            if (session == null)
            {
                return;
            }

            session.LastRecvTime = TimeHelper.ClientNow();
            SessionStreamDispatcher.Instance.Dispatch(me.SessionStreamDispatcherType, session, memoryStream);
        }

        public static void OnError(this NetInnerComponent me, long channelId, int error)
        {
            Session session = me.GetChild<Session>(channelId);
            if (session == null)
            {
                return;
            }

            session.Error = error;
            session.Dispose();
        }

        // 这个channelId是由CreateAcceptChannelId生成的
        public static void OnAccept(this NetInnerComponent me, long channelId, IPEndPoint ipEndPoint)
        {
            Session session = me.AddChildWithId<Session, AService>(channelId, me.Service);
            session.RemoteAddress = ipEndPoint;
            //session.AddComponent<SessionIdleCheckerComponent, int, int, int>(NetThreadComponent.checkInteral, NetThreadComponent.recvMaxIdleTime, NetThreadComponent.sendMaxIdleTime);
        }

        // 这个channelId是由CreateConnectChannelId生成的
        public static Session Create(this NetInnerComponent me, IPEndPoint ipEndPoint)
        {
            uint localConn = me.Service.CreateRandomLocalConn();
            long channelId = me.Service.CreateConnectChannelId(localConn);
            Session session = me.CreateInner(channelId, ipEndPoint);
            return session;
        }

        private static Session CreateInner(this NetInnerComponent me, long channelId, IPEndPoint ipEndPoint)
        {
            Session session = me.AddChildWithId<Session, AService>(channelId, me.Service);

            session.RemoteAddress = ipEndPoint;

            me.Service.GetOrCreate(channelId, ipEndPoint);

            //session.AddComponent<InnerPingComponent>();
            //session.AddComponent<SessionIdleCheckerComponent, int, int, int>(NetThreadComponent.checkInteral, NetThreadComponent.recvMaxIdleTime, NetThreadComponent.sendMaxIdleTime);

            return session;
        }

        // 内网actor session，channelId是进程号
        public static Session Get(this NetInnerComponent me, long channelId)
        {
            Session session = me.GetChild<Session>(channelId);
            if (session == null)
            {
                IPEndPoint ipEndPoint = StartProcessConfigCategory.Instance.Get((int) channelId).InnerIPPort;
                session = me.CreateInner(channelId, ipEndPoint);
            }

            return session;
        }
    }
}