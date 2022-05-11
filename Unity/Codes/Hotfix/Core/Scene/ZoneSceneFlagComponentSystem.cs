namespace ET
{
    [ObjectSystem]
    public class ZoneSceneFlagComponentDestroySystem: DestroySystem<ZoneSceneFlagComponent>
    {
        public override void Destroy(ZoneSceneFlagComponent me)
        {
            ZoneSceneManagerComponent.Instance.Remove(me.DomainZone());
        }
    }

    [ObjectSystem]
    public class ZoneSceneFlagComponentAwakeSystem: AwakeSystem<ZoneSceneFlagComponent>
    {
        public override void Awake(ZoneSceneFlagComponent me)
        {
            ZoneSceneManagerComponent.Instance.Add(me.GetParent<Scene>());
        }
    }
}