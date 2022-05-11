using System;

namespace ET
{
    [Timer(TimerType.SessionAcceptTimeout)]
    public class SessionAcceptTimeout: ATimer<SessionAcceptTimeoutComponent>
    {
        public override void Run(SessionAcceptTimeoutComponent me)
        {
            try
            {
                me.Parent.Dispose();
            }
            catch (Exception e)
            {
                Log.Error($"move timer error: {me.Id}\n{e}");
            }
        }
    }
    
    [ObjectSystem]
    public class SessionAcceptTimeoutComponentAwakeSystem: AwakeSystem<SessionAcceptTimeoutComponent>
    {
        public override void Awake(SessionAcceptTimeoutComponent me)
        {
            me.Timer = TimerComponent.Instance.NewOnceTimer(TimeHelper.ServerNow() + 5000, TimerType.SessionAcceptTimeout, me);
        }
    }

    [ObjectSystem]
    public class SessionAcceptTimeoutComponentDestroySystem: DestroySystem<SessionAcceptTimeoutComponent>
    {
        public override void Destroy(SessionAcceptTimeoutComponent me)
        {
            TimerComponent.Instance.Remove(ref me.Timer);
        }
    }
}