using System;
using System.Runtime.InteropServices;
using BulletSharp.Math;


namespace BulletSharp
{
	[Flags]
	public enum HingeFlags
	{
		None = 0,
		CfmStop = 1,
		ErpStop = 2,
		CfmNormal = 4,
		ErpNormal = 8
	}

	public class HingeConstraint : TypedConstraint
	{
		internal HingeConstraint(IntPtr native)
			: base(native)
		{
		}

		public HingeConstraint(RigidBody rigidBodyA, RigidBody rigidBodyB, Vector3 pivotInA,
			Vector3 pivotInB, Vector3 axisInA, Vector3 axisInB, bool useReferenceFrameA = false)
			: base(UnsafeNativeMethods.btHingeConstraint_new(rigidBodyA.Native, rigidBodyB.Native,
				ref pivotInA, ref pivotInB, ref axisInA, ref axisInB, useReferenceFrameA))
		{
			_rigidBodyA = rigidBodyA;
			_rigidBodyB = rigidBodyB;
		}

		public HingeConstraint(RigidBody rigidBodyA, Vector3 pivotInA, Vector3 axisInA,
			bool useReferenceFrameA = false)
			: base(UnsafeNativeMethods.btHingeConstraint_new2(rigidBodyA.Native, ref pivotInA, ref axisInA,
				useReferenceFrameA))
		{
			_rigidBodyA = rigidBodyA;
			_rigidBodyB = GetFixedBody();
		}

		public HingeConstraint(RigidBody rigidBodyA, RigidBody rigidBodyB, Matrix rigidBodyAFrame,
			Matrix rigidBodyBFrame, bool useReferenceFrameA = false)
			: base(UnsafeNativeMethods.btHingeConstraint_new3(rigidBodyA.Native, rigidBodyB.Native,
				ref rigidBodyAFrame, ref rigidBodyBFrame, useReferenceFrameA))
		{
			_rigidBodyA = rigidBodyA;
			_rigidBodyB = rigidBodyB;
		}

		public HingeConstraint(RigidBody rigidBodyA, Matrix rigidBodyAFrame, bool useReferenceFrameA = false)
			: base(UnsafeNativeMethods.btHingeConstraint_new4(rigidBodyA.Native, ref rigidBodyAFrame,
				useReferenceFrameA))
		{
			_rigidBodyA = rigidBodyA;
			_rigidBodyB = GetFixedBody();
		}

		public void EnableAngularMotor(bool enableMotor, float targetVelocity, float maxMotorImpulse)
		{
			UnsafeNativeMethods.btHingeConstraint_enableAngularMotor(Native, enableMotor, targetVelocity,
				maxMotorImpulse);
		}

		public float GetHingeAngleRef(ref Matrix transA, ref Matrix transB)
		{
			return UnsafeNativeMethods.btHingeConstraint_getHingeAngle(Native, ref transA, ref transB);
		}

		public float GetHingeAngle(Matrix transA, Matrix transB)
		{
			return UnsafeNativeMethods.btHingeConstraint_getHingeAngle(Native, ref transA, ref transB);
		}

		public void GetInfo1NonVirtual(ConstraintInfo1 info)
		{
			UnsafeNativeMethods.btHingeConstraint_getInfo1NonVirtual(Native, info._native);
		}

		public void GetInfo2Internal(ConstraintInfo2 info, Matrix transA, Matrix transB,
			Vector3 angVelA, Vector3 angVelB)
		{
			UnsafeNativeMethods.btHingeConstraint_getInfo2Internal(Native, info._native, ref transA,
				ref transB, ref angVelA, ref angVelB);
		}

		public void GetInfo2InternalUsingFrameOffset(ConstraintInfo2 info, Matrix transA,
			Matrix transB, Vector3 angVelA, Vector3 angVelB)
		{
			UnsafeNativeMethods.btHingeConstraint_getInfo2InternalUsingFrameOffset(Native, info._native,
				ref transA, ref transB, ref angVelA, ref angVelB);
		}

		public void GetInfo2NonVirtual(ConstraintInfo2 info, Matrix transA, Matrix transB,
			Vector3 angVelA, Vector3 angVelB)
		{
			UnsafeNativeMethods.btHingeConstraint_getInfo2NonVirtual(Native, info._native, ref transA,
				ref transB, ref angVelA, ref angVelB);
		}

		public void SetAxisRef(ref Vector3 axisInA)
		{
			UnsafeNativeMethods.btHingeConstraint_setAxis(Native, ref axisInA);
		}

		public void SetAxis(Vector3 axisInA)
		{
			UnsafeNativeMethods.btHingeConstraint_setAxis(Native, ref axisInA);
		}

		public void SetFramesRef(ref Matrix frameA, ref Matrix frameB)
		{
			UnsafeNativeMethods.btHingeConstraint_setFrames(Native, ref frameA, ref frameB);
		}

		public void SetFrames(Matrix frameA, Matrix frameB)
		{
			UnsafeNativeMethods.btHingeConstraint_setFrames(Native, ref frameA, ref frameB);
		}

		public void SetLimit(float low, float high)
		{
			UnsafeNativeMethods.btHingeConstraint_setLimit(Native, low, high);
		}

		public void SetLimit(float low, float high, float softness)
		{
			UnsafeNativeMethods.btHingeConstraint_setLimit2(Native, low, high, softness);
		}

		public void SetLimit(float low, float high, float softness, float biasFactor)
		{
			UnsafeNativeMethods.btHingeConstraint_setLimit3(Native, low, high, softness, biasFactor);
		}

		public void SetLimit(float low, float high, float softness, float biasFactor,
			float relaxationFactor)
		{
			UnsafeNativeMethods.btHingeConstraint_setLimit4(Native, low, high, softness, biasFactor,
				relaxationFactor);
		}

		public void SetMotorTarget(float targetAngle, float deltaTime)
		{
			UnsafeNativeMethods.btHingeConstraint_setMotorTarget(Native, targetAngle, deltaTime);
		}

		public void SetMotorTargetRef(ref Quaternion qAinB, float deltaTime)
		{
			UnsafeNativeMethods.btHingeConstraint_setMotorTarget2(Native, ref qAinB, deltaTime);
		}

		public void SetMotorTarget(Quaternion qAinB, float deltaTime)
		{
			UnsafeNativeMethods.btHingeConstraint_setMotorTarget2(Native, ref qAinB, deltaTime);
		}

		public void TestLimitRef(ref Matrix transA, ref Matrix transB)
		{
			UnsafeNativeMethods.btHingeConstraint_testLimit(Native, ref transA, ref transB);
		}

		public void TestLimit(Matrix transA, Matrix transB)
		{
			UnsafeNativeMethods.btHingeConstraint_testLimit(Native, ref transA, ref transB);
		}

		public void UpdateRhs(float timeStep)
		{
			UnsafeNativeMethods.btHingeConstraint_updateRHS(Native, timeStep);
		}

		public Matrix AFrame
		{
			get
			{
				Matrix value;
				UnsafeNativeMethods.btHingeConstraint_getAFrame(Native, out value);
				return value;
			}
		}

		public bool AngularOnly
		{
			get { return  UnsafeNativeMethods.btHingeConstraint_getAngularOnly(Native);}
			set {  UnsafeNativeMethods.btHingeConstraint_setAngularOnly(Native, value);}
		}

		public Matrix BFrame
		{
			get
			{
				Matrix value;
				UnsafeNativeMethods.btHingeConstraint_getBFrame(Native, out value);
				return value;
			}
		}

		public bool EnableMotor
		{
			get { return  UnsafeNativeMethods.btHingeConstraint_getEnableAngularMotor(Native);}
			set {  UnsafeNativeMethods.btHingeConstraint_enableMotor(Native, value);}
		}

		public HingeFlags Flags{ get { return UnsafeNativeMethods.btHingeConstraint_getFlags(Native);} }

		public Matrix FrameOffsetA
		{
			get
			{
				Matrix value;
				UnsafeNativeMethods.btHingeConstraint_getFrameOffsetA(Native, out value);
				return value;
			}
		}

		public Matrix FrameOffsetB
		{
			get
			{
				Matrix value;
				UnsafeNativeMethods.btHingeConstraint_getFrameOffsetB(Native, out value);
				return value;
			}
		}

		public bool HasLimit{ get { return  UnsafeNativeMethods.btHingeConstraint_hasLimit(Native);} }

		public float HingeAngle{ get { return  UnsafeNativeMethods.btHingeConstraint_getHingeAngle2(Native);} }

		public float LimitBiasFactor{ get { return  UnsafeNativeMethods.btHingeConstraint_getLimitBiasFactor(Native);} }

		public float LimitRelaxationFactor{ get { return  UnsafeNativeMethods.btHingeConstraint_getLimitRelaxationFactor(Native);} }

		public float LimitSign{ get { return  UnsafeNativeMethods.btHingeConstraint_getLimitSign(Native);} }

		public float LimitSoftness{ get { return  UnsafeNativeMethods.btHingeConstraint_getLimitSoftness(Native);} }

		public float LowerLimit{ get { return  UnsafeNativeMethods.btHingeConstraint_getLowerLimit(Native);} }

		public float MaxMotorImpulse
		{
			get { return  UnsafeNativeMethods.btHingeConstraint_getMaxMotorImpulse(Native);}
			set {  UnsafeNativeMethods.btHingeConstraint_setMaxMotorImpulse(Native, value);}
		}

		public float MotorTargetVelocity{ get { return  UnsafeNativeMethods.btHingeConstraint_getMotorTargetVelocity(Native);} }

		public int SolveLimit{ get { return  UnsafeNativeMethods.btHingeConstraint_getSolveLimit(Native);} }

		public float UpperLimit{ get { return  UnsafeNativeMethods.btHingeConstraint_getUpperLimit(Native);} }

		public bool UseFrameOffset
		{
			get { return  UnsafeNativeMethods.btHingeConstraint_getUseFrameOffset(Native);}
			set {  UnsafeNativeMethods.btHingeConstraint_setUseFrameOffset(Native, value);}
		}

		public bool UseReferenceFrameA
		{
			get { return  UnsafeNativeMethods.btHingeConstraint_getUseReferenceFrameA(Native);}
			set {  UnsafeNativeMethods.btHingeConstraint_setUseReferenceFrameA(Native, value);}
		}
	}

	public class HingeAccumulatedAngleConstraint : HingeConstraint
	{
		public HingeAccumulatedAngleConstraint(RigidBody rigidBodyA, RigidBody rigidBodyB,
			Vector3 pivotInA, Vector3 pivotInB, Vector3 axisInA, Vector3 axisInB, bool useReferenceFrameA = false)
			: base(UnsafeNativeMethods.btHingeAccumulatedAngleConstraint_new(rigidBodyA.Native, rigidBodyB.Native,
				ref pivotInA, ref pivotInB, ref axisInA, ref axisInB, useReferenceFrameA))
		{
			_rigidBodyA = rigidBodyA;
			_rigidBodyB = rigidBodyB;
		}

		public HingeAccumulatedAngleConstraint(RigidBody rigidBodyA, Vector3 pivotInA,
			Vector3 axisInA, bool useReferenceFrameA = false)
			: base(UnsafeNativeMethods.btHingeAccumulatedAngleConstraint_new2(rigidBodyA.Native, ref pivotInA,
				ref axisInA, useReferenceFrameA))
		{
			_rigidBodyA = rigidBodyA;
			_rigidBodyB = GetFixedBody();
		}

		public HingeAccumulatedAngleConstraint(RigidBody rigidBodyA, RigidBody rigidBodyB,
			Matrix rigidBodyAFrame, Matrix rigidBodyBFrame, bool useReferenceFrameA = false)
			: base(UnsafeNativeMethods.btHingeAccumulatedAngleConstraint_new3(rigidBodyA.Native, rigidBodyB.Native,
				ref rigidBodyAFrame, ref rigidBodyBFrame, useReferenceFrameA))
		{
			_rigidBodyA = rigidBodyA;
			_rigidBodyB = rigidBodyB;
		}

		public HingeAccumulatedAngleConstraint(RigidBody rigidBodyA, Matrix rigidBodyAFrame,
			bool useReferenceFrameA = false)
			: base(UnsafeNativeMethods.btHingeAccumulatedAngleConstraint_new4(rigidBodyA.Native, ref rigidBodyAFrame,
				useReferenceFrameA))
		{
			_rigidBodyA = rigidBodyA;
			_rigidBodyB = GetFixedBody();
		}

		public float AccumulatedHingeAngle
		{
			get { return  UnsafeNativeMethods.btHingeAccumulatedAngleConstraint_getAccumulatedHingeAngle(Native);}
			set {  UnsafeNativeMethods.btHingeAccumulatedAngleConstraint_setAccumulatedHingeAngle(Native, value);}
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct HingeConstraintFloatData
	{
		public TypedConstraintFloatData TypedConstraintData;
		public TransformFloatData RigidBodyAFrame;
		public TransformFloatData RigidBodyBFrame;
		public int UseReferenceFrameA;
		public int AngularOnly;
		public int EnableAngularMotor;
		public float MotorTargetVelocity;
		public float MaxMotorImpulse;
		public float LowerLimit;
		public float UpperLimit;
		public float LimitSoftness;
		public float BiasFactor;
		public float RelaxationFactor;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(HingeConstraintFloatData), fieldName).ToInt32(); }
	}
}
