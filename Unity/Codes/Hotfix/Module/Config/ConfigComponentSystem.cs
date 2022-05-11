using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ET
{
	[ObjectSystem]
    public class ConfigAwakeSystem : AwakeSystem<ConfigComponent>
    {
        public override void Awake(ConfigComponent me)
        {
	        ConfigComponent.Instance = me;
        }
    }
    
    [ObjectSystem]
    public class ConfigDestroySystem : DestroySystem<ConfigComponent>
    {
	    public override void Destroy(ConfigComponent me)
	    {
		    ConfigComponent.Instance = null;
	    }
    }
    
    [FriendClass(typeof(ConfigComponent))]
    public static class ConfigComponentSystem
	{
		public static void LoadOneConfig(this ConfigComponent me, Type configType)
		{
			byte[] oneConfigBytes = me.ConfigLoader.GetOneConfigBytes(configType.FullName);

			object category = ProtobufHelper.FromBytes(configType, oneConfigBytes, 0, oneConfigBytes.Length);

			me.AllConfig[configType] = category;
		}
		
		public static void Load(this ConfigComponent me)
		{
			me.AllConfig.Clear();
			List<Type> types = Game.EventSystem.GetTypes(typeof (ConfigAttribute));
			
			Dictionary<string, byte[]> configBytes = new Dictionary<string, byte[]>();
			me.ConfigLoader.GetAllConfigBytes(configBytes);

			foreach (Type type in types)
			{
				me.LoadOneInThread(type, configBytes);
			}
		}
		
		public static async ETTask LoadAsync(this ConfigComponent me)
		{
			me.AllConfig.Clear();
			List<Type> types = Game.EventSystem.GetTypes(typeof (ConfigAttribute));
			
			Dictionary<string, byte[]> configBytes = new Dictionary<string, byte[]>();
			me.ConfigLoader.GetAllConfigBytes(configBytes);

			using (ListComponent<Task> listTasks = ListComponent<Task>.Create())
			{
				foreach (Type type in types)
				{
					Task task = Task.Run(() => me.LoadOneInThread(type, configBytes));
					listTasks.Add(task);
				}

				await Task.WhenAll(listTasks.ToArray());
			}
		}

		private static void LoadOneInThread(this ConfigComponent me, Type configType, Dictionary<string, byte[]> configBytes)
		{
			byte[] oneConfigBytes = configBytes[configType.Name];

			object category = ProtobufHelper.FromBytes(configType, oneConfigBytes, 0, oneConfigBytes.Length);

			lock (me)
			{
				me.AllConfig[configType] = category;	
			}
		}
	}
}