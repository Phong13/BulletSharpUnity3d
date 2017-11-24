using System;
using System.Runtime.InteropServices;
using BulletSharp.Math;


namespace BulletSharp
{
	[Flags]
	public enum ConeTwistFlags
	{
		None = 0,
		LinearCfm = 1,
		LinearErp = 2,
		AngularCfm = 4
	}

	public class ConeTwistConstraint : TypedConstraint
	{
		public ConeTwistConstraint(RigidBody rigidBodyA, RigidBody rigidBodyB, Matrix rigidBodyAFrame,
			Matrix rigidBodyBFrame)
			: base(UnsafeNativeMethods.btConeTwistConstraint_new(rigidBodyA.Native, rigidBodyB.Native,
				ref rigidBodyAFrame, ref rigidBodyBFrame))
		{
			_rigidBodyA = rigidBodyA;
			_rigidBodyB = rigidBodyB;
		}

		public ConeTwistConstraint(RigidBody rigidBodyA, Matrix rigidBodyAFrame)
			: base(UnsafeNativeMethods.btConeTwistConstraint_new2(rigidBodyA.Native, ref rigidBodyAFrame))
		{
			_rigidBodyA = rigidBodyA;
			_rigidBodyB = GetFixedBody();
		}

		public void CalcAngleInfo()
		{
			UnsafeNativeMethods.btConeTwistConstraint_calcAngleInfo(Native);
		}

		public void CalcAngleInfo2Ref(ref Matrix transA, ref Matrix transB, ref Matrix invInertiaWorldA,
			Matrix invInertiaWorldB)
		{
			UnsafeNativeMethods.btConeTwistConstraint_calcAngleInfo2(Native, ref transA, ref transB,
				ref invInertiaWorldA, ref invInertiaWorldB);
		}

		public void CalcAngleInfo2(Matrix transA, Matrix transB, Matrix invInertiaWorldA,
			Matrix invInertiaWorldB)
		{
			UnsafeNativeMethods.btConeTwistConstraint_calcAngleInfo2(Native, ref transA, ref transB,
				ref invInertiaWorldA, ref invInertiaWorldB);
		}

		public void EnableMotor(bool b)
		{
			UnsafeNativeMethods.btConeTwistConstraint_enableMotor(Native, b);
		}

		public void GetInfo2NonVirtualRef(ConstraintInfo2 info, ref Matrix transA, ref Matrix transB,
			Matrix invInertiaWorldA, Matrix invInertiaWorldB)
		{
			UnsafeNativeMethods.btConeTwistConstraint_getInfo2NonVirtual(Native, info._native, ref transA,
				ref transB, ref invInertiaWorldA, ref invInertiaWorldB);
		}

		public void GetInfo2NonVirtual(ConstraintInfo2 info, Matrix transA, Matrix transB,
			Matrix invInertiaWorldA, Matrix invInertiaWorldB)
		{
			UnsafeNativeMethods.btConeTwistConstraint_getInfo2NonVirtual(Native, info._native, ref transA,
				ref transB, ref invInertiaWorldA, ref invInertiaWorldB);
		}

		public float GetLimit(int limitIndex)
		{
			return UnsafeNativeMethods.btConeTwistConstraint_getLimit(Native, limitIndex);
		}

		public Vector3 GetPointForAngle(float fAngleInRadians, float fLength)
		{
			Vector3 value;
			UnsafeNativeMethods.btConeTwistConstraint_GetPointForAngle(Native, fAngleInRadians, fLength,
				out value);
			return value;
		}

		public void SetFramesRef(ref Matrix frameA, ref Matrix frameB)
		{
			UnsafeNativeMethods.btConeTwistConstraint_setFrames(Native, ref frameA, ref frameB);
		}

		public void SetFrames(Matrix frameA, Matrix frameB)
		{
			UnsafeNativeMethods.btConeTwistConstraint_setFrames(Native, ref frameA, ref frameB);
		}

		public void SetLimit(int limitIndex, float limitValue)
		{
			UnsafeNativeMethods.btConeTwistConstraint_setLimit(Native, limitIndex, limitValue);
		}

		public void SetLimit(float swingSpan1, float swingSpan2, float twistSpan,
			float softness = 1.0f, float biasFactor = 0.3f, float relaxationFactor = 1.0f)
		{
			UnsafeNativeMethods.btConeTwistConstraint_setLimit2(Native, swingSpan1, swingSpan2, twistSpan,
				softness, biasFactor, relaxationFactor);
		}

		public void SetMaxMotorImpulseNormalized(float maxMotorImpulse)
		{
			UnsafeNativeMethods.btConeTwistConstraint_setMaxMotorImpulseNormalized(Native, maxMotorImpulse);
		}

		public void SetMotorTargetInConstraintSpace(Quaternion q)
		{
			UnsafeNativeMethods.btConeTwistConstraint_setMotorTargetInConstraintSpace(Native, ref q);
		}

		public void UpdateRhs(float timeStep)
		{
			UnsafeNativeMethods.btConeTwistConstraint_updateRHS(Native, timeStep);
		}

		public Matrix AFrame
		{
			get
			{
				Matrix value;
				UnsafeNativeMethods.btConeTwistConstraint_getAFrame(Native, out value);
				return value;
			}
		}

		public bool AngularOnly
		{
			get { return  UnsafeNativeMethods.btConeTwistConstraint_getAngularOnly(Native);}
			set {  UnsafeNativeMethods.btConeTwistConstraint_setAngularOnly(Native, value);}
		}

		public Matrix BFrame
		{
			get
			{
				Matrix value;
				UnsafeNativeMethods.btConeTwistConstraint_getBFrame(Native, out value);
				return value;
			}
		}

		public float BiasFactor{ get { return  UnsafeNativeMethods.btConeTwistConstraint_getBiasFactor(Native);} }

		public float Damping
		{
			get { return  UnsafeNativeMethods.btConeTwistConstraint_getDamping(Native);}
			set {  UnsafeNativeMethods.btConeTwistConstraint_setDamping(Native, value);}
		}

		public float FixThresh
		{
			get { return  UnsafeNativeMethods.btConeTwistConstraint_getFixThresh(Native);}
			set {  UnsafeNativeMethods.btConeTwistConstraint_setFixThresh(Native, value);}
		}

		public ConeTwistFlags Flags{ get { return  UnsafeNativeMethods.btConeTwistConstraint_getFlags(Native);} }

		public Matrix FrameOffsetA
		{
			get
			{
				Matrix value;
				UnsafeNativeMethods.btConeTwistConstraint_getFrameOffsetA(Native, out value);
				return value;
			}
		}

		public Matrix FrameOffsetB
		{
			get
			{
				Matrix value;
				UnsafeNativeMethods.btConeTwistConstraint_getFrameOffsetB(Native, out value);
				return value;
			}
		}

		public bool IsMaxMotorImpulseNormalized{ get { return  UnsafeNativeMethods.btConeTwistConstraint_isMaxMotorImpulseNormalized(Native);} }

		public bool IsMotorEnabled{ get { return  UnsafeNativeMethods.btConeTwistConstraint_isMotorEnabled(Native);} }

		public bool IsPastSwingLimit{ get { return  UnsafeNativeMethods.btConeTwistConstraint_isPastSwingLimit(Native);} }

		public float LimitSoftness{ get { return  UnsafeNativeMethods.btConeTwistConstraint_getLimitSoftness(Native);} }

		public float MaxMotorImpulse
		{
			get { return  UnsafeNativeMethods.btConeTwistConstraint_getMaxMotorImpulse(Native);}
			set {  UnsafeNativeMethods.btConeTwistConstraint_setMaxMotorImpulse(Native, value);}
		}

		public Quaternion MotorTarget
		{
			get
			{
				Quaternion value;
				UnsafeNativeMethods.btConeTwistConstraint_getMotorTarget(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btConeTwistConstraint_setMotorTarget(Native, ref value);}
		}

		public float RelaxationFactor{ get { return  UnsafeNativeMethods.btConeTwistConstraint_getRelaxationFactor(Native);} }
		public int SolveSwingLimit{ get { return  UnsafeNativeMethods.btConeTwistConstraint_getSolveSwingLimit(Native);} }
		public int SolveTwistLimit{ get { return  UnsafeNativeMethods.btConeTwistConstraint_getSolveTwistLimit(Native);} }
		public float SwingSpan1{ get { return  UnsafeNativeMethods.btConeTwistConstraint_getSwingSpan1(Native);} }
		public float SwingSpan2{ get { return  UnsafeNativeMethods.btConeTwistConstraint_getSwingSpan2(Native);} }
		public float TwistAngle{ get { return  UnsafeNativeMethods.btConeTwistConstraint_getTwistAngle(Native);} }
		public float TwistLimitSign{ get { return  UnsafeNativeMethods.btConeTwistConstraint_getTwistLimitSign(Native);} }
		public float TwistSpan{ get { return  UnsafeNativeMethods.btConeTwistConstraint_getTwistSpan(Native);} }
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct ConeTwistConstraintFloatData
	{
		public TypedConstraintFloatData TypedConstraintData;
		public TransformFloatData RigidBodyAFrame;
		public TransformFloatData RigidBodyBFrame;
		public float SwingSpan1;
		public float SwingSpan2;
		public float TwistSpan;
		public float LimitSoftness;
		public float BiasFactor;
		public float RelaxationFactor;
		public float Damping;
		public int Pad;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(ConeTwistConstraintFloatData), fieldName).ToInt32(); }
	}
}
