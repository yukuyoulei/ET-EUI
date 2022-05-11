
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgEntityTreeViewComponentAwakeSystem : AwakeSystem<DlgEntityTreeViewComponent> 
	{
		public override void Awake(DlgEntityTreeViewComponent me)
		{
			me.uiTransform = me.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgEntityTreeViewComponentDestroySystem : DestroySystem<DlgEntityTreeViewComponent> 
	{
		public override void Destroy(DlgEntityTreeViewComponent me)
		{
			me.DestroyWidget();
		}
	}
}
