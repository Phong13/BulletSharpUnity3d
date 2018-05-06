

namespace BulletSharp
{
	public class MultiBodyJointMotor : MultiBodyConstraint
	{
		public MultiBodyJointMotor(MultiBody body, int link, float desiredVelocity,
			float maxMotorImpulse)
			: base(UnsafeNativeMethods.btMultiBodyJointMotor_new(body.Native, link, desiredVelocity,
				maxMotorImpulse), body, body)
		{
		}

		public MultiBodyJointMotor(MultiBody body, int link, int linkDoF, float desiredVelocity,
			float maxMotorImpulse)
			: base(UnsafeNativeMethods.btMultiBodyJointMotor_new2(body.Native, link, linkDoF, desiredVelocity,
				maxMotorImpulse), body, body)
		{
		}

		public void SetVelocityTarget(float velTarget)
		{
			UnsafeNativeMethods.btMultiBodyJointMotor_setVelocityTarget(Native, velTarget);
		}

        public void SetVelocityTarget(float velTarget, float kd)
        {
            UnsafeNativeMethods.btMultiBodyJointMotor_setVelocityTarget2(Native, velTarget,kd);
        }

        public void SetPositionTarget(float velTarget)
        {
            UnsafeNativeMethods.btMultiBodyJointMotor_setPositionTarget(Native, velTarget);
        }

        public void SetPositionTarget(float velTarget, float kp)
        {
            UnsafeNativeMethods.btMultiBodyJointMotor_setPositionTarget2(Native, velTarget, kp);
        }
	}
}
