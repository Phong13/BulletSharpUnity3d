using BulletSharp;
using UnityEngine;

namespace BulletUnity
{
    [AddComponentMenu("Physics Bullet/Shapes/Cylinder")]
    public class BCylinderShape : BCollisionShape
    {
        [SerializeField]
        protected Vector2 halfExtent = new Vector2(0.5f, 0.5f);
        public Vector2 HalfExtent
        {
            get { return halfExtent; }
            set
            {
                if (collisionShapePtr != null && value != halfExtent)
                {
                    Debug.LogError("Cannot change the extents after the bullet shape has been created. Extents is only the initial value " +
                                    "Use LocalScaling to change the shape of a bullet shape.");
                }
                else
                {
                    halfExtent = value;
                }
            }
        }

        [SerializeField]
        protected Vector2 m_localScaling = Vector2.one;
        public Vector2 LocalScaling
        {
            get { return m_localScaling; }
            set
            {
                m_localScaling = value;
                if (collisionShapePtr != null)
                {
                    ((CylinderShape)collisionShapePtr).LocalScaling = new Vector3(value.x, value.y, value.x).ToBullet();
                }
            }
        }

        public override void OnDrawGizmosSelected()
        {
            if (!drawGizmo)
            {
                return;
            }
            UnityEngine.Vector3 position = transform.position;
            UnityEngine.Quaternion rotation = transform.rotation;
            UnityEngine.Vector3 scale = new Vector3(m_localScaling.x, m_localScaling.y, m_localScaling.x);
            BUtility.DebugDrawCylinder(position, rotation, scale, halfExtent.x, HalfExtent.y, 1, Color.yellow);
        }

        public override CollisionShape CopyCollisionShape()
        {
            CylinderShape cs = new CylinderShape(new Vector3(HalfExtent.x, HalfExtent.y, HalfExtent.x).ToBullet());
            cs.LocalScaling = new Vector3(m_localScaling.x, m_localScaling.y, m_localScaling.x).ToBullet();
            return cs;
        }

        public override CollisionShape GetCollisionShape()
        {
            if (collisionShapePtr == null)
            {
                collisionShapePtr = new CylinderShape(new Vector3(HalfExtent.x, HalfExtent.y, HalfExtent.x).ToBullet());
                ((CylinderShape)collisionShapePtr).LocalScaling = new Vector3(m_localScaling.x, m_localScaling.y, m_localScaling.x).ToBullet();
            }
            return collisionShapePtr;
        }
    }
}