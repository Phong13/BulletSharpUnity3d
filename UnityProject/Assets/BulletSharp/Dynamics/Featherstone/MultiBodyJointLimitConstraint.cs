using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class MultiBodyJointLimitConstraint : MultiBodyConstraint
	{
		internal MultiBodyJointLimitConstraint(IntPtr native)
			: base(native)
		{
		}

		public MultiBodyJointLimitConstraint(MultiBody body, int link, float lower, float upper)
			: base(btMultiBodyJointLimitConstraint_new(body._native, link, lower, upper))
		{
            _multiBodyA = body;
            _multiBodyB = body;
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultiBodyJointLimitConstraint_new(IntPtr body, int link, float lower, float upper);
	}
}
