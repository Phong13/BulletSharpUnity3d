using BulletSharp.Math;

namespace BulletSharp
{
	public class Box2DShape : PolyhedralConvexShape
	{
		private Vector3Array _normals;
		private Vector3Array _vertices;

		public Box2DShape(Vector3 boxHalfExtents)
			: base(UnsafeNativeMethods.btBox2dShape_new(ref boxHalfExtents))
		{
		}

		public Box2DShape(float boxHalfExtent)
			: base(UnsafeNativeMethods.btBox2dShape_new2(boxHalfExtent))
		{
		}

		public Box2DShape(float boxHalfExtentX, float boxHalfExtentY, float boxHalfExtentZ)
			: base(UnsafeNativeMethods.btBox2dShape_new3(boxHalfExtentX, boxHalfExtentY, boxHalfExtentZ))
		{
		}

		public void GetPlaneEquation(out Vector4 plane, int i)
		{
            UnsafeNativeMethods.btBox2dShape_getPlaneEquation(Native, out plane, i);
		}

		public Vector3 Centroid
		{
			get
			{
				Vector3 value;
                UnsafeNativeMethods.btBox2dShape_getCentroid(Native, out value);
				return value;
			}
		}

		public Vector3 HalfExtentsWithMargin
		{
			get
			{
				Vector3 value;
                UnsafeNativeMethods.btBox2dShape_getHalfExtentsWithMargin(Native, out value);
				return value;
			}
		}

		public Vector3 HalfExtentsWithoutMargin
		{
			get
			{
				Vector3 value;
                UnsafeNativeMethods.btBox2dShape_getHalfExtentsWithoutMargin(Native, out value);
				return value;
			}
		}

		public Vector3Array Normals
		{
			get
			{
				if (_normals == null)
				{
					_normals = new Vector3Array(UnsafeNativeMethods.btBox2dShape_getNormals(Native), 4);
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
					_vertices = new Vector3Array(UnsafeNativeMethods.btBox2dShape_getVertices(Native), 4);
				}
				return _vertices;
			}
		}
	}
}
