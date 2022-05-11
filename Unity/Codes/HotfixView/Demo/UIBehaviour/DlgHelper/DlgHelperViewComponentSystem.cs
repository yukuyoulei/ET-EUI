
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgHelperViewComponentAwakeSystem : AwakeSystem<DlgHelperViewComponent> 
	{
		public override void Awake(DlgHelperViewComponent me)
		{
			me.uiTransform = me.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgHelperViewComponentDestroySystem : DestroySystem<DlgHelperViewComponent> 
	{
		public override void Destroy(DlgHelperViewComponent me)
		{
			me.DestroyWidget();
		}
	}
}
