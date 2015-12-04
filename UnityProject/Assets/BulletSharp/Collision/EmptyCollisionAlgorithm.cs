using System;
using System.Runtime.InteropServices;
using System.Security;

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
				: base(btEmptyAlgorithm_CreateFunc_new(), false)
			{
			}

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btEmptyAlgorithm_CreateFunc_new();
		}

		public EmptyAlgorithm(CollisionAlgorithmConstructionInfo ci)
			: base(btEmptyAlgorithm_new(ci._native))
		{
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btEmptyAlgorithm_new(IntPtr ci);
	}
}
