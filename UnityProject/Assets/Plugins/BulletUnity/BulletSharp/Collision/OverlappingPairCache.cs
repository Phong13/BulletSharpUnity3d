using System;
using System.Runtime.InteropServices;
using System.Security;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public abstract class OverlapCallback : IDisposable
	{
		internal IntPtr Native;

		public bool ProcessOverlap(BroadphasePair pair)
		{
			return btOverlapCallback_processOverlap(Native, pair.Native);
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
				btOverlapCallback_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~OverlapCallback()
		{
			Dispose(false);
		}
	}

	public abstract class OverlapFilterCallback : IDisposable
	{
		[UnmanagedFunctionPointer(BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
		private delegate bool NeedBroadphaseCollisionUnmanagedDelegate(IntPtr proxy0, IntPtr proxy1);

		internal IntPtr Native;
		private NeedBroadphaseCollisionUnmanagedDelegate _needBroadphaseCollision;

		internal OverlapFilterCallback(IntPtr native)
		{
			Native = native;
		}

		public OverlapFilterCallback()
		{
			_needBroadphaseCollision = NeedBroadphaseCollisionUnmanaged;
			Native = btOverlapFilterCallbackWrapper_new(Marshal.GetFunctionPointerForDelegate(_needBroadphaseCollision));
		}

		private bool NeedBroadphaseCollisionUnmanaged(IntPtr proxy0, IntPtr proxy1)
		{
			return NeedBroadphaseCollision(BroadphaseProxy.GetManaged(proxy0), BroadphaseProxy.GetManaged(proxy1));
		}

		public abstract bool NeedBroadphaseCollision(BroadphaseProxy proxy0, BroadphaseProxy proxy1);

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				btOverlapFilterCallback_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~OverlapFilterCallback()
		{
			Dispose(false);
		}
	}

	public abstract class OverlappingPairCache : OverlappingPairCallback
	{
		private OverlappingPairCallback _ghostPairCallback;
		private AlignedBroadphasePairArray _overlappingPairArray;

		internal OverlappingPairCache(IntPtr native, bool preventDelete)
			: base(native, preventDelete)
		{
		}

		public void CleanOverlappingPair(BroadphasePair pair, Dispatcher dispatcher)
		{
			btOverlappingPairCache_cleanOverlappingPair(Native, pair.Native, dispatcher.Native);
		}

		public void CleanProxyFromPairs(BroadphaseProxy proxy, Dispatcher dispatcher)
		{
			btOverlappingPairCache_cleanProxyFromPairs(Native, proxy.Native, dispatcher.Native);
		}

		public BroadphasePair FindPair(BroadphaseProxy proxy0, BroadphaseProxy proxy1)
		{
			return new BroadphasePair(btOverlappingPairCache_findPair(Native, proxy0.Native, proxy1.Native));
		}
		/*
		public void ProcessAllOverlappingPairs(OverlapCallback __unnamed0, Dispatcher dispatcher)
		{
			btOverlappingPairCache_processAllOverlappingPairs(Native, __unnamed0.Native,
				dispatcher.Native);
		}
		*/
		public void SetInternalGhostPairCallback(OverlappingPairCallback ghostPairCallback)
		{
			_ghostPairCallback = ghostPairCallback;
			btOverlappingPairCache_setInternalGhostPairCallback(Native, ghostPairCallback.Native);
		}

		public void SetOverlapFilterCallback(OverlapFilterCallback callback)
		{
			btOverlappingPairCache_setOverlapFilterCallback(Native, callback.Native);
		}

		public void SortOverlappingPairs(Dispatcher dispatcher)
		{
			btOverlappingPairCache_sortOverlappingPairs(Native, dispatcher.Native);
		}

		public bool HasDeferredRemoval => btOverlappingPairCache_hasDeferredRemoval(Native);

		public int NumOverlappingPairs => btOverlappingPairCache_getNumOverlappingPairs(Native);

		public AlignedBroadphasePairArray OverlappingPairArray
		{
			get
			{
				IntPtr pairArrayPtr = btOverlappingPairCache_getOverlappingPairArray(Native);
				if (_overlappingPairArray == null || _overlappingPairArray.Native != pairArrayPtr)
				{
					_overlappingPairArray = new AlignedBroadphasePairArray(pairArrayPtr);
				}
				return _overlappingPairArray;
			}
		}
	}

	public class HashedOverlappingPairCache : OverlappingPairCache
	{
		private OverlapFilterCallback _overlapFilterCallback;

		internal HashedOverlappingPairCache(IntPtr native, bool preventDelete)
			: base(native, preventDelete)
		{
		}

		public HashedOverlappingPairCache()
			: base(btHashedOverlappingPairCache_new(), false)
		{
		}

		public override BroadphasePair AddOverlappingPair(BroadphaseProxy proxy0, BroadphaseProxy proxy1)
		{
			return new BroadphasePair(btOverlappingPairCallback_addOverlappingPair(Native, proxy0.Native,
				proxy1.Native));
		}

		public bool NeedsBroadphaseCollision(BroadphaseProxy proxy0, BroadphaseProxy proxy1)
		{
			return btHashedOverlappingPairCache_needsBroadphaseCollision(Native,
				proxy0.Native, proxy1.Native);
		}

		public override IntPtr RemoveOverlappingPair(BroadphaseProxy proxy0, BroadphaseProxy proxy1,
			Dispatcher dispatcher)
		{
			return btOverlappingPairCallback_removeOverlappingPair(Native, proxy0.Native,
				proxy1.Native, dispatcher.Native);
		}

		public override void RemoveOverlappingPairsContainingProxy(BroadphaseProxy proxy0,
			Dispatcher dispatcher)
		{
			btOverlappingPairCallback_removeOverlappingPairsContainingProxy(Native,
				proxy0.Native, dispatcher.Native);
		}

		public int Count => btHashedOverlappingPairCache_GetCount(Native);

		public OverlapFilterCallback OverlapFilterCallback
		{
			get => _overlapFilterCallback;
			set
			{
				_overlapFilterCallback = value;
				SetOverlapFilterCallback(value);
			}
		}
	}

	public class SortedOverlappingPairCache : OverlappingPairCache
	{
		private OverlapFilterCallback _overlapFilterCallback;

		public SortedOverlappingPairCache()
			: base(btSortedOverlappingPairCache_new(), false)
		{
		}

		public override BroadphasePair AddOverlappingPair(BroadphaseProxy proxy0, BroadphaseProxy proxy1)
		{
			return new BroadphasePair(btOverlappingPairCallback_addOverlappingPair(Native, proxy0.Native,
				proxy1.Native));
		}

		public bool NeedsBroadphaseCollision(BroadphaseProxy proxy0, BroadphaseProxy proxy1)
		{
			return btSortedOverlappingPairCache_needsBroadphaseCollision(Native,
				proxy0.Native, proxy1.Native);
		}

		public override IntPtr RemoveOverlappingPair(BroadphaseProxy proxy0, BroadphaseProxy proxy1, 
			Dispatcher dispatcher)
		{
			return btOverlappingPairCallback_removeOverlappingPair(Native, proxy0.Native,
				proxy1.Native, dispatcher.Native);
		}

		public override void RemoveOverlappingPairsContainingProxy(BroadphaseProxy proxy0,
			Dispatcher dispatcher)
		{
			btOverlappingPairCallback_removeOverlappingPairsContainingProxy(Native,
				proxy0.Native, dispatcher.Native);
		}

		public OverlapFilterCallback OverlapFilterCallback
		{
			get => _overlapFilterCallback;
			set
			{
				_overlapFilterCallback = value;
				SetOverlapFilterCallback(value);
			}
		}
	}

	public class NullPairCache : OverlappingPairCache
	{
		public NullPairCache()
			: base(btNullPairCache_new(), false)
		{
		}

		public override BroadphasePair AddOverlappingPair(BroadphaseProxy proxy0, BroadphaseProxy proxy1)
		{
			return new BroadphasePair(btOverlappingPairCallback_addOverlappingPair(Native, proxy0.Native,
				proxy1.Native));
		}

		public override IntPtr RemoveOverlappingPair(BroadphaseProxy proxy0, BroadphaseProxy proxy1,
			Dispatcher dispatcher)
		{
			return btOverlappingPairCallback_removeOverlappingPair(Native, proxy0.Native,
				proxy1.Native, dispatcher.Native);
		}

		public override void RemoveOverlappingPairsContainingProxy(BroadphaseProxy proxy0,
			Dispatcher dispatcher)
		{
			btOverlappingPairCallback_removeOverlappingPairsContainingProxy(Native, proxy0.Native,
				dispatcher.Native);
		}
	}
}
