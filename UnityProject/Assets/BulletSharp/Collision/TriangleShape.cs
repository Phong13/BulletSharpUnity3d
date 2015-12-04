using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class TriangleShape : PolyhedralConvexShape
	{
        private Vector3Array _vertices;

		internal TriangleShape(IntPtr native)
			: base(native)
		{
		}

		public TriangleShape()
			: base(btTriangleShape_new())
		{
		}

		public TriangleShape(Vector3 p0, Vector3 p1, Vector3 p2)
			: base(btTriangleShape_new2(ref p0, ref p1, ref p2))
		{
		}

		public void CalcNormal(out Vector3 normal)
		{
			btTriangleShape_calcNormal(_native, out normal);
		}

		public void GetPlaneEquation(int i, out Vector3 planeNormal, out Vector3 planeSupport)
		{
			btTriangleShape_getPlaneEquation(_native, i, out planeNormal, out planeSupport);
		}

		public IntPtr GetVertexPtr(int index)
		{
			return btTriangleShape_getVertexPtr(_native, index);
		}

		public Vector3Array Vertices
		{
			get
			{
                if (_vertices == null)
                {
                    _vertices = new Vector3Array(btTriangleShape_getVertices1(_native), 3);
                }
                return _vertices;
			}
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btTriangleShape_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btTriangleShape_new2([In] ref Vector3 p0, [In] ref Vector3 p1, [In] ref Vector3 p2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTriangleShape_calcNormal(IntPtr obj, [Out] out Vector3 normal);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTriangleShape_getPlaneEquation(IntPtr obj, int i, [Out] out Vector3 planeNormal, [Out] out Vector3 planeSupport);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btTriangleShape_getVertexPtr(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btTriangleShape_getVertices1(IntPtr obj);
	}
}
