using System;


namespace BulletSharp
{
	public abstract class MlcpSolverInterface : IDisposable
	{
		internal IntPtr Native;

		internal MlcpSolverInterface(IntPtr native)
		{
			Native = native;
		}
		/*
		public bool SolveMLCP(btMatrixX<float> a, btVectorX<float> b, btVectorX<float> x,
			btVectorX<float> lo, btVectorX<float> hi, AlignedObjectArray<int> limitDependency,
			int numIterations, bool useSparsity = true)
		{
			return btMLCPSolverInterface_solveMLCP(Native, a.Native, b.Native,
				x.Native, lo.Native, hi.Native, limitDependency.Native, numIterations,
				useSparsity);
		}
		*/
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				UnsafeNativeMethods.btMLCPSolverInterface_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~MlcpSolverInterface()
		{
			Dispose(false);
		}
	}
}
