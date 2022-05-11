using System;
using System.Collections.Generic;
using System.Linq;

namespace ET
{
    [ObjectSystem]
    public class AIDispatcherComponentAwakeSystem: AwakeSystem<AIDispatcherComponent>
    {
        public override void Awake(AIDispatcherComponent me)
        {
            AIDispatcherComponent.Instance = me;
            me.Load();
        }
    }

    [ObjectSystem]
    public class AIDispatcherComponentLoadSystem: LoadSystem<AIDispatcherComponent>
    {
        public override void Load(AIDispatcherComponent me)
        {
            me.Load();
        }
    }

    [ObjectSystem]
    public class AIDispatcherComponentDestroySystem: DestroySystem<AIDispatcherComponent>
    {
        public override void Destroy(AIDispatcherComponent me)
        {
            me.AIHandlers.Clear();
            AIDispatcherComponent.Instance = null;
        }
    }

    [FriendClass(typeof(AIDispatcherComponent))]
    public static class AIDispatcherComponentSystem
    {
        public static void Load(this AIDispatcherComponent me)
        {
            me.AIHandlers.Clear();
            
            var types = Game.EventSystem.GetTypes(typeof (AIHandlerAttribute));
            foreach (Type type in types)
            {
                AAIHandler aaiHandler = Activator.CreateInstance(type) as AAIHandler;
                if (aaiHandler == null)
                {
                    Log.Error($"robot ai is not AAIHandler: {type.Name}");
                    continue;
                }
                me.AIHandlers.Add(type.Name, aaiHandler);
            }
        }
    }
}