using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class GhostObject : CollisionObject
	{
        AlignedCollisionObjectArray _overlappingPairs;

		internal GhostObject(IntPtr native)
			: base(native)
		{
		}

		public GhostObject()
			: base(btGhostObject_new())
		{
		}

		public void AddOverlappingObjectInternal(BroadphaseProxy otherProxy)
		{
			btGhostObject_addOverlappingObjectInternal(_native, otherProxy._native);
		}

		public void AddOverlappingObjectInternal(BroadphaseProxy otherProxy, BroadphaseProxy thisProxy)
		{
			btGhostObject_addOverlappingObjectInternal2(_native, otherProxy._native, thisProxy._native);
		}

        public void ConvexSweepTestRef(ConvexShape castShape, ref Matrix convexFromWorld, ref Matrix convexToWorld, ConvexResultCallback resultCallback)
        {
            btGhostObject_convexSweepTest(_native, castShape._native, ref convexFromWorld, ref convexToWorld, resultCallback._native);
        }

		public void ConvexSweepTest(ConvexShape castShape, Matrix convexFromWorld, Matrix convexToWorld, ConvexResultCallback resultCallback)
		{
			btGhostObject_convexSweepTest(_native, castShape._native, ref convexFromWorld, ref convexToWorld, resultCallback._native);
		}

        public void ConvexSweepTestRef(ConvexShape castShape, ref Matrix convexFromWorld, ref Matrix convexToWorld, ConvexResultCallback resultCallback, float allowedCcdPenetration)
        {
            btGhostObject_convexSweepTest2(_native, castShape._native, ref convexFromWorld, ref convexToWorld, resultCallback._native, allowedCcdPenetration);
        }

		public void ConvexSweepTest(ConvexShape castShape, Matrix convexFromWorld, Matrix convexToWorld, ConvexResultCallback resultCallback, float allowedCcdPenetration)
		{
			btGhostObject_convexSweepTest2(_native, castShape._native, ref convexFromWorld, ref convexToWorld, resultCallback._native, allowedCcdPenetration);
		}

		public CollisionObject GetOverlappingObject(int index)
		{
			return CollisionObject.GetManaged(btGhostObject_getOverlappingObject(_native, index));
		}

        public void RayTestRef(ref Vector3 rayFromWorld, ref Vector3 rayToWorld, RayResultCallback resultCallback)
        {
            btGhostObject_rayTest(_native, ref rayFromWorld, ref rayToWorld, resultCallback._native);
        }

		public void RayTest(Vector3 rayFromWorld, Vector3 rayToWorld, RayResultCallback resultCallback)
		{
			btGhostObject_rayTest(_native, ref rayFromWorld, ref rayToWorld, resultCallback._native);
		}

		public void RemoveOverlappingObjectInternal(BroadphaseProxy otherProxy, Dispatcher dispatcher)
		{
			btGhostObject_removeOverlappingObjectInternal(_native, otherProxy._native, dispatcher._native);
		}

		public void RemoveOverlappingObjectInternal(BroadphaseProxy otherProxy, Dispatcher dispatcher, BroadphaseProxy thisProxy)
		{
			btGhostObject_removeOverlappingObjectInternal2(_native, otherProxy._native, dispatcher._native, thisProxy._native);
		}

		public static GhostObject Upcast(CollisionObject colObj)
		{
			return GetManaged(btGhostObject_upcast(colObj._native)) as GhostObject;
		}

		public int NumOverlappingObjects
		{
			get { return btGhostObject_getNumOverlappingObjects(_native); }
		}

		public AlignedCollisionObjectArray OverlappingPairs
		{
            get
            {
                if (_overlappingPairs == null)
                {
                    _overlappingPairs = new AlignedCollisionObjectArray(btGhostObject_getOverlappingPairs(_native));
                }
                return _overlappingPairs;
            }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGhostObject_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGhostObject_addOverlappingObjectInternal(IntPtr obj, IntPtr otherProxy);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGhostObject_addOverlappingObjectInternal2(IntPtr obj, IntPtr otherProxy, IntPtr thisProxy);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGhostObject_convexSweepTest(IntPtr obj, IntPtr castShape, [In] ref Matrix convexFromWorld, [In] ref Matrix convexToWorld, IntPtr resultCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGhostObject_convexSweepTest2(IntPtr obj, IntPtr castShape, [In] ref Matrix convexFromWorld, [In] ref Matrix convexToWorld, IntPtr resultCallback, float allowedCcdPenetration);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGhostObject_getNumOverlappingObjects(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGhostObject_getOverlappingObject(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGhostObject_getOverlappingPairs(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGhostObject_rayTest(IntPtr obj, [In] ref Vector3 rayFromWorld, [In] ref Vector3 rayToWorld, IntPtr resultCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGhostObject_removeOverlappingObjectInternal(IntPtr obj, IntPtr otherProxy, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGhostObject_removeOverlappingObjectInternal2(IntPtr obj, IntPtr otherProxy, IntPtr dispatcher, IntPtr thisProxy);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGhostObject_upcast(IntPtr colObj);
	}

	public class PairCachingGhostObject : GhostObject
	{
        HashedOverlappingPairCache _overlappingPairCache;

		public PairCachingGhostObject()
			: base(btPairCachingGhostObject_new())
		{
		}

		public HashedOverlappingPairCache OverlappingPairCache
		{
            get
            {
                if (_overlappingPairCache == null)
                {
                    _overlappingPairCache = new HashedOverlappingPairCache(btPairCachingGhostObject_getOverlappingPairCache(_native), true);
                }
                return _overlappingPairCache;
            }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btPairCachingGhostObject_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btPairCachingGhostObject_getOverlappingPairCache(IntPtr obj);
	}

	public class GhostPairCallback : OverlappingPairCallback
	{
		internal GhostPairCallback(IntPtr native)
			: base(native)
		{
		}

		public GhostPairCallback()
			: base(btGhostPairCallback_new())
		{
		}

        public override BroadphasePair AddOverlappingPair(BroadphaseProxy proxy0, BroadphaseProxy proxy1)
        {
            return new BroadphasePair(btOverlappingPairCallback_addOverlappingPair(_native, proxy0._native, proxy1._native));
        }

        public override IntPtr RemoveOverlappingPair(BroadphaseProxy proxy0, BroadphaseProxy proxy1, Dispatcher dispatcher)
        {
            return btOverlappingPairCallback_removeOverlappingPair(_native, proxy0._native, proxy1._native, dispatcher._native);
        }

        public override void RemoveOverlappingPairsContainingProxy(BroadphaseProxy proxy0, Dispatcher dispatcher)
        {
            btOverlappingPairCallback_removeOverlappingPairsContainingProxy(_native, proxy0._native, dispatcher._native);
        }

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGhostPairCallback_new();
	}
}
