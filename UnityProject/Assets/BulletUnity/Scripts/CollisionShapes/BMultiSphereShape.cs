using System;
using UnityEngine;
using System.Collections;
using BulletSharp;

namespace BulletUnity {

	[AddComponentMenu("Physics Bullet/Shapes/Multi Sphere")]
    public class BMultiSphereShape : BCollisionShape {
        [Serializable]
        public struct Sphere {
            public Vector3 position;
            public float radius;
        }

        public Sphere[] spheres = new Sphere[0];

        public override void OnDrawGizmosSelected() {
            for (int i = 0; i < spheres.Length; i++) {
                BUtility.DebugDrawSphere(transform.TransformPoint(spheres[i].position), Quaternion.identity, Vector3.one, Vector3.one * spheres[i].radius, Gizmos.color);
            }
        }

        public override CollisionShape GetCollisionShape() {
            if (collisionShapePtr == null) {
                BulletSharp.Math.Vector3[] positions = new BulletSharp.Math.Vector3[spheres.Length];
                float[] radius = new float[spheres.Length];
                for (int i = 0; i < spheres.Length; i++) {
                    positions[i] = spheres[i].position.ToBullet();
                    radius[i] = spheres[i].radius;
                }
                collisionShapePtr = new MultiSphereShape(positions, radius);
            }
            return collisionShapePtr;
        }
    }
}
