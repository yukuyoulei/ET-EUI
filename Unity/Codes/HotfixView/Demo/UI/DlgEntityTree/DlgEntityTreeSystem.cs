using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    [FriendClass(typeof(DlgEntityTree))]
    public static class DlgEntityTreeSystem
    {
        public static void RegisterUIEvent(this DlgEntityTree me)
        {
            me.View.EBtnGatherButton.AddListener(() =>
            {
                me.OnGather();
            });

            me.View.ELoopScrollList_EntityNameLoopVerticalScrollRect.AddItemRefreshListener((tr, idx) =>
            {
                var item = me.dItems[idx].BindTrans(tr);
                item.ELabel_NameText.SetText(me.lEntityNames[idx]);
            });

            me.View.EBtnCloseButton.AddListener(() =>
            {
                me.ZoneScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_EntityTree);
            });
        }

        public static void OnGather(this DlgEntityTree me)
        {
            var tree = EntityTreeHelper.GetClientEntity();
            me.lEntityNames = new List<string>();
            foreach (var node in tree)
            {
                me.lEntityNames.Add($"[{node.layer}]" + GetTab(node.layer) + node.node);
            }

            me.RemoveUIScrollItems(ref me.dItems);
            me.dItems = new Dictionary<int, Scroll_Item_EntityName>();
            me.AddUIScrollItems(ref me.dItems, me.lEntityNames.Count);
            me.View.ELoopScrollList_EntityNameLoopVerticalScrollRect.SetVisible(true, me.lEntityNames.Count);
            Log.Info($"me.lEntityNames.Count {me.lEntityNames.Count}");
        }
        public static void ShowWindow(this DlgEntityTree me, Entity contextData = null)
        {
            me.OnGather();
        }

        public static void HideWindow(this DlgEntityTree me, Entity contextData = null)
        {
            me.RemoveUIScrollItems(ref me.dItems);
        }

        private static string GetTab(int tabcount)
        {
            var res = "";
            for (var i = 0; i < tabcount; i++)
                res += "  ";
            return res;
        }

    }
}
