using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{/*
	public class ConvexTriangleCallback : TriangleCallback
	{
		internal ConvexTriangleCallback(IntPtr native)
			: base(native)
		{
		}

		public ConvexTriangleCallback(Dispatcher dispatcher, CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap, bool isSwapped)
			: base(btConvexTriangleCallback_new(dispatcher._native, body0Wrap._native, body1Wrap._native, isSwapped))
		{
		}

		public void ClearCache()
		{
			btConvexTriangleCallback_clearCache(_native);
		}

		public void ClearWrapperData()
		{
			btConvexTriangleCallback_clearWrapperData(_native);
		}

		public void SetTimeStepAndCounters(float collisionMarginTriangle, DispatcherInfo dispatchInfo, CollisionObjectWrapper convexBodyWrap, CollisionObjectWrapper triBodyWrap, ManifoldResult resultOut)
		{
			btConvexTriangleCallback_setTimeStepAndCounters(_native, collisionMarginTriangle, dispatchInfo._native, convexBodyWrap._native, triBodyWrap._native, resultOut._native);
		}

		public Vector3 AabbMax
		{
			get
			{
				Vector3 value;
				btConvexTriangleCallback_getAabbMax(_native, out value);
				return value;
			}
		}

		public Vector3 AabbMin
		{
			get
			{
				Vector3 value;
				btConvexTriangleCallback_getAabbMin(_native, out value);
				return value;
			}
		}

		public PersistentManifold ManifoldPtr
		{
			get { return btConvexTriangleCallback_getManifoldPtr(_native); }
			set { btConvexTriangleCallback_setManifoldPtr(_native, value._native); }
		}

		public int TriangleCount
		{
			get { return btConvexTriangleCallback_getTriangleCount(_native); }
			set { btConvexTriangleCallback_setTriangleCount(_native, value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btConvexTriangleCallback_new(IntPtr dispatcher, IntPtr body0Wrap, IntPtr body1Wrap, bool isSwapped);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexTriangleCallback_clearCache(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexTriangleCallback_clearWrapperData(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexTriangleCallback_getAabbMax(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexTriangleCallback_getAabbMin(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btConvexTriangleCallback_getManifoldPtr(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btConvexTriangleCallback_getTriangleCount(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexTriangleCallback_setManifoldPtr(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexTriangleCallback_setTimeStepAndCounters(IntPtr obj, float collisionMarginTriangle, IntPtr dispatchInfo, IntPtr convexBodyWrap, IntPtr triBodyWrap, IntPtr resultOut);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexTriangleCallback_setTriangleCount(IntPtr obj, int value);
	}
*/
	public class ConvexConcaveCollisionAlgorithm : ActivatingCollisionAlgorithm
	{
		public class CreateFunc : CollisionAlgorithmCreateFunc
		{
			internal CreateFunc(IntPtr native)
				: base(native, true)
			{
			}

			public CreateFunc()
				: base(btConvexConcaveCollisionAlgorithm_CreateFunc_new(), false)
			{
			}

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btConvexConcaveCollisionAlgorithm_CreateFunc_new();
		}

		public class SwappedCreateFunc : CollisionAlgorithmCreateFunc
		{
			internal SwappedCreateFunc(IntPtr native)
				: base(native, true)
			{
			}

			public SwappedCreateFunc()
				: base(btConvexConcaveCollisionAlgorithm_SwappedCreateFunc_new(), false)
			{
			}

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btConvexConcaveCollisionAlgorithm_SwappedCreateFunc_new();
		}

		public ConvexConcaveCollisionAlgorithm(CollisionAlgorithmConstructionInfo ci, CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap, bool isSwapped)
			: base(btConvexConcaveCollisionAlgorithm_new(ci._native, body0Wrap._native, body1Wrap._native, isSwapped))
		{
		}

		public void ClearCache()
		{
			btConvexConcaveCollisionAlgorithm_clearCache(_native);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btConvexConcaveCollisionAlgorithm_new(IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap, bool isSwapped);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexConcaveCollisionAlgorithm_clearCache(IntPtr obj);
	}
}
