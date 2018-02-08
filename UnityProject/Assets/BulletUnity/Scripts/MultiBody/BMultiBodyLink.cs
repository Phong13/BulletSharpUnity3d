using UnityEngine;
using System.Collections;
using BulletSharp;

namespace BulletUnity
{
    public class BMultiBodyLink : BCollisionObject
    {
        public float mass = 1;
        public FeatherstoneJointType jointType = FeatherstoneJointType.Spherical;
        public Vector3 localPivotPosition;
        public Vector3 rotationAxis;
        public float jointDamping = 0f;
        public float jointFriction = 0f;

        [Range(.05f,10f)]
        public float gizmoScale = .15f;
        internal MultiBodyLinkCollider m_linkCollider;

        [System.NonSerialized]
        internal int parentIndex;
        [System.NonSerialized]
        internal int index;

        public MultiBodyLinkCollider GetLinkCollider()
        {
            return m_linkCollider;
        }

        internal override void Start()
        {
            //override baseclass because we don't want this collision object auto added to world.
            //the multibody will add it.
            m_startHasBeenCalled = true;
        }

        private void Update()
        {
            if (m_linkCollider != null && isInWorld)
            {
                Matrix4x4 m = m_linkCollider.WorldTransform.ToUnity();
                transform.position = BSExtensionMethods2.ExtractTranslationFromMatrix(ref m);
                transform.rotation = BSExtensionMethods2.ExtractRotationFromMatrix(ref m);
            }
        }

        protected override void OnDisable()
        {
            if (m_linkCollider != null && isInWorld)
            {
                //all constraints using RB must be disabled before rigid body is disabled
                /*
                for (int i = m_rigidBody.NumConstraintRefs - 1; i >= 0; i--)
                {
                    BTypedConstraint btc = (BTypedConstraint)m_rigidBody.GetConstraintRef(i).Userobject;
                    Debug.Assert(btc != null);
                    btc.enabled = false; //should remove it from the scene
                }
                */
            }
            base.OnDisable();
        }

        protected override void Dispose(bool isdisposing)
        {
            if (isInWorld && isdisposing && m_linkCollider != null)
            {
                BPhysicsWorld pw = BPhysicsWorld.Get();
                if (pw != null && pw.world != null)
                {
                    //constraints must be removed before rigid body is removed
                    /*
                    for (int i = m_linkCollider.NumConstraintRefs; i > 0; i--)
                    {
                        BTypedConstraint tc = (BTypedConstraint)m_rigidBody.GetConstraintRef(i - 1).Userobject;
                        ((DiscreteDynamicsWorld)pw.world).RemoveConstraint(tc.GetConstraint());
                    }
                    */
                    
                    ((DiscreteDynamicsWorld)pw.world).RemoveCollisionObject(m_linkCollider);
                }
            }
            if (m_linkCollider != null)
            {
                m_linkCollider.Dispose();
                m_linkCollider = null;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Vector3 rperp = Vector3.up;
            Vector3 forward = rotationAxis;
            if (jointType == FeatherstoneJointType.Planar ||
                jointType == FeatherstoneJointType.Prismatic ||
                jointType == FeatherstoneJointType.Revolute)
            {
                //uses axis of rotation
                if (rotationAxis != Vector3.zero)
                {
                    BUtility.GetPerpendicularVector(forward, out rperp);
                    rperp.Normalize();
                }
            } else
            {
                rotationAxis = transform.forward;
                rperp = transform.up;
            }
            BUtility.DebugDrawTransform(transform, localPivotPosition, rotationAxis,rperp, gizmoScale);
        }
    }
}
