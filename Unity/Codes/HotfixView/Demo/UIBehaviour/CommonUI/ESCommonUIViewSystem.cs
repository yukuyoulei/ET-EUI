
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class ESCommonUIAwakeSystem : AwakeSystem<ESCommonUI,Transform> 
	{
		public override void Awake(ESCommonUI me,Transform transform)
		{
			me.uiTransform = transform;
		}
	}


	[ObjectSystem]
	public class ESCommonUIDestroySystem : DestroySystem<ESCommonUI> 
	{
		public override void Destroy(ESCommonUI me)
		{
			me.DestroyWidget();
		}
	}
}
