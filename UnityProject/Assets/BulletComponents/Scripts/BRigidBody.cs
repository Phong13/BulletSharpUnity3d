using UnityEngine;
using BulletSharp;
using BulletSharp.Math;
using System.Collections;

public class BRigidBody : MonoBehaviour {
    public enum RBType {
        dynamic,
        kinematic,
        isStatic,
    }

    public RigidBody rigidbody;
    //todo should be a property so rigidbody is updated when this is updated
    public float mass;
    public RBType type;
    public BCollisionShape.CollisionShapeType shapeType;
    [SerializeField]
    public BCollisionShape collisionShape = new BCollisionShapeBox();

    BGameObjectMotionState myMotionState;

    protected bool isInWorld;

    void _CreateRigidBody() {
        BPhysicsWorld world = BPhysicsWorld.Get();
        //rigidbody is dynamic if and only if mass is non zero, otherwise static

        BulletSharp.Math.Vector3 localInertia = BulletSharp.Math.Vector3.Zero;

        collisionShape.CreateCollisionShape();

        if (type == RBType.dynamic) {
            collisionShape.collisionShapePtr.CalculateLocalInertia(mass, out localInertia);
        }

        //using motionstate is recommended, it provides interpolation capabilities, and only synchronizes 'active' objects
        BGameObjectMotionState myMotionState = new BGameObjectMotionState(transform);

        UnityEngine.Vector3 uv = transform.localScale;
        //collisionShape = new BoxShape(uv.x,uv.y,uv.z);
        //Debug.Log(name + " " + uv);
        RigidBodyConstructionInfo rbInfo = new RigidBodyConstructionInfo(mass, myMotionState, collisionShape.collisionShapePtr, localInertia);
        rigidbody = new RigidBody(rbInfo);
        rbInfo.Dispose();
    }

    void Awake() {
        _CreateRigidBody();
    }

    void OnEnable() {
        if (BPhysicsWorld.Get().AddRigidBody(rigidbody)) {
            isInWorld = true;
        }
    }

    void OnDisable() {
        BPhysicsWorld.Get().RemoveRigidBody(rigidbody);
        isInWorld = false;
    }

    void OnDestroy() {
        Debug.Log("Destroying RigidBody " + name);
        if (isInWorld) {
            BPhysicsWorld pw = BPhysicsWorld.Get();
            if (pw != null) {
                pw.World.RemoveRigidBody(rigidbody);
            }
        }
        if (rigidbody != null) {
            if (rigidbody.MotionState != null) rigidbody.MotionState.Dispose();
            rigidbody.Dispose();
        }
    }
}
