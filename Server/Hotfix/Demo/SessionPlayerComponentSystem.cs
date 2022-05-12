

namespace ET
{
	[FriendClass(typeof(SessionPlayerComponent))]
	public static class SessionPlayerComponentSystem
	{
		public class SessionPlayerComponentDestroySystem: DestroySystem<SessionPlayerComponent>
		{
			public override void Destroy(SessionPlayerComponent me)
			{
				// 发送断线消息
				ActorLocationSenderComponent.Instance.Send(me.PlayerId, new G2M_SessionDisconnect());
				me.Domain.GetComponent<PlayerComponent>()?.Remove(me.PlayerId);
			}
		}

		public static Player GetMyPlayer(this SessionPlayerComponent me)
		{
			return me.Domain.GetComponent<PlayerComponent>().Get(me.PlayerId);
		}
	}
}
