using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class Box2DShape : PolyhedralConvexShape
	{
        private Vector3Array _normals;
        private Vector3Array _vertices;

		public Box2DShape(Vector3 boxHalfExtents)
			: base(btBox2dShape_new(ref boxHalfExtents))
		{
		}

        public Box2DShape(float boxHalfExtent)
            : base(btBox2dShape_new2(boxHalfExtent))
        {
        }

        public Box2DShape(float boxHalfExtentX, float boxHalfExtentY, float boxHalfExtentZ)
            : base(btBox2dShape_new3(boxHalfExtentX, boxHalfExtentY, boxHalfExtentZ))
        {
        }

		public void GetPlaneEquation(out Vector4 plane, int i)
		{
			btBox2dShape_getPlaneEquation(_native, out plane, i);
		}

		public Vector3 Centroid
		{
			get
			{
				Vector3 value;
				btBox2dShape_getCentroid(_native, out value);
				return value;
			}
		}

		public Vector3 HalfExtentsWithMargin
		{
			get
			{
				Vector3 value;
				btBox2dShape_getHalfExtentsWithMargin(_native, out value);
				return value;
			}
		}

		public Vector3 HalfExtentsWithoutMargin
		{
			get
			{
				Vector3 value;
				btBox2dShape_getHalfExtentsWithoutMargin(_native, out value);
				return value;
			}
		}

		public Vector3Array Normals
		{
			get
			{
                if (_normals == null)
                {
                    _normals = new Vector3Array(btBox2dShape_getNormals(_native), 4);
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
                    _vertices = new Vector3Array(btBox2dShape_getVertices(_native), 4);
                }
                return _vertices;
			}
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBox2dShape_new([In] ref Vector3 boxHalfExtents);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btBox2dShape_new2(float boxHalfExtent);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btBox2dShape_new3(float boxHalfExtentX, float boxHalfExtentY, float boxHalfExtentZ);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBox2dShape_getCentroid(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBox2dShape_getHalfExtentsWithMargin(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBox2dShape_getHalfExtentsWithoutMargin(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBox2dShape_getNormals(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBox2dShape_getPlaneEquation(IntPtr obj, [Out] out Vector4 plane, int i);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBox2dShape_getVertices(IntPtr obj);
	}
}
