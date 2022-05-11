using System;

namespace ET
{
    
    [ObjectSystem]
    public class UIPathComponentAwakeSystem : AwakeSystem<UIPathComponent>
    {
        public override void Awake(UIPathComponent me)
        {
            UIPathComponent.Instance = me;
            me.Awake();
        }
    }
    
    [ObjectSystem]
    public class UIPathComponentDestroySystem : DestroySystem<UIPathComponent>
    {
        public override void Destroy(UIPathComponent me)
        {
            me.WindowPrefabPath.Clear();
            me.WindowTypeIdDict.Clear();
            UIPathComponent.Instance = null;
        }
    }
    
    [FriendClass(typeof(UIPathComponent))]
    public static class UIPathComponentSystem
    {
        public static void Awake(this UIPathComponent me)
        {
            foreach (WindowID windowID in Enum.GetValues(typeof(WindowID)))
            {
                string dlgName = "Dlg" + windowID.ToString().Split('_')[1];
                me.WindowPrefabPath.Add((int)windowID , dlgName);
                me.WindowTypeIdDict.Add(dlgName, (int)windowID );
            }
        }
    }
}