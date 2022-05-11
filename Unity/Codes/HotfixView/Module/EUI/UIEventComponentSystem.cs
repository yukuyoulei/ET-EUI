using System;

namespace ET
{
    [ObjectSystem]
    public class UIEventComponentAwakeSystem : AwakeSystem<UIEventComponent>
    {
        public override void Awake(UIEventComponent me)
        {
            UIEventComponent.Instance = me;
            me.Awake();
        }
    }
    
    [ObjectSystem]
    public class UIEventComponentDestroySystem : DestroySystem<UIEventComponent>
    {
        public override void Destroy(UIEventComponent me)
        {
            me.UIEventHandlers.Clear();
            UIEventComponent.Instance = null;
        }
    }
    
    [FriendClass(typeof(UIEventComponent))]
    public static class UIEventComponentSystem
    {
        public static void Awake(this UIEventComponent me)
        {
            me.UIEventHandlers.Clear();
            foreach (Type v in Game.EventSystem.GetTypes(typeof (AUIEventAttribute)))
            {
                AUIEventAttribute attr = v.GetCustomAttributes(typeof (AUIEventAttribute), false)[0] as AUIEventAttribute;
                me.UIEventHandlers.Add(attr.WindowID, Activator.CreateInstance(v) as IAUIEventHandler);
            }
        }
        
        public static IAUIEventHandler GetUIEventHandler(this UIEventComponent me,WindowID windowID)
        {
            if (me.UIEventHandlers.TryGetValue(windowID, out IAUIEventHandler handler))
            {
                return handler;
            }
            Log.Error($"windowId : {windowID} is not have any uiEvent");
            return null;
        }
    }
}