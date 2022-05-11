using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace ET
{
	namespace EventType
	{
		public class NumbericChange: DisposeObject
		{
			public static readonly NumbericChange Instance = new NumbericChange();
			
			public Entity Parent;
			public int NumericType;
			public long Old;
			public long New;
		}
	}

	[FriendClass(typeof(NumericComponent))]
	public static class NumericComponentSystem
	{
		public static float GetAsFloat(this NumericComponent me, int numericType)
		{
			return (float)me.GetByKey(numericType) / 10000;
		}

		public static int GetAsInt(this NumericComponent me, int numericType)
		{
			return (int)me.GetByKey(numericType);
		}
		
		public static long GetAsLong(this NumericComponent me, int numericType)
		{
			return me.GetByKey(numericType);
		}

		public static void Set(this NumericComponent me, int nt, float value)
		{
			me[nt] = (int) (value * 10000);
		}

		public static void Set(this NumericComponent me, int nt, int value)
		{
			me[nt] = value;
		}
		
		public static void Set(this NumericComponent me, int nt, long value)
		{
			me[nt] = value;
		}

		public static void SetNoEvent(this NumericComponent me, int numericType, long value)
		{
			me.Insert(numericType,value,false);
		}
		
		public static void Insert(this NumericComponent me, int numericType, long value,bool isPublicEvent = true)
		{
			long oldValue = me.GetByKey(numericType);
			if (oldValue == value)
			{
				return;
			}

			me.NumericDic[numericType] = value;

			if (numericType >= NumericType.Max)
			{
				me.Update(numericType,isPublicEvent);
				return;
			}

			if (isPublicEvent)
			{
				EventType.NumbericChange args = EventType.NumbericChange.Instance;
				args.Parent = me.Parent;
				args.NumericType = numericType;
				args.Old = oldValue;
				args.New = value;
				Game.EventSystem.PublishClass(args);
			}
		}
		
		public static long GetByKey(this NumericComponent me, int key)
		{
			long value = 0;
			me.NumericDic.TryGetValue(key, out value);
			return value;
		}

		public static void Update(this NumericComponent me, int numericType,bool isPublicEvent)
		{
			int final = (int) numericType / 10;
			int bas = final * 10 + 1; 
			int add = final * 10 + 2;
			int pct = final * 10 + 3;
			int finalAdd = final * 10 + 4;
			int finalPct = final * 10 + 5;

			// 一个数值可能会多种情况影响，比如速度,加个buff可能增加速度绝对值100，也有些buff增加10%速度，所以一个值可以由5个值进行控制其最终结果
			// final = (((base + add) * (100 + pct) / 100) + finalAdd) * (100 + finalPct) / 100;
			long result = (long)(((me.GetByKey(bas) + me.GetByKey(add)) * (100 + me.GetAsFloat(pct)) / 100f + me.GetByKey(finalAdd)) * (100 + me.GetAsFloat(finalPct)) / 100f);
			me.Insert(final,result,isPublicEvent);
		}
	}
	

	public class NumericComponent: Entity, IAwake, ITransfer
	{
		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public Dictionary<int, long> NumericDic = new Dictionary<int, long>();

		public long this[int numericType]
		{
			get
			{
				return this.GetByKey(numericType);
			}
			set
			{
				this.Insert(numericType,value);
			}
		}
	}
}