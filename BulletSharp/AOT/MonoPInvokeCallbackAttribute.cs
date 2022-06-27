using System;

namespace AOT
{
	[AttributeUsage(System.AttributeTargets.Method)]
	public sealed class MonoPInvokeCallbackAttribute : Attribute
	{
		public MonoPInvokeCallbackAttribute(Type type) {

		}
	}
}