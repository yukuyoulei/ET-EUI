using UnityEngine;

namespace ET
{
    
    [ObjectSystem]
    public class UIBaseWindowAwakeSystem : AwakeSystem<UIBaseWindow>
    {
        public override void Awake(UIBaseWindow me)
        {
            me.WindowData = me.AddChild<WindowCoreData>();
        }
    }
    
    public  static class UIBaseWindowSystem  
    {
        public static void SetRoot(this UIBaseWindow me, Transform rootTransform)
        {
            if(me.uiTransform == null)
            {
                Log.Error($"uibaseWindows {me.WindowID} uiTransform is null!!!");
                return;
            }
            if(rootTransform == null)
            {
                Log.Error($"uibaseWindows {me.WindowID} rootTransform is null!!!");
                return;
            }
            me.uiTransform.SetParent(rootTransform, false);
            me.uiTransform.transform.localScale = Vector3.one;
        }
    }
}
