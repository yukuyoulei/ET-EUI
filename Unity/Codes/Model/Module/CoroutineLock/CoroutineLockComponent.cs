using System;
using System.Collections.Generic;

namespace ET
{
    [FriendClass(typeof (CoroutineLockComponent))]
    public static class CoroutineLockComponentSystem
    {
        [ObjectSystem]
        public class CoroutineLockComponentAwakeSystem: AwakeSystem<CoroutineLockComponent>
        {
            public override void Awake(CoroutineLockComponent me)
            {
                CoroutineLockComponent.Instance = me;

                me.list = new List<CoroutineLockQueueType>(CoroutineLockType.Max);
                for (int i = 0; i < CoroutineLockType.Max; ++i)
                {
                    CoroutineLockQueueType coroutineLockQueueType = me.AddChildWithId<CoroutineLockQueueType>(++me.idGenerator);
                    me.list.Add(coroutineLockQueueType);
                }

                me.foreachFunc = (k, v) =>
                {
                    if (k > me.timeNow)
                    {
                        me.minTime = k;
                        return false;
                    }

                    me.timeOutIds.Enqueue(k);
                    return true;
                };
            }
        }

        [ObjectSystem]
        public class CoroutineLockComponentDestroySystem: DestroySystem<CoroutineLockComponent>
        {
            public override void Destroy(CoroutineLockComponent me)
            {
                me.list.Clear();
                me.nextFrameRun.Clear();
                me.timers.Clear();
                me.timeOutIds.Clear();
                me.timerOutTimer.Clear();
                me.idGenerator = 0;
                me.minTime = 0;
            }
        }

        [FriendClass(typeof (CoroutineLock))]
        public class CoroutineLockComponentUpdateSystem: UpdateSystem<CoroutineLockComponent>
        {
            public override void Update(CoroutineLockComponent me)
            {
                // 检测超时的CoroutineLock
                TimeoutCheck(me);

                // 循环过程中会有对象继续加入队列
                while (me.nextFrameRun.Count > 0)
                {
                    (int coroutineLockType, long key, int count) = me.nextFrameRun.Dequeue();
                    me.Notify(coroutineLockType, key, count);
                }
            }

            private void TimeoutCheck(CoroutineLockComponent me)
            {
                // 超时的锁
                if (me.timers.Count == 0)
                {
                    return;
                }

                me.timeNow = TimeHelper.ClientFrameTime();

                if (me.timeNow < me.minTime)
                {
                    return;
                }

                me.timers.ForEachFunc(me.foreachFunc);

                me.timerOutTimer.Clear();

                while (me.timeOutIds.Count > 0)
                {
                    long time = me.timeOutIds.Dequeue();
                    var list = me.timers[time];
                    for (int i = 0; i < list.Count; ++i)
                    {
                        CoroutineLockTimer coroutineLockTimer = list[i];
                        me.timerOutTimer.Enqueue(coroutineLockTimer);
                    }

                    me.timers.Remove(time);
                }

                while (me.timerOutTimer.Count > 0)
                {
                    CoroutineLockTimer coroutineLockTimer = me.timerOutTimer.Dequeue();
                    if (coroutineLockTimer.CoroutineLockInstanceId != coroutineLockTimer.CoroutineLock.InstanceId)
                    {
                        continue;
                    }

                    CoroutineLock coroutineLock = coroutineLockTimer.CoroutineLock;
                    // 超时直接调用下一个锁
                    me.RunNextCoroutine(coroutineLock.coroutineLockType, coroutineLock.key, coroutineLock.level + 1);
                    coroutineLock.coroutineLockType = CoroutineLockType.None; // 上面调用了下一个, dispose不再调用
                }
            }
        }

        public static void RunNextCoroutine(this CoroutineLockComponent me, int coroutineLockType, long key, int level)
        {
            // 一个协程队列一帧处理超过100个,说明比较多了,打个warning,检查一下是否够正常
            if (level == 100)
            {
                Log.Warning($"too much coroutine level: {coroutineLockType} {key}");
            }

            me.nextFrameRun.Enqueue((coroutineLockType, key, level));
        }

        private static void AddTimer(this CoroutineLockComponent me, long tillTime, CoroutineLock coroutineLock)
        {
            me.timers.Add(tillTime, new CoroutineLockTimer(coroutineLock));
            if (tillTime < me.minTime)
            {
                me.minTime = tillTime;
            }
        }

        public static async ETTask<CoroutineLock> Wait(this CoroutineLockComponent me, int coroutineLockType, long key, int time = 60000)
        {
            CoroutineLockQueueType coroutineLockQueueType = me.list[coroutineLockType];

            if (!coroutineLockQueueType.TryGetValue(key, out CoroutineLockQueue queue))
            {
                coroutineLockQueueType.Add(key, me.AddChildWithId<CoroutineLockQueue>(++me.idGenerator, true));
                return me.CreateCoroutineLock(coroutineLockType, key, time, 1);
            }

            ETTask<CoroutineLock> tcs = ETTask<CoroutineLock>.Create(true);
            queue.Add(tcs, time);
            return await tcs;
        }

        private static CoroutineLock CreateCoroutineLock(this CoroutineLockComponent me, int coroutineLockType, long key, int time, int level)
        {
            CoroutineLock coroutineLock = me.AddChildWithId<CoroutineLock, int, long, int>(++me.idGenerator, coroutineLockType, key, level, true);
            if (time > 0)
            {
                me.AddTimer(TimeHelper.ClientFrameTime() + time, coroutineLock);
            }

            return coroutineLock;
        }

        public static void Notify(this CoroutineLockComponent me, int coroutineLockType, long key, int level)
        {
            CoroutineLockQueueType coroutineLockQueueType = me.list[coroutineLockType];
            if (!coroutineLockQueueType.TryGetValue(key, out CoroutineLockQueue queue))
            {
                return;
            }

            if (queue.Count == 0)
            {
                coroutineLockQueueType.Remove(key);
                return;
            }

            CoroutineLockInfo coroutineLockInfo = queue.Dequeue();
            coroutineLockInfo.Tcs.SetResult(me.CreateCoroutineLock(coroutineLockType, key, coroutineLockInfo.Time, level));
        }
    }

    public class CoroutineLockComponent: Entity, IAwake, IUpdate, IDestroy
    {
        public static CoroutineLockComponent Instance;

        public List<CoroutineLockQueueType> list;
        public Queue<(int, long, int)> nextFrameRun = new Queue<(int, long, int)>();
        public MultiMap<long, CoroutineLockTimer> timers = new MultiMap<long, CoroutineLockTimer>();
        public Queue<long> timeOutIds = new Queue<long>();
        public Queue<CoroutineLockTimer> timerOutTimer = new Queue<CoroutineLockTimer>();
        public long idGenerator;
        public long minTime;
        public long timeNow;
        public Func<long, List<CoroutineLockTimer>, bool> foreachFunc;
    }
}