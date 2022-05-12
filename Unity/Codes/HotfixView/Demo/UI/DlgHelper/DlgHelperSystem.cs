using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	public static class DlgHelperSystem
	{

		public static void RegisterUIEvent(this DlgHelper me)
		{
			me.View.EButton_SEntityButton.AddListener(me.RequestEntityTreeFromServer);
			me.View.EButton_CEntityButton.AddListener(me.GatherEntityTreeFromClient);
		}

		public static void ShowWindow(this DlgHelper me, Entity contextData = null)
		{
		}

		public static void GatherEntityTreeFromClient(this DlgHelper me)
		{
			me.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_EntityTree);
		}
		public static async void RequestEntityTreeFromServer(this DlgHelper me)
		{
			await EntityTreeHelper.Request(me.ZoneScene());
		}
	}
}
