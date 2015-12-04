using System;
using System.Runtime.InteropServices;
using System.Security;

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
				: base(btCompoundCollisionAlgorithm_CreateFunc_new(), false)
			{
			}

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btCompoundCollisionAlgorithm_CreateFunc_new();
		}

		public class SwappedCreateFunc : CollisionAlgorithmCreateFunc
		{
			internal SwappedCreateFunc(IntPtr native)
				: base(native, true)
			{
			}

			public SwappedCreateFunc()
				: base(btCompoundCollisionAlgorithm_SwappedCreateFunc_new(), false)
			{
			}

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btCompoundCollisionAlgorithm_SwappedCreateFunc_new();
		}

		internal CompoundCollisionAlgorithm(IntPtr native)
			: base(native)
		{
		}

		public CompoundCollisionAlgorithm(CollisionAlgorithmConstructionInfo ci, CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap, bool isSwapped)
			: base(btCompoundCollisionAlgorithm_new(ci._native, body0Wrap._native, body1Wrap._native, isSwapped))
		{
		}

		public CollisionAlgorithm GetChildAlgorithm(int n)
		{
			return new CollisionAlgorithm(btCompoundCollisionAlgorithm_getChildAlgorithm(_native, n));
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCompoundCollisionAlgorithm_new(IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap, bool isSwapped);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCompoundCollisionAlgorithm_getChildAlgorithm(IntPtr obj, int n);
	}
}
