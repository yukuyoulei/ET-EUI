using System;

namespace ET
{
    [ObjectSystem]
    public class CoroutineLockAwakeSystem: AwakeSystem<CoroutineLock, int, long, int>
    {
        public override void Awake(CoroutineLock me, int type, long k, int count)
        {
            me.coroutineLockType = type;
            me.key = k;
            me.level = count;
        }
    }

    [ObjectSystem]
    public class CoroutineLockDestroySystem: DestroySystem<CoroutineLock>
    {
        public override void Destroy(CoroutineLock me)
        {
            if (me.coroutineLockType != CoroutineLockType.None)
            {
                CoroutineLockComponent.Instance.RunNextCoroutine(me.coroutineLockType, me.key, me.level + 1);
            }
            else
            {
                // CoroutineLockType.None说明协程锁超时了
                Log.Error($"coroutine lock timeout: {me.coroutineLockType} {me.key} {me.level}");
            }
            me.coroutineLockType = CoroutineLockType.None;
            me.key = 0;
            me.level = 0;
        }
    }
    
    public class CoroutineLock: Entity, IAwake<int, long, int>, IDestroy
    {
        public int coroutineLockType;
        public long key;
        public int level;
    }
}