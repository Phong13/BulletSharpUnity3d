using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class DefaultCollisionConstructionInfo : IDisposable
	{
		internal IntPtr _native;
		private PoolAllocator _collisionAlgorithmPool;
		private PoolAllocator _persistentManifoldPool;

		public DefaultCollisionConstructionInfo()
		{
			_native = btDefaultCollisionConstructionInfo_new();
		}

		public PoolAllocator CollisionAlgorithmPool
		{
			get => _collisionAlgorithmPool;
			set
			{
				btDefaultCollisionConstructionInfo_setCollisionAlgorithmPool(_native, value._native);
				_collisionAlgorithmPool = value;
			}
		}

		public int CustomCollisionAlgorithmMaxElementSize
		{
			get => btDefaultCollisionConstructionInfo_getCustomCollisionAlgorithmMaxElementSize(_native);
			set => btDefaultCollisionConstructionInfo_setCustomCollisionAlgorithmMaxElementSize(_native, value);
		}

		public int DefaultMaxCollisionAlgorithmPoolSize
		{
			get => btDefaultCollisionConstructionInfo_getDefaultMaxCollisionAlgorithmPoolSize(_native);
			set => btDefaultCollisionConstructionInfo_setDefaultMaxCollisionAlgorithmPoolSize(_native, value);
		}

		public int DefaultMaxPersistentManifoldPoolSize
		{
			get => btDefaultCollisionConstructionInfo_getDefaultMaxPersistentManifoldPoolSize(_native);
			set => btDefaultCollisionConstructionInfo_setDefaultMaxPersistentManifoldPoolSize(_native, value);
		}

		public PoolAllocator PersistentManifoldPool
		{
			get => _persistentManifoldPool;
			set
			{
				btDefaultCollisionConstructionInfo_setPersistentManifoldPool(_native, value._native);
				_persistentManifoldPool = value;
			}
		}

		public int UseEpaPenetrationAlgorithm
		{
			get => btDefaultCollisionConstructionInfo_getUseEpaPenetrationAlgorithm(_native);
			set => btDefaultCollisionConstructionInfo_setUseEpaPenetrationAlgorithm(_native, value);
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
	}

	public class DefaultCollisionConfiguration : CollisionConfiguration
	{
		private PoolAllocator _collisionAlgorithmPool;
		private PoolAllocator _persistentManifoldPool;

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
			_collisionAlgorithmPool = constructionInfo.CollisionAlgorithmPool;
			_persistentManifoldPool = constructionInfo.PersistentManifoldPool;
		}

		public override CollisionAlgorithmCreateFunc GetClosestPointsAlgorithmCreateFunc(BroadphaseNativeType proxyType0, BroadphaseNativeType proxyType1)
		{
			IntPtr createFunc = btCollisionConfiguration_getClosestPointsAlgorithmCreateFunc(Native, (int)proxyType0, (int)proxyType1);
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

		public override CollisionAlgorithmCreateFunc GetCollisionAlgorithmCreateFunc(BroadphaseNativeType proxyType0, BroadphaseNativeType proxyType1)
		{
			IntPtr createFunc = btCollisionConfiguration_getCollisionAlgorithmCreateFunc(Native, (int)proxyType0, (int)proxyType1);
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

		public void SetConvexConvexMultipointIterations(int numPerturbationIterations = 3,
			int minimumPointsPerturbationThreshold = 3)
		{
			btDefaultCollisionConfiguration_setConvexConvexMultipointIterations(
				Native, numPerturbationIterations, minimumPointsPerturbationThreshold);
		}

		public void SetPlaneConvexMultipointIterations(int numPerturbationIterations = 3,
			int minimumPointsPerturbationThreshold = 3)
		{
			btDefaultCollisionConfiguration_setPlaneConvexMultipointIterations(Native,
				numPerturbationIterations, minimumPointsPerturbationThreshold);
		}

		public override PoolAllocator CollisionAlgorithmPool
		{
			get
			{
				if (_collisionAlgorithmPool == null)
				{
					_collisionAlgorithmPool = new PoolAllocator(btCollisionConfiguration_getCollisionAlgorithmPool(Native));
				}
				return _collisionAlgorithmPool;
			}
		}

		public override PoolAllocator PersistentManifoldPool
		{
			get
			{
				if (_persistentManifoldPool == null)
				{
					_persistentManifoldPool = new PoolAllocator(btCollisionConfiguration_getPersistentManifoldPool(Native));
				}
				return _persistentManifoldPool;
			}
		}
	}
}
