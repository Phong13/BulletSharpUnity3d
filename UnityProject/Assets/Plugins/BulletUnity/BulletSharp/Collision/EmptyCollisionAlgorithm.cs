using System;


namespace BulletSharp
{
	public class EmptyAlgorithm : CollisionAlgorithm
	{
		public class CreateFunc : CollisionAlgorithmCreateFunc
		{
			internal CreateFunc(IntPtr native)
				: base(native, true)
			{
			}

			public CreateFunc()
				: base(UnsafeNativeMethods.btEmptyAlgorithm_CreateFunc_new(), false)
			{
			}

			public override CollisionAlgorithm CreateCollisionAlgorithm(CollisionAlgorithmConstructionInfo __unnamed0, CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap)
			{
				return new EmptyAlgorithm(UnsafeNativeMethods.btCollisionAlgorithmCreateFunc_CreateCollisionAlgorithm(
					Native, __unnamed0.Native, body0Wrap.Native, body1Wrap.Native));
			}
		}

		internal EmptyAlgorithm(IntPtr native)
			: base(native)
		{
		}

		public EmptyAlgorithm(CollisionAlgorithmConstructionInfo ci)
			: base(UnsafeNativeMethods.btEmptyAlgorithm_new(ci.Native))
		{
		}
	}
}
