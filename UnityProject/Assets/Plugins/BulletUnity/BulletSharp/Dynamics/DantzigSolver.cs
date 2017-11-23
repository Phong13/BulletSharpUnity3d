using System;
using static BulletSharp.UnsafeNativeMethods;

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
	}
}
