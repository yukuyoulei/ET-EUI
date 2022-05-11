using System;
using System.Threading;

namespace ET
{
    [ObjectSystem]
    public class NetThreadComponentAwakeSystem: AwakeSystem<NetThreadComponent>
    {
        public override void Awake(NetThreadComponent me)
        {
            NetThreadComponent.Instance = me;
            
            me.ThreadSynchronizationContext = ThreadSynchronizationContext.Instance;

            me.foreachAction = service => service.Update();
        }
    }

    [ObjectSystem]
    public class NetThreadComponentUpdateSystem: LateUpdateSystem<NetThreadComponent>
    {
        public override void LateUpdate(NetThreadComponent me)
        {
            me.Services.Foreach(me.foreachAction);
        }
    }
    
    [ObjectSystem]
    public class NetThreadComponentDestroySystem: DestroySystem<NetThreadComponent>
    {
        public override void Destroy(NetThreadComponent me)
        {
            me.Stop();
        }
    }
    
    [FriendClass(typeof(NetThreadComponent))]
    public static class NetThreadComponentSystem
    {

        public static void Stop(this NetThreadComponent me)
        {
        }

        public static void Add(this NetThreadComponent me, AService kService)
        {
            // 这里要去下一帧添加，避免foreach错误
            me.ThreadSynchronizationContext.PostNext(() =>
            {
                if (kService.IsDispose())
                {
                    return;
                }
                me.Services.Add(kService);
            });
        }
        
        public static void Remove(this NetThreadComponent me, AService kService)
        {
            // 这里要去下一帧删除，避免foreach错误
            me.ThreadSynchronizationContext.PostNext(() =>
            {
                if (kService.IsDispose())
                {
                    return;
                }
                me.Services.Remove(kService);
            });
        }
        
    }
}