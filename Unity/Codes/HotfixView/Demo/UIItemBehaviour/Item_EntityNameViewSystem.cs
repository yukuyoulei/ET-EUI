
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
    [ObjectSystem]
    public class Scroll_Item_EntityNameDestroySystem : DestroySystem<Scroll_Item_EntityName>
    {
        public override void Destroy(Scroll_Item_EntityName me)
        {
            me.DestroyWidget();
        }
    }
}
