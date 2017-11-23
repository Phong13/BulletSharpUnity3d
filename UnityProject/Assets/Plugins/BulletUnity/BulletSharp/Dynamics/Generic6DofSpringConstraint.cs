using System.Runtime.InteropServices;
using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class Generic6DofSpringConstraint : Generic6DofConstraint
	{
		public Generic6DofSpringConstraint(RigidBody rigidBodyA, RigidBody rigidBodyB,
			Matrix frameInA, Matrix frameInB, bool useLinearReferenceFrameA)
			: base(btGeneric6DofSpringConstraint_new(rigidBodyA.Native, rigidBodyB.Native,
				ref frameInA, ref frameInB, useLinearReferenceFrameA))
		{
			_rigidBodyA = rigidBodyA;
			_rigidBodyB = rigidBodyB;
		}

		public Generic6DofSpringConstraint(RigidBody rigidBodyB, Matrix frameInB,
			bool useLinearReferenceFrameB)
			: base(btGeneric6DofSpringConstraint_new2(rigidBodyB.Native, ref frameInB,
				useLinearReferenceFrameB))
		{
			_rigidBodyA = GetFixedBody();
			_rigidBodyB = rigidBodyB;
		}

		public void EnableSpring(int index, bool onOff)
		{
			btGeneric6DofSpringConstraint_enableSpring(Native, index, onOff);
		}

		public float GetDamping(int index)
		{
			return btGeneric6DofSpringConstraint_getDamping(Native, index);
		}

		public float GetEquilibriumPoint(int index)
		{
			return btGeneric6DofSpringConstraint_getEquilibriumPoint(Native, index);
		}

		public float GetStiffness(int index)
		{
			return btGeneric6DofSpringConstraint_getStiffness(Native, index);
		}

		public bool IsSpringEnabled(int index)
		{
			return btGeneric6DofSpringConstraint_isSpringEnabled(Native, index);
		}

		public void SetDamping(int index, float damping)
		{
			btGeneric6DofSpringConstraint_setDamping(Native, index, damping);
		}

		public void SetEquilibriumPoint()
		{
			btGeneric6DofSpringConstraint_setEquilibriumPoint(Native);
		}

		public void SetEquilibriumPoint(int index)
		{
			btGeneric6DofSpringConstraint_setEquilibriumPoint2(Native, index);
		}

		public void SetEquilibriumPoint(int index, float val)
		{
			btGeneric6DofSpringConstraint_setEquilibriumPoint3(Native, index, val);
		}

		public void SetStiffness(int index, float stiffness)
		{
			btGeneric6DofSpringConstraint_setStiffness(Native, index, stiffness);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal unsafe struct Generic6DofSpringConstraintFloatData
	{
		public Generic6DofConstraintFloatData SixDofData;
		public fixed int SpringEnabled[6];
		public fixed float EquilibriumPoint[6];
		public fixed float SpringStiffness[6];
		public fixed float SpringDamping[6];

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(Generic6DofSpringConstraintFloatData), fieldName).ToInt32(); }
	}
}
