using System;
using BulletSharp.Math;


namespace BulletSharp
{
	public class DefaultMotionState : MotionState
	{
		public DefaultMotionState()
			: base(UnsafeNativeMethods.btDefaultMotionState_new())
		{
		}

		public DefaultMotionState(Matrix startTrans)
			: base(UnsafeNativeMethods.btDefaultMotionState_new2(ref startTrans))
		{
		}

		public DefaultMotionState(Matrix startTrans, Matrix centerOfMassOffset)
			: base(UnsafeNativeMethods.btDefaultMotionState_new3(ref startTrans, ref centerOfMassOffset))
		{
		}

		public override void GetWorldTransform(out Matrix worldTrans)
		{
			UnsafeNativeMethods.btMotionState_getWorldTransform(_native, out worldTrans);
		}

		public override void SetWorldTransform(ref Matrix worldTrans)
		{
			UnsafeNativeMethods.btMotionState_setWorldTransform(_native, ref worldTrans);
		}

		public Matrix CenterOfMassOffset
		{
			get
			{
				Matrix value;
				UnsafeNativeMethods.btDefaultMotionState_getCenterOfMassOffset(_native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btDefaultMotionState_setCenterOfMassOffset(_native, ref value);
		}

		public Matrix GraphicsWorldTrans
		{
			get
			{
				Matrix value;
				UnsafeNativeMethods.btDefaultMotionState_getGraphicsWorldTrans(_native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btDefaultMotionState_setGraphicsWorldTrans(_native, ref value);
		}

		public Matrix StartWorldTrans
		{
			get
			{
				Matrix value;
				UnsafeNativeMethods.btDefaultMotionState_getStartWorldTrans(_native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btDefaultMotionState_setStartWorldTrans(_native, ref value);
		}

		public IntPtr UserPointer
		{
			get => UnsafeNativeMethods.btDefaultMotionState_getUserPointer(_native);
			set => UnsafeNativeMethods.btDefaultMotionState_setUserPointer(_native, value);
		}
	}
}
