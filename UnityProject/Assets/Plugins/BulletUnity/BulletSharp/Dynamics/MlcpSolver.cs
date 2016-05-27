using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class MlcpSolver : SequentialImpulseConstraintSolver
	{
        private MlcpSolverInterface _mlcpSolver;

		public MlcpSolver(MlcpSolverInterface solver)
			: base(btMLCPSolver_new(solver._native), false)
		{
            _mlcpSolver = solver;
		}

		public void SetMLCPSolver(MlcpSolverInterface solver)
		{
			btMLCPSolver_setMLCPSolver(_native, solver._native);
            _mlcpSolver = solver;
		}

		public int NumFallbacks
		{
			get { return btMLCPSolver_getNumFallbacks(_native); }
			set { btMLCPSolver_setNumFallbacks(_native, value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMLCPSolver_new(IntPtr solver);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btMLCPSolver_getNumFallbacks(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMLCPSolver_setMLCPSolver(IntPtr obj, IntPtr solver);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMLCPSolver_setNumFallbacks(IntPtr obj, int num);
	}
}
