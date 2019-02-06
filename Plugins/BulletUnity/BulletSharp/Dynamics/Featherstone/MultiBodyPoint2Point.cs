using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class MultiBodyPoint2Point : MultiBodyConstraint
	{
		internal MultiBodyPoint2Point(IntPtr native)
			: base(native)
		{
		}

		public MultiBodyPoint2Point(MultiBody body, int link, RigidBody bodyB, Vector3 pivotInA, Vector3 pivotInB)
			: base(btMultiBodyPoint2Point_new(body._native, link, bodyB._native, ref pivotInA, ref pivotInB))
		{
            _multiBodyA = body;
		}

		public MultiBodyPoint2Point(MultiBody bodyA, int linkA, MultiBody bodyB, int linkB, Vector3 pivotInA, Vector3 pivotInB)
			: base(btMultiBodyPoint2Point_new2(bodyA._native, linkA, bodyB._native, linkB, ref pivotInA, ref pivotInB))
		{
            _multiBodyA = bodyA;
            _multiBodyB = bodyB;
		}

		public Vector3 PivotInB
		{
			get
			{
				Vector3 value;
				btMultiBodyPoint2Point_getPivotInB(_native, out value);
				return value;
			}
			set { btMultiBodyPoint2Point_setPivotInB(_native, ref value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultiBodyPoint2Point_new(IntPtr body, int link, IntPtr bodyB, [In] ref Vector3 pivotInA, [In] ref Vector3 pivotInB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultiBodyPoint2Point_new2(IntPtr bodyA, int linkA, IntPtr bodyB, int linkB, [In] ref Vector3 pivotInA, [In] ref Vector3 pivotInB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodyPoint2Point_getPivotInB(IntPtr obj, [Out] out Vector3 pivotInB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodyPoint2Point_setPivotInB(IntPtr obj, [In] ref Vector3 pivotInB);
	}
}
