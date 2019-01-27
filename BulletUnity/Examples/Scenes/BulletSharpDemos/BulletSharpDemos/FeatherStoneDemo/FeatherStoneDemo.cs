using BulletSharp;
using BulletSharp.Math;
using DemoFramework;
using System;
using BulletSharpExamples;

namespace FeatherStoneDemo
{
    class FeatherStoneDemo : Demo
    {
        Vector3 eye = new Vector3(0, 5, 10);
        Vector3 target = new Vector3(0, 0, 0);

        // create 125 (5x5x5) dynamic objects
        const int ArraySizeX = 5, ArraySizeY = 5, ArraySizeZ = 5;

        // scaling of the objects (0.1 = 20 centimeter boxes )
        const float StartPosX = -5;
        const float StartPosY = 2;
        const float StartPosZ = -3;

        const float Friction = 1.0f;

        protected override void OnInitialize()
        {
            Freelook.SetEyeTarget(eye, target);

            Graphics.SetFormText("BulletSharp - FeatherStone Demo");
            Graphics.SetInfoText("Move using mouse and WASD+shift\n" +
                "F3 - Toggle debug\n" +
                //"F11 - Toggle fullscreen\n" +
                "Space - Shoot box");
        }

        protected override void OnInitializePhysics()
        {
            // collision configuration contains default setup for memory, collision setup
            CollisionConf = new DefaultCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(CollisionConf);

            Broadphase = new DbvtBroadphase();
            Solver = new MultiBodyConstraintSolver();

            World = new MultiBodyDynamicsWorld(Dispatcher, Broadphase, Solver as MultiBodyConstraintSolver, CollisionConf);
            World.Gravity = new Vector3(0, -10, 0);

            // create a few basic rigid bodies
            BoxShape groundShape = new BoxShape(50, 50, 50);
            //groundShape.InitializePolyhedralFeatures();
            //CollisionShape groundShape = new StaticPlaneShape(new Vector3(0,1,0), 50);

            CollisionShapes.Add(groundShape);
            CollisionObject ground = LocalCreateRigidBody(0, Matrix.Translation(0, -51.55f, 0), groundShape, false);
            ground.UserObject = "Ground";


            int numLinks = 5;
            bool spherical = true;
            bool floatingBase = false;
            Vector3 basePosition = new Vector3(-0.4f, 3.0f, 0.0f);
            Vector3 baseHalfExtents = new Vector3(0.05f, 0.37f, 0.1f);
            Vector3 linkHalfExtents = new Vector3(0.05f, 0.37f, 0.1f);
            var mb = CreateFeatherstoneMultiBody(World as MultiBodyDynamicsWorld, numLinks, basePosition, baseHalfExtents, linkHalfExtents, spherical, floatingBase);

            floatingBase = !floatingBase;

            mb.CanSleep = true;
            mb.HasSelfCollision = false;
            mb.UseGyroTerm = true;

            bool damping = true;
            if (damping)
            {
                mb.LinearDamping = 0.1f;
                mb.AngularDamping = 0.9f;
            }
            else
            {
                mb.LinearDamping = 0;
                mb.AngularDamping = 0;
            }

            if (numLinks > 0)
            {
                float q0 = 45.0f * (float)Math.PI / 180.0f;
                if (spherical)
                {
                    Quaternion quat0 = Quaternion.RotationAxis(Vector3.Normalize(new Vector3(1, 1, 0)), q0);
                    quat0.Normalize();
                    mb.SetJointPosMultiDof(0, new float[] { quat0.X, quat0.Y, quat0.Z, quat0.W });
                }
                else
                {
                    mb.SetJointPosMultiDof(0, new float[] { q0 });
                }
            }
            AddColliders(mb, baseHalfExtents, linkHalfExtents);


            LocalCreateRigidBody(1, Matrix.Translation(0, -0.95f, 0), new BoxShape(0.5f, 0.5f, 0.5f), false);
        }

        MultiBody CreateFeatherstoneMultiBody(MultiBodyDynamicsWorld world, int numLinks,
            Vector3 basePosition, Vector3 baseHalfExtents, Vector3 linkHalfExtents, bool spherical, bool floating)
        {
            float mass = 1;
            Vector3 inertia = Vector3.Zero;
            if (mass != 0)
            {
                using (var box = new BoxShape(baseHalfExtents))
                {
                    box.CalculateLocalInertia(mass, out inertia);
                }
            }

            var mb = new MultiBody(numLinks, mass, inertia, !floating, false);
            //body.HasSelfCollision = false;

            //body.BaseVelocity = Vector3.Zero;
            mb.BasePosition = basePosition;
            //body.WorldToBaseRot = new Quaternion(0, 0, 1, -0.125f * (float)Math.PI);
            mb.WorldToBaseRot = Quaternion.Identity;

            float linkMass = 1;
            Vector3 linkInertia = Vector3.Zero;
            if (linkMass != 0)
            {
                using (var box = new BoxShape(linkHalfExtents))
                {
                    box.CalculateLocalInertia(linkMass, out linkInertia);
                }
            }

            //y-axis assumed up
            Vector3 parentComToCurrentCom = new Vector3(0, -linkHalfExtents[1] * 2.0f, 0);       //par body's COM to cur body's COM offset	
            Vector3 currentPivotToCurrentCom = new Vector3(0, -linkHalfExtents[1], 0);          //cur body's COM to cur body's PIV offset
            Vector3 parentComToCurrentPivot = parentComToCurrentCom - currentPivotToCurrentCom; //par body's COM to cur body's PIV offset

            for (int i = 0; i < numLinks; i++)
            {
                if (spherical)
                {
                    mb.SetupSpherical(i, linkMass, linkInertia, i - 1,
                        Quaternion.Identity, parentComToCurrentPivot, currentPivotToCurrentCom, false);
                }
                else
                {
                    Vector3 hingeJointAxis = new Vector3(1, 0, 0);
                    mb.SetupRevolute(i, linkMass, linkInertia, i - 1,
                        Quaternion.Identity, hingeJointAxis, parentComToCurrentPivot, currentPivotToCurrentCom, false);
                }
            }

            mb.FinalizeMultiDof();

            (World as MultiBodyDynamicsWorld).AddMultiBody(mb);

            return mb;
        }

        void AddBoxes()
        {
            // create a few dynamic rigidbodies
            const float mass = 1.0f;

            BoxShape colShape = new BoxShape(1);
            CollisionShapes.Add(colShape);
            Vector3 localInertia = colShape.CalculateLocalInertia(mass);

            const float startX = StartPosX - ArraySizeX / 2;
            const float startY = StartPosY;
            const float startZ = StartPosZ - ArraySizeZ / 2;

            int k, i, j;
            for (k = 0; k < ArraySizeY; k++)
            {
                for (i = 0; i < ArraySizeX; i++)
                {
                    for (j = 0; j < ArraySizeZ; j++)
                    {
                        Matrix startTransform = Matrix.Translation(
                            3 * i + startX,
                            3 * k + startY,
                            3 * j + startZ
                        );

                        // using motionstate is recommended, it provides interpolation capabilities
                        // and only synchronizes 'active' objects
                        DefaultMotionState myMotionState = new DefaultMotionState(startTransform);
                        using (var rbInfo = new RigidBodyConstructionInfo(mass, myMotionState, colShape, localInertia))
                        {
                            var body = new RigidBody(rbInfo);
                            World.AddRigidBody(body);
                        }
                    }
                }
            }
        }

        void AddColliders(MultiBody multiBody, Vector3 baseHalfExtents, Vector3 linkHalfExtents)
        {
            // Add a collider for the base
            Quaternion[] worldToLocal = new Quaternion[multiBody.NumLinks + 1];
            Vector3[] localOrigin = new Vector3[multiBody.NumLinks + 1];

            worldToLocal[0] = multiBody.WorldToBaseRot;
            localOrigin[0] = multiBody.BasePosition;

            //if (true)
            {
                var collider = new MultiBodyLinkCollider(multiBody, -1);
                collider.CollisionShape = new BoxShape(baseHalfExtents);

                Matrix tr = Matrix.RotationQuaternion(worldToLocal[0].Inverse);
                tr.Origin = localOrigin[0];
                collider.WorldTransform = tr;

                World.AddCollisionObject(collider, CollisionFilterGroups.StaticFilter,
                    CollisionFilterGroups.DefaultFilter | CollisionFilterGroups.StaticFilter);
                BulletExampleRunner.Get().CreateUnityMultiBodyLinkColliderProxy(collider);
                collider.Friction = Friction;
                multiBody.BaseCollider = collider;
            }

            for (int i = 0; i < multiBody.NumLinks; i++)
            {
                int parent = multiBody.GetParent(i);
                worldToLocal[i + 1] = multiBody.GetParentToLocalRot(i) * worldToLocal[parent + 1];
                localOrigin[i + 1] = localOrigin[parent + 1] + (worldToLocal[i + 1].Inverse.Rotate(multiBody.GetRVector(i)));
            }

            for (int i = 0; i < multiBody.NumLinks; i++)
            {
                var collider = new MultiBodyLinkCollider(multiBody, i);
                collider.CollisionShape = new BoxShape(linkHalfExtents);
                Matrix tr = Matrix.RotationQuaternion(worldToLocal[i + 1].Inverse) * Matrix.Translation(localOrigin[i + 1]);
                collider.WorldTransform = tr;
                World.AddCollisionObject(collider, CollisionFilterGroups.StaticFilter,
                    CollisionFilterGroups.DefaultFilter | CollisionFilterGroups.StaticFilter);
                collider.Friction = Friction;
                BulletExampleRunner.Get().CreateUnityMultiBodyLinkColliderProxy(collider);
                multiBody.GetLink(i).Collider = collider;
            }
        }

        public override RigidBody LocalCreateRigidBody(float mass, Matrix startTransform, CollisionShape shape,bool isKinematic)
        {
            //rigidbody is dynamic if and only if mass is non zero, otherwise static
            bool isDynamic = (mass != 0.0f);

            Vector3 localInertia = Vector3.Zero;
            if (isDynamic)
                shape.CalculateLocalInertia(mass, out localInertia);

            //using motionstate is recommended, it provides interpolation capabilities, and only synchronizes 'active' objects

            RigidBody body;
            using (var rbInfo = new RigidBodyConstructionInfo(mass, null, shape, localInertia))
            {
                body = new RigidBody(rbInfo);
            }
            
            body.WorldTransform = startTransform;
            World.AddRigidBody(body, CollisionFilterGroups.DefaultFilter,
                CollisionFilterGroups.DefaultFilter | CollisionFilterGroups.StaticFilter);
            return body;
        }
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            using (Demo demo = new FeatherStoneDemo())
            {
                GraphicsLibraryManager.Run(demo);
            }
        }
    }
}
