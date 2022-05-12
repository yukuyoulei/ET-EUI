namespace ET
{
	public class EntityTree_ShowDlg : AEventAsync<EventType.EntityTree>
	{
		protected override async ETTask Run(EventType.EntityTree args)
		{
			await args.ZoneScene.GetComponent<UIComponent>().ShowWindowAsync(WindowID.WindowID_EntityTree
				, WindowID.WindowID_Invaild, new ShowWindowData()
				{
					contextData = new ServerEntityNodes()
					{
						nodes = args.nodes
					}
				});
		}
	}
}