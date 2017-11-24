using System;
using System.Runtime.InteropServices;
using BulletSharp.Math;


namespace BulletSharp
{
	[Flags]
	public enum SliderFlags
	{
		None = 0,
		CfmDirLinear = 1,
		ErpDirLinear = 2,
		CfmDirAngular = 4,
		ErpDirAngular = 8,
		CfmOrthoLinear = 16,
		ErpOrthoLinear = 32,
		CfmOrthoAngular = 64,
		ErpOrthoAngular = 128,
		CfmLimitLinear = 512,
		ErpLimitLinear = 1024,
		CfmLimitAngular = 2048,
		ErpLimitAngular = 4096
	}

	public class SliderConstraint : TypedConstraint
	{
		public SliderConstraint(RigidBody rigidBodyA, RigidBody rigidBodyB, Matrix frameInA,
			Matrix frameInB, bool useLinearReferenceFrameA)
			: base(UnsafeNativeMethods.btSliderConstraint_new(rigidBodyA.Native, rigidBodyB.Native,
				ref frameInA, ref frameInB, useLinearReferenceFrameA))
		{
			_rigidBodyA = rigidBodyA;
			_rigidBodyB = rigidBodyB;
		}

		public SliderConstraint(RigidBody rigidBodyB, Matrix frameInB, bool useLinearReferenceFrameA)
			: base(UnsafeNativeMethods.btSliderConstraint_new2(rigidBodyB.Native, ref frameInB, useLinearReferenceFrameA))
		{
			_rigidBodyA = GetFixedBody();
			_rigidBodyB = rigidBodyB;
		}

		public void CalculateTransformsRef(ref Matrix transA, ref Matrix transB)
		{
			UnsafeNativeMethods.btSliderConstraint_calculateTransforms(Native, ref transA, ref transB);
		}

		public void CalculateTransforms(Matrix transA, Matrix transB)
		{
			UnsafeNativeMethods.btSliderConstraint_calculateTransforms(Native, ref transA, ref transB);
		}

		public void GetInfo1NonVirtual(ConstraintInfo1 info)
		{
			UnsafeNativeMethods.btSliderConstraint_getInfo1NonVirtual(Native, info._native);
		}

		public void GetInfo2NonVirtual(ConstraintInfo2 info, Matrix transA, Matrix transB,
			Vector3 linVelA, Vector3 linVelB, float rbAinvMass, float rbBinvMass)
		{
			UnsafeNativeMethods.btSliderConstraint_getInfo2NonVirtual(Native, info._native, ref transA,
				ref transB, ref linVelA, ref linVelB, rbAinvMass, rbBinvMass);
		}

		public void SetFramesRef(ref Matrix frameA, ref Matrix frameB)
		{
			UnsafeNativeMethods.btSliderConstraint_setFrames(Native, ref frameA, ref frameB);
		}

		public void SetFrames(Matrix frameA, Matrix frameB)
		{
			UnsafeNativeMethods.btSliderConstraint_setFrames(Native, ref frameA, ref frameB);
		}

		public void TestAngularLimits()
		{
			UnsafeNativeMethods.btSliderConstraint_testAngLimits(Native);
		}

		public void TestLinearLimits()
		{
			UnsafeNativeMethods.btSliderConstraint_testLinLimits(Native);
		}

		public Vector3 AncorInA
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btSliderConstraint_getAncorInA(Native, out value);
				return value;
			}
		}

		public Vector3 AncorInB
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btSliderConstraint_getAncorInB(Native, out value);
				return value;
			}
		}

		public float AngularDepth{ get { return  UnsafeNativeMethods.btSliderConstraint_getAngDepth(Native);} }

		public float AngularPosition{ get { return  UnsafeNativeMethods.btSliderConstraint_getAngularPos(Native);} }

		public Matrix CalculatedTransformA
		{
			get
			{
				Matrix value;
				UnsafeNativeMethods.btSliderConstraint_getCalculatedTransformA(Native, out value);
				return value;
			}
		}

		public Matrix CalculatedTransformB
		{
			get
			{
				Matrix value;
				UnsafeNativeMethods.btSliderConstraint_getCalculatedTransformB(Native, out value);
				return value;
			}
		}

		public float DampingDirAngular
		{
			get { return  UnsafeNativeMethods.btSliderConstraint_getDampingDirAng(Native);}
			set {  UnsafeNativeMethods.btSliderConstraint_setDampingDirAng(Native, value);}
		}

		public float DampingDirLinear
		{
			get { return  UnsafeNativeMethods.btSliderConstraint_getDampingDirLin(Native);}
			set {  UnsafeNativeMethods.btSliderConstraint_setDampingDirLin(Native, value);}
		}

		public float DampingLimAngular
		{
			get { return  UnsafeNativeMethods.btSliderConstraint_getDampingLimAng(Native);}
			set {  UnsafeNativeMethods.btSliderConstraint_setDampingLimAng(Native, value);}
		}

		public float DampingLimLinear
		{
			get { return  UnsafeNativeMethods.btSliderConstraint_getDampingLimLin(Native);}
			set {  UnsafeNativeMethods.btSliderConstraint_setDampingLimLin(Native, value);}
		}

		public float DampingOrthoAngular
		{
			get { return  UnsafeNativeMethods.btSliderConstraint_getDampingOrthoAng(Native);}
			set {  UnsafeNativeMethods.btSliderConstraint_setDampingOrthoAng(Native, value);}
		}

		public float DampingOrthoLinear
		{
			get { return  UnsafeNativeMethods.btSliderConstraint_getDampingOrthoLin(Native);}
			set {  UnsafeNativeMethods.btSliderConstraint_setDampingOrthoLin(Native, value);}
		}

		public SliderFlags Flags{ get { return  UnsafeNativeMethods.btSliderConstraint_getFlags(Native);} }
		public Matrix FrameOffsetA
		{
			get
			{
				Matrix value;
				UnsafeNativeMethods.btSliderConstraint_getFrameOffsetA(Native, out value);
				return value;
			}
		}

		public Matrix FrameOffsetB
		{
			get
			{
				Matrix value;
				UnsafeNativeMethods.btSliderConstraint_getFrameOffsetB(Native, out value);
				return value;
			}
		}

		public float LinearDepth{ get { return  UnsafeNativeMethods.btSliderConstraint_getLinDepth(Native);} }

		public float LinearPosition{ get { return  UnsafeNativeMethods.btSliderConstraint_getLinearPos(Native);} }

		public float LowerAngularLimit
		{
			get { return  UnsafeNativeMethods.btSliderConstraint_getLowerAngLimit(Native);}
			set {  UnsafeNativeMethods.btSliderConstraint_setLowerAngLimit(Native, value);}
		}

		public float LowerLinearLimit
		{
			get { return  UnsafeNativeMethods.btSliderConstraint_getLowerLinLimit(Native);}
			set {  UnsafeNativeMethods.btSliderConstraint_setLowerLinLimit(Native, value);}
		}

		public float MaxAngMotorForce
		{
			get { return  UnsafeNativeMethods.btSliderConstraint_getMaxAngMotorForce(Native);}
			set {  UnsafeNativeMethods.btSliderConstraint_setMaxAngMotorForce(Native, value);}
		}

		public float MaxLinearMotorForce
		{
			get { return  UnsafeNativeMethods.btSliderConstraint_getMaxLinMotorForce(Native);}
			set {  UnsafeNativeMethods.btSliderConstraint_setMaxLinMotorForce(Native, value);}
		}

		public bool PoweredAngularMotor
		{
			get { return  UnsafeNativeMethods.btSliderConstraint_getPoweredAngMotor(Native);}
			set {  UnsafeNativeMethods.btSliderConstraint_setPoweredAngMotor(Native, value);}
		}

		public bool PoweredLinearMotor
		{
			get { return  UnsafeNativeMethods.btSliderConstraint_getPoweredLinMotor(Native);}
			set {  UnsafeNativeMethods.btSliderConstraint_setPoweredLinMotor(Native, value);}
		}

		public float RestitutionDirAngular
		{
			get { return  UnsafeNativeMethods.btSliderConstraint_getRestitutionDirAng(Native);}
			set {  UnsafeNativeMethods.btSliderConstraint_setRestitutionDirAng(Native, value);}
		}

		public float RestitutionDirLinear
		{
			get { return  UnsafeNativeMethods.btSliderConstraint_getRestitutionDirLin(Native);}
			set {  UnsafeNativeMethods.btSliderConstraint_setRestitutionDirLin(Native, value);}
		}

		public float RestitutionLimAngular
		{
			get { return  UnsafeNativeMethods.btSliderConstraint_getRestitutionLimAng(Native);}
			set {  UnsafeNativeMethods.btSliderConstraint_setRestitutionLimAng(Native, value);}
		}

		public float RestitutionLimLinear
		{
			get { return  UnsafeNativeMethods.btSliderConstraint_getRestitutionLimLin(Native);}
			set {  UnsafeNativeMethods.btSliderConstraint_setRestitutionLimLin(Native, value);}
		}

		public float RestitutionOrthoAngular
		{
			get { return  UnsafeNativeMethods.btSliderConstraint_getRestitutionOrthoAng(Native);}
			set {  UnsafeNativeMethods.btSliderConstraint_setRestitutionOrthoAng(Native, value);}
		}

		public float RestitutionOrthoLinear
		{
			get { return  UnsafeNativeMethods.btSliderConstraint_getRestitutionOrthoLin(Native);}
			set {  UnsafeNativeMethods.btSliderConstraint_setRestitutionOrthoLin(Native, value);}
		}

		public float SoftnessDirAngular
		{
			get { return  UnsafeNativeMethods.btSliderConstraint_getSoftnessDirAng(Native);}
			set {  UnsafeNativeMethods.btSliderConstraint_setSoftnessDirAng(Native, value);}
		}

		public float SoftnessDirLinear
		{
			get { return  UnsafeNativeMethods.btSliderConstraint_getSoftnessDirLin(Native);}
			set {  UnsafeNativeMethods.btSliderConstraint_setSoftnessDirLin(Native, value);}
		}

		public float SoftnessLimAngular
		{
			get { return  UnsafeNativeMethods.btSliderConstraint_getSoftnessLimAng(Native);}
			set {  UnsafeNativeMethods.btSliderConstraint_setSoftnessLimAng(Native, value);}
		}

		public float SoftnessLimLinear
		{
			get { return  UnsafeNativeMethods.btSliderConstraint_getSoftnessLimLin(Native);}
			set {  UnsafeNativeMethods.btSliderConstraint_setSoftnessLimLin(Native, value);}
		}

		public float SoftnessOrthoAngular
		{
			get { return  UnsafeNativeMethods.btSliderConstraint_getSoftnessOrthoAng(Native);}
			set {  UnsafeNativeMethods.btSliderConstraint_setSoftnessOrthoAng(Native, value);}
		}

		public float SoftnessOrthoLinear
		{
			get { return  UnsafeNativeMethods.btSliderConstraint_getSoftnessOrthoLin(Native);}
			set {  UnsafeNativeMethods.btSliderConstraint_setSoftnessOrthoLin(Native, value);}
		}

		public bool SolveAngularLimit{ get { return  UnsafeNativeMethods.btSliderConstraint_getSolveAngLimit(Native);} }

		public bool SolveLinearLimit{ get { return  UnsafeNativeMethods.btSliderConstraint_getSolveLinLimit(Native);} }

		public float TargetAngularMotorVelocity
		{
			get { return  UnsafeNativeMethods.btSliderConstraint_getTargetAngMotorVelocity(Native);}
			set {  UnsafeNativeMethods.btSliderConstraint_setTargetAngMotorVelocity(Native, value);}
		}

		public float TargetLinearMotorVelocity
		{
			get { return  UnsafeNativeMethods.btSliderConstraint_getTargetLinMotorVelocity(Native);}
			set {  UnsafeNativeMethods.btSliderConstraint_setTargetLinMotorVelocity(Native, value);}
		}

		public float UpperAngularLimit
		{
			get { return  UnsafeNativeMethods.btSliderConstraint_getUpperAngLimit(Native);}
			set {  UnsafeNativeMethods.btSliderConstraint_setUpperAngLimit(Native, value);}
		}

		public float UpperLinearLimit
		{
			get { return  UnsafeNativeMethods.btSliderConstraint_getUpperLinLimit(Native);}
			set {  UnsafeNativeMethods.btSliderConstraint_setUpperLinLimit(Native, value);}
		}

		public bool UseFrameOffset
		{
			get { return  UnsafeNativeMethods.btSliderConstraint_getUseFrameOffset(Native);}
			set {  UnsafeNativeMethods.btSliderConstraint_setUseFrameOffset(Native, value);}
		}

		public bool UseLinearReferenceFrameA{ get { return  UnsafeNativeMethods.btSliderConstraint_getUseLinearReferenceFrameA(Native);} }
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct SliderConstraintFloatData
	{
		public TypedConstraintFloatData TypedConstraintData;
		public TransformFloatData RigidBodyAFrame;
		public TransformFloatData RigidBodyBFrame;
		public float LinearUpperLimit;
		public float LinearLowerLimit;
		public float AngularUpperLimit;
		public float AngularLowerLimit;
		public int UseLinearReferenceFrameA;
		public int UseOffsetForConstraintFrame;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(SliderConstraintFloatData), fieldName).ToInt32(); }
	}
}
