using System;
using System.Runtime.InteropServices;
using System.Security;

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
				: base(btSoftBodyConcaveCollisionAlgorithm_CreateFunc_new(), false)
			{
			}

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btSoftBodyConcaveCollisionAlgorithm_CreateFunc_new();
		}

		public class SwappedCreateFunc : CollisionAlgorithmCreateFunc
		{
			internal SwappedCreateFunc(IntPtr native)
				: base(native, true)
			{
			}

			public SwappedCreateFunc()
				: base(btSoftBodyConcaveCollisionAlgorithm_SwappedCreateFunc_new(), false)
			{
			}

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btSoftBodyConcaveCollisionAlgorithm_SwappedCreateFunc_new();
		}

		internal SoftBodyConcaveCollisionAlgorithm(IntPtr native)
			: base(native)
		{
		}

		public SoftBodyConcaveCollisionAlgorithm(CollisionAlgorithmConstructionInfo ci, CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap, bool isSwapped)
			: base(btSoftBodyConcaveCollisionAlgorithm_new(ci._native, body0Wrap._native, body1Wrap._native, isSwapped))
		{
		}

		public void ClearCache()
		{
			btSoftBodyConcaveCollisionAlgorithm_clearCache(_native);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBodyConcaveCollisionAlgorithm_new(IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap, bool isSwapped);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyConcaveCollisionAlgorithm_clearCache(IntPtr obj);
	}
}
