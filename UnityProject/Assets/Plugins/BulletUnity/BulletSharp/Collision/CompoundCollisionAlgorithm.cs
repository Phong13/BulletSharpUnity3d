using System;


namespace BulletSharp
{
	public class CompoundCollisionAlgorithm : ActivatingCollisionAlgorithm
	{
		public class CreateFunc : CollisionAlgorithmCreateFunc
		{
			internal CreateFunc(IntPtr native)
				: base(native, true)
			{
			}

			public CreateFunc()
				: base(UnsafeNativeMethods.btCompoundCollisionAlgorithm_CreateFunc_new(), false)
			{
			}

			public override CollisionAlgorithm CreateCollisionAlgorithm(CollisionAlgorithmConstructionInfo __unnamed0,
				CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap)
			{
				return new CompoundCollisionAlgorithm(UnsafeNativeMethods.btCollisionAlgorithmCreateFunc_CreateCollisionAlgorithm(
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
				: base(UnsafeNativeMethods.btCompoundCollisionAlgorithm_SwappedCreateFunc_new(), false)
			{
			}

			public override CollisionAlgorithm CreateCollisionAlgorithm(CollisionAlgorithmConstructionInfo __unnamed0,
				CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap)
			{
				return new CompoundCollisionAlgorithm(UnsafeNativeMethods.btCollisionAlgorithmCreateFunc_CreateCollisionAlgorithm(
					Native, __unnamed0.Native, body0Wrap.Native, body1Wrap.Native));
			}
		}

		internal CompoundCollisionAlgorithm(IntPtr native)
			: base(native)
		{
		}

		public CompoundCollisionAlgorithm(CollisionAlgorithmConstructionInfo ci,
			CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap, bool isSwapped)
			: base(UnsafeNativeMethods.btCompoundCollisionAlgorithm_new(ci.Native, body0Wrap.Native,
				body1Wrap.Native, isSwapped))
		{
		}

		public CollisionAlgorithm GetChildAlgorithm(int n)
		{
			return new CollisionAlgorithm(UnsafeNativeMethods.btCompoundCollisionAlgorithm_getChildAlgorithm(Native, n));
		}
	}
}
