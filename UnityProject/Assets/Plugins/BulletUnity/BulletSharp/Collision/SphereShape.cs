using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class SphereShape : ConvexInternalShape
	{
		public SphereShape(float radius)
			: base(btSphereShape_new(radius))
		{
		}

		public void SetUnscaledRadius(float radius)
		{
			btSphereShape_setUnscaledRadius(Native, radius);
		}

		public float Radius => btSphereShape_getRadius(Native);
	}
}
