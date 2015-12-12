using UnityEngine;
using System.Collections;
using BulletSharp;
using BulletSharp.Math;

public class BulletMultiBodyLinkColliderProxy : MonoBehaviour {
    public MultiBodyLinkCollider target;

	// Update is called once per frame
	void Update () {
        Matrix4x4 m = target.WorldTransform.ToUnity();
        transform.position = BSExtensionMethods.ExtractTranslationFromMatrix(ref m);
        transform.rotation = BSExtensionMethods.ExtractRotationFromMatrix(ref m);
        transform.localScale = BSExtensionMethods.ExtractScaleFromMatrix(ref m);
    }
}
