using System;
using UnityEngine;
using System.Collections;
using BulletSharp;

namespace BulletUnity
{
	[AddComponentMenu("Physics Bullet/Shapes/Cone")]
    public class BConeShape : BCollisionShape
    {
        [SerializeField]
        protected float radius = 1f;
        public float Radius
        {
            get { return radius; }
            set
            {
                if (collisionShapePtr != null && value != radius)
                {
                    Debug.LogError("Cannot change the radius after the bullet shape has been created. Radius is only the initial value " +
                                    "Use LocalScaling to change the shape of a bullet shape.");
                }
                else {
                    radius = value;
                }
            }
        }

        [SerializeField]
        protected float height = 2f;
        public float Height
        {
            get { return height; }
            set
            {
                if (collisionShapePtr != null && value != height)
                {
                    Debug.LogError("Cannot change the height after the bullet shape has been created. Height is only the initial value " +
                                    "Use LocalScaling to change the shape of a bullet shape.");
                }
                else {
                    height = value;
                }
            }
        }

        public override void OnDrawGizmosSelected()
        {
            if (drawGizmo == false)
            {
                return;
            }
            UnityEngine.Vector3 position = transform.position;
            UnityEngine.Quaternion rotation = transform.rotation;
            BUtility.DebugDrawCone(position, rotation, LocalScaling, radius, height, 1, Color.yellow);
        }

        public override CollisionShape CopyCollisionShape()
        {
            ConeShape cs = new ConeShape(radius, height);
            cs.LocalScaling = m_localScaling.ToBullet();
            return cs;
        }

        public override CollisionShape GetCollisionShape()
        {
            if (collisionShapePtr == null)
            {
                collisionShapePtr = new ConeShape(radius, height);
                ((ConeShape)collisionShapePtr).LocalScaling = m_localScaling.ToBullet();
            }
            return collisionShapePtr;
        }
    }
}