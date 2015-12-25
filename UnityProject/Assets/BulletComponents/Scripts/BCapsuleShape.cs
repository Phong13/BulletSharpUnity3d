using System;
using UnityEngine;
using System.Collections;
using BulletSharp;
namespace BulletUnity {
    public class BCapsuleShape : BCollisionShape {
        public float radius = 1f;
        public float height = 2f;

        public override void OnDrawGizmosSelected() {
            UnityEngine.Vector3 position = transform.position;
            UnityEngine.Quaternion rotation = transform.rotation;
            UnityEngine.Vector3 scale = Vector3.one;
            BUtility.DebugDrawCapsule(position, rotation, scale, radius, height / 2f, 1, Gizmos.color);

        }

        public override CollisionShape GetCollisionShape() {
            if (collisionShapePtr == null) {
                collisionShapePtr = new CapsuleShape(radius, height);
            }
            return collisionShapePtr;
        }
    }
}
