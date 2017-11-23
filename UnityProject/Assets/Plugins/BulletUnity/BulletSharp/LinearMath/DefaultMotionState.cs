using System;
using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class DefaultMotionState : MotionState
	{
		public DefaultMotionState()
			: base(btDefaultMotionState_new())
		{
		}

		public DefaultMotionState(Matrix startTrans)
			: base(btDefaultMotionState_new2(ref startTrans))
		{
		}

		public DefaultMotionState(Matrix startTrans, Matrix centerOfMassOffset)
			: base(btDefaultMotionState_new3(ref startTrans, ref centerOfMassOffset))
		{
		}

		public override void GetWorldTransform(out Matrix worldTrans)
		{
			btMotionState_getWorldTransform(_native, out worldTrans);
		}

		public override void SetWorldTransform(ref Matrix worldTrans)
		{
			btMotionState_setWorldTransform(_native, ref worldTrans);
		}

		public Matrix CenterOfMassOffset
		{
			get
			{
				Matrix value;
				btDefaultMotionState_getCenterOfMassOffset(_native, out value);
				return value;
			}
			set => btDefaultMotionState_setCenterOfMassOffset(_native, ref value);
		}

		public Matrix GraphicsWorldTrans
		{
			get
			{
				Matrix value;
				btDefaultMotionState_getGraphicsWorldTrans(_native, out value);
				return value;
			}
			set => btDefaultMotionState_setGraphicsWorldTrans(_native, ref value);
		}

		public Matrix StartWorldTrans
		{
			get
			{
				Matrix value;
				btDefaultMotionState_getStartWorldTrans(_native, out value);
				return value;
			}
			set => btDefaultMotionState_setStartWorldTrans(_native, ref value);
		}

		public IntPtr UserPointer
		{
			get => btDefaultMotionState_getUserPointer(_native);
			set => btDefaultMotionState_setUserPointer(_native, value);
		}
	}
}
