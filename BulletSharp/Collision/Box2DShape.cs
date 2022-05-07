using System;
using System.Numerics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class Box2DShape : PolyhedralConvexShape
	{
		private Vector3Array _normals;
		private Vector3Array _vertices;

		public Box2DShape(Vector3 boxHalfExtents)
		{
			var native = btBox2dShape_new(ref boxHalfExtents);
			InitializeCollisionShape(native);
		}

		public Box2DShape(float boxHalfExtent)
		{
			var native = btBox2dShape_new2(boxHalfExtent);
			InitializeCollisionShape(native);
		}

		public Box2DShape(float boxHalfExtentX, float boxHalfExtentY, float boxHalfExtentZ)
		{
			var native = btBox2dShape_new3(boxHalfExtentX, boxHalfExtentY, boxHalfExtentZ);
			InitializeCollisionShape(native);
		}

		public void GetPlaneEquation(out Vector4 plane, int i)
		{
			btBox2dShape_getPlaneEquation(Native, out plane, i);
		}

		public Vector3 Centroid
		{
			get
			{
				btBox2dShape_getCentroid(Native, out var value);
				return value;
			}
		}

		public Vector3 HalfExtentsWithMargin
		{
			get
			{
				btBox2dShape_getHalfExtentsWithMargin(Native, out var value);
				return value;
			}
		}

		public Vector3 HalfExtentsWithoutMargin
		{
			get
			{
				btBox2dShape_getHalfExtentsWithoutMargin(Native, out var value);
				return value;
			}
		}

		public Vector3Array Normals
		{
			get
			{
				if (_normals == null)
				{
					_normals = new Vector3Array(btBox2dShape_getNormals(Native), 4);
				}
				return _normals;
			}
		}

		public Vector3Array Vertices
		{
			get
			{
				if (_vertices == null)
				{
					_vertices = new Vector3Array(btBox2dShape_getVertices(Native), 4);
				}
				return _vertices;
			}
		}
	}
}
