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

        public override void OnDrawGizmosSelected()
        {
            if (!drawGizmo)
            {
                return;
            }
            UnityEngine.Vector3 position = transform.position;
            UnityEngine.Quaternion rotation = transform.rotation;
            BUtility.DebugDrawCylinder(position, rotation, LocalScaling, halfExtent.x, halfExtent.y, 1, Color.yellow);
        }

        public override CollisionShape CopyCollisionShape()
        {
            CylinderShape cs = new CylinderShape(new Vector3(HalfExtent.x, HalfExtent.y, HalfExtent.x).ToBullet());
            cs.LocalScaling = m_localScaling.ToBullet();
            return cs;
        }

        public override CollisionShape GetCollisionShape()
        {
            if (collisionShapePtr == null)
            {
                collisionShapePtr = new CylinderShape(new Vector3(HalfExtent.x, HalfExtent.y, HalfExtent.x).ToBullet());
                ((CylinderShape)collisionShapePtr).LocalScaling = m_localScaling.ToBullet();
            }
            return collisionShapePtr;
        }
    }
}