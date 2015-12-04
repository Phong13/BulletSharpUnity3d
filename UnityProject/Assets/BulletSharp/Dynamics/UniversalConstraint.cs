using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class UniversalConstraint : Generic6DofConstraint
	{
		public UniversalConstraint(RigidBody rigidBodyA, RigidBody rigidBodyB, Vector3 anchor, Vector3 axis1, Vector3 axis2)
			: base(btUniversalConstraint_new(rigidBodyA._native, rigidBodyB._native, ref anchor, ref axis1, ref axis2))
		{
			_rigidBodyA = rigidBodyA;
			_rigidBodyB = rigidBodyB;
		}

		public void SetLowerLimit(float ang1min, float ang2min)
		{
			btUniversalConstraint_setLowerLimit(_native, ang1min, ang2min);
		}

		public void SetUpperLimit(float ang1max, float ang2max)
		{
			btUniversalConstraint_setUpperLimit(_native, ang1max, ang2max);
		}

		public Vector3 Anchor
		{
			get
			{
				Vector3 value;
				btUniversalConstraint_getAnchor(_native, out value);
				return value;
			}
		}

		public Vector3 Anchor2
		{
			get
			{
				Vector3 value;
				btUniversalConstraint_getAnchor2(_native, out value);
				return value;
			}
		}

		public float Angle1
		{
			get { return btUniversalConstraint_getAngle1(_native); }
		}

		public float Angle2
		{
			get { return btUniversalConstraint_getAngle2(_native); }
		}

		public Vector3 Axis1
		{
			get
			{
				Vector3 value;
				btUniversalConstraint_getAxis1(_native, out value);
				return value;
			}
		}

		public Vector3 Axis2
		{
			get
			{
				Vector3 value;
				btUniversalConstraint_getAxis2(_native, out value);
				return value;
			}
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btUniversalConstraint_new(IntPtr rbA, IntPtr rbB, [In] ref Vector3 anchor, [In] ref Vector3 axis1, [In] ref Vector3 axis2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btUniversalConstraint_getAnchor(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btUniversalConstraint_getAnchor2(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btUniversalConstraint_getAngle1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btUniversalConstraint_getAngle2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btUniversalConstraint_getAxis1(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btUniversalConstraint_getAxis2(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btUniversalConstraint_setLowerLimit(IntPtr obj, float ang1min, float ang2min);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btUniversalConstraint_setUpperLimit(IntPtr obj, float ang1max, float ang2max);
	}
}
