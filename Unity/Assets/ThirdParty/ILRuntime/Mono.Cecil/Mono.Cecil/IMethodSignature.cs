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

	public interface IMethodSignature : IMetadataTokenProvider {

		bool HasThis { get; set; }
		bool ExplicitThis { get; set; }
		MethodCallingConvention CallingConvention { get; set; }

		bool HasParameters { get; }
		Collection<ParameterDefinition> Parameters { get; }
		TypeReference ReturnType { get; set; }
		MethodReturnType MethodReturnType { get; }
	}

	static partial class Mixin {

		public static bool HasImplicitThis (this IMethodSignature me)
		{
			return me.HasThis && !me.ExplicitThis;
		}

		public static void MethodSignatureFullName (this IMethodSignature me, StringBuilder builder)
		{
			builder.Append ("(");

			if (me.HasParameters) {
				var parameters = me.Parameters;
				for (int i = 0; i < parameters.Count; i++) {
					var parameter = parameters [i];
					if (i > 0)
						builder.Append (",");

					if (parameter.ParameterType.IsSentinel)
						builder.Append ("...,");

					builder.Append (parameter.ParameterType.FullName);
				}
			}

			builder.Append (")");
		}
	}
}
