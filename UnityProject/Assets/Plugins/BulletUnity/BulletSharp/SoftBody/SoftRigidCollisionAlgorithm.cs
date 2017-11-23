using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class SoftRigidCollisionAlgorithm : CollisionAlgorithm
	{
		public class CreateFunc : CollisionAlgorithmCreateFunc
		{
			internal CreateFunc(IntPtr native)
				: base(native, true)
			{
			}

			public CreateFunc()
				: base(btSoftRigidCollisionAlgorithm_CreateFunc_new(), false)
			{
			}

			public override CollisionAlgorithm CreateCollisionAlgorithm(CollisionAlgorithmConstructionInfo __unnamed0,
				CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap)
			{
				return new SoftRigidCollisionAlgorithm(btCollisionAlgorithmCreateFunc_CreateCollisionAlgorithm(
					Native, __unnamed0.Native, body0Wrap.Native, body1Wrap.Native));
			}
		}

		internal SoftRigidCollisionAlgorithm(IntPtr native)
			: base(native)
		{
		}

		public SoftRigidCollisionAlgorithm(PersistentManifold mf, CollisionAlgorithmConstructionInfo ci,
			CollisionObjectWrapper col0, CollisionObjectWrapper col1Wrap, bool isSwapped)
			: base(btSoftRigidCollisionAlgorithm_new(mf.Native, ci.Native, col0.Native,
				col1Wrap.Native, isSwapped))
		{
		}
	}
}
