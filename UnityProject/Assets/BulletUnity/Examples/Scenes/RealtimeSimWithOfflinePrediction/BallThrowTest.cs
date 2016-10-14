using UnityEngine;
using BulletUnity;
using BulletUnity.Primitives;
using BulletSharp;
using System.Collections;
using System.Collections.Generic;


public class BallThrowTest : MonoBehaviour
{
    public enum BallStatus
    {

    }
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
    void Awake()
    {
        Debug.Log("Awake " + Time.frameCount);
        startFrame = Time.frameCount;
        ballRigidbody.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount - startFrame == 50 && !simulationStarted)
        {
            Debug.Log("Starting simulation.");
            ballRigidbody.gameObject.SetActive(true);
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
            //prevent this clause from executing again
            ballPositionsRealtime.Add(Vector3.zero);
        }
    }
}
