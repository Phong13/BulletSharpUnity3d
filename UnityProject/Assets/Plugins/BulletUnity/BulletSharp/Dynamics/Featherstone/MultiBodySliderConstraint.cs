using BulletSharp.Math;


namespace BulletSharp
{
	public class MultiBodySliderConstraint : MultiBodyConstraint
	{
		public MultiBodySliderConstraint(MultiBody body, int link, RigidBody bodyB,
			Vector3 pivotInA, Vector3 pivotInB, Matrix frameInA, Matrix frameInB, Vector3 jointAxis)
			: base(UnsafeNativeMethods.btMultiBodySliderConstraint_new(body.Native, link, bodyB.Native,
				ref pivotInA, ref pivotInB, ref frameInA, ref frameInB, ref jointAxis), body, null)
		{
		}

		public MultiBodySliderConstraint(MultiBody bodyA, int linkA, MultiBody bodyB,
			int linkB, Vector3 pivotInA, Vector3 pivotInB, Matrix frameInA, Matrix frameInB,
			Vector3 jointAxis)
			: base(UnsafeNativeMethods.btMultiBodySliderConstraint_new2(bodyA.Native, linkA, bodyB.Native,
				linkB, ref pivotInA, ref pivotInB, ref frameInA, ref frameInB, ref jointAxis), bodyA, bodyB)
		{
		}

		public Matrix FrameInA
		{
			get
			{
				Matrix value;
				UnsafeNativeMethods.btMultiBodySliderConstraint_getFrameInA(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btMultiBodySliderConstraint_setFrameInA(Native, ref value);
		}

		public Matrix FrameInB
		{
			get
			{
				Matrix value;
				UnsafeNativeMethods.btMultiBodySliderConstraint_getFrameInB(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btMultiBodySliderConstraint_setFrameInB(Native, ref value);
		}

		public Vector3 JointAxis
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btMultiBodySliderConstraint_getJointAxis(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btMultiBodySliderConstraint_setJointAxis(Native, ref value);
		}

		public Vector3 PivotInA
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btMultiBodySliderConstraint_getPivotInA(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btMultiBodySliderConstraint_setPivotInA(Native, ref value);
		}

		public Vector3 PivotInB
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btMultiBodySliderConstraint_getPivotInB(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btMultiBodySliderConstraint_setPivotInB(Native, ref value);
		}
	}
}
