using System;
using UnityEngine;
using System.Collections;
using BulletSharp;


namespace BulletUnity {
    public class BDynamicsWorld : BPhysicsWorld, IDisposable {

        public DefaultCollisionConfiguration CollisionConf;
        public CollisionDispatcher Dispatcher;
        public DbvtBroadphase Broadphase;

        protected override void _InitializePhysicsWorld() {
            base._InitializePhysicsWorld();
            CollisionConf = new DefaultCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(CollisionConf);
            Broadphase = new DbvtBroadphase();
            World = new DiscreteDynamicsWorld(Dispatcher, Broadphase, null, CollisionConf);
            UnityEngine.Vector3 v = UnityEngine.Physics.gravity;
            World.Gravity = new BulletSharp.Math.Vector3(v.x, v.y, v.z);
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
