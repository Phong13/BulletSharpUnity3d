using UnityEngine;
using System.Collections;
using BulletSharp;
using BulletSharp.Math;
using BulletUnity;

public class BulletRigidBodyProxy : MonoBehaviour {
    public CollisionObject target;
	
	// Update is called once per frame
	void Update () {
        Matrix4x4 m = target.WorldTransform.ToUnity();
        transform.position = BSExtensionMethods2.ExtractTranslationFromMatrix(ref m);
        transform.rotation = BSExtensionMethods2.ExtractRotationFromMatrix(ref m);
    }
}
