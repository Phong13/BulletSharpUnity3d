using System;
using BulletSharp.Math;


namespace BulletSharp
{
	public abstract class ConvexPenetrationDepthSolver : IDisposable
	{
		internal IntPtr Native;

		internal ConvexPenetrationDepthSolver(IntPtr native)
		{
			Native = native;
		}

		public bool CalcPenDepth(VoronoiSimplexSolver simplexSolver, ConvexShape convexA,
			ConvexShape convexB, Matrix transA, Matrix transB, out Vector3 v, out Vector3 pa,
			out Vector3 pb, IDebugDraw debugDraw)
		{
			return UnsafeNativeMethods.btConvexPenetrationDepthSolver_calcPenDepth(Native, simplexSolver.Native,
				convexA.Native, convexB.Native, ref transA, ref transB, out v, out pa,
				out pb, DebugDraw.GetUnmanaged(debugDraw));
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				UnsafeNativeMethods.btConvexPenetrationDepthSolver_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~ConvexPenetrationDepthSolver()
		{
			Dispose(false);
		}
	}
}
