
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[EnableMethod]
	public  class DlgEntityTreeViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button EBtnGatherButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EBtnGatherButton == null )
     			{
		    		this.m_EBtnGatherButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"BG/EBtnGather");
     			}
     			return this.m_EBtnGatherButton;
     		}
     	}

		public UnityEngine.UI.Image EBtnGatherImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EBtnGatherImage == null )
     			{
		    		this.m_EBtnGatherImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"BG/EBtnGather");
     			}
     			return this.m_EBtnGatherImage;
     		}
     	}

		public UnityEngine.UI.LoopVerticalScrollRect ELoopScrollList_EntityNameLoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELoopScrollList_EntityNameLoopVerticalScrollRect == null )
     			{
		    		this.m_ELoopScrollList_EntityNameLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"BG/ELoopScrollList_EntityName");
     			}
     			return this.m_ELoopScrollList_EntityNameLoopVerticalScrollRect;
     		}
     	}

		public UnityEngine.UI.Button EBtnCloseButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EBtnCloseButton == null )
     			{
		    		this.m_EBtnCloseButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"BG/EBtnClose");
     			}
     			return this.m_EBtnCloseButton;
     		}
     	}

		public UnityEngine.UI.Image EBtnCloseImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EBtnCloseImage == null )
     			{
		    		this.m_EBtnCloseImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"BG/EBtnClose");
     			}
     			return this.m_EBtnCloseImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EBtnGatherButton = null;
			this.m_EBtnGatherImage = null;
			this.m_ELoopScrollList_EntityNameLoopVerticalScrollRect = null;
			this.m_EBtnCloseButton = null;
			this.m_EBtnCloseImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_EBtnGatherButton = null;
		private UnityEngine.UI.Image m_EBtnGatherImage = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_ELoopScrollList_EntityNameLoopVerticalScrollRect = null;
		private UnityEngine.UI.Button m_EBtnCloseButton = null;
		private UnityEngine.UI.Image m_EBtnCloseImage = null;
		public Transform uiTransform = null;
	}
}
