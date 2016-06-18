using System;
using UnityEngine;
using BulletSharp;
using BulletSharp.Math;
using System.Collections;
using BulletUnity.Debugging;

namespace BulletUnity {
    /*
        todo 
        continuous collision detection ccd
        */
	[AddComponentMenu("Physics Bullet/RigidBody")]
    public class BRigidBody : BCollisionObject, IDisposable {
        BGameObjectMotionState m_motionState;

        RigidBody m_rigidBody
        {
            get { return (RigidBody) m_collisionObject; }
            set { m_collisionObject = value; }
        }


        BulletSharp.Math.Vector3 _localInertia = BulletSharp.Math.Vector3.Zero;
        public BulletSharp.Math.Vector3 localInertia {
            get {
                return _localInertia;
            }
        }

        public bool isDynamic()
        {
            return (m_collisionFlags & BulletSharp.CollisionFlags.StaticObject) != BulletSharp.CollisionFlags.StaticObject
				&& (m_collisionFlags & BulletSharp.CollisionFlags.KinematicObject) != BulletSharp.CollisionFlags.KinematicObject;
        }

        [SerializeField]
        float _friction = .5f;
        public float friction
        {
            get { return _friction; }
            set {
                if (m_collisionObject != null && _friction != value)
                {
                    m_collisionObject.Friction = value;
                }
                _friction = value;
            }
        }

        [SerializeField]
        float _rollingFriction = 0f;
        public float rollingFriction
        {
            get { return _rollingFriction; }
            set {
                if (m_collisionObject != null && _rollingFriction != value)
                {
                    m_collisionObject.RollingFriction = value;
                }
                _rollingFriction = value;
            }
        }

        [SerializeField]
        float _linearDamping = 0f;
        public float linearDamping
        {
            get { return _linearDamping; }
            set {
                if (m_collisionObject != null && _linearDamping != value)
                {
                    m_rigidBody.SetDamping(value,_angularDamping);
                }
                _linearDamping = value;
            }
        }

        [SerializeField]
        float _angularDamping = 0f;
        public float angularDamping
        {
            get { return _angularDamping; }
            set {
                if (m_collisionObject != null && _angularDamping != value)
                {
                    m_rigidBody.SetDamping(_linearDamping,value);
                }
                _angularDamping = value; }
        }

        [SerializeField]
        float _restitution = 0f;
        public float restitution
        {
            get { return _restitution; }
            set {
                if (m_collisionObject != null && _restitution != value)
                {
                    m_collisionObject.Restitution = value;
                }
                _restitution = value; }
        }

        [SerializeField]
        float _linearSleepingThreshold = .8f;
        public float linearSleepingThreshold
        {
            get { return _linearSleepingThreshold; }
            set {
                if (m_collisionObject != null && _linearSleepingThreshold != value)
                {
                    m_rigidBody.SetSleepingThresholds(value,_angularSleepingThreshold);
                }
                _linearSleepingThreshold = value; }
        }

        [SerializeField]
        float _angularSleepingThreshold = 1f;
        public float angularSleepingThreshold
        {
            get { return _angularSleepingThreshold; }
            set {
                if (m_collisionObject != null && _angularSleepingThreshold != value)
                {
                    m_rigidBody.SetSleepingThresholds(_linearSleepingThreshold, value);
                }
                _angularSleepingThreshold = value; }
        }
        
        [SerializeField]
        bool _additionalDamping = false;
        public bool additionalDamping
        {
            get { return _additionalDamping; }
            set {
                if (isInWorld && _additionalDamping != value)
                {
					BDebug.LogError(debugType, "Need to remove and re-add the rigid body to change additional damping setting");
                    return;
                }
                _additionalDamping = value;
            }
        }

        [SerializeField]
        float _additionalDampingFactor = .005f;
        public float additionalDampingFactor
        {
            get { return _additionalDampingFactor; }
            set {
                if (m_collisionObject != null && _additionalDampingFactor != value)
                {
					BDebug.LogError(debugType, "Additional Damping settings cannot be changed once the Rigid Body has been created");
                    return;
                }
                _additionalDampingFactor = value; }
        }

        [SerializeField]
        float _additionalLinearDampingThresholdSqr = .01f;
        public float additionalLinearDampingThresholdSqr
        {
            get { return _additionalLinearDampingThresholdSqr; }
            set {
                if (m_collisionObject != null && _additionalLinearDampingThresholdSqr != value)
                {
					BDebug.LogError(debugType, "Additional Damping settings cannot be changed once the Rigid Body has been created");
                    return;
                }
                _additionalLinearDampingThresholdSqr = value; }
        }

        [SerializeField]
        float _additionalAngularDampingThresholdSqr = .01f;
        public float additionalAngularDampingThresholdSqr
        {
            get { return _additionalAngularDampingThresholdSqr; }
            set {
                if (m_collisionObject != null && _additionalAngularDampingThresholdSqr != value)
                {
					BDebug.LogError(debugType, "Additional Damping settings cannot be changed once the Rigid Body has been created");
                    return;
                }
                _additionalAngularDampingThresholdSqr = value; }
        }

        [SerializeField]
        float _additionalAngularDampingFactor = .01f;
        public float additionalAngularDampingFactor
        {
            get { return _additionalAngularDampingFactor; }
            set {
                if (m_collisionObject != null && _additionalAngularDampingFactor != value)
                {
					BDebug.LogError(debugType, "Additional Damping settings cannot be changed once the Rigid Body has been created");
                    return;
                }
                _additionalAngularDampingFactor = value; }
        }

        /* can lock axis with this */
        [SerializeField]
        UnityEngine.Vector3 _linearFactor = UnityEngine.Vector3.one;
        public UnityEngine.Vector3 linearFactor
        {
            get { return _linearFactor; }
            set {
                if (m_collisionObject != null && _linearFactor != value)
                {
                    m_rigidBody.LinearFactor = value.ToBullet();
                }
                _linearFactor = value;
            }
        }

        [SerializeField]
        UnityEngine.Vector3 _angularFactor = UnityEngine.Vector3.one;
        public UnityEngine.Vector3 angularFactor
        {
            get { return _angularFactor; }
            set {
                if (m_rigidBody != null && _angularFactor != value)
                {
                    m_rigidBody.AngularFactor = value.ToBullet();
                }
                _angularFactor = value; }
        }

        [SerializeField]
        float _mass = 1f;
        public float mass {
            set {
                if (_mass != value) {
                    if (_mass == 0f && isDynamic()) {
						BDebug.LogError(debugType, "Rigid bodies that are not static or kinematic must have positive mass");
                        return;
                    }
                    if (m_rigidBody != null)
                    {
                        _localInertia = BulletSharp.Math.Vector3.Zero;
                        if (isDynamic())
                        {
                            m_collisionShape.GetCollisionShape().CalculateLocalInertia(_mass, out _localInertia);
                        }
                        m_rigidBody.SetMassProps(_mass, _localInertia);
                    }
                    _mass = value;
                }
            }
            get {
                return _mass;
            }
        }

        [SerializeField]
        protected UnityEngine.Vector3 _linearVelocity;
        public UnityEngine.Vector3 velocity {
            get {
                if (isInWorld) {
                    return m_rigidBody.LinearVelocity.ToUnity();
                } else {
                    return _linearVelocity;
                }
            }
            set {
                if (isInWorld) {
                    m_rigidBody.LinearVelocity = value.ToBullet();
                }
                _linearVelocity = value;
            }
        }

        [SerializeField]
        protected UnityEngine.Vector3 _angularVelocity;
        public UnityEngine.Vector3 angularVelocity {
            get {
                if (isInWorld) {
                    return m_rigidBody.AngularVelocity.ToUnity();
                } else {
                    return _angularVelocity;
                }
            }
            set {
                if (isInWorld) {
                    m_rigidBody.AngularVelocity = value.ToBullet();
                }
                _angularVelocity = value;
            }
        }

        public BDebug.DebugType debugType;

        //called by Physics World just before rigid body is added to world.
        //the current rigid body properties are used to rebuild the rigid body.
        internal override bool _BuildCollisionObject() {
            BPhysicsWorld world = BPhysicsWorld.Get();
            if (m_rigidBody != null) {
                if (isInWorld && world != null) {
                    isInWorld = false;
                    world.RemoveRigidBody(m_rigidBody);
                }
            }
            
            if (transform.localScale != UnityEngine.Vector3.one) {
				BDebug.LogError(debugType, "The local scale on this rigid body is not one. Bullet physics does not support scaling on a rigid body world transform. Instead alter the dimensions of the CollisionShape.");
            }

            m_collisionShape = GetComponent<BCollisionShape>();
            if (m_collisionShape == null) {
				BDebug.LogError(debugType, "There was no collision shape component attached to this BRigidBody. {0}", name);
                return false;
            }

            CollisionShape cs = m_collisionShape.GetCollisionShape();
            //rigidbody is dynamic if and only if mass is non zero, otherwise static
            _localInertia = BulletSharp.Math.Vector3.Zero;
            if (isDynamic()) {
                cs.CalculateLocalInertia(_mass, out _localInertia);
            }

            if (m_rigidBody == null) {
                m_motionState = new BGameObjectMotionState(transform);
                float bulletMass = _mass;
                if (!isDynamic())
                {
                    bulletMass = 0f;
                }

                RigidBodyConstructionInfo rbInfo = new RigidBodyConstructionInfo(bulletMass, m_motionState, cs, _localInertia);
                rbInfo.Friction = _friction;
                rbInfo.RollingFriction = _rollingFriction;
                rbInfo.LinearDamping = _linearDamping;
                rbInfo.AngularDamping = _angularDamping;
                rbInfo.Restitution = _restitution;
                rbInfo.LinearSleepingThreshold = _linearSleepingThreshold;
                rbInfo.AngularSleepingThreshold = _angularSleepingThreshold;
                rbInfo.AdditionalDamping = _additionalDamping;
                rbInfo.AdditionalAngularDampingFactor = _additionalAngularDampingFactor;
                rbInfo.AdditionalAngularDampingThresholdSqr = _additionalAngularDampingThresholdSqr;
                rbInfo.AdditionalDampingFactor = _additionalDampingFactor;
                rbInfo.AdditionalLinearDampingThresholdSqr = _additionalLinearDampingThresholdSqr;
                m_rigidBody = new RigidBody(rbInfo);
                m_rigidBody.UserObject = this;
                m_rigidBody.AngularVelocity = _angularVelocity.ToBullet();
                m_rigidBody.LinearVelocity = _linearVelocity.ToBullet();
                rbInfo.Dispose();
            } else {
                float usedMass = 0f;
                if (isDynamic())
                {
                    usedMass = _mass;
                }
                m_rigidBody.SetMassProps(usedMass, _localInertia);
                m_rigidBody.Friction = _friction;
                m_rigidBody.RollingFriction = _rollingFriction;
                m_rigidBody.SetDamping(_linearDamping,_angularDamping);
                m_rigidBody.Restitution = _restitution;
                m_rigidBody.SetSleepingThresholds(_linearSleepingThreshold, _angularSleepingThreshold);
                m_rigidBody.AngularVelocity = _angularVelocity.ToBullet();
                m_rigidBody.LinearVelocity = _linearVelocity.ToBullet();
                m_rigidBody.CollisionShape = cs;
                
            }
            m_rigidBody.CollisionFlags = m_collisionFlags;
            m_rigidBody.LinearFactor = _linearFactor.ToBullet();
            m_rigidBody.AngularFactor = _angularFactor.ToBullet();

            //if kinematic then disable deactivation
            if ((m_collisionFlags & BulletSharp.CollisionFlags.KinematicObject) != 0) {
                m_rigidBody.ActivationState = ActivationState.DisableDeactivation;
            }
            return true;
        }

        protected override void Awake() {
            BRigidBody[] rbs = GetComponentsInParent<BRigidBody>();
            if (rbs.Length != 1) {
				BDebug.LogError(debugType, "Can't nest rigid bodies. The transforms are updated by Bullet in undefined order which can cause spasing. Object {0}", name);
            }
            m_collisionShape = GetComponent<BCollisionShape>();
            if (m_collisionShape == null) {
				BDebug.LogError(debugType, "A BRigidBody component must be on an object with a BCollisionShape component.");
            }
        }

        protected override void OnDisable()
        {
            if (m_rigidBody != null && isInWorld) {
                //all constraints using RB must be disabled before rigid body is disabled
                for (int i = m_rigidBody.NumConstraintRefs - 1; i >= 0; i--)
                {
                    BTypedConstraint btc = (BTypedConstraint) m_rigidBody.GetConstraintRef(i).Userobject;
                    Debug.Assert(btc != null);
                    btc.enabled = false; //should remove it from the scene
                }
            }
            base.OnDisable();
        }

        protected override void AddObjectToBulletWorld()
        {
            BPhysicsWorld.Get().AddRigidBody(this);
        }

        protected override void RemoveObjectFromBulletWorld()
        {
            BPhysicsWorld pw = BPhysicsWorld.Get();
            if (pw != null && m_rigidBody != null && isInWorld)
            {
                Debug.Assert(m_rigidBody.NumConstraintRefs == 0, "Removing rigid body that still had constraints. Remove constraints first.");
                //constraints must be removed before rigid body is removed
                pw.RemoveRigidBody((RigidBody)m_collisionObject);
            }
        }

        protected override void Dispose(bool isdisposing) {
            if (isInWorld && isdisposing && m_rigidBody != null) {
                BPhysicsWorld pw = BPhysicsWorld.Get();
                if (pw != null && pw.world != null) {
                    //constraints must be removed before rigid body is removed
                    for (int i = m_rigidBody.NumConstraintRefs; i > 0; i--)
                    {
                        BTypedConstraint tc = (BTypedConstraint) m_rigidBody.GetConstraintRef(i - 1).Userobject;
                        ((DiscreteDynamicsWorld)pw.world).RemoveConstraint(tc.GetConstraint());
                    }
                    ((DiscreteDynamicsWorld) pw.world).RemoveRigidBody(m_rigidBody);
                }
            }
            if (m_rigidBody != null) {
                if (m_rigidBody.MotionState != null) m_rigidBody.MotionState.Dispose();
                m_rigidBody.Dispose();
                m_rigidBody = null;
            }
        }

        public void AddImpulse(UnityEngine.Vector3 impulse)
        {
            if (isInWorld)
            {
                m_rigidBody.ApplyCentralImpulse(impulse.ToBullet());
            }
        }


        public void AddImpulseAtPosition(UnityEngine.Vector3 impulse, UnityEngine.Vector3 relativePostion)
        {
            if (isInWorld)
            {
                m_rigidBody.ApplyImpulse(impulse.ToBullet(), relativePostion.ToBullet());
            }
        }

        public void AddTorqueImpulse(UnityEngine.Vector3 impulseTorque)
        {
            if (isInWorld)
            {
                m_rigidBody.ApplyTorqueImpulse(impulseTorque.ToBullet());
            }
        }

        /**
        Warning for single pulses use AddImpulse. AddForce should only be used over a period of time (several fixedTimeSteps or longer)
        The force accumulator is cleared after every StepSimulation call including interpolation StepSimulation calls which clear the force
        accumulator and do nothing. 
        */
        public void AddForce(UnityEngine.Vector3 force) {
            if (isInWorld) {
                m_rigidBody.ApplyCentralForce(force.ToBullet());
            }
        }

        /**
         Warning for single pulses use AddImpulse. AddForce should only be used over a period of time (several fixedTimeSteps or longer)
         The force accumulator is cleared after every StepSimulation call including interpolation StepSimulation calls which clear the force
         accumulator and do nothing. 
         */
        public void AddForceAtPosition(UnityEngine.Vector3 force, UnityEngine.Vector3 relativePostion) {
            if (isInWorld) {
                m_rigidBody.ApplyForce(force.ToBullet(), relativePostion.ToBullet());
            }
        }

        /**
         Warning for single pulses use AddImpulse. AddForce should only be used over a period of time (several fixedTimeSteps or longer)
         The force accumulator is cleared after every StepSimulation call including interpolation StepSimulation calls which clear the force
         accumulator and do nothing. 
         */
        public void AddTorque(UnityEngine.Vector3 torque) {
            if (isInWorld) {
                m_rigidBody.ApplyTorque(torque.ToBullet());
            }
        }

    }
}
