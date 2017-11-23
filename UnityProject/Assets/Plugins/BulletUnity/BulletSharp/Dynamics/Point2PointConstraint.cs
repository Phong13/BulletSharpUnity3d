using System;
using System.Runtime.InteropServices;
using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

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
		internal IntPtr Native;

		internal ConstraintSetting(IntPtr native)
		{
			Native = native;
		}

		public float Damping
		{
			get => btConstraintSetting_getDamping(Native);
			set => btConstraintSetting_setDamping(Native, value);
		}

		public float ImpulseClamp
		{
			get => btConstraintSetting_getImpulseClamp(Native);
			set => btConstraintSetting_setImpulseClamp(Native, value);
		}

		public float Tau
		{
			get => btConstraintSetting_getTau(Native);
			set => btConstraintSetting_setTau(Native, value);
		}
	}

	public class Point2PointConstraint : TypedConstraint
	{
		public Point2PointConstraint(RigidBody rigidBodyA, RigidBody rigidBodyB,
			Vector3 pivotInA, Vector3 pivotInB)
			: base(btPoint2PointConstraint_new(rigidBodyA.Native, rigidBodyB.Native,
				ref pivotInA, ref pivotInB))
		{
			_rigidBodyA = rigidBodyA;
			_rigidBodyB = rigidBodyB;
		}

		public Point2PointConstraint(RigidBody rigidBodyA, Vector3 pivotInA)
			: base(btPoint2PointConstraint_new2(rigidBodyA.Native, ref pivotInA))
		{
			_rigidBodyA = rigidBodyA;
			_rigidBodyB = GetFixedBody();
		}

		public void GetInfo1NonVirtual(ConstraintInfo1 info)
		{
			btPoint2PointConstraint_getInfo1NonVirtual(Native, info._native);
		}

		public void GetInfo2NonVirtual(ConstraintInfo2 info, Matrix body0Trans, Matrix body1Trans)
		{
			btPoint2PointConstraint_getInfo2NonVirtual(Native, info._native, ref body0Trans,
				ref body1Trans);
		}

		public void UpdateRhs(float timeStep)
		{
			btPoint2PointConstraint_updateRHS(Native, timeStep);
		}

		public Point2PointFlags Flags => btPoint2PointConstraint_getFlags(Native);

		public Vector3 PivotInA
		{
			get
			{
				Vector3 value;
				btPoint2PointConstraint_getPivotInA(Native, out value);
				return value;
			}
			set => btPoint2PointConstraint_setPivotA(Native, ref value);
		}

		public Vector3 PivotInB
		{
			get
			{
				Vector3 value;
				btPoint2PointConstraint_getPivotInB(Native, out value);
				return value;
			}
			set => btPoint2PointConstraint_setPivotB(Native, ref value);
		}

		public ConstraintSetting Setting => new ConstraintSetting(btPoint2PointConstraint_getSetting(Native));

		public bool UseSolveConstraintObsolete
		{
			get => btPoint2PointConstraint_getUseSolveConstraintObsolete(Native);
			set => btPoint2PointConstraint_setUseSolveConstraintObsolete(Native, value);
		}
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
