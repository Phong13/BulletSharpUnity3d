using System;
using UnityEngine;
using System.Collections;
using BulletSharp;

namespace BulletUnity {
    public class BBoxShape : BCollisionShape {
        public Vector3 extents = Vector3.one;

        public override void OnDrawGizmosSelected() {
            UnityEngine.Vector3 position = transform.position;
            UnityEngine.Quaternion rotation = transform.rotation;
            UnityEngine.Vector3 scale = Vector3.one;
            BUtility.DebugDrawBox(position, rotation, scale, extents, Color.yellow);
        }

        public override CollisionShape GetCollisionShape() {
            if (collisionShapePtr == null) {
                collisionShapePtr = new BoxShape(extents.ToBullet());
            }
            return collisionShapePtr;
        }
    }
}
