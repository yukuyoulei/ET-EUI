using System;
using System.Collections.Generic;

namespace ET
{
    public enum TimerClass
    {
        None,
        OnceTimer,
        OnceWaitTimer,
        RepeatedTimer,
    }
    
    [ObjectSystem]
    public class TimerActionAwakeSystem: AwakeSystem<TimerAction, TimerClass, long, int, object>
    {
        public override void Awake(TimerAction me, TimerClass timerClass, long time, int type, object obj)
        {
            me.TimerClass = timerClass;
            me.Object = obj;
            me.Time = time;
            me.Type = type;
        }
    }

    [ObjectSystem]
    public class TimerActionDestroySystem: DestroySystem<TimerAction>
    {
        public override void Destroy(TimerAction me)
        {
            me.Object = null;
            me.Time = 0;
            me.TimerClass = TimerClass.None;
            me.Type = 0;
        }
    }
    
    public class TimerAction: Entity, IAwake, IAwake<TimerClass, long, int, object>, IDestroy
    {
        public TimerClass TimerClass;

        public object Object;

        public long Time;

        public int Type;
    }

    [FriendClass(typeof(TimerAction))]
    [FriendClass(typeof(TimerComponent))]
    public static class TimerComponentSystem
    {
        [ObjectSystem]
        public class TimerComponentAwakeSystem: AwakeSystem<TimerComponent>
        {
            public override void Awake(TimerComponent me)
            {
                TimerComponent.Instance = me;
                me.Init();
            }
        }

        [ObjectSystem]
        public class TimerComponentUpdateSystem: UpdateSystem<TimerComponent>
        {
            public override void Update(TimerComponent me)
            {
                #region 每帧执行的timer，不用foreach TimeId，减少GC

                int count = me.everyFrameTimer.Count;
                for (int i = 0; i < count; ++i)
                {
                    long timerId = me.everyFrameTimer.Dequeue();
                    TimerAction timerAction = me.GetChild<TimerAction>(timerId);
                    if (timerAction == null)
                    {
                        continue;
                    }
                    me.Run(timerAction);
                }

                #endregion
            
                if (me.TimeId.Count == 0)
                {
                    return;
                }

                me.timeNow = TimeHelper.ServerNow();

                if (me.timeNow < me.minTime)
                {
                    return;
                }

                me.TimeId.ForEachFunc(me.foreachFunc);

                while (me.timeOutTime.Count > 0)
                {
                    long time = me.timeOutTime.Dequeue();
                    var list = me.TimeId[time];
                    for (int i = 0; i < list.Count; ++i)
                    {
                        long timerId = list[i];
                        me.timeOutTimerIds.Enqueue(timerId);
                    }

                    me.TimeId.Remove(time);
                }

                while (me.timeOutTimerIds.Count > 0)
                {
                    long timerId = me.timeOutTimerIds.Dequeue();

                    TimerAction timerAction = me.GetChild<TimerAction>(timerId);
                    if (timerAction == null)
                    {
                        continue;
                    }
                    me.Run(timerAction);
                }
            }
        }
    
        [ObjectSystem]
        public class TimerComponentLoadSystem: LoadSystem<TimerComponent>
        {
            public override void Load(TimerComponent me)
            {
                me.Init();
            }
        }

        [ObjectSystem]
        public class TimerComponentDestroySystem: DestroySystem<TimerComponent>
        {
            public override void Destroy(TimerComponent me)
            {
                TimerComponent.Instance = null;
            }
        }

        private static void Init(this TimerComponent me)
        {
            me.foreachFunc = (k, v) =>
            {
                if (k > me.timeNow)
                {
                    me.minTime = k;
                    return false;
                }

                me.timeOutTime.Enqueue(k);
                return true;
            };
            
            me.timerActions = new ITimer[TimerComponent.TimeTypeMax];

            List<Type> types = Game.EventSystem.GetTypes(typeof (TimerAttribute));

            foreach (Type type in types)
            {
                ITimer iTimer = Activator.CreateInstance(type) as ITimer;
                if (iTimer == null)
                {
                    Log.Error($"Timer Action {type.Name} 需要继承 ITimer");
                    continue;
                }
                
                object[] attrs = type.GetCustomAttributes(typeof(TimerAttribute), false);
                if (attrs.Length == 0)
                {
                    continue;
                }

                foreach (object attr in attrs)
                {
                    TimerAttribute timerAttribute = attr as TimerAttribute;
                    me.timerActions[timerAttribute.Type] = iTimer;
                }
            }
        }

        private static void Run(this TimerComponent me, TimerAction timerAction)
        {
            switch (timerAction.TimerClass)
            {
                case TimerClass.OnceTimer:
                {
                    int type = timerAction.Type;
                    ITimer timer = me.timerActions[type];
                    if (timer == null)
                    {
                        Log.Error($"not found timer action: {type}");
                        return;
                    }
                    timer.Handle(timerAction.Object);
                    break;
                }
                case TimerClass.OnceWaitTimer:
                {
                    ETTask<bool> tcs = timerAction.Object as ETTask<bool>;
                    me.Remove(timerAction.Id);
                    tcs.SetResult(true);
                    break;
                }
                case TimerClass.RepeatedTimer:
                {
                    int type = timerAction.Type;
                    long tillTime = TimeHelper.ServerNow() + timerAction.Time;
                    me.AddTimer(tillTime, timerAction);

                    ITimer timer = me.timerActions[type];
                    if (timer == null)
                    {
                        Log.Error($"not found timer action: {type}");
                        return;
                    }
                    timer.Handle(timerAction.Object);
                    break;
                }
            }
        }
        
        private static void AddTimer(this TimerComponent me, long tillTime, TimerAction timer)
        {
            if (timer.TimerClass == TimerClass.RepeatedTimer && timer.Time == 0)
            {
                me.everyFrameTimer.Enqueue(timer.Id);
                return;
            }
            me.TimeId.Add(tillTime, timer.Id);
            if (tillTime < me.minTime)
            {
                me.minTime = tillTime;
            }
        }

        public static bool Remove(this TimerComponent me, ref long id)
        {
            long i = id;
            id = 0;
            return me.Remove(i);
        }
        
        private static bool Remove(this TimerComponent me, long id)
        {
            if (id == 0)
            {
                return false;
            }

            TimerAction timerAction = me.GetChild<TimerAction>(id);
            if (timerAction == null)
            {
                return false;
            }
            timerAction.Dispose();
            return true;
        }

        public static async ETTask<bool> WaitTillAsync(this TimerComponent me, long tillTime, ETCancellationToken cancellationToken = null)
        {
            if (me.timeNow >= tillTime)
            {
                return true;
            }

            ETTask<bool> tcs = ETTask<bool>.Create(true);
            TimerAction timer = me.AddChild<TimerAction, TimerClass, long, int, object>(TimerClass.OnceWaitTimer, tillTime - me.timeNow, 0, tcs, true);
            me.AddTimer(tillTime, timer);
            long timerId = timer.Id;

            void CancelAction()
            {
                if (me.Remove(timerId))
                {
                    tcs.SetResult(false);
                }
            }
            
            bool ret;
            try
            {
                cancellationToken?.Add(CancelAction);
                ret = await tcs;
            }
            finally
            {
                cancellationToken?.Remove(CancelAction);    
            }
            return ret;
        }

        public static async ETTask<bool> WaitFrameAsync(this TimerComponent me, ETCancellationToken cancellationToken = null)
        {
            bool ret = await me.WaitAsync(1, cancellationToken);
            return ret;
        }

        public static async ETTask<bool> WaitAsync(this TimerComponent me, long time, ETCancellationToken cancellationToken = null)
        {
            if (time == 0)
            {
                return true;
            }
            long tillTime = TimeHelper.ServerNow() + time;

            ETTask<bool> tcs = ETTask<bool>.Create(true);
            
            TimerAction timer = me.AddChild<TimerAction, TimerClass, long, int, object>(TimerClass.OnceWaitTimer, time, 0, tcs, true);
            me.AddTimer(tillTime, timer);
            long timerId = timer.Id;

            void CancelAction()
            {
                if (me.Remove(timerId))
                {
                    tcs.SetResult(false);
                }
            }

            bool ret;
            try
            {
                cancellationToken?.Add(CancelAction);
                ret = await tcs;
            }
            finally
            {
                cancellationToken?.Remove(CancelAction); 
            }
            return ret;
        }
        
        // 用这个优点是可以热更，缺点是回调式的写法，逻辑不连贯。WaitTillAsync不能热更，优点是逻辑连贯。
        // wait时间短并且逻辑需要连贯的建议WaitTillAsync
        // wait时间长不需要逻辑连贯的建议用NewOnceTimer
        public static long NewOnceTimer(this TimerComponent me, long tillTime, int type, object args)
        {
            if (tillTime < TimeHelper.ServerNow())
            {
                Log.Warning($"new once time too small: {tillTime}");
            }
            TimerAction timer = me.AddChild<TimerAction, TimerClass, long, int, object>(TimerClass.OnceTimer, tillTime, type, args, true);
            me.AddTimer(tillTime, timer);
            return timer.Id;
        }

        public static long NewFrameTimer(this TimerComponent me, int type, object args)
        {
#if NOT_UNITY
			return me.NewRepeatedTimerInner(100, type, args);
#else
            return me.NewRepeatedTimerInner(0, type, args);
#endif
        }

        /// <summary>
        /// 创建一个RepeatedTimer
        /// </summary>
        private static long NewRepeatedTimerInner(this TimerComponent me, long time, int type, object args)
        {
#if NOT_UNITY
			if (time < 100)
			{ 
				throw new Exception($"repeated timer < 100, timerType: time: {time}");
			}
#endif
            long tillTime = TimeHelper.ServerNow() + time;
            TimerAction timer = me.AddChild<TimerAction, TimerClass, long, int, object>(TimerClass.RepeatedTimer, time, type, args, true);

            // 每帧执行的不用加到timerId中，防止遍历
            me.AddTimer(tillTime, timer);
            return timer.Id;
        }

        public static long NewRepeatedTimer(this TimerComponent me, long time, int type, object args)
        {
            if (time < 100)
            {
                Log.Error($"time too small: {time}");
                return 0;
            }
            return me.NewRepeatedTimerInner(time, type, args);
        }
    }

    

    public class TimerComponent: Entity, IAwake, IUpdate, ILoad, IDestroy
    {
        public static TimerComponent Instance
        {
            get;
            set;
        }
        
        public long timeNow;
        public Func<long, List<long>, bool> foreachFunc;

        /// <summary>
        /// key: time, value: timer id
        /// </summary>
        public readonly MultiMap<long, long> TimeId = new MultiMap<long, long>();

        public readonly Queue<long> timeOutTime = new Queue<long>();

        public readonly Queue<long> timeOutTimerIds = new Queue<long>();
        
        public readonly Queue<long> everyFrameTimer = new Queue<long>();

        // 记录最小时间，不用每次都去MultiMap取第一个值
        public long minTime;

        public const int TimeTypeMax = 10000;

        public ITimer[] timerActions;
    }
}