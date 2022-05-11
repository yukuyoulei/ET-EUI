namespace ET
{
    [ObjectSystem]
    public class UnitSystem: AwakeSystem<Unit, int>
    {
        public override void Awake(Unit me, int configId)
        {
            me.ConfigId = configId;
        }
    }
}