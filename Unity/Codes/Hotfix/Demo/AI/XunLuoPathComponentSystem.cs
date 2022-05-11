using UnityEngine;

namespace ET
{
    [FriendClass(typeof(XunLuoPathComponent))]
    public static class XunLuoPathComponentSystem
    {
        public static Vector3 GetCurrent(this XunLuoPathComponent me)
        {
            return me.path[me.Index];
        }
        
        public static void MoveNext(this XunLuoPathComponent me)
        {
            me.Index = ++me.Index % me.path.Length;
        }
    }
}