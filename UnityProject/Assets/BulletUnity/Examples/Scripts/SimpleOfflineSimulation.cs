using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BulletSharp;
using BulletSharp.Math;

/* 
A simple physics simulation that is not connected in any way to the Unity scene 
*/
public class SimpleOfflineSimulation : MonoBehaviour {
  
    void Start () {
        //Create a World
        Debug.Log("Initialize physics");
        List<CollisionShape> CollisionShapes = new List<CollisionShape>();

        DefaultCollisionConfiguration CollisionConf = new DefaultCollisionConfiguration();
        CollisionDispatcher Dispatcher = new CollisionDispatcher(CollisionConf);

        DbvtBroadphase Broadphase = new DbvtBroadphase();

        DiscreteDynamicsWorld World = new DiscreteDynamicsWorld(Dispatcher, Broadphase, null, CollisionConf);
        World.Gravity = new BulletSharp.Math.Vector3(0, -10, 0);


        // create a few dynamic rigidbodies
        const float mass = 1.0f;
        //Add a single cube
        RigidBody fallRigidBody;
        BoxShape shape = new BoxShape(1f, 1f, 1f);
        BulletSharp.Math.Vector3 localInertia = BulletSharp.Math.Vector3.Zero;
        shape.CalculateLocalInertia(mass, out localInertia);
        RigidBodyConstructionInfo rbInfo = new RigidBodyConstructionInfo(mass, null, shape, localInertia);
        fallRigidBody = new RigidBody(rbInfo);
        rbInfo.Dispose();
        Matrix st = Matrix.Translation(new BulletSharp.Math.Vector3(0f, 10f, 0f));
        fallRigidBody.WorldTransform = st;
        World.AddRigidBody(fallRigidBody);

        //Step the simulation 300 steps
        for (int i = 0; i < 300; i++)
        {
            World.StepSimulation(1f / 60f, 10);

            Matrix trans;
            fallRigidBody.GetWorldTransform(out trans);

            Debug.Log("box height: " + trans.Origin);
        }

        //Clean up.
        World.RemoveRigidBody(fallRigidBody);
        fallRigidBody.Dispose();

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

            //delete collision shapes
            foreach (CollisionShape ss in CollisionShapes)
                ss.Dispose();
            CollisionShapes.Clear();

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
    }
}
