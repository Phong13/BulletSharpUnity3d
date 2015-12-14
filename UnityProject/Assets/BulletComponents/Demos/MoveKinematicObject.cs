using UnityEngine;
using System.Collections;

public class MoveKinematicObject : MonoBehaviour {
    BRigidBody brb;
    void Start()
    {
        brb = GetComponent<BRigidBody>();
    }

    void FixedUpdate()
    {
        transform.position = new UnityEngine.Vector3(Mathf.Sin(Time.time),0f,Mathf.Cos(Time.time)) * 5f;
        //test switching between dynamic and kinematic
        if (Time.frameCount % 200 == 0)
        {
            brb.type = BRigidBody.RBType.dynamic;
        }
    }
}
