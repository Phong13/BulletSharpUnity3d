using System;
using BulletSharp;
using BulletSharp.Math;

namespace RagdollDemo
{
    enum BodyPart
    {
        Pelvis, Spine, Head,
        LeftUpperLeg, LeftLowerLeg,
        RightUpperLeg, RightLowerLeg,
        LeftUpperArm, LeftLowerArm,
        RightUpperArm, RightLowerArm,
        Count
    };

    enum Joint
    {
        PelvisSpine, SpineHead,
        LeftHip, LeftKnee,
        RightHip, RightKnee,
        LeftShoulder, LeftElbow,
        RightShoulder, RightElbow,
        Count
    };

    class Ragdoll
    {
        const float ConstraintDebugSize = 0.2f;
        const float PI_2 = (float)Math.PI / 2;
        const float PI_4 = (float)Math.PI / 4;

        DynamicsWorld ownerWorld;
        CollisionShape[] shapes = new CollisionShape[(int)BodyPart.Count];
        RigidBody[] bodies = new RigidBody[(int)BodyPart.Count];
        TypedConstraint[] joints = new TypedConstraint[(int)Joint.Count];

        public Ragdoll(DynamicsWorld ownerWorld, Vector3 positionOffset)
        {
            this.ownerWorld = ownerWorld;

            // Setup the geometry
            shapes[(int)BodyPart.Pelvis] = new CapsuleShape(0.15f, 0.20f);
            shapes[(int)BodyPart.Spine] = new CapsuleShape(0.15f, 0.28f);
            shapes[(int)BodyPart.Head] = new CapsuleShape(0.10f, 0.05f);
            shapes[(int)BodyPart.LeftUpperLeg] = new CapsuleShape(0.05f, 0.45f);
            shapes[(int)BodyPart.LeftLowerLeg] = new CapsuleShape(0.04f, 0.37f);
            shapes[(int)BodyPart.RightUpperLeg] = new CapsuleShape(0.05f, 0.45f);
            shapes[(int)BodyPart.RightLowerLeg] = new CapsuleShape(0.04f, 0.37f);
            shapes[(int)BodyPart.LeftUpperArm] = new CapsuleShape(0.04f, 0.33f);
            shapes[(int)BodyPart.LeftLowerArm] = new CapsuleShape(0.03f, 0.25f);
            shapes[(int)BodyPart.RightUpperArm] = new CapsuleShape(0.04f, 0.33f);
            shapes[(int)BodyPart.RightLowerArm] = new CapsuleShape(0.03f, 0.25f);

            Matrix offset = Matrix.Translation(positionOffset);
            Matrix transform;
            transform = offset * Matrix.Translation(0, 1, 0);
            bodies[(int)BodyPart.Pelvis] = LocalCreateRigidBody(1, transform, shapes[(int)BodyPart.Pelvis]);

            transform = offset * Matrix.Translation(0, 1.2f, 0);
            bodies[(int)BodyPart.Spine] = LocalCreateRigidBody(1, transform, shapes[(int)BodyPart.Spine]);

            transform = offset * Matrix.Translation(0, 1.6f, 0);
            bodies[(int)BodyPart.Head] = LocalCreateRigidBody(1, transform, shapes[(int)BodyPart.Head]);

            transform = offset * Matrix.Translation(-0.18f, 0.6f, 0);
            bodies[(int)BodyPart.LeftUpperLeg] = LocalCreateRigidBody(1, transform, shapes[(int)BodyPart.LeftUpperLeg]);

            transform = offset * Matrix.Translation(-0.18f, 0.2f, 0);
            bodies[(int)BodyPart.LeftLowerLeg] = LocalCreateRigidBody(1, transform, shapes[(int)BodyPart.LeftLowerLeg]);

            transform = offset * Matrix.Translation(0.18f, 0.65f, 0);
            bodies[(int)BodyPart.RightUpperLeg] = LocalCreateRigidBody(1, transform, shapes[(int)BodyPart.RightUpperLeg]);

            transform = offset * Matrix.Translation(0.18f, 0.2f, 0);
            bodies[(int)BodyPart.RightLowerLeg] = LocalCreateRigidBody(1, transform, shapes[(int)BodyPart.RightLowerLeg]);

            transform = Matrix.RotationZ(PI_2) * offset * Matrix.Translation(-0.35f, 1.45f, 0);
            bodies[(int)BodyPart.LeftUpperArm] = LocalCreateRigidBody(1, transform, shapes[(int)BodyPart.LeftUpperArm]);

            transform = Matrix.RotationZ(PI_2) * offset * Matrix.Translation(-0.7f, 1.45f, 0);
            bodies[(int)BodyPart.LeftLowerArm] = LocalCreateRigidBody(1, transform, shapes[(int)BodyPart.LeftLowerArm]);

            transform = Matrix.RotationZ(-PI_2) * offset * Matrix.Translation(0.35f, 1.45f, 0);
            bodies[(int)BodyPart.RightUpperArm] = LocalCreateRigidBody(1, transform, shapes[(int)BodyPart.RightUpperArm]);

            transform = Matrix.RotationZ(-PI_2) * offset * Matrix.Translation(0.7f, 1.45f, 0);
            bodies[(int)BodyPart.RightLowerArm] = LocalCreateRigidBody(1, transform, shapes[(int)BodyPart.RightLowerArm]);

            // Setup some damping on the m_bodies
            foreach (RigidBody body in bodies)
            {
                body.SetDamping(0.05f, 0.85f);
                body.DeactivationTime = 0.8f;
                body.SetSleepingThresholds(1.6f, 2.5f);
            }

            // Now setup the constraints
            HingeConstraint hingeC;
            ConeTwistConstraint coneC;

            Matrix localA, localB;

            localA = Matrix.RotationYawPitchRoll(PI_2, 0, 0) * Matrix.Translation(0, 0.15f, 0);
            localB = Matrix.RotationYawPitchRoll(PI_2, 0, 0) * Matrix.Translation(0, -0.15f, 0);
            hingeC = new HingeConstraint(bodies[(int)BodyPart.Pelvis], bodies[(int)BodyPart.Spine], localA, localB);
            hingeC.SetLimit(-PI_4, PI_2);
            joints[(int)Joint.PelvisSpine] = hingeC;
            hingeC.DebugDrawSize = ConstraintDebugSize;

            ownerWorld.AddConstraint(joints[(int)Joint.PelvisSpine], true);


            localA = Matrix.RotationYawPitchRoll(0, 0, PI_2) * Matrix.Translation(0, 0.30f, 0);
            localB = Matrix.RotationYawPitchRoll(0, 0, PI_2) * Matrix.Translation(0, -0.14f, 0);
            coneC = new ConeTwistConstraint(bodies[(int)BodyPart.Spine], bodies[(int)BodyPart.Head], localA, localB);
            coneC.SetLimit(PI_4, PI_4, PI_2);
            joints[(int)Joint.SpineHead] = coneC;
            coneC.DebugDrawSize = ConstraintDebugSize;

            ownerWorld.AddConstraint(joints[(int)Joint.SpineHead], true);


            localA = Matrix.RotationYawPitchRoll(0, 0, -PI_4 * 5) * Matrix.Translation(-0.18f, -0.18f, 0);
            localB = Matrix.RotationYawPitchRoll(0, 0, -PI_4 * 5) * Matrix.Translation(0, 0.225f, 0);
            coneC = new ConeTwistConstraint(bodies[(int)BodyPart.Pelvis], bodies[(int)BodyPart.LeftUpperLeg], localA, localB);
            coneC.SetLimit(PI_4, PI_4, 0);
            joints[(int)Joint.LeftHip] = coneC;
            coneC.DebugDrawSize = ConstraintDebugSize;

            ownerWorld.AddConstraint(joints[(int)Joint.LeftHip], true);


            localA = Matrix.RotationYawPitchRoll(PI_2, 0, 0) * Matrix.Translation(0, -0.225f, 0);
            localB = Matrix.RotationYawPitchRoll(PI_2, 0, 0) * Matrix.Translation(0, 0.185f, 0);
            hingeC = new HingeConstraint(bodies[(int)BodyPart.LeftUpperLeg], bodies[(int)BodyPart.LeftLowerLeg], localA, localB);
            hingeC.SetLimit(0, PI_2);
            joints[(int)Joint.LeftKnee] = hingeC;
            hingeC.DebugDrawSize = ConstraintDebugSize;

            ownerWorld.AddConstraint(joints[(int)Joint.LeftKnee], true);


            localA = Matrix.RotationYawPitchRoll(0, 0, PI_4) * Matrix.Translation(0.18f, -0.10f, 0);
            localB = Matrix.RotationYawPitchRoll(0, 0, PI_4) * Matrix.Translation(0, 0.225f, 0);
            coneC = new ConeTwistConstraint(bodies[(int)BodyPart.Pelvis], bodies[(int)BodyPart.RightUpperLeg], localA, localB);
            coneC.SetLimit(PI_4, PI_4, 0);
            joints[(int)Joint.RightHip] = coneC;
            coneC.DebugDrawSize = ConstraintDebugSize;

            ownerWorld.AddConstraint(joints[(int)Joint.RightHip], true);


            localA = Matrix.RotationYawPitchRoll(PI_2, 0, 0) * Matrix.Translation(0, -0.225f, 0);
            localB = Matrix.RotationYawPitchRoll(PI_2, 0, 0) * Matrix.Translation(0, 0.185f, 0);
            hingeC = new HingeConstraint(bodies[(int)BodyPart.RightUpperLeg], bodies[(int)BodyPart.RightLowerLeg], localA, localB);
            hingeC.SetLimit(0, PI_2);
            joints[(int)Joint.RightKnee] = hingeC;
            hingeC.DebugDrawSize = ConstraintDebugSize;

            ownerWorld.AddConstraint(joints[(int)Joint.RightKnee], true);


            localA = Matrix.RotationYawPitchRoll(0, 0, (float)Math.PI) * Matrix.Translation(-0.2f, 0.15f, 0);
            localB = Matrix.RotationYawPitchRoll(0, 0, PI_2) * Matrix.Translation(0, -0.18f, 0);
            coneC = new ConeTwistConstraint(bodies[(int)BodyPart.Spine], bodies[(int)BodyPart.LeftUpperArm], localA, localB);
            coneC.SetLimit(PI_2, PI_2, 0);
            joints[(int)Joint.LeftShoulder] = coneC;
            coneC.DebugDrawSize = ConstraintDebugSize;

            ownerWorld.AddConstraint(joints[(int)Joint.LeftShoulder], true);


            localA = Matrix.RotationYawPitchRoll(PI_2, 0, 0) * Matrix.Translation(0, 0.18f, 0);
            localB = Matrix.RotationYawPitchRoll(PI_2, 0, 0) * Matrix.Translation(0, -0.14f, 0);
            hingeC = new HingeConstraint(bodies[(int)BodyPart.LeftUpperArm], bodies[(int)BodyPart.LeftLowerArm], localA, localB);
            hingeC.SetLimit(0, PI_2);
            joints[(int)Joint.LeftElbow] = hingeC;
            hingeC.DebugDrawSize = ConstraintDebugSize;

            ownerWorld.AddConstraint(joints[(int)Joint.LeftElbow], true);


            localA = Matrix.RotationYawPitchRoll(0, 0, 0) * Matrix.Translation(0.2f, 0.15f, 0);
            localB = Matrix.RotationYawPitchRoll(0, 0, PI_2) * Matrix.Translation(0, -0.18f, 0);
            coneC = new ConeTwistConstraint(bodies[(int)BodyPart.Spine], bodies[(int)BodyPart.RightUpperArm], localA, localB);
            coneC.SetLimit(PI_2, PI_2, 0);
            joints[(int)Joint.RightShoulder] = coneC;
            coneC.DebugDrawSize = ConstraintDebugSize;

            ownerWorld.AddConstraint(joints[(int)Joint.RightShoulder], true);


            localA = Matrix.RotationYawPitchRoll(PI_2, 0, 0) * Matrix.Translation(0, 0.18f, 0);
            localB = Matrix.RotationYawPitchRoll(PI_2, 0, 0) * Matrix.Translation(0, -0.14f, 0);
            hingeC = new HingeConstraint(bodies[(int)BodyPart.RightUpperArm], bodies[(int)BodyPart.RightLowerArm], localA, localB);
            //hingeC.SetLimit(-PI_2, 0);
            hingeC.SetLimit(0, PI_2);
            joints[(int)Joint.RightElbow] = hingeC;
            hingeC.DebugDrawSize = ConstraintDebugSize;

            ownerWorld.AddConstraint(joints[(int)Joint.RightElbow], true);
        }

        RigidBody LocalCreateRigidBody(float mass, Matrix startTransform, CollisionShape shape)
        {
            bool isDynamic = (mass != 0.0f);

            Vector3 localInertia = Vector3.Zero;
            if (isDynamic)
                shape.CalculateLocalInertia(mass, out localInertia);

            DefaultMotionState myMotionState = new DefaultMotionState(startTransform);

            RigidBodyConstructionInfo rbInfo = new RigidBodyConstructionInfo(mass, myMotionState, shape, localInertia);
            RigidBody body = new RigidBody(rbInfo);
            rbInfo.Dispose();

            ownerWorld.AddRigidBody(body);

            return body;
        }
    }
}
