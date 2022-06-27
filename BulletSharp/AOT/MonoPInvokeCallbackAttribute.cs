using System;

namespace AOT
{
	[AttributeUsage(System.AttributeTargets.Method)]
	internal sealed class MonoPInvokeCallbackAttribute : Attribute
	{
		public MonoPInvokeCallbackAttribute(Type type) {

		}
	}
}