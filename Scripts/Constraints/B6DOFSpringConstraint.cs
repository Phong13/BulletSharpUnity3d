using BulletSharp;
using UnityEngine;
using BM = BulletSharp.Math;

namespace BulletUnity
{
    [AddComponentMenu("Physics Bullet/Constraints/6 Degree Of Freedom Spring")]
    public class B6DOFSpringConstraint : BTypedConstraint
    {
        [Header("Spring")]
        [SerializeField]
        protected Axis m_springAxis;
        public Axis SpringAxis
        {
            get { return m_springAxis; }
        }

        [SerializeField]
        protected float m_stiffness = 10f;
        public float Stiffness
        {
            get { return m_stiffness; }
            set
            {
                m_stiffness = value;
                if (m_constraintPtr != null)
                {
                    ((Generic6DofSpringConstraint)m_constraintPtr).SetStiffness((int)m_springAxis, m_stiffness);
                }
            }
        }

        [SerializeField]
        protected float m_damping = 0.5f;
        public float Damping
        {
            get { return m_damping; }
            set
            {
                m_damping = value;
                if (m_constraintPtr != null)
                {
                    ((Generic6DofSpringConstraint)m_constraintPtr).SetDamping((int)m_springAxis, m_damping);
                }
            }
        }


        [Header("Limits")]
        [SerializeField]
        protected bool m_setLimit;
        public bool SetLimit
        {
            get { return m_setLimit; }
            set { m_setLimit = value; }
        }


        [SerializeField]
        protected float m_lowLimit;
        public float LowLimit
        {
            get { return m_lowLimit; }
            set
            {
                m_lowLimit = value;
                if (m_constraintPtr != null)
                {
                    ((Generic6DofSpringConstraint)m_constraintPtr).SetLimit((int)m_springAxis, m_lowLimit, m_highLimit);
                }
            }
        }

        [SerializeField]
        protected float m_highLimit;
        public float HighLimit
        {
            get { return m_highLimit; }
            set
            {
                m_highLimit = value;
                if (m_constraintPtr != null)
                {
                    ((Generic6DofSpringConstraint)m_constraintPtr).SetLimit((int)m_springAxis, m_lowLimit, m_highLimit);
                }
            }
        }

        internal override bool _BuildConstraint()
        {
            BPhysicsWorld world = BPhysicsWorld.Get();
            if (m_constraintPtr != null)
            {
                if (m_isInWorld && world != null)
                {
                    m_isInWorld = false;
                    world.RemoveConstraint(m_constraintPtr);
                }
            }
            BRigidBody targetRigidBodyA = GetComponent<BRigidBody>();
            if (targetRigidBodyA == null)
            {
                Debug.LogError("BGeneric6DofSpringConstraint needs to be added to a component with a BRigidBody.");
                return false;
            }
            if (!targetRigidBodyA.isInWorld)
            {
                world.AddRigidBody(targetRigidBodyA);
            }
            if (m_localConstraintAxisX == Vector3.zero)
            {
                Debug.LogError("Constaint axis cannot be zero vector");
                return false;
            }
            RigidBody rba = (RigidBody)targetRigidBodyA.GetCollisionObject();
            if (rba == null)
            {
                Debug.LogError("Constraint could not get bullet RigidBody from target rigid body");
                return false;
            }
            if (m_constraintType == ConstraintType.constrainToAnotherBody)
            {
                if (m_otherRigidBody == null)
                {
                    Debug.LogError("Other rigid body must not be null");
                    return false;
                }
                if (!m_otherRigidBody.isInWorld)
                {
                    world.AddRigidBody(m_otherRigidBody);
                }
                RigidBody rbb = (RigidBody)m_otherRigidBody.GetCollisionObject();
                if (rbb == null)
                {
                    Debug.LogError("Constraint could not get bullet RigidBody from target rigid body");
                    return false;
                }
                BM.Matrix frameInA, frameInOther;
                string errormsg = "";
                if (CreateFramesA_B(m_localConstraintAxisX, m_localConstraintAxisY, m_localConstraintPoint, out frameInA, out frameInOther, ref errormsg))
                {
                    m_constraintPtr = new Generic6DofSpringConstraint(rba, rbb, frameInA, frameInOther, true);
                }
                else
                {
                    Debug.LogError(errormsg);
                    return false;
                }
            }
            else
            {
                // TODO
                //   m_constraintPtr = new Generic6DofSpringConstraint(rba, m_localConstraintPoint.ToBullet(), m_localConstraintAxisX.ToBullet(), false);
            }
            if (m_setLimit)
            {
                ((Generic6DofSpringConstraint)m_constraintPtr).SetLimit((int)m_springAxis, m_lowLimit, m_highLimit);
            }
            ((Generic6DofSpringConstraint)m_constraintPtr).EnableSpring((int)m_springAxis, true);
            ((Generic6DofSpringConstraint)m_constraintPtr).SetStiffness((int)m_springAxis, m_stiffness);
            ((Generic6DofSpringConstraint)m_constraintPtr).SetDamping((int)m_springAxis, m_damping);
            m_constraintPtr.Userobject = this;
            m_constraintPtr.DebugDrawSize = m_debugDrawSize;
            m_constraintPtr.BreakingImpulseThreshold = m_breakingImpulseThreshold;
            m_constraintPtr.OverrideNumSolverIterations = m_overrideNumSolverIterations;
            return true;
        }


    }
}