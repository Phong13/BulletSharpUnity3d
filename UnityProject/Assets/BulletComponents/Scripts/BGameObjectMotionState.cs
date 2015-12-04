using UnityEngine;
using System.Collections;
using BulletSharp;
using BulletSharp.Math;
using System;

public class BGameObjectMotionState : MotionState {

    public Transform transform;

    public BGameObjectMotionState(Transform t) {
        transform = t;
    }
    
    //Bullet wants me to fill in worldTrans from Unity
    public override void GetWorldTransform(out Matrix worldTrans) {
        Matrix4x4 m = transform.localToWorldMatrix;
        worldTrans = m.ToBullet();
    }

    //Bullet calls this so I can copy bullet data to unity
    public override void SetWorldTransform(ref Matrix worldTrans) {
        Matrix4x4 m = worldTrans.ToUnity();
        transform.position = BSExtensionMethods.ExtractTranslationFromMatrix(ref m);
        transform.rotation = BSExtensionMethods.ExtractRotationFromMatrix(ref m);
        transform.localScale = BSExtensionMethods.ExtractScaleFromMatrix(ref m);
    }
}
