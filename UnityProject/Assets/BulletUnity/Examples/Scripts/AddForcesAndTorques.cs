using UnityEngine;
using System.Collections;
using BulletUnity;

public class AddForcesAndTorques : MonoBehaviour {
    BRigidBody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<BRigidBody>();
        
	}

    void FixedUpdate()
    {
        if (Time.frameCount > 10 && Time.frameCount < 20)
        {
            rb.AddTorque(Vector3.right * 100f);
            rb.AddTorque(Vector3.up * 10f);
        }
    }
	
}
