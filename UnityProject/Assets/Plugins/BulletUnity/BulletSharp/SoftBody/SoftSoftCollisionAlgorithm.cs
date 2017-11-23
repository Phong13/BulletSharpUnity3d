using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class SoftSoftCollisionAlgorithm : CollisionAlgorithm
	{
		public class CreateFunc : CollisionAlgorithmCreateFunc
		{
			internal CreateFunc(IntPtr native)
				: base(native, true)
			{
			}

			public CreateFunc()
				: base(btSoftSoftCollisionAlgorithm_CreateFunc_new(), false)
			{
			}

			public override CollisionAlgorithm CreateCollisionAlgorithm(CollisionAlgorithmConstructionInfo __unnamed0,
				CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap)
			{
				return new SoftSoftCollisionAlgorithm(btCollisionAlgorithmCreateFunc_CreateCollisionAlgorithm(
					Native, __unnamed0.Native, body0Wrap.Native, body1Wrap.Native));
			}
		}

		internal SoftSoftCollisionAlgorithm(IntPtr native)
			: base(native)
		{
		}

		public SoftSoftCollisionAlgorithm(CollisionAlgorithmConstructionInfo ci)
			: base(btSoftSoftCollisionAlgorithm_new(ci.Native))
		{
		}

		public SoftSoftCollisionAlgorithm(PersistentManifold mf, CollisionAlgorithmConstructionInfo ci,
			CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap)
			: base(btSoftSoftCollisionAlgorithm_new2(mf.Native, ci.Native, body0Wrap.Native,
				body1Wrap.Native))
		{
		}
	}
}
