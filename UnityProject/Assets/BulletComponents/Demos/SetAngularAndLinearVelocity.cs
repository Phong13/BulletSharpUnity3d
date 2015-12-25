using UnityEngine;
using System.Collections;
using BulletUnity;

public class SetAngularAndLinearVelocity : MonoBehaviour {
    BRigidBody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<BRigidBody>();
        rb.velocity = Vector3.right * 4f;
        rb.angularVelocity = Vector3.up * 5f;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Velocity " + rb.velocity);
	}
}
