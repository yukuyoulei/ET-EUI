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
using ILRuntime.Mono.Collections.Generic;

namespace ILRuntime.Mono {

	static class Empty<T> {

		public static readonly T [] Array = new T [0];
	}

	class ArgumentNullOrEmptyException : ArgumentException {

		public ArgumentNullOrEmptyException (string paramName)
			: base ("Argument null or empty", paramName)
		{
		}
	}
}

namespace ILRuntime.Mono.Cecil {

	static partial class Mixin {

		public static bool IsNullOrEmpty<T> (this T [] me)
		{
			return me == null || me.Length == 0;
		}

		public static bool IsNullOrEmpty<T> (this Collection<T> me)
		{
			return me == null || me.size == 0;
		}

		public static T [] Resize<T> (this T [] me, int length)
		{
			Array.Resize (ref me, length);
			return me;
		}

		public static T [] Add<T> (this T [] me, T item)
		{
			if (me == null) {
				me = new [] { item };
				return me;
			}

			me = me.Resize (me.Length + 1);
			me [me.Length - 1] = item;
			return me;
		}
	}
}
