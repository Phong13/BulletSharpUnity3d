using UnityEngine;
using System;
using BulletSharp;

namespace BulletUnity {
    [System.Serializable]
    public abstract class BCollisionShape : MonoBehaviour, IDisposable {
        public enum CollisionShapeType {
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

        void OnDestroy() {
            Dispose(false);
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isdisposing) {
            if (collisionShapePtr != null) {
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
    }
}


