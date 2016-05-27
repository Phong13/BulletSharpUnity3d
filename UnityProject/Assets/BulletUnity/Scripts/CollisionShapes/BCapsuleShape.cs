using System;
using UnityEngine;
using System.Collections;
using BulletSharp;

namespace BulletUnity {

	[AddComponentMenu("Physics Bullet/Capsule Shape")]
    public class BCapsuleShape : BCollisionShape {
        public enum CapsuleAxis
        {
            x,
            y,
            z
        }

        public float radius = 1f;
        public float height = 2f;
        public CapsuleAxis upAxis = CapsuleAxis.y;

        public override void OnDrawGizmosSelected() {
            UnityEngine.Vector3 position = transform.position;
            UnityEngine.Quaternion rotation = transform.rotation;
            UnityEngine.Vector3 scale = Vector3.one;
            if (upAxis == CapsuleAxis.x)
            {
                rotation = Quaternion.AngleAxis(90, Vector3.forward) * rotation;
            } else if (upAxis == CapsuleAxis.z)
            {
                rotation = Quaternion.AngleAxis(90, Vector3.right) * rotation;
            }
            BUtility.DebugDrawCapsule(position, rotation, scale, radius, height / 2f, 1, Gizmos.color);

        }

        public override CollisionShape GetCollisionShape() {
            if (collisionShapePtr == null) {
                CapsuleShape cs = null;
                if (upAxis == CapsuleAxis.x)
                {
                    cs = new CapsuleShapeX(radius, height);
                } else if (upAxis == CapsuleAxis.y)
                {
                    cs = new CapsuleShape(radius, height);
                } else if (upAxis == CapsuleAxis.z)
                {
                    cs = new CapsuleShapeZ(radius, height);
                } else
                {
                    Debug.LogError("invalid axis value");
                }
                collisionShapePtr = cs;

            }
            return collisionShapePtr;
        }
    }
}
