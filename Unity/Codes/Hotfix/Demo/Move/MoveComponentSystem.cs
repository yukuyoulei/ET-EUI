using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    [Timer(TimerType.MoveTimer)]
    public class MoveTimer: ATimer<MoveComponent>
    {
        public override void Run(MoveComponent me)
        {
            try
            {
                me.MoveForward(false);
            }
            catch (Exception e)
            {
                Log.Error($"move timer error: {me.Id}\n{e}");
            }
        }
    }
    
    [ObjectSystem]
    public class MoveComponentDestroySystem: DestroySystem<MoveComponent>
    {
        public override void Destroy(MoveComponent me)
        {
            me.Clear();
        }
    }

    [ObjectSystem]
    public class MoveComponentAwakeSystem: AwakeSystem<MoveComponent>
    {
        public override void Awake(MoveComponent me)
        {
            me.StartTime = 0;
            me.StartPos = Vector3.zero;
            me.NeedTime = 0;
            me.MoveTimer = 0;
            me.Callback = null;
            me.Targets.Clear();
            me.Speed = 0;
            me.N = 0;
            me.TurnTime = 0;
        }
    }

    [FriendClass(typeof(MoveComponent))]
    public static class MoveComponentSystem
    {
        public static bool IsArrived(this MoveComponent me)
        {
            return me.Targets.Count == 0;
        }

        public static bool ChangeSpeed(this MoveComponent me, float speed)
        {
            if (me.IsArrived())
            {
                return false;
            }

            if (speed < 0.0001)
            {
                return false;
            }
            
            Unit unit = me.GetParent<Unit>();

            using (ListComponent<Vector3> path = ListComponent<Vector3>.Create())
            {
                me.MoveForward(true);
                
                path.Add(unit.Position); // 第一个是Unit的pos
                for (int i = me.N; i < me.Targets.Count; ++i)
                {
                    path.Add(me.Targets[i]);
                }
                me.MoveToAsync(path, speed).Coroutine();
            }
            return true;
        }

        public static async ETTask<bool> MoveToAsync(this MoveComponent me, List<Vector3> target, float speed, int turnTime = 100, ETCancellationToken cancellationToken = null)
        {
            me.Stop();

            foreach (Vector3 v in target)
            {
                me.Targets.Add(v);
            }

            me.IsTurnHorizontal = true;
            me.TurnTime = turnTime;
            me.Speed = speed;
            ETTask<bool> tcs = ETTask<bool>.Create(true);
            me.Callback = (ret) => { tcs.SetResult(ret); };

            Game.EventSystem.Publish(new EventType.MoveStart(){Unit = me.GetParent<Unit>()});
            
            me.StartMove();
            
            void CancelAction()
            {
                me.Stop();
            }
            
            bool moveRet;
            try
            {
                cancellationToken?.Add(CancelAction);
                moveRet = await tcs;
            }
            finally
            {
                cancellationToken?.Remove(CancelAction);
            }

            if (moveRet)
            {
                Game.EventSystem.Publish(new EventType.MoveStop(){Unit = me.GetParent<Unit>()});
            }
            return moveRet;
        }

        public static void MoveForward(this MoveComponent me, bool needCancel)
        {
            Unit unit = me.GetParent<Unit>();
            
            long timeNow = TimeHelper.ClientNow();
            long moveTime = timeNow - me.StartTime;

            while (true)
            {
                if (moveTime <= 0)
                {
                    return;
                }
                
                // 计算位置插值
                if (moveTime >= me.NeedTime)
                {
                    unit.Position = me.NextTarget;
                    if (me.TurnTime > 0)
                    {
                        unit.Rotation = me.To;
                    }
                }
                else
                {
                    // 计算位置插值
                    float amount = moveTime * 1f / me.NeedTime;
                    if (amount > 0)
                    {
                        Vector3 newPos = Vector3.Lerp(me.StartPos, me.NextTarget, amount);
                        unit.Position = newPos;
                    }
                    
                    // 计算方向插值
                    if (me.TurnTime > 0)
                    {
                        amount = moveTime * 1f / me.TurnTime;
                        Quaternion q = Quaternion.Slerp(me.From, me.To, amount);
                        unit.Rotation = q;
                    }
                }

                moveTime -= me.NeedTime;

                // 表示这个点还没走完，等下一帧再来
                if (moveTime < 0)
                {
                    return;
                }
                
                // 到这里说明这个点已经走完
                
                // 如果是最后一个点
                if (me.N >= me.Targets.Count - 1)
                {
                    unit.Position = me.NextTarget;
                    unit.Rotation = me.To;

                    Action<bool> callback = me.Callback;
                    me.Callback = null;

                    me.Clear();
                    callback?.Invoke(!needCancel);
                    return;
                }

                me.SetNextTarget();
            }
        }

        private static void StartMove(this MoveComponent me)
        {
            Unit unit = me.GetParent<Unit>();
            
            me.BeginTime = TimeHelper.ClientNow();
            me.StartTime = me.BeginTime;
            me.SetNextTarget();

            me.MoveTimer = TimerComponent.Instance.NewFrameTimer(TimerType.MoveTimer, me);
        }

        private static void SetNextTarget(this MoveComponent me)
        {

            Unit unit = me.GetParent<Unit>();

            ++me.N;

            // 时间计算用服务端的位置, 但是移动要用客户端的位置来插值
            Vector3 v = me.GetFaceV();
            float distance = v.magnitude;
            
            // 插值的起始点要以unit的真实位置来算
            me.StartPos = unit.Position;

            me.StartTime += me.NeedTime;
            
            me.NeedTime = (long) (distance / me.Speed * 1000);

            
            if (me.TurnTime > 0)
            {
                // 要用unit的位置
                Vector3 faceV = me.GetFaceV();
                if (faceV.sqrMagnitude < 0.0001f)
                {
                    return;
                }
                me.From = unit.Rotation;
                
                if (me.IsTurnHorizontal)
                {
                    faceV.y = 0;
                }

                if (Math.Abs(faceV.x) > 0.01 || Math.Abs(faceV.z) > 0.01)
                {
                    me.To = Quaternion.LookRotation(faceV, Vector3.up);
                }

                return;
            }
            
            if (me.TurnTime == 0) // turn time == 0 立即转向
            {
                Vector3 faceV = me.GetFaceV();
                if (me.IsTurnHorizontal)
                {
                    faceV.y = 0;
                }

                if (Math.Abs(faceV.x) > 0.01 || Math.Abs(faceV.z) > 0.01)
                {
                    me.To = Quaternion.LookRotation(faceV, Vector3.up);
                    unit.Rotation = me.To;
                }
            }
        }

        private static Vector3 GetFaceV(this MoveComponent me)
        {
            return me.NextTarget - me.PreTarget;
        }

        public static bool FlashTo(this MoveComponent me, Vector3 target)
        {
            Unit unit = me.GetParent<Unit>();
            unit.Position = target;
            return true;
        }

        public static void Stop(this MoveComponent me)
        {
            if (me.Targets.Count > 0)
            {
                me.MoveForward(true);
            }

            me.Clear();
        }

        public static void Clear(this MoveComponent me)
        {
            me.StartTime = 0;
            me.StartPos = Vector3.zero;
            me.BeginTime = 0;
            me.NeedTime = 0;
            TimerComponent.Instance?.Remove(ref me.MoveTimer);
            me.Targets.Clear();
            me.Speed = 0;
            me.N = 0;
            me.TurnTime = 0;
            me.IsTurnHorizontal = false;

            if (me.Callback != null)
            {
                Action<bool> callback = me.Callback;
                me.Callback = null;
                callback.Invoke(false);
            }
        }
    }
}