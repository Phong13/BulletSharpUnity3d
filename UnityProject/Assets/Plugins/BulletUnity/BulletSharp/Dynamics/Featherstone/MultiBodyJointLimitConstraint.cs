

namespace BulletSharp
{
	public class MultiBodyJointLimitConstraint : MultiBodyConstraint
	{
        /// <summary>
        /// There should only be one of these per joint link.
        /// </summary>
        /// <param name="lower">Radians</param>
        /// <param name="upper">Radians</param>
		public MultiBodyJointLimitConstraint(MultiBody body, int link, float lower,
			float upper)
			: base(UnsafeNativeMethods.btMultiBodyJointLimitConstraint_new(body.Native, link, lower,
				upper), body, body)
		{
		}
	}
}
