using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;


namespace BulletSharp
{
	public enum RotateOrder
	{
		XYZ,
		XZY,
		YXZ,
		YZX,
		ZXY,
		ZYX
	}

	public class RotationalLimitMotor2 : IDisposable
	{
		internal IntPtr _native;
		private bool _preventDelete;

		internal RotationalLimitMotor2(IntPtr native, bool preventDelete)
		{
			_native = native;
			_preventDelete = preventDelete;
		}

		public RotationalLimitMotor2()
		{
			_native = UnsafeNativeMethods.btRotationalLimitMotor2_new();
		}

		public RotationalLimitMotor2(RotationalLimitMotor2 limitMotor)
		{
			_native = UnsafeNativeMethods.btRotationalLimitMotor2_new2(limitMotor._native);
		}

		public void TestLimitValue(float testValue)
		{
			UnsafeNativeMethods.btRotationalLimitMotor2_testLimitValue(_native, testValue);
		}

		public float Bounce
		{
			get { return  UnsafeNativeMethods.btRotationalLimitMotor2_getBounce(_native);}
			set {  UnsafeNativeMethods.btRotationalLimitMotor2_setBounce(_native, value);}
		}

		public int CurrentLimit
		{
			get { return  UnsafeNativeMethods.btRotationalLimitMotor2_getCurrentLimit(_native);}
			set {  UnsafeNativeMethods.btRotationalLimitMotor2_setCurrentLimit(_native, value);}
		}

		public float CurrentLimitError
		{
			get { return  UnsafeNativeMethods.btRotationalLimitMotor2_getCurrentLimitError(_native);}
			set {  UnsafeNativeMethods.btRotationalLimitMotor2_setCurrentLimitError(_native, value);}
		}

		public float CurrentLimitErrorHi
		{
			get { return  UnsafeNativeMethods.btRotationalLimitMotor2_getCurrentLimitErrorHi(_native);}
			set {  UnsafeNativeMethods.btRotationalLimitMotor2_setCurrentLimitErrorHi(_native, value);}
		}

		public float CurrentPosition
		{
			get { return  UnsafeNativeMethods.btRotationalLimitMotor2_getCurrentPosition(_native);}
			set {  UnsafeNativeMethods.btRotationalLimitMotor2_setCurrentPosition(_native, value);}
		}

		public bool EnableMotor
		{
			get { return  UnsafeNativeMethods.btRotationalLimitMotor2_getEnableMotor(_native);}
			set {  UnsafeNativeMethods.btRotationalLimitMotor2_setEnableMotor(_native, value);}
		}

		public bool EnableSpring
		{
			get { return  UnsafeNativeMethods.btRotationalLimitMotor2_getEnableSpring(_native);}
			set {  UnsafeNativeMethods.btRotationalLimitMotor2_setEnableSpring(_native, value);}
		}

		public float EquilibriumPoint
		{
			get { return  UnsafeNativeMethods.btRotationalLimitMotor2_getEquilibriumPoint(_native);}
			set {  UnsafeNativeMethods.btRotationalLimitMotor2_setEquilibriumPoint(_native, value);}
		}

		public float HiLimit
		{
			get { return  UnsafeNativeMethods.btRotationalLimitMotor2_getHiLimit(_native);}
			set {  UnsafeNativeMethods.btRotationalLimitMotor2_setHiLimit(_native, value);}
		}

		public bool IsLimited{ get { return  UnsafeNativeMethods.btRotationalLimitMotor2_isLimited(_native);} }

		public float LoLimit
		{
			get { return  UnsafeNativeMethods.btRotationalLimitMotor2_getLoLimit(_native);}
			set {  UnsafeNativeMethods.btRotationalLimitMotor2_setLoLimit(_native, value);}
		}

		public float MaxMotorForce
		{
			get { return  UnsafeNativeMethods.btRotationalLimitMotor2_getMaxMotorForce(_native);}
			set {  UnsafeNativeMethods.btRotationalLimitMotor2_setMaxMotorForce(_native, value);}
		}

		public float MotorCfm
		{
			get { return  UnsafeNativeMethods.btRotationalLimitMotor2_getMotorCFM(_native);}
			set {  UnsafeNativeMethods.btRotationalLimitMotor2_setMotorCFM(_native, value);}
		}

		public float MotorErp
		{
			get { return  UnsafeNativeMethods.btRotationalLimitMotor2_getMotorERP(_native);}
			set {  UnsafeNativeMethods.btRotationalLimitMotor2_setMotorERP(_native, value);}
		}

		public bool ServoMotor
		{
			get { return  UnsafeNativeMethods.btRotationalLimitMotor2_getServoMotor(_native);}
			set {  UnsafeNativeMethods.btRotationalLimitMotor2_setServoMotor(_native, value);}
		}

		public float ServoTarget
		{
			get { return  UnsafeNativeMethods.btRotationalLimitMotor2_getServoTarget(_native);}
			set {  UnsafeNativeMethods.btRotationalLimitMotor2_setServoTarget(_native, value);}
		}

		public float SpringDamping
		{
			get { return  UnsafeNativeMethods.btRotationalLimitMotor2_getSpringDamping(_native);}
			set {  UnsafeNativeMethods.btRotationalLimitMotor2_setSpringDamping(_native, value);}
		}

		public bool SpringDampingLimited
		{
			get { return  UnsafeNativeMethods.btRotationalLimitMotor2_getSpringDampingLimited(_native);}
			set {  UnsafeNativeMethods.btRotationalLimitMotor2_setSpringDampingLimited(_native, value);}
		}

		public float SpringStiffness
		{
			get { return  UnsafeNativeMethods.btRotationalLimitMotor2_getSpringStiffness(_native);}
			set {  UnsafeNativeMethods.btRotationalLimitMotor2_setSpringStiffness(_native, value);}
		}

		public bool SpringStiffnessLimited
		{
			get { return  UnsafeNativeMethods.btRotationalLimitMotor2_getSpringStiffnessLimited(_native);}
			set {  UnsafeNativeMethods.btRotationalLimitMotor2_setSpringStiffnessLimited(_native, value);}
		}

		public float StopCfm
		{
			get { return  UnsafeNativeMethods.btRotationalLimitMotor2_getStopCFM(_native);}
			set {  UnsafeNativeMethods.btRotationalLimitMotor2_setStopCFM(_native, value);}
		}

		public float StopErp
		{
			get { return  UnsafeNativeMethods.btRotationalLimitMotor2_getStopERP(_native);}
			set {  UnsafeNativeMethods.btRotationalLimitMotor2_setStopERP(_native, value);}
		}

		public float TargetVelocity
		{
			get { return  UnsafeNativeMethods.btRotationalLimitMotor2_getTargetVelocity(_native);}
			set {  UnsafeNativeMethods.btRotationalLimitMotor2_setTargetVelocity(_native, value);}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				if (!_preventDelete)
				{
					UnsafeNativeMethods.btRotationalLimitMotor2_delete(_native);
				}
				_native = IntPtr.Zero;
			}
		}

		~RotationalLimitMotor2()
		{
			Dispose(false);
		}
	}

	public class TranslationalLimitMotor2 : IDisposable
	{
		internal IntPtr _native;
		bool _preventDelete;

		internal TranslationalLimitMotor2(IntPtr native, bool preventDelete)
		{
			_native = native;
			_preventDelete = preventDelete;
		}

		public TranslationalLimitMotor2()
		{
			_native = UnsafeNativeMethods.btTranslationalLimitMotor2_new();
		}

		public TranslationalLimitMotor2(TranslationalLimitMotor2 other)
		{
			_native = UnsafeNativeMethods.btTranslationalLimitMotor2_new2(other._native);
		}

		public bool IsLimited(int limitIndex)
		{
			return UnsafeNativeMethods.btTranslationalLimitMotor2_isLimited(_native, limitIndex);
		}

		public void TestLimitValue(int limitIndex, float testValue)
		{
			UnsafeNativeMethods.btTranslationalLimitMotor2_testLimitValue(_native, limitIndex, testValue);
		}

		public Vector3 Bounce
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btTranslationalLimitMotor2_getBounce(_native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btTranslationalLimitMotor2_setBounce(_native, ref value);}
		}
		/*
		public IntArray CurrentLimit
		{
			get { return new IntArray(UnsafeNativeMethods.btTranslationalLimitMotor2_getCurrentLimit(_native), 3); }
		}
		*/
		public Vector3 CurrentLimitError
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btTranslationalLimitMotor2_getCurrentLimitError(_native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btTranslationalLimitMotor2_setCurrentLimitError(_native, ref value);}
		}

		public Vector3 CurrentLimitErrorHi
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btTranslationalLimitMotor2_getCurrentLimitErrorHi(_native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btTranslationalLimitMotor2_setCurrentLimitErrorHi(_native, ref value);}
		}

		public Vector3 CurrentLinearDiff
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btTranslationalLimitMotor2_getCurrentLinearDiff(_native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btTranslationalLimitMotor2_setCurrentLinearDiff(_native, ref value);}
		}
		/*
		public BoolArray EnableMotor
		{
			get { return new BoolArray(UnsafeNativeMethods.btTranslationalLimitMotor2_getEnableMotor(_native), 3); }
		}

		public BoolArray EnableSpring
		{
			get { return new BoolArray(UnsafeNativeMethods.btTranslationalLimitMotor2_getEnableSpring(_native), 3); }
		}
		*/
		public Vector3 EquilibriumPoint
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btTranslationalLimitMotor2_getEquilibriumPoint(_native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btTranslationalLimitMotor2_setEquilibriumPoint(_native, ref value);}
		}

		public Vector3 LowerLimit
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btTranslationalLimitMotor2_getLowerLimit(_native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btTranslationalLimitMotor2_setLowerLimit(_native, ref value);}
		}

		public Vector3 MaxMotorForce
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btTranslationalLimitMotor2_getMaxMotorForce(_native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btTranslationalLimitMotor2_setMaxMotorForce(_native, ref value);}
		}

		public Vector3 MotorCFM
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btTranslationalLimitMotor2_getMotorCFM(_native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btTranslationalLimitMotor2_setMotorCFM(_native, ref value);}
		}

		public Vector3 MotorERP
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btTranslationalLimitMotor2_getMotorERP(_native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btTranslationalLimitMotor2_setMotorERP(_native, ref value);}
		}
		/*
		public BoolArray ServoMotor
		{
			get { return new BoolArray(UnsafeNativeMethods.btTranslationalLimitMotor2_getServoMotor(_native)); }
		}
		*/
		public Vector3 ServoTarget
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btTranslationalLimitMotor2_getServoTarget(_native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btTranslationalLimitMotor2_setServoTarget(_native, ref value);}
		}

		public Vector3 SpringDamping
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btTranslationalLimitMotor2_getSpringDamping(_native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btTranslationalLimitMotor2_setSpringDamping(_native, ref value);}
		}
		/*
		public BoolArray SpringDampingLimited
		{
			get { return UnsafeNativeMethods.btTranslationalLimitMotor2_getSpringDampingLimited(_native); }
		}
		*/
		public Vector3 SpringStiffness
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btTranslationalLimitMotor2_getSpringStiffness(_native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btTranslationalLimitMotor2_setSpringStiffness(_native, ref value);}
		}
		/*
		public BoolArray SpringStiffnessLimited
		{
			get { return UnsafeNativeMethods.btTranslationalLimitMotor2_getSpringStiffnessLimited(_native); }
		}
		*/
		public Vector3 StopCfm
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btTranslationalLimitMotor2_getStopCFM(_native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btTranslationalLimitMotor2_setStopCFM(_native, ref value);}
		}

		public Vector3 StopEep
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btTranslationalLimitMotor2_getStopERP(_native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btTranslationalLimitMotor2_setStopERP(_native, ref value);}
		}

		public Vector3 TargetVelocity
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btTranslationalLimitMotor2_getTargetVelocity(_native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btTranslationalLimitMotor2_setTargetVelocity(_native, ref value);}
		}

		public Vector3 UpperLimit
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btTranslationalLimitMotor2_getUpperLimit(_native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btTranslationalLimitMotor2_setUpperLimit(_native, ref value);}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				if (!_preventDelete)
				{
					UnsafeNativeMethods.btTranslationalLimitMotor2_delete(_native);
				}
				_native = IntPtr.Zero;
			}
		}

		~TranslationalLimitMotor2()
		{
			Dispose(false);
		}
	}

	public class Generic6DofSpring2Constraint : TypedConstraint
	{
		private RotationalLimitMotor2[] _angularLimits = new RotationalLimitMotor2[3];
		private TranslationalLimitMotor2 _linearLimits;

		internal Generic6DofSpring2Constraint(IntPtr native)
			: base(native)
		{
		}

		public Generic6DofSpring2Constraint(RigidBody rigidBodyA, RigidBody rigidBodyB,
			Matrix frameInA, Matrix frameInB, RotateOrder rotOrder = RotateOrder.XYZ)
			: base(UnsafeNativeMethods.btGeneric6DofSpring2Constraint_new(rigidBodyA.Native, rigidBodyB.Native,
				ref frameInA, ref frameInB, rotOrder))
		{
			_rigidBodyA = rigidBodyA;
			_rigidBodyB = rigidBodyB;
		}

		public Generic6DofSpring2Constraint(RigidBody rigidBodyB, Matrix frameInB,
			RotateOrder rotOrder = RotateOrder.XYZ)
			: base(UnsafeNativeMethods.btGeneric6DofSpring2Constraint_new2(rigidBodyB.Native, ref frameInB,
				rotOrder))
		{
			_rigidBodyA = GetFixedBody();
			_rigidBodyB = rigidBodyB;
		}

		public static float BtGetMatrixElem(Matrix mat, int index)
		{
			return UnsafeNativeMethods.btGeneric6DofSpring2Constraint_btGetMatrixElem(ref mat, index);
		}

		public void CalculateTransforms(Matrix transA, Matrix transB)
		{
			UnsafeNativeMethods.btGeneric6DofSpring2Constraint_calculateTransforms(Native, ref transA,
				ref transB);
		}

		public void CalculateTransforms()
		{
			UnsafeNativeMethods.btGeneric6DofSpring2Constraint_calculateTransforms2(Native);
		}

		public void EnableMotor(int index, bool onOff)
		{
			UnsafeNativeMethods.btGeneric6DofSpring2Constraint_enableMotor(Native, index, onOff);
		}

		public void EnableSpring(int index, bool onOff)
		{
			UnsafeNativeMethods.btGeneric6DofSpring2Constraint_enableSpring(Native, index, onOff);
		}

		public float GetAngle(int axisIndex)
		{
			return UnsafeNativeMethods.btGeneric6DofSpring2Constraint_getAngle(Native, axisIndex);
		}

		public Vector3 GetAxis(int axisIndex)
		{
			Vector3 value;
			UnsafeNativeMethods.btGeneric6DofSpring2Constraint_getAxis(Native, axisIndex, out value);
			return value;
		}

		public float GetRelativePivotPosition(int axisIndex)
		{
			return UnsafeNativeMethods.btGeneric6DofSpring2Constraint_getRelativePivotPosition(Native,
				axisIndex);
		}

		public RotationalLimitMotor2 GetRotationalLimitMotor(int index)
		{
			if (_angularLimits[index] == null)
			{
				_angularLimits[index] = new RotationalLimitMotor2(UnsafeNativeMethods.btGeneric6DofSpring2Constraint_getRotationalLimitMotor(Native, index), true);
			}
			return _angularLimits[index];
		}

		public bool IsLimited(int limitIndex)
		{
			return UnsafeNativeMethods.btGeneric6DofSpring2Constraint_isLimited(Native, limitIndex);
		}

		public static bool MatrixToEulerZXY(Matrix mat, ref Vector3 xyz)
		{
			return UnsafeNativeMethods.btGeneric6DofSpring2Constraint_matrixToEulerZXY(ref mat, ref xyz);
		}

		public static bool MatrixToEulerZYX(Matrix mat, ref Vector3 xyz)
		{
			return UnsafeNativeMethods.btGeneric6DofSpring2Constraint_matrixToEulerZYX(ref mat, ref xyz);
		}

		public static bool MatrixToEulerXZY(Matrix mat, ref Vector3 xyz)
		{
			return UnsafeNativeMethods.btGeneric6DofSpring2Constraint_matrixToEulerXZY(ref mat, ref xyz);
		}

		public static bool MatrixToEulerXYZ(Matrix mat, ref Vector3 xyz)
		{
			return UnsafeNativeMethods.btGeneric6DofSpring2Constraint_matrixToEulerXYZ(ref mat, ref xyz);
		}

		public static bool MatrixToEulerYZX(Matrix mat, ref Vector3 xyz)
		{
			return UnsafeNativeMethods.btGeneric6DofSpring2Constraint_matrixToEulerYZX(ref mat, ref xyz);
		}

		public static bool MatrixToEulerYXZ(Matrix mat, ref Vector3 xyz)
		{
			return UnsafeNativeMethods.btGeneric6DofSpring2Constraint_matrixToEulerYXZ(ref mat, ref xyz);
		}

		public void SetAxis(Vector3 axis1, Vector3 axis2)
		{
			UnsafeNativeMethods.btGeneric6DofSpring2Constraint_setAxis(Native, ref axis1, ref axis2);
		}

		public void SetBounce(int index, float bounce)
		{
			UnsafeNativeMethods.btGeneric6DofSpring2Constraint_setBounce(Native, index, bounce);
		}

		public void SetDamping(int index, float damping, bool limitIfNeeded = true)
		{
			UnsafeNativeMethods.btGeneric6DofSpring2Constraint_setDamping(Native, index, damping, limitIfNeeded);
		}

		public void SetEquilibriumPoint()
		{
			UnsafeNativeMethods.btGeneric6DofSpring2Constraint_setEquilibriumPoint(Native);
		}

		public void SetEquilibriumPoint(int index, float val)
		{
			UnsafeNativeMethods.btGeneric6DofSpring2Constraint_setEquilibriumPoint2(Native, index, val);
		}

		public void SetEquilibriumPoint(int index)
		{
			UnsafeNativeMethods.btGeneric6DofSpring2Constraint_setEquilibriumPoint3(Native, index);
		}

		public void SetFrames(Matrix frameA, Matrix frameB)
		{
			UnsafeNativeMethods.btGeneric6DofSpring2Constraint_setFrames(Native, ref frameA, ref frameB);
		}

		public void SetLimit(int axis, float lo, float hi)
		{
			UnsafeNativeMethods.btGeneric6DofSpring2Constraint_setLimit(Native, axis, lo, hi);
		}

		public void SetLimitReversed(int axis, float lo, float hi)
		{
			UnsafeNativeMethods.btGeneric6DofSpring2Constraint_setLimitReversed(Native, axis, lo, hi);
		}

		public void SetMaxMotorForce(int index, float force)
		{
			UnsafeNativeMethods.btGeneric6DofSpring2Constraint_setMaxMotorForce(Native, index, force);
		}

		public void SetServo(int index, bool onOff)
		{
			UnsafeNativeMethods.btGeneric6DofSpring2Constraint_setServo(Native, index, onOff);
		}

		public void SetServoTarget(int index, float target)
		{
			UnsafeNativeMethods.btGeneric6DofSpring2Constraint_setServoTarget(Native, index, target);
		}

		public void SetStiffness(int index, float stiffness, bool limitIfNeeded = true)
		{
			UnsafeNativeMethods.btGeneric6DofSpring2Constraint_setStiffness(Native, index, stiffness,
				limitIfNeeded);
		}

		public void SetTargetVelocity(int index, float velocity)
		{
			UnsafeNativeMethods.btGeneric6DofSpring2Constraint_setTargetVelocity(Native, index, velocity);
		}

		public Vector3 AngularLowerLimit
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btGeneric6DofSpring2Constraint_getAngularLowerLimit(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btGeneric6DofSpring2Constraint_setAngularLowerLimit(Native, ref value);}
		}

		public Vector3 AngularLowerLimitReversed
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btGeneric6DofSpring2Constraint_getAngularLowerLimitReversed(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btGeneric6DofSpring2Constraint_setAngularLowerLimitReversed(Native, ref value);}
		}

		public Vector3 AngularUpperLimit
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btGeneric6DofSpring2Constraint_getAngularUpperLimit(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btGeneric6DofSpring2Constraint_setAngularUpperLimit(Native, ref value);}
		}

		public Vector3 AngularUpperLimitReversed
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btGeneric6DofSpring2Constraint_getAngularUpperLimitReversed(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btGeneric6DofSpring2Constraint_setAngularUpperLimitReversed(Native, ref value);}
		}

		public Matrix CalculatedTransformA
		{
			get
			{
				Matrix value;
				UnsafeNativeMethods.btGeneric6DofSpring2Constraint_getCalculatedTransformA(Native, out value);
				return value;
			}
		}

		public Matrix CalculatedTransformB
		{
			get
			{
				Matrix value;
				UnsafeNativeMethods.btGeneric6DofSpring2Constraint_getCalculatedTransformB(Native, out value);
				return value;
			}
		}

		public Matrix FrameOffsetA
		{
			get
			{
				Matrix value;
				UnsafeNativeMethods.btGeneric6DofSpring2Constraint_getFrameOffsetA(Native, out value);
				return value;
			}
		}

		public Matrix FrameOffsetB
		{
			get
			{
				Matrix value;
				UnsafeNativeMethods.btGeneric6DofSpring2Constraint_getFrameOffsetB(Native, out value);
				return value;
			}
		}

		public Vector3 LinearLowerLimit
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btGeneric6DofSpring2Constraint_getLinearLowerLimit(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btGeneric6DofSpring2Constraint_setLinearLowerLimit(Native, ref value);}
		}

		public Vector3 LinearUpperLimit
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btGeneric6DofSpring2Constraint_getLinearUpperLimit(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btGeneric6DofSpring2Constraint_setLinearUpperLimit(Native, ref value);}
		}

		public RotateOrder RotationOrder
		{
			get { return  UnsafeNativeMethods.btGeneric6DofSpring2Constraint_getRotationOrder(Native);}
			set {  UnsafeNativeMethods.btGeneric6DofSpring2Constraint_setRotationOrder(Native, value);}
		}

		public TranslationalLimitMotor2 TranslationalLimitMotor
		{
			get
			{
				if (_linearLimits == null)
				{
					_linearLimits = new TranslationalLimitMotor2(UnsafeNativeMethods.btGeneric6DofSpring2Constraint_getTranslationalLimitMotor(Native), true);
				}
				return _linearLimits;
			}
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal unsafe struct Generic6DofSpring2ConstraintFloatData
	{
		public TypedConstraintFloatData TypeConstraintData;
		public TransformFloatData RigidBodyAFrame;
		public TransformFloatData RigidBodyBFrame;
		public Vector3FloatData LinearUpperLimit;
		public Vector3FloatData LinearLowerLimit;
		public Vector3FloatData LinearBounce;
		public Vector3FloatData LinearStopErp;
		public Vector3FloatData LinearStopCfm;
		public Vector3FloatData LinearMotorErp;
		public Vector3FloatData LinearMotorCfm;
		public Vector3FloatData LinearTargetVelocity;
		public Vector3FloatData LinearMaxMotorForce;
		public Vector3FloatData LinearServoTarget;
		public Vector3FloatData LinearSpringStiffness;
		public Vector3FloatData LinearSpringDamping;
		public Vector3FloatData LinearEquilibriumPoint;
		public fixed byte LinearEnableMotor[4];
		public fixed byte LinearServoMotor[4];
		public fixed byte LinearEnableSpring[4];
		public fixed byte LinearSpringStiffnessLimited[4];
		public fixed byte LinearSpringDampingLimited[4];
		public int Padding;
		public Vector3FloatData AngularUpperLimit;
		public Vector3FloatData AngularLowerLimit;
		public Vector3FloatData AngularBounce;
		public Vector3FloatData AngularStopErp;
		public Vector3FloatData AngularStopCfm;
		public Vector3FloatData AngularMotorErp;
		public Vector3FloatData AngularMotorCfm;
		public Vector3FloatData AngularTargetVelocity;
		public Vector3FloatData AngularMaxMotorForce;
		public Vector3FloatData AngularServoTarget;
		public Vector3FloatData AngularSpringStiffness;
		public Vector3FloatData AngularSpringDamping;
		public Vector3FloatData AngularEquilibriumPoint;
		public fixed byte AngularEnableMotor[4];
		public fixed byte AngularServoMotor[4];
		public fixed byte AngularEnableSpring[4];
		public fixed byte AngularSpringStiffnessLimited[4];
		public fixed byte AngularSpringDampingLimited[4];
		public int RotateOrder;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(Generic6DofSpring2ConstraintFloatData), fieldName).ToInt32(); }
	}
}
