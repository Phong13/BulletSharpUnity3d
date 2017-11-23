using System;


namespace BulletSharp
{
	public class SoftBodyConcaveCollisionAlgorithm : CollisionAlgorithm
	{
		public class CreateFunc : CollisionAlgorithmCreateFunc
		{
			internal CreateFunc(IntPtr native)
				: base(native, true)
			{
			}

			public CreateFunc()
				: base(UnsafeNativeMethods.btSoftBodyConcaveCollisionAlgorithm_CreateFunc_new(), false)
			{
			}

			public override CollisionAlgorithm CreateCollisionAlgorithm(CollisionAlgorithmConstructionInfo __unnamed0,
				CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap)
			{
				return new SoftBodyConcaveCollisionAlgorithm(UnsafeNativeMethods.btCollisionAlgorithmCreateFunc_CreateCollisionAlgorithm(
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
				: base(UnsafeNativeMethods.btSoftBodyConcaveCollisionAlgorithm_SwappedCreateFunc_new(), false)
			{
			}

			public override CollisionAlgorithm CreateCollisionAlgorithm(CollisionAlgorithmConstructionInfo __unnamed0,
				CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap)
			{
				return new SoftBodyConcaveCollisionAlgorithm(UnsafeNativeMethods.btCollisionAlgorithmCreateFunc_CreateCollisionAlgorithm(
					Native, __unnamed0.Native, body0Wrap.Native, body1Wrap.Native));
			}
		}

		internal SoftBodyConcaveCollisionAlgorithm(IntPtr native)
			: base(native)
		{
		}

		public SoftBodyConcaveCollisionAlgorithm(CollisionAlgorithmConstructionInfo ci,
			CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap, bool isSwapped)
			: base(UnsafeNativeMethods.btSoftBodyConcaveCollisionAlgorithm_new(ci.Native, body0Wrap.Native,
				body1Wrap.Native, isSwapped))
		{
		}

		public void ClearCache()
		{
			UnsafeNativeMethods.btSoftBodyConcaveCollisionAlgorithm_clearCache(Native);
		}
	}
}
