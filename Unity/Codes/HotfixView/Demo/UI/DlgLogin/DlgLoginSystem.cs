using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ET
{
	public static  class DlgLoginSystem
	{

		public static void RegisterUIEvent(this DlgLogin me)
		{
			me.View.E_LoginButton.AddListener(() => { me.OnLoginClickHandler();});
		}

		public static void ShowWindow(this DlgLogin me, Entity contextData = null)
		{
			
		}
		
		public static void OnLoginClickHandler(this DlgLogin me)
		{
			LoginHelper.Login(
				me.DomainScene(), 
				ConstValue.LoginAddress, 
				me.View.E_AccountInputField.GetComponent<InputField>().text, 
				me.View.E_PasswordInputField.GetComponent<InputField>().text).Coroutine();
		}
		
		public static void HideWindow(this DlgLogin me)
		{

		}
		
	}
}
