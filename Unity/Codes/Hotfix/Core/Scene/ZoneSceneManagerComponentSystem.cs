using System;

namespace ET
{
    [ObjectSystem]
    public class ZoneSceneManagerComponentAwakeSystem: AwakeSystem<ZoneSceneManagerComponent>
    {
        public override void Awake(ZoneSceneManagerComponent me)
        {
            ZoneSceneManagerComponent.Instance = me;
        }
    }

    [ObjectSystem]
    public class ZoneSceneManagerComponentDestroySystem: DestroySystem<ZoneSceneManagerComponent>
    {
        public override void Destroy(ZoneSceneManagerComponent me)
        {
            me.ZoneScenes.Clear();
        }
    }

    [FriendClass(typeof(ZoneSceneManagerComponent))]
    public static class ZoneSceneManagerComponentSystem
    {
        public static Scene ZoneScene(this Entity entity)
        {
            return ZoneSceneManagerComponent.Instance.Get(entity.DomainZone());
        }
        
        public static void Add(this ZoneSceneManagerComponent me, Scene zoneScene)
        {
            me.ZoneScenes.Add(zoneScene.Zone, zoneScene);
        }
        
        public static Scene Get(this ZoneSceneManagerComponent me, int zone)
        {
            me.ZoneScenes.TryGetValue(zone, out Scene scene);
            return scene;
        }
        
        public static void Remove(this ZoneSceneManagerComponent me, int zone)
        {
            me.ZoneScenes.Remove(zone);
        }
    }
}