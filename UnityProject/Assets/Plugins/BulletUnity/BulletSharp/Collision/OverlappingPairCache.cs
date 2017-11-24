using System;
using System.Runtime.InteropServices;
using System.Security;


namespace BulletSharp
{
	public abstract class OverlapCallback : IDisposable
	{
		internal IntPtr Native;

		public bool ProcessOverlap(BroadphasePair pair)
		{
			return UnsafeNativeMethods.btOverlapCallback_processOverlap(Native, pair.Native);
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
				UnsafeNativeMethods.btOverlapCallback_delete(Native);
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
			Native = UnsafeNativeMethods.btOverlapFilterCallbackWrapper_new(Marshal.GetFunctionPointerForDelegate(_needBroadphaseCollision));
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
				UnsafeNativeMethods.btOverlapFilterCallback_delete(Native);
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
			UnsafeNativeMethods.btOverlappingPairCache_cleanOverlappingPair(Native, pair.Native, dispatcher.Native);
		}

		public void CleanProxyFromPairs(BroadphaseProxy proxy, Dispatcher dispatcher)
		{
			UnsafeNativeMethods.btOverlappingPairCache_cleanProxyFromPairs(Native, proxy.Native, dispatcher.Native);
		}

		public BroadphasePair FindPair(BroadphaseProxy proxy0, BroadphaseProxy proxy1)
		{
			return new BroadphasePair(UnsafeNativeMethods.btOverlappingPairCache_findPair(Native, proxy0.Native, proxy1.Native));
		}
		/*
		public void ProcessAllOverlappingPairs(OverlapCallback __unnamed0, Dispatcher dispatcher)
		{
			UnsafeNativeMethods.btOverlappingPairCache_processAllOverlappingPairs(Native, __unnamed0.Native,
				dispatcher.Native);
		}
		*/
		public void SetInternalGhostPairCallback(OverlappingPairCallback ghostPairCallback)
		{
			_ghostPairCallback = ghostPairCallback;
			UnsafeNativeMethods.btOverlappingPairCache_setInternalGhostPairCallback(Native, ghostPairCallback.Native);
		}

		public void SetOverlapFilterCallback(OverlapFilterCallback callback)
		{
			UnsafeNativeMethods.btOverlappingPairCache_setOverlapFilterCallback(Native, callback.Native);
		}

		public void SortOverlappingPairs(Dispatcher dispatcher)
		{
			UnsafeNativeMethods.btOverlappingPairCache_sortOverlappingPairs(Native, dispatcher.Native);
		}

		public bool HasDeferredRemoval{ get { return  UnsafeNativeMethods.btOverlappingPairCache_hasDeferredRemoval(Native);} }

		public int NumOverlappingPairs{ get { return  UnsafeNativeMethods.btOverlappingPairCache_getNumOverlappingPairs(Native);} }

		public AlignedBroadphasePairArray OverlappingPairArray
		{
			get
			{
				IntPtr pairArrayPtr = UnsafeNativeMethods.btOverlappingPairCache_getOverlappingPairArray(Native);
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
			: base(UnsafeNativeMethods.btHashedOverlappingPairCache_new(), false)
		{
		}

		public override BroadphasePair AddOverlappingPair(BroadphaseProxy proxy0, BroadphaseProxy proxy1)
		{
			return new BroadphasePair(UnsafeNativeMethods.btOverlappingPairCallback_addOverlappingPair(Native, proxy0.Native,
				proxy1.Native));
		}

		public bool NeedsBroadphaseCollision(BroadphaseProxy proxy0, BroadphaseProxy proxy1)
		{
			return UnsafeNativeMethods.btHashedOverlappingPairCache_needsBroadphaseCollision(Native,
				proxy0.Native, proxy1.Native);
		}

		public override IntPtr RemoveOverlappingPair(BroadphaseProxy proxy0, BroadphaseProxy proxy1,
			Dispatcher dispatcher)
		{
			return UnsafeNativeMethods.btOverlappingPairCallback_removeOverlappingPair(Native, proxy0.Native,
				proxy1.Native, dispatcher.Native);
		}

		public override void RemoveOverlappingPairsContainingProxy(BroadphaseProxy proxy0,
			Dispatcher dispatcher)
		{
			UnsafeNativeMethods.btOverlappingPairCallback_removeOverlappingPairsContainingProxy(Native,
				proxy0.Native, dispatcher.Native);
		}

		public int Count{ get { return  UnsafeNativeMethods.btHashedOverlappingPairCache_GetCount(Native);} }

		public OverlapFilterCallback OverlapFilterCallback
		{
			get { return  _overlapFilterCallback;}
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
			: base(UnsafeNativeMethods.btSortedOverlappingPairCache_new(), false)
		{
		}

		public override BroadphasePair AddOverlappingPair(BroadphaseProxy proxy0, BroadphaseProxy proxy1)
		{
			return new BroadphasePair(UnsafeNativeMethods.btOverlappingPairCallback_addOverlappingPair(Native, proxy0.Native,
				proxy1.Native));
		}

		public bool NeedsBroadphaseCollision(BroadphaseProxy proxy0, BroadphaseProxy proxy1)
		{
			return UnsafeNativeMethods.btSortedOverlappingPairCache_needsBroadphaseCollision(Native,
				proxy0.Native, proxy1.Native);
		}

		public override IntPtr RemoveOverlappingPair(BroadphaseProxy proxy0, BroadphaseProxy proxy1, 
			Dispatcher dispatcher)
		{
			return UnsafeNativeMethods.btOverlappingPairCallback_removeOverlappingPair(Native, proxy0.Native,
				proxy1.Native, dispatcher.Native);
		}

		public override void RemoveOverlappingPairsContainingProxy(BroadphaseProxy proxy0,
			Dispatcher dispatcher)
		{
			UnsafeNativeMethods.btOverlappingPairCallback_removeOverlappingPairsContainingProxy(Native,
				proxy0.Native, dispatcher.Native);
		}

		public OverlapFilterCallback OverlapFilterCallback
		{
			get { return  _overlapFilterCallback;}
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
			: base(UnsafeNativeMethods.btNullPairCache_new(), false)
		{
		}

		public override BroadphasePair AddOverlappingPair(BroadphaseProxy proxy0, BroadphaseProxy proxy1)
		{
			return new BroadphasePair(UnsafeNativeMethods.btOverlappingPairCallback_addOverlappingPair(Native, proxy0.Native,
				proxy1.Native));
		}

		public override IntPtr RemoveOverlappingPair(BroadphaseProxy proxy0, BroadphaseProxy proxy1,
			Dispatcher dispatcher)
		{
			return UnsafeNativeMethods.btOverlappingPairCallback_removeOverlappingPair(Native, proxy0.Native,
				proxy1.Native, dispatcher.Native);
		}

		public override void RemoveOverlappingPairsContainingProxy(BroadphaseProxy proxy0,
			Dispatcher dispatcher)
		{
			UnsafeNativeMethods.btOverlappingPairCallback_removeOverlappingPairsContainingProxy(Native, proxy0.Native,
				dispatcher.Native);
		}
	}
}
