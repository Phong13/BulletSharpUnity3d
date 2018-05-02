using System;
using System.Runtime.InteropServices;

namespace BulletSharp
{
	public class ConstraintSolverPoolMultiThreaded : ConstraintSolver
	{
		public ConstraintSolverPoolMultiThreaded(int numSolvers)
			: base(UnsafeNativeMethods.btConstraintSolverPoolMt_new(numSolvers), false)
		{
		}
	}

	public class DiscreteDynamicsWorldMultiThreaded : DiscreteDynamicsWorld
	{
        // See comments in CollisionWorld
        public static DiscreteDynamicsWorldMultiThreaded CreateDiscreteDynamicsWorldMultiThreaded(Dispatcher dispatcher, BroadphaseInterface pairCache,
            ConstraintSolverPoolMultiThreaded constraintSolver, CollisionConfiguration collisionConfiguration)
        {
            DiscreteDynamicsWorldMultiThreaded w = new DiscreteDynamicsWorldMultiThreaded(dispatcher, pairCache, constraintSolver);
            w.CreateNativePart(collisionConfiguration);
            return w;
        }

        protected DiscreteDynamicsWorldMultiThreaded(Dispatcher dispatcher, BroadphaseInterface pairCache,
			ConstraintSolverPoolMultiThreaded constraintSolver)
			: base(dispatcher, pairCache, constraintSolver)
		{
		}

        protected override void CreateNativePart(CollisionConfiguration collisionConfiguration)
        {
            Native = UnsafeNativeMethods.btDiscreteDynamicsWorldMt_new(
                _dispatcher != null ? _dispatcher.Native : IntPtr.Zero,
                _broadphase != null ? _broadphase.Native : IntPtr.Zero, 
                _constraintSolver != null ? _constraintSolver.Native : IntPtr.Zero,
                collisionConfiguration != null ? collisionConfiguration.Native : IntPtr.Zero);
            CollisionObjectArray = new AlignedCollisionObjectArray(UnsafeNativeMethods.btCollisionWorld_getCollisionObjectArray(Native), this);
            _native2ManagedMap.Add(Native, this);
        }
    }
}
