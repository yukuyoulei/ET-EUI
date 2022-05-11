//
// Author:
//   Jb Evain (jbevain@gmail.com)
//
// Copyright (c) 2008 - 2015 Jb Evain
// Copyright (c) 2008 - 2011 Novell, Inc.
//
// Licensed under the MIT/X11 license.
//

using System;
using System.Threading;
using ILRuntime.Mono.Collections.Generic;

namespace ILRuntime.Mono.Cecil {

	public interface ICustomAttributeProvider : IMetadataTokenProvider {

		Collection<CustomAttribute> CustomAttributes { get; }

		bool HasCustomAttributes { get; }
	}

	static partial class Mixin {

		public static bool GetHasCustomAttributes (
			this ICustomAttributeProvider me,
			ModuleDefinition module)
		{
			return module.HasImage () && module.Read (me, (provider, reader) => reader.HasCustomAttributes (provider));
		}

		public static Collection<CustomAttribute> GetCustomAttributes (
			this ICustomAttributeProvider me,
			ref Collection<CustomAttribute> variable,
			ModuleDefinition module)
		{
			if (module.HasImage ())
				return module.Read (ref variable, me, (provider, reader) => reader.ReadCustomAttributes (provider));

			Interlocked.CompareExchange (ref variable, new Collection<CustomAttribute> (), null);
			return variable;
		}
	}
}
