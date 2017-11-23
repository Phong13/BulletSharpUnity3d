using System;
using System.Runtime.InteropServices;
using BulletSharp.Math;


namespace BulletSharp
{
	public class CylinderShape : ConvexInternalShape
	{
		internal CylinderShape(IntPtr native)
			: base(native)
		{
		}

		public CylinderShape(Vector3 halfExtents)
			: base(UnsafeNativeMethods.btCylinderShape_new(ref halfExtents))
		{
		}

		public CylinderShape(float halfExtentX, float halfExtentY, float halfExtentZ)
			: base(UnsafeNativeMethods.btCylinderShape_new2(halfExtentX, halfExtentY, halfExtentZ))
		{
		}

		public Vector3 HalfExtentsWithMargin
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btCylinderShape_getHalfExtentsWithMargin(Native, out value);
				return value;
			}
		}

		public Vector3 HalfExtentsWithoutMargin
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btCylinderShape_getHalfExtentsWithoutMargin(Native, out value);
				return value;
			}
		}

		public float Radius => UnsafeNativeMethods.btCylinderShape_getRadius(Native);

		public int UpAxis => UnsafeNativeMethods.btCylinderShape_getUpAxis(Native);
	}

	public class CylinderShapeX : CylinderShape
	{
		public CylinderShapeX(Vector3 halfExtents)
			: base(UnsafeNativeMethods.btCylinderShapeX_new(ref halfExtents))
		{
		}

		public CylinderShapeX(float halfExtentX, float halfExtentY, float halfExtentZ)
			: base(UnsafeNativeMethods.btCylinderShapeX_new2(halfExtentX, halfExtentY, halfExtentZ))
		{
		}
	}

	public class CylinderShapeZ : CylinderShape
	{
		public CylinderShapeZ(Vector3 halfExtents)
			: base(UnsafeNativeMethods.btCylinderShapeZ_new(ref halfExtents))
		{
		}

		public CylinderShapeZ(float halfExtentX, float halfExtentY, float halfExtentZ)
			: base(UnsafeNativeMethods.btCylinderShapeZ_new2(halfExtentX, halfExtentY, halfExtentZ))
		{
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct CylinderShapeFloatData
	{
		public ConvexInternalShapeFloatData ConvexInternalShapeData;
		public int UpAxis;
		public int Padding;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(CylinderShapeFloatData), fieldName).ToInt32(); }
	}
}
