using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class MultiBodyConstraintSolver : SequentialImpulseConstraintSolver
	{
		public MultiBodyConstraintSolver()
			: base(btMultiBodyConstraintSolver_new(), false)
		{
		}
		/*
		public float SolveGroupCacheFriendlyFinish(CollisionObject bodies, int numBodies,
			ContactSolverInfo infoGlobal)
		{
			return btMultiBodyConstraintSolver_solveGroupCacheFriendlyFinish(Native,
				bodies.Native, numBodies, infoGlobal.Native);
		}

		public void SolveMultiBodyGroup(CollisionObject bodies, int numBodies, PersistentManifold manifold,
			int numManifolds, TypedConstraint constraints, int numConstraints, MultiBodyConstraint multiBodyConstraints,
			int numMultiBodyConstraints, ContactSolverInfo info, IDebugDraw debugDrawer,
			Dispatcher dispatcher)
		{
			btMultiBodyConstraintSolver_solveMultiBodyGroup(Native, bodies.Native,
				numBodies, manifold.Native, numManifolds, constraints.Native, numConstraints,
				multiBodyConstraints.Native, numMultiBodyConstraints, info.Native,
				debugDrawer.Native, dispatcher.Native);
		}
		*/
	}
}
