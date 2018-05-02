using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;
using AOT;


namespace BulletSharp
{
	public enum DynamicsWorldType
	{
		Simple = 1,
		Discrete = 2,
		Continuous = 3,
		SoftRigid = 4,
		Gpu = 5
	}

	public abstract class DynamicsWorld : CollisionWorld
	{
        /// <summary>
        /// This is a hack to deal with the fact that callbacks (preTick and postTick) must be static on AOT platforms.
        /// 
        /// The problem is that when native code calls the static managed function (eg. preTick): Which DynamicsWorld instance should the call apply to?
        /// I have delt with this in other cases by having native wrappers cache a reference to the managed wrapper and added a parameter
        /// to the static function: parameter = IntPtr to the managed wrapper that the call applies to.
        /// It is a lot of work to use that approach in this case because Native does not point to a native BulletSharp wrapper (which I am willing to modify),
        /// instead it points to the raw btDynamicsWorld object which I am very reluctant to modify because I don't want to get into touching Bullet Physics source
        /// which makes updating harder. I could write wrappers for all the differen world types implementing all the methods but that is a lot of code to write an maintain.
        /// 
        /// Instead the hack is to maintain a static dictionary that maps a native pointer to a btPhysicsWorld to its corresponding wrapper on the managed side.
        /// This dictionary exists entirely here on the managed side.
        /// In the preTick and postTick calls we will look up the Managed wrapper that corresponds to the native World.
        /// </summary>
        protected static Dictionary<IntPtr, DynamicsWorld> _native2ManagedMap = new Dictionary<IntPtr, DynamicsWorld>();

        public delegate void InternalTickCallback(DynamicsWorld world, float timeStep);
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		delegate void InternalTickCallbackUnmanaged(IntPtr world, float timeStep);

		private InternalTickCallback _preTickCallback;
		private InternalTickCallback _postTickCallback;
		private InternalTickCallbackUnmanaged _preTickCallbackUnmanaged;
		private InternalTickCallbackUnmanaged _postTickCallbackUnmanaged;
		protected ConstraintSolver _constraintSolver;
		private ContactSolverInfo _solverInfo;

		private Dictionary<IAction, ActionInterfaceWrapper> _actions;
		private List<TypedConstraint> _constraints = new List<TypedConstraint>();

        protected DynamicsWorld(Dispatcher dispatcher, BroadphaseInterface broadphasePairCache) : base(dispatcher, broadphasePairCache)
        {
        }

        public void AddAction(IAction action)
		{
			if (_actions == null)
			{
				_actions = new Dictionary<IAction, ActionInterfaceWrapper>();
			}
			else if (_actions.ContainsKey(action))
			{
				return;
			}

			ActionInterfaceWrapper wrapper = new ActionInterfaceWrapper(action, this);
			_actions.Add(action, wrapper);
			UnsafeNativeMethods.btDynamicsWorld_addAction(Native, wrapper._native);
		}

		public void AddConstraint(TypedConstraint constraint, bool disableCollisionsBetweenLinkedBodies = false)
		{
			_constraints.Add(constraint);
			UnsafeNativeMethods.btDynamicsWorld_addConstraint(Native, constraint.Native, disableCollisionsBetweenLinkedBodies);

			if (disableCollisionsBetweenLinkedBodies)
			{
				RigidBody rigidBody = constraint.RigidBodyA;
				if (rigidBody._constraintRefs == null)
				{
					rigidBody._constraintRefs = new List<TypedConstraint>();
				}
				rigidBody._constraintRefs.Add(constraint);

				rigidBody = constraint.RigidBodyB;
				if (rigidBody._constraintRefs == null)
				{
					rigidBody._constraintRefs = new List<TypedConstraint>();
				}
				rigidBody._constraintRefs.Add(constraint);
			}
		}

		public void AddRigidBody(RigidBody body)
		{
			CollisionObjectArray.Add(body);
		}

		public void AddRigidBody(RigidBody body, CollisionFilterGroups group, CollisionFilterGroups mask)
		{
			CollisionObjectArray.Add(body, (int)group, (int)mask);
		}

		public void AddRigidBody(RigidBody body, int group, int mask)
		{
			CollisionObjectArray.Add(body, group, mask);
		}

		public void ClearForces()
		{
			UnsafeNativeMethods.btDynamicsWorld_clearForces(Native);
		}

		public TypedConstraint GetConstraint(int index)
		{
			System.Diagnostics.Debug.Assert(UnsafeNativeMethods.btDynamicsWorld_getConstraint(Native, index) == _constraints[index].Native);
			return _constraints[index];
		}

		public void GetGravity(out Vector3 gravity)
		{
			UnsafeNativeMethods.btDynamicsWorld_getGravity(Native, out gravity);
		}

		public void RemoveAction(IAction action)
		{
			if (_actions == null)
			{
				// No actions have been added
				return;
			}

			ActionInterfaceWrapper wrapper;
			if (_actions.TryGetValue(action, out wrapper))
			{
				UnsafeNativeMethods.btDynamicsWorld_removeAction(Native, wrapper._native);
				_actions.Remove(action);
				wrapper.Dispose();
			}
		}

		public void RemoveConstraint(TypedConstraint constraint)
		{
			RigidBody rigidBody = constraint.RigidBodyA;
			if (rigidBody._constraintRefs != null)
			{
				rigidBody._constraintRefs.Remove(constraint);
			}
			rigidBody = constraint.RigidBodyB;
			if (rigidBody._constraintRefs != null)
			{
				rigidBody._constraintRefs.Remove(constraint);
			}

			int itemIndex = _constraints.IndexOf(constraint);
			int lastIndex = _constraints.Count - 1;
			_constraints[itemIndex] = _constraints[lastIndex];
			_constraints.RemoveAt(lastIndex);
			UnsafeNativeMethods.btDynamicsWorld_removeConstraint(Native, constraint.Native);
		}

		public void RemoveRigidBody(RigidBody body)
		{
			CollisionObjectArray.Remove(body);
		}

		public void SetGravity(ref Vector3 gravity)
		{
			UnsafeNativeMethods.btDynamicsWorld_setGravity(Native, ref gravity);
		}

        [MonoPInvokeCallback(typeof(InternalTickCallbackUnmanaged))]
        static private void InternalPreTickCallbackNative(IntPtr world, float timeStep)
		{
            DynamicsWorld dw = _native2ManagedMap[world];
            if (dw != null) dw._preTickCallback(dw, timeStep);
		}

        [MonoPInvokeCallback(typeof(InternalTickCallbackUnmanaged))]
        static private void InternalPostTickCallbackNative(IntPtr world, float timeStep)
		{
            DynamicsWorld dw = _native2ManagedMap[world];
            if (dw != null) dw._postTickCallback(dw, timeStep);
		}

        public void SetInternalTickCallback(InternalTickCallback callback, Object worldUserInfo = null,
			bool isPreTick = false)
		{
			if (isPreTick)
			{
				SetInternalPreTickCallback(callback);
			}
			else
			{
				SetInternalPostTickCallback(callback);
			}
			WorldUserInfo = worldUserInfo;
		}

		private void SetInternalPreTickCallback(InternalTickCallback callback)
		{
			if (_preTickCallback != callback)
			{
				_preTickCallback = callback;
				if (callback != null)
				{
					if (_preTickCallbackUnmanaged == null)
					{
						_preTickCallbackUnmanaged = new InternalTickCallbackUnmanaged(InternalPreTickCallbackNative);
					}
					UnsafeNativeMethods.btDynamicsWorld_setInternalTickCallback(Native,
						Marshal.GetFunctionPointerForDelegate(_preTickCallbackUnmanaged), IntPtr.Zero, true);
				}
				else
				{
					_preTickCallbackUnmanaged = null;
					UnsafeNativeMethods.btDynamicsWorld_setInternalTickCallback(Native, IntPtr.Zero, IntPtr.Zero, true);
				}
			}
		}

		private void SetInternalPostTickCallback(InternalTickCallback callback)
		{
			if (_postTickCallback != callback)
			{
				_postTickCallback = callback;
				if (callback != null)
				{
					if (_postTickCallbackUnmanaged == null)
					{
						_postTickCallbackUnmanaged = new InternalTickCallbackUnmanaged(InternalPostTickCallbackNative);
					}
					UnsafeNativeMethods.btDynamicsWorld_setInternalTickCallback(Native,
						Marshal.GetFunctionPointerForDelegate(_postTickCallbackUnmanaged), IntPtr.Zero, false);
				}
				else
				{
					_postTickCallbackUnmanaged = null;
					UnsafeNativeMethods.btDynamicsWorld_setInternalTickCallback(Native, IntPtr.Zero, IntPtr.Zero, false);
				}
			}
		}

		public int StepSimulation(float timeStep, int maxSubSteps = 1, float fixedTimeStep = 1.0f / 60.0f)
		{
			return UnsafeNativeMethods.btDynamicsWorld_stepSimulation(Native, timeStep, maxSubSteps,
				fixedTimeStep);
		}

		public void SynchronizeMotionStates()
		{
			UnsafeNativeMethods.btDynamicsWorld_synchronizeMotionStates(Native);
		}

		public ConstraintSolver ConstraintSolver
		{
			get
			{
				if (_constraintSolver == null)
				{
					_constraintSolver = new SequentialImpulseConstraintSolver(UnsafeNativeMethods.btDynamicsWorld_getConstraintSolver(Native), true);
				}
				return _constraintSolver;
			}
			set
			{
				_constraintSolver = value;
				UnsafeNativeMethods.btDynamicsWorld_setConstraintSolver(Native, value.Native);
			}
		}

		public Vector3 Gravity
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btDynamicsWorld_getGravity(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btDynamicsWorld_setGravity(Native, ref value);}
		}

		public int NumConstraints{ get { return  UnsafeNativeMethods.btDynamicsWorld_getNumConstraints(Native);} }

		public ContactSolverInfo SolverInfo
		{
			get
			{
				if (_solverInfo == null)
				{
					_solverInfo = new ContactSolverInfo(UnsafeNativeMethods.btDynamicsWorld_getSolverInfo(Native), true);
				}
				return _solverInfo;
			}
		}

		public DynamicsWorldType WorldType{ get { return  UnsafeNativeMethods.btDynamicsWorld_getWorldType(Native);} }

		public Object WorldUserInfo { get; set; }

		protected override void Dispose(bool disposing)
		{
			if (_actions != null)
			{
				foreach (ActionInterfaceWrapper wrapper in _actions.Values)
				{
					wrapper.Dispose();
				}
			}
            if (Native != IntPtr.Zero)
            {
                if (_native2ManagedMap.ContainsKey(Native))
                {
                    _native2ManagedMap.Remove(Native);
                }
            }
            base.Dispose(disposing);
		}
	}
}
