
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[EnableMethod]
	public  class DlgHelperViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Text E_TipText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TipText == null )
     			{
		    		this.m_E_TipText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Tip");
     			}
     			return this.m_E_TipText;
     		}
     	}

		public UnityEngine.UI.Button EButton_SEntityButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_SEntityButton == null )
     			{
		    		this.m_EButton_SEntityButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Tip/EButton_SEntity");
     			}
     			return this.m_EButton_SEntityButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_SEntityImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_SEntityImage == null )
     			{
		    		this.m_EButton_SEntityImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Tip/EButton_SEntity");
     			}
     			return this.m_EButton_SEntityImage;
     		}
     	}

		public UnityEngine.UI.Button EButton_CEntityButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_CEntityButton == null )
     			{
		    		this.m_EButton_CEntityButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Tip/EButton_CEntity");
     			}
     			return this.m_EButton_CEntityButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_CEntityImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_CEntityImage == null )
     			{
		    		this.m_EButton_CEntityImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Tip/EButton_CEntity");
     			}
     			return this.m_EButton_CEntityImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_TipText = null;
			this.m_EButton_SEntityButton = null;
			this.m_EButton_SEntityImage = null;
			this.m_EButton_CEntityButton = null;
			this.m_EButton_CEntityImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Text m_E_TipText = null;
		private UnityEngine.UI.Button m_EButton_SEntityButton = null;
		private UnityEngine.UI.Image m_EButton_SEntityImage = null;
		private UnityEngine.UI.Button m_EButton_CEntityButton = null;
		private UnityEngine.UI.Image m_EButton_CEntityImage = null;
		public Transform uiTransform = null;
	}
}
