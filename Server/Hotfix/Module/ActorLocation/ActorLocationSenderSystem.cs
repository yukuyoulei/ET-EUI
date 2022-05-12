using System;

namespace ET
{
    [ObjectSystem]
    public class ActorLocationSenderAwakeSystem: AwakeSystem<ActorLocationSender>
    {
        public override void Awake(ActorLocationSender me)
        {
            me.LastSendOrRecvTime = TimeHelper.ServerNow();
            me.ActorId = 0;
            me.Error = 0;
        }
    }

    [ObjectSystem]
    public class ActorLocationSenderDestroySystem: DestroySystem<ActorLocationSender>
    {
        public override void Destroy(ActorLocationSender me)
        {
            Log.Debug($"actor location remove: {me.Id}");
            me.LastSendOrRecvTime = 0;
            me.ActorId = 0;
            me.Error = 0;
        }
    }
}