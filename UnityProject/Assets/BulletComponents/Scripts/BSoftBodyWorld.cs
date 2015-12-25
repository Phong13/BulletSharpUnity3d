using UnityEngine;
using System.Collections;
using BulletSharp;
using BulletSharp.Math;
using BulletSharp.SoftBody;

namespace BulletUnity {
    public class BSoftBodyWorld : BDynamicsWorld {

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
    }
}
