using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class MultiBody : IDisposable
	{
		internal IntPtr _native;
        MultiBodyLink[] _links;

		internal MultiBody(IntPtr native)
		{
			_native = native;
		}

		public MultiBody(int nLinks, float mass, Vector3 inertia, bool fixedBase, bool canSleep)
		{
			_native = btMultiBody_new(nLinks, mass, ref inertia, fixedBase, canSleep);
		}

		public void AddBaseConstraintForce(Vector3 f)
		{
			btMultiBody_addBaseConstraintForce(_native, ref f);
		}

		public void AddBaseConstraintTorque(Vector3 t)
		{
			btMultiBody_addBaseConstraintTorque(_native, ref t);
		}

		public void AddBaseForce(Vector3 f)
		{
			btMultiBody_addBaseForce(_native, ref f);
		}

		public void AddBaseTorque(Vector3 t)
		{
			btMultiBody_addBaseTorque(_native, ref t);
		}

		public void AddJointTorque(int i, float q)
		{
			btMultiBody_addJointTorque(_native, i, q);
		}

		public void AddJointTorqueMultiDof(int i, float[] q)
		{
			btMultiBody_addJointTorqueMultiDof(_native, i, q);
		}

		public void AddJointTorqueMultiDof(int i, int dof, float q)
		{
			btMultiBody_addJointTorqueMultiDof2(_native, i, dof, q);
		}

		public void AddLinkConstraintForce(int i, Vector3 f)
		{
			btMultiBody_addLinkConstraintForce(_native, i, ref f);
		}

		public void AddLinkConstraintTorque(int i, Vector3 t)
		{
			btMultiBody_addLinkConstraintTorque(_native, i, ref t);
		}

		public void AddLinkForce(int i, Vector3 f)
		{
			btMultiBody_addLinkForce(_native, i, ref f);
		}

		public void AddLinkTorque(int i, Vector3 t)
		{
			btMultiBody_addLinkTorque(_native, i, ref t);
		}
        /*

		public void ApplyDeltaVeeMultiDof(float deltaVee, float multiplier)
		{
			btMultiBody_applyDeltaVeeMultiDof(_native, deltaVee._native, multiplier);
		}

		public void ApplyDeltaVeeMultiDof2(float deltaVee, float multiplier)
		{
			btMultiBody_applyDeltaVeeMultiDof2(_native, deltaVee._native, multiplier);
		}

		public void CalcAccelerationDeltasMultiDof(float force, float output, AlignedObjectArray scratchR, AlignedObjectArray scratchV)
		{
			btMultiBody_calcAccelerationDeltasMultiDof(_native, force._native, output._native, scratchR._native, scratchV._native);
		}
        */
		public int CalculateSerializeBufferSize()
		{
			return btMultiBody_calculateSerializeBufferSize(_native);
		}

		public void CheckMotionAndSleepIfRequired(float timestep)
		{
			btMultiBody_checkMotionAndSleepIfRequired(_native, timestep);
		}

		public void ClearConstraintForces()
		{
			btMultiBody_clearConstraintForces(_native);
		}

		public void ClearForcesAndTorques()
		{
			btMultiBody_clearForcesAndTorques(_native);
		}

		public void ClearVelocities()
		{
			btMultiBody_clearVelocities(_native);
		}
        /*
		public void ComputeAccelerationsArticulatedBodyAlgorithmMultiDof(float deltaTime, AlignedObjectArray scratchR, AlignedObjectArray scratchV, AlignedObjectArray scratchM)
		{
			btMultiBody_computeAccelerationsArticulatedBodyAlgorithmMultiDof(_native, deltaTime, scratchR._native, scratchV._native, scratchM._native);
		}

		public void ComputeAccelerationsArticulatedBodyAlgorithmMultiDof(float deltaTime, AlignedObjectArray scratchR, AlignedObjectArray scratchV, AlignedObjectArray scratchM, bool isConstraintPass)
		{
			btMultiBody_computeAccelerationsArticulatedBodyAlgorithmMultiDof2(_native, deltaTime, scratchR._native, scratchV._native, scratchM._native, isConstraintPass);
		}

		public void FillConstraintJacobianMultiDof(int link, Vector3 contactPoint, Vector3 normalAng, Vector3 normalLin, float jac, AlignedObjectArray scratchR, AlignedObjectArray scratchV, AlignedObjectArray scratchM)
		{
			btMultiBody_fillConstraintJacobianMultiDof(_native, link, ref contactPoint, ref normalAng, ref normalLin, jac._native, scratchR._native, scratchV._native, scratchM._native);
		}

		public void FillContactJacobianMultiDof(int link, Vector3 contactPoint, Vector3 normal, float jac, AlignedObjectArray scratchR, AlignedObjectArray scratchV, AlignedObjectArray scratchM)
		{
			btMultiBody_fillContactJacobianMultiDof(_native, link, ref contactPoint, ref normal, jac._native, scratchR._native, scratchV._native, scratchM._native);
		}
        */
		public void FinalizeMultiDof()
		{
			btMultiBody_finalizeMultiDof(_native);
		}
        /*
		public void ForwardKinematics(AlignedObjectArray scratchQ, AlignedObjectArray scratchM)
		{
			btMultiBody_forwardKinematics(_native, scratchQ._native, scratchM._native);
		}
        */
		public float GetJointPos(int i)
		{
			return btMultiBody_getJointPos(_native, i);
		}
        /*
		public float GetJointPosMultiDof(int i)
		{
			return btMultiBody_getJointPosMultiDof(_native, i);
		}
        */
		public float GetJointTorque(int i)
		{
			return btMultiBody_getJointTorque(_native, i);
		}
        /*
		public float GetJointTorqueMultiDof(int i)
		{
			return btMultiBody_getJointTorqueMultiDof(_native, i);
		}
        */
		public float GetJointVel(int i)
		{
			return btMultiBody_getJointVel(_native, i);
		}
        /*
		public float GetJointVelMultiDof(int i)
		{
			return btMultiBody_getJointVelMultiDof(_native, i);
		}
        */
		public MultiBodyLink GetLink(int index)
		{
            if (_links == null) {
                _links = new MultiBodyLink[NumLinks];
            }
            if (_links[index] == null) {
                _links[index] = new MultiBodyLink(btMultiBody_getLink(_native, index));
            }
            return _links[index];
		}

		public Vector3 GetLinkForce(int i)
		{
			Vector3 value;
			btMultiBody_getLinkForce(_native, i, out value);
			return value;
		}

		public Vector3 GetLinkInertia(int i)
		{
			Vector3 value;
			btMultiBody_getLinkInertia(_native, i, out value);
			return value;
		}

		public float GetLinkMass(int i)
		{
			return btMultiBody_getLinkMass(_native, i);
		}

		public Vector3 GetLinkTorque(int i)
		{
			Vector3 value;
			btMultiBody_getLinkTorque(_native, i, out value);
			return value;
		}

		public int GetParent(int linkNum)
		{
			return btMultiBody_getParent(_native, linkNum);
		}

		public Quaternion GetParentToLocalRot(int i)
		{
			Quaternion value;
			btMultiBody_getParentToLocalRot(_native, i, out value);
			return value;
		}

		public Vector3 GetRVector(int i)
		{
			Vector3 value;
			btMultiBody_getRVector(_native, i, out value);
			return value;
		}

		public void GoToSleep()
		{
			btMultiBody_goToSleep(_native);
		}

		public bool InternalNeedsJointFeedback()
		{
			return btMultiBody_internalNeedsJointFeedback(_native);
		}

		public Vector3 LocalDirToWorld(int i, Vector3 vec)
		{
			Vector3 value;
			btMultiBody_localDirToWorld(_native, i, ref vec, out value);
			return value;
		}

		public Vector3 LocalPosToWorld(int i, Vector3 vec)
		{
			Vector3 value;
			btMultiBody_localPosToWorld(_native, i, ref vec, out value);
			return value;
		}

		public void ProcessDeltaVeeMultiDof2()
		{
			btMultiBody_processDeltaVeeMultiDof2(_native);
		}

		public string Serialize(IntPtr dataBuffer, Serializer serializer)
		{
			return Marshal.PtrToStringAnsi(btMultiBody_serialize(_native, dataBuffer, serializer._native));
		}

		public void SetJointPos(int i, float q)
		{
			btMultiBody_setJointPos(_native, i, q);
		}

		public void SetJointPosMultiDof(int i, float[] q)
		{
			btMultiBody_setJointPosMultiDof(_native, i, q);
		}

		public void SetJointVel(int i, float qdot)
		{
			btMultiBody_setJointVel(_native, i, qdot);
		}

		public void SetJointVelMultiDof(int i, float[] qdot)
		{
			btMultiBody_setJointVelMultiDof(_native, i, qdot);
		}

		public void SetPosUpdated(bool updated)
		{
			btMultiBody_setPosUpdated(_native, updated);
		}

		public void SetupFixed(int linkIndex, float mass, Vector3 inertia, int parent, Quaternion rotParentToThis, Vector3 parentComToThisPivotOffset, Vector3 thisPivotToThisComOffset)
		{
			btMultiBody_setupFixed(_native, linkIndex, mass, ref inertia, parent, ref rotParentToThis, ref parentComToThisPivotOffset, ref thisPivotToThisComOffset);
		}

		public void SetupPlanar(int i, float mass, Vector3 inertia, int parent, Quaternion rotParentToThis, Vector3 rotationAxis, Vector3 parentComToThisComOffset)
		{
			btMultiBody_setupPlanar(_native, i, mass, ref inertia, parent, ref rotParentToThis, ref rotationAxis, ref parentComToThisComOffset);
		}

		public void SetupPlanar(int i, float mass, Vector3 inertia, int parent, Quaternion rotParentToThis, Vector3 rotationAxis, Vector3 parentComToThisComOffset, bool disableParentCollision)
		{
			btMultiBody_setupPlanar2(_native, i, mass, ref inertia, parent, ref rotParentToThis, ref rotationAxis, ref parentComToThisComOffset, disableParentCollision);
		}

		public void SetupPrismatic(int i, float mass, Vector3 inertia, int parent, Quaternion rotParentToThis, Vector3 jointAxis, Vector3 parentComToThisPivotOffset, Vector3 thisPivotToThisComOffset, bool disableParentCollision)
		{
			btMultiBody_setupPrismatic(_native, i, mass, ref inertia, parent, ref rotParentToThis, ref jointAxis, ref parentComToThisPivotOffset, ref thisPivotToThisComOffset, disableParentCollision);
		}

		public void SetupRevolute(int linkIndex, float mass, Vector3 inertia, int parentIndex, Quaternion rotParentToThis, Vector3 jointAxis, Vector3 parentComToThisPivotOffset, Vector3 thisPivotToThisComOffset)
		{
			btMultiBody_setupRevolute(_native, linkIndex, mass, ref inertia, parentIndex, ref rotParentToThis, ref jointAxis, ref parentComToThisPivotOffset, ref thisPivotToThisComOffset);
		}

		public void SetupRevolute(int linkIndex, float mass, Vector3 inertia, int parentIndex, Quaternion rotParentToThis, Vector3 jointAxis, Vector3 parentComToThisPivotOffset, Vector3 thisPivotToThisComOffset, bool disableParentCollision)
		{
			btMultiBody_setupRevolute2(_native, linkIndex, mass, ref inertia, parentIndex, ref rotParentToThis, ref jointAxis, ref parentComToThisPivotOffset, ref thisPivotToThisComOffset, disableParentCollision);
		}

		public void SetupSpherical(int linkIndex, float mass, Vector3 inertia, int parent, Quaternion rotParentToThis, Vector3 parentComToThisPivotOffset, Vector3 thisPivotToThisComOffset)
		{
			btMultiBody_setupSpherical(_native, linkIndex, mass, ref inertia, parent, ref rotParentToThis, ref parentComToThisPivotOffset, ref thisPivotToThisComOffset);
		}

		public void SetupSpherical(int linkIndex, float mass, Vector3 inertia, int parent, Quaternion rotParentToThis, Vector3 parentComToThisPivotOffset, Vector3 thisPivotToThisComOffset, bool disableParentCollision)
		{
			btMultiBody_setupSpherical2(_native, linkIndex, mass, ref inertia, parent, ref rotParentToThis, ref parentComToThisPivotOffset, ref thisPivotToThisComOffset, disableParentCollision);
		}

		public void StepPositionsMultiDof(float deltaTime)
		{
			btMultiBody_stepPositionsMultiDof(_native, deltaTime);
		}

		public void StepPositionsMultiDof(float deltaTime, float[] pq)
		{
			btMultiBody_stepPositionsMultiDof2(_native, deltaTime, pq);
		}

		public void StepPositionsMultiDof(float deltaTime, float[] pq, float[] pqd)
		{
			btMultiBody_stepPositionsMultiDof3(_native, deltaTime, pq, pqd);
		}
        /*
		public void UpdateCollisionObjectWorldTransforms(AlignedObjectArray scratchQ, AlignedObjectArray scratchM)
		{
			btMultiBody_updateCollisionObjectWorldTransforms(_native, scratchQ._native, scratchM._native);
		}
        */
		public void WakeUp()
		{
			btMultiBody_wakeUp(_native);
		}

		public Vector3 WorldDirToLocal(int i, Vector3 vec)
		{
			Vector3 value;
			btMultiBody_worldDirToLocal(_native, i, ref vec, out value);
			return value;
		}

		public Vector3 WorldPosToLocal(int i, Vector3 vec)
		{
			Vector3 value;
			btMultiBody_worldPosToLocal(_native, i, ref vec, out value);
			return value;
		}

		public float AngularDamping
		{
			get { return btMultiBody_getAngularDamping(_native); }
			set { btMultiBody_setAngularDamping(_native, value); }
		}

		public Vector3 AngularMomentum
		{
			get
			{
				Vector3 value;
				btMultiBody_getAngularMomentum(_native, out value);
				return value;
			}
		}

		public MultiBodyLinkCollider BaseCollider
		{
            get { return CollisionObject.GetManaged(btMultiBody_getBaseCollider(_native)) as MultiBodyLinkCollider; }
			set { btMultiBody_setBaseCollider(_native, value._native); }
		}

		public Vector3 BaseForce
		{
			get
			{
				Vector3 value;
				btMultiBody_getBaseForce(_native, out value);
				return value;
			}
		}

		public Vector3 BaseInertia
		{
			get
			{
				Vector3 value;
				btMultiBody_getBaseInertia(_native, out value);
				return value;
			}
			set { btMultiBody_setBaseInertia(_native, ref value); }
		}

		public float BaseMass
		{
			get { return btMultiBody_getBaseMass(_native); }
			set { btMultiBody_setBaseMass(_native, value); }
		}
        /*
		public char BaseName
		{
			get { return btMultiBody_getBaseName(_native); }
			set { btMultiBody_setBaseName(_native, value._native); }
		}
        */
		public Vector3 BaseOmega
		{
			get
			{
				Vector3 value;
				btMultiBody_getBaseOmega(_native, out value);
				return value;
			}
			set { btMultiBody_setBaseOmega(_native, ref value); }
		}

		public Vector3 BasePosition
		{
			get
			{
				Vector3 value;
				btMultiBody_getBasePos(_native, out value);
				return value;
			}
			set { btMultiBody_setBasePos(_native, ref value); }
		}

		public Vector3 BaseTorque
		{
			get
			{
				Vector3 value;
				btMultiBody_getBaseTorque(_native, out value);
				return value;
			}
		}

		public Vector3 BaseVelocity
		{
			get
			{
				Vector3 value;
				btMultiBody_getBaseVel(_native, out value);
				return value;
			}
			set { btMultiBody_setBaseVel(_native, ref value); }
		}

		public Matrix BaseWorldTransform
		{
			get
			{
				Matrix value;
				btMultiBody_getBaseWorldTransform(_native, out value);
				return value;
			}
			set { btMultiBody_setBaseWorldTransform(_native, ref value); }
		}

		public bool CanSleep
		{
			get { return btMultiBody_getCanSleep(_native); }
			set { btMultiBody_setCanSleep(_native, value); }
		}

		public int CompanionId
		{
			get { return btMultiBody_getCompanionId(_native); }
			set { btMultiBody_setCompanionId(_native, value); }
		}

		public bool HasFixedBase
		{
			get { return btMultiBody_hasFixedBase(_native); }
		}

		public bool HasSelfCollision
		{
			get { return btMultiBody_hasSelfCollision(_native); }
			set { btMultiBody_setHasSelfCollision(_native, value); }
		}

		public bool IsAwake
		{
			get { return btMultiBody_isAwake(_native); }
		}

		public bool IsPosUpdated
		{
			get { return btMultiBody_isPosUpdated(_native); }
		}

		public bool IsUsingGlobalVelocities
		{
			get { return btMultiBody_isUsingGlobalVelocities(_native); }
            set { btMultiBody_useGlobalVelocities(_native, value); }
		}

		public bool IsUsingRK4Integration
		{
			get { return btMultiBody_isUsingRK4Integration(_native); }
            set { btMultiBody_useRK4Integration(_native, value); }
		}

		public float KineticEnergy
		{
			get { return btMultiBody_getKineticEnergy(_native); }
		}

		public float LinearDamping
		{
			get { return btMultiBody_getLinearDamping(_native); }
			set { btMultiBody_setLinearDamping(_native, value); }
		}

		public float MaxAppliedImpulse
		{
			get { return btMultiBody_getMaxAppliedImpulse(_native); }
			set { btMultiBody_setMaxAppliedImpulse(_native, value); }
		}

		public float MaxCoordinateVelocity
		{
			get { return btMultiBody_getMaxCoordinateVelocity(_native); }
			set { btMultiBody_setMaxCoordinateVelocity(_native, value); }
		}

		public int NumDofs
		{
			get { return btMultiBody_getNumDofs(_native); }
		}

		public int NumLinks
		{
			get { return btMultiBody_getNumLinks(_native); }
            set
            {
                btMultiBody_setNumLinks(_native, value);
                if (_links != null)
                {
                    Array.Resize(ref _links, value);
                }
            }
		}

		public int NumPosVars
		{
			get { return btMultiBody_getNumPosVars(_native); }
		}

		public bool UseGyroTerm
		{
			get { return btMultiBody_getUseGyroTerm(_native); }
			set { btMultiBody_setUseGyroTerm(_native, value); }
		}
        /*
		public float VelocityVector
		{
			get { return btMultiBody_getVelocityVector(_native); }
		}
        */
		public Quaternion WorldToBaseRot
		{
			get
			{
				Quaternion value;
				btMultiBody_getWorldToBaseRot(_native, out value);
				return value;
			}
			set { btMultiBody_setWorldToBaseRot(_native, ref value); }
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
				btMultiBody_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~MultiBody()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultiBody_new(int n_links, float mass, [In] ref Vector3 inertia, bool fixedBase, bool canSleep);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_addBaseConstraintForce(IntPtr obj, [In] ref Vector3 f);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_addBaseConstraintTorque(IntPtr obj, [In] ref Vector3 t);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_addBaseForce(IntPtr obj, [In] ref Vector3 f);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_addBaseTorque(IntPtr obj, [In] ref Vector3 t);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_addJointTorque(IntPtr obj, int i, float Q);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_addJointTorqueMultiDof(IntPtr obj, int i, float[] Q);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_addJointTorqueMultiDof2(IntPtr obj, int i, int dof, float Q);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_addLinkConstraintForce(IntPtr obj, int i, [In] ref Vector3 f);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_addLinkConstraintTorque(IntPtr obj, int i, [In] ref Vector3 t);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_addLinkForce(IntPtr obj, int i, [In] ref Vector3 f);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_addLinkTorque(IntPtr obj, int i, [In] ref Vector3 t);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_applyDeltaVeeMultiDof(IntPtr obj, float[] delta_vee, float multiplier);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_applyDeltaVeeMultiDof2(IntPtr obj, float[] delta_vee, float multiplier);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_calcAccelerationDeltasMultiDof(IntPtr obj, float[] force, float[] output, IntPtr scratch_r, IntPtr scratch_v);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btMultiBody_calculateSerializeBufferSize(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_checkMotionAndSleepIfRequired(IntPtr obj, float timestep);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_clearConstraintForces(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_clearForcesAndTorques(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_clearVelocities(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_computeAccelerationsArticulatedBodyAlgorithmMultiDof(IntPtr obj, float dt, IntPtr scratch_r, IntPtr scratch_v, IntPtr scratch_m);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_computeAccelerationsArticulatedBodyAlgorithmMultiDof2(IntPtr obj, float dt, IntPtr scratch_r, IntPtr scratch_v, IntPtr scratch_m, bool isConstraintPass);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_fillConstraintJacobianMultiDof(IntPtr obj, int link, [In] ref Vector3 contact_point, [In] ref Vector3 normal_ang, [In] ref Vector3 normal_lin, float[] jac, IntPtr scratch_r, IntPtr scratch_v, IntPtr scratch_m);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_fillContactJacobianMultiDof(IntPtr obj, int link, [In] ref Vector3 contact_point, [In] ref Vector3 normal, float[] jac, IntPtr scratch_r, IntPtr scratch_v, IntPtr scratch_m);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_finalizeMultiDof(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_forwardKinematics(IntPtr obj, IntPtr scratch_q, IntPtr scratch_m);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultiBody_getAngularDamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_getAngularMomentum(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultiBody_getBaseCollider(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_getBaseForce(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_getBaseInertia(IntPtr obj, [Out] out Vector3 inertia);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultiBody_getBaseMass(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultiBody_getBaseName(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_getBaseOmega(IntPtr obj, [Out] out Vector3 omega);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_getBasePos(IntPtr obj, [Out] out Vector3 pos);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_getBaseTorque(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btMultiBody_getBaseVel(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_getBaseWorldTransform(IntPtr obj, [Out] out Matrix tr);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btMultiBody_getCanSleep(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btMultiBody_getCompanionId(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultiBody_getJointPos(IntPtr obj, int i);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultiBody_getJointPosMultiDof(IntPtr obj, int i);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultiBody_getJointTorque(IntPtr obj, int i);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultiBody_getJointTorqueMultiDof(IntPtr obj, int i);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultiBody_getJointVel(IntPtr obj, int i);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultiBody_getJointVelMultiDof(IntPtr obj, int i);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultiBody_getKineticEnergy(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultiBody_getLinearDamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultiBody_getLink(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_getLinkForce(IntPtr obj, int i, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_getLinkInertia(IntPtr obj, int i, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultiBody_getLinkMass(IntPtr obj, int i);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_getLinkTorque(IntPtr obj, int i, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultiBody_getMaxAppliedImpulse(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultiBody_getMaxCoordinateVelocity(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btMultiBody_getNumDofs(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btMultiBody_getNumLinks(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btMultiBody_getNumPosVars(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btMultiBody_getParent(IntPtr obj, int link_num);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_getParentToLocalRot(IntPtr obj, int i, [Out] out Quaternion value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_getRVector(IntPtr obj, int i, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btMultiBody_getUseGyroTerm(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultiBody_getVelocityVector(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_getWorldToBaseRot(IntPtr obj, [Out] out Quaternion rot);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_goToSleep(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btMultiBody_hasFixedBase(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btMultiBody_hasSelfCollision(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btMultiBody_internalNeedsJointFeedback(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btMultiBody_isAwake(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btMultiBody_isPosUpdated(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btMultiBody_isUsingGlobalVelocities(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btMultiBody_isUsingRK4Integration(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_localDirToWorld(IntPtr obj, int i, [In] ref Vector3 vec, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_localPosToWorld(IntPtr obj, int i, [In] ref Vector3 vec, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_processDeltaVeeMultiDof2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultiBody_serialize(IntPtr obj, IntPtr dataBuffer, IntPtr serializer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setAngularDamping(IntPtr obj, float damp);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setBaseCollider(IntPtr obj, IntPtr collider);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setBaseInertia(IntPtr obj, [In] ref Vector3 inertia);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setBaseMass(IntPtr obj, float mass);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setBaseName(IntPtr obj, IntPtr name);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setBaseOmega(IntPtr obj, [In] ref Vector3 omega);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setBasePos(IntPtr obj, [In] ref Vector3 pos);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setBaseVel(IntPtr obj, [In] ref Vector3 vel);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setBaseWorldTransform(IntPtr obj, [In] ref Matrix tr);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setCanSleep(IntPtr obj, bool canSleep);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setCompanionId(IntPtr obj, int id);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setHasSelfCollision(IntPtr obj, bool hasSelfCollision);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setJointPos(IntPtr obj, int i, float q);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setJointPosMultiDof(IntPtr obj, int i, float[] q);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setJointVel(IntPtr obj, int i, float qdot);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setJointVelMultiDof(IntPtr obj, int i, float[] qdot);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setLinearDamping(IntPtr obj, float damp);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setMaxAppliedImpulse(IntPtr obj, float maxImp);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setMaxCoordinateVelocity(IntPtr obj, float maxVel);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setNumLinks(IntPtr obj, int numLinks);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setPosUpdated(IntPtr obj, bool updated);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setupFixed(IntPtr obj, int linkIndex, float mass, [In] ref Vector3 inertia, int parent, [In] ref Quaternion rotParentToThis, [In] ref Vector3 parentComToThisPivotOffset, [In] ref Vector3 thisPivotToThisComOffset);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setupPlanar(IntPtr obj, int i, float mass, [In] ref Vector3 inertia, int parent, [In] ref Quaternion rotParentToThis, [In] ref Vector3 rotationAxis, [In] ref Vector3 parentComToThisComOffset);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setupPlanar2(IntPtr obj, int i, float mass, [In] ref Vector3 inertia, int parent, [In] ref Quaternion rotParentToThis, [In] ref Vector3 rotationAxis, [In] ref Vector3 parentComToThisComOffset, bool disableParentCollision);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setupPrismatic(IntPtr obj, int i, float mass, [In] ref Vector3 inertia, int parent, [In] ref Quaternion rotParentToThis, [In] ref Vector3 jointAxis, [In] ref Vector3 parentComToThisPivotOffset, [In] ref Vector3 thisPivotToThisComOffset, bool disableParentCollision);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setupRevolute(IntPtr obj, int linkIndex, float mass, [In] ref Vector3 inertia, int parentIndex, [In] ref Quaternion rotParentToThis, [In] ref Vector3 jointAxis, [In] ref Vector3 parentComToThisPivotOffset, [In] ref Vector3 thisPivotToThisComOffset);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setupRevolute2(IntPtr obj, int linkIndex, float mass, [In] ref Vector3 inertia, int parentIndex, [In] ref Quaternion rotParentToThis, [In] ref Vector3 jointAxis, [In] ref Vector3 parentComToThisPivotOffset, [In] ref Vector3 thisPivotToThisComOffset, bool disableParentCollision);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setupSpherical(IntPtr obj, int linkIndex, float mass, [In] ref Vector3 inertia, int parent, [In] ref Quaternion rotParentToThis, [In] ref Vector3 parentComToThisPivotOffset, [In] ref Vector3 thisPivotToThisComOffset);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setupSpherical2(IntPtr obj, int linkIndex, float mass, [In] ref Vector3 inertia, int parent, [In] ref Quaternion rotParentToThis, [In] ref Vector3 parentComToThisPivotOffset, [In] ref Vector3 thisPivotToThisComOffset, bool disableParentCollision);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setUseGyroTerm(IntPtr obj, bool useGyro);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setWorldToBaseRot(IntPtr obj, [In] ref Quaternion rot);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_stepPositionsMultiDof(IntPtr obj, float dt);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_stepPositionsMultiDof2(IntPtr obj, float dt, float[] pq);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_stepPositionsMultiDof3(IntPtr obj, float dt, float[] pq, float[] pqd);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_updateCollisionObjectWorldTransforms(IntPtr obj, IntPtr scratch_q, IntPtr scratch_m);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_useGlobalVelocities(IntPtr obj, bool use);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_useRK4Integration(IntPtr obj, bool use);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_wakeUp(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_worldDirToLocal(IntPtr obj, int i, [In] ref Vector3 vec, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_worldPosToLocal(IntPtr obj, int i, [In] ref Vector3 vec, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_delete(IntPtr obj);
	}
}
