using UnityEngine;
using System.Collections;
using BulletUnity;

public class MoveKinematicObject : MonoBehaviour {

    void FixedUpdate()
    {
        transform.position = new UnityEngine.Vector3(Mathf.Sin(Time.time),0f,Mathf.Cos(Time.time)) * 5f;
        //test switching between dynamic and kinematic

    }
}
