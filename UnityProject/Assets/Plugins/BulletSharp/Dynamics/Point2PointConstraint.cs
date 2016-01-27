using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	[Flags]
	public enum Point2PointFlags
	{
		None = 0,
		Erp = 1,
		Cfm = 2
	}

	public class ConstraintSetting
	{
		internal IntPtr _native;

		internal ConstraintSetting(IntPtr native)
		{
			_native = native;
		}

		public float Damping
		{
			get { return btConstraintSetting_getDamping(_native); }
			set { btConstraintSetting_setDamping(_native, value); }
		}

		public float ImpulseClamp
		{
			get { return btConstraintSetting_getImpulseClamp(_native); }
			set { btConstraintSetting_setImpulseClamp(_native, value); }
		}

		public float Tau
		{
			get { return btConstraintSetting_getTau(_native); }
			set { btConstraintSetting_setTau(_native, value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btConstraintSetting_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btConstraintSetting_getDamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btConstraintSetting_getImpulseClamp(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btConstraintSetting_getTau(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConstraintSetting_setDamping(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConstraintSetting_setImpulseClamp(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConstraintSetting_setTau(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConstraintSetting_delete(IntPtr obj);
	}

	public class Point2PointConstraint : TypedConstraint
	{
		public Point2PointConstraint(RigidBody rigidBodyA, RigidBody rigidBodyB, Vector3 pivotInA, Vector3 pivotInB)
			: base(btPoint2PointConstraint_new(rigidBodyA._native, rigidBodyB._native, ref pivotInA, ref pivotInB))
		{
			_rigidBodyA = rigidBodyA;
			_rigidBodyB = rigidBodyB;
		}

		public Point2PointConstraint(RigidBody rigidBodyA, Vector3 pivotInA)
			: base(btPoint2PointConstraint_new2(rigidBodyA._native, ref pivotInA))
		{
			_rigidBodyA = rigidBodyA;
            _rigidBodyB = GetFixedBody();
		}

		public void GetInfo1NonVirtual(ConstraintInfo1 info)
		{
			btPoint2PointConstraint_getInfo1NonVirtual(_native, info._native);
		}

		public void GetInfo2NonVirtual(ConstraintInfo2 info, Matrix body0Trans, Matrix body1Trans)
		{
			btPoint2PointConstraint_getInfo2NonVirtual(_native, info._native, ref body0Trans, ref body1Trans);
		}

		public void UpdateRhs(float timeStep)
		{
			btPoint2PointConstraint_updateRHS(_native, timeStep);
		}

		public Point2PointFlags Flags
		{
			get { return btPoint2PointConstraint_getFlags(_native); }
		}

		public Vector3 PivotInA
		{
			get
			{
				Vector3 value;
				btPoint2PointConstraint_getPivotInA(_native, out value);
				return value;
			}
            set { btPoint2PointConstraint_setPivotA(_native, ref value); }
		}

		public Vector3 PivotInB
		{
			get
			{
				Vector3 value;
				btPoint2PointConstraint_getPivotInB(_native, out value);
				return value;
			}
            set { btPoint2PointConstraint_setPivotB(_native, ref value); }
		}

		public ConstraintSetting Setting
		{
            get { return new ConstraintSetting(btPoint2PointConstraint_getSetting(_native)); }
		}

		public bool UseSolveConstraintObsolete
		{
			get { return btPoint2PointConstraint_getUseSolveConstraintObsolete(_native); }
			set { btPoint2PointConstraint_setUseSolveConstraintObsolete(_native, value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btPoint2PointConstraint_new(IntPtr rbA, IntPtr rbB, [In] ref Vector3 pivotInA, [In] ref Vector3 pivotInB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btPoint2PointConstraint_new2(IntPtr rbA, [In] ref Vector3 pivotInA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern Point2PointFlags btPoint2PointConstraint_getFlags(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPoint2PointConstraint_getInfo1NonVirtual(IntPtr obj, IntPtr info);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPoint2PointConstraint_getInfo2NonVirtual(IntPtr obj, IntPtr info, [In] ref Matrix body0_trans, [In] ref Matrix body1_trans);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPoint2PointConstraint_getPivotInA(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPoint2PointConstraint_getPivotInB(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btPoint2PointConstraint_getSetting(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btPoint2PointConstraint_getUseSolveConstraintObsolete(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPoint2PointConstraint_setPivotA(IntPtr obj, [In] ref Vector3 pivotA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPoint2PointConstraint_setPivotB(IntPtr obj, [In] ref Vector3 pivotB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPoint2PointConstraint_setUseSolveConstraintObsolete(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPoint2PointConstraint_updateRHS(IntPtr obj, float timeStep);
	}

    [StructLayout(LayoutKind.Sequential)]
    internal struct Point2PointConstraintFloatData
    {
        public TypedConstraintFloatData TypedConstraintData;
        public Vector3FloatData PivotInA;
        public Vector3FloatData PivotInB;

        public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(Point2PointConstraintFloatData), fieldName).ToInt32(); }
    }
}
