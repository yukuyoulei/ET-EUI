namespace ET
{
	[ObjectSystem]
	public class UnitComponentAwakeSystem : AwakeSystem<UnitComponent>
	{
		public override void Awake(UnitComponent me)
		{
		}
	}
	
	[ObjectSystem]
	public class UnitComponentDestroySystem : DestroySystem<UnitComponent>
	{
		public override void Destroy(UnitComponent me)
		{
		}
	}
	
	public static class UnitComponentSystem
	{
		public static void Add(this UnitComponent me, Unit unit)
		{
		}

		public static Unit Get(this UnitComponent me, long id)
		{
			Unit unit = me.GetChild<Unit>(id);
			return unit;
		}

		public static void Remove(this UnitComponent me, long id)
		{
			Unit unit = me.GetChild<Unit>(id);
			unit?.Dispose();
		}
	}
}