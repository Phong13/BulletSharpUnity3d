using UnityEngine;
using System.Collections;
using BulletSharp;
using BulletSharp.Math;
using System;

public class BGameObjectMotionState : MotionState, IDisposable {

    public Transform transform;
    Matrix wt;

    public BGameObjectMotionState(Transform t) {
        transform = t;
    }
    
    //Bullet wants me to fill in worldTrans
    //This is called by bullet once when rigid body is added to the the world
    //For kinematic rigid bodies it is called every simulation step
    public override void GetWorldTransform(out Matrix worldTrans) {
        //Matrix4x4 trans = transform.localToWorldMatrix;
        //worldTrans = trans.ToBullet();
        
        BulletSharp.Math.Quaternion q = transform.rotation.ToBullet();
        BulletSharp.Math.Matrix.RotationQuaternion(ref q, out worldTrans);
        worldTrans.Origin = transform.position.ToBullet();
    }

    //Bullet calls this so I can copy bullet data to unity
    public override void SetWorldTransform(ref Matrix m) {
        BulletSharp.Math.Vector3 pos = m.Origin;
        UnityEngine.Quaternion q = new UnityEngine.Quaternion();
        q.w = Mathf.Sqrt(Mathf.Max(0, 1 + m[0, 0] + m[1, 1] + m[2, 2])) / 2;
        q.x = Mathf.Sqrt(Mathf.Max(0, 1 + m[0, 0] - m[1, 1] - m[2, 2])) / 2;
        q.y = Mathf.Sqrt(Mathf.Max(0, 1 - m[0, 0] + m[1, 1] - m[2, 2])) / 2;
        q.z = Mathf.Sqrt(Mathf.Max(0, 1 - m[0, 0] - m[1, 1] + m[2, 2])) / 2;
        q.x *= Mathf.Sign(q.x * (m[1, 2] - m[2, 1]));
        q.y *= Mathf.Sign(q.y * (m[2, 0] - m[0, 2]));
        q.z *= Mathf.Sign(q.z * (m[0, 1] - m[1, 2]));

        transform.position = pos.ToUnity();
        transform.rotation = q;
    }
}
