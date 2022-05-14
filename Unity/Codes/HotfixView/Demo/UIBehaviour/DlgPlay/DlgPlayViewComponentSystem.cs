
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgPlayViewComponentAwakeSystem : AwakeSystem<DlgPlayViewComponent> 
	{
		public override void Awake(DlgPlayViewComponent me)
		{
			me.uiTransform = me.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgPlayViewComponentDestroySystem : DestroySystem<DlgPlayViewComponent> 
	{
		public override void Destroy(DlgPlayViewComponent me)
		{
			me.DestroyWidget();
		}
	}
}
