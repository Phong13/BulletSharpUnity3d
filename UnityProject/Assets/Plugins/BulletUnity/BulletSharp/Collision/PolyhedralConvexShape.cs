using System;
using BulletSharp.Math;


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
			UnsafeNativeMethods.btPolyhedralConvexShape_getEdge(Native, i, out pa, out pb);
		}

		public void GetPlane(out Vector3 planeNormal, out Vector3 planeSupport, int i)
		{
			UnsafeNativeMethods.btPolyhedralConvexShape_getPlane(Native, out planeNormal, out planeSupport,
				i);
		}

		public void GetVertex(int i, out Vector3 vtx)
		{
			UnsafeNativeMethods.btPolyhedralConvexShape_getVertex(Native, i, out vtx);
		}

		public bool InitializePolyhedralFeatures(int shiftVerticesByMargin = 0)
		{
			return UnsafeNativeMethods.btPolyhedralConvexShape_initializePolyhedralFeatures(Native,
				shiftVerticesByMargin);
		}

		public bool IsInsideRef(ref Vector3 pt, float tolerance)
		{
			return UnsafeNativeMethods.btPolyhedralConvexShape_isInside(Native, ref pt, tolerance);
		}

		public bool IsInside(Vector3 pt, float tolerance)
		{
			return UnsafeNativeMethods.btPolyhedralConvexShape_isInside(Native, ref pt, tolerance);
		}

		public ConvexPolyhedron ConvexPolyhedron
		{
			get
			{
				if (_convexPolyhedron == null)
				{
					IntPtr ptr = UnsafeNativeMethods.btPolyhedralConvexShape_getConvexPolyhedron(Native);
					if (ptr == IntPtr.Zero)
					{
						return null;
					}
					_convexPolyhedron = new ConvexPolyhedron();
				}
				return _convexPolyhedron;
			}
		}

		public int NumEdges => UnsafeNativeMethods.btPolyhedralConvexShape_getNumEdges(Native);

		public int NumPlanes => UnsafeNativeMethods.btPolyhedralConvexShape_getNumPlanes(Native);

		public int NumVertices => UnsafeNativeMethods.btPolyhedralConvexShape_getNumVertices(Native);
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
			UnsafeNativeMethods.btPolyhedralConvexAabbCachingShape_getNonvirtualAabb(Native, ref trans,
				out aabbMin, out aabbMax, margin);
		}

		public void GetNonvirtualAabb(Matrix trans, out Vector3 aabbMin, out Vector3 aabbMax,
			float margin)
		{
			UnsafeNativeMethods.btPolyhedralConvexAabbCachingShape_getNonvirtualAabb(Native, ref trans,
				out aabbMin, out aabbMax, margin);
		}

		public void RecalcLocalAabb()
		{
			UnsafeNativeMethods.btPolyhedralConvexAabbCachingShape_recalcLocalAabb(Native);
		}
	}
}
