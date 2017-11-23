using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class Convex2DShape : ConvexShape
	{
		public Convex2DShape(ConvexShape convexChildShape)
			: base(btConvex2dShape_new(convexChildShape.Native))
		{
			ChildShape = convexChildShape;
		}

		public ConvexShape ChildShape { get; }
	}
}
