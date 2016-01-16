using System;
using UnityEngine;
using BulletSharp;
using BulletSharp.Math;
using System.Collections;


namespace BulletUnity {
    /*
        todo 
        continuous collision detection ccd
        */
    public class BRigidBody : MonoBehaviour, IDisposable {
        public enum RBType {
            dynamic,
            kinematic,
            isStatic,
        }

        protected RigidBody m_Brigidbody;
        BGameObjectMotionState m_motionState;
        protected bool isInWorld = false;
        BCollisionShape m_collisionShape;

        BulletSharp.Math.Vector3 _localInertia = BulletSharp.Math.Vector3.Zero;
        public BulletSharp.Math.Vector3 localInertia {
            get {
                return _localInertia;
            }
        }

        [SerializeField]
        bool _isTrigger = false;
        public bool isTrigger
        {
            get { return _isTrigger; }
            set{
                if (isInWorld && _isTrigger != value)
                {
                    Debug.LogError("Cannot set isTrigger on RigidBody that is in the physics world");
                    return;
                }    
                _isTrigger = value;
            }
        }

        [SerializeField]
        float _friction = .5f;
        public float friction
        {
            get { return _friction; }
            set {
                if (isInWorld || m_Brigidbody != null && _friction != value)
                {
                    m_Brigidbody.Friction = value;
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
                if (isInWorld || m_Brigidbody != null && _rollingFriction != value)
                {
                    m_Brigidbody.RollingFriction = value;
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
                if (isInWorld || m_Brigidbody != null && _linearDamping != value)
                {
                    m_Brigidbody.SetDamping(value,_angularDamping);
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
                if (isInWorld || m_Brigidbody != null && _angularDamping != value)
                {
                    m_Brigidbody.SetDamping(_linearDamping,value);
                }
                _angularDamping = value; }
        }

        [SerializeField]
        float _restitution = 0f;
        public float restitution
        {
            get { return _restitution; }
            set {
                if (isInWorld || m_Brigidbody != null && _restitution != value)
                {
                    m_Brigidbody.Restitution = value;
                }
                _restitution = value; }
        }

        [SerializeField]
        float _linearSleepingThreshold = .8f;
        public float linearSleepingThreshold
        {
            get { return _linearSleepingThreshold; }
            set {
                if (isInWorld || m_Brigidbody != null && _linearSleepingThreshold != value)
                {
                    m_Brigidbody.SetSleepingThresholds(value,_angularSleepingThreshold);
                }
                _linearSleepingThreshold = value; }
        }

        [SerializeField]
        float _angularSleepingThreshold = 1f;
        public float angularSleepingThreshold
        {
            get { return _angularSleepingThreshold; }
            set {
                if (isInWorld || m_Brigidbody != null && _angularSleepingThreshold != value)
                {
                    m_Brigidbody.SetSleepingThresholds(_linearSleepingThreshold, value);
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
                    Debug.LogError("Need to remove and re-add the rigid body to change additional damping setting");
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
                if (isInWorld && _additionalDampingFactor != value)
                {
                    Debug.LogError("Need to remove and re-add the rigid body to change additional damping setting");
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
                if (isInWorld && _additionalLinearDampingThresholdSqr != value)
                {
                    Debug.LogError("Need to remove and re-add the rigid body to change additional damping setting");
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
                if (isInWorld && _additionalAngularDampingThresholdSqr != value)
                {
                    Debug.LogError("Need to remove and re-add the rigid body to change additional damping setting");
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
                if (isInWorld && _additionalAngularDampingFactor != value)
                {
                    Debug.LogError("Need to remove and re-add the rigid body to change additional damping setting");
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
                if (isInWorld || m_Brigidbody != null && _linearFactor != value)
                {
                    m_Brigidbody.LinearFactor = value.ToBullet();
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
                if (isInWorld || m_Brigidbody != null && _angularFactor != value)
                {
                    m_Brigidbody.AngularFactor = value.ToBullet();
                }
                _angularFactor = value; }
        }

        [SerializeField]
        float _mass = 1f;
        public float mass {
            set {
                if (_mass != value) {
                    if (_mass == 0f && _type == RBType.dynamic) {
                        Debug.LogError("Dynamic rigid bodies must have positive mass");
                        return;
                    }
                    if (isInWorld)
                    {
                        _localInertia = BulletSharp.Math.Vector3.Zero;
                        if (_type == RBType.dynamic)
                        {
                            m_collisionShape.GetCollisionShape().CalculateLocalInertia(_mass, out _localInertia);
                        }
                        m_Brigidbody.SetMassProps(_mass, _localInertia);
                    }
                    _mass = value;
                }
            }
            get {
                return _mass;
            }
        }

        [SerializeField]
        RBType _type;
        public RBType type {
            set {
                if (isInWorld && _type != value) {
                    Debug.LogError("Cannot change the type of a rigid body while it is in the Physics World. Remove, the rigid body, change the type, then re-add the rigid body.");
                    return;
                }
                _type = value;
            }
            get {
                return _type;
            }
        }

        public UnityEngine.Vector3 velocity {
            get {
                if (isInWorld) {
                    return m_Brigidbody.LinearVelocity.ToUnity();
                } else {
                    return UnityEngine.Vector3.zero;
                }
            }
            set {
                if (isInWorld) {
                    m_Brigidbody.LinearVelocity = value.ToBullet();
                }
            }
        }

        public UnityEngine.Vector3 angularVelocity {
            get {
                if (isInWorld) {
                    return m_Brigidbody.AngularVelocity.ToUnity();
                } else {
                    return UnityEngine.Vector3.zero;
                }
            }
            set {
                if (isInWorld) {
                    m_Brigidbody.AngularVelocity = value.ToBullet();
                }
            }
        }

        //called by Physics World just before rigid body is added to world.
        //the current rigid body properties are used to rebuild the rigid body.
        internal bool _BuildRigidBody() {
            BPhysicsWorld world = BPhysicsWorld.Get();
            if (m_Brigidbody != null) {
                if (isInWorld && world != null) {
                    isInWorld = false;
                    world.RemoveRigidBody(m_Brigidbody);
                }
            }
            
            if (transform.localScale != UnityEngine.Vector3.one) {
                Debug.LogError("The local scale on this rigid body is not one. Bullet physics does not support scaling on a rigid body world transform. Instead alter the dimensions of the CollisionShape.");
            }

            m_collisionShape = GetComponent<BCollisionShape>();
            if (m_collisionShape == null) {
                Debug.LogError("There was no collision shape component attached to this BRigidBody. " + name);
                return false;
            }

            CollisionShape cs = m_collisionShape.GetCollisionShape();
            //rigidbody is dynamic if and only if mass is non zero, otherwise static
            _localInertia = BulletSharp.Math.Vector3.Zero;
            if (_type == RBType.dynamic) {
                cs.CalculateLocalInertia(_mass, out _localInertia);
            }

            if (m_Brigidbody == null) {
                m_motionState = new BGameObjectMotionState(transform);
                RigidBodyConstructionInfo rbInfo;
                if (_type == RBType.dynamic) {
                    rbInfo = new RigidBodyConstructionInfo(_mass, m_motionState, cs, _localInertia);
                } else {
                    rbInfo = new RigidBodyConstructionInfo(0, m_motionState, cs, localInertia);
                }
                m_Brigidbody = new RigidBody(rbInfo);
                rbInfo.Dispose();
            } else {
                m_Brigidbody.SetMassProps(_mass, localInertia);
                m_Brigidbody.CollisionShape = cs;
            }

            if (_type == RBType.kinematic) {
                m_Brigidbody.CollisionFlags = m_Brigidbody.CollisionFlags | BulletSharp.CollisionFlags.KinematicObject;
                m_Brigidbody.ActivationState = ActivationState.DisableDeactivation;
            }
            if (_isTrigger)
            {
                m_Brigidbody.CollisionFlags = m_Brigidbody.CollisionFlags | BulletSharp.CollisionFlags.NoContactResponse;
            }
            return true;
        }

        public RigidBody GetRigidBody() {
            if (m_Brigidbody == null) {
                _BuildRigidBody();
            }
            return m_Brigidbody;
        }

        void Awake() {
            BRigidBody[] rbs = GetComponentsInParent<BRigidBody>();
            if (rbs.Length != 1) {
                Debug.LogError("Can't nest rigid bodies. The transforms are updated by Bullet in undefined order which can cause spasing. Object " + name);
            }
            m_collisionShape = GetComponent<BCollisionShape>();
            if (m_collisionShape == null) {
                Debug.LogError("A BRigidBody component must be on an object with a BCollisionShape component.");
            }
        }

        void Start() { 
        //void OnEnable() {
            if (BPhysicsWorld.Get().AddRigidBody(this)) {
                isInWorld = true;
            }
        }

        void OnDisable() {
            if (isInWorld) {
                BPhysicsWorld.Get().RemoveRigidBody(m_Brigidbody);
            }
            isInWorld = false;
        }

        void OnDestroy() {
            Dispose(false);
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isdisposing) {
            if (isInWorld && isdisposing && m_Brigidbody != null) {
                BPhysicsWorld pw = BPhysicsWorld.Get();
                if (pw != null && pw.world != null) {
                    ((DiscreteDynamicsWorld) pw.world).RemoveRigidBody(m_Brigidbody);
                }
            }
            if (m_Brigidbody != null) {
                if (m_Brigidbody.MotionState != null) m_Brigidbody.MotionState.Dispose();
                m_Brigidbody.Dispose();
                m_Brigidbody = null;
            }
            Debug.Log("Destroying RigidBody " + name);
        }

        public void AddForce(UnityEngine.Vector3 force) {
            if (isInWorld) {
                m_Brigidbody.ApplyCentralForce(force.ToBullet());
            }
        }

        public void AddForceAtPosition(UnityEngine.Vector3 force, UnityEngine.Vector3 relativePostion) {
            if (isInWorld) {
                m_Brigidbody.ApplyForce(force.ToBullet(), relativePostion.ToBullet());
            }
        }

        public void AddTorque(UnityEngine.Vector3 torque) {
            if (isInWorld) {
                m_Brigidbody.ApplyTorque(torque.ToBullet());
            }
        }
    }
}
