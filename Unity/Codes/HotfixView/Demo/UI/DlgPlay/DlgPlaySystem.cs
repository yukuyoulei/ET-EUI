using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    [FriendClass(typeof(DlgPlay))]
    public static class DlgPlaySystem
    {

        public static void RegisterUIEvent(this DlgPlay me)
        {
            me.View.ECloseButton.AddListener(() =>
            {
                me.View.ZoneScene().GetComponent<UIComponent>().HideWindow<DlgPlay>();
            });

        }

        public static void ShowWindow(this DlgPlay me, Entity contextData = null)
        {
        }



    }
}
