using System;
using BulletSharp.Math;


namespace BulletSharp
{
	public class BuSimplex1To4 : PolyhedralConvexAabbCachingShape
	{
		internal BuSimplex1To4(IntPtr native)
			: base(native)
		{
		}

		public BuSimplex1To4()
			: base(UnsafeNativeMethods.btBU_Simplex1to4_new())
		{
		}

		public BuSimplex1To4(Vector3 pt0)
			: base(UnsafeNativeMethods.btBU_Simplex1to4_new2(ref pt0))
		{
		}

		public BuSimplex1To4(Vector3 pt0, Vector3 pt1)
			: base(UnsafeNativeMethods.btBU_Simplex1to4_new3(ref pt0, ref pt1))
		{
		}

		public BuSimplex1To4(Vector3 pt0, Vector3 pt1, Vector3 pt2)
			: base(UnsafeNativeMethods.btBU_Simplex1to4_new4(ref pt0, ref pt1, ref pt2))
		{
		}

		public BuSimplex1To4(Vector3 pt0, Vector3 pt1, Vector3 pt2, Vector3 pt3)
			: base(UnsafeNativeMethods.btBU_Simplex1to4_new5(ref pt0, ref pt1, ref pt2, ref pt3))
		{
		}

		public void AddVertexRef(ref Vector3 pt)
		{
			UnsafeNativeMethods.btBU_Simplex1to4_addVertex(Native, ref pt);
		}

		public void AddVertex(Vector3 pt)
		{
			UnsafeNativeMethods.btBU_Simplex1to4_addVertex(Native, ref pt);
		}

		public int GetIndex(int i)
		{
			return UnsafeNativeMethods.btBU_Simplex1to4_getIndex(Native, i);
		}

		public void Reset()
		{
			UnsafeNativeMethods.btBU_Simplex1to4_reset(Native);
		}
	}
}
