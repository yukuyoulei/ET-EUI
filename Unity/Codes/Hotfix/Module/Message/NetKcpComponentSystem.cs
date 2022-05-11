using System;
using System.IO;
using System.Net;

namespace ET
{
    [FriendClass(typeof(NetThreadComponent))]
    [ObjectSystem]
    public class NetKcpComponentAwakeSystem: AwakeSystem<NetKcpComponent, int>
    {
        public override void Awake(NetKcpComponent me, int sessionStreamDispatcherType)
        {
            me.SessionStreamDispatcherType = sessionStreamDispatcherType;
            
            me.Service = new TService(NetThreadComponent.Instance.ThreadSynchronizationContext, ServiceType.Outer);
            me.Service.ErrorCallback += (channelId, error) => me.OnError(channelId, error);
            me.Service.ReadCallback += (channelId, Memory) => me.OnRead(channelId, Memory);

            NetThreadComponent.Instance.Add(me.Service);
        }
    }

    [FriendClass(typeof(NetThreadComponent))]
    [ObjectSystem]
    public class NetKcpComponentAwake1System: AwakeSystem<NetKcpComponent, IPEndPoint, int>
    {
        public override void Awake(NetKcpComponent me, IPEndPoint address, int sessionStreamDispatcherType)
        {
            me.SessionStreamDispatcherType = sessionStreamDispatcherType;
            
            me.Service = new TService(NetThreadComponent.Instance.ThreadSynchronizationContext, address, ServiceType.Outer);
            me.Service.ErrorCallback += (channelId, error) => me.OnError(channelId, error);
            me.Service.ReadCallback += (channelId, Memory) => me.OnRead(channelId, Memory);
            me.Service.AcceptCallback += (channelId, IPAddress) => me.OnAccept(channelId, IPAddress);

            NetThreadComponent.Instance.Add(me.Service);
        }
    }

    [ObjectSystem]
    public class NetKcpComponentDestroySystem: DestroySystem<NetKcpComponent>
    {
        public override void Destroy(NetKcpComponent me)
        {
            NetThreadComponent.Instance.Remove(me.Service);
            me.Service.Destroy();
        }
    }

    [FriendClass(typeof(NetKcpComponent))]
    public static class NetKcpComponentSystem
    {
        public static void OnRead(this NetKcpComponent me, long channelId, MemoryStream memoryStream)
        {
            Session session = me.GetChild<Session>(channelId);
            if (session == null)
            {
                return;
            }

            session.LastRecvTime = TimeHelper.ClientNow();
            SessionStreamDispatcher.Instance.Dispatch(me.SessionStreamDispatcherType, session, memoryStream);
        }

        public static void OnError(this NetKcpComponent me, long channelId, int error)
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
        public static void OnAccept(this NetKcpComponent me, long channelId, IPEndPoint ipEndPoint)
        {
            Session session = me.AddChildWithId<Session, AService>(channelId, me.Service);
            session.RemoteAddress = ipEndPoint;

            // 挂上这个组件，5秒就会删除session，所以客户端验证完成要删除这个组件。该组件的作用就是防止外挂一直连接不发消息也不进行权限验证
            session.AddComponent<SessionAcceptTimeoutComponent>();
            // 客户端连接，2秒检查一次recv消息，10秒没有消息则断开
            session.AddComponent<SessionIdleCheckerComponent, int>(NetThreadComponent.checkInteral);
        }

        public static Session Get(this NetKcpComponent me, long id)
        {
            Session session = me.GetChild<Session>(id);
            return session;
        }

        public static Session Create(this NetKcpComponent me, IPEndPoint realIPEndPoint)
        {
            long channelId = RandomHelper.RandInt64();
            Session session = me.AddChildWithId<Session, AService>(channelId, me.Service);
            session.RemoteAddress = realIPEndPoint;
            session.AddComponent<SessionIdleCheckerComponent, int>(NetThreadComponent.checkInteral);
            
            me.Service.GetOrCreate(session.Id, realIPEndPoint);

            return session;
        }
    }
}