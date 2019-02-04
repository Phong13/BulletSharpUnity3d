using BulletSharp;
using BulletSharp.Math;
using DemoFramework;
using System;
using BulletSharpExamples;

namespace DistanceDemo
{
    class DistanceDemo : Demo
    {
        Vector3 eye = new Vector3(30, 20, 10);
        Vector3 target = new Vector3(0, 5, -4);

        //Vector3 distanceFrom, distanceTo;
        //float distance;

        Matrix rotBodyPosition = Matrix.Translation(0, 10, 0);
        Matrix body2Position = Matrix.Translation(0, 5, 0);
        RigidBody rotBody /*, body2*/;
        ConvexShape colShape0, colShape1;

        VoronoiSimplexSolver sGjkSimplexSolver = new VoronoiSimplexSolver();

        float rotation = 0;

        protected override void OnInitialize()
        {
            Freelook.SetEyeTarget(eye, target);

            Graphics.SetFormText("BulletSharp - Distance Demo");
            Graphics.SetInfoText("Move using mouse and WASD+shift\n" +
                "F3 - Toggle debug\n" +
                //"F11 - Toggle fullscreen\n" +
                "Space - Shoot box");

            IsDebugDrawEnabled = false;
        }

        protected override void OnInitializePhysics()
        {
            CollisionConf = new DefaultCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(CollisionConf);

            Broadphase = new DbvtBroadphase();

            World = new DiscreteDynamicsWorld(Dispatcher, Broadphase, null, CollisionConf);
            World.Gravity = new Vector3(0, -10, 0);

            // ground
            CollisionShape groundShape = new BoxShape(50, 1, 50);
            CollisionShapes.Add(groundShape);
            CollisionObject ground = LocalCreateRigidBody(0, Matrix.Identity, groundShape);
            ground.UserObject = "Ground";

            // Objects
            //colShape = new BoxShape(1);
            Vector3[] points0 = {
                new Vector3(1, 0, 0), new Vector3(0, 1, 0), new Vector3(0, 0, 1)
            };
            Vector3[] points1 = {
                new Vector3(1, 0, 0), new Vector3(0, 1, 0), new Vector3(0, 0, 1), new Vector3(0,0,-1), new Vector3(-1,-1,0)
            };
            colShape0 = new ConvexHullShape(points0);
            colShape1 = new ConvexHullShape(points1);
            CollisionShapes.Add(colShape0);
            CollisionShapes.Add(colShape1);

            /*body2 =*/ LocalCreateRigidBody(0, body2Position, colShape1);

            rotBody = LocalCreateRigidBody(0, rotBodyPosition, colShape0);
            rotBody.CollisionFlags |= CollisionFlags.KinematicObject;
            rotBody.ActivationState = ActivationState.DisableDeactivation;
        }

        //static Vector3 red = new Vector3(1.0f, 0.0f, 0.0f);
        public override void OnUpdate()
        {
            base.OnUpdate();

            rotation += FrameDelta;
            rotBody.CenterOfMassTransform = Matrix.RotationX(rotation) * rotBodyPosition;

            GjkPairDetector detector = new GjkPairDetector(colShape0, colShape1, sGjkSimplexSolver, null);
            detector.CachedSeparatingAxis = new Vector3(0.00000000f, 0.059727669f, 0.29259586f);

            GjkPairDetector.ClosestPointInput input = new GjkPairDetector.ClosestPointInput();
            input.TransformA = rotBody.CenterOfMassTransform;
            input.TransformB = body2Position;

            PointCollector result = new PointCollector();
            detector.GetClosestPoints(input, result, null);

            /*
            if (result.HasResult && IsDebugDrawEnabled)
            {
                distanceFrom = result.PointInWorld;
                distanceTo = result.PointInWorld + result.NormalOnBInWorld * result.Distance;
                distance = result.Distance;
                //World.DebugDrawer.DrawLine(ref distanceFrom, ref distanceTo, ref red);
            }
            */
        }
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            using (Demo demo = new DistanceDemo())
            {
                GraphicsLibraryManager.Run(demo);
            }
        }
    }
}
