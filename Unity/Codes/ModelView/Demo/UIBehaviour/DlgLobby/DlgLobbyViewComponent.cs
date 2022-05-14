
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[EnableMethod]
	public  class DlgLobbyViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.RectTransform EGBackGroundRectTransform
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EGBackGroundRectTransform == null )
     			{
		    		this.m_EGBackGroundRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject,"EGBackGround");
     			}
     			return this.m_EGBackGroundRectTransform;
     		}
     	}

		public UnityEngine.UI.Button E_EnterMapButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_EnterMapButton == null )
     			{
		    		this.m_E_EnterMapButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/RightBottom/E_EnterMap");
     			}
     			return this.m_E_EnterMapButton;
     		}
     	}

		public UnityEngine.UI.Image E_EnterMapImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_EnterMapImage == null )
     			{
		    		this.m_E_EnterMapImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/RightBottom/E_EnterMap");
     			}
     			return this.m_E_EnterMapImage;
     		}
     	}

		public UnityEngine.UI.Button EHeroesButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EHeroesButton == null )
     			{
		    		this.m_EHeroesButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/LeftBottom/EHeroes");
     			}
     			return this.m_EHeroesButton;
     		}
     	}

		public UnityEngine.UI.Image EHeroesImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EHeroesImage == null )
     			{
		    		this.m_EHeroesImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/LeftBottom/EHeroes");
     			}
     			return this.m_EHeroesImage;
     		}
     	}

		public UnityEngine.UI.Button EPicsButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EPicsButton == null )
     			{
		    		this.m_EPicsButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/LeftBottom/EPics");
     			}
     			return this.m_EPicsButton;
     		}
     	}

		public UnityEngine.UI.Image EPicsImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EPicsImage == null )
     			{
		    		this.m_EPicsImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/LeftBottom/EPics");
     			}
     			return this.m_EPicsImage;
     		}
     	}

		public UnityEngine.UI.Image EImgProcessFillImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EImgProcessFillImage == null )
     			{
		    		this.m_EImgProcessFillImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/CenterBottom/Process/EImgProcessFill");
     			}
     			return this.m_EImgProcessFillImage;
     		}
     	}

		public UnityEngine.UI.Text ETextProcessText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ETextProcessText == null )
     			{
		    		this.m_ETextProcessText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"EGBackGround/CenterBottom/Process/ETextProcess");
     			}
     			return this.m_ETextProcessText;
     		}
     	}

		public UnityEngine.UI.Button EButtonMenuButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButtonMenuButton == null )
     			{
		    		this.m_EButtonMenuButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/LeftTop/EButtonMenu");
     			}
     			return this.m_EButtonMenuButton;
     		}
     	}

		public UnityEngine.UI.Image EButtonMenuImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButtonMenuImage == null )
     			{
		    		this.m_EButtonMenuImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/LeftTop/EButtonMenu");
     			}
     			return this.m_EButtonMenuImage;
     		}
     	}

		public UnityEngine.UI.Text ELabelDiamond1Text
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabelDiamond1Text == null )
     			{
		    		this.m_ELabelDiamond1Text = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"EGBackGround/LeftTop/DiamondBg/ELabelDiamond1");
     			}
     			return this.m_ELabelDiamond1Text;
     		}
     	}

		public UnityEngine.UI.Text ELabelCoinText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabelCoinText == null )
     			{
		    		this.m_ELabelCoinText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"EGBackGround/LeftTop/CoinBg/ELabelCoin");
     			}
     			return this.m_ELabelCoinText;
     		}
     	}

		public UnityEngine.UI.Button EButtonAddCoinButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButtonAddCoinButton == null )
     			{
		    		this.m_EButtonAddCoinButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/LeftTop/CoinBg/EButtonAddCoin");
     			}
     			return this.m_EButtonAddCoinButton;
     		}
     	}

		public UnityEngine.UI.Image EButtonAddCoinImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButtonAddCoinImage == null )
     			{
		    		this.m_EButtonAddCoinImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/LeftTop/CoinBg/EButtonAddCoin");
     			}
     			return this.m_EButtonAddCoinImage;
     		}
     	}

		public UnityEngine.UI.Button EButtonStoreButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButtonStoreButton == null )
     			{
		    		this.m_EButtonStoreButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/LeftTop/LeftButtons/EButtonStore");
     			}
     			return this.m_EButtonStoreButton;
     		}
     	}

		public UnityEngine.UI.Image EButtonStoreImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButtonStoreImage == null )
     			{
		    		this.m_EButtonStoreImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/LeftTop/LeftButtons/EButtonStore");
     			}
     			return this.m_EButtonStoreImage;
     		}
     	}

		public UnityEngine.UI.Button EButtonGiftButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButtonGiftButton == null )
     			{
		    		this.m_EButtonGiftButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/LeftTop/LeftButtons/EButtonGift");
     			}
     			return this.m_EButtonGiftButton;
     		}
     	}

		public UnityEngine.UI.Image EButtonGiftImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButtonGiftImage == null )
     			{
		    		this.m_EButtonGiftImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/LeftTop/LeftButtons/EButtonGift");
     			}
     			return this.m_EButtonGiftImage;
     		}
     	}

		public UnityEngine.UI.Button EButtonCouponButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButtonCouponButton == null )
     			{
		    		this.m_EButtonCouponButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/LeftTop/LeftButtons/EButtonCoupon");
     			}
     			return this.m_EButtonCouponButton;
     		}
     	}

		public UnityEngine.UI.Image EButtonCouponImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButtonCouponImage == null )
     			{
		    		this.m_EButtonCouponImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/LeftTop/LeftButtons/EButtonCoupon");
     			}
     			return this.m_EButtonCouponImage;
     		}
     	}

		public UnityEngine.UI.Image EMiniMapImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EMiniMapImage == null )
     			{
		    		this.m_EMiniMapImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/RightTop/EMiniMap");
     			}
     			return this.m_EMiniMapImage;
     		}
     	}

		public UnityEngine.UI.Text EChapterText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EChapterText == null )
     			{
		    		this.m_EChapterText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"EGBackGround/RightTop/EMiniMap/EChapter");
     			}
     			return this.m_EChapterText;
     		}
     	}

		public UnityEngine.UI.Text ELabelLevelText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabelLevelText == null )
     			{
		    		this.m_ELabelLevelText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"EGBackGround/RightTop/EMiniMap/LevelBg/ELabelLevel");
     			}
     			return this.m_ELabelLevelText;
     		}
     	}

		public UnityEngine.UI.Text ELabelHeartCountText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabelHeartCountText == null )
     			{
		    		this.m_ELabelHeartCountText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"EGBackGround/RightTop/HeartBg/ELabelHeartCount");
     			}
     			return this.m_ELabelHeartCountText;
     		}
     	}

		public UnityEngine.UI.Text ELabelHeartFullText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabelHeartFullText == null )
     			{
		    		this.m_ELabelHeartFullText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"EGBackGround/RightTop/HeartBg/ELabelHeartFull");
     			}
     			return this.m_ELabelHeartFullText;
     		}
     	}

		public UnityEngine.UI.RawImage EFunctionsRawImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EFunctionsRawImage == null )
     			{
		    		this.m_EFunctionsRawImage = UIFindHelper.FindDeepChild<UnityEngine.UI.RawImage>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions");
     			}
     			return this.m_EFunctionsRawImage;
     		}
     	}

		public UnityEngine.UI.Button EButtonHideLeftButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButtonHideLeftButton == null )
     			{
		    		this.m_EButtonHideLeftButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/leftpadding/EButtonHideLeft");
     			}
     			return this.m_EButtonHideLeftButton;
     		}
     	}

		public UnityEngine.UI.Image EButtonHideLeftImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButtonHideLeftImage == null )
     			{
		    		this.m_EButtonHideLeftImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/leftpadding/EButtonHideLeft");
     			}
     			return this.m_EButtonHideLeftImage;
     		}
     	}

		public UnityEngine.UI.Button EButtonSettingsButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButtonSettingsButton == null )
     			{
		    		this.m_EButtonSettingsButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/leftpadding/EButtonSettings");
     			}
     			return this.m_EButtonSettingsButton;
     		}
     	}

		public UnityEngine.UI.Image EButtonSettingsImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButtonSettingsImage == null )
     			{
		    		this.m_EButtonSettingsImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/leftpadding/EButtonSettings");
     			}
     			return this.m_EButtonSettingsImage;
     		}
     	}

		public UnityEngine.UI.Button EButtonRoleButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButtonRoleButton == null )
     			{
		    		this.m_EButtonRoleButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/leftpadding/EButtonRole");
     			}
     			return this.m_EButtonRoleButton;
     		}
     	}

		public UnityEngine.UI.Image EButtonRoleImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButtonRoleImage == null )
     			{
		    		this.m_EButtonRoleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/leftpadding/EButtonRole");
     			}
     			return this.m_EButtonRoleImage;
     		}
     	}

		public UnityEngine.UI.Button EButtonMailButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButtonMailButton == null )
     			{
		    		this.m_EButtonMailButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/leftpadding/EButtonMail");
     			}
     			return this.m_EButtonMailButton;
     		}
     	}

		public UnityEngine.UI.Image EButtonMailImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButtonMailImage == null )
     			{
		    		this.m_EButtonMailImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/leftpadding/EButtonMail");
     			}
     			return this.m_EButtonMailImage;
     		}
     	}

		public UnityEngine.UI.Button EButtonCalendarButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButtonCalendarButton == null )
     			{
		    		this.m_EButtonCalendarButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/leftpadding/EButtonCalendar");
     			}
     			return this.m_EButtonCalendarButton;
     		}
     	}

		public UnityEngine.UI.Image EButtonCalendarImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButtonCalendarImage == null )
     			{
		    		this.m_EButtonCalendarImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/leftpadding/EButtonCalendar");
     			}
     			return this.m_EButtonCalendarImage;
     		}
     	}

		public UnityEngine.UI.Image ERoleIconImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ERoleIconImage == null )
     			{
		    		this.m_ERoleIconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/RoleInfo/ERoleIcon");
     			}
     			return this.m_ERoleIconImage;
     		}
     	}

		public UnityEngine.UI.Text ERoleNameText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ERoleNameText == null )
     			{
		    		this.m_ERoleNameText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/RoleInfo/ERoleName");
     			}
     			return this.m_ERoleNameText;
     		}
     	}

		public UnityEngine.UI.Text ERoleIDText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ERoleIDText == null )
     			{
		    		this.m_ERoleIDText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/RoleInfo/ERoleID");
     			}
     			return this.m_ERoleIDText;
     		}
     	}

		public UnityEngine.UI.Button EButtonCopyInfoButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButtonCopyInfoButton == null )
     			{
		    		this.m_EButtonCopyInfoButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/RoleInfo/ERoleID/EButtonCopyInfo");
     			}
     			return this.m_EButtonCopyInfoButton;
     		}
     	}

		public UnityEngine.UI.Image EButtonCopyInfoImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButtonCopyInfoImage == null )
     			{
		    		this.m_EButtonCopyInfoImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/RoleInfo/ERoleID/EButtonCopyInfo");
     			}
     			return this.m_EButtonCopyInfoImage;
     		}
     	}

		public UnityEngine.UI.Text ELabelDiamond2Text
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabelDiamond2Text == null )
     			{
		    		this.m_ELabelDiamond2Text = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/RoleInfo/DiamondBg/ELabelDiamond2");
     			}
     			return this.m_ELabelDiamond2Text;
     		}
     	}

		public UnityEngine.UI.Image ERewardMaskImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ERewardMaskImage == null )
     			{
		    		this.m_ERewardMaskImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/RoleInfo/RawImage/Image/ERewardMask");
     			}
     			return this.m_ERewardMaskImage;
     		}
     	}

		public UnityEngine.UI.Button EFuncHeroButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EFuncHeroButton == null )
     			{
		    		this.m_EFuncHeroButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/Functions/Scroll View/Viewport/Content/EFuncHero");
     			}
     			return this.m_EFuncHeroButton;
     		}
     	}

		public UnityEngine.UI.Image EFuncHeroImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EFuncHeroImage == null )
     			{
		    		this.m_EFuncHeroImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/Functions/Scroll View/Viewport/Content/EFuncHero");
     			}
     			return this.m_EFuncHeroImage;
     		}
     	}

		public UnityEngine.UI.Button EFuncPicsButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EFuncPicsButton == null )
     			{
		    		this.m_EFuncPicsButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/Functions/Scroll View/Viewport/Content/EFuncPics");
     			}
     			return this.m_EFuncPicsButton;
     		}
     	}

		public UnityEngine.UI.Image EFuncPicsImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EFuncPicsImage == null )
     			{
		    		this.m_EFuncPicsImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/Functions/Scroll View/Viewport/Content/EFuncPics");
     			}
     			return this.m_EFuncPicsImage;
     		}
     	}

		public UnityEngine.UI.Button EFuncLuopanButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EFuncLuopanButton == null )
     			{
		    		this.m_EFuncLuopanButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/Functions/Scroll View/Viewport/Content/EFuncLuopan");
     			}
     			return this.m_EFuncLuopanButton;
     		}
     	}

		public UnityEngine.UI.Image EFuncLuopanImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EFuncLuopanImage == null )
     			{
		    		this.m_EFuncLuopanImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/Functions/Scroll View/Viewport/Content/EFuncLuopan");
     			}
     			return this.m_EFuncLuopanImage;
     		}
     	}

		public UnityEngine.UI.Button EFuncKongjianButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EFuncKongjianButton == null )
     			{
		    		this.m_EFuncKongjianButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/Functions/Scroll View/Viewport/Content/EFuncKongjian");
     			}
     			return this.m_EFuncKongjianButton;
     		}
     	}

		public UnityEngine.UI.Image EFuncKongjianImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EFuncKongjianImage == null )
     			{
		    		this.m_EFuncKongjianImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/Functions/Scroll View/Viewport/Content/EFuncKongjian");
     			}
     			return this.m_EFuncKongjianImage;
     		}
     	}

		public UnityEngine.UI.Button EFuncTiaozhanButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EFuncTiaozhanButton == null )
     			{
		    		this.m_EFuncTiaozhanButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/Functions/Scroll View/Viewport/Content/EFuncTiaozhan");
     			}
     			return this.m_EFuncTiaozhanButton;
     		}
     	}

		public UnityEngine.UI.Image EFuncTiaozhanImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EFuncTiaozhanImage == null )
     			{
		    		this.m_EFuncTiaozhanImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/Functions/Scroll View/Viewport/Content/EFuncTiaozhan");
     			}
     			return this.m_EFuncTiaozhanImage;
     		}
     	}

		public UnityEngine.UI.Button EFuncDituButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EFuncDituButton == null )
     			{
		    		this.m_EFuncDituButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/Functions/Scroll View/Viewport/Content/EFuncDitu");
     			}
     			return this.m_EFuncDituButton;
     		}
     	}

		public UnityEngine.UI.Image EFuncDituImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EFuncDituImage == null )
     			{
		    		this.m_EFuncDituImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/Functions/Scroll View/Viewport/Content/EFuncDitu");
     			}
     			return this.m_EFuncDituImage;
     		}
     	}

		public UnityEngine.UI.Button EFuncRenwuButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EFuncRenwuButton == null )
     			{
		    		this.m_EFuncRenwuButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/Functions/Scroll View/Viewport/Content/EFuncRenwu");
     			}
     			return this.m_EFuncRenwuButton;
     		}
     	}

		public UnityEngine.UI.Image EFuncRenwuImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EFuncRenwuImage == null )
     			{
		    		this.m_EFuncRenwuImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/Functions/Scroll View/Viewport/Content/EFuncRenwu");
     			}
     			return this.m_EFuncRenwuImage;
     		}
     	}

		public UnityEngine.UI.Button EFuncJuqingButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EFuncJuqingButton == null )
     			{
		    		this.m_EFuncJuqingButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/Functions/Scroll View/Viewport/Content/EFuncJuqing");
     			}
     			return this.m_EFuncJuqingButton;
     		}
     	}

		public UnityEngine.UI.Image EFuncJuqingImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EFuncJuqingImage == null )
     			{
		    		this.m_EFuncJuqingImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/Functions/Scroll View/Viewport/Content/EFuncJuqing");
     			}
     			return this.m_EFuncJuqingImage;
     		}
     	}

		public UnityEngine.UI.Button EFuncYaoqingButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EFuncYaoqingButton == null )
     			{
		    		this.m_EFuncYaoqingButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/Functions/Scroll View/Viewport/Content/EFuncYaoqing");
     			}
     			return this.m_EFuncYaoqingButton;
     		}
     	}

		public UnityEngine.UI.Image EFuncYaoqingImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EFuncYaoqingImage == null )
     			{
		    		this.m_EFuncYaoqingImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/Functions/Scroll View/Viewport/Content/EFuncYaoqing");
     			}
     			return this.m_EFuncYaoqingImage;
     		}
     	}

		public UnityEngine.UI.Button EFuncShejiaoButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EFuncShejiaoButton == null )
     			{
		    		this.m_EFuncShejiaoButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/Functions/Scroll View/Viewport/Content/EFuncShejiao");
     			}
     			return this.m_EFuncShejiaoButton;
     		}
     	}

		public UnityEngine.UI.Image EFuncShejiaoImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EFuncShejiaoImage == null )
     			{
		    		this.m_EFuncShejiaoImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/Functions/Scroll View/Viewport/Content/EFuncShejiao");
     			}
     			return this.m_EFuncShejiaoImage;
     		}
     	}

		public UnityEngine.UI.Button EFuncLiwuButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EFuncLiwuButton == null )
     			{
		    		this.m_EFuncLiwuButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/Functions/Scroll View/Viewport/Content/EFuncLiwu");
     			}
     			return this.m_EFuncLiwuButton;
     		}
     	}

		public UnityEngine.UI.Image EFuncLiwuImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EFuncLiwuImage == null )
     			{
		    		this.m_EFuncLiwuImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/Functions/Scroll View/Viewport/Content/EFuncLiwu");
     			}
     			return this.m_EFuncLiwuImage;
     		}
     	}

		public UnityEngine.UI.Button EFuncPaihangButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EFuncPaihangButton == null )
     			{
		    		this.m_EFuncPaihangButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/Functions/Scroll View/Viewport/Content/EFuncPaihang");
     			}
     			return this.m_EFuncPaihangButton;
     		}
     	}

		public UnityEngine.UI.Image EFuncPaihangImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EFuncPaihangImage == null )
     			{
		    		this.m_EFuncPaihangImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/Functions/Scroll View/Viewport/Content/EFuncPaihang");
     			}
     			return this.m_EFuncPaihangImage;
     		}
     	}

		public UnityEngine.UI.Button EFuncShangchengButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EFuncShangchengButton == null )
     			{
		    		this.m_EFuncShangchengButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/Functions/Scroll View/Viewport/Content/EFuncShangcheng");
     			}
     			return this.m_EFuncShangchengButton;
     		}
     	}

		public UnityEngine.UI.Image EFuncShangchengImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EFuncShangchengImage == null )
     			{
		    		this.m_EFuncShangchengImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/Functions/Scroll View/Viewport/Content/EFuncShangcheng");
     			}
     			return this.m_EFuncShangchengImage;
     		}
     	}

		public UnityEngine.UI.Button EFuncHuodongButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EFuncHuodongButton == null )
     			{
		    		this.m_EFuncHuodongButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/Functions/Scroll View/Viewport/Content/EFuncHuodong");
     			}
     			return this.m_EFuncHuodongButton;
     		}
     	}

		public UnityEngine.UI.Image EFuncHuodongImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EFuncHuodongImage == null )
     			{
		    		this.m_EFuncHuodongImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/Functions/Scroll View/Viewport/Content/EFuncHuodong");
     			}
     			return this.m_EFuncHuodongImage;
     		}
     	}

		public UnityEngine.UI.Button EFuncChuqianguanButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EFuncChuqianguanButton == null )
     			{
		    		this.m_EFuncChuqianguanButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/Functions/Scroll View/Viewport/Content/EFuncChuqianguan");
     			}
     			return this.m_EFuncChuqianguanButton;
     		}
     	}

		public UnityEngine.UI.Image EFuncChuqianguanImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EFuncChuqianguanImage == null )
     			{
		    		this.m_EFuncChuqianguanImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/Functions/Scroll View/Viewport/Content/EFuncChuqianguan");
     			}
     			return this.m_EFuncChuqianguanImage;
     		}
     	}

		public UnityEngine.UI.Button EFuncZhuanpanButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EFuncZhuanpanButton == null )
     			{
		    		this.m_EFuncZhuanpanButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/Functions/Scroll View/Viewport/Content/EFuncZhuanpan");
     			}
     			return this.m_EFuncZhuanpanButton;
     		}
     	}

		public UnityEngine.UI.Image EFuncZhuanpanImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EFuncZhuanpanImage == null )
     			{
		    		this.m_EFuncZhuanpanImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/Functions/Scroll View/Viewport/Content/EFuncZhuanpan");
     			}
     			return this.m_EFuncZhuanpanImage;
     		}
     	}

		public UnityEngine.UI.Button EFuncTongxingzhengButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EFuncTongxingzhengButton == null )
     			{
		    		this.m_EFuncTongxingzhengButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/Functions/EFuncTongxingzheng");
     			}
     			return this.m_EFuncTongxingzhengButton;
     		}
     	}

		public UnityEngine.UI.Image EFuncTongxingzhengImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EFuncTongxingzhengImage == null )
     			{
		    		this.m_EFuncTongxingzhengImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/LeftCenterFill/EFunctions/Functions/EFuncTongxingzheng");
     			}
     			return this.m_EFuncTongxingzhengImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EGBackGroundRectTransform = null;
			this.m_E_EnterMapButton = null;
			this.m_E_EnterMapImage = null;
			this.m_EHeroesButton = null;
			this.m_EHeroesImage = null;
			this.m_EPicsButton = null;
			this.m_EPicsImage = null;
			this.m_EImgProcessFillImage = null;
			this.m_ETextProcessText = null;
			this.m_EButtonMenuButton = null;
			this.m_EButtonMenuImage = null;
			this.m_ELabelDiamond1Text = null;
			this.m_ELabelCoinText = null;
			this.m_EButtonAddCoinButton = null;
			this.m_EButtonAddCoinImage = null;
			this.m_EButtonStoreButton = null;
			this.m_EButtonStoreImage = null;
			this.m_EButtonGiftButton = null;
			this.m_EButtonGiftImage = null;
			this.m_EButtonCouponButton = null;
			this.m_EButtonCouponImage = null;
			this.m_EMiniMapImage = null;
			this.m_EChapterText = null;
			this.m_ELabelLevelText = null;
			this.m_ELabelHeartCountText = null;
			this.m_ELabelHeartFullText = null;
			this.m_EFunctionsRawImage = null;
			this.m_EButtonHideLeftButton = null;
			this.m_EButtonHideLeftImage = null;
			this.m_EButtonSettingsButton = null;
			this.m_EButtonSettingsImage = null;
			this.m_EButtonRoleButton = null;
			this.m_EButtonRoleImage = null;
			this.m_EButtonMailButton = null;
			this.m_EButtonMailImage = null;
			this.m_EButtonCalendarButton = null;
			this.m_EButtonCalendarImage = null;
			this.m_ERoleIconImage = null;
			this.m_ERoleNameText = null;
			this.m_ERoleIDText = null;
			this.m_EButtonCopyInfoButton = null;
			this.m_EButtonCopyInfoImage = null;
			this.m_ELabelDiamond2Text = null;
			this.m_ERewardMaskImage = null;
			this.m_EFuncHeroButton = null;
			this.m_EFuncHeroImage = null;
			this.m_EFuncPicsButton = null;
			this.m_EFuncPicsImage = null;
			this.m_EFuncLuopanButton = null;
			this.m_EFuncLuopanImage = null;
			this.m_EFuncKongjianButton = null;
			this.m_EFuncKongjianImage = null;
			this.m_EFuncTiaozhanButton = null;
			this.m_EFuncTiaozhanImage = null;
			this.m_EFuncDituButton = null;
			this.m_EFuncDituImage = null;
			this.m_EFuncRenwuButton = null;
			this.m_EFuncRenwuImage = null;
			this.m_EFuncJuqingButton = null;
			this.m_EFuncJuqingImage = null;
			this.m_EFuncYaoqingButton = null;
			this.m_EFuncYaoqingImage = null;
			this.m_EFuncShejiaoButton = null;
			this.m_EFuncShejiaoImage = null;
			this.m_EFuncLiwuButton = null;
			this.m_EFuncLiwuImage = null;
			this.m_EFuncPaihangButton = null;
			this.m_EFuncPaihangImage = null;
			this.m_EFuncShangchengButton = null;
			this.m_EFuncShangchengImage = null;
			this.m_EFuncHuodongButton = null;
			this.m_EFuncHuodongImage = null;
			this.m_EFuncChuqianguanButton = null;
			this.m_EFuncChuqianguanImage = null;
			this.m_EFuncZhuanpanButton = null;
			this.m_EFuncZhuanpanImage = null;
			this.m_EFuncTongxingzhengButton = null;
			this.m_EFuncTongxingzhengImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EGBackGroundRectTransform = null;
		private UnityEngine.UI.Button m_E_EnterMapButton = null;
		private UnityEngine.UI.Image m_E_EnterMapImage = null;
		private UnityEngine.UI.Button m_EHeroesButton = null;
		private UnityEngine.UI.Image m_EHeroesImage = null;
		private UnityEngine.UI.Button m_EPicsButton = null;
		private UnityEngine.UI.Image m_EPicsImage = null;
		private UnityEngine.UI.Image m_EImgProcessFillImage = null;
		private UnityEngine.UI.Text m_ETextProcessText = null;
		private UnityEngine.UI.Button m_EButtonMenuButton = null;
		private UnityEngine.UI.Image m_EButtonMenuImage = null;
		private UnityEngine.UI.Text m_ELabelDiamond1Text = null;
		private UnityEngine.UI.Text m_ELabelCoinText = null;
		private UnityEngine.UI.Button m_EButtonAddCoinButton = null;
		private UnityEngine.UI.Image m_EButtonAddCoinImage = null;
		private UnityEngine.UI.Button m_EButtonStoreButton = null;
		private UnityEngine.UI.Image m_EButtonStoreImage = null;
		private UnityEngine.UI.Button m_EButtonGiftButton = null;
		private UnityEngine.UI.Image m_EButtonGiftImage = null;
		private UnityEngine.UI.Button m_EButtonCouponButton = null;
		private UnityEngine.UI.Image m_EButtonCouponImage = null;
		private UnityEngine.UI.Image m_EMiniMapImage = null;
		private UnityEngine.UI.Text m_EChapterText = null;
		private UnityEngine.UI.Text m_ELabelLevelText = null;
		private UnityEngine.UI.Text m_ELabelHeartCountText = null;
		private UnityEngine.UI.Text m_ELabelHeartFullText = null;
		private UnityEngine.UI.RawImage m_EFunctionsRawImage = null;
		private UnityEngine.UI.Button m_EButtonHideLeftButton = null;
		private UnityEngine.UI.Image m_EButtonHideLeftImage = null;
		private UnityEngine.UI.Button m_EButtonSettingsButton = null;
		private UnityEngine.UI.Image m_EButtonSettingsImage = null;
		private UnityEngine.UI.Button m_EButtonRoleButton = null;
		private UnityEngine.UI.Image m_EButtonRoleImage = null;
		private UnityEngine.UI.Button m_EButtonMailButton = null;
		private UnityEngine.UI.Image m_EButtonMailImage = null;
		private UnityEngine.UI.Button m_EButtonCalendarButton = null;
		private UnityEngine.UI.Image m_EButtonCalendarImage = null;
		private UnityEngine.UI.Image m_ERoleIconImage = null;
		private UnityEngine.UI.Text m_ERoleNameText = null;
		private UnityEngine.UI.Text m_ERoleIDText = null;
		private UnityEngine.UI.Button m_EButtonCopyInfoButton = null;
		private UnityEngine.UI.Image m_EButtonCopyInfoImage = null;
		private UnityEngine.UI.Text m_ELabelDiamond2Text = null;
		private UnityEngine.UI.Image m_ERewardMaskImage = null;
		private UnityEngine.UI.Button m_EFuncHeroButton = null;
		private UnityEngine.UI.Image m_EFuncHeroImage = null;
		private UnityEngine.UI.Button m_EFuncPicsButton = null;
		private UnityEngine.UI.Image m_EFuncPicsImage = null;
		private UnityEngine.UI.Button m_EFuncLuopanButton = null;
		private UnityEngine.UI.Image m_EFuncLuopanImage = null;
		private UnityEngine.UI.Button m_EFuncKongjianButton = null;
		private UnityEngine.UI.Image m_EFuncKongjianImage = null;
		private UnityEngine.UI.Button m_EFuncTiaozhanButton = null;
		private UnityEngine.UI.Image m_EFuncTiaozhanImage = null;
		private UnityEngine.UI.Button m_EFuncDituButton = null;
		private UnityEngine.UI.Image m_EFuncDituImage = null;
		private UnityEngine.UI.Button m_EFuncRenwuButton = null;
		private UnityEngine.UI.Image m_EFuncRenwuImage = null;
		private UnityEngine.UI.Button m_EFuncJuqingButton = null;
		private UnityEngine.UI.Image m_EFuncJuqingImage = null;
		private UnityEngine.UI.Button m_EFuncYaoqingButton = null;
		private UnityEngine.UI.Image m_EFuncYaoqingImage = null;
		private UnityEngine.UI.Button m_EFuncShejiaoButton = null;
		private UnityEngine.UI.Image m_EFuncShejiaoImage = null;
		private UnityEngine.UI.Button m_EFuncLiwuButton = null;
		private UnityEngine.UI.Image m_EFuncLiwuImage = null;
		private UnityEngine.UI.Button m_EFuncPaihangButton = null;
		private UnityEngine.UI.Image m_EFuncPaihangImage = null;
		private UnityEngine.UI.Button m_EFuncShangchengButton = null;
		private UnityEngine.UI.Image m_EFuncShangchengImage = null;
		private UnityEngine.UI.Button m_EFuncHuodongButton = null;
		private UnityEngine.UI.Image m_EFuncHuodongImage = null;
		private UnityEngine.UI.Button m_EFuncChuqianguanButton = null;
		private UnityEngine.UI.Image m_EFuncChuqianguanImage = null;
		private UnityEngine.UI.Button m_EFuncZhuanpanButton = null;
		private UnityEngine.UI.Image m_EFuncZhuanpanImage = null;
		private UnityEngine.UI.Button m_EFuncTongxingzhengButton = null;
		private UnityEngine.UI.Image m_EFuncTongxingzhengImage = null;
		public Transform uiTransform = null;
	}
}
