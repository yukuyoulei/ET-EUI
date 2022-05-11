using System;

namespace ET
{
    public static class CurrentScenesComponentSystem
    {
        [ObjectSystem]
        public class CurrentScenesComponentAwakeSystem: AwakeSystem<CurrentScenesComponent>
        {
            public override void Awake(CurrentScenesComponent me)
            {
            }
        }

        public static Scene CurrentScene(this Scene zoneScene)
        {
            return zoneScene.GetComponent<CurrentScenesComponent>()?.Scene;
        }
    }
}