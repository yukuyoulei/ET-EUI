using UnityEngine;
using System;
using System.Collections.Generic;

namespace ET
{
    [ObjectSystem]
    public class UIComponentAwakeSystem : AwakeSystem<UIComponent>
    {
        public override void Awake(UIComponent me)
        {
            me.Awake();
        }
    }
    
    [ObjectSystem]
    public class UIComponentDestroySystem : DestroySystem<UIComponent>
    {
        public override void Destroy(UIComponent me)
        {
            me.Destroy();
        }
    }

    [FriendClass(typeof(ShowWindowData))]
    [FriendClass(typeof(WindowCoreData))]
    [FriendClass(typeof(UIPathComponent))]
    [FriendClass(typeof(UIBaseWindow))]
    [FriendClass(typeof(UIComponent))]
    public static class UIComponentSystem
    {
        public static void Awake(this UIComponent me)
        {
            if (null != me.AllWindowsDic)
            {
                me.AllWindowsDic.Clear();
            }
            if (null != me.VisibleWindowsDic)
            {
                me.VisibleWindowsDic.Clear();
            }
            if (me.VisibleWindowsQueue != null)
            {
                me.VisibleWindowsQueue.Clear();
            }
            if (me.HideWindowsStack != null)
            {
                me.HideWindowsStack.Clear();
            }
        }
        
        /// <summary>
        /// 窗口是否是正在显示的 
        /// </summary>
        /// <OtherParam name="id"></OtherParam>
        /// <returns></returns>
        public static bool IsWindowVisible(this UIComponent me,WindowID id)
        {
            return me.VisibleWindowsDic.ContainsKey((int)id);
        }
        
        
        /// <summary>
        /// 隐藏最新出现的窗口
        /// </summary>
        public static void HideLastWindows(this UIComponent me)
        {
            if (me.VisibleWindowsQueue.Count <= 0)
            {
                return;
            }
            WindowID windowID  = me.VisibleWindowsQueue[me.VisibleWindowsQueue.Count - 1];
            if (!me.IsWindowVisible(windowID))
            {
               return;
            }
            me.HideWindow(windowID);
        }
        
        /// <summary>
        /// 彻底关闭最新出现的窗口
        /// </summary>
        public static void CloseLastWindows(this UIComponent me)
        {
            if (me.VisibleWindowsQueue.Count <= 0)
            {
                return;
            }
            WindowID windowID  = me.VisibleWindowsQueue[me.VisibleWindowsQueue.Count - 1];
            if (!me.IsWindowVisible(windowID))
            {
                return;
            }
            me.CloseWindow(windowID);
        }
        
        public static void ShowWindow<T>(this UIComponent me,WindowID preWindowID = WindowID.WindowID_Invaild, ShowWindowData showData = null) where T : Entity
        {
            WindowID windowsId = me.GetWindowIdByGeneric<T>();
            me.ShowWindow(windowsId,preWindowID,showData);
        }
        
        /// <summary>
        /// 现实ID指定的UI窗口
        /// </summary>
        /// <OtherParam name="id"></OtherParam>
        /// <OtherParam name="showData"></OtherParam>
        public static void ShowWindow(this UIComponent me,WindowID id, WindowID preWindowID = WindowID.WindowID_Invaild, ShowWindowData showData = null)
        {
            UIBaseWindow baseWindow = me.ReadyToShowBaseWindow(id, showData);
            if (null != baseWindow)
            {
                me.RealShowWindow(baseWindow, id, showData,preWindowID);
            }
        }

        public static async ETTask ShowWindowAsync(this UIComponent me,WindowID id,WindowID preWindowID = WindowID.WindowID_Invaild, ShowWindowData showData = null)
        {
            try
            {
                if (me.LoadingWindows.Contains(id))
                {
                    return;
                }
                UIBaseWindow baseWindow = await me.ShowBaseWindowAsync(id, showData);
                if (null != baseWindow)
                {
                    me.RealShowWindow(baseWindow, id, showData,preWindowID);
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }
        
        public static async ETTask ShowWindowAsync<T>(this UIComponent me,WindowID preWindowID = WindowID.WindowID_Invaild, ShowWindowData showData = null) where T : Entity
        {
            WindowID windowsId = me.GetWindowIdByGeneric<T>();
           await me.ShowWindowAsync(windowsId,preWindowID,showData);
        }
        
        public static void HideAndShowWindowStack(this UIComponent me,WindowID hideWindowId, WindowID showWindowId)
        {
            me.HideWindow(hideWindowId,true);
            me.ShowWindow(showWindowId,preWindowID:hideWindowId);
        }
        
        public static void HideAndShowWindowStack<T,K>(this UIComponent me) where T : Entity  where K : Entity
        {
            WindowID hideWindowId = me.GetWindowIdByGeneric<T>();
            WindowID showWindowId = me.GetWindowIdByGeneric<K>();
            me.HideAndShowWindowStack(hideWindowId,showWindowId);
        }
        
        public static async ETTask HideAndShowWindowStackAsync(this UIComponent me,WindowID hideWindowId, WindowID showWindowId)
        {
            me.HideWindow(hideWindowId,true);
            await me.ShowWindowAsync(showWindowId,preWindowID:hideWindowId);
        }
        
        public static async ETTask HideAndShowWindowStackAsync<T,K>(this UIComponent me) where T : Entity  where K : Entity
        {
            WindowID hideWindowId = me.GetWindowIdByGeneric<T>();
            WindowID showWindowId = me.GetWindowIdByGeneric<K>();
            await me.HideAndShowWindowStackAsync(hideWindowId,showWindowId);
        }
        
        
        /// <summary>
        /// 隐藏ID指定的UI窗口
        /// </summary>
        /// <OtherParam name="id"></OtherParam>
        /// <OtherParam name="onComplete"></OtherParam>
        public static void HideWindow(this UIComponent me,WindowID id,bool isPushToStack = false)
        {
            if ( !me.CheckDirectlyHide(id))
            {
                Log.Warning($"检测关闭 WindowsID: {id} 失败！");
                return;
            }

            if ( isPushToStack )
            {
                return;
            }

            if (me.HideWindowsStack.Count <= 0)
            {
                return;
            }

            WindowID preWindowID = me.HideWindowsStack.Pop(); ;
            me.ShowWindow(preWindowID);
        }
        
        public static void  HideWindow<T>(this UIComponent me,bool isPushToStack = false) where T : Entity 
        {
            WindowID hideWindowId = me.GetWindowIdByGeneric<T>();
            me.HideWindow(hideWindowId,isPushToStack);
        }
        
        
        /// <summary>
        /// 卸载指定的UI窗口实例
        /// </summary>
        /// <OtherParam name="id"></OtherParam>
        public static void UnLoadWindow(this UIComponent me,WindowID id,bool isDispose = true)
        {
            UIBaseWindow baseWindow = me.GetUIBaseWindow(id);
            if (null == baseWindow)
            {
              Log.Error($"UIBaseWindow WindowId {id} is null!!!");
              return;
            }
            UIEventComponent.Instance.GetUIEventHandler(id).BeforeUnload(baseWindow);
            if(baseWindow.IsPreLoad)
            {
                Game.Scene.GetComponent<ResourcesComponent>()?.UnloadBundle(baseWindow.UIPrefabGameObject.name.StringToAB());
                UnityEngine.Object.Destroy( baseWindow.UIPrefabGameObject);
                baseWindow.UIPrefabGameObject = null;
            }
            if (isDispose)
            {
                me.AllWindowsDic.Remove((int) id);
                me.VisibleWindowsDic.Remove((int) id);
                me.VisibleWindowsQueue.Remove(id);
                baseWindow?.Dispose();
            }
        }

        public static void  UnLoadWindow<T>(this UIComponent me) where T : Entity 
        {
            WindowID hideWindowId = me.GetWindowIdByGeneric<T>();
            me.UnLoadWindow(hideWindowId);
        }

        private static  UIBaseWindow  ReadyToShowBaseWindow(this UIComponent me,WindowID id, ShowWindowData showData = null)
        {
            UIBaseWindow baseWindow = me.GetUIBaseWindow(id);
            // 如果UI不存在开始实例化新的窗口
            if (null == baseWindow)
            {
                baseWindow = me.AddChild<UIBaseWindow>();
                baseWindow.WindowID = id;
                me.LoadBaseWindows(baseWindow);
            }
            
            if (!baseWindow.IsPreLoad)
            {
                me.LoadBaseWindows(baseWindow);
            }
            return baseWindow;
        }

        private static async ETTask<UIBaseWindow> ShowBaseWindowAsync(this UIComponent me,WindowID id, ShowWindowData showData = null)
        {
            UIBaseWindow baseWindow = me.GetUIBaseWindow(id);
            if (null == baseWindow)
            {
                if (UIPathComponent.Instance.WindowPrefabPath.ContainsKey((int)id))
                {
                    baseWindow          = me.AddChild<UIBaseWindow>();
                    baseWindow.WindowID = id;
                    await me.LoadBaseWindowsAsync(baseWindow);
                }
            }
            if (!baseWindow.IsPreLoad)
            {
                await me.LoadBaseWindowsAsync(baseWindow);
            }
            return baseWindow;
        }
        
        public static void Destroy(this UIComponent me)
        {
            me.ClearAllWindow();
        }

        private static UIBaseWindow GetUIBaseWindow(this UIComponent me,WindowID id)
        {
            if (me.AllWindowsDic.ContainsKey((int)id))
            {
                return me.AllWindowsDic[(int)id];
            }
            return null;
        }
        
        public static T GetDlgLogic<T>(this UIComponent me,bool isNeedShowState = false) where  T : Entity
        {
            WindowID windowsId = me.GetWindowIdByGeneric<T>();
            UIBaseWindow baseWindow = me.GetUIBaseWindow(windowsId);
            if ( null == baseWindow )
            {
                Log.Warning($"{windowsId} is not created!");
                return null;
            }
            if ( !baseWindow.IsPreLoad )
            {
                Log.Warning($"{windowsId} is not loaded!");
                return null;
            }

            if (isNeedShowState )
            {
                if ( !me.IsWindowVisible(windowsId) )
                {
                    Log.Warning($"{windowsId} is need show state!");
                    return null;
                }
            }
            
            return baseWindow.GetComponent<T>();
        }
        
        public static WindowID GetWindowIdByGeneric<T>(this UIComponent me) where  T : Entity
        {
            if ( UIPathComponent.Instance.WindowTypeIdDict.TryGetValue(typeof(T).Name,out int windowsId) )
            {
                return (WindowID)windowsId;
            }
            Log.Error($"{typeof(T).FullName} is not have any windowId!" );
            return  WindowID.WindowID_Invaild;
        }
        
        public static void CloseWindow(this UIComponent me,WindowID windowId)
        {
            if (!me.VisibleWindowsDic.ContainsKey((int)windowId))
            {
                return;
            }
            me.HideWindow(windowId);
            me.UnLoadWindow(windowId);
            Debug.Log("<color=magenta>## close window without PopNavigationWindow() ##</color>");
        }
        
        public static void  CloseWindow<T>(this UIComponent me) where T : Entity 
        {
            WindowID hideWindowId = me.GetWindowIdByGeneric<T>();
            me.CloseWindow(hideWindowId);
        }
        
        public static void ClearAllWindow(this UIComponent me)
        {
            if (me.AllWindowsDic == null)
            {
                return;
            }
            foreach (KeyValuePair<int, UIBaseWindow> window in me.AllWindowsDic)
            {
                UIBaseWindow baseWindow = window.Value;
                if (baseWindow == null|| baseWindow.IsDisposed)
                {
                    continue;
                }
                me.HideWindow(baseWindow.WindowID);
                me.UnLoadWindow(baseWindow.WindowID,false);
                baseWindow?.Dispose();
            }
            me.AllWindowsDic.Clear();
            me.VisibleWindowsDic.Clear();
            me.LoadingWindows.Clear();
            me.VisibleWindowsQueue.Clear();
            me.HideWindowsStack.Clear();
            me.UIBaseWindowlistCached.Clear();
        }
        
        public static void HideAllShownWindow(this UIComponent me,bool includeFixed = false)
        {
            me.UIBaseWindowlistCached.Clear();
            foreach (KeyValuePair<int, UIBaseWindow> window in me.VisibleWindowsDic)
            {
                if (window.Value.WindowData.windowType == UIWindowType.Fixed && !includeFixed)
                    continue;
                if (window.Value.IsDisposed)
                {
                    continue;
                }
                
                me.UIBaseWindowlistCached.Add((WindowID)window.Key);
                window.Value.UIPrefabGameObject?.SetActive(false);
                UIEventComponent.Instance.GetUIEventHandler(window.Value.WindowID).OnHideWindow(window.Value);
            }
            if (me.UIBaseWindowlistCached.Count > 0)
            {
                for (int i = 0; i < me.UIBaseWindowlistCached.Count; i++)
                {
                    me.VisibleWindowsDic.Remove((int)me.UIBaseWindowlistCached[i]);
                }
            }
            me.VisibleWindowsQueue.Clear();
            me.HideWindowsStack.Clear();
        }
        
        private static void RealShowWindow(this UIComponent me,UIBaseWindow baseWindow, WindowID id, ShowWindowData showData = null,WindowID preWindowID = WindowID.WindowID_Invaild)
        {
            if (baseWindow.WindowData.windowType == UIWindowType.PopUp && baseWindow.WindowID != WindowID.WindowID_MessageBox)
            {
                me.VisibleWindowsQueue.Add(id);
            }
            
            Entity contextData = showData == null ? null : showData.contextData;
            baseWindow.UIPrefabGameObject?.SetActive(true);
            UIEventComponent.Instance.GetUIEventHandler(id).OnShowWindow(baseWindow,contextData);
            
            me.VisibleWindowsDic[(int)id] = baseWindow;
            if (preWindowID != WindowID.WindowID_Invaild)
            {
                me.HideWindowsStack.Push(preWindowID);
            }
         
            Debug.Log("<color=magenta>### current Navigation window </color>" + baseWindow.WindowID.ToString());
        }
        
        private static bool CheckDirectlyHide(this UIComponent me,WindowID id)
        {
            if (!me.VisibleWindowsDic.ContainsKey((int)id))
            {
                return false;
            }

            UIBaseWindow baseWindow = me.VisibleWindowsDic[(int)id];
            if (baseWindow != null && !baseWindow.IsDisposed )
            {
                baseWindow.UIPrefabGameObject?.SetActive(false);
                UIEventComponent.Instance.GetUIEventHandler(id).OnHideWindow(baseWindow);
            }
            me.VisibleWindowsDic.Remove((int)id);
            me.VisibleWindowsQueue.Remove(id);
            return true;
        }
        
        /// <summary>
        /// 同步加载
        /// </summary>
        private static void LoadBaseWindows(this UIComponent me,  UIBaseWindow baseWindow)
        {
            if ( !UIPathComponent.Instance.WindowPrefabPath.TryGetValue((int)baseWindow.WindowID,out string value) )
            {
                Log.Error($"{baseWindow.WindowID} uiPath is not Exist!");
                return;
            }
            ResourcesComponent.Instance.LoadBundle(value.StringToAB());
            GameObject go                      = ResourcesComponent.Instance.GetAsset(value.StringToAB(), value ) as GameObject;
            baseWindow.UIPrefabGameObject      = UnityEngine.Object.Instantiate(go);
            baseWindow.UIPrefabGameObject.name = go.name;
            
            UIEventComponent.Instance.GetUIEventHandler(baseWindow.WindowID).OnInitWindowCoreData(baseWindow);
            
            baseWindow?.SetRoot(EUIRootHelper.GetTargetRoot(baseWindow.WindowData.windowType));
            baseWindow.uiTransform.SetAsLastSibling();
            
            UIEventComponent.Instance.GetUIEventHandler(baseWindow.WindowID).OnInitComponent(baseWindow);
            UIEventComponent.Instance.GetUIEventHandler(baseWindow.WindowID).OnRegisterUIEvent(baseWindow);
            
            me.AllWindowsDic[(int)baseWindow.WindowID] = baseWindow;
        }

        /// <summary>
        /// 异步加载
        /// </summary>
        private static async ETTask LoadBaseWindowsAsync(this UIComponent me,  UIBaseWindow baseWindow)
        {
            
            if ( !UIPathComponent.Instance.WindowPrefabPath.TryGetValue((int)baseWindow.WindowID,out string value) )
            {
                Log.Error($"{baseWindow.WindowID} is not Exist!");
                return;
            }
            me.LoadingWindows.Add(baseWindow.WindowID);
            await ResourcesComponent.Instance.LoadBundleAsync(value.StringToAB());
            GameObject go                      = ResourcesComponent.Instance.GetAsset(value.StringToAB(), value ) as GameObject;
            baseWindow.UIPrefabGameObject      = UnityEngine.Object.Instantiate(go);
            baseWindow.UIPrefabGameObject.name = go.name;
            
            UIEventComponent.Instance.GetUIEventHandler(baseWindow.WindowID).OnInitWindowCoreData(baseWindow);
            
            baseWindow?.SetRoot(EUIRootHelper.GetTargetRoot(baseWindow.WindowData.windowType));
            baseWindow.uiTransform.SetAsLastSibling();
            
            UIEventComponent.Instance.GetUIEventHandler(baseWindow.WindowID).OnInitComponent(baseWindow);
            UIEventComponent.Instance.GetUIEventHandler(baseWindow.WindowID).OnRegisterUIEvent(baseWindow);
            
            me.AllWindowsDic[(int)baseWindow.WindowID] = baseWindow;
            me.LoadingWindows.Remove(baseWindow.WindowID);
        }
       

    }
}