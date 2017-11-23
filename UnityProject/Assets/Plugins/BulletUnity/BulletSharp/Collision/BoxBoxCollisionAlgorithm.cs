using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class BoxBoxCollisionAlgorithm : ActivatingCollisionAlgorithm
	{
		public class CreateFunc : CollisionAlgorithmCreateFunc
		{
			internal CreateFunc(IntPtr native)
				: base(native, true)
			{
			}

			public CreateFunc()
				: base(btBoxBoxCollisionAlgorithm_CreateFunc_new(), false)
			{
			}

			public override CollisionAlgorithm CreateCollisionAlgorithm(CollisionAlgorithmConstructionInfo __unnamed0,
				CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap)
			{
				return new BoxBoxCollisionAlgorithm(btCollisionAlgorithmCreateFunc_CreateCollisionAlgorithm(
					Native, __unnamed0.Native, body0Wrap.Native, body1Wrap.Native));
			}
    	}

		internal BoxBoxCollisionAlgorithm(IntPtr native)
			: base(native)
		{
		}

		public BoxBoxCollisionAlgorithm(CollisionAlgorithmConstructionInfo ci)
			: base(btBoxBoxCollisionAlgorithm_new(ci.Native))
		{
		}

		public BoxBoxCollisionAlgorithm(PersistentManifold mf, CollisionAlgorithmConstructionInfo ci,
			CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap)
			: base(btBoxBoxCollisionAlgorithm_new2(mf.Native, ci.Native, body0Wrap.Native,
				body1Wrap.Native))
		{
		}
	}
}
