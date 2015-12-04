using System;
using System.Runtime.InteropServices;
using System.Security;

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

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btSoftSoftCollisionAlgorithm_CreateFunc_new();
		}

		internal SoftSoftCollisionAlgorithm(IntPtr native)
			: base(native)
		{
		}

		public SoftSoftCollisionAlgorithm(CollisionAlgorithmConstructionInfo ci)
			: base(btSoftSoftCollisionAlgorithm_new(ci._native))
		{
		}

		public SoftSoftCollisionAlgorithm(PersistentManifold mf, CollisionAlgorithmConstructionInfo ci, CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap)
			: base(btSoftSoftCollisionAlgorithm_new2(mf._native, ci._native, body0Wrap._native, body1Wrap._native))
		{
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftSoftCollisionAlgorithm_new(IntPtr ci);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftSoftCollisionAlgorithm_new2(IntPtr mf, IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap);
	}
}
