

namespace BulletSharp
{
	public class MlcpSolver : SequentialImpulseConstraintSolver
	{
		private MlcpSolverInterface _mlcpSolver;

		public MlcpSolver(MlcpSolverInterface solver)
			: base(UnsafeNativeMethods.btMLCPSolver_new(solver.Native), false)
		{
			_mlcpSolver = solver;
		}

		public void SetMLCPSolver(MlcpSolverInterface solver)
		{
			UnsafeNativeMethods.btMLCPSolver_setMLCPSolver(Native, solver.Native);
			_mlcpSolver = solver;
		}

		public int NumFallbacks
		{
			get { return  UnsafeNativeMethods.btMLCPSolver_getNumFallbacks(Native);}
			set {  UnsafeNativeMethods.btMLCPSolver_setNumFallbacks(Native, value);}
		}
	}
}
