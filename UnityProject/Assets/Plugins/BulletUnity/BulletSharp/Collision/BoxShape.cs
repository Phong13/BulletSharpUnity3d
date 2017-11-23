using BulletSharp.Math;

namespace BulletSharp
{
	public class BoxShape : PolyhedralConvexShape
	{
		public BoxShape(Vector3 boxHalfExtents)
			: base(UnsafeNativeMethods.btBoxShape_new(ref boxHalfExtents))
		{
		}

		public BoxShape(float boxHalfExtent)
			: base(UnsafeNativeMethods.btBoxShape_new2(boxHalfExtent))
		{
		}

		public BoxShape(float boxHalfExtentX, float boxHalfExtentY, float boxHalfExtentZ)
			: base(UnsafeNativeMethods.btBoxShape_new3(boxHalfExtentX, boxHalfExtentY, boxHalfExtentZ))
		{
		}

		public void GetPlaneEquation(out Vector4 plane, int i)
		{
            UnsafeNativeMethods.btBoxShape_getPlaneEquation(Native, out plane, i);
		}

		public Vector3 HalfExtentsWithMargin
		{
			get
			{
				Vector3 value;
                UnsafeNativeMethods.btBoxShape_getHalfExtentsWithMargin(Native, out value);
				return value;
			}
		}

		public Vector3 HalfExtentsWithoutMargin
		{
			get
			{
				Vector3 value;
                UnsafeNativeMethods.btBoxShape_getHalfExtentsWithoutMargin(Native, out value);
				return value;
			}
		}
	}
}
