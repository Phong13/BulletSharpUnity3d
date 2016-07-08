using UnityEngine;
using System.Collections.Generic;
using BulletUnity;
using BulletSharp;
using BulletSharp.Math;

public static class OfflineBallSimulation {
    public static DiscreteDynamicsWorld World;

    //IMPORTANT Time.fixedTime must match the timestep being used here.
    public static List<UnityEngine.Vector3> SimulateBall(BRigidBody ballRb, UnityEngine.Vector3 ballThrowForce, int numberOfSimulationSteps, bool reverseOrder)
	{
		var ballPositions = new List<UnityEngine.Vector3>(numberOfSimulationSteps);

		//Create a World
		Debug.Log("Initialize physics");
        
		CollisionConfiguration CollisionConf;
		CollisionDispatcher Dispatcher;
	    BroadphaseInterface Broadphase;
        CollisionWorld cw;
        SequentialImpulseConstraintSolver Solver;
        BulletSharp.SoftBody.SoftBodyWorldInfo softBodyWorldInfo;

        //This should create a copy of the BPhysicsWorld with the same settings
        BPhysicsWorld bw = BPhysicsWorld.Get();
        bw.CreatePhysicsWorld(out cw, out CollisionConf, out Dispatcher, out Broadphase, out Solver, out softBodyWorldInfo);
        World = (DiscreteDynamicsWorld) cw;

        //Copy all existing rigidbodies in scene
        // IMPORTANT rigidbodies must be added to the offline world in the same order that they are in the source world
        // this is because collisions must be resolved in the same order for the sim to be deterministic
        DiscreteDynamicsWorld sourceWorld = (DiscreteDynamicsWorld) bw.world;
		BulletSharp.RigidBody bulletBallRb = null;
        BulletSharp.Math.Matrix mm = BulletSharp.Math.Matrix.Identity;
		for(int i = 0; i < sourceWorld.NumCollisionObjects; i++)
		{
            CollisionObject co = sourceWorld.CollisionObjectArray[i];
            if (co != null && co.UserObject is BRigidBody)
            {
                BRigidBody rb = (BRigidBody) co.UserObject;
                float mass = rb.isDynamic() ? rb.mass : 0f;
                BCollisionShape existingShape = rb.GetComponent<BCollisionShape>();
                CollisionShape shape = null;
                if (existingShape is BSphereShape)
                {
                    shape = ((BSphereShape)existingShape).CopyCollisionShape();
                }
                else if (existingShape is BBoxShape)
                {
                    shape = ((BBoxShape)existingShape).CopyCollisionShape();
                }

                RigidBody bulletRB = null;
                BulletSharp.Math.Vector3 localInertia = new BulletSharp.Math.Vector3();
                rb.CreateOrConfigureRigidBody(ref bulletRB, ref localInertia, shape, null);
                BulletSharp.Math.Vector3 pos = rb.GetCollisionObject().WorldTransform.Origin;
                BulletSharp.Math.Quaternion rot = rb.GetCollisionObject().WorldTransform.GetOrientation();
                BulletSharp.Math.Matrix.AffineTransformation(1f, ref rot, ref pos, out mm);
                bulletRB.WorldTransform = mm;
                World.AddRigidBody(bulletRB, rb.groupsIBelongTo, rb.collisionMask);
                if (rb == ballRb)
                {
                    bulletBallRb = bulletRB;
                    bulletRB.ApplyCentralImpulse(ballThrowForce.ToBullet());
                }
            }
        }

		//Step the simulation numberOfSimulationSteps times
		for (int i = 0; i < numberOfSimulationSteps; i++)
		{
			int numSteps = World.StepSimulation(1f / 60f, 10, 1f / 60f);
			ballPositions.Add(bulletBallRb.WorldTransform.Origin.ToUnity());
        }

		UnityEngine.Debug.Log("ExitPhysics");
		if (World != null)
		{
			//remove/dispose constraints
			int i;
			for (i = World.NumConstraints - 1; i >= 0; i--)
			{
				TypedConstraint constraint = World.GetConstraint(i);
				World.RemoveConstraint(constraint);
				constraint.Dispose();
			}

			//remove the rigidbodies from the dynamics world and delete them
			for (i = World.NumCollisionObjects - 1; i >= 0; i--)
			{
				CollisionObject obj = World.CollisionObjectArray[i];
				RigidBody body = obj as RigidBody;
				if (body != null && body.MotionState != null)
				{
					body.MotionState.Dispose();
				}
				World.RemoveCollisionObject(obj);
				obj.Dispose();
			}

			World.Dispose();
			Broadphase.Dispose();
			Dispatcher.Dispose();
			CollisionConf.Dispose();
		}

		if (Broadphase != null)
		{
			Broadphase.Dispose();
		}
		if (Dispatcher != null)
		{
			Dispatcher.Dispose();
		}
		if (CollisionConf != null)
		{
			CollisionConf.Dispose();
		}

		return ballPositions;
	}
}
