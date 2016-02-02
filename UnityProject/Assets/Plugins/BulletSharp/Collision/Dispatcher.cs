using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public enum DispatchFunc
	{
		Discrete = 1,
		Continuous
	}

	public class DispatcherInfo
	{
		internal IntPtr _native;

		internal DispatcherInfo(IntPtr native)
		{
			_native = native;
		}

		public float AllowedCcdPenetration
		{
			get { return btDispatcherInfo_getAllowedCcdPenetration(_native); }
			set { btDispatcherInfo_setAllowedCcdPenetration(_native, value); }
		}

		public float ConvexConservativeDistanceThreshold
		{
			get { return btDispatcherInfo_getConvexConservativeDistanceThreshold(_native); }
			set { btDispatcherInfo_setConvexConservativeDistanceThreshold(_native, value); }
		}

		public IDebugDraw DebugDraw
		{
			get { return BulletSharp.DebugDraw.GetManaged(btDispatcherInfo_getDebugDraw(_native)); }
            set { btDispatcherInfo_setDebugDraw(_native, BulletSharp.DebugDraw.GetUnmanaged(value)); }
		}

        public DispatchFunc DispatchFunc
		{
			get { return btDispatcherInfo_getDispatchFunc(_native); }
			set { btDispatcherInfo_setDispatchFunc(_native, value); }
		}

		public bool EnableSatConvex
		{
			get { return btDispatcherInfo_getEnableSatConvex(_native); }
			set { btDispatcherInfo_setEnableSatConvex(_native, value); }
		}

		public bool EnableSpu
		{
			get { return btDispatcherInfo_getEnableSPU(_native); }
			set { btDispatcherInfo_setEnableSPU(_native, value); }
		}

		public int StepCount
		{
			get { return btDispatcherInfo_getStepCount(_native); }
			set { btDispatcherInfo_setStepCount(_native, value); }
		}

		public float TimeOfImpact
		{
			get { return btDispatcherInfo_getTimeOfImpact(_native); }
			set { btDispatcherInfo_setTimeOfImpact(_native, value); }
		}

		public float TimeStep
		{
			get { return btDispatcherInfo_getTimeStep(_native); }
			set { btDispatcherInfo_setTimeStep(_native, value); }
		}

		public bool UseContinuous
		{
			get { return btDispatcherInfo_getUseContinuous(_native); }
			set { btDispatcherInfo_setUseContinuous(_native, value); }
		}

		public bool UseConvexConservativeDistanceUtil
		{
			get { return btDispatcherInfo_getUseConvexConservativeDistanceUtil(_native); }
			set { btDispatcherInfo_setUseConvexConservativeDistanceUtil(_native, value); }
		}

		public bool UseEpa
		{
			get { return btDispatcherInfo_getUseEpa(_native); }
			set { btDispatcherInfo_setUseEpa(_native, value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btDispatcherInfo_getAllowedCcdPenetration(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btDispatcherInfo_getConvexConservativeDistanceThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDispatcherInfo_getDebugDraw(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern DispatchFunc btDispatcherInfo_getDispatchFunc(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btDispatcherInfo_getEnableSatConvex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btDispatcherInfo_getEnableSPU(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btDispatcherInfo_getStepCount(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btDispatcherInfo_getTimeOfImpact(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btDispatcherInfo_getTimeStep(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btDispatcherInfo_getUseContinuous(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btDispatcherInfo_getUseConvexConservativeDistanceUtil(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btDispatcherInfo_getUseEpa(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDispatcherInfo_setAllowedCcdPenetration(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDispatcherInfo_setConvexConservativeDistanceThreshold(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDispatcherInfo_setDebugDraw(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btDispatcherInfo_setDispatchFunc(IntPtr obj, DispatchFunc value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDispatcherInfo_setEnableSatConvex(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDispatcherInfo_setEnableSPU(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDispatcherInfo_setStepCount(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDispatcherInfo_setTimeOfImpact(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDispatcherInfo_setTimeStep(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDispatcherInfo_setUseContinuous(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDispatcherInfo_setUseConvexConservativeDistanceUtil(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDispatcherInfo_setUseEpa(IntPtr obj, bool value);
	}

	public abstract class Dispatcher : IDisposable
	{
		internal IntPtr _native;

        internal List<CollisionWorld> _worldRefs = new List<CollisionWorld>(1);
        internal bool _worldDeferredCleanup;

		internal Dispatcher(IntPtr native)
		{
			_native = native;
		}

		public IntPtr AllocateCollisionAlgorithm(int size)
		{
			return btDispatcher_allocateCollisionAlgorithm(_native, size);
		}

		public void ClearManifold(PersistentManifold manifold)
		{
			btDispatcher_clearManifold(_native, manifold._native);
		}

		public void DispatchAllCollisionPairs(OverlappingPairCache pairCache, DispatcherInfo dispatchInfo, Dispatcher dispatcher)
		{
			btDispatcher_dispatchAllCollisionPairs(_native, pairCache._native, dispatchInfo._native, dispatcher._native);
		}

		public CollisionAlgorithm FindAlgorithm(CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap)
		{
            return new CollisionAlgorithm(btDispatcher_findAlgorithm(_native, body0Wrap._native, body1Wrap._native));
		}

		public CollisionAlgorithm FindAlgorithm(CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap, PersistentManifold sharedManifold)
		{
            return new CollisionAlgorithm(btDispatcher_findAlgorithm2(_native, body0Wrap._native, body1Wrap._native, sharedManifold._native));
		}

		public void FreeCollisionAlgorithm(IntPtr ptr)
		{
			btDispatcher_freeCollisionAlgorithm(_native, ptr);
		}

		public PersistentManifold GetManifoldByIndexInternal(int index)
		{
            return new PersistentManifold(btDispatcher_getManifoldByIndexInternal(_native, index), true);
		}

		public PersistentManifold GetNewManifold(CollisionObject b0, CollisionObject b1)
		{
			return new PersistentManifold(btDispatcher_getNewManifold(_native, b0._native, b1._native), true);
		}

		public bool NeedsCollision(CollisionObject body0, CollisionObject body1)
		{
			return btDispatcher_needsCollision(_native, body0._native, body1._native);
		}

		public bool NeedsResponse(CollisionObject body0, CollisionObject body1)
		{
			return btDispatcher_needsResponse(_native, body0._native, body1._native);
		}

		public void ReleaseManifold(PersistentManifold manifold)
		{
			btDispatcher_releaseManifold(_native, manifold._native);
		}
        /*
		public PersistentManifold InternalManifoldPointer
		{
			get { return btDispatcher_getInternalManifoldPointer(_native); }
		}

		public PoolAllocator InternalManifoldPool
		{
			get { return btDispatcher_getInternalManifoldPool(_native); }
		}
        */
		public int NumManifolds
		{
			get { return btDispatcher_getNumManifolds(_native); }
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
                if (_worldRefs.Count == 0)
                {
                    btDispatcher_delete(_native);
                    _native = IntPtr.Zero;
                }
                else
                {
                    // Can't delete dispatcher, because it is referenced by a world,
                    // tell the world to clean up the broadphase later.
                    _worldDeferredCleanup = true;
                }
			}
		}

		~Dispatcher()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDispatcher_allocateCollisionAlgorithm(IntPtr obj, int size);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDispatcher_clearManifold(IntPtr obj, IntPtr manifold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDispatcher_dispatchAllCollisionPairs(IntPtr obj, IntPtr pairCache, IntPtr dispatchInfo, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDispatcher_findAlgorithm(IntPtr obj, IntPtr body0Wrap, IntPtr body1Wrap);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDispatcher_findAlgorithm2(IntPtr obj, IntPtr body0Wrap, IntPtr body1Wrap, IntPtr sharedManifold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDispatcher_freeCollisionAlgorithm(IntPtr obj, IntPtr ptr);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDispatcher_getInternalManifoldPointer(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDispatcher_getInternalManifoldPool(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDispatcher_getManifoldByIndexInternal(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDispatcher_getNewManifold(IntPtr obj, IntPtr b0, IntPtr b1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btDispatcher_getNumManifolds(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btDispatcher_needsCollision(IntPtr obj, IntPtr body0, IntPtr body1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btDispatcher_needsResponse(IntPtr obj, IntPtr body0, IntPtr body1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDispatcher_releaseManifold(IntPtr obj, IntPtr manifold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDispatcher_delete(IntPtr obj);
	}
}
