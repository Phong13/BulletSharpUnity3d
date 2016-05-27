using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

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

		public void GetPlaneEquation(out Vector4 plane, out int i)
		{
			btBoxShape_getPlaneEquation(_native, out plane, out i);
		}

		public Vector3 HalfExtentsWithMargin
		{
			get
			{
				Vector3 value;
				btBoxShape_getHalfExtentsWithMargin(_native, out value);
				return value;
			}
		}

		public Vector3 HalfExtentsWithoutMargin
		{
			get
			{
				Vector3 value;
				btBoxShape_getHalfExtentsWithoutMargin(_native, out value);
				return value;
			}
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBoxShape_new([In] ref Vector3 boxHalfExtents);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btBoxShape_new2(float boxHalfExtent);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btBoxShape_new3(float boxHalfExtentX, float boxHalfExtentY, float boxHalfExtentZ);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBoxShape_getHalfExtentsWithMargin(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBoxShape_getHalfExtentsWithoutMargin(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBoxShape_getPlaneEquation(IntPtr obj, [Out] out Vector4 plane, [Out] out int i);
	}
}
