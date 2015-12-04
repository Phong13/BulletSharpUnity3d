using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class MultiBodyDynamicsWorld : DiscreteDynamicsWorld
	{
        private List<MultiBody> _bodies;
        private List<MultiBodyConstraint> _constraints;

		public MultiBodyDynamicsWorld(Dispatcher dispatcher, BroadphaseInterface pairCache, MultiBodyConstraintSolver constraintSolver, CollisionConfiguration collisionConfiguration)
			: base(btMultiBodyDynamicsWorld_new(dispatcher._native, pairCache._native, constraintSolver._native, collisionConfiguration._native))
		{
            _constraintSolver = constraintSolver;
            _dispatcher = dispatcher;
            _broadphase = pairCache;

            _bodies = new List<MultiBody>();
            _constraints = new List<MultiBodyConstraint>();
		}

		public void AddMultiBody(MultiBody body)
		{
			btMultiBodyDynamicsWorld_addMultiBody(_native, body._native);
            _bodies.Add(body);
		}

        public void AddMultiBody(MultiBody body, CollisionFilterGroups group, CollisionFilterGroups mask)
		{
            btMultiBodyDynamicsWorld_addMultiBody3(_native, body._native, (short)group, (short)mask);
            _bodies.Add(body);
		}

		public void AddMultiBodyConstraint(MultiBodyConstraint constraint)
		{
			btMultiBodyDynamicsWorld_addMultiBodyConstraint(_native, constraint._native);
            _constraints.Add(constraint);
		}

		public void ClearMultiBodyConstraintForces()
		{
			btMultiBodyDynamicsWorld_clearMultiBodyConstraintForces(_native);
		}

		public void ClearMultiBodyForces()
		{
			btMultiBodyDynamicsWorld_clearMultiBodyForces(_native);
		}

		public void DebugDrawMultiBodyConstraint(MultiBodyConstraint constraint)
		{
			btMultiBodyDynamicsWorld_debugDrawMultiBodyConstraint(_native, constraint._native);
		}

		public void ForwardKinematics()
		{
			btMultiBodyDynamicsWorld_forwardKinematics(_native);
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
			btMultiBodyDynamicsWorld_integrateTransforms(_native, timeStep);
		}

		public void RemoveMultiBody(MultiBody body)
		{
			btMultiBodyDynamicsWorld_removeMultiBody(_native, body._native);
            _bodies.Remove(body);
		}

		public void RemoveMultiBodyConstraint(MultiBodyConstraint constraint)
		{
			btMultiBodyDynamicsWorld_removeMultiBodyConstraint(_native, constraint._native);
            _constraints.Remove(constraint);
		}

		public int NumMultibodies
		{
			get { return _bodies.Count; }
		}

		public int NumMultiBodyConstraints
		{
			get { return btMultiBodyDynamicsWorld_getNumMultiBodyConstraints(_native); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultiBodyDynamicsWorld_new(IntPtr dispatcher, IntPtr pairCache, IntPtr constraintSolver, IntPtr collisionConfiguration);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodyDynamicsWorld_addMultiBody(IntPtr obj, IntPtr body);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern void btMultiBodyDynamicsWorld_addMultiBody2(IntPtr obj, IntPtr body, short group);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodyDynamicsWorld_addMultiBody3(IntPtr obj, IntPtr body, short group, short mask);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodyDynamicsWorld_addMultiBodyConstraint(IntPtr obj, IntPtr constraint);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodyDynamicsWorld_clearMultiBodyConstraintForces(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodyDynamicsWorld_clearMultiBodyForces(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodyDynamicsWorld_debugDrawMultiBodyConstraint(IntPtr obj, IntPtr constraint);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodyDynamicsWorld_forwardKinematics(IntPtr obj);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern IntPtr btMultiBodyDynamicsWorld_getMultiBody(IntPtr obj, int mbIndex);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern IntPtr btMultiBodyDynamicsWorld_getMultiBodyConstraint(IntPtr obj, int constraintIndex);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern int btMultiBodyDynamicsWorld_getNumMultibodies(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btMultiBodyDynamicsWorld_getNumMultiBodyConstraints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodyDynamicsWorld_integrateTransforms(IntPtr obj, float timeStep);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodyDynamicsWorld_removeMultiBody(IntPtr obj, IntPtr body);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodyDynamicsWorld_removeMultiBodyConstraint(IntPtr obj, IntPtr constraint);
	}
}
