using BulletSharp;
using System.Linq;
using UnityEngine;

namespace BulletUnity
{
    /*
    Doesn't check for changes in the transforms on the child objects or the transforms between 
    the child colliders and the compound collider. If the child colliders change then 
    updateChildTransform must explicitly be called 


    todo should handle
        - scaling of itself and children even just to warn
        - children being removed, destroyed
        - children being moved to different possibly invalid locations in hierarchy 
    */
    [AddComponentMenu("Physics Bullet/Shapes/Compund")]
    public class BCompoundShape : BCollisionShape
    {

        // TODO We should store the transform matrix to apply between the subshape and the mainshape
        // because there are different cases.
        // With shapes from unity colliders, we need to invert the local scale of the object as the collider takes it into account already
        // With shapes from bullet, we must not invert the local scale
        public struct CollisionShapeWithTransform
        {
            public CollisionShape Shape;
            public Transform Transform;

            public CollisionShapeWithTransform(CollisionShape shape, Transform transform)
            {
                Shape = shape;
                Transform = transform;
            }
        }

        [HideInInspector]
        [SerializeField]
        protected BCollisionShape[] colliders;

        //TODO the gizmos do not draw correctly when collision shape is scaled
        public override void OnDrawGizmosSelected()
        {
            if (!drawGizmo)
            {
                return;
            }
            if (colliders != null)
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i] != null)
                    {
                        colliders[i].OnDrawGizmosSelected();
                    }
                }
            }
        }

        protected virtual CompoundShape _CreateCompoundShape(bool copyChildren)
        {


            //TODO
            // some of the collider types (non-finite and other compound colliders) are probably not
            // can only be added to game object with rigid body attached.
            // allowed should check for these.
            // what about scaling not sure if it is handled correctly
            CompoundShape cs = new CompoundShape();
            CollisionShapeWithTransform[] collisionShapes = GetSubCollisionShapes(copyChildren);
            for (int i = 0; i < collisionShapes.Length; i++)
            {
                CollisionShape chcs = collisionShapes[i].Shape;
                // we need to invert the scale
                BulletSharp.Math.Matrix m = (this.transform.worldToLocalMatrix * collisionShapes[i].Transform.localToWorldMatrix * Matrix4x4.Scale(collisionShapes[i].Transform.lossyScale).inverse).ToBullet();
                cs.AddChildShape(m, chcs);
            }
            cs.LocalScaling = m_localScaling.ToBullet();
            cs.Margin = m_Margin;
            return cs;
        }

        protected virtual CollisionShapeWithTransform[] GetSubCollisionShapes(bool copyChildren)
        {
            BCollisionShape[] css = GetComponentsInChildren<BCollisionShape>();
            colliders = new BCollisionShape[css.Length - 1];
            int ii = 0;
            for (int i = 0; i < css.Length; i++)
            {
                if (css[i] == this)
                {
                    //skip
                }
                else
                {
                    colliders[ii] = css[i];
                    ii++;
                }
            }
            if (colliders.Length == 0)
            {
                Debug.LogError("Compound collider");
            }
            if (copyChildren)
                return colliders.Select(cs => new CollisionShapeWithTransform(cs.CopyCollisionShape(), cs.transform)).ToArray();
            else
                return colliders.Select(cs => new CollisionShapeWithTransform(cs.GetCollisionShape(), cs.transform)).ToArray();
        }

        public override CollisionShape CopyCollisionShape()
        {
            return _CreateCompoundShape(true);
        }

        public override CollisionShape GetCollisionShape()
        {
            if (collisionShapePtr == null)
            {
                collisionShapePtr = _CreateCompoundShape(false);
            }
            return collisionShapePtr;
        }
    }
}
