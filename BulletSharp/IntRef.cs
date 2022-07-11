using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace BulletSharp
{
	internal static class IntRef
	{
		public static ulong LoadValueIn<T>(T tvalue) {
			return (ulong)GCHandle.ToIntPtr(GCHandle.Alloc(tvalue,GCHandleType.Weak));
		}

		public static T GetValue<T>(ulong value) {
			var hanndle = GCHandle.FromIntPtr(new IntPtr((long)value));
			return hanndle.Target is null ? throw new Exception("Data was garbage collected") : (T)hanndle.Target;
		}
	}
}
