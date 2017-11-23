using System;
using System.Runtime.InteropServices;
using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

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
			: base(btConeTwistConstraint_new(rigidBodyA.Native, rigidBodyB.Native,
				ref rigidBodyAFrame, ref rigidBodyBFrame))
		{
			_rigidBodyA = rigidBodyA;
			_rigidBodyB = rigidBodyB;
		}

		public ConeTwistConstraint(RigidBody rigidBodyA, Matrix rigidBodyAFrame)
			: base(btConeTwistConstraint_new2(rigidBodyA.Native, ref rigidBodyAFrame))
		{
			_rigidBodyA = rigidBodyA;
			_rigidBodyB = GetFixedBody();
		}

		public void CalcAngleInfo()
		{
			btConeTwistConstraint_calcAngleInfo(Native);
		}

		public void CalcAngleInfo2Ref(ref Matrix transA, ref Matrix transB, ref Matrix invInertiaWorldA,
			Matrix invInertiaWorldB)
		{
			btConeTwistConstraint_calcAngleInfo2(Native, ref transA, ref transB,
				ref invInertiaWorldA, ref invInertiaWorldB);
		}

		public void CalcAngleInfo2(Matrix transA, Matrix transB, Matrix invInertiaWorldA,
			Matrix invInertiaWorldB)
		{
			btConeTwistConstraint_calcAngleInfo2(Native, ref transA, ref transB,
				ref invInertiaWorldA, ref invInertiaWorldB);
		}

		public void EnableMotor(bool b)
		{
			btConeTwistConstraint_enableMotor(Native, b);
		}

		public void GetInfo2NonVirtualRef(ConstraintInfo2 info, ref Matrix transA, ref Matrix transB,
			Matrix invInertiaWorldA, Matrix invInertiaWorldB)
		{
			btConeTwistConstraint_getInfo2NonVirtual(Native, info._native, ref transA,
				ref transB, ref invInertiaWorldA, ref invInertiaWorldB);
		}

		public void GetInfo2NonVirtual(ConstraintInfo2 info, Matrix transA, Matrix transB,
			Matrix invInertiaWorldA, Matrix invInertiaWorldB)
		{
			btConeTwistConstraint_getInfo2NonVirtual(Native, info._native, ref transA,
				ref transB, ref invInertiaWorldA, ref invInertiaWorldB);
		}

		public float GetLimit(int limitIndex)
		{
			return btConeTwistConstraint_getLimit(Native, limitIndex);
		}

		public Vector3 GetPointForAngle(float fAngleInRadians, float fLength)
		{
			Vector3 value;
			btConeTwistConstraint_GetPointForAngle(Native, fAngleInRadians, fLength,
				out value);
			return value;
		}

		public void SetFramesRef(ref Matrix frameA, ref Matrix frameB)
		{
			btConeTwistConstraint_setFrames(Native, ref frameA, ref frameB);
		}

		public void SetFrames(Matrix frameA, Matrix frameB)
		{
			btConeTwistConstraint_setFrames(Native, ref frameA, ref frameB);
		}

		public void SetLimit(int limitIndex, float limitValue)
		{
			btConeTwistConstraint_setLimit(Native, limitIndex, limitValue);
		}

		public void SetLimit(float swingSpan1, float swingSpan2, float twistSpan,
			float softness = 1.0f, float biasFactor = 0.3f, float relaxationFactor = 1.0f)
		{
			btConeTwistConstraint_setLimit2(Native, swingSpan1, swingSpan2, twistSpan,
				softness, biasFactor, relaxationFactor);
		}

		public void SetMaxMotorImpulseNormalized(float maxMotorImpulse)
		{
			btConeTwistConstraint_setMaxMotorImpulseNormalized(Native, maxMotorImpulse);
		}

		public void SetMotorTargetInConstraintSpace(Quaternion q)
		{
			btConeTwistConstraint_setMotorTargetInConstraintSpace(Native, ref q);
		}

		public void UpdateRhs(float timeStep)
		{
			btConeTwistConstraint_updateRHS(Native, timeStep);
		}

		public Matrix AFrame
		{
			get
			{
				Matrix value;
				btConeTwistConstraint_getAFrame(Native, out value);
				return value;
			}
		}

		public bool AngularOnly
		{
			get => btConeTwistConstraint_getAngularOnly(Native);
			set => btConeTwistConstraint_setAngularOnly(Native, value);
		}

		public Matrix BFrame
		{
			get
			{
				Matrix value;
				btConeTwistConstraint_getBFrame(Native, out value);
				return value;
			}
		}

		public float BiasFactor => btConeTwistConstraint_getBiasFactor(Native);

		public float Damping
		{
			get => btConeTwistConstraint_getDamping(Native);
			set => btConeTwistConstraint_setDamping(Native, value);
		}

		public float FixThresh
		{
			get => btConeTwistConstraint_getFixThresh(Native);
			set => btConeTwistConstraint_setFixThresh(Native, value);
		}

		public ConeTwistFlags Flags => btConeTwistConstraint_getFlags(Native);

		public Matrix FrameOffsetA
		{
			get
			{
				Matrix value;
				btConeTwistConstraint_getFrameOffsetA(Native, out value);
				return value;
			}
		}

		public Matrix FrameOffsetB
		{
			get
			{
				Matrix value;
				btConeTwistConstraint_getFrameOffsetB(Native, out value);
				return value;
			}
		}

		public bool IsMaxMotorImpulseNormalized => btConeTwistConstraint_isMaxMotorImpulseNormalized(Native);

		public bool IsMotorEnabled => btConeTwistConstraint_isMotorEnabled(Native);

		public bool IsPastSwingLimit => btConeTwistConstraint_isPastSwingLimit(Native);

		public float LimitSoftness => btConeTwistConstraint_getLimitSoftness(Native);

		public float MaxMotorImpulse
		{
			get => btConeTwistConstraint_getMaxMotorImpulse(Native);
			set => btConeTwistConstraint_setMaxMotorImpulse(Native, value);
		}

		public Quaternion MotorTarget
		{
			get
			{
				Quaternion value;
				btConeTwistConstraint_getMotorTarget(Native, out value);
				return value;
			}
			set => btConeTwistConstraint_setMotorTarget(Native, ref value);
		}

		public float RelaxationFactor => btConeTwistConstraint_getRelaxationFactor(Native);
		public int SolveSwingLimit => btConeTwistConstraint_getSolveSwingLimit(Native);
		public int SolveTwistLimit => btConeTwistConstraint_getSolveTwistLimit(Native);
		public float SwingSpan1 => btConeTwistConstraint_getSwingSpan1(Native);
		public float SwingSpan2 => btConeTwistConstraint_getSwingSpan2(Native);
		public float TwistAngle => btConeTwistConstraint_getTwistAngle(Native);
		public float TwistLimitSign => btConeTwistConstraint_getTwistLimitSign(Native);
		public float TwistSpan => btConeTwistConstraint_getTwistSpan(Native);
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
