using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class UniformScalingShape : ConvexShape
	{
		public UniformScalingShape(ConvexShape convexChildShape, float uniformScalingFactor)
			: base(btUniformScalingShape_new(convexChildShape.Native, uniformScalingFactor))
		{
			ChildShape = convexChildShape;
		}

		public ConvexShape ChildShape { get; }

		public float UniformScalingFactor
		{
			get { return btUniformScalingShape_getUniformScalingFactor(Native); }
		}
	}
}
