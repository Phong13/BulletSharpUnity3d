using BulletSharp;
using BulletSharp.Math;
using BulletSharpExamples;
using DemoFramework;
using System;

namespace BspDemo
{
    public class BspToBulletConverter : BspConverter
    {
        Demo demo;

        public BspToBulletConverter(Demo demo)
        {
            this.demo = demo;
        }

        public override void AddConvexVerticesCollider(AlignedVector3Array vertices, bool isEntity, Vector3 entityTargetLocation)
        {
            // perhaps we can do something special with entities (isEntity)
            // like adding a collision Triggering (as example)

            if (vertices.Count == 0)
                return;

            float mass = 0.0f;
            //can use a shift
            Matrix startTransform = Matrix.Translation(0, 0, -10.0f);
            CollisionShape shape = new ConvexHullShape(vertices);
            demo.CollisionShapes.Add(shape);

            demo.LocalCreateRigidBody(mass, startTransform, shape);
        }
    }

    class BspDemo : Demo
    {
        Vector3 eye = new Vector3(10, 10, 10);
        Vector3 target = new Vector3(0, 0, 0);

        protected override void OnInitialize()
        {
            Freelook.Up = Vector3.UnitZ;
            Freelook.SetEyeTarget(eye, target);

            Graphics.SetFormText("BulletSharp - Quake BSP Physics Viewer");
        }

        protected override void OnInitializePhysics()
        {
            // collision configuration contains default setup for memory, collision setup
            CollisionConf = new DefaultCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(CollisionConf);

            Broadphase = new DbvtBroadphase();
            Solver = new SequentialImpulseConstraintSolver();

            World = new DiscreteDynamicsWorld(Dispatcher, Broadphase, Solver, CollisionConf);
            World.Gravity = Freelook.Up * -10.0f;

            BspLoader bspLoader = new BspLoader();
            //string[] args = Environment.GetCommandLineArgs();
            //if (args.Length == 1)
            //{
            UnityEngine.TextAsset bytes = (UnityEngine.TextAsset)UnityEngine.Resources.Load("BspDemo");
            System.IO.Stream byteStream = new System.IO.MemoryStream(bytes.bytes);
            bspLoader.LoadBspFile(byteStream);
            //}
            //else
            //{
            //    bspLoader.LoadBspFile(args[1]);
            //}
            BspConverter bsp2Bullet = new BspToBulletConverter(this);
            bsp2Bullet.ConvertBsp(bspLoader, 0.1f);
        }
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            using (Demo demo = new BspDemo())
            {
                GraphicsLibraryManager.Run(demo);
            }
        }
    }
}
