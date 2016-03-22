using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public sealed class TransformUtil
	{
        public static void CalculateDiffAxisAngle(ref Matrix transform0, ref Matrix transform1, out Vector3 axis, out float angle)
		{
			btTransformUtil_calculateDiffAxisAngle(ref transform0, ref transform1, out axis, out angle);
		}

        public static void CalculateDiffAxisAngleQuaternion(ref Quaternion orn0, ref Quaternion orn1a, out Vector3 axis, out float angle)
		{
			btTransformUtil_calculateDiffAxisAngleQuaternion(ref orn0, ref orn1a, out axis, out angle);
		}

        public static void CalculateVelocity(ref Matrix transform0, ref Matrix transform1, float timeStep, out Vector3 linVel, out Vector3 angVel)
		{
			btTransformUtil_calculateVelocity(ref transform0, ref transform1, timeStep, out linVel, out angVel);
		}

        public static void CalculateVelocityQuaternion(ref Vector3 pos0, ref Vector3 pos1, ref Quaternion orn0, ref Quaternion orn1, float timeStep, out Vector3 linVel, out Vector3 angVel)
		{
			btTransformUtil_calculateVelocityQuaternion(ref pos0, ref pos1, ref orn0, ref orn1, timeStep, out linVel, out angVel);
		}

        public static void IntegrateTransform(ref Matrix curTrans, ref Vector3 linvel, ref Vector3 angvel, float timeStep, out Matrix predictedTransform)
		{
			btTransformUtil_integrateTransform(ref curTrans, ref linvel, ref angvel, timeStep, out predictedTransform);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTransformUtil_calculateDiffAxisAngle([In] ref Matrix transform0, [In] ref Matrix transform1, [Out] out Vector3 axis, [Out] out float angle);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btTransformUtil_calculateDiffAxisAngleQuaternion([In] ref Quaternion orn0, [In] ref Quaternion orn1a, [Out] out Vector3 axis, [Out] out float angle);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTransformUtil_calculateVelocity([In] ref Matrix transform0, [In] ref Matrix transform1, float timeStep, [Out] out Vector3 linVel, [Out] out Vector3 angVel);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTransformUtil_calculateVelocityQuaternion([In] ref Vector3 pos0, [In] ref Vector3 pos1, [In] ref Quaternion orn0, [In] ref Quaternion orn1, float timeStep, [Out] out Vector3 linVel, [Out] out Vector3 angVel);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTransformUtil_integrateTransform([In] ref Matrix curTrans, [In] ref Vector3 linvel, [In] ref Vector3 angvel, float timeStep, [Out] out Matrix predictedTransform);
	}

	public class ConvexSeparatingDistanceUtil : IDisposable
	{
		internal IntPtr _native;

		internal ConvexSeparatingDistanceUtil(IntPtr native)
		{
			_native = native;
		}

		public ConvexSeparatingDistanceUtil(float boundingRadiusA, float boundingRadiusB)
		{
			_native = btConvexSeparatingDistanceUtil_new(boundingRadiusA, boundingRadiusB);
		}

		public void InitSeparatingDistance(ref Vector3 separatingVector, float separatingDistance, ref Matrix transA, ref Matrix transB)
		{
			btConvexSeparatingDistanceUtil_initSeparatingDistance(_native, ref separatingVector, separatingDistance, ref transA, ref transB);
		}

		public void UpdateSeparatingDistance(ref Matrix transA, ref Matrix transB)
		{
			btConvexSeparatingDistanceUtil_updateSeparatingDistance(_native, ref transA, ref transB);
		}

		public float ConservativeSeparatingDistance
		{
			get { return btConvexSeparatingDistanceUtil_getConservativeSeparatingDistance(_native); }
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				btConvexSeparatingDistanceUtil_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~ConvexSeparatingDistanceUtil()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btConvexSeparatingDistanceUtil_new(float boundingRadiusA, float boundingRadiusB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btConvexSeparatingDistanceUtil_getConservativeSeparatingDistance(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexSeparatingDistanceUtil_initSeparatingDistance(IntPtr obj, [In] ref Vector3 separatingVector, float separatingDistance, [In] ref Matrix transA, [In] ref Matrix transB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexSeparatingDistanceUtil_updateSeparatingDistance(IntPtr obj, [In] ref Matrix transA, [In] ref Matrix transB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexSeparatingDistanceUtil_delete(IntPtr obj);
	}
}
