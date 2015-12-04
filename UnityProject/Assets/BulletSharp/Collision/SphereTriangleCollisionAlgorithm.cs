using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class SphereTriangleCollisionAlgorithm : ActivatingCollisionAlgorithm
	{
		public class CreateFunc : CollisionAlgorithmCreateFunc
		{
			internal CreateFunc(IntPtr native)
				: base(native, true)
			{
			}

			public CreateFunc()
				: base(btSphereTriangleCollisionAlgorithm_CreateFunc_new(), false)
			{
			}

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btSphereTriangleCollisionAlgorithm_CreateFunc_new();
		}

		public SphereTriangleCollisionAlgorithm(PersistentManifold mf, CollisionAlgorithmConstructionInfo ci, CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap, bool swapped)
			: base(btSphereTriangleCollisionAlgorithm_new(mf._native, ci._native, body0Wrap._native, body1Wrap._native, swapped))
		{
		}

		public SphereTriangleCollisionAlgorithm(CollisionAlgorithmConstructionInfo ci)
			: base(btSphereTriangleCollisionAlgorithm_new2(ci._native))
		{
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSphereTriangleCollisionAlgorithm_new(IntPtr mf, IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap, bool swapped);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSphereTriangleCollisionAlgorithm_new2(IntPtr ci);
	}
}
