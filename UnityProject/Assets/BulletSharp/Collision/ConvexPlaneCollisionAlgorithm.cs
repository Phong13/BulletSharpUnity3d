using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class ConvexPlaneCollisionAlgorithm : CollisionAlgorithm
	{
		public class CreateFunc : CollisionAlgorithmCreateFunc
		{
			internal CreateFunc(IntPtr native)
				: base(native, true)
			{
			}

			public CreateFunc()
				: base(btConvexPlaneCollisionAlgorithm_CreateFunc_new(), false)
			{
			}

			public int MinimumPointsPerturbationThreshold
			{
				get { return btConvexPlaneCollisionAlgorithm_CreateFunc_getMinimumPointsPerturbationThreshold(_native); }
				set { btConvexPlaneCollisionAlgorithm_CreateFunc_setMinimumPointsPerturbationThreshold(_native, value); }
			}

			public int NumPerturbationIterations
			{
				get { return btConvexPlaneCollisionAlgorithm_CreateFunc_getNumPerturbationIterations(_native); }
				set { btConvexPlaneCollisionAlgorithm_CreateFunc_setNumPerturbationIterations(_native, value); }
			}

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btConvexPlaneCollisionAlgorithm_CreateFunc_new();
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern int btConvexPlaneCollisionAlgorithm_CreateFunc_getMinimumPointsPerturbationThreshold(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern int btConvexPlaneCollisionAlgorithm_CreateFunc_getNumPerturbationIterations(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btConvexPlaneCollisionAlgorithm_CreateFunc_setMinimumPointsPerturbationThreshold(IntPtr obj, int value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btConvexPlaneCollisionAlgorithm_CreateFunc_setNumPerturbationIterations(IntPtr obj, int value);
		}

		internal ConvexPlaneCollisionAlgorithm(IntPtr native)
			: base(native)
		{
		}

		public ConvexPlaneCollisionAlgorithm(PersistentManifold mf, CollisionAlgorithmConstructionInfo ci, CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap, bool isSwapped, int numPerturbationIterations, int minimumPointsPerturbationThreshold)
			: base(btConvexPlaneCollisionAlgorithm_new(mf._native, ci._native, body0Wrap._native, body1Wrap._native, isSwapped, numPerturbationIterations, minimumPointsPerturbationThreshold))
		{
		}

		public void CollideSingleContact(Quaternion perturbeRot, CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap, DispatcherInfo dispatchInfo, ManifoldResult resultOut)
		{
			btConvexPlaneCollisionAlgorithm_collideSingleContact(_native, ref perturbeRot, body0Wrap._native, body1Wrap._native, dispatchInfo._native, resultOut._native);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btConvexPlaneCollisionAlgorithm_new(IntPtr mf, IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap, bool isSwapped, int numPerturbationIterations, int minimumPointsPerturbationThreshold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexPlaneCollisionAlgorithm_collideSingleContact(IntPtr obj, [In] ref Quaternion perturbeRot, IntPtr body0Wrap, IntPtr body1Wrap, IntPtr dispatchInfo, IntPtr resultOut);
	}
}
