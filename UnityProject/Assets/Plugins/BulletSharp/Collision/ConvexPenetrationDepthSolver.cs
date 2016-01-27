using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public abstract class ConvexPenetrationDepthSolver : IDisposable
	{
		internal IntPtr _native;

		internal ConvexPenetrationDepthSolver(IntPtr native)
		{
			_native = native;
		}

		public bool CalcPenDepth(VoronoiSimplexSolver simplexSolver, ConvexShape convexA, ConvexShape convexB, Matrix transA, Matrix transB, out Vector3 v, out Vector3 pa, out Vector3 pb, IDebugDraw debugDraw)
		{
			return btConvexPenetrationDepthSolver_calcPenDepth(_native, simplexSolver._native, convexA._native, convexB._native, ref transA, ref transB, out v, out pa, out pb, DebugDraw.GetUnmanaged(debugDraw));
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				btConvexPenetrationDepthSolver_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~ConvexPenetrationDepthSolver()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btConvexPenetrationDepthSolver_calcPenDepth(IntPtr obj, IntPtr simplexSolver, IntPtr convexA, IntPtr convexB, [In] ref Matrix transA, [In] ref Matrix transB, [Out] out Vector3 v, [Out] out Vector3 pa, [Out] out Vector3 pb, IntPtr debugDraw);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexPenetrationDepthSolver_delete(IntPtr obj);
	}
}
