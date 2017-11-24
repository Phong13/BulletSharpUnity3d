

namespace BulletSharp
{
	public class Convex2DShape : ConvexShape
	{
		public Convex2DShape(ConvexShape convexChildShape)
			: base(UnsafeNativeMethods.btConvex2dShape_new(convexChildShape.Native))
		{
			ChildShape = convexChildShape;
		}


		public ConvexShape ChildShape {
            get;
            set;
        }
	}
}
