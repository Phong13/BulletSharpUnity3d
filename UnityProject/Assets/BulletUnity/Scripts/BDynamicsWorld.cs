using System;
using UnityEngine;
using System.Collections;
using BulletSharp;


namespace BulletUnity {
    public class BDynamicsWorld : BPhysicsWorld, IDisposable {
        public enum CollisionConfType
        {
            DefaultDynamicsWorldCollisionConf,
            SoftBodyRigidBodyCollisionConf,
        }

        public enum BroadphaseType
        {
            DynamicAABBBroadphase,
            Axis3SweepBroadphase,
            Axis3SweepBroadphase_32bit,
            SimpleBroadphase,
        }

        public CollisionConfType collisionType = CollisionConfType.DefaultDynamicsWorldCollisionConf;

        public BroadphaseType broadphaseType = BroadphaseType.DynamicAABBBroadphase;

        public Vector3 axis3SweepBroadphaseMin = new Vector3(-1000f, -1000f, -1000f);
        public Vector3 axis3SweepBroadphaseMax = new Vector3(1000f, 1000f, 1000f);
        const int axis3SweepMaxProxies = 32766;

        public Vector3 gravity = new Vector3(0f,-9.8f,0f);

        public CollisionConfiguration CollisionConf;
        public CollisionDispatcher Dispatcher;
        public BroadphaseInterface Broadphase;

        protected override void _InitializePhysicsWorld() {
            base._InitializePhysicsWorld();
            
            if (collisionType == CollisionConfType.DefaultDynamicsWorldCollisionConf)
            {
                CollisionConf = new DefaultCollisionConfiguration();
            } else if (collisionType == CollisionConfType.SoftBodyRigidBodyCollisionConf)
            {
                CollisionConf = new SoftBodyRigidBodyCollisionConfiguration();
            }

            Dispatcher = new CollisionDispatcher(CollisionConf);

            if (broadphaseType == BroadphaseType.DynamicAABBBroadphase)
            {
                Broadphase = new DbvtBroadphase();
            } else if (broadphaseType == BroadphaseType.Axis3SweepBroadphase)
            {
                Broadphase = new AxisSweep3(axis3SweepBroadphaseMin.ToBullet(), axis3SweepBroadphaseMax.ToBullet(), axis3SweepMaxProxies);
            } else if (broadphaseType == BroadphaseType.Axis3SweepBroadphase_32bit)
            {
                Broadphase = new AxisSweep3_32Bit(axis3SweepBroadphaseMin.ToBullet(), axis3SweepBroadphaseMax.ToBullet(), axis3SweepMaxProxies);
            } else
            {
                Broadphase = new SimpleBroadphase();
            }
            
            World = new DiscreteDynamicsWorld(Dispatcher, Broadphase, null, CollisionConf);
            World.Gravity = gravity.ToBullet();
            if (_doDebugDraw) {
                DebugDrawUnity db = new DebugDrawUnity();
                db.DebugMode = _debugDrawMode;
                World.DebugDrawer = db;
            }
        }

        protected override void Dispose(bool disposing) {
            Debug.Log("BDynamicsWorld Disposing physics.");

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

                if (World.DebugDrawer != null) {
                    if (World.DebugDrawer is IDisposable) {
                        IDisposable dis = (IDisposable)World.DebugDrawer;
                        dis.Dispose();
                    }
                }

                //delete collision shapes
                //foreach (CollisionShape shape in CollisionShapes)
                //    shape.Dispose();
                //CollisionShapes.Clear();

                World.Dispose();
                Broadphase.Dispose();
                Dispatcher.Dispose();
                CollisionConf.Dispose();

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
            base.Dispose(disposing);
        }
    }
}
