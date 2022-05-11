//
// Author:
//   Jb Evain (jbevain@gmail.com)
//
// Copyright (c) 2008 - 2015 Jb Evain
// Copyright (c) 2008 - 2011 Novell, Inc.
//
// Licensed under the MIT/X11 license.
//

using System.Text;

using ILRuntime.Mono.Collections.Generic;

namespace ILRuntime.Mono.Cecil {

	public interface IGenericInstance : IMetadataTokenProvider {

		bool HasGenericArguments { get; }
		Collection<TypeReference> GenericArguments { get; }
	}

	static partial class Mixin {

		public static bool ContainsGenericParameter (this IGenericInstance me)
		{
			var arguments = me.GenericArguments;

			for (int i = 0; i < arguments.Count; i++)
				if (arguments [i].ContainsGenericParameter)
					return true;

			return false;
		}

		public static void GenericInstanceFullName (this IGenericInstance me, StringBuilder builder)
		{
			builder.Append ("<");
			var arguments = me.GenericArguments;
			for (int i = 0; i < arguments.Count; i++) {
				if (i > 0)
					builder.Append (",");
				builder.Append (arguments [i].FullName);
			}
			builder.Append (">");
		}
	}
}
