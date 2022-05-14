
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[EnableMethod]
	public  class DlgPlayViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Text ETitleText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ETitleText == null )
     			{
		    		this.m_ETitleText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Sprite_BackGround/ETitle");
     			}
     			return this.m_ETitleText;
     		}
     	}

		public UnityEngine.UI.Button ECloseButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ECloseButton == null )
     			{
		    		this.m_ECloseButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Sprite_BackGround/EClose");
     			}
     			return this.m_ECloseButton;
     		}
     	}

		public UnityEngine.UI.Image ECloseImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ECloseImage == null )
     			{
		    		this.m_ECloseImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Sprite_BackGround/EClose");
     			}
     			return this.m_ECloseImage;
     		}
     	}

		public UnityEngine.UI.Button EButtonLuopanButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButtonLuopanButton == null )
     			{
		    		this.m_EButtonLuopanButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Sprite_BackGround/Center/EButtonLuopan");
     			}
     			return this.m_EButtonLuopanButton;
     		}
     	}

		public UnityEngine.UI.Image EButtonLuopanImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButtonLuopanImage == null )
     			{
		    		this.m_EButtonLuopanImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Sprite_BackGround/Center/EButtonLuopan");
     			}
     			return this.m_EButtonLuopanImage;
     		}
     	}

		public UnityEngine.UI.Button EButtonJianduiButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButtonJianduiButton == null )
     			{
		    		this.m_EButtonJianduiButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Sprite_BackGround/Center/EButtonJiandui");
     			}
     			return this.m_EButtonJianduiButton;
     		}
     	}

		public UnityEngine.UI.Image EButtonJianduiImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButtonJianduiImage == null )
     			{
		    		this.m_EButtonJianduiImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Sprite_BackGround/Center/EButtonJiandui");
     			}
     			return this.m_EButtonJianduiImage;
     		}
     	}

		public UnityEngine.UI.Button EButtonZhuxianButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButtonZhuxianButton == null )
     			{
		    		this.m_EButtonZhuxianButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Sprite_BackGround/Center/EButtonZhuxian");
     			}
     			return this.m_EButtonZhuxianButton;
     		}
     	}

		public UnityEngine.UI.Image EButtonZhuxianImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButtonZhuxianImage == null )
     			{
		    		this.m_EButtonZhuxianImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Sprite_BackGround/Center/EButtonZhuxian");
     			}
     			return this.m_EButtonZhuxianImage;
     		}
     	}

		public UnityEngine.UI.Button EButtonPataButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButtonPataButton == null )
     			{
		    		this.m_EButtonPataButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Sprite_BackGround/Center/EButtonPata");
     			}
     			return this.m_EButtonPataButton;
     		}
     	}

		public UnityEngine.UI.Image EButtonPataImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButtonPataImage == null )
     			{
		    		this.m_EButtonPataImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Sprite_BackGround/Center/EButtonPata");
     			}
     			return this.m_EButtonPataImage;
     		}
     	}

		public UnityEngine.UI.Button EButtonTansuoButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButtonTansuoButton == null )
     			{
		    		this.m_EButtonTansuoButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Sprite_BackGround/Center/EButtonTansuo");
     			}
     			return this.m_EButtonTansuoButton;
     		}
     	}

		public UnityEngine.UI.Image EButtonTansuoImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButtonTansuoImage == null )
     			{
		    		this.m_EButtonTansuoImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Sprite_BackGround/Center/EButtonTansuo");
     			}
     			return this.m_EButtonTansuoImage;
     		}
     	}

		public UnityEngine.UI.Button EButtonJingjiButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButtonJingjiButton == null )
     			{
		    		this.m_EButtonJingjiButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Sprite_BackGround/Center/EButtonJingji");
     			}
     			return this.m_EButtonJingjiButton;
     		}
     	}

		public UnityEngine.UI.Image EButtonJingjiImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButtonJingjiImage == null )
     			{
		    		this.m_EButtonJingjiImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Sprite_BackGround/Center/EButtonJingji");
     			}
     			return this.m_EButtonJingjiImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_ETitleText = null;
			this.m_ECloseButton = null;
			this.m_ECloseImage = null;
			this.m_EButtonLuopanButton = null;
			this.m_EButtonLuopanImage = null;
			this.m_EButtonJianduiButton = null;
			this.m_EButtonJianduiImage = null;
			this.m_EButtonZhuxianButton = null;
			this.m_EButtonZhuxianImage = null;
			this.m_EButtonPataButton = null;
			this.m_EButtonPataImage = null;
			this.m_EButtonTansuoButton = null;
			this.m_EButtonTansuoImage = null;
			this.m_EButtonJingjiButton = null;
			this.m_EButtonJingjiImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Text m_ETitleText = null;
		private UnityEngine.UI.Button m_ECloseButton = null;
		private UnityEngine.UI.Image m_ECloseImage = null;
		private UnityEngine.UI.Button m_EButtonLuopanButton = null;
		private UnityEngine.UI.Image m_EButtonLuopanImage = null;
		private UnityEngine.UI.Button m_EButtonJianduiButton = null;
		private UnityEngine.UI.Image m_EButtonJianduiImage = null;
		private UnityEngine.UI.Button m_EButtonZhuxianButton = null;
		private UnityEngine.UI.Image m_EButtonZhuxianImage = null;
		private UnityEngine.UI.Button m_EButtonPataButton = null;
		private UnityEngine.UI.Image m_EButtonPataImage = null;
		private UnityEngine.UI.Button m_EButtonTansuoButton = null;
		private UnityEngine.UI.Image m_EButtonTansuoImage = null;
		private UnityEngine.UI.Button m_EButtonJingjiButton = null;
		private UnityEngine.UI.Image m_EButtonJingjiImage = null;
		public Transform uiTransform = null;
	}
}
