using BulletSharp.Math;


namespace BulletSharp
{
	public class MultiBodyFixedConstraint : MultiBodyConstraint
	{
		public MultiBodyFixedConstraint(MultiBody body, int link, RigidBody bodyB,
			Vector3 pivotInA, Vector3 pivotInB, Matrix frameInA, Matrix frameInB)
			: base(UnsafeNativeMethods.btMultiBodyFixedConstraint_new(body.Native, link, bodyB.Native,
				ref pivotInA, ref pivotInB, ref frameInA, ref frameInB), body, null)
		{
		}

		public MultiBodyFixedConstraint(MultiBody bodyA, int linkA, MultiBody bodyB,
			int linkB, Vector3 pivotInA, Vector3 pivotInB, Matrix frameInA, Matrix frameInB)
			: base(UnsafeNativeMethods.btMultiBodyFixedConstraint_new2(bodyA.Native, linkA, bodyB.Native,
				linkB, ref pivotInA, ref pivotInB, ref frameInA, ref frameInB), bodyA, bodyB)
		{
		}

		public Matrix FrameInA
		{
			get
			{
				Matrix value;
				UnsafeNativeMethods.btMultiBodyFixedConstraint_getFrameInA(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btMultiBodyFixedConstraint_setFrameInA(Native, ref value);
		}

		public Matrix FrameInB
		{
			get
			{
				Matrix value;
				UnsafeNativeMethods.btMultiBodyFixedConstraint_getFrameInB(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btMultiBodyFixedConstraint_setFrameInB(Native, ref value);
		}

		public Vector3 PivotInA
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btMultiBodyFixedConstraint_getPivotInA(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btMultiBodyFixedConstraint_setPivotInA(Native, ref value);
		}

		public Vector3 PivotInB
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btMultiBodyFixedConstraint_getPivotInB(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btMultiBodyFixedConstraint_setPivotInB(Native, ref value);
		}
	}
}
