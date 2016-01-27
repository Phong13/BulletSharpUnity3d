using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public abstract class PolyhedralConvexShape : ConvexInternalShape
	{
        ConvexPolyhedron _convexPolyhedron;

		internal PolyhedralConvexShape(IntPtr native)
			: base(native)
		{
		}

		public void GetEdge(int i, out Vector3 pa, out Vector3 pb)
		{
			btPolyhedralConvexShape_getEdge(_native, i, out pa, out pb);
		}

		public void GetPlane(out Vector3 planeNormal, out Vector3 planeSupport, int i)
		{
			btPolyhedralConvexShape_getPlane(_native, out planeNormal, out planeSupport, i);
		}

		public void GetVertex(int i, out Vector3 vtx)
		{
			btPolyhedralConvexShape_getVertex(_native, i, out vtx);
		}

		public bool InitializePolyhedralFeatures()
		{
			return btPolyhedralConvexShape_initializePolyhedralFeatures(_native);
		}

		public bool InitializePolyhedralFeatures(int shiftVerticesByMargin)
		{
			return btPolyhedralConvexShape_initializePolyhedralFeatures2(_native, shiftVerticesByMargin);
		}

        public bool IsInside(ref Vector3 pt, float tolerance)
		{
			return btPolyhedralConvexShape_isInside(_native, ref pt, tolerance);
		}

		public ConvexPolyhedron ConvexPolyhedron
		{
            get
            {
                if (_convexPolyhedron == null)
                {
                    IntPtr ptr = btPolyhedralConvexShape_getConvexPolyhedron(_native);
                    if (ptr == IntPtr.Zero)
                    {
                        return null;
                    }
                    _convexPolyhedron = new ConvexPolyhedron();
                }
                return _convexPolyhedron;
            }
		}

		public int NumEdges
		{
			get { return btPolyhedralConvexShape_getNumEdges(_native); }
		}

		public int NumPlanes
		{
			get { return btPolyhedralConvexShape_getNumPlanes(_native); }
		}

		public int NumVertices
		{
			get { return btPolyhedralConvexShape_getNumVertices(_native); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btPolyhedralConvexShape_getConvexPolyhedron(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPolyhedralConvexShape_getEdge(IntPtr obj, int i, [Out] out Vector3 pa, [Out] out Vector3 pb);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btPolyhedralConvexShape_getNumEdges(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btPolyhedralConvexShape_getNumPlanes(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btPolyhedralConvexShape_getNumVertices(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPolyhedralConvexShape_getPlane(IntPtr obj, [Out] out Vector3 planeNormal, [Out] out Vector3 planeSupport, int i);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPolyhedralConvexShape_getVertex(IntPtr obj, int i, [Out] out Vector3 vtx);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btPolyhedralConvexShape_initializePolyhedralFeatures(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btPolyhedralConvexShape_initializePolyhedralFeatures2(IntPtr obj, int shiftVerticesByMargin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btPolyhedralConvexShape_isInside(IntPtr obj, [In] ref Vector3 pt, float tolerance);
	}

	public abstract class PolyhedralConvexAabbCachingShape : PolyhedralConvexShape
	{
		internal PolyhedralConvexAabbCachingShape(IntPtr native)
			: base(native)
		{
		}

        public void GetNonvirtualAabbRef(ref Matrix trans, out Vector3 aabbMin, out Vector3 aabbMax, float margin)
        {
            btPolyhedralConvexAabbCachingShape_getNonvirtualAabb(_native, ref trans, out aabbMin, out aabbMax, margin);
        }

		public void GetNonvirtualAabb(Matrix trans, out Vector3 aabbMin, out Vector3 aabbMax, float margin)
		{
			btPolyhedralConvexAabbCachingShape_getNonvirtualAabb(_native, ref trans, out aabbMin, out aabbMax, margin);
		}

		public void RecalcLocalAabb()
		{
			btPolyhedralConvexAabbCachingShape_recalcLocalAabb(_native);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPolyhedralConvexAabbCachingShape_getNonvirtualAabb(IntPtr obj, [In] ref Matrix trans, [Out] out Vector3 aabbMin, [Out] out Vector3 aabbMax, float margin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPolyhedralConvexAabbCachingShape_recalcLocalAabb(IntPtr obj);
	}
}
