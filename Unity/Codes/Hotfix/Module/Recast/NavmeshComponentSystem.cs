using System;

namespace ET
{
    [FriendClass(typeof(NavmeshComponent))]
    public static class NavmeshComponentSystem
    {
        public class AwakeSystem: AwakeSystem<NavmeshComponent, Func<string, byte[]>>
        {
            public override void Awake(NavmeshComponent me, Func<string, byte[]> loader)
            {
                NavmeshComponent.Instance = me;
                me.Loader = loader;
            }
        }
        
        public static long Get(this NavmeshComponent me, string name)
        {
            long ptr;
            if (me.Navmeshs.TryGetValue(name, out ptr))
            {
                return ptr;
            }

            byte[] buffer = me.Loader(name);
            if (buffer.Length == 0)
            {
                throw new Exception($"no nav data: {name}");
            }

            ptr = Recast.RecastLoadLong(name.GetHashCode(), buffer, buffer.Length);
            me.Navmeshs[name] = ptr;

            return ptr;
        }
    }
}