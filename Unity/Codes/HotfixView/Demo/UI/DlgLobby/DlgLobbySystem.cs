using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	public static  class DlgLobbySystem
	{

		public static void RegisterUIEvent(this DlgLobby me)
		{
		  me.View.E_EnterMapButton.AddListener(async ()=>
		  {
			  await me.OnEnterMapClickHandler();
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
