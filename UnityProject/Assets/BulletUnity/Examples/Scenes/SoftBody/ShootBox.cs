using UnityEngine;
using System.Collections;
using BulletSharp.SoftBody;
using System;
//using BulletSharp;
using System.Collections.Generic;
using BulletUnity.Primitives;

namespace BulletUnity
{
    class ShootBox : MonoBehaviour
    {

        [Range(0.5f, 10f)]
        public float maxShotsPerSecond = 3.0f;
        [Range(0.5f, 1000f)]
        public float shootBoxInitialSpeed = 10f;

        public BSphereMeshSettings meshSettings = new BSphereMeshSettings();

        public float mass = 10f;
        public float lifeTime = 120f;

        float lastShotTime = 0f;


        void Update()
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if ((Time.time - lastShotTime) < (1 / maxShotsPerSecond))
                    return;

                Vector3 camPos = Camera.main.transform.position;
                Quaternion camRot = Camera.main.transform.rotation;

                GameObject go = BSphere.CreateNew(camPos + new Vector3(0, 0, 2), camRot);

                BSphere bs = go.GetComponent<BSphere>();
                bs.meshSettings = meshSettings;
                bs.BuildMesh();

                lastShotTime = Time.time;

                //linVel.Normalize();
                BRigidBody bRb = go.GetComponent<BRigidBody>();
                bRb.mass = mass;
            
                BulletSharp.RigidBody rb = (BulletSharp.RigidBody) bRb.GetCollisionObject();

                rb.LinearVelocity = (Camera.main.transform.forward * shootBoxInitialSpeed).ToBullet();
                rb.AngularVelocity = BulletSharp.Math.Vector3.Zero;
                rb.ContactProcessingThreshold = 1e30f;

                go.GetComponent<MeshRenderer>().material.color =
                  new Color(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f));

                Destroy(go, lifeTime);
            }

        }


    }
}
