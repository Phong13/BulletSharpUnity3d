using BulletSharp.Math;


namespace BulletSharp
{
	public class MinkowskiSumShape : ConvexInternalShape
	{
		public MinkowskiSumShape(ConvexShape shapeA, ConvexShape shapeB)
			: base(UnsafeNativeMethods.btMinkowskiSumShape_new(shapeA.Native, shapeB.Native))
		{
			ShapeA = shapeA;
			ShapeB = shapeB;
		}

		public ConvexShape ShapeA
        {
            get;
            set;
        }

		public ConvexShape ShapeB
        {
            get;
            set;
        }

		public Matrix TransformA
		{
			get
			{
				Matrix value;
				UnsafeNativeMethods.btMinkowskiSumShape_getTransformA(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btMinkowskiSumShape_setTransformA(Native, ref value);}
		}

		public Matrix TransformB
		{
			get
			{
				Matrix value;
				UnsafeNativeMethods.btMinkowskiSumShape_GetTransformB(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btMinkowskiSumShape_setTransformB(Native, ref value);}
		}
	}
}
