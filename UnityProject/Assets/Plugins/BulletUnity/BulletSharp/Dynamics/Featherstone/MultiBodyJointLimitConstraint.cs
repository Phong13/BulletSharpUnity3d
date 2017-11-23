using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class MultiBodyJointLimitConstraint : MultiBodyConstraint
	{
		public MultiBodyJointLimitConstraint(MultiBody body, int link, float lower,
			float upper)
			: base(btMultiBodyJointLimitConstraint_new(body.Native, link, lower,
				upper), body, body)
		{
		}
	}
}
