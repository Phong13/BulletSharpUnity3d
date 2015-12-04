using System;
using System.Runtime.InteropServices;
using System.Security;

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

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btBoxBoxCollisionAlgorithm_CreateFunc_new();
		}

		public BoxBoxCollisionAlgorithm(CollisionAlgorithmConstructionInfo ci)
			: base(btBoxBoxCollisionAlgorithm_new(ci._native))
		{
		}

		public BoxBoxCollisionAlgorithm(PersistentManifold mf, CollisionAlgorithmConstructionInfo ci, CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap)
			: base(btBoxBoxCollisionAlgorithm_new2(mf._native, ci._native, body0Wrap._native, body1Wrap._native))
		{
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBoxBoxCollisionAlgorithm_new(IntPtr ci);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBoxBoxCollisionAlgorithm_new2(IntPtr mf, IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap);
	}
}
