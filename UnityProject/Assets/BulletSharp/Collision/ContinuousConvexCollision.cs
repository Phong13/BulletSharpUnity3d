using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class ContinuousConvexCollision : ConvexCast
	{
		public ContinuousConvexCollision(ConvexShape shapeA, ConvexShape shapeB, VoronoiSimplexSolver simplexSolver, ConvexPenetrationDepthSolver penetrationDepthSolver)
			: base(btContinuousConvexCollision_new(shapeA._native, shapeB._native, simplexSolver._native, penetrationDepthSolver._native))
		{
		}

		public ContinuousConvexCollision(ConvexShape shapeA, StaticPlaneShape plane)
			: base(btContinuousConvexCollision_new2(shapeA._native, plane._native))
		{
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btContinuousConvexCollision_new(IntPtr shapeA, IntPtr shapeB, IntPtr simplexSolver, IntPtr penetrationDepthSolver);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btContinuousConvexCollision_new2(IntPtr shapeA, IntPtr plane);
	}
}
