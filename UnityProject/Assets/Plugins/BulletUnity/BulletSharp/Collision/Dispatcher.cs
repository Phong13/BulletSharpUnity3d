using System;
using System.Collections.Generic;

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
			get
            {
                return UnsafeNativeMethods.btDispatcherInfo_getAllowedCcdPenetration(Native);
            }
			set
            {
                UnsafeNativeMethods.btDispatcherInfo_setAllowedCcdPenetration(Native, value);
            }
		}

		public float ConvexConservativeDistanceThreshold
		{
			get { return UnsafeNativeMethods.btDispatcherInfo_getConvexConservativeDistanceThreshold(Native); }
			set { UnsafeNativeMethods.btDispatcherInfo_setConvexConservativeDistanceThreshold(Native, value); }
        }

		public IDebugDraw DebugDraw
		{
			get
            { return BulletSharp.DebugDraw.GetManaged(UnsafeNativeMethods.btDispatcherInfo_getDebugDraw(Native));
            }
            set { UnsafeNativeMethods.btDispatcherInfo_setDebugDraw(Native, BulletSharp.DebugDraw.GetUnmanaged(value)); }
            }

		public DispatchFunc DispatchFunc
		{
			get
            { return UnsafeNativeMethods.btDispatcherInfo_getDispatchFunc(Native);
            }
            set { UnsafeNativeMethods.btDispatcherInfo_setDispatchFunc(Native, value); }
        }

		public bool EnableSatConvex
		{
			get
            { return UnsafeNativeMethods.btDispatcherInfo_getEnableSatConvex(Native);
            }
            set { UnsafeNativeMethods.btDispatcherInfo_setEnableSatConvex(Native, value); }
        }

		public bool EnableSpu
		{
			get { return UnsafeNativeMethods.btDispatcherInfo_getEnableSPU(Native); }
            set { UnsafeNativeMethods.btDispatcherInfo_setEnableSPU(Native, value); }
        }

		public int StepCount
		{
			get { return UnsafeNativeMethods.btDispatcherInfo_getStepCount(Native); }
            set { UnsafeNativeMethods.btDispatcherInfo_setStepCount(Native, value); }
        }

		public float TimeOfImpact
		{
			get { return UnsafeNativeMethods.btDispatcherInfo_getTimeOfImpact(Native); }
            set { UnsafeNativeMethods.btDispatcherInfo_setTimeOfImpact(Native, value); }
        }

		public float TimeStep
		{
			get { return UnsafeNativeMethods.btDispatcherInfo_getTimeStep(Native); }
            set { UnsafeNativeMethods.btDispatcherInfo_setTimeStep(Native, value); }
        }

		public bool UseContinuous
		{
			get { return UnsafeNativeMethods.btDispatcherInfo_getUseContinuous(Native); }
            set { UnsafeNativeMethods.btDispatcherInfo_setUseContinuous(Native, value); }
        }

		public bool UseConvexConservativeDistanceUtil
		{
			get { return UnsafeNativeMethods.btDispatcherInfo_getUseConvexConservativeDistanceUtil(Native); }
            set { UnsafeNativeMethods.btDispatcherInfo_setUseConvexConservativeDistanceUtil(Native, value); }
        }

		public bool UseEpa
		{
			get { return UnsafeNativeMethods.btDispatcherInfo_getUseEpa(Native); }
            set { UnsafeNativeMethods.btDispatcherInfo_setUseEpa(Native, value); }
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
			return UnsafeNativeMethods.btDispatcher_allocateCollisionAlgorithm(Native, size);
		}

		public void ClearManifold(PersistentManifold manifold)
		{
            UnsafeNativeMethods.btDispatcher_clearManifold(Native, manifold.Native);
		}

		public void DispatchAllCollisionPairs(OverlappingPairCache pairCache, DispatcherInfo dispatchInfo,
			Dispatcher dispatcher)
		{
            UnsafeNativeMethods.btDispatcher_dispatchAllCollisionPairs(Native, pairCache.Native, dispatchInfo.Native,
				dispatcher.Native);
		}

		public CollisionAlgorithm FindAlgorithm(CollisionObjectWrapper body0Wrap,
			CollisionObjectWrapper body1Wrap, PersistentManifold sharedManifold,
			DispatcherQueryType queryType)
		{
			return new CollisionAlgorithm(UnsafeNativeMethods.btDispatcher_findAlgorithm(Native, body0Wrap.Native, body1Wrap.Native, sharedManifold.Native, queryType));
		}

		public void FreeCollisionAlgorithm(IntPtr ptr)
		{
            UnsafeNativeMethods.btDispatcher_freeCollisionAlgorithm(Native, ptr);
		}

		public PersistentManifold GetManifoldByIndexInternal(int index)
		{
			return new PersistentManifold(UnsafeNativeMethods.btDispatcher_getManifoldByIndexInternal(Native, index), true);
		}

		public PersistentManifold GetNewManifold(CollisionObject b0, CollisionObject b1)
		{
			return new PersistentManifold(UnsafeNativeMethods.btDispatcher_getNewManifold(Native, b0.Native, b1.Native), true);
		}

		public bool NeedsCollision(CollisionObject body0, CollisionObject body1)
		{
			return UnsafeNativeMethods.btDispatcher_needsCollision(Native, body0.Native, body1.Native);
		}

		public bool NeedsResponse(CollisionObject body0, CollisionObject body1)
		{
			return UnsafeNativeMethods.btDispatcher_needsResponse(Native, body0.Native, body1.Native);
		}

		public void ReleaseManifold(PersistentManifold manifold)
		{
            UnsafeNativeMethods.btDispatcher_releaseManifold(Native, manifold.Native);
		}
        /*
		public PersistentManifold InternalManifoldPointer
		{
			get { return UnsafeNativeMethods.btDispatcher_getInternalManifoldPointer(Native); }
		}

		public PoolAllocator InternalManifoldPool
		{
			get { return UnsafeNativeMethods.btDispatcher_getInternalManifoldPool(Native); }
		}
		*/
        public int NumManifolds()
        {
            return UnsafeNativeMethods.btDispatcher_getNumManifolds(Native);
        }

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
                    UnsafeNativeMethods.btDispatcher_delete(Native);
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
