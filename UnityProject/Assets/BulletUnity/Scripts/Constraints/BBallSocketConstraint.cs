using System;
using UnityEngine;
using System.Collections;
using BulletSharp;
using BM = BulletSharp.Math;

namespace BulletUnity {
    [System.Serializable]
    public class BBallSocketConstraint : BTypedConstraint {


        //todo should be properties so can capture changes and propagate to scene

        [Header("Constraint Point Local To This Object")]
        public Vector3 m_localConstraintPoint = Vector3.zero;

        //called by Physics World just before constraint is added to world.
        //the current constraint properties are used to rebuild the constraint.
        internal override bool _BuildConstraint() {
            BPhysicsWorld world = BPhysicsWorld.Get();
            if (m_constraintPtr != null) {
                if (m_isInWorld && world != null) {
                    m_isInWorld = false;
                    world.RemoveConstraint(m_constraintPtr);
                }
            }
            BRigidBody targetRigidBodyA = GetComponent<BRigidBody>();
            if (targetRigidBodyA == null)
            {
                Debug.LogError("BallSocketConstraint needs to be added to a component with a BRigidBody.");
                return false;
            }
            if (!targetRigidBodyA.isInWorld)
            {
                world.AddRigidBody(targetRigidBodyA);
            }
            RigidBody rba = (RigidBody) targetRigidBodyA.GetCollisionObject();
            if (rba == null)
            {
                Debug.LogError("Constraint could not get bullet RigidBody from target rigid body A");
                return false;
            }
            if (m_constraintType == ConstraintType.constrainToAnotherBody)
            {
                if (m_otherRigidBody == null)
                {
                    Debug.LogError("Other rigid body was not set");
                    return false;
                }
                if (!m_otherRigidBody.isInWorld)
                {
                    world.AddRigidBody(m_otherRigidBody);
                }
                RigidBody rbb = (RigidBody) m_otherRigidBody.GetCollisionObject();
                if (rbb == null)
                {
                    Debug.LogError("Constraint could not get bullet RigidBody from target rigid body B");
                    return false;
                }
                Vector3 pivotInOther = m_otherRigidBody.transform.InverseTransformPoint(targetRigidBodyA.transform.TransformPoint(m_localConstraintPoint));
                m_constraintPtr = new Point2PointConstraint(rbb, rba, pivotInOther.ToBullet(), m_localConstraintPoint.ToBullet());
            } else
            {
                m_constraintPtr = new Point2PointConstraint(rba, m_localConstraintPoint.ToBullet());
            }
            m_constraintPtr.Userobject = this;
            return true;
        }
    }
}
