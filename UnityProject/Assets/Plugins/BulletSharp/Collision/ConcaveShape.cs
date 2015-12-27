using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
    public enum PhyScalarType
	{
        Float = 0,
        Double = 1,
        Integer = 2,
        Short = 3,
        FixedPoint88 = 4,
        UChar = 5
	}

	public class ConcaveShape : CollisionShape
	{
		internal ConcaveShape(IntPtr native)
			: base(native)
		{
		}

		public void ProcessAllTriangles(TriangleCallback callback, Vector3 aabbMin, Vector3 aabbMax)
		{
			btConcaveShape_processAllTriangles(_native, callback._native, ref aabbMin, ref aabbMax);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConcaveShape_processAllTriangles(IntPtr obj, IntPtr callback, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax);
	}
}
