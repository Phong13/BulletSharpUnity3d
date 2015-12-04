using System;
using System.Runtime.InteropServices;
using System.Security;

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

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btSphereSphereCollisionAlgorithm_CreateFunc_new();
		}

		internal SphereSphereCollisionAlgorithm(IntPtr native)
			: base(native)
		{
		}

		public SphereSphereCollisionAlgorithm(PersistentManifold mf, CollisionAlgorithmConstructionInfo ci, CollisionObjectWrapper col0Wrap, CollisionObjectWrapper col1Wrap)
			: base(btSphereSphereCollisionAlgorithm_new(mf._native, ci._native, col0Wrap._native, col1Wrap._native))
		{
		}

		public SphereSphereCollisionAlgorithm(CollisionAlgorithmConstructionInfo ci)
			: base(btSphereSphereCollisionAlgorithm_new2(ci._native))
		{
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSphereSphereCollisionAlgorithm_new(IntPtr mf, IntPtr ci, IntPtr col0Wrap, IntPtr col1Wrap);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSphereSphereCollisionAlgorithm_new2(IntPtr ci);
	}
}
