using BulletSharp;
using BulletSharp.Math;
using System;
using UnityEngine;
using BM = BulletSharp.Math;

namespace BulletUnity
{
    public class BGameObjectMotionState : MotionState, IDisposable
    {
        class BulletTransformData
        {
            public Matrix Transform;
            public double TimeStamp;

            public BulletTransformData(Matrix transform, double time = -1)
            {
                Transform = transform;
                TimeStamp = time;
            }
        }

        protected Transform transform;

        BulletTransformData lastBulletTransform;
        BulletTransformData previousBulletTransform;

        bool mustUpdateTransform = false;

        BulletSharp.Math.Vector3 pos;
        BulletSharp.Math.Quaternion rot;

        BTThreadedWorldHelper threadHelper;


        private object lck = new object();

        public BGameObjectMotionState(Transform transform)
        {
            this.transform = transform;
            pos = transform.position.ToBullet();
            rot = transform.rotation.ToBullet();
            threadHelper = BPhysicsWorld.Get().PhysicsWorldHelper as BTThreadedWorldHelper;
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
            double timeStamp = -1;
            if (threadHelper != null)
                timeStamp = threadHelper.TotalSimulationTime;
            lock (lck)
            {
                previousBulletTransform = lastBulletTransform;
                lastBulletTransform = new BulletTransformData(m, timeStamp);
                mustUpdateTransform = true;
            }
        }

        // Update is called once per frame
        public void Update()
        {
            lock (lck)
            {
                if (mustUpdateTransform)
                {
                    UnityEngine.Vector3 position = BSExtensionMethods2.ExtractTranslationFromMatrix(ref lastBulletTransform.Transform);
                    UnityEngine.Quaternion rotation = BSExtensionMethods2.ExtractRotationFromMatrix(ref lastBulletTransform.Transform);
                    // Interpolation is needed in threaded mode
                    // Maybe we can have a call to the physics engine in an update method instead ?
                    if (threadHelper != null && previousBulletTransform != null)
                    {
                        double currentTime = threadHelper.TotalSimulationTime;
                        UnityEngine.Vector3 previousPosition = BSExtensionMethods2.ExtractTranslationFromMatrix(ref previousBulletTransform.Transform);
                        UnityEngine.Quaternion previousRotation = BSExtensionMethods2.ExtractRotationFromMatrix(ref previousBulletTransform.Transform);

                        double interpolationFactor = (currentTime - lastBulletTransform.TimeStamp) / (lastBulletTransform.TimeStamp - previousBulletTransform.TimeStamp);

                        transform.position = UnityEngine.Vector3.LerpUnclamped(previousPosition, position, (float)interpolationFactor);
                        transform.rotation = UnityEngine.Quaternion.SlerpUnclamped(previousRotation, rotation, (float)interpolationFactor);
                    }
                    else
                    {
                        transform.position = position;
                        transform.rotation = rotation;
                    }
                    mustUpdateTransform = false;
                }
                pos = transform.position.ToBullet();
                rot = transform.rotation.ToBullet();
            }
        }

    }
}
