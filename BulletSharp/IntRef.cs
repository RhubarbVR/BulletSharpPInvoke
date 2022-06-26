using System;
using System.Collections.Generic;
using System.Text;

namespace BulletSharp
{
	internal static class IntRef
	{
		public static ulong currentValue;

		public static ulong GetNextValue() {
			lock (typeof(IntRef)) {
				currentValue++;
			}
			return currentValue;
		}

		public static readonly Dictionary<ulong, object> pairs = new();

		public static ulong LoadValueIn<T>(T tvalue) {
			var value = GetNextValue();
			lock (pairs) {
				pairs.Add(value, tvalue);
				if(tvalue is BulletDisposableObject disposableObject) {
					disposableObject.OnDispose += () => pairs.Remove(value);
				}
			}
			return value;
		}

		public static T GetValue<T>(ulong value) {
			return (T)pairs[value];
		}
	}
}
