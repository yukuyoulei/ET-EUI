using System;
using System.Collections.Generic;

namespace ET
{
	[FriendClass(typeof(GateMapComponent))]
	[MessageHandler]
	public class C2G_EntitiesGetHandler : AMRpcHandler<C2G_EntitiesGetRequest, G2C_EntitiesGetResponse>
	{
		protected override async ETTask Run(Session session, C2G_EntitiesGetRequest request, G2C_EntitiesGetResponse response, Action reply)
		{
			foreach (var t in EntityTreeHelper.GetClientEntity())
			{
				response.entities.Add($"{t.Key}{GetTab(t.Key)}{t.Value}");
			}
			reply();
			await ETTask.CompletedTask;
		}

		private static string GetTab(int tabcount)
		{
			var res = "";
			for (var i = 0; i < tabcount; i++)
				res += "  ";
			return res;
		}
	}

	public static class EntityTreeHelper
	{
		private static KeyValuePair<int, string> CreateNode(this List<KeyValuePair<int, string>> list, int layer, string node)
		{
			var n = new KeyValuePair<int, string>(layer, node);
			list.Add(n);
			return n;
		}
		public static List<KeyValuePair<int, string>> GetClientEntity()
		{
			var res = new List<KeyValuePair<int, string>>(64);
			var layer = 0;
			var n = res.CreateNode(layer, "Game.Scene");
			GatherEntities(res, Game.Scene, n);
			return res;
		}

		private static void GatherEntities(List<KeyValuePair<int, string>> res, Entity ent, KeyValuePair<int, string> parent)
		{
			if (ent.Children.Count > 0)
			{
				foreach (var c in ent.Children.Values)
				{
					GatherEntities(res, c, res.CreateNode(parent.Key + 1, "♦" + c.GetType().Name));
				}
			}
			if (ent.Components.Count > 0)
			{
				foreach (var c in ent.Components.Values)
				{
					GatherEntities(res, c, res.CreateNode(parent.Key + 1, "○" + c.GetType().Name));
				}
			}
		}
	}
}