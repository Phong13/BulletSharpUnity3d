using System;
using System.Runtime.InteropServices;

namespace BulletSharp.SoftBody
{
	public class SoftRigidDynamicsWorld : DiscreteDynamicsWorld
	{
        // See comments in CollisionWorld
        public static SoftRigidDynamicsWorld CreateSoftRigidDynamicsWorld(Dispatcher dispatcher, BroadphaseInterface pairCache,
            ConstraintSolver constraintSolver, CollisionConfiguration collisionConfiguration,
            SoftBodySolver softBodySolver = null)
        {
            SoftRigidDynamicsWorld w = new SoftRigidDynamicsWorld(dispatcher, pairCache, constraintSolver, softBodySolver);
            w.CreateNativePart(collisionConfiguration);
            return w;
        }

        private AlignedSoftBodyArray _softBodyArray;
		private SoftBodySolver _softBodySolver; // private ref passed to bodies during AddSoftBody
		private bool _ownsSolver;

        protected SoftRigidDynamicsWorld(Dispatcher dispatcher, BroadphaseInterface pairCache,
			ConstraintSolver constraintSolver, 
			SoftBodySolver softBodySolver = null)
			: base(dispatcher, pairCache, constraintSolver)
		{
            if (softBodySolver != null)
            {
                _softBodySolver = softBodySolver;
                _ownsSolver = false;
            }
            else
            {
                _softBodySolver = new DefaultSoftBodySolver();
                _ownsSolver = true;
            }

			_constraintSolver = constraintSolver;
		}

        protected override void CreateNativePart(CollisionConfiguration collisionConfiguration)
        {
            Native = UnsafeNativeMethods.btSoftRigidDynamicsWorld_new(
                _dispatcher.Native, _broadphase.Native,
                (_constraintSolver != null) ? _constraintSolver.Native : IntPtr.Zero,
                collisionConfiguration.Native, 
                _softBodySolver._native);
            CollisionObjectArray = new AlignedCollisionObjectArray(UnsafeNativeMethods.btCollisionWorld_getCollisionObjectArray(Native), this);
            WorldInfo = new SoftBodyWorldInfo(UnsafeNativeMethods.btSoftRigidDynamicsWorld_getWorldInfo(Native), true);
            WorldInfo.Dispatcher = _dispatcher;
            WorldInfo.Broadphase = _broadphase;
            _native2ManagedMap.Add(Native, this);
        }

        public void AddSoftBody(SoftBody body)
		{
			body.SoftBodySolver = _softBodySolver;
			CollisionObjectArray.Add(body);
		}

		public void AddSoftBody(SoftBody body, CollisionFilterGroups collisionFilterGroup, CollisionFilterGroups collisionFilterMask)
		{
			body.SoftBodySolver = _softBodySolver;
			CollisionObjectArray.Add(body, (int)collisionFilterGroup, (int)collisionFilterMask);
		}

		public void AddSoftBody(SoftBody body, int collisionFilterGroup, int collisionFilterMask)
		{
			body.SoftBodySolver = _softBodySolver;
			CollisionObjectArray.Add(body, collisionFilterGroup, collisionFilterMask);
		}

		public void RemoveSoftBody(SoftBody body)
		{
			CollisionObjectArray.Remove(body);
		}

		public int DrawFlags
		{
			get { return  UnsafeNativeMethods.btSoftRigidDynamicsWorld_getDrawFlags(Native);}
			set {  UnsafeNativeMethods.btSoftRigidDynamicsWorld_setDrawFlags(Native, value);}
		}

		public AlignedSoftBodyArray SoftBodyArray
		{
			get
			{
				if (_softBodyArray == null)
				{
					_softBodyArray = new AlignedSoftBodyArray(UnsafeNativeMethods.btSoftRigidDynamicsWorld_getSoftBodyArray(Native));
				}
				return _softBodyArray;
			}
		}

		public SoftBodyWorldInfo WorldInfo { get; set; }

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_ownsSolver)
				{
					_softBodySolver.Dispose();
				}
			}
			base.Dispose(disposing);
		}
	}
}
