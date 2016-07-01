using UnityEngine;
using System.Collections;
using BulletSharp;
using BM = BulletSharp.Math;
using System;
using System.Runtime.InteropServices;
using AOT;

namespace BulletUnity {
    public class BGameObjectMotionState : MotionState, IDisposable {

        public Transform transform;
        BM.Matrix wt;

        public BGameObjectMotionState(Transform t) {
            transform = t;
        }

		public delegate void GetTransformDelegate(out BM.Matrix worldTrans);
		public delegate void SetTransformDelegate(ref BM.Matrix m);

        //Bullet wants me to fill in worldTrans
        //This is called by bullet once when rigid body is added to the the world
        //For kinematic rigid bodies it is called every simulation step
		//[MonoPInvokeCallback(typeof(GetTransformDelegate))]
        public override void GetWorldTransform(out BM.Matrix worldTrans) {
            BulletSharp.Math.Vector3 pos = transform.position.ToBullet();
            BulletSharp.Math.Quaternion rot = transform.rotation.ToBullet();
            BulletSharp.Math.Matrix.AffineTransformation(1f, ref rot, ref pos, out worldTrans);
        }

        //Bullet calls this so I can copy bullet data to unity
        public override void SetWorldTransform(ref BM.Matrix m) {
            transform.position = BSExtensionMethods2.ExtractTranslationFromMatrix(ref m);
            transform.rotation = BSExtensionMethods2.ExtractRotationFromMatrix(ref m);
        }
    }
}
