using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	[FriendClass(typeof(DlgRedDot))]
	public static  class DlgRedDotSystem
	{

		public static void RegisterUIEvent(this DlgRedDot me)
		{

			
		}

		public static void ShowWindow(this DlgRedDot me, Entity contextData = null)
		{
			
		}

		public static void OnBagNode1ClickHandler(this DlgRedDot me)
		{
			me.RedDotBagCount1 += 1;

		}
		 
		public static void OnBagNode2ClickHandler(this DlgRedDot me)
		{
			me.RedDotBagCount2 += 1;

		}
		
		
		public static void OnMailNode1ClickHandler(this DlgRedDot me)
		{
			me.RedDotMailCount1 += 1;
	
		}
		 
		public static void OnMailNode2ClickHandler(this DlgRedDot me)
		{
			me.RedDotMailCount2 += 1;

		}
		
	}
}
