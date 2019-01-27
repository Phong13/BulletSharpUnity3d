using System;
using UnityEngine;
using System.Collections;
using BulletSharp;

namespace BulletUnity {
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
    public class BCompoundShape : BCollisionShape {
        [SerializeField]
        protected BCollisionShape[] colliders;

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
                    ((CompoundShape)collisionShapePtr).LocalScaling = value.ToBullet();
                }
            }
        }

        //TODO the gizmos do not draw correctly when collision shape is scaled
        public override void OnDrawGizmosSelected() {
            if (drawGizmo == false)
            {
                return;
            }
            if (colliders != null) {
                for (int i = 0; i < colliders.Length; i++) {
                    if (colliders[i] != null)
                    {
                        colliders[i].OnDrawGizmosSelected();
                    }
                }
            }
        }

        CompoundShape _CreateCompoundShape(bool copyChildren)
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
                else {
                    colliders[ii] = css[i];
                    ii++;
                }
            }
            if (colliders.Length == 0)
            {
                Debug.LogError("Compound collider");
            }

            //TODO
            // some of the collider types (non-finite and other compound colliders) are probably not
            // can only be added to game object with rigid body attached.
            // allowed should check for these.
            // what about scaling not sure if it is handled correctly
            CompoundShape cs = new CompoundShape();
            for (int i = 0; i < colliders.Length; i++)
            {
                CollisionShape chcs;
                if (copyChildren == true)
                {
                    chcs = colliders[i].CopyCollisionShape();
                }
                else {
                    chcs = colliders[i].GetCollisionShape();
                }

                Vector3 up = Vector3.up;
                Vector3 origin = Vector3.zero;
                Vector3 forward = Vector3.forward;
                //to world
                up = colliders[i].transform.TransformDirection(up);
                origin = colliders[i].transform.TransformPoint(origin);
                forward = colliders[i].transform.TransformDirection(forward);
                //to compound collider
                up = transform.InverseTransformDirection(up);
                origin = transform.InverseTransformPoint(origin);
                forward = transform.InverseTransformDirection(forward);
                Quaternion q = Quaternion.LookRotation(forward, up);

                /*
                Some collision shapes can have local scaling applied. Use
                btCollisionShape::setScaling(vector3).Non uniform scaling with different scaling
                values for each axis, can be used for btBoxShape, btMultiSphereShape,
                btConvexShape, btTriangleMeshShape.Note that a non - uniform scaled
                sphere can be created by using a btMultiSphereShape with 1 sphere.
                */

                BulletSharp.Math.Matrix m = BulletSharp.Math.Matrix.AffineTransformation(1f, q.ToBullet(), origin.ToBullet());

                cs.AddChildShape(m, chcs);
            }
            cs.LocalScaling = m_localScaling.ToBullet();
            return cs;
        }

        public override CollisionShape CopyCollisionShape()
        {
            return _CreateCompoundShape(true);
        }

        public override CollisionShape GetCollisionShape() {
            if (collisionShapePtr == null) {
                collisionShapePtr = _CreateCompoundShape(false);
            }
            return collisionShapePtr;
        }
    }
}
