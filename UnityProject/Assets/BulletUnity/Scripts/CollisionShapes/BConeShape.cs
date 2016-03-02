using System;
using UnityEngine;
using System.Collections;
using BulletSharp;

namespace BulletUnity
{
	[AddComponentMenu("Physics Bullet/Cone Shape")]
    public class BConeShape : BCollisionShape
    {
        //public Vector3 halfExtent = new Vector3(0.5f, 0.5f, 0.5f);
        public float radius = 0.5f;
        public float height = 1.0f;

        public override void OnDrawGizmosSelected()
        {
            UnityEngine.Vector3 position = transform.position;
            UnityEngine.Quaternion rotation = transform.rotation;
            UnityEngine.Vector3 scale = Vector3.one;
            BUtility.DebugDrawCone(position, rotation, scale, radius, height, 1, Color.yellow);
        }

        public override CollisionShape GetCollisionShape()
        {
            if (collisionShapePtr == null)
            {
                collisionShapePtr = new ConeShape(radius, height);
            }
            return collisionShapePtr;
        }
    }
}