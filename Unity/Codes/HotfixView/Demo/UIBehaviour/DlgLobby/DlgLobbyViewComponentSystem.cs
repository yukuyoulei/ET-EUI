
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgLobbyViewComponentAwakeSystem : AwakeSystem<DlgLobbyViewComponent> 
	{
		public override void Awake(DlgLobbyViewComponent me)
		{
			me.uiTransform = me.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgLobbyViewComponentDestroySystem : DestroySystem<DlgLobbyViewComponent> 
	{
		public override void Destroy(DlgLobbyViewComponent me)
		{
			me.DestroyWidget();
		}
	}
}
