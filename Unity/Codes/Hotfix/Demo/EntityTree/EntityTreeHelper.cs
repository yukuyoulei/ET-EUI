﻿using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
	public static class EntityTreeHelper
	{
		public static KeyValuePair<int, string> CreateNode(this List<KeyValuePair<int, string>> list, int layer, string node)
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

		public static async ETTask Request(Scene zoneScene)
		{
			var s = zoneScene.GetComponent<SessionComponent>().Session;
			var req = (G2C_EntitiesGetResponse)await s.Call(new C2G_EntitiesGetRequest());
			Game.EventSystem.PublishAsync(new EventType.EntityTree() { ZoneScene = zoneScene, nodes = req.entities }).Coroutine();
		}
	}
}
