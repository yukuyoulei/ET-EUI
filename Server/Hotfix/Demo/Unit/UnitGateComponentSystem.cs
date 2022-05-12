namespace ET
{
    public static class UnitGateComponentSystem
    {
        public class UnitGateComponentAwakeSystem : AwakeSystem<UnitGateComponent, long>
        {
            public override void Awake(UnitGateComponent me, long a)
            {
                me.GateSessionActorId = a;
            }
        }
    }
}