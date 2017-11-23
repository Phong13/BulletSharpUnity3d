using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class MultiBodyJointMotor : MultiBodyConstraint
	{
		public MultiBodyJointMotor(MultiBody body, int link, float desiredVelocity,
			float maxMotorImpulse)
			: base(btMultiBodyJointMotor_new(body.Native, link, desiredVelocity,
				maxMotorImpulse), body, body)
		{
		}

		public MultiBodyJointMotor(MultiBody body, int link, int linkDoF, float desiredVelocity,
			float maxMotorImpulse)
			: base(btMultiBodyJointMotor_new2(body.Native, link, linkDoF, desiredVelocity,
				maxMotorImpulse), body, body)
		{
		}

		public void SetVelocityTarget(float velTarget)
		{
			btMultiBodyJointMotor_setVelocityTarget(Native, velTarget);
		}
	}
}
