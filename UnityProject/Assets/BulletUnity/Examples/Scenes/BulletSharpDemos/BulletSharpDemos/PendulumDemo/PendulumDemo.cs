#define PENDULUM_DAMPING

using BulletSharp;
using BulletSharp.Math;
using DemoFramework;
using System;

namespace PendulumDemo
{
    internal static class Program
    {
        //[STAThread]
        //static void Main()
        //{
        //    DemoRunner.Run<PendulumDemo>();
        //}
    }

    internal sealed class PendulumDemo : IDemoConfiguration, IUpdateReceiver
    {
        public ISimulation CreateSimulation(Demo demo)
        {
            demo.FreeLook.Eye = new Vector3(-2, 0, 1);
            demo.FreeLook.Target = new Vector3(0, -0.5f, 0);
            demo.IsDebugDrawEnabled = false;
            demo.DebugDrawMode = DebugDrawModes.DrawWireframe | DebugDrawModes.DrawContactPoints | DebugDrawModes.DrawAabb;
            demo.Graphics.WindowTitle = "BulletSharp - Pendulum Demo";
            return new PendulumDemoSimulation();
        }

        public void Update(Demo demo)
        {
            if (demo.IsDebugDrawEnabled)
            {
                var simulation = demo.Simulation as PendulumDemoSimulation;
                simulation.DrawPendulum();
            }
        }
    }

    internal sealed class PendulumDemoSimulation : ISimulation
    {
        const float radius = 0.05f;

        private MultiBodyConstraintSolver _solver;

        public PendulumDemoSimulation()
        {
            CollisionConfiguration = new DefaultCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(CollisionConfiguration);

            Broadphase = new DbvtBroadphase();
            _solver = new MultiBodyConstraintSolver();

            MultiBodyWorld = new MultiBodyDynamicsWorld(Dispatcher, Broadphase, _solver, CollisionConfiguration);
            World.SetInternalTickCallback(TickCallback, null, true);

            const bool floating = false;
            const bool gyro = false;
            const int numLinks = 1;
            const bool canSleep = false;
            const bool selfCollide = false;
            var linkHalfExtents = new Vector3(0.05f, 0.5f, 0.1f);
            var baseHalfExtents = new Vector3(0.05f, 0.5f, 0.1f);

            var baseInertiaDiag = Vector3.Zero;
            const float baseMass = 0;

            MultiBody = new MultiBody(numLinks, baseMass, baseInertiaDiag, !floating, canSleep);
            //MultiBody.UseRK4Integration = true;
            //MultiBody.BaseWorldTransform = Matrix.Identity;

            //init the links
            var hingeJointAxis = new Vector3(1, 0, 0);

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
			
			    MultiBody.SetupRevolute(i, linkMass, linkInertiaDiag, i - 1, Quaternion.Identity,
                    hingeJointAxis, parentComToCurrentPivot, currentPivotToCurrentCom, false);
		    }

            MultiBody.FinalizeMultiDof();

            MultiBodyWorld.AddMultiBody(MultiBody);
            MultiBody.CanSleep = canSleep;
            MultiBody.HasSelfCollision = selfCollide;
            MultiBody.UseGyroTerm = gyro;

#if PENDULUM_DAMPING
            MultiBody.LinearDamping = 0.1f;
            MultiBody.AngularDamping = 0.9f;
#else
            MultiBody.LinearDamping = 0;
            MultiBody.AngularDamping = 0;
#endif

            for (int i = 0; i < numLinks; i++)
            {
                var shape = new SphereShape(radius);
                var col = new MultiBodyLinkCollider(MultiBody, i);
                col.CollisionShape = shape;
                const bool isDynamic = true;
                CollisionFilterGroups collisionFilterGroup = isDynamic ? CollisionFilterGroups.DefaultFilter : CollisionFilterGroups.StaticFilter;
                CollisionFilterGroups collisionFilterMask = isDynamic ? CollisionFilterGroups.AllFilter : CollisionFilterGroups.AllFilter & ~CollisionFilterGroups.StaticFilter;
                World.AddCollisionObject(col, collisionFilterGroup, collisionFilterMask);
                MultiBody.GetLink(i).Collider = col;
            }
        }

        public CollisionConfiguration CollisionConfiguration { get; set; }
        public CollisionDispatcher Dispatcher { get; set; }
        public BroadphaseInterface Broadphase { get; set; }
        public DiscreteDynamicsWorld World
        {
            get { return MultiBodyWorld; }
            set { MultiBodyWorld = (MultiBodyDynamicsWorld)value; }
        }

        private MultiBodyDynamicsWorld MultiBodyWorld { get; set; }

        public MultiBody MultiBody { get; set; }

        public void DrawPendulum()
        {
            Vector3 from = MultiBody.BaseWorldTransform.Origin;
            Vector3 to = MultiBody.GetLink(0).Collider.WorldTransform.Origin;
            Vector3 color = new Vector3(1, 0, 0);
            World.DebugDrawer.DrawLine(ref from, ref to, ref color);
        }

        public void Dispose()
        {
            _solver.Dispose();

            this.StandardCleanup();
        }

        private void TickCallback(DynamicsWorld world, float timeStep)
        {
            MultiBody.AddJointTorque(0, timeStep * 1000.0f);
        }
    }
}
