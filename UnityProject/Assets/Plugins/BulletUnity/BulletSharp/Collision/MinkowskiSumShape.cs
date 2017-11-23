using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class MinkowskiSumShape : ConvexInternalShape
	{
		public MinkowskiSumShape(ConvexShape shapeA, ConvexShape shapeB)
			: base(btMinkowskiSumShape_new(shapeA.Native, shapeB.Native))
		{
			ShapeA = shapeA;
			ShapeB = shapeB;
		}

		public ConvexShape ShapeA { get; }

		public ConvexShape ShapeB { get; }

		public Matrix TransformA
		{
			get
			{
				Matrix value;
				btMinkowskiSumShape_getTransformA(Native, out value);
				return value;
			}
			set => btMinkowskiSumShape_setTransformA(Native, ref value);
		}

		public Matrix TransformB
		{
			get
			{
				Matrix value;
				btMinkowskiSumShape_GetTransformB(Native, out value);
				return value;
			}
			set => btMinkowskiSumShape_setTransformB(Native, ref value);
		}
	}
}
