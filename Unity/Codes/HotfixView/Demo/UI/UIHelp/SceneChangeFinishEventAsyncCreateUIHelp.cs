namespace ET
{
    public class SceneChangeFinishEventAsyncCreateUIHelp : AEventAsync<EventType.SceneChangeFinish>
    {
        protected override async ETTask Run(EventType.SceneChangeFinish args)
        {
            //UIHelper.Create(args.CurrentScene, UIType.UIHelp, UILayer.Mid).Coroutine();
            args.ZoneScene.GetComponent<UIComponent>().HideAllShownWindow();
            await ETTask.CompletedTask;
        }
    }
}
