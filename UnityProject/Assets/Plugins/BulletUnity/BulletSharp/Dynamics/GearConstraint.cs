using System.Runtime.InteropServices;
using BulletSharp.Math;


namespace BulletSharp
{
	public class GearConstraint : TypedConstraint
	{
		public GearConstraint(RigidBody rigidBodyA, RigidBody rigidBodyB, Vector3 axisInA,
			Vector3 axisInB, float ratio = 1.0f)
			: base(UnsafeNativeMethods.btGearConstraint_new(rigidBodyA.Native, rigidBodyB.Native,
				ref axisInA, ref axisInB, ratio))
		{
			_rigidBodyA = rigidBodyA;
			_rigidBodyB = rigidBodyB;
		}

		public Vector3 AxisA
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btGearConstraint_getAxisA(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btGearConstraint_setAxisA(Native, ref value);
		}

		public Vector3 AxisB
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btGearConstraint_getAxisB(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btGearConstraint_setAxisB(Native, ref value);
		}

		public float Ratio
		{
			get => UnsafeNativeMethods.btGearConstraint_getRatio(Native);
			set => UnsafeNativeMethods.btGearConstraint_setRatio(Native, value);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct GearConstraintFloatData
	{
		public TypedConstraintFloatData TypedConstraintData;
		public Vector3FloatData AxisInA;
		public Vector3FloatData AxisInB;
		public float Ratio;
		public int Padding;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(GearConstraintFloatData), fieldName).ToInt32(); }
	}
}
