using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using BulletSharp.Math;


namespace BulletSharp
{
	[Flags]
	public enum RigidBodyFlags
	{
		None = 0,
		DisableWorldGravity = 1,
		EnableGyroscopicForceExplicit = 2,
		EnableGyroscopicForceImplicitWorld = 4,
		EnableGyroscopicForceImplicitBody = 8,
		EnableGyroscopicForce = EnableGyroscopicForceImplicitBody
	}

	public class RigidBody : CollisionObject
	{
		private MotionState _motionState;
		internal List<TypedConstraint> _constraintRefs;

		public RigidBody(RigidBodyConstructionInfo constructionInfo)
			: base(UnsafeNativeMethods.btRigidBody_new(constructionInfo.Native))
		{
			_collisionShape = constructionInfo.CollisionShape;
			_motionState = constructionInfo.MotionState;
		}

		public void AddConstraintRef(TypedConstraint constraint)
		{
			if (_constraintRefs == null)
			{
				_constraintRefs = new List<TypedConstraint>();
			}
			_constraintRefs.Add(constraint);
			UnsafeNativeMethods.btRigidBody_addConstraintRef(Native, constraint.Native);
		}

		public void ApplyCentralForceRef(ref Vector3 force)
		{
			UnsafeNativeMethods.btRigidBody_applyCentralForce(Native, ref force);
		}

		public void ApplyCentralForce(Vector3 force)
		{
			UnsafeNativeMethods.btRigidBody_applyCentralForce(Native, ref force);
		}

		public void ApplyCentralImpulseRef(ref Vector3 impulse)
		{
			UnsafeNativeMethods.btRigidBody_applyCentralImpulse(Native, ref impulse);
		}

		public void ApplyCentralImpulse(Vector3 impulse)
		{
			UnsafeNativeMethods.btRigidBody_applyCentralImpulse(Native, ref impulse);
		}

		public void ApplyDamping(float timeStep)
		{
			UnsafeNativeMethods.btRigidBody_applyDamping(Native, timeStep);
		}

		public void ApplyForceRef(ref Vector3 force, ref Vector3 relPos)
		{
			UnsafeNativeMethods.btRigidBody_applyForce(Native, ref force, ref relPos);
		}

		public void ApplyForce(Vector3 force, Vector3 relPos)
		{
			UnsafeNativeMethods.btRigidBody_applyForce(Native, ref force, ref relPos);
		}

		public void ApplyGravity()
		{
			UnsafeNativeMethods.btRigidBody_applyGravity(Native);
		}

		public void ApplyImpulseRef(ref Vector3 impulse, ref Vector3 relPos)
		{
			UnsafeNativeMethods.btRigidBody_applyImpulse(Native, ref impulse, ref relPos);
		}

		public void ApplyImpulse(Vector3 impulse, Vector3 relPos)
		{
			UnsafeNativeMethods.btRigidBody_applyImpulse(Native, ref impulse, ref relPos);
		}

		public void ApplyTorqueRef(ref Vector3 torque)
		{
			UnsafeNativeMethods.btRigidBody_applyTorque(Native, ref torque);
		}

		public void ApplyTorque(Vector3 torque)
		{
			UnsafeNativeMethods.btRigidBody_applyTorque(Native, ref torque);
		}

		public void ApplyTorqueImpulseRef(ref Vector3 torque)
		{
			UnsafeNativeMethods.btRigidBody_applyTorqueImpulse(Native, ref torque);
		}

		public void ApplyTorqueImpulse(Vector3 torque)
		{
			UnsafeNativeMethods.btRigidBody_applyTorqueImpulse(Native, ref torque);
		}

		public void ClearForces()
		{
			UnsafeNativeMethods.btRigidBody_clearForces(Native);
		}

		public void ComputeAngularImpulseDenominator(ref Vector3 axis, out float result)
		{
			result = UnsafeNativeMethods.btRigidBody_computeAngularImpulseDenominator(Native, ref axis);
		}

		public float ComputeAngularImpulseDenominator(Vector3 axis)
		{
			return UnsafeNativeMethods.btRigidBody_computeAngularImpulseDenominator(Native, ref axis);
		}

		public Vector3 ComputeGyroscopicForceExplicit(float maxGyroscopicForce)
		{
			Vector3 value;
			UnsafeNativeMethods.btRigidBody_computeGyroscopicForceExplicit(Native, maxGyroscopicForce,
				out value);
			return value;
		}

		public Vector3 ComputeGyroscopicImpulseImplicitBody(float step)
		{
			Vector3 value;
			UnsafeNativeMethods.btRigidBody_computeGyroscopicImpulseImplicit_Body(Native, step, out value);
			return value;
		}

		public Vector3 ComputeGyroscopicImpulseImplicitWorld(float deltaTime)
		{
			Vector3 value;
			UnsafeNativeMethods.btRigidBody_computeGyroscopicImpulseImplicit_World(Native, deltaTime,
				out value);
			return value;
		}

		public float ComputeImpulseDenominator(Vector3 pos, Vector3 normal)
		{
			return UnsafeNativeMethods.btRigidBody_computeImpulseDenominator(Native, ref pos, ref normal);
		}

		public void GetAabb(out Vector3 aabbMin, out Vector3 aabbMax)
		{
			UnsafeNativeMethods.btRigidBody_getAabb(Native, out aabbMin, out aabbMax);
		}

		public TypedConstraint GetConstraintRef(int index)
		{
			System.Diagnostics.Debug.Assert(_constraintRefs != null);
			return _constraintRefs[index];
		}

		public void GetVelocityInLocalPoint(ref Vector3 relPos, out Vector3 value)
		{
			UnsafeNativeMethods.btRigidBody_getVelocityInLocalPoint(Native, ref relPos, out value);
		}

		public Vector3 GetVelocityInLocalPoint(Vector3 relPos)
		{
			Vector3 value;
			UnsafeNativeMethods.btRigidBody_getVelocityInLocalPoint(Native, ref relPos, out value);
			return value;
		}

		public void IntegrateVelocities(float step)
		{
			UnsafeNativeMethods.btRigidBody_integrateVelocities(Native, step);
		}

		public void PredictIntegratedTransform(float step, out Matrix predictedTransform)
		{
			UnsafeNativeMethods.btRigidBody_predictIntegratedTransform(Native, step, out predictedTransform);
		}

		public void ProceedToTransformRef(ref Matrix newTrans)
		{
			UnsafeNativeMethods.btRigidBody_proceedToTransform(Native, ref newTrans);
		}

		public void ProceedToTransform(Matrix newTrans)
		{
			UnsafeNativeMethods.btRigidBody_proceedToTransform(Native, ref newTrans);
		}

		public void RemoveConstraintRef(TypedConstraint constraint)
		{
			if (_constraintRefs != null)
			{
				_constraintRefs.Remove(constraint);
				UnsafeNativeMethods.btRigidBody_removeConstraintRef(Native, constraint.Native);
			}
		}

		public void SaveKinematicState(float step)
		{
			UnsafeNativeMethods.btRigidBody_saveKinematicState(Native, step);
		}

		public void SetDamping(float linDamping, float angDamping)
		{
			UnsafeNativeMethods.btRigidBody_setDamping(Native, linDamping, angDamping);
		}

		public void SetMassPropsRef(float mass, ref Vector3 inertia)
		{
			UnsafeNativeMethods.btRigidBody_setMassProps(Native, mass, ref inertia);
		}

		public void SetMassProps(float mass, Vector3 inertia)
		{
			UnsafeNativeMethods.btRigidBody_setMassProps(Native, mass, ref inertia);
		}

		public void SetNewBroadphaseProxy(BroadphaseProxy broadphaseProxy)
		{
			UnsafeNativeMethods.btRigidBody_setNewBroadphaseProxy(Native, broadphaseProxy.Native);
		}

		public void SetSleepingThresholds(float linear, float angular)
		{
			UnsafeNativeMethods.btRigidBody_setSleepingThresholds(Native, linear, angular);
		}

		public void TranslateRef(ref Vector3 v)
		{
			UnsafeNativeMethods.btRigidBody_translate(Native, ref v);
		}

		public void Translate(Vector3 v)
		{
			UnsafeNativeMethods.btRigidBody_translate(Native, ref v);
		}

		public static RigidBody Upcast(CollisionObject colObj)
		{
			return GetManaged(UnsafeNativeMethods.btRigidBody_upcast(colObj.Native)) as RigidBody;
		}

		public void UpdateDeactivation(float timeStep)
		{
			UnsafeNativeMethods.btRigidBody_updateDeactivation(Native, timeStep);
		}

		public void UpdateInertiaTensor()
		{
			UnsafeNativeMethods.btRigidBody_updateInertiaTensor(Native);
		}

		public bool WantsSleeping()
		{
			return UnsafeNativeMethods.btRigidBody_wantsSleeping(Native);
		}

		public float AngularDamping => UnsafeNativeMethods.btRigidBody_getAngularDamping(Native);

		public Vector3 AngularFactor
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btRigidBody_getAngularFactor(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btRigidBody_setAngularFactor(Native, ref value);
		}

		public float AngularSleepingThreshold => UnsafeNativeMethods.btRigidBody_getAngularSleepingThreshold(Native);

		public Vector3 AngularVelocity
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btRigidBody_getAngularVelocity(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btRigidBody_setAngularVelocity(Native, ref value);
		}

		public BroadphaseProxy BroadphaseProxy => BroadphaseProxy.GetManaged(UnsafeNativeMethods.btRigidBody_getBroadphaseProxy(Native));

		public Vector3 CenterOfMassPosition
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btRigidBody_getCenterOfMassPosition(Native, out value);
				return value;
			}
		}

		public Matrix CenterOfMassTransform
		{
			get
			{
				Matrix value;
				UnsafeNativeMethods.btRigidBody_getCenterOfMassTransform(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btRigidBody_setCenterOfMassTransform(Native, ref value);
		}

		public int ContactSolverType
		{
			get => UnsafeNativeMethods.btRigidBody_getContactSolverType(Native);
			set => UnsafeNativeMethods.btRigidBody_setContactSolverType(Native, value);
		}

		public RigidBodyFlags Flags
		{
			get => btRigidBody_getFlags(Native);
			set => UnsafeNativeMethods.btRigidBody_setFlags(Native, value);
		}

		public int FrictionSolverType
		{
			get => UnsafeNativeMethods.btRigidBody_getFrictionSolverType(Native);
			set => UnsafeNativeMethods.btRigidBody_setFrictionSolverType(Native, value);
		}

		public Vector3 Gravity
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btRigidBody_getGravity(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btRigidBody_setGravity(Native, ref value);
		}

		public Vector3 InvInertiaDiagLocal
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btRigidBody_getInvInertiaDiagLocal(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btRigidBody_setInvInertiaDiagLocal(Native, ref value);
		}

		public Matrix InvInertiaTensorWorld
		{
			get
			{
				Matrix value;
				UnsafeNativeMethods.btRigidBody_getInvInertiaTensorWorld(Native, out value);
				return value;
			}
		}

		public float InvMass => UnsafeNativeMethods.btRigidBody_getInvMass(Native);

		public bool IsInWorld => UnsafeNativeMethods.btRigidBody_isInWorld(Native);

		public float LinearDamping => UnsafeNativeMethods.btRigidBody_getLinearDamping(Native);

		public Vector3 LinearFactor
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btRigidBody_getLinearFactor(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btRigidBody_setLinearFactor(Native, ref value);
		}

		public float LinearSleepingThreshold => UnsafeNativeMethods.btRigidBody_getLinearSleepingThreshold(Native);

		public Vector3 LinearVelocity
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btRigidBody_getLinearVelocity(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btRigidBody_setLinearVelocity(Native, ref value);
		}

		public Vector3 LocalInertia
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btRigidBody_getLocalInertia(Native, out value);
				return value;
			}
		}

		public MotionState MotionState
		{
			get => _motionState;
			set
			{
				UnsafeNativeMethods.btRigidBody_setMotionState(Native, (value != null) ? value._native : IntPtr.Zero);
				_motionState = value;
			}
		}

		public int NumConstraintRefs => (_constraintRefs != null) ? _constraintRefs.Count : 0;

		public Quaternion Orientation
		{
			get
			{
				Quaternion value;
				UnsafeNativeMethods.btRigidBody_getOrientation(Native, out value);
				return value;
			}
		}

		public Vector3 TotalForce
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btRigidBody_getTotalForce(Native, out value);
				return value;
			}
		}

		public Vector3 TotalTorque
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btRigidBody_getTotalTorque(Native, out value);
				return value;
			}
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct RigidBodyFloatData
	{
		public CollisionObjectFloatData CollisionObjectData;
		public Matrix3x3FloatData InvInertiaTensorWorld;
		public Vector3FloatData LinearVelocity;
		public Vector3FloatData AngularVelocity;
		public Vector3FloatData AngularFactor;
		public Vector3FloatData LinearFactor;
		public Vector3FloatData Gravity;
		public Vector3FloatData GravityAcceleration;
		public Vector3FloatData InvInertiaLocal;
		public Vector3FloatData TotalForce;
		public Vector3FloatData TotalTorque;
		public float InverseMass;
		public float LinearDamping;
		public float AngularDamping;
		public float AdditionalDampingFactor;
		public float AdditionalLinearDampingThresholdSqr;
		public float AdditionalAngularDampingThresholdSqr;
		public float AdditionalAngularDampingFactor;
		public float LinearSleepingThreshold;
		public float AngularSleepingThreshold;
		public int AdditionalDamping;
		//public int Padding;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(RigidBodyFloatData), fieldName).ToInt32(); }
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct RigidBodyDoubleData
	{
		public CollisionObjectDoubleData CollisionObjectData;
		public Matrix3x3DoubleData InvInertiaTensorWorld;
		public Vector3DoubleData LinearVelocity;
		public Vector3DoubleData AngularVelocity;
		public Vector3DoubleData AngularFactor;
		public Vector3DoubleData LinearFactor;
		public Vector3DoubleData Gravity;
		public Vector3DoubleData GravityAcceleration;
		public Vector3DoubleData InvInertiaLocal;
		public Vector3DoubleData TotalForce;
		public Vector3DoubleData TotalTorque;
		public double InverseMass;
		public double LinearDamping;
		public double AngularDamping;
		public double AdditionalDampingFactor;
		public double AdditionalLinearDampingThresholdSqr;
		public double AdditionalAngularDampingThresholdSqr;
		public double AdditionalAngularDampingFactor;
		public double LinearSleepingThreshold;
		public double AngularSleepingThreshold;
		public int AdditionalDamping;
		//public int Padding;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(RigidBodyDoubleData), fieldName).ToInt32(); }
	}
}
