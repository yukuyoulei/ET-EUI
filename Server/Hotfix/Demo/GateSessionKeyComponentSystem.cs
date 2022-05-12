namespace ET
{
    [FriendClass(typeof(GateSessionKeyComponent))]
    public static class GateSessionKeyComponentSystem
    {
        public static void Add(this GateSessionKeyComponent me, long key, string account)
        {
            me.sessionKey.Add(key, account);
            me.TimeoutRemoveKey(key).Coroutine();
        }

        public static string Get(this GateSessionKeyComponent me, long key)
        {
            string account = null;
            me.sessionKey.TryGetValue(key, out account);
            return account;
        }

        public static void Remove(this GateSessionKeyComponent me, long key)
        {
            me.sessionKey.Remove(key);
        }

        private static async ETTask TimeoutRemoveKey(this GateSessionKeyComponent me, long key)
        {
            await TimerComponent.Instance.WaitAsync(20000);
            me.sessionKey.Remove(key);
        }
    }
}