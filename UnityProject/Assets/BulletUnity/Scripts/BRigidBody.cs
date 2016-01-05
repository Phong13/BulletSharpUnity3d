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

        public bool _isTrigger = false;
        public float friction = .5f;
        public float rollingFriction = 0f;
        public float linearDamping = 0f;
        public float angularDamping = 0f;
        public float restitution = 0f;
        public float linearSleepingThreshold = .8f;
        public float angularSleepingThreshold = 1f;
        public bool additionalDamping = false;
        public float additionalDampingFactor = .005f;
        public float additionalLinearDampingThresholdSqr = .01f;
        public float additionalAngularDampingThresholdSqr = .01f;
        public float additionalAngularDampingFactor = .01f;

        /* can lock axis with this */
        public UnityEngine.Vector3 linearFactor = UnityEngine.Vector3.one;
        public UnityEngine.Vector3 angularFactor = UnityEngine.Vector3.one;

        [SerializeField]
        float _mass = 1f;
        public float mass {
            set {
                if (_mass != value) {
                    if (_mass == 0f && _type == RBType.dynamic) {
                        Debug.LogError("Dynamic rigid bodies must have positive mass");
                        return;
                    }
                }
                if (isInWorld) {
                    _localInertia = BulletSharp.Math.Vector3.Zero;
                    if (_type == RBType.dynamic) {
                        m_collisionShape.GetCollisionShape().CalculateLocalInertia(_mass, out _localInertia);
                    }
                    m_Brigidbody.SetMassProps(_mass, _localInertia);
                }
                _mass = value;
            }
            get {
                return _mass;
            }
        }

        [SerializeField]
        RBType _type;
        public RBType type {
            set {
                if (isInWorld) {
                    Debug.LogError("Cannot change the type of a rigid body while it is in the Physics World. Remove, the rigid body, change the type, then re-add the rigid body.");
                    return;
                }
                _type = type;
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
                if (pw != null && pw.World != null) {
                    pw.World.RemoveRigidBody(m_Brigidbody);
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
