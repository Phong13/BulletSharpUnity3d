using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;


namespace BulletSharp
{
	[Flags]
	public enum SixDofFlags
	{
		None = 0,
		CfmNormal = 1,
		CfmStop = 2,
		ErpStop = 4
	}

	public class RotationalLimitMotor : IDisposable
	{
		internal IntPtr Native;
		bool _preventDelete;

		internal RotationalLimitMotor(IntPtr native, bool preventDelete)
		{
			Native = native;
			_preventDelete = preventDelete;
		}

		public RotationalLimitMotor()
		{
			Native = UnsafeNativeMethods.btRotationalLimitMotor_new();
		}

		public RotationalLimitMotor(RotationalLimitMotor limitMotor)
		{
			Native = UnsafeNativeMethods.btRotationalLimitMotor_new2(limitMotor.Native);
		}

		public bool NeedApplyTorques()
		{
			return UnsafeNativeMethods.btRotationalLimitMotor_needApplyTorques(Native);
		}

		public float SolveAngularLimitsRef(float timeStep, ref Vector3 axis, float jacDiagABInv,
			RigidBody body0, RigidBody body1)
		{
			return UnsafeNativeMethods.btRotationalLimitMotor_solveAngularLimits(Native, timeStep, ref axis,
				jacDiagABInv, body0.Native, body1.Native);
		}

		public float SolveAngularLimits(float timeStep, Vector3 axis, float jacDiagABInv,
			RigidBody body0, RigidBody body1)
		{
			return UnsafeNativeMethods.btRotationalLimitMotor_solveAngularLimits(Native, timeStep, ref axis,
				jacDiagABInv, body0.Native, body1.Native);
		}

		public int TestLimitValue(float testValue)
		{
			return UnsafeNativeMethods.btRotationalLimitMotor_testLimitValue(Native, testValue);
		}

		public float AccumulatedImpulse
		{
			get { return  UnsafeNativeMethods.btRotationalLimitMotor_getAccumulatedImpulse(Native);}
			set {  UnsafeNativeMethods.btRotationalLimitMotor_setAccumulatedImpulse(Native, value);}
		}

		public float Bounce
		{
			get { return  UnsafeNativeMethods.btRotationalLimitMotor_getBounce(Native);}
			set {  UnsafeNativeMethods.btRotationalLimitMotor_setBounce(Native, value);}
		}

		public int CurrentLimit
		{
			get { return  UnsafeNativeMethods.btRotationalLimitMotor_getCurrentLimit(Native);}
			set {  UnsafeNativeMethods.btRotationalLimitMotor_setCurrentLimit(Native, value);}
		}

		public float CurrentLimitError
		{
			get { return  UnsafeNativeMethods.btRotationalLimitMotor_getCurrentLimitError(Native);}
			set {  UnsafeNativeMethods.btRotationalLimitMotor_setCurrentLimitError(Native, value);}
		}

		public float CurrentPosition
		{
			get { return  UnsafeNativeMethods.btRotationalLimitMotor_getCurrentPosition(Native);}
			set {  UnsafeNativeMethods.btRotationalLimitMotor_setCurrentPosition(Native, value);}
		}

		public float Damping
		{
			get { return  UnsafeNativeMethods.btRotationalLimitMotor_getDamping(Native);}
			set {  UnsafeNativeMethods.btRotationalLimitMotor_setDamping(Native, value);}
		}

		public bool EnableMotor
		{
			get { return  UnsafeNativeMethods.btRotationalLimitMotor_getEnableMotor(Native);}
			set {  UnsafeNativeMethods.btRotationalLimitMotor_setEnableMotor(Native, value);}
		}

		public float HiLimit
		{
			get { return  UnsafeNativeMethods.btRotationalLimitMotor_getHiLimit(Native);}
			set {  UnsafeNativeMethods.btRotationalLimitMotor_setHiLimit(Native, value);}
		}

		public bool IsLimited{ get { return  UnsafeNativeMethods.btRotationalLimitMotor_isLimited(Native);} }
		public float LimitSoftness
		{
			get { return  UnsafeNativeMethods.btRotationalLimitMotor_getLimitSoftness(Native);}
			set {  UnsafeNativeMethods.btRotationalLimitMotor_setLimitSoftness(Native, value);}
		}

		public float LoLimit
		{
			get { return  UnsafeNativeMethods.btRotationalLimitMotor_getLoLimit(Native);}
			set {  UnsafeNativeMethods.btRotationalLimitMotor_setLoLimit(Native, value);}
		}

		public float MaxLimitForce
		{
			get { return  UnsafeNativeMethods.btRotationalLimitMotor_getMaxLimitForce(Native);}
			set {  UnsafeNativeMethods.btRotationalLimitMotor_setMaxLimitForce(Native, value);}
		}

		public float MaxMotorForce
		{
			get { return  UnsafeNativeMethods.btRotationalLimitMotor_getMaxMotorForce(Native);}
			set {  UnsafeNativeMethods.btRotationalLimitMotor_setMaxMotorForce(Native, value);}
		}

		public float NormalCfm
		{
			get { return  UnsafeNativeMethods.btRotationalLimitMotor_getNormalCFM(Native);}
			set {  UnsafeNativeMethods.btRotationalLimitMotor_setNormalCFM(Native, value);}
		}

		public float StopCfm
		{
			get { return  UnsafeNativeMethods.btRotationalLimitMotor_getStopCFM(Native);}
			set {  UnsafeNativeMethods.btRotationalLimitMotor_setStopCFM(Native, value);}
		}

		public float StopErp
		{
			get { return  UnsafeNativeMethods.btRotationalLimitMotor_getStopERP(Native);}
			set {  UnsafeNativeMethods.btRotationalLimitMotor_setStopERP(Native, value);}
		}

		public float TargetVelocity
		{
			get { return  UnsafeNativeMethods.btRotationalLimitMotor_getTargetVelocity(Native);}
			set {  UnsafeNativeMethods.btRotationalLimitMotor_setTargetVelocity(Native, value);}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				if (!_preventDelete)
				{
					UnsafeNativeMethods.btRotationalLimitMotor_delete(Native);
				}
				Native = IntPtr.Zero;
			}
		}

		~RotationalLimitMotor()
		{
			Dispose(false);
		}
	}

	public class TranslationalLimitMotor : IDisposable
	{
		internal IntPtr Native;
		private bool _preventDelete;

		internal TranslationalLimitMotor(IntPtr native, bool preventDelete)
		{
			Native = native;
			_preventDelete = preventDelete;
		}

		public TranslationalLimitMotor()
		{
			Native = UnsafeNativeMethods.btTranslationalLimitMotor_new();
		}

		public TranslationalLimitMotor(TranslationalLimitMotor other)
		{
			Native = UnsafeNativeMethods.btTranslationalLimitMotor_new2(other.Native);
		}

		public bool IsLimited(int limitIndex)
		{
			return UnsafeNativeMethods.btTranslationalLimitMotor_isLimited(Native, limitIndex);
		}

		public bool NeedApplyForce(int limitIndex)
		{
			return UnsafeNativeMethods.btTranslationalLimitMotor_needApplyForce(Native, limitIndex);
		}

		public float SolveLinearAxisRef(float timeStep, float jacDiagABInv, RigidBody body1,
			ref Vector3 pointInA, RigidBody body2, ref Vector3 pointInB, int limitIndex, ref Vector3 axisNormalOnA,
			ref Vector3 anchorPos)
		{
			return UnsafeNativeMethods.btTranslationalLimitMotor_solveLinearAxis(Native, timeStep, jacDiagABInv,
				body1.Native, ref pointInA, body2.Native, ref pointInB, limitIndex,
				ref axisNormalOnA, ref anchorPos);
		}

		public float SolveLinearAxis(float timeStep, float jacDiagABInv, RigidBody body1,
			Vector3 pointInA, RigidBody body2, Vector3 pointInB, int limitIndex, Vector3 axisNormalOnA,
			Vector3 anchorPos)
		{
			return UnsafeNativeMethods.btTranslationalLimitMotor_solveLinearAxis(Native, timeStep, jacDiagABInv,
				body1.Native, ref pointInA, body2.Native, ref pointInB, limitIndex,
				ref axisNormalOnA, ref anchorPos);
		}

		public int TestLimitValue(int limitIndex, float testValue)
		{
			return UnsafeNativeMethods.btTranslationalLimitMotor_testLimitValue(Native, limitIndex,
				testValue);
		}

		public Vector3 AccumulatedImpulse
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btTranslationalLimitMotor_getAccumulatedImpulse(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btTranslationalLimitMotor_setAccumulatedImpulse(Native, ref value);}
		}
		/*
		public IntArray CurrentLimit
		{
			get { return new IntArray(UnsafeNativeMethods.btTranslationalLimitMotor_getCurrentLimit(Native), 3); }
		}
		*/
		public Vector3 CurrentLimitError
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btTranslationalLimitMotor_getCurrentLimitError(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btTranslationalLimitMotor_setCurrentLimitError(Native, ref value);}
		}

		public Vector3 CurrentLinearDiff
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btTranslationalLimitMotor_getCurrentLinearDiff(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btTranslationalLimitMotor_setCurrentLinearDiff(Native, ref value);}
		}

		public float Damping
		{
			get { return  UnsafeNativeMethods.btTranslationalLimitMotor_getDamping(Native);}
			set {  UnsafeNativeMethods.btTranslationalLimitMotor_setDamping(Native, value);}
		}
		/*
		public bool EnableMotor
		{
			get { return UnsafeNativeMethods.btTranslationalLimitMotor_getEnableMotor(_native); }
		}
		*/
		public float LimitSoftness
		{
			get { return  UnsafeNativeMethods.btTranslationalLimitMotor_getLimitSoftness(Native);}
			set {  UnsafeNativeMethods.btTranslationalLimitMotor_setLimitSoftness(Native, value);}
		}

		public Vector3 LowerLimit
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btTranslationalLimitMotor_getLowerLimit(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btTranslationalLimitMotor_setLowerLimit(Native, ref value);}
		}

		public Vector3 MaxMotorForce
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btTranslationalLimitMotor_getMaxMotorForce(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btTranslationalLimitMotor_setMaxMotorForce(Native, ref value);}
		}

		public Vector3 NormalCfm
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btTranslationalLimitMotor_getNormalCFM(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btTranslationalLimitMotor_setNormalCFM(Native, ref value);}
		}

		public float Restitution
		{
			get { return  UnsafeNativeMethods.btTranslationalLimitMotor_getRestitution(Native);}
			set {  UnsafeNativeMethods.btTranslationalLimitMotor_setRestitution(Native, value);}
		}

		public Vector3 StopCfm
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btTranslationalLimitMotor_getStopCFM(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btTranslationalLimitMotor_setStopCFM(Native, ref value);}
		}

		public Vector3 StopErp
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btTranslationalLimitMotor_getStopERP(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btTranslationalLimitMotor_setStopERP(Native, ref value);}
		}

		public Vector3 TargetVelocity
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btTranslationalLimitMotor_getTargetVelocity(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btTranslationalLimitMotor_setTargetVelocity(Native, ref value);}
		}

		public Vector3 UpperLimit
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btTranslationalLimitMotor_getUpperLimit(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btTranslationalLimitMotor_setUpperLimit(Native, ref value);}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				if (!_preventDelete)
				{
					UnsafeNativeMethods.btTranslationalLimitMotor_delete(Native);
				}
				Native = IntPtr.Zero;
			}
		}

		~TranslationalLimitMotor()
		{
			Dispose(false);
		}
	}

	public class Generic6DofConstraint : TypedConstraint
	{
		private RotationalLimitMotor[] _angularLimits = new RotationalLimitMotor[3];
		private TranslationalLimitMotor _linearLimits;

		internal Generic6DofConstraint(IntPtr native)
			: base(native)
		{
		}

		public Generic6DofConstraint(RigidBody rigidBodyA, RigidBody rigidBodyB,
			Matrix frameInA, Matrix frameInB, bool useLinearReferenceFrameA)
			: base(UnsafeNativeMethods.btGeneric6DofConstraint_new(rigidBodyA.Native, rigidBodyB.Native,
				ref frameInA, ref frameInB, useLinearReferenceFrameA))
		{
			_rigidBodyA = rigidBodyA;
			_rigidBodyB = rigidBodyB;
		}

		public Generic6DofConstraint(RigidBody rigidBodyB, Matrix frameInB, bool useLinearReferenceFrameB)
			: base(UnsafeNativeMethods.btGeneric6DofConstraint_new2(rigidBodyB.Native, ref frameInB,
				useLinearReferenceFrameB))
		{
			_rigidBodyA = GetFixedBody();
			_rigidBodyB = rigidBodyB;
		}

		public void CalcAnchorPos()
		{
			UnsafeNativeMethods.btGeneric6DofConstraint_calcAnchorPos(Native);
		}

		public void CalculateTransformsRef(ref Matrix transA, ref Matrix transB)
		{
			UnsafeNativeMethods.btGeneric6DofConstraint_calculateTransforms(Native, ref transA, ref transB);
		}

		public void CalculateTransforms(Matrix transA, Matrix transB)
		{
			UnsafeNativeMethods.btGeneric6DofConstraint_calculateTransforms(Native, ref transA, ref transB);
		}

		public void CalculateTransforms()
		{
			UnsafeNativeMethods.btGeneric6DofConstraint_calculateTransforms2(Native);
		}

		public int GetLimitMotorInfo2(RotationalLimitMotor limitMotor, Matrix transA,
			Matrix transB, Vector3 linVelA, Vector3 linVelB, Vector3 angVelA, Vector3 angVelB,
			ConstraintInfo2 info, int row, ref Vector3 ax1, int rotational, int rotAllowed = 0)
		{
			return UnsafeNativeMethods.btGeneric6DofConstraint_get_limit_motor_info2(Native, limitMotor.Native,
				ref transA, ref transB, ref linVelA, ref linVelB, ref angVelA, ref angVelB,
				info._native, row, ref ax1, rotational, rotAllowed);
		}

		public float GetAngle(int axisIndex)
		{
			return UnsafeNativeMethods.btGeneric6DofConstraint_getAngle(Native, axisIndex);
		}

		public Vector3 GetAxis(int axisIndex)
		{
			Vector3 value;
			UnsafeNativeMethods.btGeneric6DofConstraint_getAxis(Native, axisIndex, out value);
			return value;
		}

		public void GetInfo1NonVirtual(ConstraintInfo1 info)
		{
			UnsafeNativeMethods.btGeneric6DofConstraint_getInfo1NonVirtual(Native, info._native);
		}

		public void GetInfo2NonVirtual(ConstraintInfo2 info, Matrix transA, Matrix transB,
			Vector3 linVelA, Vector3 linVelB, Vector3 angVelA, Vector3 angVelB)
		{
			UnsafeNativeMethods.btGeneric6DofConstraint_getInfo2NonVirtual(Native, info._native, ref transA,
				ref transB, ref linVelA, ref linVelB, ref angVelA, ref angVelB);
		}

		public float GetRelativePivotPosition(int axisIndex)
		{
			return UnsafeNativeMethods.btGeneric6DofConstraint_getRelativePivotPosition(Native, axisIndex);
		}

		public RotationalLimitMotor GetRotationalLimitMotor(int index)
		{
			if (_angularLimits[index] == null)
			{
				_angularLimits[index] = new RotationalLimitMotor(UnsafeNativeMethods.btGeneric6DofConstraint_getRotationalLimitMotor(Native, index), true);
			}
			return _angularLimits[index];
		}

		public bool IsLimited(int limitIndex)
		{
			return UnsafeNativeMethods.btGeneric6DofConstraint_isLimited(Native, limitIndex);
		}

		public void SetAxisRef(ref Vector3 axis1, ref Vector3 axis2)
		{
			UnsafeNativeMethods.btGeneric6DofConstraint_setAxis(Native, ref axis1, ref axis2);
		}

		public void SetAxis(Vector3 axis1, Vector3 axis2)
		{
			UnsafeNativeMethods.btGeneric6DofConstraint_setAxis(Native, ref axis1, ref axis2);
		}

		public void SetFramesRef(ref Matrix frameA, ref Matrix frameB)
		{
			UnsafeNativeMethods.btGeneric6DofConstraint_setFrames(Native, ref frameA, ref frameB);
		}

		public void SetFrames(Matrix frameA, Matrix frameB)
		{
			UnsafeNativeMethods.btGeneric6DofConstraint_setFrames(Native, ref frameA, ref frameB);
		}

		public void SetLimit(int axis, float lo, float hi)
		{
			UnsafeNativeMethods.btGeneric6DofConstraint_setLimit(Native, axis, lo, hi);
		}

		public bool TestAngularLimitMotor(int axisIndex)
		{
			return UnsafeNativeMethods.btGeneric6DofConstraint_testAngularLimitMotor(Native, axisIndex);
		}

		public void UpdateRhs(float timeStep)
		{
			UnsafeNativeMethods.btGeneric6DofConstraint_updateRHS(Native, timeStep);
		}

		public Vector3 AngularLowerLimit
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btGeneric6DofConstraint_getAngularLowerLimit(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btGeneric6DofConstraint_setAngularLowerLimit(Native, ref value);}
		}

		public Vector3 AngularUpperLimit
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btGeneric6DofConstraint_getAngularUpperLimit(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btGeneric6DofConstraint_setAngularUpperLimit(Native, ref value);}
		}

		public Matrix CalculatedTransformA
		{
			get
			{
				Matrix value;
				UnsafeNativeMethods.btGeneric6DofConstraint_getCalculatedTransformA(Native, out value);
				return value;
			}
		}

		public Matrix CalculatedTransformB
		{
			get
			{
				Matrix value;
				UnsafeNativeMethods.btGeneric6DofConstraint_getCalculatedTransformB(Native, out value);
				return value;
			}
		}

		public SixDofFlags Flags{ get { return  UnsafeNativeMethods.btGeneric6DofConstraint_getFlags(Native);} }

		public Matrix FrameOffsetA
		{
			get
			{
				Matrix value;
				UnsafeNativeMethods.btGeneric6DofConstraint_getFrameOffsetA(Native, out value);
				return value;
			}
		}

		public Matrix FrameOffsetB
		{
			get
			{
				Matrix value;
				UnsafeNativeMethods.btGeneric6DofConstraint_getFrameOffsetB(Native, out value);
				return value;
			}
		}

		public Vector3 LinearLowerLimit
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btGeneric6DofConstraint_getLinearLowerLimit(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btGeneric6DofConstraint_setLinearLowerLimit(Native, ref value);}
		}

		public Vector3 LinearUpperLimit
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btGeneric6DofConstraint_getLinearUpperLimit(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btGeneric6DofConstraint_setLinearUpperLimit(Native, ref value);}
		}

		public TranslationalLimitMotor TranslationalLimitMotor
		{
			get
			{
				if (_linearLimits == null)
				{
					_linearLimits = new TranslationalLimitMotor(UnsafeNativeMethods.btGeneric6DofConstraint_getTranslationalLimitMotor(Native), true);
				}
				return _linearLimits;
			}
		}

		public bool UseFrameOffset
		{
			get { return  UnsafeNativeMethods.btGeneric6DofConstraint_getUseFrameOffset(Native);}
			set {  UnsafeNativeMethods.btGeneric6DofConstraint_setUseFrameOffset(Native, value);}
		}

		public bool UseLinearReferenceFrameA
		{
			get { return  UnsafeNativeMethods.btGeneric6DofConstraint_getUseLinearReferenceFrameA(Native);}
			set {  UnsafeNativeMethods.btGeneric6DofConstraint_setUseLinearReferenceFrameA(Native, value);}
		}

		public bool UseSolveConstraintObsolete
		{
			get { return  UnsafeNativeMethods.btGeneric6DofConstraint_getUseSolveConstraintObsolete(Native);}
			set {  UnsafeNativeMethods.btGeneric6DofConstraint_setUseSolveConstraintObsolete(Native, value);}
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct Generic6DofConstraintFloatData
	{
		public TypedConstraintFloatData TypedConstraintData;
		public TransformFloatData RigidBodyAFrame;
		public TransformFloatData RigidBodyBFrame;
		public Vector3FloatData LinearUpperLimit;
		public Vector3FloatData LinearLowerLimit;
		public Vector3FloatData AngularUpperLimit;
		public Vector3FloatData AngularLowerLimit;
		public int UseLinearReferenceFrameA;
		public int UseOffsetForConstraintFrame;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(Generic6DofConstraintFloatData), fieldName).ToInt32(); }
	}
}
