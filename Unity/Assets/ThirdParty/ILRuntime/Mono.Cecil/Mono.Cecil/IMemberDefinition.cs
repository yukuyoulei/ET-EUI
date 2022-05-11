//
// Author:
//   Jb Evain (jbevain@gmail.com)
//
// Copyright (c) 2008 - 2015 Jb Evain
// Copyright (c) 2008 - 2011 Novell, Inc.
//
// Licensed under the MIT/X11 license.
//

namespace ILRuntime.Mono.Cecil {

	public interface IMemberDefinition : ICustomAttributeProvider {

		string Name { get; set; }
		string FullName { get; }

		bool IsSpecialName { get; set; }
		bool IsRuntimeSpecialName { get; set; }

		TypeDefinition DeclaringType { get; set; }
	}

	static partial class Mixin {

		public static bool GetAttributes (this uint me, uint attributes)
		{
			return (me & attributes) != 0;
		}

		public static uint SetAttributes (this uint me, uint attributes, bool value)
		{
			if (value)
				return me | attributes;

			return me & ~attributes;
		}

		public static bool GetMaskedAttributes (this uint me, uint mask, uint attributes)
		{
			return (me & mask) == attributes;
		}

		public static uint SetMaskedAttributes (this uint me, uint mask, uint attributes, bool value)
		{
			if (value) {
				me &= ~mask;
				return me | attributes;
			}

			return me & ~(mask & attributes);
		}

		public static bool GetAttributes (this ushort me, ushort attributes)
		{
			return (me & attributes) != 0;
		}

		public static ushort SetAttributes (this ushort me, ushort attributes, bool value)
		{
			if (value)
				return (ushort) (me | attributes);

			return (ushort) (me & ~attributes);
		}

		public static bool GetMaskedAttributes (this ushort me, ushort mask, uint attributes)
		{
			return (me & mask) == attributes;
		}

		public static ushort SetMaskedAttributes (this ushort me, ushort mask, uint attributes, bool value)
		{
			if (value) {
				me = (ushort) (me & ~mask);
				return (ushort) (me | attributes);
			}

			return (ushort) (me & ~(mask & attributes));
		}
	}
}
