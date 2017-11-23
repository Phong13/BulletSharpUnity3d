using System;


namespace BulletSharp
{
	public class SphereTriangleCollisionAlgorithm : ActivatingCollisionAlgorithm
	{
		public class CreateFunc : CollisionAlgorithmCreateFunc
		{
			internal CreateFunc(IntPtr native)
				: base(native, true)
			{
			}

			public CreateFunc()
				: base(UnsafeNativeMethods.btSphereTriangleCollisionAlgorithm_CreateFunc_new(), false)
			{
			}

			public override CollisionAlgorithm CreateCollisionAlgorithm(CollisionAlgorithmConstructionInfo __unnamed0, CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap)
			{
				return new SphereTriangleCollisionAlgorithm(UnsafeNativeMethods.btCollisionAlgorithmCreateFunc_CreateCollisionAlgorithm(
					Native, __unnamed0.Native, body0Wrap.Native, body1Wrap.Native));
			}
		}

		internal SphereTriangleCollisionAlgorithm(IntPtr native)
			: base(native)
		{
		}

		public SphereTriangleCollisionAlgorithm(PersistentManifold mf, CollisionAlgorithmConstructionInfo ci,
			CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap, bool swapped)
			: base(UnsafeNativeMethods.btSphereTriangleCollisionAlgorithm_new(mf.Native, ci.Native,
				body0Wrap.Native, body1Wrap.Native, swapped))
		{
		}

		public SphereTriangleCollisionAlgorithm(CollisionAlgorithmConstructionInfo ci)
			: base(UnsafeNativeMethods.btSphereTriangleCollisionAlgorithm_new2(ci.Native))
		{
		}
	}
}
