using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class BoxShape : PolyhedralConvexShape
	{
		public BoxShape(Vector3 boxHalfExtents)
			: base(btBoxShape_new(ref boxHalfExtents))
		{
		}

		public BoxShape(float boxHalfExtent)
			: base(btBoxShape_new2(boxHalfExtent))
		{
		}

		public BoxShape(float boxHalfExtentX, float boxHalfExtentY, float boxHalfExtentZ)
			: base(btBoxShape_new3(boxHalfExtentX, boxHalfExtentY, boxHalfExtentZ))
		{
		}

		public void GetPlaneEquation(out Vector4 plane, int i)
		{
			btBoxShape_getPlaneEquation(Native, out plane, i);
		}

		public Vector3 HalfExtentsWithMargin
		{
			get
			{
				Vector3 value;
				btBoxShape_getHalfExtentsWithMargin(Native, out value);
				return value;
			}
		}

		public Vector3 HalfExtentsWithoutMargin
		{
			get
			{
				Vector3 value;
				btBoxShape_getHalfExtentsWithoutMargin(Native, out value);
				return value;
			}
		}
	}
}
