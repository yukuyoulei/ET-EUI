using System;

namespace ET
{
    [ObjectSystem]
    public class PingComponentAwakeSystem: AwakeSystem<PingComponent>
    {
        public override void Awake(PingComponent me)
        {
            PingAsync(me).Coroutine();
        }

        private static async ETTask PingAsync(PingComponent me)
        {
            Session session = me.GetParent<Session>();
            long instanceId = me.InstanceId;
            
            while (true)
            {
                if (me.InstanceId != instanceId)
                {
                    return;
                }

                long time1 = TimeHelper.ClientNow();
                try
                {
                    G2C_Ping response = await session.Call(me.C2G_Ping) as G2C_Ping;

                    if (me.InstanceId != instanceId)
                    {
                        return;
                    }

                    long time2 = TimeHelper.ClientNow();
                    me.Ping = time2 - time1;
                    
                    Game.TimeInfo.ServerMinusClientTime = response.Time + (time2 - time1) / 2 - time2;

                    await TimerComponent.Instance.WaitAsync(2000);
                }
                catch (RpcException e)
                {
                    // session断开导致ping rpc报错，记录一下即可，不需要打成error
                    Log.Info($"ping error: {me.Id} {e.Error}");
                    return;
                }
                catch (Exception e)
                {
                    Log.Error($"ping error: \n{e}");
                }
            }
        }
    }

    [ObjectSystem]
    public class PingComponentDestroySystem: DestroySystem<PingComponent>
    {
        public override void Destroy(PingComponent me)
        {
            me.Ping = default;
        }
    }
}