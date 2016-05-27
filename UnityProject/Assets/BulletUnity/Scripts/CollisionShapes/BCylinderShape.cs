using System;
using UnityEngine;
using System.Collections;
using BulletSharp;

namespace BulletUnity
{
    [AddComponentMenu("Physics Bullet/Shapes/Cylinder")]
    public class BCylinderShape : BCollisionShape
    {
        public Vector3 halfExtent = new Vector3(0.5f, 0.5f, 0.5f);

        public override void OnDrawGizmosSelected()
        {
            UnityEngine.Vector3 position = transform.position;
            UnityEngine.Quaternion rotation = transform.rotation;
            UnityEngine.Vector3 scale = Vector3.one;
            BUtility.DebugDrawCylinder(position, rotation, scale, halfExtent.x, halfExtent.y, 1, Color.yellow);
        }
        
        public override CollisionShape GetCollisionShape()
        {
            if (collisionShapePtr == null)
            {
                collisionShapePtr = new CylinderShape(halfExtent.ToBullet());
            }
            return collisionShapePtr;
        }
    }
}