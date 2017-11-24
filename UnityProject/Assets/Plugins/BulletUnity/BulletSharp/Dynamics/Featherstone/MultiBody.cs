using System;
using System.Runtime.InteropServices;
using BulletSharp.Math;


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
			Native = UnsafeNativeMethods.btMultiBody_new(nLinks, mass, ref inertia, fixedBase, canSleep);
		}

		public void AddBaseConstraintForce(Vector3 f)
		{
			UnsafeNativeMethods.btMultiBody_addBaseConstraintForce(Native, ref f);
		}

		public void AddBaseConstraintTorque(Vector3 t)
		{
			UnsafeNativeMethods.btMultiBody_addBaseConstraintTorque(Native, ref t);
		}

		public void AddBaseForce(Vector3 f)
		{
			UnsafeNativeMethods.btMultiBody_addBaseForce(Native, ref f);
		}

		public void AddBaseTorque(Vector3 t)
		{
			UnsafeNativeMethods.btMultiBody_addBaseTorque(Native, ref t);
		}

		public void AddJointTorque(int i, float q)
		{
			UnsafeNativeMethods.btMultiBody_addJointTorque(Native, i, q);
		}

		public void AddJointTorqueMultiDof(int i, float[] q)
		{
			UnsafeNativeMethods.btMultiBody_addJointTorqueMultiDof(Native, i, q);
		}

		public void AddJointTorqueMultiDof(int i, int dof, float q)
		{
			UnsafeNativeMethods.btMultiBody_addJointTorqueMultiDof2(Native, i, dof, q);
		}

		public void AddLinkConstraintForce(int i, Vector3 f)
		{
			UnsafeNativeMethods.btMultiBody_addLinkConstraintForce(Native, i, ref f);
		}

		public void AddLinkConstraintTorque(int i, Vector3 t)
		{
			UnsafeNativeMethods.btMultiBody_addLinkConstraintTorque(Native, i, ref t);
		}

		public void AddLinkForce(int i, Vector3 f)
		{
			UnsafeNativeMethods.btMultiBody_addLinkForce(Native, i, ref f);
		}

		public void AddLinkTorque(int i, Vector3 t)
		{
			UnsafeNativeMethods.btMultiBody_addLinkTorque(Native, i, ref t);
		}
		/*

		public void ApplyDeltaVeeMultiDof(float deltaVee, float multiplier)
		{
			UnsafeNativeMethods.btMultiBody_applyDeltaVeeMultiDof(Native, deltaVee.Native, multiplier);
		}

		public void ApplyDeltaVeeMultiDof2(float deltaVee, float multiplier)
		{
			UnsafeNativeMethods.btMultiBody_applyDeltaVeeMultiDof2(Native, deltaVee.Native, multiplier);
		}

		public void CalcAccelerationDeltasMultiDof(float force, float output, AlignedObjectArray<float> scratchR,
			AlignedObjectArray<btVector3> scratchV)
		{
			UnsafeNativeMethods.btMultiBody_calcAccelerationDeltasMultiDof(Native, force.Native, output.Native,
				scratchR.Native, scratchV.Native);
		}
		*/
		public int CalculateSerializeBufferSize()
		{
			return UnsafeNativeMethods.btMultiBody_calculateSerializeBufferSize(Native);
		}

		public void CheckMotionAndSleepIfRequired(float timestep)
		{
			UnsafeNativeMethods.btMultiBody_checkMotionAndSleepIfRequired(Native, timestep);
		}

		public void ClearConstraintForces()
		{
			UnsafeNativeMethods.btMultiBody_clearConstraintForces(Native);
		}

		public void ClearForcesAndTorques()
		{
			UnsafeNativeMethods.btMultiBody_clearForcesAndTorques(Native);
		}

		public void ClearVelocities()
		{
			UnsafeNativeMethods.btMultiBody_clearVelocities(Native);
		}
		/*
		public void ComputeAccelerationsArticulatedBodyAlgorithmMultiDof(float deltaTime,
			AlignedObjectArray<float> scratchR, AlignedObjectArray<btVector3> scratchV,
			AlignedObjectArray<btMatrix3x3> scratchM, bool isConstraintPass = false)
		{
			UnsafeNativeMethods.btMultiBody_computeAccelerationsArticulatedBodyAlgorithmMultiDof(Native,
				deltaTime, scratchR.Native, scratchV.Native, scratchM.Native, isConstraintPass);
		}

		public void FillConstraintJacobianMultiDof(int link, Vector3 contactPoint,
			Vector3 normalAng, Vector3 normalLin, float jac, AlignedObjectArray<float> scratchR,
			AlignedObjectArray<btVector3> scratchV, AlignedObjectArray<btMatrix3x3> scratchM)
		{
			UnsafeNativeMethods.btMultiBody_fillConstraintJacobianMultiDof(Native, link, ref contactPoint,
				ref normalAng, ref normalLin, jac.Native, scratchR.Native, scratchV.Native,
				scratchM.Native);
		}

		public void FillContactJacobianMultiDof(int link, Vector3 contactPoint, Vector3 normal,
			float jac, AlignedObjectArray<float> scratchR, AlignedObjectArray<btVector3> scratchV,
			AlignedObjectArray<btMatrix3x3> scratchM)
		{
			UnsafeNativeMethods.btMultiBody_fillContactJacobianMultiDof(Native, link, ref contactPoint,
				ref normal, jac.Native, scratchR.Native, scratchV.Native, scratchM.Native);
		}
		*/
		public void FinalizeMultiDof()
		{
			UnsafeNativeMethods.btMultiBody_finalizeMultiDof(Native);
		}
		/*
		public void ForwardKinematics(btAlignedObjectArray<btQuaternion> scratchQ,
			AlignedObjectArray<btVector3> scratchM)
		{
			UnsafeNativeMethods.btMultiBody_forwardKinematics(Native, scratchQ.Native, scratchM.Native);
		}
		*/
		public float GetJointPos(int i)
		{
			return UnsafeNativeMethods.btMultiBody_getJointPos(Native, i);
		}
		/*
		public float GetJointPosMultiDof(int i)
		{
			return UnsafeNativeMethods.btMultiBody_getJointPosMultiDof(Native, i);
		}
		*/
		public float GetJointTorque(int i)
		{
			return UnsafeNativeMethods.btMultiBody_getJointTorque(Native, i);
		}
		/*
		public float GetJointTorqueMultiDof(int i)
		{
			return UnsafeNativeMethods.btMultiBody_getJointTorqueMultiDof(Native, i);
		}
		*/
		public float GetJointVel(int i)
		{
			return UnsafeNativeMethods.btMultiBody_getJointVel(Native, i);
		}
		/*
		public float GetJointVelMultiDof(int i)
		{
			return UnsafeNativeMethods.btMultiBody_getJointVelMultiDof(Native, i);
		}
		*/
		public MultiBodyLink GetLink(int index)
		{
			if (_links == null) {
				_links = new MultiBodyLink[NumLinks];
			}
			if (_links[index] == null) {
				_links[index] = new MultiBodyLink(UnsafeNativeMethods.btMultiBody_getLink(Native, index));
			}
			return _links[index];
		}

		public Vector3 GetLinkForce(int i)
		{
			Vector3 value;
			UnsafeNativeMethods.btMultiBody_getLinkForce(Native, i, out value);
			return value;
		}

		public Vector3 GetLinkInertia(int i)
		{
			Vector3 value;
			UnsafeNativeMethods.btMultiBody_getLinkInertia(Native, i, out value);
			return value;
		}

		public float GetLinkMass(int i)
		{
			return UnsafeNativeMethods.btMultiBody_getLinkMass(Native, i);
		}

		public Vector3 GetLinkTorque(int i)
		{
			Vector3 value;
			UnsafeNativeMethods.btMultiBody_getLinkTorque(Native, i, out value);
			return value;
		}

		public int GetParent(int linkNum)
		{
			return UnsafeNativeMethods.btMultiBody_getParent(Native, linkNum);
		}

		public Quaternion GetParentToLocalRot(int i)
		{
			Quaternion value;
			UnsafeNativeMethods.btMultiBody_getParentToLocalRot(Native, i, out value);
			return value;
		}

		public Vector3 GetRVector(int i)
		{
			Vector3 value;
			UnsafeNativeMethods.btMultiBody_getRVector(Native, i, out value);
			return value;
		}

		public void GoToSleep()
		{
			UnsafeNativeMethods.btMultiBody_goToSleep(Native);
		}

		public bool InternalNeedsJointFeedback()
		{
			return UnsafeNativeMethods.btMultiBody_internalNeedsJointFeedback(Native);
		}

		public Vector3 LocalDirToWorld(int i, Vector3 vec)
		{
			Vector3 value;
			UnsafeNativeMethods.btMultiBody_localDirToWorld(Native, i, ref vec, out value);
			return value;
		}

		public Matrix LocalFrameToWorld(int i, Matrix mat)
		{
			Matrix value;
			UnsafeNativeMethods.btMultiBody_localFrameToWorld(Native, i, ref mat, out value);
			return value;
		}

		public Vector3 LocalPosToWorld(int i, Vector3 vec)
		{
			Vector3 value;
			UnsafeNativeMethods.btMultiBody_localPosToWorld(Native, i, ref vec, out value);
			return value;
		}

		public void ProcessDeltaVeeMultiDof2()
		{
			UnsafeNativeMethods.btMultiBody_processDeltaVeeMultiDof2(Native);
		}

		public string Serialize(IntPtr dataBuffer, Serializer serializer)
		{
			return Marshal.PtrToStringAnsi(UnsafeNativeMethods.btMultiBody_serialize(Native, dataBuffer, serializer._native));
		}

		public void SetJointPos(int i, float q)
		{
			UnsafeNativeMethods.btMultiBody_setJointPos(Native, i, q);
		}

		public void SetJointPosMultiDof(int i, float[] q)
		{
			UnsafeNativeMethods.btMultiBody_setJointPosMultiDof(Native, i, q);
		}

		public void SetJointVel(int i, float qdot)
		{
			UnsafeNativeMethods.btMultiBody_setJointVel(Native, i, qdot);
		}

		public void SetJointVelMultiDof(int i, float[] qdot)
		{
			UnsafeNativeMethods.btMultiBody_setJointVelMultiDof(Native, i, qdot);
		}

		public void SetPosUpdated(bool updated)
		{
			UnsafeNativeMethods.btMultiBody_setPosUpdated(Native, updated);
		}

		public void SetupFixed(int linkIndex, float mass, Vector3 inertia, int parent,
			Quaternion rotParentToThis, Vector3 parentComToThisPivotOffset, Vector3 thisPivotToThisComOffset,
			bool deprecatedDisableParentCollision = true)
		{
			UnsafeNativeMethods.btMultiBody_setupFixed(Native, linkIndex, mass, ref inertia, parent,
				ref rotParentToThis, ref parentComToThisPivotOffset, ref thisPivotToThisComOffset,
				deprecatedDisableParentCollision);
		}

		public void SetupPlanar(int i, float mass, Vector3 inertia, int parent, Quaternion rotParentToThis,
			Vector3 rotationAxis, Vector3 parentComToThisComOffset, bool disableParentCollision = false)
		{
			UnsafeNativeMethods.btMultiBody_setupPlanar(Native, i, mass, ref inertia, parent, ref rotParentToThis,
				ref rotationAxis, ref parentComToThisComOffset, disableParentCollision);
		}

		public void SetupPrismatic(int i, float mass, Vector3 inertia, int parent,
			Quaternion rotParentToThis, Vector3 jointAxis, Vector3 parentComToThisPivotOffset,
			Vector3 thisPivotToThisComOffset, bool disableParentCollision)
		{
			UnsafeNativeMethods.btMultiBody_setupPrismatic(Native, i, mass, ref inertia, parent, ref rotParentToThis,
				ref jointAxis, ref parentComToThisPivotOffset, ref thisPivotToThisComOffset,
				disableParentCollision);
		}

		public void SetupRevolute(int linkIndex, float mass, Vector3 inertia, int parentIndex,
			Quaternion rotParentToThis, Vector3 jointAxis, Vector3 parentComToThisPivotOffset,
			Vector3 thisPivotToThisComOffset, bool disableParentCollision = false)
		{
			UnsafeNativeMethods.btMultiBody_setupRevolute(Native, linkIndex, mass, ref inertia, parentIndex,
				ref rotParentToThis, ref jointAxis, ref parentComToThisPivotOffset,
				ref thisPivotToThisComOffset, disableParentCollision);
		}

		public void SetupSpherical(int linkIndex, float mass, Vector3 inertia, int parent,
			Quaternion rotParentToThis, Vector3 parentComToThisPivotOffset, Vector3 thisPivotToThisComOffset,
			bool disableParentCollision = false)
		{
			UnsafeNativeMethods.btMultiBody_setupSpherical(Native, linkIndex, mass, ref inertia, parent,
				ref rotParentToThis, ref parentComToThisPivotOffset, ref thisPivotToThisComOffset,
				disableParentCollision);
		}

		public void StepPositionsMultiDof(float deltaTime, float[] pq = null, float[] pqd = null)
		{
			UnsafeNativeMethods.btMultiBody_stepPositionsMultiDof(Native, deltaTime, pq, pqd);
		}
		/*
		public void StepVelocitiesMultiDof(float deltaTime, AlignedObjectArray<float> scratchR,
			AlignedObjectArray<btVector3> scratchV, AlignedObjectArray<btMatrix3x3> scratchM,
			bool isConstraintPass = false)
		{
			UnsafeNativeMethods.btMultiBody_stepVelocitiesMultiDof(Native, deltaTime, scratchR.Native,
				scratchV.Native, scratchM.Native, isConstraintPass);
		}

		public void UpdateCollisionObjectWorldTransforms(btAlignedObjectArray<btQuaternion> scratchQ,
			AlignedObjectArray<btVector3> scratchM)
		{
			UnsafeNativeMethods.btMultiBody_updateCollisionObjectWorldTransforms(Native, scratchQ.Native,
				scratchM.Native);
		}
		*/
		public void WakeUp()
		{
			UnsafeNativeMethods.btMultiBody_wakeUp(Native);
		}

		public Vector3 WorldDirToLocal(int i, Vector3 vec)
		{
			Vector3 value;
			UnsafeNativeMethods.btMultiBody_worldDirToLocal(Native, i, ref vec, out value);
			return value;
		}

		public Vector3 WorldPosToLocal(int i, Vector3 vec)
		{
			Vector3 value;
			UnsafeNativeMethods.btMultiBody_worldPosToLocal(Native, i, ref vec, out value);
			return value;
		}

		public float AngularDamping
		{
			get { return  UnsafeNativeMethods.btMultiBody_getAngularDamping(Native);}
			set {  UnsafeNativeMethods.btMultiBody_setAngularDamping(Native, value);}
		}

		public Vector3 AngularMomentum
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btMultiBody_getAngularMomentum(Native, out value);
				return value;
			}
		}

		public MultiBodyLinkCollider BaseCollider
		{
			get { return  CollisionObject.GetManaged(UnsafeNativeMethods.btMultiBody_getBaseCollider(Native)) as MultiBodyLinkCollider;}
			set {  UnsafeNativeMethods.btMultiBody_setBaseCollider(Native, value.Native);}
		}

		public Vector3 BaseForce
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btMultiBody_getBaseForce(Native, out value);
				return value;
			}
		}

		public Vector3 BaseInertia
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btMultiBody_getBaseInertia(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btMultiBody_setBaseInertia(Native, ref value);}
		}

		public float BaseMass
		{
			get { return  UnsafeNativeMethods.btMultiBody_getBaseMass(Native);}
			set {  UnsafeNativeMethods.btMultiBody_setBaseMass(Native, value);}
		}
		/*
		public char BaseName
		{
			get { return UnsafeNativeMethods.btMultiBody_getBaseName(Native); }
			set { UnsafeNativeMethods.btMultiBody_setBaseName(Native, value.Native); }
		}
		*/
		public Vector3 BaseOmega
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btMultiBody_getBaseOmega(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btMultiBody_setBaseOmega(Native, ref value);}
		}

		public Vector3 BasePosition
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btMultiBody_getBasePos(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btMultiBody_setBasePos(Native, ref value);}
		}

		public Vector3 BaseTorque
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btMultiBody_getBaseTorque(Native, out value);
				return value;
			}
		}

		public Vector3 BaseVelocity
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btMultiBody_getBaseVel(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btMultiBody_setBaseVel(Native, ref value);}
		}

		public Matrix BaseWorldTransform
		{
			get
			{
				Matrix value;
				UnsafeNativeMethods.btMultiBody_getBaseWorldTransform(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btMultiBody_setBaseWorldTransform(Native, ref value);}
		}

		public bool CanSleep
		{
			get { return  UnsafeNativeMethods.btMultiBody_getCanSleep(Native);}
			set {  UnsafeNativeMethods.btMultiBody_setCanSleep(Native, value);}
		}

		public int CompanionId
		{
			get { return  UnsafeNativeMethods.btMultiBody_getCompanionId(Native);}
			set {  UnsafeNativeMethods.btMultiBody_setCompanionId(Native, value);}
		}

		public bool HasFixedBase{ get { return  UnsafeNativeMethods.btMultiBody_hasFixedBase(Native);} }
		public bool HasSelfCollision
		{
			get { return  UnsafeNativeMethods.btMultiBody_hasSelfCollision(Native);}
			set {  UnsafeNativeMethods.btMultiBody_setHasSelfCollision(Native, value);}
		}

		public bool IsAwake{ get { return  UnsafeNativeMethods.btMultiBody_isAwake(Native);} }

		public bool IsPosUpdated{ get { return  UnsafeNativeMethods.btMultiBody_isPosUpdated(Native);} }

		public bool IsUsingGlobalVelocities
		{
			get { return  UnsafeNativeMethods.btMultiBody_isUsingGlobalVelocities(Native);}
			set {  UnsafeNativeMethods.btMultiBody_useGlobalVelocities(Native, value);}
		}

		public bool IsUsingRK4Integration
		{
			get { return  UnsafeNativeMethods.btMultiBody_isUsingRK4Integration(Native);}
			set {  UnsafeNativeMethods.btMultiBody_useRK4Integration(Native, value);}
		}

		public float KineticEnergy{ get { return  UnsafeNativeMethods.btMultiBody_getKineticEnergy(Native);} }

		public float LinearDamping
		{
			get { return  UnsafeNativeMethods.btMultiBody_getLinearDamping(Native);}
			set {  UnsafeNativeMethods.btMultiBody_setLinearDamping(Native, value);}
		}

		public float MaxAppliedImpulse
		{
			get { return  UnsafeNativeMethods.btMultiBody_getMaxAppliedImpulse(Native);}
			set {  UnsafeNativeMethods.btMultiBody_setMaxAppliedImpulse(Native, value);}
		}

		public float MaxCoordinateVelocity
		{
			get { return  UnsafeNativeMethods.btMultiBody_getMaxCoordinateVelocity(Native);}
			set {  UnsafeNativeMethods.btMultiBody_setMaxCoordinateVelocity(Native, value);}
		}

		public int NumDofs{ get { return  UnsafeNativeMethods.btMultiBody_getNumDofs(Native);} }

		public int NumLinks
		{
			get { return  UnsafeNativeMethods.btMultiBody_getNumLinks(Native);}
			set
			{
				UnsafeNativeMethods.btMultiBody_setNumLinks(Native, value);
				if (_links != null)
				{
					Array.Resize(ref _links, value);
				}
			}
		}

		public int NumPosVars{ get { return  UnsafeNativeMethods.btMultiBody_getNumPosVars(Native);} }

		public bool UseGyroTerm
		{
			get { return  UnsafeNativeMethods.btMultiBody_getUseGyroTerm(Native);}
			set {  UnsafeNativeMethods.btMultiBody_setUseGyroTerm(Native, value);}
		}

		public int UserIndex
		{
			get { return  UnsafeNativeMethods.btMultiBody_getUserIndex(Native);}
			set {  UnsafeNativeMethods.btMultiBody_setUserIndex(Native, value);}
		}

		public int UserIndex2
		{
			get { return  UnsafeNativeMethods.btMultiBody_getUserIndex2(Native);}
			set {  UnsafeNativeMethods.btMultiBody_setUserIndex2(Native, value);}
		}

		public IntPtr UserPointer
		{
			get { return  UnsafeNativeMethods.btMultiBody_getUserPointer(Native);}
			set {  UnsafeNativeMethods.btMultiBody_setUserPointer(Native, value);}
		}
		/*
		public float VelocityVector
		{
			get { return UnsafeNativeMethods.btMultiBody_getVelocityVector(Native); }
		}
		*/
		public Quaternion WorldToBaseRot
		{
			get
			{
				Quaternion value;
				UnsafeNativeMethods.btMultiBody_getWorldToBaseRot(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btMultiBody_setWorldToBaseRot(Native, ref value);}
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
				UnsafeNativeMethods.btMultiBody_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~MultiBody()
		{
			Dispose(false);
		}
	}
}
