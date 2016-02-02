using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public static class GeometryUtil
	{
		public static bool AreVerticesBehindPlane(Vector3 planeNormal, AlignedVector3Array vertices, float margin)
		{
			return btGeometryUtil_areVerticesBehindPlane(ref planeNormal, vertices._native, margin);
		}

        public static void GetPlaneEquationsFromVertices(AlignedVector3Array vertices, AlignedVector3Array planeEquationsOut)
		{
			btGeometryUtil_getPlaneEquationsFromVertices(vertices._native, planeEquationsOut._native);
		}

        public static void GetVerticesFromPlaneEquations(AlignedVector3Array planeEquations, AlignedVector3Array verticesOut)
		{
			btGeometryUtil_getVerticesFromPlaneEquations(planeEquations._native, verticesOut._native);
		}
        /*
		public static bool IsInside(AlignedVector3Array vertices, Vector3 planeNormal, float margin)
		{
			return btGeometryUtil_isInside(vertices._native, ref planeNormal, margin);
		}
        */
        public static bool IsPointInsidePlanes(AlignedVector3Array planeEquations, Vector3 point, float margin)
		{
			return btGeometryUtil_isPointInsidePlanes(planeEquations._native, ref point, margin);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btGeometryUtil_areVerticesBehindPlane([In] ref Vector3 planeNormal, IntPtr vertices, float margin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeometryUtil_getPlaneEquationsFromVertices(IntPtr vertices, IntPtr planeEquationsOut);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeometryUtil_getVerticesFromPlaneEquations(IntPtr planeEquations, IntPtr verticesOut);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//[return: MarshalAs(UnmanagedType.I1)]
		//static extern bool btGeometryUtil_isInside(IntPtr vertices, IntPtr planeNormal, float margin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btGeometryUtil_isPointInsidePlanes(IntPtr planeEquations, [In] ref Vector3 point, float margin);
	}
}
