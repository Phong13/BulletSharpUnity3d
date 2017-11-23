using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class Hinge2Constraint : Generic6DofSpring2Constraint
	{
		public Hinge2Constraint(RigidBody rigidBodyA, RigidBody rigidBodyB, Vector3 anchor,
			Vector3 axis1, Vector3 axis2)
			: base(btHinge2Constraint_new(rigidBodyA.Native, rigidBodyB.Native,
				ref anchor, ref axis1, ref axis2))
		{
			_rigidBodyA = rigidBodyA;
			_rigidBodyB = rigidBodyB;
		}

		public void SetLowerLimit(float ang1min)
		{
			btHinge2Constraint_setLowerLimit(Native, ang1min);
		}

		public void SetUpperLimit(float ang1max)
		{
			btHinge2Constraint_setUpperLimit(Native, ang1max);
		}

		public Vector3 Anchor
		{
			get
			{
				Vector3 value;
				btHinge2Constraint_getAnchor(Native, out value);
				return value;
			}
		}

		public Vector3 Anchor2
		{
			get
			{
				Vector3 value;
				btHinge2Constraint_getAnchor2(Native, out value);
				return value;
			}
		}

		public float Angle1 => btHinge2Constraint_getAngle1(Native);

		public float Angle2 => btHinge2Constraint_getAngle2(Native);

		public Vector3 Axis1
		{
			get
			{
				Vector3 value;
				btHinge2Constraint_getAxis1(Native, out value);
				return value;
			}
		}

		public Vector3 Axis2
		{
			get
			{
				Vector3 value;
				btHinge2Constraint_getAxis2(Native, out value);
				return value;
			}
		}
	}
}
