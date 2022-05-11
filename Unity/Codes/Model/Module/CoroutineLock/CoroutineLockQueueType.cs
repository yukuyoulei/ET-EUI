using System.Collections.Generic;

namespace ET
{
    [FriendClass(typeof(CoroutineLockQueueType))]
    public static class CoroutineLockQueueTypeSystem
    {
        [ObjectSystem]
        public class CoroutineLockQueueTypeAwakeSystem: AwakeSystem<CoroutineLockQueueType>
        {
            public override void Awake(CoroutineLockQueueType me)
            {
                if (me.dictionary == null)
                {
                    me.dictionary = new Dictionary<long, CoroutineLockQueue>();
                }

                me.dictionary.Clear();
            }
        }

        [ObjectSystem]
        public class CoroutineLockQueueTypeDestroySystem: DestroySystem<CoroutineLockQueueType>
        {
            public override void Destroy(CoroutineLockQueueType me)
            {
                me.dictionary.Clear();
            }
        }
        
        public static bool TryGetValue(this CoroutineLockQueueType me, long key, out CoroutineLockQueue value)
        {
            return me.dictionary.TryGetValue(key, out value);
        }

        public static void Remove(this CoroutineLockQueueType me, long key)
        {
            if (me.dictionary.TryGetValue(key, out CoroutineLockQueue value))
            {
                value.Dispose();
            }
            me.dictionary.Remove(key);
        }
        
        public static void Add(this CoroutineLockQueueType me, long key, CoroutineLockQueue value)
        {
            me.dictionary.Add(key, value);
        }
    }
    
    public class CoroutineLockQueueType: Entity, IAwake, IDestroy
    {
        public Dictionary<long, CoroutineLockQueue> dictionary;
    }
}