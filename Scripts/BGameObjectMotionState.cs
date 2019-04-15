using BulletSharp;
using BulletSharp.Math;
using System;
using UnityEngine;
using BM = BulletSharp.Math;

namespace BulletUnity
{
    public class BGameObjectMotionState : MotionState, IDisposable
    {

        public Transform transform;

        Matrix lastBulletTransform;
        bool mustUpdateTransform = false;

        BulletSharp.Math.Vector3 pos;
        BulletSharp.Math.Quaternion rot;


        public BGameObjectMotionState(Transform t)
        {
            transform = t;
            pos = transform.position.ToBullet();
            rot = transform.rotation.ToBullet();
        }

        public delegate void GetTransformDelegate(out BM.Matrix worldTrans);
        public delegate void SetTransformDelegate(ref BM.Matrix m);

        //Bullet wants me to fill in worldTrans
        //This is called by bullet once when rigid body is added to the the world
        //For kinematic rigid bodies it is called every simulation step
        //[MonoPInvokeCallback(typeof(GetTransformDelegate))]
        public override void GetWorldTransform(out BM.Matrix worldTrans)
        {
            BulletSharp.Math.Matrix.AffineTransformation(1f, ref rot, ref pos, out worldTrans);
        }

        //Bullet calls this so I can copy bullet data to unity
        public override void SetWorldTransform(ref BM.Matrix m)
        {
            lock (transform)
            {
                lastBulletTransform = m;
                mustUpdateTransform = true;
            }
        }

        // Update is called once per frame
        public void Update()
        {
            lock (transform)
            {
                if (mustUpdateTransform)
                {
                    transform.position = BSExtensionMethods2.ExtractTranslationFromMatrix(ref lastBulletTransform);
                    transform.rotation = BSExtensionMethods2.ExtractRotationFromMatrix(ref lastBulletTransform);
                    mustUpdateTransform = false;
                }
                pos = transform.position.ToBullet();
                rot = transform.rotation.ToBullet();
            }
        }
    }
}
