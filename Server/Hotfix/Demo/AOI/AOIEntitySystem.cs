using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    [FriendClass(typeof(AOIEntity))]
    [FriendClass(typeof(Cell))]
    public static class AOIEntitySystem
    {
        [ObjectSystem]
        public class AwakeSystem: AwakeSystem<AOIEntity, int, Vector3>
        {
            public override void Awake(AOIEntity me, int distance, Vector3 pos)
            {
                me.ViewDistance = distance;
                me.Domain.GetComponent<AOIManagerComponent>().Add(me, pos.x, pos.z);
            }
        }

        [ObjectSystem]
        public class DestroySystem: DestroySystem<AOIEntity>
        {
            public override void Destroy(AOIEntity me)
            {
                me.Domain.GetComponent<AOIManagerComponent>()?.Remove(me);
                me.ViewDistance = 0;
                me.SeeUnits.Clear();
                me.SeePlayers.Clear();
                me.BeSeePlayers.Clear();
                me.BeSeeUnits.Clear();
                me.SubEnterCells.Clear();
                me.SubLeaveCells.Clear();
                me.Cell = null;
            }
        }
        
        // 获取在自己视野中的对象
        public static Dictionary<long, AOIEntity> GetSeeUnits(this AOIEntity me)
        {
            return me.SeeUnits;
        }

        public static Dictionary<long, AOIEntity> GetBeSeePlayers(this AOIEntity me)
        {
            return me.BeSeePlayers;
        }

        public static Dictionary<long, AOIEntity> GetSeePlayers(this AOIEntity me)
        {
            return me.SeePlayers;
        }

        // cell中的unit进入me的视野
        public static void SubEnter(this AOIEntity me, Cell cell)
        {
            cell.SubsEnterEntities.Add(me.Id, me);
            foreach (KeyValuePair<long, AOIEntity> kv in cell.AOIUnits)
            {
                if (kv.Key == me.Id)
                {
                    continue;
                }

                me.EnterSight(kv.Value);
            }
        }

        public static void UnSubEnter(this AOIEntity me, Cell cell)
        {
            cell.SubsEnterEntities.Remove(me.Id);
        }

        public static void SubLeave(this AOIEntity me, Cell cell)
        {
            cell.SubsLeaveEntities.Add(me.Id, me);
        }

        // cell中的unit离开me的视野
        public static void UnSubLeave(this AOIEntity me, Cell cell)
        {
            foreach (KeyValuePair<long, AOIEntity> kv in cell.AOIUnits)
            {
                if (kv.Key == me.Id)
                {
                    continue;
                }

                me.LeaveSight(kv.Value);
            }

            cell.SubsLeaveEntities.Remove(me.Id);
        }

        // enter进入me视野
        public static void EnterSight(this AOIEntity me, AOIEntity enter)
        {
            // 有可能之前在Enter，后来出了Enter还在LeaveCell，这样仍然没有删除，继续进来Enter，这种情况不需要处理
            if (me.SeeUnits.ContainsKey(enter.Id))
            {
                return;
            }
            
            if (!AOISeeCheckHelper.IsCanSee(me, enter))
            {
                return;
            }

            if (me.Unit.Type == UnitType.Player)
            {
                if (enter.Unit.Type == UnitType.Player)
                {
                    me.SeeUnits.Add(enter.Id, enter);
                    enter.BeSeeUnits.Add(me.Id, me);
                    me.SeePlayers.Add(enter.Id, enter);
                    enter.BeSeePlayers.Add(me.Id, me);
                    
                }
                else
                {
                    me.SeeUnits.Add(enter.Id, enter);
                    enter.BeSeeUnits.Add(me.Id, me);
                    enter.BeSeePlayers.Add(me.Id, me);
                }
            }
            else
            {
                if (enter.Unit.Type == UnitType.Player)
                {
                    me.SeeUnits.Add(enter.Id, enter);
                    enter.BeSeeUnits.Add(me.Id, me);
                    me.SeePlayers.Add(enter.Id, enter);
                }
                else
                {
                    me.SeeUnits.Add(enter.Id, enter);
                    enter.BeSeeUnits.Add(me.Id, me);
                }
            }
            Game.EventSystem.Publish(new EventType.UnitEnterSightRange() {A = me, B = enter});
        }

        // leave离开me视野
        public static void LeaveSight(this AOIEntity me, AOIEntity leave)
        {
            if (me.Id == leave.Id)
            {
                return;
            }

            if (!me.SeeUnits.ContainsKey(leave.Id))
            {
                return;
            }

            me.SeeUnits.Remove(leave.Id);
            if (leave.Unit.Type == UnitType.Player)
            {
                me.SeePlayers.Remove(leave.Id);
            }

            leave.BeSeeUnits.Remove(me.Id);
            if (me.Unit.Type == UnitType.Player)
            {
                leave.BeSeePlayers.Remove(me.Id);
            }

            Game.EventSystem.Publish(new EventType.UnitLeaveSightRange {A = me, B = leave});
        }

        /// <summary>
        /// 是否在Unit视野范围内
        /// </summary>
        /// <param name="me"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static bool IsBeSee(this AOIEntity me, long unitId)
        {
            return me.BeSeePlayers.ContainsKey(unitId);
        }
    }
}