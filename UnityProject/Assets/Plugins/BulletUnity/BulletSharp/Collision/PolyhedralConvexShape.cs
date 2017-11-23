using System;
using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public abstract class PolyhedralConvexShape : ConvexInternalShape
	{
		private ConvexPolyhedron _convexPolyhedron;

		internal PolyhedralConvexShape(IntPtr native)
			: base(native)
		{
		}

		public void GetEdge(int i, out Vector3 pa, out Vector3 pb)
		{
			btPolyhedralConvexShape_getEdge(Native, i, out pa, out pb);
		}

		public void GetPlane(out Vector3 planeNormal, out Vector3 planeSupport, int i)
		{
			btPolyhedralConvexShape_getPlane(Native, out planeNormal, out planeSupport,
				i);
		}

		public void GetVertex(int i, out Vector3 vtx)
		{
			btPolyhedralConvexShape_getVertex(Native, i, out vtx);
		}

		public bool InitializePolyhedralFeatures(int shiftVerticesByMargin = 0)
		{
			return btPolyhedralConvexShape_initializePolyhedralFeatures(Native,
				shiftVerticesByMargin);
		}

		public bool IsInsideRef(ref Vector3 pt, float tolerance)
		{
			return btPolyhedralConvexShape_isInside(Native, ref pt, tolerance);
		}

		public bool IsInside(Vector3 pt, float tolerance)
		{
			return btPolyhedralConvexShape_isInside(Native, ref pt, tolerance);
		}

		public ConvexPolyhedron ConvexPolyhedron
		{
			get
			{
				if (_convexPolyhedron == null)
				{
					IntPtr ptr = btPolyhedralConvexShape_getConvexPolyhedron(Native);
					if (ptr == IntPtr.Zero)
					{
						return null;
					}
					_convexPolyhedron = new ConvexPolyhedron();
				}
				return _convexPolyhedron;
			}
		}

		public int NumEdges => btPolyhedralConvexShape_getNumEdges(Native);

		public int NumPlanes => btPolyhedralConvexShape_getNumPlanes(Native);

		public int NumVertices => btPolyhedralConvexShape_getNumVertices(Native);
	}

	public abstract class PolyhedralConvexAabbCachingShape : PolyhedralConvexShape
	{
		internal PolyhedralConvexAabbCachingShape(IntPtr native)
			: base(native)
		{
		}

		public void GetNonvirtualAabbRef(ref Matrix trans, out Vector3 aabbMin, out Vector3 aabbMax,
			float margin)
		{
			btPolyhedralConvexAabbCachingShape_getNonvirtualAabb(Native, ref trans,
				out aabbMin, out aabbMax, margin);
		}

		public void GetNonvirtualAabb(Matrix trans, out Vector3 aabbMin, out Vector3 aabbMax,
			float margin)
		{
			btPolyhedralConvexAabbCachingShape_getNonvirtualAabb(Native, ref trans,
				out aabbMin, out aabbMax, margin);
		}

		public void RecalcLocalAabb()
		{
			btPolyhedralConvexAabbCachingShape_recalcLocalAabb(Native);
		}
	}
}
