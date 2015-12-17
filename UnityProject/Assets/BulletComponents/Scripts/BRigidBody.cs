using System;
using UnityEngine;
using BulletSharp;
using BulletSharp.Math;
using System.Collections;

/*
    todo 
    continuous collision detection ccd
    */
public class BRigidBody : MonoBehaviour,IDisposable {
    public enum RBType {
        dynamic,
        kinematic,
        isStatic,
    }

    protected RigidBody m_Brigidbody;
    BGameObjectMotionState m_motionState;
    protected bool isInWorld = false;
    BCollisionShape m_collisionShape;

    [SerializeField]
    float _mass = 1f;
    public float mass
    {
        set
        {
            if (_mass != value)
            {
                if (_mass == 0f && _type == RBType.dynamic)
                {
                    Debug.LogError("Dynamic rigid bodies must have positive mass");
                    return;
                }
            }
            if (isInWorld)
            {
                BulletSharp.Math.Vector3 inertia = m_Brigidbody.CollisionShape.CalculateLocalInertia(_mass);
                m_Brigidbody.SetMassProps(_mass, inertia);
            }
            _mass = value;
        }
        get
        {
            return _mass;
        }
    }

    [SerializeField]
    RBType _type;
    public RBType type
    {
        set
        {
            if (isInWorld)
            {
                Debug.LogError("Cannot change the type of a rigid body while it is in the Physics World. Remove, the rigid body, change the type, then re-add the rigid body.");
                return;
            }
            _type = type;
        }
        get
        {
            return _type;
        }
    }

    public UnityEngine.Vector3 velocity
    {
        get
        {
            if (isInWorld)
            {
                return m_Brigidbody.LinearVelocity.ToUnity();
            } else
            {
                return UnityEngine.Vector3.zero;
            }
        }
        set
        {
            if (isInWorld)
            {
                m_Brigidbody.LinearVelocity = value.ToBullet();
            }
        }
    }

    public UnityEngine.Vector3 angularVelocity
    {
        get
        {
            if (isInWorld)
            {
                return m_Brigidbody.AngularVelocity.ToUnity();
            }
            else
            {
                return UnityEngine.Vector3.zero;
            }
        }
        set
        {
            if (isInWorld)
            {
                m_Brigidbody.AngularVelocity = value.ToBullet();
            }
        }
    }

    //TODO this should be modified so it is safe to call just before a rigidbody is added to the physics world
    //It should be possible to call multiple times.
    void _CreateRigidBody() {
        BPhysicsWorld world = BPhysicsWorld.Get();
        if (m_Brigidbody != null)
        {
            if (isInWorld && world != null)
            {
                isInWorld = false;
                world.World.RemoveRigidBody(m_Brigidbody);
            }
        }

        if (transform.localScale != UnityEngine.Vector3.one)
        {
            Debug.LogError("The local scale on this rigid body is not one. Bullet physics does not support scaling on a rigid body world transform. Instead alter the dimensions of the CollisionShape.");
        }



        //rigidbody is dynamic if and only if mass is non zero, otherwise static
        BulletSharp.Math.Vector3 localInertia = BulletSharp.Math.Vector3.Zero;

        CollisionShape cs = m_collisionShape.GetCollisionShape();

        if (_type == RBType.dynamic) {
            cs.CalculateLocalInertia(_mass, out localInertia);
        }

        //using motionstate is recommended, it provides interpolation capabilities, and only synchronizes 'active' objects
        m_motionState = new BGameObjectMotionState(transform);

        UnityEngine.Vector3 uv = transform.localScale;

        RigidBodyConstructionInfo rbInfo;
        if (_type == RBType.dynamic) {
            rbInfo = new RigidBodyConstructionInfo(_mass, m_motionState, cs, localInertia);
        } else
        {
            rbInfo = new RigidBodyConstructionInfo(0, m_motionState, cs, localInertia);
        }

        m_Brigidbody = new RigidBody(rbInfo);
        if (_type == RBType.kinematic)
        {
            m_Brigidbody.CollisionFlags = m_Brigidbody.CollisionFlags | BulletSharp.CollisionFlags.KinematicObject;
            m_Brigidbody.ActivationState = ActivationState.DisableDeactivation;
        }

        rbInfo.Dispose();
    }

    void Awake() {
        BRigidBody[] rbs = GetComponentsInParent<BRigidBody>();
        if (rbs.Length != 1)
        {
            Debug.LogError("Can't nest rigid bodies. The transforms are updated by Bullet in undefined order which can cause spasing. Object " + name);
        }
        m_collisionShape = GetComponent<BCollisionShape>();
        if (m_collisionShape == null)
        {
            Debug.LogError("A BRigidBody component must be on an object with a BCollisionShape component.");
        }
        else
        {
            _CreateRigidBody();
        }
    }

    void OnEnable() {
        if (m_Brigidbody != null && BPhysicsWorld.Get().AddRigidBody(m_Brigidbody)) {
            isInWorld = true;
        }
    }

    void OnDisable() {
        if (isInWorld)
        {
            BPhysicsWorld.Get().RemoveRigidBody(m_Brigidbody);
        }
        isInWorld = false;
    }

    void OnDestroy() {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool isdisposing)
    {
        if (isInWorld && isdisposing && m_Brigidbody != null)
        {
            BPhysicsWorld pw = BPhysicsWorld.Get();
            if (pw != null && pw.World != null)
            {
                pw.World.RemoveRigidBody(m_Brigidbody);
            }
        }
        if (m_Brigidbody != null)
        {
            if (m_Brigidbody.MotionState != null) m_Brigidbody.MotionState.Dispose();
            m_Brigidbody.Dispose();
            m_Brigidbody = null;
        }
        Debug.Log("Destroying RigidBody " + name);
    }

    public void AddForce(UnityEngine.Vector3 force)
    {
        if (isInWorld)
        {
            m_Brigidbody.ApplyCentralForce(force.ToBullet());
        }
    }

    public void AddForceAtPosition(UnityEngine.Vector3 force, UnityEngine.Vector3 relativePostion)
    {
        if (isInWorld)
        {
            m_Brigidbody.ApplyForce(force.ToBullet(), relativePostion.ToBullet());
        }
    }

    public void AddTorque(UnityEngine.Vector3 torque)
    {
        if (isInWorld)
        {
            m_Brigidbody.ApplyTorque(torque.ToBullet());
        }
    }
}
