using BulletSharp;
using BulletSharp.Math;
using DemoFramework;
using System;
using BulletSharpExamples;

namespace RollingFrictionDemo
{
    class RollingFrictionDemo : Demo
    {
        Vector3 eye = new Vector3(10, 10, 40);
        Vector3 target = new Vector3(0, 5, -4);

        // create 125 (5x5x5) dynamic objects
        const int ArraySizeX = 5, ArraySizeY = 5, ArraySizeZ = 5;

        // scaling of the objects (0.1 = 20 centimeter boxes )
        const float StartPosX = -5;
        const float StartPosY = -5;
        const float StartPosZ = -3;

        protected override void OnInitialize()
        {
            Freelook.SetEyeTarget(eye, target);

            Graphics.SetFormText("BulletSharp - Rolling Friction Demo");
        }

        protected override void OnInitializePhysics()
        {
            // collision configuration contains default setup for memory, collision setup
            CollisionConf = new DefaultCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(CollisionConf);

            Broadphase = new DbvtBroadphase();

            World = new DiscreteDynamicsWorld(Dispatcher, Broadphase, null, CollisionConf);
            World.Gravity = new Vector3(0, -10, 0);

            // create the ground
            CollisionShape groundShape = new BoxShape(20, 50, 10);
            CollisionShapes.Add(groundShape);
            CollisionObject ground = LocalCreateRigidBody(0,
                Matrix.RotationAxis(new Vector3(0, 0, 1), (float)Math.PI * 0.03f) * Matrix.Translation(0, -50, 0),
                groundShape);
            ground.Friction = 1;
            ground.RollingFriction = 1;
            ground.UserObject = "Ground";

            groundShape = new BoxShape(100, 50, 100);
            CollisionShapes.Add(groundShape);
            ground = LocalCreateRigidBody(0, Matrix.Translation(0, -54, 0), groundShape);
            ground.Friction = 1;
            ground.RollingFriction = 1;
            ground.UserObject = "Ground";

            // create a few dynamic rigidbodies
            CollisionShape[] colShapes = {
			    new SphereShape(1),
			    new CapsuleShape(0.5f,1),
			    new CapsuleShapeX(0.5f,1),
			    new CapsuleShapeZ(0.5f,1),
			    new ConeShape(0.5f,1),
			    new ConeShapeX(0.5f,1),
			    new ConeShapeZ(0.5f,1),
			    new CylinderShape(new Vector3(0.5f,1,0.5f)),
			    new CylinderShapeX(new Vector3(1,0.5f,0.5f)),
			    new CylinderShapeZ(new Vector3(0.5f,0.5f,1)),
		    };
            foreach (var collisionShape in colShapes)
            {
                CollisionShapes.Add(collisionShape);
            }

            const float mass = 1.0f;

            CollisionShape colShape = new BoxShape(1);
            CollisionShapes.Add(colShape);
            Vector3 localInertia = colShape.CalculateLocalInertia(mass);

            var rbInfo = new RigidBodyConstructionInfo(mass, null, null, localInertia);

            const float startX = StartPosX - ArraySizeX / 2;
            const float startY = StartPosY;
            const float startZ = StartPosZ - ArraySizeZ / 2;

            int shapeIndex = 0;
            for (int k = 0; k < ArraySizeY; k++)
            {
                for (int i = 0; i < ArraySizeX; i++)
                {
                    for (int j = 0; j < ArraySizeZ; j++)
                    {
                        Matrix startTransform = Matrix.Translation(
                            2 * i + startX,
                            2 * k + startY + 20,
                            2 * j + startZ
                        );
                        shapeIndex++;

                        // using motionstate is recommended, it provides interpolation capabilities
                        // and only synchronizes 'active' objects
                        rbInfo.MotionState = new DefaultMotionState(startTransform);
                        rbInfo.CollisionShape = colShapes[shapeIndex % colShapes.Length];

                        RigidBody body = new RigidBody(rbInfo);
                        body.Friction = 1;
                        body.RollingFriction = 0.3f;
                        body.SetAnisotropicFriction(colShape.AnisotropicRollingFrictionDirection, AnisotropicFrictionFlags.RollingFriction);

                        World.AddRigidBody(body);
                    }
                }
            }

            rbInfo.Dispose();
        }
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            using (Demo demo = new RollingFrictionDemo())
            {
                GraphicsLibraryManager.Run(demo);
            }
        }
    }
}
