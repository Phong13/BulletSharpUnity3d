using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class DefaultCollisionConstructionInfo : IDisposable
	{
		internal IntPtr _native;

		public DefaultCollisionConstructionInfo()
		{
			_native = btDefaultCollisionConstructionInfo_new();
		}
        /*
		public PoolAllocator CollisionAlgorithmPool
		{
			get { return btDefaultCollisionConstructionInfo_getCollisionAlgorithmPool(_native); }
			set { btDefaultCollisionConstructionInfo_setCollisionAlgorithmPool(_native, value._native); }
		}
        */
		public int CustomCollisionAlgorithmMaxElementSize
		{
			get { return btDefaultCollisionConstructionInfo_getCustomCollisionAlgorithmMaxElementSize(_native); }
			set { btDefaultCollisionConstructionInfo_setCustomCollisionAlgorithmMaxElementSize(_native, value); }
		}

		public int DefaultMaxCollisionAlgorithmPoolSize
		{
			get { return btDefaultCollisionConstructionInfo_getDefaultMaxCollisionAlgorithmPoolSize(_native); }
			set { btDefaultCollisionConstructionInfo_setDefaultMaxCollisionAlgorithmPoolSize(_native, value); }
		}

		public int DefaultMaxPersistentManifoldPoolSize
		{
			get { return btDefaultCollisionConstructionInfo_getDefaultMaxPersistentManifoldPoolSize(_native); }
			set { btDefaultCollisionConstructionInfo_setDefaultMaxPersistentManifoldPoolSize(_native, value); }
		}
        /*
		public PoolAllocator PersistentManifoldPool
		{
			get { return btDefaultCollisionConstructionInfo_getPersistentManifoldPool(_native); }
			set { btDefaultCollisionConstructionInfo_setPersistentManifoldPool(_native, value._native); }
		}
        */
		public int UseEpaPenetrationAlgorithm
		{
			get { return btDefaultCollisionConstructionInfo_getUseEpaPenetrationAlgorithm(_native); }
			set { btDefaultCollisionConstructionInfo_setUseEpaPenetrationAlgorithm(_native, value); }
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
				btDefaultCollisionConstructionInfo_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~DefaultCollisionConstructionInfo()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDefaultCollisionConstructionInfo_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDefaultCollisionConstructionInfo_getCollisionAlgorithmPool(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btDefaultCollisionConstructionInfo_getCustomCollisionAlgorithmMaxElementSize(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btDefaultCollisionConstructionInfo_getDefaultMaxCollisionAlgorithmPoolSize(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btDefaultCollisionConstructionInfo_getDefaultMaxPersistentManifoldPoolSize(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDefaultCollisionConstructionInfo_getPersistentManifoldPool(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btDefaultCollisionConstructionInfo_getUseEpaPenetrationAlgorithm(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDefaultCollisionConstructionInfo_setCollisionAlgorithmPool(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDefaultCollisionConstructionInfo_setCustomCollisionAlgorithmMaxElementSize(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDefaultCollisionConstructionInfo_setDefaultMaxCollisionAlgorithmPoolSize(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDefaultCollisionConstructionInfo_setDefaultMaxPersistentManifoldPoolSize(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDefaultCollisionConstructionInfo_setPersistentManifoldPool(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDefaultCollisionConstructionInfo_setUseEpaPenetrationAlgorithm(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDefaultCollisionConstructionInfo_delete(IntPtr obj);
	}

	public class DefaultCollisionConfiguration : CollisionConfiguration
	{
        private VoronoiSimplexSolver _simplexSolver;

		internal DefaultCollisionConfiguration(IntPtr native)
			: base(native)
		{
		}

		public DefaultCollisionConfiguration()
			: base(btDefaultCollisionConfiguration_new())
		{
		}

		public DefaultCollisionConfiguration(DefaultCollisionConstructionInfo constructionInfo)
			: base(btDefaultCollisionConfiguration_new2(constructionInfo._native))
		{
		}

        public override CollisionAlgorithmCreateFunc GetCollisionAlgorithmCreateFunc(BroadphaseNativeType proxyType0, BroadphaseNativeType proxyType1)
        {
            IntPtr createFunc = btCollisionConfiguration_getCollisionAlgorithmCreateFunc(_native, (int)proxyType0, (int)proxyType1);
            if (proxyType0 == BroadphaseNativeType.BoxShape && proxyType1 == BroadphaseNativeType.BoxShape)
            {
                return new BoxBoxCollisionAlgorithm.CreateFunc(createFunc);
            }
            if (proxyType0 == BroadphaseNativeType.SphereShape && proxyType1 == BroadphaseNativeType.SphereShape)
            {
                return new SphereSphereCollisionAlgorithm.CreateFunc(createFunc);
            }
            if (proxyType0 == BroadphaseNativeType.SphereShape && proxyType1 == BroadphaseNativeType.TriangleShape)
            {
                return new SphereTriangleCollisionAlgorithm.CreateFunc(createFunc);
            }
            if (proxyType0 == BroadphaseNativeType.TriangleShape && proxyType1 == BroadphaseNativeType.SphereShape)
            {
                return new SphereTriangleCollisionAlgorithm.CreateFunc(createFunc);
            }
            if (proxyType0 == BroadphaseNativeType.StaticPlaneShape && BroadphaseProxy.IsConvex(proxyType1))
            {
                return new ConvexPlaneCollisionAlgorithm.CreateFunc(createFunc);
            }
            if (proxyType1 == BroadphaseNativeType.StaticPlaneShape && BroadphaseProxy.IsConvex(proxyType0))
            {
                return new ConvexPlaneCollisionAlgorithm.CreateFunc(createFunc);
            }
            if (BroadphaseProxy.IsConvex(proxyType0) && BroadphaseProxy.IsConvex(proxyType1))
            {
                return new ConvexConvexAlgorithm.CreateFunc(createFunc);
            }
            if (BroadphaseProxy.IsConvex(proxyType0) && BroadphaseProxy.IsConcave(proxyType1))
            {
                return new ConvexConcaveCollisionAlgorithm.CreateFunc(createFunc);
            }
            if (BroadphaseProxy.IsConvex(proxyType1) && BroadphaseProxy.IsConcave(proxyType0))
            {
                return new ConvexConcaveCollisionAlgorithm.SwappedCreateFunc(createFunc);
            }
            if (BroadphaseProxy.IsCompound(proxyType0))
            {
                return new CompoundCompoundCollisionAlgorithm.CreateFunc(createFunc);
            }
            if (BroadphaseProxy.IsCompound(proxyType1))
            {
                return new CompoundCompoundCollisionAlgorithm.SwappedCreateFunc(createFunc);
            }
            return new EmptyAlgorithm.CreateFunc(createFunc);
        }

		public void SetConvexConvexMultipointIterations()
		{
			btDefaultCollisionConfiguration_setConvexConvexMultipointIterations(_native);
		}

		public void SetConvexConvexMultipointIterations(int numPerturbationIterations)
		{
			btDefaultCollisionConfiguration_setConvexConvexMultipointIterations2(_native, numPerturbationIterations);
		}

		public void SetConvexConvexMultipointIterations(int numPerturbationIterations, int minimumPointsPerturbationThreshold)
		{
			btDefaultCollisionConfiguration_setConvexConvexMultipointIterations3(_native, numPerturbationIterations, minimumPointsPerturbationThreshold);
		}

		public void SetPlaneConvexMultipointIterations()
		{
			btDefaultCollisionConfiguration_setPlaneConvexMultipointIterations(_native);
		}

		public void SetPlaneConvexMultipointIterations(int numPerturbationIterations)
		{
			btDefaultCollisionConfiguration_setPlaneConvexMultipointIterations2(_native, numPerturbationIterations);
		}

		public void SetPlaneConvexMultipointIterations(int numPerturbationIterations, int minimumPointsPerturbationThreshold)
		{
			btDefaultCollisionConfiguration_setPlaneConvexMultipointIterations3(_native, numPerturbationIterations, minimumPointsPerturbationThreshold);
		}

		public VoronoiSimplexSolver SimplexSolver
		{
            get
            {
                if (_simplexSolver == null)
                {
                    _simplexSolver = new VoronoiSimplexSolver(btDefaultCollisionConfiguration_getSimplexSolver(_native), true);
                }
                return _simplexSolver;
            }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDefaultCollisionConfiguration_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDefaultCollisionConfiguration_new2(IntPtr constructionInfo);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDefaultCollisionConfiguration_getSimplexSolver(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDefaultCollisionConfiguration_setConvexConvexMultipointIterations(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDefaultCollisionConfiguration_setConvexConvexMultipointIterations2(IntPtr obj, int numPerturbationIterations);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDefaultCollisionConfiguration_setConvexConvexMultipointIterations3(IntPtr obj, int numPerturbationIterations, int minimumPointsPerturbationThreshold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDefaultCollisionConfiguration_setPlaneConvexMultipointIterations(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDefaultCollisionConfiguration_setPlaneConvexMultipointIterations2(IntPtr obj, int numPerturbationIterations);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDefaultCollisionConfiguration_setPlaneConvexMultipointIterations3(IntPtr obj, int numPerturbationIterations, int minimumPointsPerturbationThreshold);
	}
}
