namespace ET
{
    [ObjectSystem]
    public class ConfigComponent_SetConfigLoader_Awake: AwakeSystem<ConfigComponent>
    {
        public override void Awake(ConfigComponent me)
        {
            me.ConfigLoader = new ConfigLoader();
        }
    }
}