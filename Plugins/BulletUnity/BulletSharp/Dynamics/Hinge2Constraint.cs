using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class Hinge2Constraint : Generic6DofSpring2Constraint
	{
		public Hinge2Constraint(RigidBody rigidBodyA, RigidBody rigidBodyB, Vector3 anchor, Vector3 axis1, Vector3 axis2)
            : base(btHinge2Constraint_new(rigidBodyA._native, rigidBodyB._native, ref anchor, ref axis1, ref axis2))
		{
			_rigidBodyA = rigidBodyA;
			_rigidBodyB = rigidBodyB;
		}

		public void SetLowerLimit(float ang1min)
		{
			btHinge2Constraint_setLowerLimit(_native, ang1min);
		}

		public void SetUpperLimit(float ang1max)
		{
			btHinge2Constraint_setUpperLimit(_native, ang1max);
		}

		public Vector3 Anchor
		{
			get
			{
				Vector3 value;
				btHinge2Constraint_getAnchor(_native, out value);
				return value;
			}
		}

		public Vector3 Anchor2
		{
			get
			{
				Vector3 value;
				btHinge2Constraint_getAnchor2(_native, out value);
				return value;
			}
		}

		public float Angle1
		{
			get { return btHinge2Constraint_getAngle1(_native); }
		}

		public float Angle2
		{
			get { return btHinge2Constraint_getAngle2(_native); }
		}

		public Vector3 Axis1
		{
			get
			{
				Vector3 value;
				btHinge2Constraint_getAxis1(_native, out value);
				return value;
			}
		}

		public Vector3 Axis2
		{
			get
			{
				Vector3 value;
				btHinge2Constraint_getAxis2(_native, out value);
				return value;
			}
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btHinge2Constraint_new(IntPtr rbA, IntPtr rbB, [In] ref Vector3 anchor, [In] ref Vector3 axis1, [In] ref Vector3 axis2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btHinge2Constraint_getAnchor(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btHinge2Constraint_getAnchor2(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btHinge2Constraint_getAngle1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btHinge2Constraint_getAngle2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btHinge2Constraint_getAxis1(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btHinge2Constraint_getAxis2(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btHinge2Constraint_setLowerLimit(IntPtr obj, float ang1min);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btHinge2Constraint_setUpperLimit(IntPtr obj, float ang1max);
	}
}
