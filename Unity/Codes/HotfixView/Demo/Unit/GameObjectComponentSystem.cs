using System;

namespace ET
{
    public static class GameObjectComponentSystem
    {
        [ObjectSystem]
        public class DestroySystem: DestroySystem<GameObjectComponent>
        {
            public override void Destroy(GameObjectComponent me)
            {
                UnityEngine.Object.Destroy(me.GameObject);
            }
        }
    }
}