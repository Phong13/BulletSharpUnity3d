using System;
using System.Runtime.InteropServices;
using BulletSharp.Math;


namespace BulletSharp
{
	public abstract class ConvexInternalShape : ConvexShape
	{
		internal ConvexInternalShape(IntPtr native)
			: base(native)
		{
		}

		public void SetSafeMargin(float minDimension, float defaultMarginMultiplier = 0.1f)
		{
			UnsafeNativeMethods.btConvexInternalShape_setSafeMargin(Native, minDimension, defaultMarginMultiplier);
		}

		public void SetSafeMarginRef(ref Vector3 halfExtents, float defaultMarginMultiplier = 0.1f)
		{
			UnsafeNativeMethods.btConvexInternalShape_setSafeMargin2(Native, ref halfExtents, defaultMarginMultiplier);
		}

		public void SetSafeMargin(Vector3 halfExtents, float defaultMarginMultiplier = 0.1f)
		{
			UnsafeNativeMethods.btConvexInternalShape_setSafeMargin2(Native, ref halfExtents, defaultMarginMultiplier);
		}

		public Vector3 ImplicitShapeDimensions
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btConvexInternalShape_getImplicitShapeDimensions(Native, out value);
				return value;
			}
			set { UnsafeNativeMethods.btConvexInternalShape_setImplicitShapeDimensions(Native, ref value); }
		}

		public Vector3 LocalScalingNV
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btConvexInternalShape_getLocalScalingNV(Native, out value);
				return value;
			}
		}

		public float MarginNV{ get { return  UnsafeNativeMethods.btConvexInternalShape_getMarginNV(Native);} }
	}

	public abstract class ConvexInternalAabbCachingShape : ConvexInternalShape
	{
		internal ConvexInternalAabbCachingShape(IntPtr native)
			: base(native)
		{
		}

		public void RecalcLocalAabb()
		{
			UnsafeNativeMethods.btConvexInternalAabbCachingShape_recalcLocalAabb(Native);
		}
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
