using System.Linq;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgLobbySystem
    {

        public static void RegisterUIEvent(this DlgLobby me)
        {
            me.View.E_EnterMapButton.AddListener(() =>
            {
                //await me.OnEnterMapClickHandler();
                me.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Play);
            });

            me.View.EButtonMenuButton.AddListener(() =>
            {
                var p = me.View.EFunctionsRawImage.rectTransform.anchoredPosition;
                p.x = me.View.EFunctionsRawImage.rectTransform.sizeDelta.x;
                me.View.EFunctionsRawImage.rectTransform.anchoredPosition = p;
            });
            me.View.EButtonHideLeftButton.AddListener(() =>
            {
				var p = me.View.EFunctionsRawImage.rectTransform.anchoredPosition;
                p.x = 0;
                me.View.EFunctionsRawImage.rectTransform.anchoredPosition = p;
            });
        }

        public static void ShowWindow(this DlgLobby me, Entity contextData = null)
        {

        }

        public static async ETTask OnEnterMapClickHandler(this DlgLobby me)
        {
            await EnterMapHelper.EnterMapAsync(me.ZoneScene());
        }
    }
}
