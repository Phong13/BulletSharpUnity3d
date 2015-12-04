using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class SphereBoxCollisionAlgorithm : ActivatingCollisionAlgorithm
	{
		public class CreateFunc : CollisionAlgorithmCreateFunc
		{
			internal CreateFunc(IntPtr native)
				: base(native, true)
			{
			}

			public CreateFunc()
				: base(btSphereBoxCollisionAlgorithm_CreateFunc_new(), false)
			{
			}

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btSphereBoxCollisionAlgorithm_CreateFunc_new();
		}

		public SphereBoxCollisionAlgorithm(PersistentManifold mf, CollisionAlgorithmConstructionInfo ci, CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap, bool isSwapped)
			: base(btSphereBoxCollisionAlgorithm_new(mf._native, ci._native, body0Wrap._native, body1Wrap._native, isSwapped))
		{
		}

        public bool GetSphereDistance(CollisionObjectWrapper boxObjWrap, ref Vector3 v3PointOnBox, ref Vector3 normal, out float penetrationDepth, ref Vector3 v3SphereCenter, float fRadius, float maxContactDistance)
		{
            return btSphereBoxCollisionAlgorithm_getSphereDistance(_native, boxObjWrap._native, ref v3PointOnBox, ref normal, out penetrationDepth, ref v3SphereCenter, fRadius, maxContactDistance);
		}

        public float GetSpherePenetration(ref Vector3 boxHalfExtent, ref Vector3 sphereRelPos, ref Vector3 closestPoint, ref Vector3 normal)
		{
            return btSphereBoxCollisionAlgorithm_getSpherePenetration(_native, ref boxHalfExtent, ref sphereRelPos, ref closestPoint, ref normal);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSphereBoxCollisionAlgorithm_new(IntPtr mf, IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap, bool isSwapped);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btSphereBoxCollisionAlgorithm_getSphereDistance(IntPtr obj, IntPtr boxObjWrap, [In] ref Vector3 v3PointOnBox, [In] ref Vector3 normal, [Out] out float penetrationDepth, [In] ref Vector3 v3SphereCenter, float fRadius, float maxContactDistance);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSphereBoxCollisionAlgorithm_getSpherePenetration(IntPtr obj, [In] ref Vector3 boxHalfExtent, [In] ref Vector3 sphereRelPos, [In] ref Vector3 closestPoint, [In] ref Vector3 normal);
	}
}
