
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class ESReuseUIAwakeSystem : AwakeSystem<ESReuseUI,Transform> 
	{
		public override void Awake(ESReuseUI me,Transform transform)
		{
			me.uiTransform = transform;
		}
	}


	[ObjectSystem]
	public class ESReuseUIDestroySystem : DestroySystem<ESReuseUI> 
	{
		public override void Destroy(ESReuseUI me)
		{
			me.DestroyWidget();
		}
	}
}
