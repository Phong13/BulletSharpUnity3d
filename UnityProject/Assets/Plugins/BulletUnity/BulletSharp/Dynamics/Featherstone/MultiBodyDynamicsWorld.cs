using System.Collections.Generic;


namespace BulletSharp
{
	public class MultiBodyDynamicsWorld : DiscreteDynamicsWorld
	{
		private List<MultiBody> _bodies;
		private List<MultiBodyConstraint> _constraints;

		public MultiBodyDynamicsWorld(Dispatcher dispatcher, BroadphaseInterface pairCache,
			MultiBodyConstraintSolver constraintSolver, CollisionConfiguration collisionConfiguration)
			: base(UnsafeNativeMethods.btMultiBodyDynamicsWorld_new(dispatcher.Native, pairCache.Native,
				constraintSolver.Native, collisionConfiguration.Native), dispatcher, pairCache)
		{
			_constraintSolver = constraintSolver;

			_bodies = new List<MultiBody>();
			_constraints = new List<MultiBodyConstraint>();
		}

		public void AddMultiBody(MultiBody body, int group = (int)CollisionFilterGroups.DefaultFilter,
			int mask = (int)CollisionFilterGroups.AllFilter)
		{
			UnsafeNativeMethods.btMultiBodyDynamicsWorld_addMultiBody(Native, body.Native, group,
				mask);
			_bodies.Add(body);
		}

		public void AddMultiBodyConstraint(MultiBodyConstraint constraint)
		{
			UnsafeNativeMethods.btMultiBodyDynamicsWorld_addMultiBodyConstraint(Native, constraint.Native);
			_constraints.Add(constraint);
		}

		public void ClearMultiBodyConstraintForces()
		{
			UnsafeNativeMethods.btMultiBodyDynamicsWorld_clearMultiBodyConstraintForces(Native);
		}

		public void ClearMultiBodyForces()
		{
			UnsafeNativeMethods.btMultiBodyDynamicsWorld_clearMultiBodyForces(Native);
		}

		public void DebugDrawMultiBodyConstraint(MultiBodyConstraint constraint)
		{
			UnsafeNativeMethods.btMultiBodyDynamicsWorld_debugDrawMultiBodyConstraint(Native, constraint.Native);
		}

		public void ForwardKinematics()
		{
			UnsafeNativeMethods.btMultiBodyDynamicsWorld_forwardKinematics(Native);
		}

		public MultiBody GetMultiBody(int mbIndex)
		{
			return _bodies[mbIndex];
		}

		public MultiBodyConstraint GetMultiBodyConstraint(int constraintIndex)
		{
			return _constraints[constraintIndex];
		}

		public void IntegrateTransforms(float timeStep)
		{
			UnsafeNativeMethods.btMultiBodyDynamicsWorld_integrateTransforms(Native, timeStep);
		}

		public void RemoveMultiBody(MultiBody body)
		{
			UnsafeNativeMethods.btMultiBodyDynamicsWorld_removeMultiBody(Native, body.Native);
			_bodies.Remove(body);
		}

		public void RemoveMultiBodyConstraint(MultiBodyConstraint constraint)
		{
			UnsafeNativeMethods.btMultiBodyDynamicsWorld_removeMultiBodyConstraint(Native, constraint.Native);
			_constraints.Remove(constraint);
		}

		public int NumMultibodies => _bodies.Count;

		public int NumMultiBodyConstraints => UnsafeNativeMethods.btMultiBodyDynamicsWorld_getNumMultiBodyConstraints(Native);
	}
}
