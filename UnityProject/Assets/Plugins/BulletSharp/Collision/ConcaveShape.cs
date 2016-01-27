using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public enum PhyScalarType
	{
		Float,
		Double,
		Integer,
		Short,
		FixedPoint88,
		UChar
	}

	public abstract class ConcaveShape : CollisionShape
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
