using BulletSharp.Math;


namespace BulletSharp
{
	public class UniversalConstraint : Generic6DofConstraint
	{
		public UniversalConstraint(RigidBody rigidBodyA, RigidBody rigidBodyB, Vector3 anchor,
			Vector3 axis1, Vector3 axis2)
			: base(UnsafeNativeMethods.btUniversalConstraint_new(rigidBodyA.Native, rigidBodyB.Native,
				ref anchor, ref axis1, ref axis2))
		{
			_rigidBodyA = rigidBodyA;
			_rigidBodyB = rigidBodyB;
		}

		public void SetLowerLimit(float ang1min, float ang2min)
		{
			UnsafeNativeMethods.btUniversalConstraint_setLowerLimit(Native, ang1min, ang2min);
		}

		public void SetUpperLimit(float ang1max, float ang2max)
		{
			UnsafeNativeMethods.btUniversalConstraint_setUpperLimit(Native, ang1max, ang2max);
		}

		public Vector3 Anchor
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btUniversalConstraint_getAnchor(Native, out value);
				return value;
			}
		}

		public Vector3 Anchor2
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btUniversalConstraint_getAnchor2(Native, out value);
				return value;
			}
		}

		public float Angle1{ get { return  UnsafeNativeMethods.btUniversalConstraint_getAngle1(Native);} }

		public float Angle2{ get { return  UnsafeNativeMethods.btUniversalConstraint_getAngle2(Native);} }

		public Vector3 Axis1
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btUniversalConstraint_getAxis1(Native, out value);
				return value;
			}
		}

		public Vector3 Axis2
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btUniversalConstraint_getAxis2(Native, out value);
				return value;
			}
		}
	}
}
