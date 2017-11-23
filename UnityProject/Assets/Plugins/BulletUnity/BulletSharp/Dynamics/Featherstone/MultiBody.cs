using System;
using System.Runtime.InteropServices;
using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class MultiBody : IDisposable
	{
		internal IntPtr Native;
		private MultiBodyLink[] _links;

		internal MultiBody(IntPtr native)
		{
			Native = native;
		}

		public MultiBody(int nLinks, float mass, Vector3 inertia, bool fixedBase,
			bool canSleep)
		{
			Native = btMultiBody_new(nLinks, mass, ref inertia, fixedBase, canSleep);
		}

		public void AddBaseConstraintForce(Vector3 f)
		{
			btMultiBody_addBaseConstraintForce(Native, ref f);
		}

		public void AddBaseConstraintTorque(Vector3 t)
		{
			btMultiBody_addBaseConstraintTorque(Native, ref t);
		}

		public void AddBaseForce(Vector3 f)
		{
			btMultiBody_addBaseForce(Native, ref f);
		}

		public void AddBaseTorque(Vector3 t)
		{
			btMultiBody_addBaseTorque(Native, ref t);
		}

		public void AddJointTorque(int i, float q)
		{
			btMultiBody_addJointTorque(Native, i, q);
		}

		public void AddJointTorqueMultiDof(int i, float[] q)
		{
			btMultiBody_addJointTorqueMultiDof(Native, i, q);
		}

		public void AddJointTorqueMultiDof(int i, int dof, float q)
		{
			btMultiBody_addJointTorqueMultiDof2(Native, i, dof, q);
		}

		public void AddLinkConstraintForce(int i, Vector3 f)
		{
			btMultiBody_addLinkConstraintForce(Native, i, ref f);
		}

		public void AddLinkConstraintTorque(int i, Vector3 t)
		{
			btMultiBody_addLinkConstraintTorque(Native, i, ref t);
		}

		public void AddLinkForce(int i, Vector3 f)
		{
			btMultiBody_addLinkForce(Native, i, ref f);
		}

		public void AddLinkTorque(int i, Vector3 t)
		{
			btMultiBody_addLinkTorque(Native, i, ref t);
		}
		/*

		public void ApplyDeltaVeeMultiDof(float deltaVee, float multiplier)
		{
			btMultiBody_applyDeltaVeeMultiDof(Native, deltaVee.Native, multiplier);
		}

		public void ApplyDeltaVeeMultiDof2(float deltaVee, float multiplier)
		{
			btMultiBody_applyDeltaVeeMultiDof2(Native, deltaVee.Native, multiplier);
		}

		public void CalcAccelerationDeltasMultiDof(float force, float output, AlignedObjectArray<float> scratchR,
			AlignedObjectArray<btVector3> scratchV)
		{
			btMultiBody_calcAccelerationDeltasMultiDof(Native, force.Native, output.Native,
				scratchR.Native, scratchV.Native);
		}
		*/
		public int CalculateSerializeBufferSize()
		{
			return btMultiBody_calculateSerializeBufferSize(Native);
		}

		public void CheckMotionAndSleepIfRequired(float timestep)
		{
			btMultiBody_checkMotionAndSleepIfRequired(Native, timestep);
		}

		public void ClearConstraintForces()
		{
			btMultiBody_clearConstraintForces(Native);
		}

		public void ClearForcesAndTorques()
		{
			btMultiBody_clearForcesAndTorques(Native);
		}

		public void ClearVelocities()
		{
			btMultiBody_clearVelocities(Native);
		}
		/*
		public void ComputeAccelerationsArticulatedBodyAlgorithmMultiDof(float deltaTime,
			AlignedObjectArray<float> scratchR, AlignedObjectArray<btVector3> scratchV,
			AlignedObjectArray<btMatrix3x3> scratchM, bool isConstraintPass = false)
		{
			btMultiBody_computeAccelerationsArticulatedBodyAlgorithmMultiDof(Native,
				deltaTime, scratchR.Native, scratchV.Native, scratchM.Native, isConstraintPass);
		}

		public void FillConstraintJacobianMultiDof(int link, Vector3 contactPoint,
			Vector3 normalAng, Vector3 normalLin, float jac, AlignedObjectArray<float> scratchR,
			AlignedObjectArray<btVector3> scratchV, AlignedObjectArray<btMatrix3x3> scratchM)
		{
			btMultiBody_fillConstraintJacobianMultiDof(Native, link, ref contactPoint,
				ref normalAng, ref normalLin, jac.Native, scratchR.Native, scratchV.Native,
				scratchM.Native);
		}

		public void FillContactJacobianMultiDof(int link, Vector3 contactPoint, Vector3 normal,
			float jac, AlignedObjectArray<float> scratchR, AlignedObjectArray<btVector3> scratchV,
			AlignedObjectArray<btMatrix3x3> scratchM)
		{
			btMultiBody_fillContactJacobianMultiDof(Native, link, ref contactPoint,
				ref normal, jac.Native, scratchR.Native, scratchV.Native, scratchM.Native);
		}
		*/
		public void FinalizeMultiDof()
		{
			btMultiBody_finalizeMultiDof(Native);
		}
		/*
		public void ForwardKinematics(btAlignedObjectArray<btQuaternion> scratchQ,
			AlignedObjectArray<btVector3> scratchM)
		{
			btMultiBody_forwardKinematics(Native, scratchQ.Native, scratchM.Native);
		}
		*/
		public float GetJointPos(int i)
		{
			return btMultiBody_getJointPos(Native, i);
		}
		/*
		public float GetJointPosMultiDof(int i)
		{
			return btMultiBody_getJointPosMultiDof(Native, i);
		}
		*/
		public float GetJointTorque(int i)
		{
			return btMultiBody_getJointTorque(Native, i);
		}
		/*
		public float GetJointTorqueMultiDof(int i)
		{
			return btMultiBody_getJointTorqueMultiDof(Native, i);
		}
		*/
		public float GetJointVel(int i)
		{
			return btMultiBody_getJointVel(Native, i);
		}
		/*
		public float GetJointVelMultiDof(int i)
		{
			return btMultiBody_getJointVelMultiDof(Native, i);
		}
		*/
		public MultiBodyLink GetLink(int index)
		{
			if (_links == null) {
				_links = new MultiBodyLink[NumLinks];
			}
			if (_links[index] == null) {
				_links[index] = new MultiBodyLink(btMultiBody_getLink(Native, index));
			}
			return _links[index];
		}

		public Vector3 GetLinkForce(int i)
		{
			Vector3 value;
			btMultiBody_getLinkForce(Native, i, out value);
			return value;
		}

		public Vector3 GetLinkInertia(int i)
		{
			Vector3 value;
			btMultiBody_getLinkInertia(Native, i, out value);
			return value;
		}

		public float GetLinkMass(int i)
		{
			return btMultiBody_getLinkMass(Native, i);
		}

		public Vector3 GetLinkTorque(int i)
		{
			Vector3 value;
			btMultiBody_getLinkTorque(Native, i, out value);
			return value;
		}

		public int GetParent(int linkNum)
		{
			return btMultiBody_getParent(Native, linkNum);
		}

		public Quaternion GetParentToLocalRot(int i)
		{
			Quaternion value;
			btMultiBody_getParentToLocalRot(Native, i, out value);
			return value;
		}

		public Vector3 GetRVector(int i)
		{
			Vector3 value;
			btMultiBody_getRVector(Native, i, out value);
			return value;
		}

		public void GoToSleep()
		{
			btMultiBody_goToSleep(Native);
		}

		public bool InternalNeedsJointFeedback()
		{
			return btMultiBody_internalNeedsJointFeedback(Native);
		}

		public Vector3 LocalDirToWorld(int i, Vector3 vec)
		{
			Vector3 value;
			btMultiBody_localDirToWorld(Native, i, ref vec, out value);
			return value;
		}

		public Matrix LocalFrameToWorld(int i, Matrix mat)
		{
			Matrix value;
			btMultiBody_localFrameToWorld(Native, i, ref mat, out value);
			return value;
		}

		public Vector3 LocalPosToWorld(int i, Vector3 vec)
		{
			Vector3 value;
			btMultiBody_localPosToWorld(Native, i, ref vec, out value);
			return value;
		}

		public void ProcessDeltaVeeMultiDof2()
		{
			btMultiBody_processDeltaVeeMultiDof2(Native);
		}

		public string Serialize(IntPtr dataBuffer, Serializer serializer)
		{
			return Marshal.PtrToStringAnsi(btMultiBody_serialize(Native, dataBuffer, serializer._native));
		}

		public void SetJointPos(int i, float q)
		{
			btMultiBody_setJointPos(Native, i, q);
		}

		public void SetJointPosMultiDof(int i, float[] q)
		{
			btMultiBody_setJointPosMultiDof(Native, i, q);
		}

		public void SetJointVel(int i, float qdot)
		{
			btMultiBody_setJointVel(Native, i, qdot);
		}

		public void SetJointVelMultiDof(int i, float[] qdot)
		{
			btMultiBody_setJointVelMultiDof(Native, i, qdot);
		}

		public void SetPosUpdated(bool updated)
		{
			btMultiBody_setPosUpdated(Native, updated);
		}

		public void SetupFixed(int linkIndex, float mass, Vector3 inertia, int parent,
			Quaternion rotParentToThis, Vector3 parentComToThisPivotOffset, Vector3 thisPivotToThisComOffset,
			bool deprecatedDisableParentCollision = true)
		{
			btMultiBody_setupFixed(Native, linkIndex, mass, ref inertia, parent,
				ref rotParentToThis, ref parentComToThisPivotOffset, ref thisPivotToThisComOffset,
				deprecatedDisableParentCollision);
		}

		public void SetupPlanar(int i, float mass, Vector3 inertia, int parent, Quaternion rotParentToThis,
			Vector3 rotationAxis, Vector3 parentComToThisComOffset, bool disableParentCollision = false)
		{
			btMultiBody_setupPlanar(Native, i, mass, ref inertia, parent, ref rotParentToThis,
				ref rotationAxis, ref parentComToThisComOffset, disableParentCollision);
		}

		public void SetupPrismatic(int i, float mass, Vector3 inertia, int parent,
			Quaternion rotParentToThis, Vector3 jointAxis, Vector3 parentComToThisPivotOffset,
			Vector3 thisPivotToThisComOffset, bool disableParentCollision)
		{
			btMultiBody_setupPrismatic(Native, i, mass, ref inertia, parent, ref rotParentToThis,
				ref jointAxis, ref parentComToThisPivotOffset, ref thisPivotToThisComOffset,
				disableParentCollision);
		}

		public void SetupRevolute(int linkIndex, float mass, Vector3 inertia, int parentIndex,
			Quaternion rotParentToThis, Vector3 jointAxis, Vector3 parentComToThisPivotOffset,
			Vector3 thisPivotToThisComOffset, bool disableParentCollision = false)
		{
			btMultiBody_setupRevolute(Native, linkIndex, mass, ref inertia, parentIndex,
				ref rotParentToThis, ref jointAxis, ref parentComToThisPivotOffset,
				ref thisPivotToThisComOffset, disableParentCollision);
		}

		public void SetupSpherical(int linkIndex, float mass, Vector3 inertia, int parent,
			Quaternion rotParentToThis, Vector3 parentComToThisPivotOffset, Vector3 thisPivotToThisComOffset,
			bool disableParentCollision = false)
		{
			btMultiBody_setupSpherical(Native, linkIndex, mass, ref inertia, parent,
				ref rotParentToThis, ref parentComToThisPivotOffset, ref thisPivotToThisComOffset,
				disableParentCollision);
		}

		public void StepPositionsMultiDof(float deltaTime, float[] pq = null, float[] pqd = null)
		{
			btMultiBody_stepPositionsMultiDof(Native, deltaTime, pq, pqd);
		}
		/*
		public void StepVelocitiesMultiDof(float deltaTime, AlignedObjectArray<float> scratchR,
			AlignedObjectArray<btVector3> scratchV, AlignedObjectArray<btMatrix3x3> scratchM,
			bool isConstraintPass = false)
		{
			btMultiBody_stepVelocitiesMultiDof(Native, deltaTime, scratchR.Native,
				scratchV.Native, scratchM.Native, isConstraintPass);
		}

		public void UpdateCollisionObjectWorldTransforms(btAlignedObjectArray<btQuaternion> scratchQ,
			AlignedObjectArray<btVector3> scratchM)
		{
			btMultiBody_updateCollisionObjectWorldTransforms(Native, scratchQ.Native,
				scratchM.Native);
		}
		*/
		public void WakeUp()
		{
			btMultiBody_wakeUp(Native);
		}

		public Vector3 WorldDirToLocal(int i, Vector3 vec)
		{
			Vector3 value;
			btMultiBody_worldDirToLocal(Native, i, ref vec, out value);
			return value;
		}

		public Vector3 WorldPosToLocal(int i, Vector3 vec)
		{
			Vector3 value;
			btMultiBody_worldPosToLocal(Native, i, ref vec, out value);
			return value;
		}

		public float AngularDamping
		{
			get => btMultiBody_getAngularDamping(Native);
			set => btMultiBody_setAngularDamping(Native, value);
		}

		public Vector3 AngularMomentum
		{
			get
			{
				Vector3 value;
				btMultiBody_getAngularMomentum(Native, out value);
				return value;
			}
		}

		public MultiBodyLinkCollider BaseCollider
		{
			get => CollisionObject.GetManaged(btMultiBody_getBaseCollider(Native)) as MultiBodyLinkCollider;
			set => btMultiBody_setBaseCollider(Native, value.Native);
		}

		public Vector3 BaseForce
		{
			get
			{
				Vector3 value;
				btMultiBody_getBaseForce(Native, out value);
				return value;
			}
		}

		public Vector3 BaseInertia
		{
			get
			{
				Vector3 value;
				btMultiBody_getBaseInertia(Native, out value);
				return value;
			}
			set => btMultiBody_setBaseInertia(Native, ref value);
		}

		public float BaseMass
		{
			get => btMultiBody_getBaseMass(Native);
			set => btMultiBody_setBaseMass(Native, value);
		}
		/*
		public char BaseName
		{
			get { return btMultiBody_getBaseName(Native); }
			set { btMultiBody_setBaseName(Native, value.Native); }
		}
		*/
		public Vector3 BaseOmega
		{
			get
			{
				Vector3 value;
				btMultiBody_getBaseOmega(Native, out value);
				return value;
			}
			set => btMultiBody_setBaseOmega(Native, ref value);
		}

		public Vector3 BasePosition
		{
			get
			{
				Vector3 value;
				btMultiBody_getBasePos(Native, out value);
				return value;
			}
			set => btMultiBody_setBasePos(Native, ref value);
		}

		public Vector3 BaseTorque
		{
			get
			{
				Vector3 value;
				btMultiBody_getBaseTorque(Native, out value);
				return value;
			}
		}

		public Vector3 BaseVelocity
		{
			get
			{
				Vector3 value;
				btMultiBody_getBaseVel(Native, out value);
				return value;
			}
			set => btMultiBody_setBaseVel(Native, ref value);
		}

		public Matrix BaseWorldTransform
		{
			get
			{
				Matrix value;
				btMultiBody_getBaseWorldTransform(Native, out value);
				return value;
			}
			set => btMultiBody_setBaseWorldTransform(Native, ref value);
		}

		public bool CanSleep
		{
			get => btMultiBody_getCanSleep(Native);
			set => btMultiBody_setCanSleep(Native, value);
		}

		public int CompanionId
		{
			get => btMultiBody_getCompanionId(Native);
			set => btMultiBody_setCompanionId(Native, value);
		}

		public bool HasFixedBase => btMultiBody_hasFixedBase(Native);
		public bool HasSelfCollision
		{
			get => btMultiBody_hasSelfCollision(Native);
			set => btMultiBody_setHasSelfCollision(Native, value);
		}

		public bool IsAwake => btMultiBody_isAwake(Native);

		public bool IsPosUpdated => btMultiBody_isPosUpdated(Native);

		public bool IsUsingGlobalVelocities
		{
			get => btMultiBody_isUsingGlobalVelocities(Native);
			set => btMultiBody_useGlobalVelocities(Native, value);
		}

		public bool IsUsingRK4Integration
		{
			get => btMultiBody_isUsingRK4Integration(Native);
			set => btMultiBody_useRK4Integration(Native, value);
		}

		public float KineticEnergy => btMultiBody_getKineticEnergy(Native);

		public float LinearDamping
		{
			get => btMultiBody_getLinearDamping(Native);
			set => btMultiBody_setLinearDamping(Native, value);
		}

		public float MaxAppliedImpulse
		{
			get => btMultiBody_getMaxAppliedImpulse(Native);
			set => btMultiBody_setMaxAppliedImpulse(Native, value);
		}

		public float MaxCoordinateVelocity
		{
			get => btMultiBody_getMaxCoordinateVelocity(Native);
			set => btMultiBody_setMaxCoordinateVelocity(Native, value);
		}

		public int NumDofs => btMultiBody_getNumDofs(Native);

		public int NumLinks
		{
			get => btMultiBody_getNumLinks(Native);
			set
			{
				btMultiBody_setNumLinks(Native, value);
				if (_links != null)
				{
					Array.Resize(ref _links, value);
				}
			}
		}

		public int NumPosVars => btMultiBody_getNumPosVars(Native);

		public bool UseGyroTerm
		{
			get => btMultiBody_getUseGyroTerm(Native);
			set => btMultiBody_setUseGyroTerm(Native, value);
		}

		public int UserIndex
		{
			get => btMultiBody_getUserIndex(Native);
			set => btMultiBody_setUserIndex(Native, value);
		}

		public int UserIndex2
		{
			get => btMultiBody_getUserIndex2(Native);
			set => btMultiBody_setUserIndex2(Native, value);
		}

		public IntPtr UserPointer
		{
			get => btMultiBody_getUserPointer(Native);
			set => btMultiBody_setUserPointer(Native, value);
		}
		/*
		public float VelocityVector
		{
			get { return btMultiBody_getVelocityVector(Native); }
		}
		*/
		public Quaternion WorldToBaseRot
		{
			get
			{
				Quaternion value;
				btMultiBody_getWorldToBaseRot(Native, out value);
				return value;
			}
			set => btMultiBody_setWorldToBaseRot(Native, ref value);
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
				btMultiBody_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~MultiBody()
		{
			Dispose(false);
		}
	}
}
