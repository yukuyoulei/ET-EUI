using System;
using UnityEngine;

namespace ET
{
	[ObjectSystem]
	public class AnimatorComponentAwakeSystem : AwakeSystem<AnimatorComponent>
	{
		public override void Awake(AnimatorComponent me)
		{
			me.Awake();
		}
	}

	[ObjectSystem]
	public class AnimatorComponentUpdateSystem : UpdateSystem<AnimatorComponent>
	{
		public override void Update(AnimatorComponent me)
		{
			me.Update();
		}
	}
	
	[ObjectSystem]
	public class AnimatorComponentDestroySystem : DestroySystem<AnimatorComponent>
	{
		public override void Destroy(AnimatorComponent me)
		{
			me.animationClips = null;
			me.Parameter = null;
			me.Animator = null;
		}
	}

	[FriendClass(typeof(AnimatorComponent))]
	public static class AnimatorComponentSystem
	{
		public static void Awake(this AnimatorComponent me)
		{
			Animator animator = me.Parent.GetComponent<GameObjectComponent>().GameObject.GetComponent<Animator>();

			if (animator == null)
			{
				return;
			}

			if (animator.runtimeAnimatorController == null)
			{
				return;
			}

			if (animator.runtimeAnimatorController.animationClips == null)
			{
				return;
			}
			me.Animator = animator;
			foreach (AnimationClip animationClip in animator.runtimeAnimatorController.animationClips)
			{
				me.animationClips[animationClip.name] = animationClip;
			}
			foreach (AnimatorControllerParameter animatorControllerParameter in animator.parameters)
			{
				me.Parameter.Add(animatorControllerParameter.name);
			}
		}

		public static void Update(this AnimatorComponent me)
		{
			if (me.isStop)
			{
				return;
			}

			if (me.MotionType == MotionType.None)
			{
				return;
			}

			try
			{
				me.Animator.SetFloat("Speed", me.MontionSpeed);

				me.Animator.SetTrigger(me.MotionType.ToString());

				me.MontionSpeed = 1;
				me.MotionType = MotionType.None;
			}
			catch (Exception ex)
			{
				throw new Exception($"动作播放失败: {me.MotionType}", ex);
			}
		}

		public static bool HasParameter(this AnimatorComponent me, string parameter)
		{
			return me.Parameter.Contains(parameter);
		}

		public static void PlayInTime(this AnimatorComponent me, MotionType motionType, float time)
		{
			AnimationClip animationClip;
			if (!me.animationClips.TryGetValue(motionType.ToString(), out animationClip))
			{
				throw new Exception($"找不到该动作: {motionType}");
			}

			float motionSpeed = animationClip.length / time;
			if (motionSpeed < 0.01f || motionSpeed > 1000f)
			{
				Log.Error($"motionSpeed数值异常, {motionSpeed}, 此动作跳过");
				return;
			}
			me.MotionType = motionType;
			me.MontionSpeed = motionSpeed;
		}

		public static void Play(this AnimatorComponent me, MotionType motionType, float motionSpeed = 1f)
		{
			if (!me.HasParameter(motionType.ToString()))
			{
				return;
			}
			me.MotionType = motionType;
			me.MontionSpeed = motionSpeed;
		}

		public static float AnimationTime(this AnimatorComponent me, MotionType motionType)
		{
			AnimationClip animationClip;
			if (!me.animationClips.TryGetValue(motionType.ToString(), out animationClip))
			{
				throw new Exception($"找不到该动作: {motionType}");
			}
			return animationClip.length;
		}

		public static void PauseAnimator(this AnimatorComponent me)
		{
			if (me.isStop)
			{
				return;
			}
			me.isStop = true;

			if (me.Animator == null)
			{
				return;
			}
			me.stopSpeed = me.Animator.speed;
			me.Animator.speed = 0;
		}

		public static void RunAnimator(this AnimatorComponent me)
		{
			if (!me.isStop)
			{
				return;
			}

			me.isStop = false;

			if (me.Animator == null)
			{
				return;
			}
			me.Animator.speed = me.stopSpeed;
		}

		public static void SetBoolValue(this AnimatorComponent me, string name, bool state)
		{
			if (!me.HasParameter(name))
			{
				return;
			}

			me.Animator.SetBool(name, state);
		}

		public static void SetFloatValue(this AnimatorComponent me, string name, float state)
		{
			if (!me.HasParameter(name))
			{
				return;
			}

			me.Animator.SetFloat(name, state);
		}

		public static void SetIntValue(this AnimatorComponent me, string name, int value)
		{
			if (!me.HasParameter(name))
			{
				return;
			}

			me.Animator.SetInteger(name, value);
		}

		public static void SetTrigger(this AnimatorComponent me, string name)
		{
			if (!me.HasParameter(name))
			{
				return;
			}

			me.Animator.SetTrigger(name);
		}

		public static void SetAnimatorSpeed(this AnimatorComponent me, float speed)
		{
			me.stopSpeed = me.Animator.speed;
			me.Animator.speed = speed;
		}

		public static void ResetAnimatorSpeed(this AnimatorComponent me)
		{
			me.Animator.speed = me.stopSpeed;
		}
	}
}