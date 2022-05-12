using System.Collections.Generic;
using System.Text;

namespace ET
{
    [ObjectSystem]
    public class CellDestroySystem: DestroySystem<Cell>
    {
        public override void Destroy(Cell me)
        {
            me.AOIUnits.Clear();

            me.SubsEnterEntities.Clear();

            me.SubsLeaveEntities.Clear();
        }
    }

    [FriendClass(typeof(Cell))]
    public static class CellSystem
    {
        public static void Add(this Cell me, AOIEntity aoiEntity)
        {
            me.AOIUnits.Add(aoiEntity.Id, aoiEntity);
        }
        
        public static void Remove(this Cell me, AOIEntity aoiEntity)
        {
            me.AOIUnits.Remove(aoiEntity.Id);
        }
        
        public static string CellIdToString(this long cellId)
        {
            int y = (int) (cellId & 0xffffffff);
            int x = (int) ((ulong) cellId >> 32);
            return $"{x}:{y}";
        }

        public static string CellIdToString(this HashSet<long> cellIds)
        {
            StringBuilder sb = new StringBuilder();
            foreach (long cellId in cellIds)
            {
                sb.Append(cellId.CellIdToString());
                sb.Append(",");
            }

            return sb.ToString();
        }
    }
}