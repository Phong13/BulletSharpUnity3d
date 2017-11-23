using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class ConstraintSolverPoolMultiThreaded : ConstraintSolver
	{
		public ConstraintSolverPoolMultiThreaded(int numSolvers)
			: base(btConstraintSolverPoolMt_new(numSolvers), false)
		{
		}
	}

	public class DiscreteDynamicsWorldMultiThreaded : DiscreteDynamicsWorld
	{
		public DiscreteDynamicsWorldMultiThreaded(Dispatcher dispatcher, BroadphaseInterface pairCache,
			ConstraintSolverPoolMultiThreaded constraintSolver, CollisionConfiguration collisionConfiguration)
			: base(btDiscreteDynamicsWorldMt_new(dispatcher != null ? dispatcher.Native : IntPtr.Zero,
				pairCache != null ? pairCache.Native : IntPtr.Zero, constraintSolver != null ? constraintSolver.Native : IntPtr.Zero,
				collisionConfiguration != null ? collisionConfiguration.Native : IntPtr.Zero), dispatcher, pairCache)
		{
		}
	}
}
