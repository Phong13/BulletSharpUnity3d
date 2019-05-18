using BulletSharp;
using System;
using UnityEngine;

namespace BulletUnity
{
    [System.Serializable]
    public abstract class BCollisionShape : MonoBehaviour, IDisposable
    {
        public enum CollisionShapeType
        {
            // dynamic
            BoxShape = 0,
            SphereShape = 1,
            CapsuleShape = 2,
            CylinderShape = 3,
            ConeShape = 4,
            ConvexHull = 5,
            CompoundShape = 6,

            // static
            BvhTriangleMeshShape = 7,
            StaticPlaneShape = 8,
        };

        protected CollisionShape collisionShapePtr = null;
        public bool drawGizmo = true;

        void OnDestroy()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isdisposing)
        {
            if (collisionShapePtr != null)
            {
                collisionShapePtr.Dispose();
                collisionShapePtr = null;
            }
        }

        public abstract void OnDrawGizmosSelected();

        public abstract CollisionShape CopyCollisionShape();

        public abstract CollisionShape GetCollisionShape();

        [SerializeField]
        protected Vector3 m_localScaling = Vector3.one;
        public Vector3 LocalScaling
        {
            get
            {
                if (collisionShapePtr != null)
                {
                    return collisionShapePtr.LocalScaling.ToUnity();
                }
                else
                {
                    return m_localScaling;
                }
            }
            set
            {
                m_localScaling = value;
                if (collisionShapePtr != null)
                {
                    collisionShapePtr.LocalScaling = value.ToBullet();
                }
            }
        }

        [SerializeField]
        protected float m_Margin = 0.04f;
        public float Margin
        {
            get
            {
                if (collisionShapePtr != null)
                {
                    return collisionShapePtr.Margin;
                }
                else
                {
                    return m_Margin;
                }
            }
            set
            {
                m_Margin = value;
                if (collisionShapePtr != null)
                {
                    collisionShapePtr.Margin = value;
                }
            }
        }
    }
}


