using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace BulletSharp
{
	internal static class IntRef
	{
		public static ulong LoadValueIn<T>(T tvalue) {
			return (ulong)Marshal.GetIUnknownForObject(tvalue);
		}

		public static T GetValue<T>(ulong value) {
			return (T)Marshal.GetObjectForIUnknown(new IntPtr((long)value));
		}
	}
}
