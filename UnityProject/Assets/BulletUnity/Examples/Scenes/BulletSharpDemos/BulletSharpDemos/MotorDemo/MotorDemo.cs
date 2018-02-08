using BulletSharp;
using BulletSharp.Math;
using DemoFramework;
using System;
using System.Collections.Generic;
using BulletSharpExamples;

namespace MotorDemo
{
    class MotorDemo : Demo
    {
        Vector3 eye = new Vector3(8, 4, 8);
        Vector3 target = new Vector3(0, 0, 0);

        List<TestRig> rigs = new List<TestRig>();
        float m_Time;
        float fCyclePeriod;
        float fMuscleStrength;

        const int NumLegs = 6;
        const int BodyPartCount = 2 * NumLegs + 1;
        const int JointCount = BodyPartCount - 1;

        class TestRig
        {
	        DynamicsWorld ownerWorld;
	        CollisionShape[] shapes = new CollisionShape[BodyPartCount];
	        RigidBody[] bodies = new RigidBody[BodyPartCount];
	        TypedConstraint[] joints = new TypedConstraint[JointCount];

	        public RigidBody LocalCreateRigidBody(float mass, Matrix startTransform, CollisionShape shape)
            {
                //rigidbody is dynamic if and only if mass is non zero, otherwise static
                bool isDynamic = (mass != 0.0f);

                Vector3 localInertia = Vector3.Zero;
                if (isDynamic)
                    shape.CalculateLocalInertia(mass, out localInertia);

                //using motionstate is recommended, it provides interpolation capabilities, and only synchronizes 'active' objects
                DefaultMotionState myMotionState = new DefaultMotionState(startTransform);
                RigidBody body;
                using (var rbInfo = new RigidBodyConstructionInfo(mass, myMotionState, shape, localInertia))
                {
                    body = new RigidBody(rbInfo);
                }

                ownerWorld.AddRigidBody(body);

                return body;
            }

            public TestRig(DynamicsWorld ownerWorld, Vector3 positionOffset, bool bFixed)
            {
                this.ownerWorld = ownerWorld;
                Vector3 vUp = new Vector3(0, 1, 0);

                //
                // Setup geometry
                //
                const float fBodySize = 0.25f;
                const float fLegLength = 0.45f;
                const float fForeLegLength = 0.75f;
                const float PI_2 = (float)(0.5f * Math.PI);
                const float PI_4 = (float)(0.25f * Math.PI);
                const float PI_8 = (float)(0.125f * Math.PI);
                shapes[0] = new CapsuleShape(fBodySize, 0.10f);
                int i;
                for (i = 0; i < NumLegs; i++)
                {
                    shapes[1 + 2 * i] = new CapsuleShape(0.10f, fLegLength);
                    shapes[2 + 2 * i] = new CapsuleShape(0.08f, fForeLegLength);
                }

                //
                // Setup rigid bodies
                //
                const float fHeight = 0.5f;
                Matrix offset = Matrix.Translation(positionOffset);

                // root
                Vector3 vRoot = new Vector3(0, fHeight, 0);
                Matrix transform = Matrix.Translation(vRoot);
                if (bFixed)
                {
                    bodies[0] = LocalCreateRigidBody(0, transform * offset, shapes[0]);
                }
                else
                {
                    bodies[0] = LocalCreateRigidBody(1, transform * offset, shapes[0]);
                }
                // legs
                for (i = 0; i < NumLegs; i++)
                {
                    float fAngle = (float)(2 * Math.PI * i / NumLegs);
                    float fSin = (float)Math.Sin(fAngle);
                    float fCos = (float)Math.Cos(fAngle);

                    Vector3 vBoneOrigin = new Vector3(fCos * (fBodySize + 0.5f * fLegLength), fHeight, fSin * (fBodySize + 0.5f * fLegLength));

                    // thigh
                    Vector3 vToBone = (vBoneOrigin - vRoot);
                    vToBone.Normalize();
                    Vector3 vAxis = Vector3.Cross(vToBone, vUp);
                    transform = Matrix.RotationQuaternion(Quaternion.RotationAxis(vAxis, PI_2)) * Matrix.Translation(vBoneOrigin);
                    bodies[1 + 2 * i] = LocalCreateRigidBody(1, transform * offset, shapes[1 + 2 * i]);

                    // shin
                    transform = Matrix.Translation(fCos * (fBodySize + fLegLength), fHeight - 0.5f * fForeLegLength, fSin * (fBodySize + fLegLength));
                    bodies[2 + 2 * i] = LocalCreateRigidBody(1, transform * offset, shapes[2 + 2 * i]);
                }

                // Setup some damping on the bodies
                for (i = 0; i < BodyPartCount; ++i)
                {
                    bodies[i].SetDamping(0.05f, 0.85f);
                    bodies[i].DeactivationTime = 0.8f;
                    //bodies[i].SetSleepingThresholds(1.6f, 2.5f);
                    bodies[i].SetSleepingThresholds(0.5f, 0.5f);
                }

                //
                // Setup the constraints
                //
                HingeConstraint hingeC;
                //ConeTwistConstraint coneC;

                Matrix localA, localB, localC;

                for (i = 0; i < NumLegs; i++)
                {
                    float fAngle = (float)(2 * Math.PI * i / NumLegs);
                    float fSin = (float)Math.Sin(fAngle);
                    float fCos = (float)Math.Cos(fAngle);

                    // hip joints
                    localA = Matrix.RotationYawPitchRoll(-fAngle, 0, 0) * Matrix.Translation(fCos * fBodySize, 0, fSin * fBodySize); // OK
                    localB = localA * bodies[0].WorldTransform * Matrix.Invert(bodies[1 + 2 * i].WorldTransform);
                    hingeC = new HingeConstraint(bodies[0], bodies[1 + 2 * i], localA, localB);
                    hingeC.SetLimit(-0.75f * PI_4, PI_8);
                    //hingeC.SetLimit(-0.1f, 0.1f);
                    joints[2 * i] = hingeC;
                    ownerWorld.AddConstraint(joints[2 * i], true);

                    // knee joints
                    localA = Matrix.RotationYawPitchRoll(-fAngle, 0, 0) * Matrix.Translation(fCos * (fBodySize + fLegLength), 0, fSin * (fBodySize + fLegLength));
                    localB = localA * bodies[0].WorldTransform * Matrix.Invert(bodies[1 + 2 * i].WorldTransform);
                    localC = localA * bodies[0].WorldTransform * Matrix.Invert(bodies[2 + 2 * i].WorldTransform);
                    hingeC = new HingeConstraint(bodies[1 + 2 * i], bodies[2 + 2 * i], localB, localC);
                    //hingeC.SetLimit(-0.01f, 0.01f);
                    hingeC.SetLimit(-PI_8, 0.2f);
                    joints[1 + 2 * i] = hingeC;
                    ownerWorld.AddConstraint(joints[1 + 2 * i], true);
                }
            }

            public void Dispose()
            {
                int i;

                // Remove all constraints
                for (i = 0; i < JointCount; ++i)
                {
                    ownerWorld.RemoveConstraint(joints[i]);
                    joints[i].Dispose();
                    joints[i] = null;
                }

                // Remove all bodies and shapes
                for (i = 0; i < BodyPartCount; ++i)
                {
                    ownerWorld.RemoveRigidBody(bodies[i]);
                    bodies[i].MotionState.Dispose();
                    bodies[i].Dispose();
                    bodies[i] = null;
                    shapes[i].Dispose();
                    shapes[i] = null;
                }
            }

            public TypedConstraint[] GetJoints()
            {
                return joints;
            }
        };

        void MotorPreTickCallback(DynamicsWorld world, float timeStep)
        {
            SetMotorTargets(timeStep);
        }

        protected override void OnInitialize()
        {
            Freelook.SetEyeTarget(eye, target);

            Graphics.SetFormText("BulletSharp - Motor Demo");
            Graphics.SetInfoText("Move using mouse and WASD+shift\n" +
                "F3 - Toggle debug\n" +
                //"F11 - Toggle fullscreen\n" +
                "Space - Shoot box");

            DebugDrawMode = DebugDrawModes.DrawConstraintLimits | DebugDrawModes.DrawConstraints | DebugDrawModes.DrawWireframe;
        }

        protected override void OnInitializePhysics()
        {
            // collision configuration contains default setup for memory, collision setup
            CollisionConf = new DefaultCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(CollisionConf);

            Vector3 worldAabbMin = new Vector3(-10000, -10000, -10000);
            Vector3 worldAabbMax = new Vector3(10000, 10000, 10000);
            Broadphase = new AxisSweep3(worldAabbMin, worldAabbMax);

            Solver = new SequentialImpulseConstraintSolver();

            World = new DiscreteDynamicsWorld(Dispatcher, Broadphase, Solver, CollisionConf);
            World.Gravity = new Vector3(0, -10, 0);
            World.SetInternalTickCallback(MotorPreTickCallback, this, true);

            // create the ground
            CollisionShape groundShape = new BoxShape(200, 10, 200);
            CollisionShapes.Add(groundShape);
            CollisionObject ground = LocalCreateRigidBody(0, Matrix.Translation(0, -10, 0), groundShape);
            ground.UserObject = "Ground";

            fCyclePeriod = 2000.0f;
            fMuscleStrength = 0.5f;
            m_Time = 0;

            SpawnTestRig(new Vector3(1, 0.5f, 0), false);
            SpawnTestRig(new Vector3(-2, 0.5f, 0), true);
        }

        private void SpawnTestRig(Vector3 startOffset, bool isFixed)
        {
            TestRig rig = new TestRig(World, startOffset, isFixed);
            rigs.Add(rig);
        }


        void SetMotorTargets(float deltaTime)
        {
            float ms = deltaTime * 1000000.0f;
            float minFPS = 1000000.0f / 60.0f;
            if (ms > minFPS)
                ms = minFPS;

            m_Time += ms;

            //
            // set per-frame sinusoidal position targets using angular motor (hacky?)
            //
            foreach (var rig in rigs)
            {
                for (int i = 0; i < 2 * NumLegs; i++)
                {
                    HingeConstraint hingeC = rig.GetJoints()[i] as HingeConstraint;
                    float fCurAngle = hingeC.HingeAngle;

                    float fTargetPercent = ((int)(m_Time / 1000.0f) % (int)fCyclePeriod) / fCyclePeriod;
                    float fTargetAngle = (float)(0.5 * (1 + Math.Sin(2.0f * Math.PI * fTargetPercent)));
                    float fTargetLimitAngle = hingeC.LowerLimit + fTargetAngle * (hingeC.UpperLimit - hingeC.LowerLimit);
                    float fAngleError = fTargetLimitAngle - fCurAngle;
                    float fDesiredAngularVel = 1000000.0f * fAngleError / ms;
                    hingeC.EnableAngularMotor(true, fDesiredAngularVel, fMuscleStrength);
                }
            }
        }

        public override void ExitPhysics()
        {
            foreach (var testRig in rigs)
            {
                testRig.Dispose();
            }
            rigs.Clear();
            base.ExitPhysics();
        }
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            using (Demo demo = new MotorDemo())
            {
                GraphicsLibraryManager.Run(demo);
            }
        }
    }
}
