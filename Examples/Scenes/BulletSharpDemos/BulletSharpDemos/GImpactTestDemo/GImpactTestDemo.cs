#define TEST_GIMPACT_TORUS
//#define BULLET_TRIANGLE_COLLISION
#define BULLET_GIMPACT
//#define BULLET_GIMPACT_CONVEX_DECOMPOSITION

using BulletSharp;
using BulletSharp.Math;
using DemoFramework;
using System;
using BulletSharpExamples;

namespace GImpactTestDemo
{
    class GImpactTestDemo : Demo
    {
        Vector3 eye = new Vector3(0, 10, 50);
        Vector3 target = new Vector3(0, 10, -4);

        CollisionShape trimeshShape;
        CollisionShape trimeshShape2;

        TriangleIndexVertexArray indexVertexArrays;
        TriangleIndexVertexArray indexVertexArrays2;

        //Vector3 kinTorusTran;
        //Quaternion kinTorusRot;
        RigidBody kinematicTorus;

        const float ShootBoxInitialSpeed = 40.0f;

        protected override void OnInitialize()
        {
            Freelook.SetEyeTarget(eye, target);

            Graphics.SetFormText("BulletSharp - GImpact Test Demo");
            Graphics.SetInfoText("Move using mouse and WASD+shift\n" +
                "F3 - Toggle debug\n" +
                //"F11 - Toggle fullscreen\n" +
                "Space - Shoot box\n" +
                ". - Shoot Bunny");

            Graphics.FarPlane = 400.0f;
        }

        protected override void OnInitializePhysics()
        {
            // collision configuration contains default setup for memory, collision setup
            CollisionConf = new DefaultCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(CollisionConf);

            //Broadphase = new SimpleBroadphase();
            Broadphase = new AxisSweep3_32Bit(new Vector3(-10000, -10000, -10000), new Vector3(10000, 10000, 10000), 1024);

            Solver = new SequentialImpulseConstraintSolver();

            World = new DiscreteDynamicsWorld(Dispatcher, Broadphase, Solver, CollisionConf);
            World.Gravity = new Vector3(0, -10, 0);


            // create trimesh model and shape
            InitGImpactCollision();

            // Create Scene
            float mass = 0.0f;

            CollisionShape staticboxShape1 = new BoxShape(200, 1, 200);//floor
            CollisionShapes.Add(staticboxShape1);
            LocalCreateRigidBody(mass, Matrix.Translation(0, -10, 0), staticboxShape1);

            CollisionShape staticboxShape2 = new BoxShape(1, 50, 200);//left wall
            CollisionShapes.Add(staticboxShape2);
            LocalCreateRigidBody(mass, Matrix.Translation(-200, 15, 0), staticboxShape2);

            CollisionShape staticboxShape3 = new BoxShape(1, 50, 200);//right wall
            CollisionShapes.Add(staticboxShape3);
            LocalCreateRigidBody(mass, Matrix.Translation(200, 15, 0), staticboxShape3);

            CollisionShape staticboxShape4 = new BoxShape(200, 50, 1);//front wall
            CollisionShapes.Add(staticboxShape4);
            LocalCreateRigidBody(mass, Matrix.Translation(0, 15, 200), staticboxShape4);

            CollisionShape staticboxShape5 = new BoxShape(200, 50, 1);//back wall
            CollisionShapes.Add(staticboxShape5);
            LocalCreateRigidBody(mass, Matrix.Translation(0, 15, -200), staticboxShape5);


            //static plane

            Vector3 normal = new Vector3(-0.5f, 0.5f, 0.0f);
            normal.Normalize();
            CollisionShape staticplaneShape6 = new StaticPlaneShape(normal, 0.5f);// A plane
            CollisionShapes.Add(staticplaneShape6);
            /*RigidBody staticBody2 =*/ LocalCreateRigidBody(mass, Matrix.Translation(0, -9, 0), staticplaneShape6);


            //another static plane

            normal = new Vector3(0.5f, 0.7f, 0.0f);
            //normal.Normalize();
            CollisionShape staticplaneShape7 = new StaticPlaneShape(normal, 0.0f);// A plane
            CollisionShapes.Add(staticplaneShape7);
            /*staticBody2 =*/ LocalCreateRigidBody(mass, Matrix.Translation(0, -10, 0), staticplaneShape7);


            // Create Static Torus
            float height = 28;
            const float step = 2.5f;
            const float massT = 1.0f;

            Matrix startTransform =
                Matrix.RotationQuaternion(Quaternion.RotationYawPitchRoll((float)Math.PI * 0.5f, 0, (float)Math.PI * 0.5f)) *
                Matrix.Translation(0, height, -5);

#if BULLET_GIMPACT
            kinematicTorus = LocalCreateRigidBody(0, startTransform, trimeshShape);
#else
                //kinematicTorus = LocalCreateRigidBody(0, startTransform, CreateTorusShape());
#endif

            //kinematicTorus.CollisionFlags = kinematicTorus.CollisionFlags | CollisionFlags.StaticObject;
            //kinematicTorus.ActivationState = ActivationState.IslandSleeping;

            kinematicTorus.CollisionFlags = kinematicTorus.CollisionFlags | CollisionFlags.KinematicObject;
            kinematicTorus.ActivationState = ActivationState.DisableDeactivation;

            // Kinematic
            //kinTorusTran = new Vector3(-0.1f, 0, 0);
            //kinTorusRot = Quaternion.RotationYawPitchRoll(0, (float)Math.PI * 0.01f, 0);


#if TEST_GIMPACT_TORUS

#if BULLET_GIMPACT
            // Create dynamic Torus
            for (int i = 0; i < 6; i++)
            {
                height -= step;
                startTransform =
                    Matrix.RotationQuaternion(Quaternion.RotationYawPitchRoll(0, 0, (float)Math.PI * 0.5f)) *
                    Matrix.Translation(0, height, -5);
                /*RigidBody bodyA =*/ LocalCreateRigidBody(massT, startTransform, trimeshShape);

                height -= step;
                startTransform =
                    Matrix.RotationQuaternion(Quaternion.RotationYawPitchRoll((float)Math.PI * 0.5f, 0, (float)Math.PI * 0.5f)) *
                    Matrix.Translation(0, height, -5);
                /*RigidBody bodyB =*/ LocalCreateRigidBody(massT, startTransform, trimeshShape);
            }
#else
            /*
            // Create dynamic Torus
            for (int i = 0; i < 6; i++)
            {
                height -= step;
                startTransform.setOrigin(btVector3(0, height, -5));
                startTransform.setRotation(btQuaternion(0, 0, 3.14159265 * 0.5));

                btRigidBody* bodyA = localCreateRigidBody(massT, startTransform, createTorusShape());

                height -= step;
                startTransform.setOrigin(btVector3(0, height, -5));
                startTransform.setRotation(btQuaternion(3.14159265 * 0.5, 0, 3.14159265 * 0.5));
                btRigidBody* bodyB = localCreateRigidBody(massT, startTransform, createTorusShape());
            }
            */
#endif
#endif

            // Create Dynamic Boxes
            for (int i = 0; i < 8; i++)
            {
                CollisionShape boxShape = new BoxShape(new Vector3(1, 1, 1));
                CollisionShapes.Add(boxShape);
                LocalCreateRigidBody(1, Matrix.Translation(2 * i - 5, 2, -3), boxShape);
            }
        }

        void InitGImpactCollision()
        {
            // Create Torus Shape

            indexVertexArrays = new TriangleIndexVertexArray(TorusMesh.Indices, TorusMesh.Vertices);

#if BULLET_GIMPACT
#if BULLET_GIMPACT_CONVEX_DECOMPOSITION
            //GImpactConvexDecompositionShape trimesh =
            //    new GImpactConvexDecompositionShape(indexVertexArrays, new Vector3(1), 0.01f);
	        //trimesh.Margin = 0.07f;
	        //trimesh.UpdateBound();
#else
            GImpactMeshShape trimesh = new GImpactMeshShape(indexVertexArrays);
            trimesh.LocalScaling = new Vector3(1);
#if BULLET_TRIANGLE_COLLISION
            trimesh.Margin = 0.07f; //?????
#else
            trimesh.Margin = 0;
#endif
            trimesh.UpdateBound();
#endif
            trimeshShape = trimesh;
#else
            //trimeshShape = new GImpactMeshData(indexVertexArrays);
#endif
            CollisionShapes.Add(trimeshShape);


            // Create Bunny Shape
            indexVertexArrays2 = new TriangleIndexVertexArray(BunnyMesh.Indices, BunnyMesh.Vertices);

#if BULLET_GIMPACT
#if BULLET_GIMPACT_CONVEX_DECOMPOSITION
            //GImpactConvexDecompositionShape trimesh2 =
            //    new GImpactConvexDecompositionShape(indexVertexArrays, new Vector3(1), 0.01f);
	        //trimesh.Margin = 0.07f;
	        //trimesh.UpdateBound();
            //trimeshShape = trimesh2;
#else
            GImpactMeshShape trimesh2 = new GImpactMeshShape(indexVertexArrays2);
            trimesh2.LocalScaling = new Vector3(1);
#if BULLET_TRIANGLE_COLLISION
            trimesh2.Margin = 0.07f; //?????
#else
            trimesh2.Margin = 0;
#endif
            trimesh2.UpdateBound();
            trimeshShape2 = trimesh2;
#endif
#else
            //trimeshShape2 = new GImpactMeshData(indexVertexArrays2);
#endif
            CollisionShapes.Add(trimeshShape2);

            //register GIMPACT algorithm
#if BULLET_GIMPACT
            GImpactCollisionAlgorithm.RegisterAlgorithm(Dispatcher);
#else
            //ConcaveConcaveCollisionAlgorithm.RegisterAlgorithm(Dispatcher);
#endif
        }

        public void ShootTrimesh(Vector3 camPos, Vector3 destination)
        {
            if (World != null)
            {
                const float mass = 4.0f;
                Matrix startTransform = Matrix.Translation(camPos);
#if BULLET_GIMPACT
                RigidBody body = LocalCreateRigidBody(mass, startTransform, trimeshShape2);
#else
		        RigidBody body = LocalCreateRigidBody(mass, startTransform, CreateBunnyShape());
#endif
                Vector3 linVel = new Vector3(destination[0] - camPos[0], destination[1] - camPos[1], destination[2] - camPos[2]);
                linVel.Normalize();
                linVel *= ShootBoxInitialSpeed * 0.25f;

                body.WorldTransform = startTransform;
                body.LinearVelocity = linVel;
                body.AngularVelocity = new Vector3(0, 0, 0);
            }
        }

        public override void OnHandleInput()
        {
            if (Input.KeysPressed.Contains(Keys.OemPeriod))
            {
                ShootTrimesh(Freelook.Eye, Freelook.Target);
            }

            base.OnHandleInput();
        }

        public override void ExitPhysics()
        {
            base.ExitPhysics();

            indexVertexArrays.Dispose();
            indexVertexArrays2.Dispose();
        }
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            using (Demo demo = new GImpactTestDemo())
            {
                GraphicsLibraryManager.Run(demo);
            }
        }
    }
}
