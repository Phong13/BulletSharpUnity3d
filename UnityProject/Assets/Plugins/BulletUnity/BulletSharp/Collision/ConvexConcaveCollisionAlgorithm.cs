using System;


namespace BulletSharp
{/*
	public class ConvexTriangleCallback : TriangleCallback
	{
		internal ConvexTriangleCallback(IntPtr native)
			: base(native)
		{
		}

		public ConvexTriangleCallback(Dispatcher dispatcher, CollisionObjectWrapper body0Wrap,
			CollisionObjectWrapper body1Wrap, bool isSwapped)
			: base(btConvexTriangleCallback_new(dispatcher.Native, body0Wrap.Native,
				body1Wrap.Native, isSwapped))
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

		public void SetTimeStepAndCounters(float collisionMarginTriangle, DispatcherInfo dispatchInfo,
			CollisionObjectWrapper convexBodyWrap, CollisionObjectWrapper triBodyWrap,
			ManifoldResult resultOut)
		{
			btConvexTriangleCallback_setTimeStepAndCounters(_native, collisionMarginTriangle,
				dispatchInfo._native, convexBodyWrap.Native, triBodyWrap.Native,
				resultOut._native);
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
			get { return  btConvexTriangleCallback_getManifoldPtr(_native);}
			set {  btConvexTriangleCallback_setManifoldPtr(_native, value._native);}
		}

		public int TriangleCount
		{
			get { return  btConvexTriangleCallback_getTriangleCount(_native);}
			set {  btConvexTriangleCallback_setTriangleCount(_native, value);}
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btConvexTriangleCallback_new(IntPtr dispatcher, IntPtr body0Wrap, IntPtr body1Wrap, bool isSwapped);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexTriangleCallback_clearCache(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexTriangleCallback_clearWrapperData(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexTriangleCallback_getAabbMax(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexTriangleCallback_getAabbMin(IntPtr obj, out Vector3 value);
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
				: base(UnsafeNativeMethods.btConvexConcaveCollisionAlgorithm_CreateFunc_new(), false)
			{
			}

			public override CollisionAlgorithm CreateCollisionAlgorithm(CollisionAlgorithmConstructionInfo __unnamed0,
				CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap)
			{
				return new ConvexConcaveCollisionAlgorithm(UnsafeNativeMethods.btCollisionAlgorithmCreateFunc_CreateCollisionAlgorithm(
					Native, __unnamed0.Native, body0Wrap.Native, body1Wrap.Native));
			}
		}

		public class SwappedCreateFunc : CollisionAlgorithmCreateFunc
		{
			internal SwappedCreateFunc(IntPtr native)
				: base(native, true)
			{
			}

			public SwappedCreateFunc()
				: base(UnsafeNativeMethods.btConvexConcaveCollisionAlgorithm_SwappedCreateFunc_new(), false)
			{
			}

			public override CollisionAlgorithm CreateCollisionAlgorithm(CollisionAlgorithmConstructionInfo __unnamed0,
				CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap)
			{
				return new ConvexConcaveCollisionAlgorithm(UnsafeNativeMethods.btCollisionAlgorithmCreateFunc_CreateCollisionAlgorithm(
					Native, __unnamed0.Native, body0Wrap.Native, body1Wrap.Native));
			}
		}

		internal ConvexConcaveCollisionAlgorithm(IntPtr native)
			: base(native)
		{
		}

		public ConvexConcaveCollisionAlgorithm(CollisionAlgorithmConstructionInfo ci,
			CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap, bool isSwapped)
			: base(UnsafeNativeMethods.btConvexConcaveCollisionAlgorithm_new(ci.Native, body0Wrap.Native,
				body1Wrap.Native, isSwapped))
		{
		}

		public void ClearCache()
		{
			UnsafeNativeMethods.btConvexConcaveCollisionAlgorithm_clearCache(Native);
		}
	}
}
