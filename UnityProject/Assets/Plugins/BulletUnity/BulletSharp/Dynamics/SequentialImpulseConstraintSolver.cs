using System;


namespace BulletSharp
{
	public class SequentialImpulseConstraintSolver : ConstraintSolver
	{
		internal SequentialImpulseConstraintSolver(IntPtr native, bool preventDelete)
			: base(native, preventDelete)
		{
		}

		public SequentialImpulseConstraintSolver()
			: base(UnsafeNativeMethods.btSequentialImpulseConstraintSolver_new(), false)
		{
		}

		public ulong BtRand2()
		{
			return UnsafeNativeMethods.btSequentialImpulseConstraintSolver_btRand2(Native);
		}

		public int BtRandInt2(int n)
		{
			return UnsafeNativeMethods.btSequentialImpulseConstraintSolver_btRandInt2(Native, n);
		}
/*
		public void SetConstraintRowSolverGeneric(SingleConstraintRowSolver rowSolver)
		{
			btSequentialImpulseConstraintSolver_setConstraintRowSolverGeneric(Native,
				rowSolver.Native);
		}

		public void SetConstraintRowSolverLowerLimit(SingleConstraintRowSolver rowSolver)
		{
			btSequentialImpulseConstraintSolver_setConstraintRowSolverLowerLimit(
				Native, rowSolver.Native);
		}

		public float SolveGroup(CollisionObject bodies, int numBodies, PersistentManifold manifold,
			int numManifolds, TypedConstraint constraints, int numConstraints, ContactSolverInfo info,
			IDebugDraw debugDrawer, Dispatcher dispatcher)
		{
			return btSequentialImpulseConstraintSolver_solveGroup(Native, bodies.Native,
				numBodies, manifold.Native, numManifolds, constraints.Native, numConstraints,
				info.Native, DebugDraw.GetUnmanaged(debugDrawer), dispatcher.Native);
		}

		public SingleConstraintRowSolver ActiveConstraintRowSolverGeneric
		{
			get { return btSequentialImpulseConstraintSolver_getActiveConstraintRowSolverGeneric(Native); }
		}

		public SingleConstraintRowSolver ActiveConstraintRowSolverLowerLimit
		{
			get { return btSequentialImpulseConstraintSolver_getActiveConstraintRowSolverLowerLimit(Native); }
		}
*/
		public ulong RandSeed
		{
			get => UnsafeNativeMethods.btSequentialImpulseConstraintSolver_getRandSeed(Native);
			set => UnsafeNativeMethods.btSequentialImpulseConstraintSolver_setRandSeed(Native, value);
		}
/*
		public SingleConstraintRowSolver ScalarConstraintRowSolverGeneric
		{
			get { return btSequentialImpulseConstraintSolver_getScalarConstraintRowSolverGeneric(Native); }
		}

		public SingleConstraintRowSolver ScalarConstraintRowSolverLowerLimit
		{
			get { return btSequentialImpulseConstraintSolver_getScalarConstraintRowSolverLowerLimit(Native); }
		}

		public SingleConstraintRowSolver SSE2ConstraintRowSolverGeneric
		{
			get { return btSequentialImpulseConstraintSolver_getSSE2ConstraintRowSolverGeneric(Native); }
		}

		public SingleConstraintRowSolver SSE2ConstraintRowSolverLowerLimit
		{
			get { return btSequentialImpulseConstraintSolver_getSSE2ConstraintRowSolverLowerLimit(Native); }
		}

		public SingleConstraintRowSolver SSE41ConstraintRowSolverGeneric
		{
			get { return btSequentialImpulseConstraintSolver_getSSE4_1ConstraintRowSolverGeneric(Native); }
		}

		public SingleConstraintRowSolver SSE41ConstraintRowSolverLowerLimit
		{
			get { return btSequentialImpulseConstraintSolver_getSSE4_1ConstraintRowSolverLowerLimit(Native); }
		}
*/
	}
}
