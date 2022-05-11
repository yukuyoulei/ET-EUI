using System;

namespace ET
{
    [Timer(TimerType.SessionIdleChecker)]
    public class SessionIdleChecker: ATimer<SessionIdleCheckerComponent>
    {
        public override void Run(SessionIdleCheckerComponent me)
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
    public class SessionIdleCheckerComponentAwakeSystem: AwakeSystem<SessionIdleCheckerComponent, int>
    {
        public override void Awake(SessionIdleCheckerComponent me, int checkInteral)
        {
            me.RepeatedTimer = TimerComponent.Instance.NewRepeatedTimer(checkInteral, TimerType.SessionIdleChecker, me);
        }
    }

    [ObjectSystem]
    public class SessionIdleCheckerComponentDestroySystem: DestroySystem<SessionIdleCheckerComponent>
    {
        public override void Destroy(SessionIdleCheckerComponent me)
        {
            TimerComponent.Instance?.Remove(ref me.RepeatedTimer);
        }
    }

    public static class SessionIdleCheckerComponentSystem
    {
        public static void Check(this SessionIdleCheckerComponent me)
        {
            Session session = me.GetParent<Session>();
            long timeNow = TimeHelper.ClientNow();

            if (timeNow - session.LastRecvTime < 30 * 1000 && timeNow - session.LastSendTime < 30 * 1000)
            {
                return;
            }

            Log.Info(
                $"session timeout: {session.Id} {timeNow} {session.LastRecvTime} {session.LastSendTime} {timeNow - session.LastRecvTime} {timeNow - session.LastSendTime}");
            session.Error = ErrorCore.ERR_SessionSendOrRecvTimeout;

            session.Dispose();
        }
    }
}