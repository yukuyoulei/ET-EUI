
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgRedDotViewComponentAwakeSystem : AwakeSystem<DlgRedDotViewComponent> 
	{
		public override void Awake(DlgRedDotViewComponent me)
		{
			me.uiTransform = me.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgRedDotViewComponentDestroySystem : DestroySystem<DlgRedDotViewComponent> 
	{
		public override void Destroy(DlgRedDotViewComponent me)
		{
			me.DestroyWidget();
		}
	}
}
