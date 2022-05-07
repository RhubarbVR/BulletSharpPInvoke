using System;
using System.Numerics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class BoxShape : PolyhedralConvexShape
	{
		public BoxShape(Vector3 boxHalfExtents)
		{
			var native = btBoxShape_new(ref boxHalfExtents);
			InitializeCollisionShape(native);
		}

		public BoxShape(float boxHalfExtent)
		{
			var native = btBoxShape_new2(boxHalfExtent);
			InitializeCollisionShape(native);
		}

		public BoxShape(float boxHalfExtentX, float boxHalfExtentY, float boxHalfExtentZ)
		{
			var native = btBoxShape_new3(boxHalfExtentX, boxHalfExtentY, boxHalfExtentZ);
			InitializeCollisionShape(native);
		}

		public void GetPlaneEquation(out Vector4 plane, int i)
		{
			btBoxShape_getPlaneEquation(Native, out plane, i);
		}

		public Vector3 HalfExtentsWithMargin
		{
			get
			{
				btBoxShape_getHalfExtentsWithMargin(Native, out var value);
				return value;
			}
		}

		public Vector3 HalfExtentsWithoutMargin
		{
			get
			{
				btBoxShape_getHalfExtentsWithoutMargin(Native, out var value);
				return value;
			}
		}
	}
}
