using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public abstract class MlcpSolverInterface : IDisposable
	{
		internal IntPtr _native;

		internal MlcpSolverInterface(IntPtr native)
		{
			_native = native;
		}
        /*
		public bool SolveMLCP(MatrixXf a, VectorXf b, VectorXf x, VectorXf lo, VectorXf hi, AlignedObjectArray limitDependency, int numIterations)
		{
			return btMLCPSolverInterface_solveMLCP(_native, a._native, b._native, x._native, lo._native, hi._native, limitDependency._native, numIterations);
		}

		public bool SolveMLCP(MatrixXf a, VectorXf b, VectorXf x, VectorXf lo, VectorXf hi, AlignedObjectArray limitDependency, int numIterations, bool useSparsity)
		{
			return btMLCPSolverInterface_solveMLCP2(_native, a._native, b._native, x._native, lo._native, hi._native, limitDependency._native, numIterations, useSparsity);
		}
        */
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				btMLCPSolverInterface_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~MlcpSolverInterface()
		{
			Dispose(false);
		}

		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//[return: MarshalAs(UnmanagedType.I1)]
		//static extern bool btMLCPSolverInterface_solveMLCP(IntPtr obj, IntPtr A, IntPtr b, IntPtr x, IntPtr lo, IntPtr hi, IntPtr limitDependency, int numIterations);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//[return: MarshalAs(UnmanagedType.I1)]
		//static extern bool btMLCPSolverInterface_solveMLCP2(IntPtr obj, IntPtr A, IntPtr b, IntPtr x, IntPtr lo, IntPtr hi, IntPtr limitDependency, int numIterations, bool useSparsity);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMLCPSolverInterface_delete(IntPtr obj);
	}
}
