using System.Collections.Generic;
namespace ET
{
    public class DlgEntityTree : Entity, IAwake, IUILogic
    {
        public DlgEntityTreeViewComponent View { get => this.Parent.GetComponent<DlgEntityTreeViewComponent>(); }

        public List<string> lEntityNames;
        public Dictionary<int, Scroll_Item_EntityName> dItems = new Dictionary<int, Scroll_Item_EntityName>();
    }
}
