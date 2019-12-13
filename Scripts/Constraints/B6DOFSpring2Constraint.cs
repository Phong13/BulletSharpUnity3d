using BulletSharp;
using UnityEngine;
using BM = BulletSharp.Math;

namespace BulletUnity
{
    [AddComponentMenu("Physics Bullet/Constraints/6 Degree Of Freedom Spring 2")]
    public class B6DOFSpring2Constraint : BTypedConstraint
    {
        [Header("Spring")]
        [SerializeField]
        protected Axis m_springAxis;
        public Axis SpringAxis
        {
            get { return m_springAxis; }
        }


        [SerializeField]
        protected float m_bounce = 0.0f;
        public float Bounce
        {
            get { return m_bounce; }
            set
            {
                m_bounce = value;
                if (m_constraintPtr != null)
                {
                    ((Generic6DofSpring2Constraint)m_constraintPtr).SetBounce((int)m_springAxis, m_bounce);
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
                    ((Generic6DofSpring2Constraint)m_constraintPtr).SetDamping((int)m_springAxis, m_damping);
                }
            }
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
                    ((Generic6DofSpring2Constraint)m_constraintPtr).SetStiffness((int)m_springAxis, m_stiffness);
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
                    ((Generic6DofSpring2Constraint)m_constraintPtr).SetLimit((int)m_springAxis, m_lowLimit, m_highLimit);
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
                    ((Generic6DofSpring2Constraint)m_constraintPtr).SetLimit((int)m_springAxis, m_lowLimit, m_highLimit);
                }
            }
        }
        [SerializeField]
        protected Vector3 m_linearLimitLower;
        public Vector3 linearLimitLower
        {
            get { return m_linearLimitLower; }
            set
            {
                m_linearLimitLower = value;
                if (m_constraintPtr != null)
                {
                    ((Generic6DofSpring2Constraint)m_constraintPtr).LinearLowerLimit = m_linearLimitLower.ToBullet();
                }
            }
        }

        [SerializeField]
        protected Vector3 m_linearLimitUpper;
        public Vector3 linearLimitUpper
        {
            get { return m_linearLimitUpper; }
            set
            {
                m_linearLimitUpper = value;
                if (m_constraintPtr != null)
                {
                    ((Generic6DofSpring2Constraint)m_constraintPtr).LinearUpperLimit = m_linearLimitUpper.ToBullet();
                }
            }
        }

        [SerializeField]
        protected Vector3 m_angularLimitLowerRadians;
        public Vector3 angularLimitLowerRadians
        {
            get { return m_angularLimitLowerRadians; }
            set
            {
                m_angularLimitLowerRadians = value;
                if (m_constraintPtr != null)
                {
                    ((Generic6DofSpring2Constraint)m_constraintPtr).AngularLowerLimit = m_angularLimitLowerRadians.ToBullet();
                }
            }
        }

        [SerializeField]
        protected Vector3 m_angularLimitUpperRadians;
        public Vector3 angularLimitUpperRadians
        {
            get { return m_angularLimitUpperRadians; }
            set
            {
                m_angularLimitUpperRadians = value;
                if (m_constraintPtr != null)
                {
                    ((Generic6DofSpring2Constraint)m_constraintPtr).AngularUpperLimit = m_angularLimitUpperRadians.ToBullet();
                }
            }
        }

        [Header("Motor")]
        [SerializeField]
        protected Vector3 m_motorLinearTargetVelocity;
        public Vector3 motorLinearTargetVelocity
        {
            get { return m_motorLinearTargetVelocity; }
            set
            {
                m_motorLinearTargetVelocity = value;
                if (m_constraintPtr != null)
                {
                    ((Generic6DofSpring2Constraint)m_constraintPtr).TranslationalLimitMotor.TargetVelocity = m_motorLinearTargetVelocity.ToBullet();
                }
            }
        }

        [SerializeField]
        protected Vector3 m_motorLinearMaxMotorForce;
        public Vector3 motorLinearMaxMotorForce
        {
            get { return m_motorLinearMaxMotorForce; }
            set
            {
                m_motorLinearMaxMotorForce = value;
                if (m_constraintPtr != null)
                {
                    ((Generic6DofSpring2Constraint)m_constraintPtr).TranslationalLimitMotor.MaxMotorForce = m_motorLinearMaxMotorForce.ToBullet();
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
                    m_constraintPtr = new Generic6DofSpring2Constraint(rba, rbb, frameInA, frameInOther);
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
                ((Generic6DofSpring2Constraint)m_constraintPtr).SetLimit((int)m_springAxis, m_lowLimit, m_highLimit);
            }
            ((Generic6DofSpring2Constraint)m_constraintPtr).EnableSpring((int)m_springAxis, true);
            ((Generic6DofSpring2Constraint)m_constraintPtr).SetStiffness((int)m_springAxis, m_stiffness);
            ((Generic6DofSpring2Constraint)m_constraintPtr).SetDamping((int)m_springAxis, m_damping);
            ((Generic6DofSpring2Constraint)m_constraintPtr).SetBounce((int)m_springAxis, m_bounce);
            ((Generic6DofSpring2Constraint)m_constraintPtr).SetEquilibriumPoint();
            ((Generic6DofSpring2Constraint)m_constraintPtr).LinearLowerLimit = m_linearLimitLower.ToBullet();
            ((Generic6DofSpring2Constraint)m_constraintPtr).LinearUpperLimit = m_linearLimitUpper.ToBullet();
            ((Generic6DofSpring2Constraint)m_constraintPtr).AngularLowerLimit = m_angularLimitLowerRadians.ToBullet();
            ((Generic6DofSpring2Constraint)m_constraintPtr).AngularUpperLimit = m_angularLimitUpperRadians.ToBullet();
            m_constraintPtr.Userobject = this;
            m_constraintPtr.DebugDrawSize = m_debugDrawSize;
            m_constraintPtr.BreakingImpulseThreshold = m_breakingImpulseThreshold;
            m_constraintPtr.OverrideNumSolverIterations = m_overrideNumSolverIterations;
            return true;
        }


    }
}