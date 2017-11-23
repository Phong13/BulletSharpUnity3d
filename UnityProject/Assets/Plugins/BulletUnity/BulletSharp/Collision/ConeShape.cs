using System;


namespace BulletSharp
{
	public class ConeShape : ConvexInternalShape
	{
		internal ConeShape(IntPtr native)
			: base(native)
		{
		}

		public ConeShape(float radius, float height)
			: base(UnsafeNativeMethods.btConeShape_new(radius, height))
		{
		}

		public int ConeUpIndex
		{
			get => UnsafeNativeMethods.btConeShape_getConeUpIndex(Native);
			set => UnsafeNativeMethods.btConeShape_setConeUpIndex(Native, value);
		}

		public float Height
		{
			get => UnsafeNativeMethods.btConeShape_getHeight(Native);
			set => UnsafeNativeMethods.btConeShape_setHeight(Native, value);
		}

		public float Radius
		{
			get => UnsafeNativeMethods.btConeShape_getRadius(Native);
			set => UnsafeNativeMethods.btConeShape_setRadius(Native, value);
		}
	}

	public class ConeShapeX : ConeShape
	{
		public ConeShapeX(float radius, float height)
			: base(UnsafeNativeMethods.btConeShapeX_new(radius, height))
		{
		}
	}

	public class ConeShapeZ : ConeShape
	{
		public ConeShapeZ(float radius, float height)
			: base(UnsafeNativeMethods.btConeShapeZ_new(radius, height))
		{
		}
	}
}
