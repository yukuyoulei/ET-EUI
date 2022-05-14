using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
	[FriendClass(typeof(AnimatorComponent))]
	public class EventMoveStartAimator : AEvent<EventType.MoveStart>
	{
		protected override void Run(MoveStart a)
		{
			a.Unit.GetComponent<AnimatorComponent>().Play(MotionType.Run);
		}
	}
	[FriendClass(typeof(AnimatorComponent))]
	public class EventMoveStopAnimator : AEvent<EventType.MoveStop>
	{
		protected override void Run(MoveStop a)
		{
			a.Unit.GetComponent<AnimatorComponent>().Play(MotionType.Idle, 0);
		}
	}
}
