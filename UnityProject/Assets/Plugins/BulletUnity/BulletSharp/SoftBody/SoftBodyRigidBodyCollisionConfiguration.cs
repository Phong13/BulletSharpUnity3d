using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class SoftBodyRigidBodyCollisionConfiguration : DefaultCollisionConfiguration
	{
		public SoftBodyRigidBodyCollisionConfiguration()
			: base(btSoftBodyRigidBodyCollisionConfiguration_new())
		{
		}

		public SoftBodyRigidBodyCollisionConfiguration(DefaultCollisionConstructionInfo constructionInfo)
			: base(btSoftBodyRigidBodyCollisionConfiguration_new2(constructionInfo._native))
		{
		}

		public override CollisionAlgorithmCreateFunc GetCollisionAlgorithmCreateFunc(BroadphaseNativeType proxyType0,
			BroadphaseNativeType proxyType1)
		{
			IntPtr createFunc = btCollisionConfiguration_getCollisionAlgorithmCreateFunc(Native, (int)proxyType0, (int)proxyType1);
			if (proxyType0 == BroadphaseNativeType.SoftBodyShape && proxyType1 == BroadphaseNativeType.SoftBodyShape)
			{
				return new SoftSoftCollisionAlgorithm.CreateFunc(createFunc);
			}
			if (proxyType0 == BroadphaseNativeType.SoftBodyShape && BroadphaseProxy.IsConvex(proxyType1))
			{
				return new SoftRigidCollisionAlgorithm.CreateFunc(createFunc);
			}
			if (BroadphaseProxy.IsConvex(proxyType0) && proxyType1 == BroadphaseNativeType.SoftBodyShape)
			{
				return new SoftRigidCollisionAlgorithm.CreateFunc(createFunc);
			}
			if (proxyType0 == BroadphaseNativeType.SoftBodyShape && BroadphaseProxy.IsConcave(proxyType1))
			{
				return new SoftBodyConcaveCollisionAlgorithm.CreateFunc(createFunc);
			}
			if (BroadphaseProxy.IsConcave(proxyType0) && proxyType1 == BroadphaseNativeType.SoftBodyShape)
			{
				return new SoftBodyConcaveCollisionAlgorithm.SwappedCreateFunc(createFunc);
			}
			return base.GetCollisionAlgorithmCreateFunc(proxyType0, proxyType1);
		}
	}
}
