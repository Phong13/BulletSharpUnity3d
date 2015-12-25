using UnityEngine;
using System.Collections;
using BulletSharp;
using BulletSharp.Math;
using System;

namespace BulletUnity {
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
            /*
            BulletSharp.Math.Vector3 pos = m.Origin;
            UnityEngine.Quaternion q = new UnityEngine.Quaternion();
            q.w = Mathf.Sqrt(Mathf.Max(0, 1 + m[0, 0] + m[1, 1] + m[2, 2])) / 2;
            q.x = Mathf.Sqrt(Mathf.Max(0, 1 + m[0, 0] - m[1, 1] - m[2, 2])) / 2;
            q.y = Mathf.Sqrt(Mathf.Max(0, 1 - m[0, 0] + m[1, 1] - m[2, 2])) / 2;
            q.z = Mathf.Sqrt(Mathf.Max(0, 1 - m[0, 0] - m[1, 1] + m[2, 2])) / 2;
            q.x *= Mathf.Sign(q.x * (m[1, 2] - m[2, 1]));
            q.y *= Mathf.Sign(q.y * (m[2, 0] - m[0, 2]));
            q.z *= Mathf.Sign(q.z * (m[0, 1] - m[1, 2]));
            */

            //todo not very efficient
            /*
            Matrix4x4 mu = m.ToUnity();
            UnityEngine.Vector3 p = BSExtensionMethods.ExtractTranslationFromMatrix(ref mu);
            UnityEngine.Quaternion q = BSExtensionMethods.ExtractRotationFromMatrix(ref mu);
            UnityEngine.Vector3 sc = BSExtensionMethods.ExtractScaleFromMatrix(ref mu);

            UnityEngine.Vector3 p1 = BSExtensionMethods.ExtractTranslationFromMatrix(ref m);
            UnityEngine.Quaternion q1 = BSExtensionMethods.ExtractRotationFromMatrix(ref m);
            UnityEngine.Vector3 sc1 = BSExtensionMethods.ExtractScaleFromMatrix(ref m);

            if (p != p1) Debug.Log("Dont match p " + p + " " + p1);
            if (q != q1) Debug.Log("Dont match q " + q + " " + q1);
            if (sc != sc1) Debug.Log("Dont match p " + sc + " " + sc1);
            */

            transform.position = BSExtensionMethods2.ExtractTranslationFromMatrix(ref m);
            transform.rotation = BSExtensionMethods2.ExtractRotationFromMatrix(ref m);
            transform.localScale = BSExtensionMethods2.ExtractScaleFromMatrix(ref m);
        }
    }
}
