using BulletSharp;
using BulletSharp.Math;
using BulletSharpExamples;
using DemoFramework;
using System;

namespace Box2DDemo
{
    class Box2DDemo : Demo
    {
        Vector3 eye = new Vector3(0, 15, 20);
        Vector3 target = new Vector3(10, 10, 0);

        ///create 25 (5x5) dynamic objects
        const int ArraySizeX = 5, ArraySizeY = 5;
        public float Depth = 0.04f;

        protected override void OnInitialize()
        {
            Freelook.SetEyeTarget(eye, target);

            Graphics.SetFormText("BulletSharp - Box 2D Demo");
        }

        protected override void OnInitializePhysics()
        {
            // collision configuration contains default setup for memory, collision setup
            CollisionConf = new DefaultCollisionConfiguration();

            // Use the default collision dispatcher. For parallel processing you can use a diffent dispatcher.
            Dispatcher = new CollisionDispatcher(CollisionConf);

            VoronoiSimplexSolver simplex = new VoronoiSimplexSolver();
            MinkowskiPenetrationDepthSolver pdSolver = new MinkowskiPenetrationDepthSolver();

            Convex2DConvex2DAlgorithm.CreateFunc convexAlgo2D = new Convex2DConvex2DAlgorithm.CreateFunc(simplex, pdSolver);

            Dispatcher.RegisterCollisionCreateFunc(BroadphaseNativeType.Convex2DShape, BroadphaseNativeType.Convex2DShape, convexAlgo2D);
            Dispatcher.RegisterCollisionCreateFunc(BroadphaseNativeType.Box2DShape, BroadphaseNativeType.Convex2DShape, convexAlgo2D);
            Dispatcher.RegisterCollisionCreateFunc(BroadphaseNativeType.Convex2DShape, BroadphaseNativeType.Box2DShape, convexAlgo2D);
            Dispatcher.RegisterCollisionCreateFunc(BroadphaseNativeType.Box2DShape, BroadphaseNativeType.Box2DShape, new Box2DBox2DCollisionAlgorithm.CreateFunc());

            Broadphase = new DbvtBroadphase();

            // the default constraint solver.
            Solver = new SequentialImpulseConstraintSolver();

            World = new DiscreteDynamicsWorld(Dispatcher, Broadphase, Solver, CollisionConf);
            World.Gravity = new Vector3(0, -10, 0);

            // create a few basic rigid bodies
            CollisionShape groundShape = new BoxShape(150, 7, 150);
            CollisionShapes.Add(groundShape);
            RigidBody ground = LocalCreateRigidBody(0, Matrix.Identity, groundShape);
            ground.UserObject = "Ground";

            // create a few dynamic rigidbodies
            // Re-using the same collision is better for memory usage and performance
            float u = 0.96f;
            Vector3[] points = { new Vector3(0, u, 0), new Vector3(-u, -u, 0), new Vector3(u, -u, 0) };
            ConvexShape childShape0 = new BoxShape(1, 1, Depth);
            ConvexShape colShape = new Convex2DShape(childShape0);
            ConvexShape childShape1 = new ConvexHullShape(points);
            ConvexShape colShape2 = new Convex2DShape(childShape1);
            ConvexShape childShape2 = new CylinderShapeZ(1, 1, Depth);
            ConvexShape colShape3 = new Convex2DShape(childShape2);

            CollisionShapes.Add(colShape);
            CollisionShapes.Add(colShape2);
            CollisionShapes.Add(colShape3);

            CollisionShapes.Add(childShape0);
            CollisionShapes.Add(childShape1);
            CollisionShapes.Add(childShape2);

            colShape.Margin = 0.03f;

            float mass = 1.0f;
            Vector3 localInertia = colShape.CalculateLocalInertia(mass);

            Matrix startTransform;

            Vector3 x = new Vector3(-ArraySizeX, 8, -20);
            Vector3 y = Vector3.Zero;
            Vector3 deltaX = new Vector3(1, 2, 0);
            Vector3 deltaY = new Vector3(2, 0, 0);

            var rbInfo = new RigidBodyConstructionInfo(mass, null, colShape, localInertia);

            int i, j;
            for (i = 0; i < ArraySizeY; i++)
            {
                y = x;
                for (j = 0; j < ArraySizeX; j++)
                {
                    startTransform = Matrix.Translation(y + new Vector3(10, 0, 0));

                    //using motionstate is recommended, it provides interpolation capabilities, and only synchronizes 'active' objects
                    rbInfo.MotionState = new DefaultMotionState(startTransform);

                    switch (j % 3)
                    {
                        case 0:
                            rbInfo.CollisionShape = colShape;
                            break;
                        case 1:
                            rbInfo.CollisionShape = colShape3;
                            break;
                        default:
                            rbInfo.CollisionShape = colShape2;
                            break;
                    }
                    RigidBody body = new RigidBody(rbInfo);
                    //body.ActivationState = ActivationState.IslandSleeping;
                    body.LinearFactor = new Vector3(1, 1, 0);
                    body.AngularFactor = new Vector3(0, 0, 1);

                    World.AddRigidBody(body);

                    y += deltaY;
                }
                x += deltaX;
            }

            rbInfo.Dispose();
        }
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            using (Demo demo = new Box2DDemo())
            {
                GraphicsLibraryManager.Run(demo);
            }
        }
    }
}
