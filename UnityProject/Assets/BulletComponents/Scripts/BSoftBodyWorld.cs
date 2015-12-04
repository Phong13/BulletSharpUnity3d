using UnityEngine;
using System.Collections;
using BulletSharp;
using BulletSharp.Math;
using BulletSharp.SoftBody;


public class BSoftBodyWorld : BPhysicsWorld {

    SoftBodyWorldInfo softBodyWorldInfo;
    public DefaultCollisionConfiguration CollisionConf;
    public CollisionDispatcher Dispatcher;
    public AxisSweep3 Broadphase;
    public SequentialImpulseConstraintSolver Solver;
    const int maxProxies = 32766;

    protected override void _InitializePhysicsWorld() {
        base._InitializePhysicsWorld();
        CollisionConf = new SoftBodyRigidBodyCollisionConfiguration();
        Dispatcher = new CollisionDispatcher(CollisionConf);

        Broadphase = new AxisSweep3(new BulletSharp.Math.Vector3(-1000, -1000, -1000),
            new BulletSharp.Math.Vector3(1000, 1000, 1000), maxProxies);

        // the default constraint solver.
        Solver = new SequentialImpulseConstraintSolver();

        softBodyWorldInfo = new SoftBodyWorldInfo {
            AirDensity = 1.2f,
            WaterDensity = 0,
            WaterOffset = 0,
            WaterNormal = BulletSharp.Math.Vector3.Zero,
            Gravity = new BulletSharp.Math.Vector3(0, -10, 0),
            Dispatcher = Dispatcher,
            Broadphase = Broadphase
        };
        softBodyWorldInfo.SparseSdf.Initialize();
          
        World = new SoftRigidDynamicsWorld(Dispatcher, Broadphase, Solver, CollisionConf);
        BulletSharp.Math.Vector3 g = new BulletSharp.Math.Vector3(0, -10, 0);
        World.Gravity = g;
        World.DispatchInfo.EnableSpu = true;
    }

    protected override void _DisposePhysicsWorld() {
        Debug.Log("Disposing physics.");
        if (World != null) {
            //remove/dispose constraints
            int i;
            for (i = World.NumConstraints - 1; i >= 0; i--) {
                TypedConstraint constraint = World.GetConstraint(i);
                World.RemoveConstraint(constraint);
                constraint.Dispose();
            }

            //remove the rigidbodies from the dynamics world and delete them
            for (i = World.NumCollisionObjects - 1; i >= 0; i--) {
                CollisionObject obj = World.CollisionObjectArray[i];
                RigidBody body = obj as RigidBody;
                if (body != null && body.MotionState != null) {
                    body.MotionState.Dispose();
                }
                World.RemoveCollisionObject(obj);
                obj.Dispose();
            }

            //delete collision shapes
            //foreach (CollisionShape shape in CollisionShapes)
            //    shape.Dispose();
            //CollisionShapes.Clear();

            World.Dispose();
            Broadphase.Dispose();
            Dispatcher.Dispose();
            CollisionConf.Dispose();
            base._DisposePhysicsWorld();
        }

        if (Broadphase != null) {
            Broadphase.Dispose();
        }
        if (Dispatcher != null) {
            Dispatcher.Dispose();
        }
        if (CollisionConf != null) {
            CollisionConf.Dispose();
        }
    }
}
