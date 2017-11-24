

namespace BulletSharp
{
	public class SphereShape : ConvexInternalShape
	{
		public SphereShape(float radius)
			: base(UnsafeNativeMethods.btSphereShape_new(radius))
		{
		}

		public void SetUnscaledRadius(float radius)
		{
			UnsafeNativeMethods.btSphereShape_setUnscaledRadius(Native, radius);
		}

		public float Radius{ get { return  UnsafeNativeMethods.btSphereShape_getRadius(Native);} }
	}
}
