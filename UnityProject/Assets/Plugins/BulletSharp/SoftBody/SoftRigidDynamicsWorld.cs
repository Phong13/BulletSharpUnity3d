using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp.SoftBody
{
	public class SoftRigidDynamicsWorld : DiscreteDynamicsWorld
	{
        private AlignedSoftBodyArray _softBodyArray;
        private SoftBodySolver _softBodySolver; // private ref passed to bodies during AddSoftBody
        private bool _ownsSolver;
        private SoftBodyWorldInfo _worldInfo;

		public SoftRigidDynamicsWorld(Dispatcher dispatcher, BroadphaseInterface pairCache, ConstraintSolver constraintSolver, CollisionConfiguration collisionConfiguration)
			: base(IntPtr.Zero)
		{
            _softBodySolver = new DefaultSoftBodySolver();
            _ownsSolver = true;

            _native = btSoftRigidDynamicsWorld_new2(dispatcher._native, pairCache._native,
                (constraintSolver != null) ? constraintSolver._native : IntPtr.Zero,
                collisionConfiguration._native, _softBodySolver._native);

            _collisionObjectArray = new AlignedCollisionObjectArray(btCollisionWorld_getCollisionObjectArray(_native), this);

            _dispatcher = dispatcher;
            _broadphase = pairCache;
            _constraintSolver = constraintSolver;
            _worldInfo = new SoftBodyWorldInfo(btSoftRigidDynamicsWorld_getWorldInfo(_native), true);
            _worldInfo.Dispatcher = dispatcher;
            _worldInfo.Broadphase = pairCache;
            _native2ManagedMap.Add(_native, this);
        }

		public SoftRigidDynamicsWorld(Dispatcher dispatcher, BroadphaseInterface pairCache, ConstraintSolver constraintSolver, CollisionConfiguration collisionConfiguration, SoftBodySolver softBodySolver)
			: base(IntPtr.Zero)
		{
            if (softBodySolver != null) {
                _softBodySolver = softBodySolver;
                _ownsSolver = false;
            } else {
                _softBodySolver = new DefaultSoftBodySolver();
                _ownsSolver = true;
            }

            _native = btSoftRigidDynamicsWorld_new2(dispatcher._native, pairCache._native,
                (constraintSolver != null) ? constraintSolver._native : IntPtr.Zero,
                collisionConfiguration._native, _softBodySolver._native);

            _collisionObjectArray = new AlignedCollisionObjectArray(btCollisionWorld_getCollisionObjectArray(_native), this);

            _dispatcher = dispatcher;
            _broadphase = pairCache;
            _constraintSolver = constraintSolver;
            _worldInfo = new SoftBodyWorldInfo(btSoftRigidDynamicsWorld_getWorldInfo(_native), true);
            _worldInfo.Dispatcher = dispatcher;
            _worldInfo.Broadphase = pairCache;
            _native2ManagedMap.Add(_native, this);
        }

		public void AddSoftBody(SoftBody body)
		{
            body.SoftBodySolver = _softBodySolver;
            _collisionObjectArray.Add(body);
		}

        public void AddSoftBody(SoftBody body, CollisionFilterGroups collisionFilterGroup, CollisionFilterGroups collisionFilterMask)
		{
            body.SoftBodySolver = _softBodySolver;
            _collisionObjectArray.Add(body, (short)collisionFilterGroup, (short)collisionFilterMask);
		}

        public void AddSoftBody(SoftBody body, short collisionFilterGroup, short collisionFilterMask)
        {
            body.SoftBodySolver = _softBodySolver;
            _collisionObjectArray.Add(body, collisionFilterGroup, collisionFilterMask);
        }

		public void RemoveSoftBody(SoftBody body)
		{
            _collisionObjectArray.Remove(body);
		}

		public int DrawFlags
		{
			get { return btSoftRigidDynamicsWorld_getDrawFlags(_native); }
			set { btSoftRigidDynamicsWorld_setDrawFlags(_native, value); }
		}

		public AlignedSoftBodyArray SoftBodyArray
		{
            get
            {
                if (_softBodyArray == null)
                {
                    _softBodyArray = new AlignedSoftBodyArray(btSoftRigidDynamicsWorld_getSoftBodyArray(_native));
                }
                return _softBodyArray;
            }
		}

		public SoftBodyWorldInfo WorldInfo
		{
            get { return _worldInfo; }
		}

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

		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern IntPtr btSoftRigidDynamicsWorld_new(IntPtr dispatcher, IntPtr pairCache, IntPtr constraintSolver, IntPtr collisionConfiguration);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftRigidDynamicsWorld_new2(IntPtr dispatcher, IntPtr pairCache, IntPtr constraintSolver, IntPtr collisionConfiguration, IntPtr softBodySolver);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btSoftRigidDynamicsWorld_getDrawFlags(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftRigidDynamicsWorld_getSoftBodyArray(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftRigidDynamicsWorld_getWorldInfo(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftRigidDynamicsWorld_setDrawFlags(IntPtr obj, int f);
	}
}
