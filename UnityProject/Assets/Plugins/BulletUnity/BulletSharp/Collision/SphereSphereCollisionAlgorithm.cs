using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class SphereSphereCollisionAlgorithm : ActivatingCollisionAlgorithm
	{
		public class CreateFunc : CollisionAlgorithmCreateFunc
		{
			internal CreateFunc(IntPtr native)
				: base(native, true)
			{
			}

			public CreateFunc()
				: base(btSphereSphereCollisionAlgorithm_CreateFunc_new(), false)
			{
			}

			public override CollisionAlgorithm CreateCollisionAlgorithm(CollisionAlgorithmConstructionInfo __unnamed0, CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap)
			{
				return new SphereSphereCollisionAlgorithm(btCollisionAlgorithmCreateFunc_CreateCollisionAlgorithm(
					Native, __unnamed0.Native, body0Wrap.Native, body1Wrap.Native));
			}
		}

		internal SphereSphereCollisionAlgorithm(IntPtr native)
			: base(native)
		{
		}

		public SphereSphereCollisionAlgorithm(PersistentManifold mf, CollisionAlgorithmConstructionInfo ci,
			CollisionObjectWrapper col0Wrap, CollisionObjectWrapper col1Wrap)
			: base(btSphereSphereCollisionAlgorithm_new(mf.Native, ci.Native, col0Wrap.Native,
				col1Wrap.Native))
		{
		}

		public SphereSphereCollisionAlgorithm(CollisionAlgorithmConstructionInfo ci)
			: base(btSphereSphereCollisionAlgorithm_new2(ci.Native))
		{
		}
	}
}
