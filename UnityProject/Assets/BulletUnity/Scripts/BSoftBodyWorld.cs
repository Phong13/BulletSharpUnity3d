using UnityEngine;
using System.Collections;
using BulletSharp;
using BulletSharp.Math;
using BulletSharp.SoftBody;

namespace BulletUnity
{

    public class BSoftBodyWorld : BDynamicsWorld
    {

        SoftBodyWorldInfo softBodyWorldInfo;
        public SequentialImpulseConstraintSolver Solver;
        const int maxProxies = 32766;

        protected override void _InitializePhysicsWorld()
        {
            Debug.Log("Creating SoftRigidDynamicsWorld");
            // collision configuration contains default setup for memory, collision setup
            CollisionConf = new SoftBodyRigidBodyCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(CollisionConf);

            //TODO I think this will limit collision detection to -1000 to 1000 should be configurable
            Broadphase = new AxisSweep3(new BulletSharp.Math.Vector3(-1000, -1000, -1000),
                new BulletSharp.Math.Vector3(1000, 1000, 1000), maxProxies);
            
            //TODO this is taken from the Bullet examples, but I don't understand why the 
            // the default constraint solver.
            Solver = new SequentialImpulseConstraintSolver();

            softBodyWorldInfo = new SoftBodyWorldInfo {
                AirDensity = 1.2f,
                WaterDensity = 0,
                WaterOffset = 0,
                WaterNormal = BulletSharp.Math.Vector3.Zero,
                Gravity = UnityEngine.Physics.gravity.ToBullet(),
                Dispatcher = Dispatcher,
                Broadphase = Broadphase
            };
            softBodyWorldInfo.SparseSdf.Initialize();

            SoftRigidDynamicsWorld sw = new SoftRigidDynamicsWorld(Dispatcher, Broadphase, Solver, CollisionConf);
            World = sw;
            World.Gravity = UnityEngine.Physics.gravity.ToBullet();
            World.DispatchInfo.EnableSpu = true;

            softBodyWorldInfo.SparseSdf.Reset();
            softBodyWorldInfo.AirDensity = 1.2f;
            softBodyWorldInfo.WaterDensity = 0;
            softBodyWorldInfo.WaterOffset = 0;
            softBodyWorldInfo.WaterNormal = BulletSharp.Math.Vector3.Zero;
            softBodyWorldInfo.Gravity = UnityEngine.Physics.gravity.ToBullet();
        }
    }
}