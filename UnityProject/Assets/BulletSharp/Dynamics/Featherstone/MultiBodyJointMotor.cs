using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class MultiBodyJointMotor : MultiBodyConstraint
	{
		internal MultiBodyJointMotor(IntPtr native)
			: base(native)
		{
		}

		public MultiBodyJointMotor(MultiBody body, int link, float desiredVelocity, float maxMotorImpulse)
			: base(btMultiBodyJointMotor_new(body._native, link, desiredVelocity, maxMotorImpulse))
		{
            _multiBodyA = body;
            _multiBodyB = body;
		}

		public MultiBodyJointMotor(MultiBody body, int link, int linkDoF, float desiredVelocity, float maxMotorImpulse)
			: base(btMultiBodyJointMotor_new2(body._native, link, linkDoF, desiredVelocity, maxMotorImpulse))
		{
            _multiBodyA = body;
            _multiBodyB = body;
		}

		public void SetVelocityTarget(float velTarget)
		{
			btMultiBodyJointMotor_setVelocityTarget(_native, velTarget);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultiBodyJointMotor_new(IntPtr body, int link, float desiredVelocity, float maxMotorImpulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultiBodyJointMotor_new2(IntPtr body, int link, int linkDoF, float desiredVelocity, float maxMotorImpulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodyJointMotor_setVelocityTarget(IntPtr obj, float velTarget);
	}
}
