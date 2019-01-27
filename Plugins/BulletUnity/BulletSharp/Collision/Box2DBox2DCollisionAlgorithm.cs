using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class Box2DBox2DCollisionAlgorithm : ActivatingCollisionAlgorithm
	{
		public class CreateFunc : CollisionAlgorithmCreateFunc
		{
			internal CreateFunc(IntPtr native)
				: base(native, true)
			{
			}

			public CreateFunc()
				: base(btBox2dBox2dCollisionAlgorithm_CreateFunc_new(), false)
			{
			}

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btBox2dBox2dCollisionAlgorithm_CreateFunc_new();
		}

		public Box2DBox2DCollisionAlgorithm(CollisionAlgorithmConstructionInfo ci)
			: base(btBox2dBox2dCollisionAlgorithm_new(ci._native))
		{
		}

		public Box2DBox2DCollisionAlgorithm(PersistentManifold mf, CollisionAlgorithmConstructionInfo ci, CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap)
			: base(btBox2dBox2dCollisionAlgorithm_new2(mf._native, ci._native, body0Wrap._native, body1Wrap._native))
		{
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBox2dBox2dCollisionAlgorithm_new(IntPtr ci);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBox2dBox2dCollisionAlgorithm_new2(IntPtr mf, IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap);
	}
}
