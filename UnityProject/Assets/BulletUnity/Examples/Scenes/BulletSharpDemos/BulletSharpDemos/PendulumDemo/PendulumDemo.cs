#define PENDULUM_DAMPING

using System;
using BulletSharp;
using BulletSharp.Math;
using DemoFramework;
using BulletSharpExamples;

namespace PendulumDemo
{
    class PendulumDemo : Demo
    {
        Vector3 eye = new Vector3(-2, 0, 1);
        Vector3 target = new Vector3(0, -0.5f, 0);

        const float radius = 0.05f;

        MultiBody multiBody;

        protected override void OnInitialize()
        {
            Freelook.SetEyeTarget(eye, target);

            Graphics.SetFormText("BulletSharp - Pendulum Demo");
            Graphics.SetInfoText("Move using mouse and WASD+shift\n" +
                "F3 - Toggle debug\n" +
                //"F11 - Toggle fullscreen\n" +
                "Space - Shoot box");

            IsDebugDrawEnabled = false;
            DebugDrawMode = DebugDrawModes.DrawWireframe | DebugDrawModes.DrawContactPoints | DebugDrawModes.DrawAabb;
        }

        protected override void OnInitializePhysics()
        {
            // collision configuration contains default setup for memory, collision setup
            CollisionConf = new DefaultCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(CollisionConf);

            Broadphase = new DbvtBroadphase();
            Solver = new MultiBodyConstraintSolver();

            World = new MultiBodyDynamicsWorld(Dispatcher, Broadphase, Solver as MultiBodyConstraintSolver, CollisionConf);
            World.Gravity = new Vector3(0, -9.81f, 0);

            const bool floating = false;
            const bool gyro = false;
            const int numLinks = 1;
            const bool canSleep = false;
            const bool selfCollide = false;
            Vector3 linkHalfExtents = new Vector3(0.05f, 0.5f, 0.1f);
            //Vector3 baseHalfExtents = new Vector3(0.05f, 0.5f, 0.1f);

            Vector3 baseInertiaDiag = Vector3.Zero;
            const float baseMass = 0;

            multiBody = new MultiBody(numLinks, baseMass, baseInertiaDiag, !floating, canSleep);
            //multiBody.UseRK4Integration = true;
            //multiBody.BaseWorldTransform = Matrix.Identity;

            //init the links
            Vector3 hingeJointAxis = new Vector3(1, 0, 0);

            //y-axis assumed up
            Vector3 parentComToCurrentCom = new Vector3(0, -linkHalfExtents[1], 0);
            Vector3 currentPivotToCurrentCom = new Vector3(0, -linkHalfExtents[1], 0);
            Vector3 parentComToCurrentPivot = parentComToCurrentCom - currentPivotToCurrentCom;

            for(int i = 0; i < numLinks; i++)
		    {
                const float linkMass = 10;
			    Vector3 linkInertiaDiag = Vector3.Zero;
                using (var shape = new SphereShape(radius))
                {
                    shape.CalculateLocalInertia(linkMass, out linkInertiaDiag);
                }
			
			    multiBody.SetupRevolute(i, linkMass, linkInertiaDiag, i - 1, Quaternion.Identity,
                    hingeJointAxis, parentComToCurrentPivot, currentPivotToCurrentCom, false);
		    }

            multiBody.FinalizeMultiDof();

            (World as MultiBodyDynamicsWorld).AddMultiBody(multiBody);
            multiBody.CanSleep = canSleep;
            multiBody.HasSelfCollision = selfCollide;
            multiBody.UseGyroTerm = gyro;

#if PENDULUM_DAMPING
            multiBody.LinearDamping = 0.1f;
            multiBody.AngularDamping = 0.9f;
#else
            multiBody.LinearDamping = 0;
            multiBody.AngularDamping = 0;
#endif

            for (int i = 0; i < numLinks; i++)
            {
                var shape = new SphereShape(radius);
                CollisionShapes.Add(shape);
                var col = new MultiBodyLinkCollider(multiBody, i);
                col.CollisionShape = shape;
                //const bool isDynamic = true;
                CollisionFilterGroups collisionFilterGroup = CollisionFilterGroups.DefaultFilter; // : CollisionFilterGroups.StaticFilter;
                CollisionFilterGroups collisionFilterMask = CollisionFilterGroups.AllFilter; // : CollisionFilterGroups.AllFilter & ~CollisionFilterGroups.StaticFilter;
                World.AddCollisionObject(col, collisionFilterGroup, collisionFilterMask);
                multiBody.GetLink(i).Collider = col;
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            multiBody.AddJointTorque(0, 20.0f);

            /*
            if (IsDebugDrawEnabled)
            {
                Vector3 from = multiBody.BaseWorldTransform.Origin;
                Vector3 to = multiBody.GetLink(0).Collider.WorldTransform.Origin;
                Vector3 color = new Vector3(1, 0, 0);
                World.DebugDrawer.DrawLine(ref from, ref to, ref color);
            }
            */
        }
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            using (Demo demo = new PendulumDemo())
            {
                GraphicsLibraryManager.Run(demo);
            }
        }
    }
}
