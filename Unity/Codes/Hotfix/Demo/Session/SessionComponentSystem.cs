namespace ET
{
	public class SessionComponentDestroySystem: DestroySystem<SessionComponent>
	{
		public override void Destroy(SessionComponent me)
		{
			me.Session.Dispose();
		}
	}
}
