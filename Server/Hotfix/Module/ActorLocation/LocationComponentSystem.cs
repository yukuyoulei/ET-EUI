namespace ET
{
    [ObjectSystem]
    public class LockInfoAwakeSystem: AwakeSystem<LockInfo, long, CoroutineLock>
    {
        public override void Awake(LockInfo me, long lockInstanceId, CoroutineLock coroutineLock)
        {
            me.CoroutineLock = coroutineLock;
            me.LockInstanceId = lockInstanceId;
        }
    }
    
    [ObjectSystem]
    public class LockInfoDestroySystem: DestroySystem<LockInfo>
    {
        public override void Destroy(LockInfo me)
        {
            me.CoroutineLock.Dispose();
            me.LockInstanceId = 0;
        }
    }
    
    [FriendClass(typeof(LocationComponent))]
    [FriendClass(typeof(LockInfo))]
    public static class LocationComponentSystem
    {
        public static async ETTask Add(this LocationComponent me, long key, long instanceId)
        {
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.Location, key))
            {
                me.locations[key] = instanceId;
                Log.Info($"location add key: {key} instanceId: {instanceId}");
            }
        }

        public static async ETTask Remove(this LocationComponent me, long key)
        {
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.Location, key))
            {
                me.locations.Remove(key);
                Log.Info($"location remove key: {key}");
            }
        }

        public static async ETTask Lock(this LocationComponent me, long key, long instanceId, int time = 0)
        {
            CoroutineLock coroutineLock = await CoroutineLockComponent.Instance.Wait(CoroutineLockType.Location, key);

            LockInfo lockInfo = me.AddChild<LockInfo, long, CoroutineLock>(instanceId, coroutineLock);
            me.lockInfos.Add(key, lockInfo);

            Log.Info($"location lock key: {key} instanceId: {instanceId}");

            if (time > 0)
            {
                long lockInfoInstanceId = lockInfo.InstanceId;
                await TimerComponent.Instance.WaitAsync(time);
                if (lockInfo.InstanceId != lockInfoInstanceId)
                {
                    return;
                }

                me.UnLock(key, instanceId, instanceId);
            }
        }

        public static void UnLock(this LocationComponent me, long key, long oldInstanceId, long newInstanceId)
        {
            if (!me.lockInfos.TryGetValue(key, out LockInfo lockInfo))
            {
                Log.Error($"location unlock not found key: {key} {oldInstanceId}");
                return;
            }

            if (oldInstanceId != lockInfo.LockInstanceId)
            {
                Log.Error($"location unlock oldInstanceId is different: {key} {oldInstanceId}");
                return;
            }

            Log.Info($"location unlock key: {key} instanceId: {oldInstanceId} newInstanceId: {newInstanceId}");

            me.locations[key] = newInstanceId;

            me.lockInfos.Remove(key);

            // 解锁
            lockInfo.Dispose();
        }

        public static async ETTask<long> Get(this LocationComponent me, long key)
        {
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.Location, key))
            {
                me.locations.TryGetValue(key, out long instanceId);
                Log.Info($"location get key: {key} instanceId: {instanceId}");
                return instanceId;
            }
        }
    }
}