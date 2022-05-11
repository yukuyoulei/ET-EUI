using System;
using UnityEngine;

namespace ET
{
    [ObjectSystem]
    public class OperaComponentAwakeSystem : AwakeSystem<OperaComponent>
    {
        public override void Awake(OperaComponent me)
        {
            me.mapMask = LayerMask.GetMask("Map");
        }
    }

    [ObjectSystem]
    public class OperaComponentUpdateSystem : UpdateSystem<OperaComponent>
    {
        public override void Update(OperaComponent me)
        {
            me.Update();
        }
    }

    [FriendClass(typeof(OperaComponent))]
    public static class OperaComponentSystem
    {
        public static void Update(this OperaComponent me)
        {
            if (InputHelper.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000, me.mapMask))
                {
                    me.ClickPoint = hit.point;
                    me.frameClickMap.X = me.ClickPoint.x;
                    me.frameClickMap.Y = me.ClickPoint.y;
                    me.frameClickMap.Z = me.ClickPoint.z;
                    me.ZoneScene().GetComponent<SessionComponent>().Session.Send(me.frameClickMap);
                }
            }

            // KeyCode.Escape
            if (InputHelper.GetKeyDown(27))
            {
                me.ZoneScene().GetComponent<UIComponent>().ShowWindow<DlgEntityTree>();
            }

            // KeyCode.T
            if (InputHelper.GetKeyDown(116))
            {
                C2M_TransferMap c2MTransferMap = new C2M_TransferMap();
                me.ZoneScene().GetComponent<SessionComponent>().Session.Call(c2MTransferMap).Coroutine();
            }
        }
    }
}