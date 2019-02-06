using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class DantzigSolver : MlcpSolverInterface
	{
		internal DantzigSolver(IntPtr native)
			: base(native)
		{
		}

		public DantzigSolver()
			: base(btDantzigSolver_new())
		{
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDantzigSolver_new();
	}
}
