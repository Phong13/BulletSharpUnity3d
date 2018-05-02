using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;

namespace BulletSharp
{
	public class MultiBodyDynamicsWorld : DiscreteDynamicsWorld
	{
		private List<MultiBody> _bodies;
		private List<MultiBodyConstraint> _constraints;

        // See comments in CollisionWorld
        public static MultiBodyDynamicsWorld CreateMultiBodyDynamicsWorld(Dispatcher dispatcher, BroadphaseInterface pairCache, MultiBodyConstraintSolver constraintSolver, CollisionConfiguration collisionConfiguration)
        {
            MultiBodyDynamicsWorld w = new MultiBodyDynamicsWorld(dispatcher, pairCache, constraintSolver);
            w.CreateNativePart(collisionConfiguration);
            return w;
        }

        protected MultiBodyDynamicsWorld(Dispatcher dispatcher, BroadphaseInterface pairCache,
			MultiBodyConstraintSolver constraintSolver)
			: base(dispatcher, pairCache, constraintSolver)
		{
			_bodies = new List<MultiBody>();
			_constraints = new List<MultiBodyConstraint>();
		}

        protected override void CreateNativePart(CollisionConfiguration collisionConfiguration)
        {
            Native = UnsafeNativeMethods.btMultiBodyDynamicsWorld_new(
                _dispatcher.Native, 
                _broadphase.Native,
                _constraintSolver.Native, 
                collisionConfiguration.Native);
            CollisionObjectArray = new AlignedCollisionObjectArray(UnsafeNativeMethods.btCollisionWorld_getCollisionObjectArray(Native), this);
            _native2ManagedMap.Add(Native, this);
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

		public int NumMultibodies{ get { return  _bodies.Count;} }

		public int NumMultiBodyConstraints{ get { return  UnsafeNativeMethods.btMultiBodyDynamicsWorld_getNumMultiBodyConstraints(Native);} }
	}
}
