using System;


namespace BulletSharp
{
	public enum ConstraintSolverType
	{
		SequentialImpulse = 1,
		Mlcp = 2,
		Nncg = 4
	}

	public abstract class ConstraintSolver : IDisposable
	{
		internal IntPtr Native;
		private bool _preventDelete;

		internal ConstraintSolver(IntPtr native, bool preventDelete)
		{
			Native = native;
			_preventDelete = preventDelete;
		}

		public void AllSolved(ContactSolverInfo __unnamed0, IDebugDraw __unnamed1)
		{
			UnsafeNativeMethods.btConstraintSolver_allSolved(Native, __unnamed0.Native, DebugDraw.GetUnmanaged(__unnamed1));
		}

		public void PrepareSolve(int __unnamed0, int __unnamed1)
		{
			UnsafeNativeMethods.btConstraintSolver_prepareSolve(Native, __unnamed0, __unnamed1);
		}

		public void Reset()
		{
			UnsafeNativeMethods.btConstraintSolver_reset(Native);
		}
		/*
		public float SolveGroup(CollisionObject bodies, int numBodies, PersistentManifold manifold,
			int numManifolds, TypedConstraint constraints, int numConstraints, ContactSolverInfo info,
			IDebugDraw debugDrawer, Dispatcher dispatcher)
		{
			return UnsafeNativeMethods.btConstraintSolver_solveGroup(Native, bodies._native, numBodies,
				manifold._native, numManifolds, constraints._native, numConstraints,
				info._native, DebugDraw.GetUnmanaged(debugDrawer), dispatcher._native);
		}
		*/
		public ConstraintSolverType SolverType => UnsafeNativeMethods.btConstraintSolver_getSolverType(Native);

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				if (!_preventDelete)
				{
					UnsafeNativeMethods.btConstraintSolver_delete(Native);
				}
				Native = IntPtr.Zero;
			}
		}

		~ConstraintSolver()
		{
			Dispose(false);
		}
	}
}
