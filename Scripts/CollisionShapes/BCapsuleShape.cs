using BulletSharp;
using UnityEngine;

namespace BulletUnity
{

    [AddComponentMenu("Physics Bullet/Shapes/Capsule")]
    public class BCapsuleShape : BCollisionShape
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
                else
                {
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
                else
                {
                    height = value;
                }
            }
        }

        [SerializeField]
        protected Axis upAxis = Axis.y;
        public Axis UpAxis
        {
            get { return upAxis; }
            set
            {
                if (collisionShapePtr != null && value != upAxis)
                {
                    Debug.LogError("Cannot change the upAxis after the bullet shape has been created. upAxis is only the initial value " +
                                    "Use LocalScaling to change the shape of a bullet shape.");
                }
                else
                {
                    upAxis = value;
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
            if (upAxis == Axis.x)
            {
                rotation = Quaternion.AngleAxis(90, transform.forward) * rotation;
            }
            else if (upAxis == Axis.z)
            {
                rotation = Quaternion.AngleAxis(90, transform.right) * rotation;
            }
            BUtility.DebugDrawCapsule(position, rotation, LocalScaling, radius, height / 2f, 1, Gizmos.color);

        }

        CapsuleShape _CreateCapsuleShape()
        {
            CapsuleShape cs = null;
            if (upAxis == Axis.x)
            {
                cs = new CapsuleShapeX(radius, height);
            }
            else if (upAxis == Axis.y)
            {
                cs = new CapsuleShape(radius, height);
            }
            else if (upAxis == Axis.z)
            {
                cs = new CapsuleShapeZ(radius, height);
            }
            else
            {
                Debug.LogError("invalid axis value");
            }
            cs.LocalScaling = m_localScaling.ToBullet();
            cs.Margin = m_Margin;
            return cs;
        }

        public override CollisionShape CopyCollisionShape()
        {
            return _CreateCapsuleShape();
        }

        public override CollisionShape GetCollisionShape()
        {
            if (collisionShapePtr == null)
            {
                collisionShapePtr = _CreateCapsuleShape();
            }
            return collisionShapePtr;
        }
    }
}
