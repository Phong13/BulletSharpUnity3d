using UnityEngine;
using BulletUnity;
using BulletUnity.Primitives;
using BulletSharp;
using System.Collections;
using System.Collections.Generic;


public class BallThrowTest : MonoBehaviour
{

    public BPhysicsWorld bulletWorld;
    public BRigidBody ballRigidbody;
    public Vector3 ballThrowImpulse;

    public int numberOfSimulationSteps;

    public GameObject ballGhostPrefab;

    bool simulationStarted = false;
    public int startFrame = 0;

    float fixedTimeStep = 1f / 60f;
    int maxSubsteps = 3;


    List<Vector3> ballPositionsRealtime = new List<Vector3>();
    List<Vector3> ballPositionsOfflineSim = new List<Vector3>();

    // Use this for initialization
    IEnumerator Start()
    {
        while (!ballRigidbody.isInWorld)
        {
            yield return null;
        }
        //Remove the rigidbody from the world until we need it
        bulletWorld.RemoveRigidBody(ballRigidbody.GetCollisionObject() as BulletSharp.RigidBody);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.frameCount == 50 && !simulationStarted)
        {
            bulletWorld.AddRigidBody(ballRigidbody);
            simulationStarted = true;
            startFrame = BPhysicsWorld.Get().frameCount;

            //first simulation ==============================
            ballPositionsOfflineSim = OfflineBallSimulation.SimulateBall(ballRigidbody, ballThrowImpulse, numberOfSimulationSteps,false);

            //Second simulation =====================
            ballRigidbody.AddImpulse(ballThrowImpulse);
            
            for (int i = 0; i < ballPositionsOfflineSim.Count; i++)
            {
                Instantiate<GameObject>(ballGhostPrefab).transform.position = ballPositionsOfflineSim[i];
            }
            
        }
        else if (simulationStarted && ballPositionsRealtime.Count < 500)
        {
            ballPositionsRealtime.Add(ballRigidbody.GetCollisionObject().WorldTransform.Origin.ToUnity());
        }
        if (ballPositionsRealtime.Count == 500)
        {
            /*
            for (int i = 0; i < ballPositionsRealtime.Count; i++)
            {
                if (ballPositionsRealtime[i] != ballPositionsOfflineSim[i])
                {
                    Instantiate<GameObject>(ballGhostPrefab).transform.position = ballPositionsRealtime[i];
                    Debug.LogWarning("Diverged " + ballPositionsRealtime[i].ToString("f7") + " " + ballPositionsOfflineSim[i].ToString("f7"));
                }
            }
            */
            //prevent this clause from executing again
            ballPositionsRealtime.Add(Vector3.zero);
        }
    }
}
