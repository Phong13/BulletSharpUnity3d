using System;


namespace BulletSharp
{
	public class DantzigSolver : MlcpSolverInterface
	{
		internal DantzigSolver(IntPtr native)
			: base(native)
		{
		}

		public DantzigSolver()
			: base(UnsafeNativeMethods.btDantzigSolver_new())
		{
		}
	}
}
