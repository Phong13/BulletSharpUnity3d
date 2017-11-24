

namespace BulletSharp
{
	public class UniformScalingShape : ConvexShape
	{
		public UniformScalingShape(ConvexShape convexChildShape, float uniformScalingFactor)
			: base(UnsafeNativeMethods.btUniformScalingShape_new(convexChildShape.Native, uniformScalingFactor))
		{
			ChildShape = convexChildShape;
		}

		public ConvexShape ChildShape
        {
            get;
            set;
        }

		public float UniformScalingFactor
		{
			get { return UnsafeNativeMethods.btUniformScalingShape_getUniformScalingFactor(Native); }
		}
	}
}
