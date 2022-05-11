using System;
using UnityEngine;

namespace ET
{
    [Timer(TimerType.AITimer)]
    public class AITimer: ATimer<AIComponent>
    {
        public override void Run(AIComponent me)
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
    public class AIComponentAwakeSystem: AwakeSystem<AIComponent, int>
    {
        public override void Awake(AIComponent me, int aiConfigId)
        {
            me.AIConfigId = aiConfigId;
            me.Timer = TimerComponent.Instance.NewRepeatedTimer(1000, TimerType.AITimer, me);
        }
    }

    [ObjectSystem]
    public class AIComponentDestroySystem: DestroySystem<AIComponent>
    {
        public override void Destroy(AIComponent me)
        {
            TimerComponent.Instance?.Remove(ref me.Timer);
            me.CancellationToken?.Cancel();
            me.CancellationToken = null;
            me.Current = 0;
        }
    }

    [FriendClass(typeof(AIComponent))]
    [FriendClass(typeof(AIDispatcherComponent))]
    public static class AIComponentSystem
    {
        public static void Check(this AIComponent me)
        {
            if (me.Parent == null)
            {
                TimerComponent.Instance.Remove(ref me.Timer);
                return;
            }

            var oneAI = AIConfigCategory.Instance.AIConfigs[me.AIConfigId];

            foreach (AIConfig aiConfig in oneAI.Values)
            {

                AIDispatcherComponent.Instance.AIHandlers.TryGetValue(aiConfig.Name, out AAIHandler aaiHandler);

                if (aaiHandler == null)
                {
                    Log.Error($"not found aihandler: {aiConfig.Name}");
                    continue;
                }

                int ret = aaiHandler.Check(me, aiConfig);
                if (ret != 0)
                {
                    continue;
                }

                if (me.Current == aiConfig.Id)
                {
                    break;
                }

                me.Cancel(); // 取消之前的行为
                ETCancellationToken cancellationToken = new ETCancellationToken();
                me.CancellationToken = cancellationToken;
                me.Current = aiConfig.Id;

                aaiHandler.Execute(me, aiConfig, cancellationToken).Coroutine();
                return;
            }
            
        }

        private static void Cancel(this AIComponent me)
        {
            me.CancellationToken?.Cancel();
            me.Current = 0;
            me.CancellationToken = null;
        }
    }
} 