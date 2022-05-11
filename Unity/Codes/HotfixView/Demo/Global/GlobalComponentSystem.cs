using UnityEngine;

namespace ET
{
    [ObjectSystem]
    public class GlobalComponentAwakeSystem: AwakeSystem<GlobalComponent>
    {
        public override void Awake(GlobalComponent me)
        {
            GlobalComponent.Instance = me;
            
            me.Global = GameObject.Find("/Global").transform;
            me.Unit = GameObject.Find("/Global/UnitRoot").transform;
            me.UI = GameObject.Find("/Global/UIRoot").transform;
            me.NormalRoot = GameObject.Find("Global/UIRoot/NormalRoot").transform;
            me.PopUpRoot = GameObject.Find("Global/UIRoot/PopUpRoot").transform;
            me.FixedRoot = GameObject.Find("Global/UIRoot/FixedRoot").transform;
            me.OtherRoot = GameObject.Find("Global/UIRoot/OtherRoot").transform;
            me.PoolRoot =  GameObject.Find("Global/PoolRoot").transform;
        }
    }
}