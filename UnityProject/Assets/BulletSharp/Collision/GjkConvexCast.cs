using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class GjkConvexCast : ConvexCast
	{
		public GjkConvexCast(ConvexShape convexA, ConvexShape convexB, VoronoiSimplexSolver simplexSolver)
			: base(btGjkConvexCast_new(convexA._native, convexB._native, simplexSolver._native))
		{
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGjkConvexCast_new(IntPtr convexA, IntPtr convexB, IntPtr simplexSolver);
	}
}
