using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public abstract class ConvexInternalShape : ConvexShape
	{
		internal ConvexInternalShape(IntPtr native)
			: base(native)
		{
		}

		public void SetSafeMargin(float minDimension)
		{
			btConvexInternalShape_setSafeMargin(_native, minDimension);
		}

		public void SetSafeMargin(float minDimension, float defaultMarginMultiplier)
		{
			btConvexInternalShape_setSafeMargin2(_native, minDimension, defaultMarginMultiplier);
		}

        public void SetSafeMarginRef(ref Vector3 halfExtents)
        {
            btConvexInternalShape_setSafeMargin3(_native, ref halfExtents);
        }

		public void SetSafeMargin(Vector3 halfExtents)
		{
			btConvexInternalShape_setSafeMargin3(_native, ref halfExtents);
		}

        public void SetSafeMarginRef(ref Vector3 halfExtents, float defaultMarginMultiplier)
        {
            btConvexInternalShape_setSafeMargin4(_native, ref halfExtents, defaultMarginMultiplier);
        }

		public void SetSafeMargin(Vector3 halfExtents, float defaultMarginMultiplier)
		{
			btConvexInternalShape_setSafeMargin4(_native, ref halfExtents, defaultMarginMultiplier);
		}

		public Vector3 ImplicitShapeDimensions
		{
			get
			{
				Vector3 value;
				btConvexInternalShape_getImplicitShapeDimensions(_native, out value);
				return value;
			}
			set { btConvexInternalShape_setImplicitShapeDimensions(_native, ref value); }
		}

		public Vector3 LocalScalingNV
		{
			get
			{
				Vector3 value;
				btConvexInternalShape_getLocalScalingNV(_native, out value);
				return value;
			}
		}

		public float MarginNV
		{
			get { return btConvexInternalShape_getMarginNV(_native); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexInternalShape_getImplicitShapeDimensions(IntPtr obj, [Out] out Vector3 dimensions);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexInternalShape_getLocalScalingNV(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btConvexInternalShape_getMarginNV(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexInternalShape_setImplicitShapeDimensions(IntPtr obj, [In] ref Vector3 dimensions);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexInternalShape_setSafeMargin(IntPtr obj, float minDimension);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexInternalShape_setSafeMargin2(IntPtr obj, float minDimension, float defaultMarginMultiplier);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexInternalShape_setSafeMargin3(IntPtr obj, [In] ref Vector3 halfExtents);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexInternalShape_setSafeMargin4(IntPtr obj, [In] ref Vector3 halfExtents, float defaultMarginMultiplier);
	}

	public abstract class ConvexInternalAabbCachingShape : ConvexInternalShape
	{
		internal ConvexInternalAabbCachingShape(IntPtr native)
			: base(native)
		{
		}

		public void RecalcLocalAabb()
		{
			btConvexInternalAabbCachingShape_recalcLocalAabb(_native);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexInternalAabbCachingShape_recalcLocalAabb(IntPtr obj);
	}

    [StructLayout(LayoutKind.Sequential)]
    internal struct ConvexInternalShapeFloatData
    {
        public CollisionShapeFloatData CollisionShapeData;
        public Vector3FloatData LocalScaling;
        public Vector3FloatData ImplicitShapeDimensions;
        public float CollisionMargin;
        public int Padding;

        public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(ConvexInternalShapeFloatData), fieldName).ToInt32(); }
    }
}
