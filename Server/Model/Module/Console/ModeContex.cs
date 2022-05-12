namespace ET
{
    [ObjectSystem]
    public class ModeContexAwakeSystem: AwakeSystem<ModeContex>
    {
        public override void Awake(ModeContex me)
        {
            me.Mode = "";
        }
    }

    [ObjectSystem]
    public class ModeContexDestroySystem: DestroySystem<ModeContex>
    {
        public override void Destroy(ModeContex me)
        {
            me.Mode = "";
        }
    }

    public class ModeContex: Entity, IAwake, IDestroy
    {
        public string Mode = "";
    }
}