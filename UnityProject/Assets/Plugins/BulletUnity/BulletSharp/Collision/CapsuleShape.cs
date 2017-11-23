using System;

namespace BulletSharp
{
	public class CapsuleShape : ConvexInternalShape
	{
		internal CapsuleShape(IntPtr native)
			: base(native)
		{
		}

		public CapsuleShape(float radius, float height)
			: base(UnsafeNativeMethods.btCapsuleShape_new(radius, height))
		{
		}

		public float HalfHeight
        {
            get { return UnsafeNativeMethods.btCapsuleShape_getHalfHeight(Native); }
        }

		public float Radius { get { return UnsafeNativeMethods.btCapsuleShape_getRadius(Native); } }

		public int UpAxis { get { return UnsafeNativeMethods.btCapsuleShape_getUpAxis(Native); } }
                
	}

	public class CapsuleShapeX : CapsuleShape
	{
		public CapsuleShapeX(float radius, float height)
			: base(UnsafeNativeMethods.btCapsuleShapeX_new(radius, height))
		{
		}
	}

	public class CapsuleShapeZ : CapsuleShape
	{
		public CapsuleShapeZ(float radius, float height)
			: base(UnsafeNativeMethods.btCapsuleShapeZ_new(radius, height))
		{
		}
	}
}
