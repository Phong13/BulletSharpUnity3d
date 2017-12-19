using BulletSharp;
using BulletSharp.Math;
using DemoFramework;
using System;
using BulletSharpExamples;

namespace BasicDemo
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            //DemoRunner.Run<BasicDemo>();
        }
    }

    class BasicDemo : IDemoConfiguration
    {
        public ISimulation CreateSimulation(Demo demo)
        {
            demo.FreeLook.Eye = new Vector3(30, 20, 15) * BasicDemoSimulation.Scale;
            demo.FreeLook.Target = new Vector3(0, 3, 0) * BasicDemoSimulation.Scale;
            demo.Graphics.WindowTitle = "BulletSharp - Basic Demo";
            return new BasicDemoSimulation();
        }
    }

    internal sealed class BasicDemoSimulation : ISimulation
    {
        public const float Scale = 0.5f;
        private const int NumBoxesX = 5, NumBoxesY = 5, NumBoxesZ = 5;
        private Vector3 _startPosition = new Vector3(0, 2, 0);

        public BasicDemoSimulation()
        {
            UnityEngine.Debug.Log("Creating BasicDemoSimulation ");
            CollisionConfiguration = new DefaultCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(CollisionConfiguration);
            Broadphase = new DbvtBroadphase();
            World = new DiscreteDynamicsWorld(Dispatcher, Broadphase, null, CollisionConfiguration);

            CreateGround();
            CreateBoxes();
        }

        public CollisionConfiguration CollisionConfiguration { get; set; }
        public CollisionDispatcher Dispatcher { get; set; }
        public BroadphaseInterface Broadphase { get; set; }
        public DiscreteDynamicsWorld World { get; set; }

        public void Dispose()
        {
            UnityEngine.Debug.Log("BasicDemo.Dispose");
            this.StandardCleanup();
        }

        private void CreateGround()
        {
            UnityEngine.Debug.Log("CreateGround");
            var groundShape = new BoxShape(Scale * new Vector3(50, 1, 50));
            //var groundShape = new StaticPlaneShape(Vector3.UnitY, Scale);

            CollisionObject ground = PhysicsHelper.CreateStaticBody(Matrix.Identity, groundShape, (DynamicsWorld) World);
            ground.UserObject = "Ground";
        }

        private void CreateBoxes()
        {
            UnityEngine.Debug.Log("CreateBoxes");
            const float mass = 1.0f;
            var shape = new BoxShape(Scale);
            Vector3 localInertia = shape.CalculateLocalInertia(mass);
            var bodyInfo = new RigidBodyConstructionInfo(mass, null, shape, localInertia);

            for (int y = 0; y < NumBoxesY; y++)
            {
                for (int x = 0; x < NumBoxesX; x++)
                {
                    for (int z = 0; z < NumBoxesZ; z++)
                    {
                        Vector3 position = _startPosition + Scale * 2 * new Vector3(x, y, z);

                        // make it drop from a height
                        position += new Vector3(0, Scale * 10, 0);

                        // using MotionState is recommended, it provides interpolation capabilities
                        // and only synchronizes 'active' objects
                        bodyInfo.MotionState = new DefaultMotionState(Matrix.Translation(position));
                        var body = new RigidBody(bodyInfo);

                        ((DynamicsWorld)World).AddRigidBody(body);
                    }
                }
            }

            bodyInfo.Dispose();
        }
    }
}
