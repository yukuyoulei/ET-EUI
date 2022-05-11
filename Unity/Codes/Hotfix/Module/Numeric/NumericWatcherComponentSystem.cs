using System;
using System.Collections.Generic;

namespace ET
{
    [FriendClass(typeof(NumericWatcherComponent))]
    public static class NumericWatcherComponentSystem
    {
        [ObjectSystem]
        public class NumericWatcherComponentAwakeSystem : AwakeSystem<NumericWatcherComponent>
        {
            public override void Awake(NumericWatcherComponent me)
            {
                NumericWatcherComponent.Instance = me;
                me.Init();
            }
        }

	
        public class NumericWatcherComponentLoadSystem : LoadSystem<NumericWatcherComponent>
        {
            public override void Load(NumericWatcherComponent me)
            {
                me.Init();
            }
        }

        private static void Init(this NumericWatcherComponent me)
        {
            me.allWatchers = new Dictionary<int, List<INumericWatcher>>();

            List<Type> types = Game.EventSystem.GetTypes(typeof(NumericWatcherAttribute));
            foreach (Type type in types)
            {
                object[] attrs = type.GetCustomAttributes(typeof(NumericWatcherAttribute), false);

                foreach (object attr in attrs)
                {
                    NumericWatcherAttribute numericWatcherAttribute = (NumericWatcherAttribute)attr;
                    INumericWatcher obj = (INumericWatcher)Activator.CreateInstance(type);
                    if (!me.allWatchers.ContainsKey(numericWatcherAttribute.NumericType))
                    {
                        me.allWatchers.Add(numericWatcherAttribute.NumericType, new List<INumericWatcher>());
                    }
                    me.allWatchers[numericWatcherAttribute.NumericType].Add(obj);
                }
            }
        }

        public static void Run(this NumericWatcherComponent me, EventType.NumbericChange args)
        {
            List<INumericWatcher> list;
            if (!me.allWatchers.TryGetValue(args.NumericType, out list))
            {
                return;
            }
            foreach (INumericWatcher numericWatcher in list)
            {
                numericWatcher.Run(args);
            }
        }
    }
}