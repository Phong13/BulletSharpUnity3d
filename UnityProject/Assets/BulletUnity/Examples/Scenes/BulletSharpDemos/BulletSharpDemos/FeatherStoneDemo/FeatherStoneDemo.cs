using BulletSharp;
using BulletSharp.Math;
using DemoFramework;
using System;

namespace FeatherStoneDemo
{
    internal static class Program
    {
        //[STAThread]
        //static void Main()
        //{
        //    DemoRunner.Run<FeatherStoneDemo>();
        //}
    }

    internal sealed class FeatherStoneDemo : IDemoConfiguration
    {
        public ISimulation CreateSimulation(Demo demo)
        {
            demo.FreeLook.Eye = new Vector3(0, 5, 10);
            demo.FreeLook.Target = Vector3.Zero;
            demo.Graphics.WindowTitle = "BulletSharp - FeatherStone Demo";
            return new FeatherStoneDemoSimulation();
        }
    }

    internal sealed class FeatherStoneDemoSimulation : ISimulation
    {
        private const int NumBoxesX = 5, NumBoxesY = 5, NumBoxesZ = 5;
        private Vector3 _startPosition = new Vector3(-5, 2, -3);

        private const float Friction = 1.0f;

        private MultiBodyConstraintSolver _solver;

        public FeatherStoneDemoSimulation()
        {
            CollisionConfiguration = new DefaultCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(CollisionConfiguration);
            Broadphase = new DbvtBroadphase();
            _solver = new MultiBodyConstraintSolver();
            MultiBodyWorld = new MultiBodyDynamicsWorld(Dispatcher, Broadphase, _solver, CollisionConfiguration);

            CreateGround();

            int numLinks = 5;
            bool spherical = true;
            bool floatingBase = false;
            var basePosition = new Vector3(-0.4f, 3.0f, 0.0f);
            var baseHalfExtents = new Vector3(0.05f, 0.37f, 0.1f);
            var linkHalfExtents = new Vector3(0.05f, 0.37f, 0.1f);
            var multiBody = CreateFeatherstoneMultiBody(MultiBodyWorld, numLinks, basePosition, baseHalfExtents, linkHalfExtents, spherical, floatingBase);

            bool damping = true;
            if (damping)
            {
                multiBody.LinearDamping = 0.1f;
                multiBody.AngularDamping = 0.9f;
            }
            else
            {
                multiBody.LinearDamping = 0;
                multiBody.AngularDamping = 0;
            }

            if (numLinks > 0)
            {
                float q0 = 45.0f * (float)Math.PI / 180.0f;
                if (spherical)
                {
                    Quaternion quat0 = Quaternion.RotationAxis(Vector3.Normalize(new Vector3(1, 1, 0)), q0);
                    quat0.Normalize();
                    multiBody.SetJointPosMultiDof(0, new float[] { quat0.X, quat0.Y, quat0.Z, quat0.W });
                }
                else
                {
                    multiBody.SetJointPosMultiDof(0, new float[] { q0 });
                }
            }
            CreateColliders(multiBody, baseHalfExtents, linkHalfExtents);
            CreateRigidBody(1, Matrix.Translation(0, -0.95f, 0), new BoxShape(0.5f, 0.5f, 0.5f));
        }

        public CollisionConfiguration CollisionConfiguration { get; set; }
        public CollisionDispatcher Dispatcher { get; set; }
        public BroadphaseInterface Broadphase { get; set; }
        public DiscreteDynamicsWorld World {
            get { return MultiBodyWorld; }
            set { MultiBodyWorld = (MultiBodyDynamicsWorld) value;  }
        }
        private MultiBodyDynamicsWorld MultiBodyWorld { get; set; }

        public void Dispose()
        {
            _solver.Dispose();

            this.StandardCleanup();
        }

        private void CreateGround()
        {
            var groundShape = new BoxShape(50, 50, 50);
            //groundShape.InitializePolyhedralFeatures();
            //var groundShape = new StaticPlaneShape(new Vector3(0, 1, 0), 50);

            CollisionObject ground = CreateRigidBody(0, Matrix.Translation(0, -51.55f, 0), groundShape);
            ground.UserObject = "Ground";
        }

        private MultiBody CreateFeatherstoneMultiBody(MultiBodyDynamicsWorld world, int numLinks,
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

            var multiBody = new MultiBody(numLinks, mass, inertia, !floating, false)
            {
                HasSelfCollision = false,
                CanSleep = true,
                UseGyroTerm = true,
                BasePosition = basePosition
            };

            //multiBody.BaseVelocity = Vector3.Zero;
            //multiBody.WorldToBaseRot = new Quaternion(0, 0, 1, -0.125f * (float)Math.PI);
            multiBody.WorldToBaseRot = Quaternion.Identity;

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
                    multiBody.SetupSpherical(i, linkMass, linkInertia, i - 1,
                        Quaternion.Identity, parentComToCurrentPivot, currentPivotToCurrentCom, false);
                }
                else
                {
                    Vector3 hingeJointAxis = new Vector3(1, 0, 0);
                    multiBody.SetupRevolute(i, linkMass, linkInertia, i - 1,
                        Quaternion.Identity, hingeJointAxis, parentComToCurrentPivot, currentPivotToCurrentCom, false);
                }
            }

            multiBody.FinalizeMultiDof();

            MultiBodyWorld.AddMultiBody(multiBody);

            return multiBody;
        }

        private void CreateBoxes()
        {
            const float mass = 1.0f;

            var shape = new BoxShape(1);
            Vector3 localInertia = shape.CalculateLocalInertia(mass);

            var rbInfo = new RigidBodyConstructionInfo(mass, null, shape, localInertia);

            for (int y = 0; y < NumBoxesY; y++)
            {
                for (int x = 0; x < NumBoxesX; x++)
                {
                    for (int z = 0; z < NumBoxesZ; z++)
                    {
                        Vector3 position = _startPosition + 3 * new Vector3(x, y, z);

                        // using motionstate is recommended, it provides interpolation capabilities
                        // and only synchronizes 'active' objects
                        rbInfo.MotionState = new DefaultMotionState(Matrix.Translation(position));
                        var body = new RigidBody(rbInfo);
                        World.AddRigidBody(body);
                    }
                }
            }

            rbInfo.Dispose();
        }

        private void CreateColliders(MultiBody multiBody, Vector3 baseHalfExtents, Vector3 linkHalfExtents)
        {
            // Add a collider for the base
            var worldToLocal = new Quaternion[multiBody.NumLinks + 1];
            var localOrigin = new Vector3[multiBody.NumLinks + 1];

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

                multiBody.GetLink(i).Collider = collider;
            }
        }

        private RigidBody CreateRigidBody(float mass, Matrix startTransform, CollisionShape shape)
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
}
