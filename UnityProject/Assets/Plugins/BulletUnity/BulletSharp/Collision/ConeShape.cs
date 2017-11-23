using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class ConeShape : ConvexInternalShape
	{
		internal ConeShape(IntPtr native)
			: base(native)
		{
		}

		public ConeShape(float radius, float height)
			: base(btConeShape_new(radius, height))
		{
		}

		public int ConeUpIndex
		{
			get => btConeShape_getConeUpIndex(Native);
			set => btConeShape_setConeUpIndex(Native, value);
		}

		public float Height
		{
			get => btConeShape_getHeight(Native);
			set => btConeShape_setHeight(Native, value);
		}

		public float Radius
		{
			get => btConeShape_getRadius(Native);
			set => btConeShape_setRadius(Native, value);
		}
	}

	public class ConeShapeX : ConeShape
	{
		public ConeShapeX(float radius, float height)
			: base(btConeShapeX_new(radius, height))
		{
		}
	}

	public class ConeShapeZ : ConeShape
	{
		public ConeShapeZ(float radius, float height)
			: base(btConeShapeZ_new(radius, height))
		{
		}
	}
}
