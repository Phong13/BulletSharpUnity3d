using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class SimpleBroadphaseProxy : BroadphaseProxy
	{
		internal SimpleBroadphaseProxy(IntPtr native)
			: base(native)
		{
		}
		/*
		public int NextFree
		{
			get { return btSimpleBroadphaseProxy_GetNextFree(_native); }
			set { btSimpleBroadphaseProxy_SetNextFree(_native, value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btSimpleBroadphaseProxy_GetNextFree(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSimpleBroadphaseProxy_SetNextFree(IntPtr obj, int next);
		*/
	}
	/*
	public class SimpleBroadphase : BroadphaseInterface
	{
		public SimpleBroadphase()
			: base(btSimpleBroadphase_new())
		{
            _overlappingPairCache = new HashedOverlappingPairCache(btBroadphaseInterface_getOverlappingPairCache(_native), true);
		}

		public SimpleBroadphase(int maxProxies)
			: base(btSimpleBroadphase_new2(maxProxies))
		{
            _overlappingPairCache = new HashedOverlappingPairCache(btBroadphaseInterface_getOverlappingPairCache(_native), true);
		}

		public SimpleBroadphase(int maxProxies, OverlappingPairCache overlappingPairCache)
			: base(btSimpleBroadphase_new3(maxProxies, overlappingPairCache._native))
		{
            _overlappingPairCache = (overlappingPairCache != null) ? overlappingPairCache : new HashedOverlappingPairCache(
                btBroadphaseInterface_getOverlappingPairCache(_native), true);
		}

		
		public static bool AabbOverlap(SimpleBroadphaseProxy proxy0, SimpleBroadphaseProxy proxy1)
		{
			return btSimpleBroadphase_aabbOverlap(proxy0._native, proxy1._native);
		}

        public override BroadphaseProxy CreateProxy(ref Vector3 aabbMin, ref Vector3 aabbMax, int shapeType, IntPtr userPtr, short collisionFilterGroup, short collisionFilterMask, Dispatcher dispatcher, IntPtr multiSapProxy)
        {
            return new SimpleBroadphaseProxy(btBroadphaseInterface_createProxy(_native, ref aabbMin, ref aabbMax, shapeType, userPtr, collisionFilterGroup, collisionFilterMask, dispatcher._native, multiSapProxy));
        }

		public bool TestAabbOverlap(BroadphaseProxy proxy0, BroadphaseProxy proxy1)
		{
			return btSimpleBroadphase_testAabbOverlap(_native, proxy0._native, proxy1._native);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSimpleBroadphase_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSimpleBroadphase_new2(int maxProxies);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSimpleBroadphase_new3(int maxProxies, IntPtr overlappingPairCache);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//[return: MarshalAs(UnmanagedType.I1)]
		//static extern bool btSimpleBroadphase_aabbOverlap(IntPtr proxy0, IntPtr proxy1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btSimpleBroadphase_testAabbOverlap(IntPtr obj, IntPtr proxy0, IntPtr proxy1);
	}
	*/
}
