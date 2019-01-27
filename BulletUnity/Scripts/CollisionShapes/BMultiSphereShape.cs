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

        [SerializeField]
        protected Vector3 m_localScaling = Vector3.one;
        public Vector3 LocalScaling
        {
            get { return m_localScaling; }
            set
            {
                m_localScaling = value;
                if (collisionShapePtr != null)
                {
                    ((MultiSphereShape)collisionShapePtr).LocalScaling = value.ToBullet();
                }
            }
        }

        public override void OnDrawGizmosSelected() {
            if (drawGizmo == false)
            {
                return;
            }
            for (int i = 0; i < spheres.Length; i++) {
                Vector3 v = spheres[i].position;
                v.x *= m_localScaling.x; v.y *= m_localScaling.y; v.z *= m_localScaling.z;
                BUtility.DebugDrawSphere(transform.TransformPoint(v), Quaternion.identity, Vector3.one, Vector3.one * spheres[i].radius, Gizmos.color);
            }
        }

        MultiSphereShape _CreateMultiSphereShape()
        {
            BulletSharp.Math.Vector3[] positions = new BulletSharp.Math.Vector3[spheres.Length];
            float[] radius = new float[spheres.Length];
            for (int i = 0; i < spheres.Length; i++)
            {
                positions[i] = spheres[i].position.ToBullet();
                radius[i] = spheres[i].radius;
            }
            MultiSphereShape mss = new MultiSphereShape(positions, radius);
            mss.LocalScaling = m_localScaling.ToBullet();
            return mss;
        }

        public override CollisionShape CopyCollisionShape()
        {
            return _CreateMultiSphereShape();
        }

        public override CollisionShape GetCollisionShape() {
            if (collisionShapePtr == null) {
                collisionShapePtr = _CreateMultiSphereShape();
            }
            return collisionShapePtr;
        }
    }
}
