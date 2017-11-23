using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class MlcpSolver : SequentialImpulseConstraintSolver
	{
		private MlcpSolverInterface _mlcpSolver;

		public MlcpSolver(MlcpSolverInterface solver)
			: base(btMLCPSolver_new(solver.Native), false)
		{
			_mlcpSolver = solver;
		}

		public void SetMLCPSolver(MlcpSolverInterface solver)
		{
			btMLCPSolver_setMLCPSolver(Native, solver.Native);
			_mlcpSolver = solver;
		}

		public int NumFallbacks
		{
			get => btMLCPSolver_getNumFallbacks(Native);
			set => btMLCPSolver_setNumFallbacks(Native, value);
		}
	}
}
