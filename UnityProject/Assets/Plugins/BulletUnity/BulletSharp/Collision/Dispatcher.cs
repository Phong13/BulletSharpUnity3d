using System;
using System.Collections.Generic;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public enum DispatchFunc
	{
		Discrete = 1,
		Continuous
	}

	public enum DispatcherQueryType
	{
		ContactPointAlgorithms = 1,
		ClosestPointAlgorithms = 2
	}

	public class DispatcherInfo
	{
		internal IntPtr Native;

		internal DispatcherInfo(IntPtr native)
		{
			Native = native;
		}

		public float AllowedCcdPenetration
		{
			get => btDispatcherInfo_getAllowedCcdPenetration(Native);
			set => btDispatcherInfo_setAllowedCcdPenetration(Native, value);
		}

		public float ConvexConservativeDistanceThreshold
		{
			get => btDispatcherInfo_getConvexConservativeDistanceThreshold(Native);
			set => btDispatcherInfo_setConvexConservativeDistanceThreshold(Native, value);
		}

		public IDebugDraw DebugDraw
		{
			get => BulletSharp.DebugDraw.GetManaged(btDispatcherInfo_getDebugDraw(Native));
			set => btDispatcherInfo_setDebugDraw(Native, BulletSharp.DebugDraw.GetUnmanaged(value));
		}

		public DispatchFunc DispatchFunc
		{
			get => btDispatcherInfo_getDispatchFunc(Native);
			set => btDispatcherInfo_setDispatchFunc(Native, value);
		}

		public bool EnableSatConvex
		{
			get => btDispatcherInfo_getEnableSatConvex(Native);
			set => btDispatcherInfo_setEnableSatConvex(Native, value);
		}

		public bool EnableSpu
		{
			get => btDispatcherInfo_getEnableSPU(Native);
			set => btDispatcherInfo_setEnableSPU(Native, value);
		}

		public int StepCount
		{
			get => btDispatcherInfo_getStepCount(Native);
			set => btDispatcherInfo_setStepCount(Native, value);
		}

		public float TimeOfImpact
		{
			get => btDispatcherInfo_getTimeOfImpact(Native);
			set => btDispatcherInfo_setTimeOfImpact(Native, value);
		}

		public float TimeStep
		{
			get => btDispatcherInfo_getTimeStep(Native);
			set => btDispatcherInfo_setTimeStep(Native, value);
		}

		public bool UseContinuous
		{
			get => btDispatcherInfo_getUseContinuous(Native);
			set => btDispatcherInfo_setUseContinuous(Native, value);
		}

		public bool UseConvexConservativeDistanceUtil
		{
			get => btDispatcherInfo_getUseConvexConservativeDistanceUtil(Native);
			set => btDispatcherInfo_setUseConvexConservativeDistanceUtil(Native, value);
		}

		public bool UseEpa
		{
			get => btDispatcherInfo_getUseEpa(Native);
			set => btDispatcherInfo_setUseEpa(Native, value);
		}
	}

	public abstract class Dispatcher : IDisposable
	{
		internal IntPtr Native;

		internal List<CollisionWorld> _worldRefs = new List<CollisionWorld>(1);
		internal bool _worldDeferredCleanup;

		internal Dispatcher(IntPtr native)
		{
			Native = native;
		}

		public IntPtr AllocateCollisionAlgorithm(int size)
		{
			return btDispatcher_allocateCollisionAlgorithm(Native, size);
		}

		public void ClearManifold(PersistentManifold manifold)
		{
			btDispatcher_clearManifold(Native, manifold.Native);
		}

		public void DispatchAllCollisionPairs(OverlappingPairCache pairCache, DispatcherInfo dispatchInfo,
			Dispatcher dispatcher)
		{
			btDispatcher_dispatchAllCollisionPairs(Native, pairCache.Native, dispatchInfo.Native,
				dispatcher.Native);
		}

		public CollisionAlgorithm FindAlgorithm(CollisionObjectWrapper body0Wrap,
			CollisionObjectWrapper body1Wrap, PersistentManifold sharedManifold,
			DispatcherQueryType queryType)
		{
			return new CollisionAlgorithm(btDispatcher_findAlgorithm(Native, body0Wrap.Native, body1Wrap.Native, sharedManifold.Native, queryType));
		}

		public void FreeCollisionAlgorithm(IntPtr ptr)
		{
			btDispatcher_freeCollisionAlgorithm(Native, ptr);
		}

		public PersistentManifold GetManifoldByIndexInternal(int index)
		{
			return new PersistentManifold(btDispatcher_getManifoldByIndexInternal(Native, index), true);
		}

		public PersistentManifold GetNewManifold(CollisionObject b0, CollisionObject b1)
		{
			return new PersistentManifold(btDispatcher_getNewManifold(Native, b0.Native, b1.Native), true);
		}

		public bool NeedsCollision(CollisionObject body0, CollisionObject body1)
		{
			return btDispatcher_needsCollision(Native, body0.Native, body1.Native);
		}

		public bool NeedsResponse(CollisionObject body0, CollisionObject body1)
		{
			return btDispatcher_needsResponse(Native, body0.Native, body1.Native);
		}

		public void ReleaseManifold(PersistentManifold manifold)
		{
			btDispatcher_releaseManifold(Native, manifold.Native);
		}
		/*
		public PersistentManifold InternalManifoldPointer
		{
			get { return btDispatcher_getInternalManifoldPointer(Native); }
		}

		public PoolAllocator InternalManifoldPool
		{
			get { return btDispatcher_getInternalManifoldPool(Native); }
		}
		*/
		public int NumManifolds => btDispatcher_getNumManifolds(Native);

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				if (_worldRefs.Count == 0)
				{
					btDispatcher_delete(Native);
					Native = IntPtr.Zero;
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
	}
}
