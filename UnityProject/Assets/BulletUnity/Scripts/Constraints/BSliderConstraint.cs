using System;
using UnityEngine;
using System.Collections;
using BulletSharp;
using BM = BulletSharp.Math;

namespace BulletUnity {
    [AddComponentMenu("Physics Bullet/Constraints/Slider")]
    public class BSliderConstraint : BTypedConstraint {
        public static string HelpMessage = "X (red) is slide axis. Angular limits are measured from Y (green) toward Z. \n" +
                                            "\nTIP: To see constraint limits:\n" +
                                            "  - In BulletPhysicsWorld turn on 'Do Debug Draw' and set 'Debug Draw Mode' flags\n" +
                                            "  - On Constraint set 'Debug Draw Size'\n" +
                                            "  - Press play";

        [Header("Limits")]
        [SerializeField]
        protected float m_lowerLinearLimit = -10f;
        public float lowerLinearLimit
        {
            get { return m_lowerLinearLimit; }
            set
            {
                
                if (m_constraintPtr != null)
                {
                    ((SliderConstraint)m_constraintPtr).LowerLinearLimit = value;
                }
                m_lowerLinearLimit = value;
            }
        }

        [SerializeField]
        protected float m_upperLinearLimit = 10f;
        public float upperLinearLimit
        {
            get { return m_upperLinearLimit; }
            set
            {

                if (m_constraintPtr != null)
                {
                    ((SliderConstraint)m_constraintPtr).UpperLinearLimit = value;
                }
                m_upperLinearLimit = value;
            }
        }

        [SerializeField]
        protected float m_lowerAngularLimitRadians = -Mathf.PI;
        public float lowerAngularLimitRadians
        {
            get { return m_lowerAngularLimitRadians; }
            set
            {

                if (m_constraintPtr != null)
                {
                    ((SliderConstraint)m_constraintPtr).LowerAngularLimit = value;
                }
                m_lowerAngularLimitRadians = value;
            }
        }

        [SerializeField]
        protected float m_upperAngularLimitRadians = Mathf.PI;
        public float upperAngularLimitRadians
        {
            get { return m_upperAngularLimitRadians; }
            set
            {

                if (m_constraintPtr != null)
                {
                    ((SliderConstraint)m_constraintPtr).UpperAngularLimit = value;
                }
                m_upperAngularLimitRadians = value;
            }
        }

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
                Debug.LogError("BSliderConstraint needs to be added to a component with a BRigidBody.");
                return false;
            }
            if (!targetRigidBodyA.isInWorld)
            {
                world.AddRigidBody(targetRigidBodyA);
            }
            RigidBody rba = (RigidBody) targetRigidBodyA.GetCollisionObject();
            if (rba == null) {
                Debug.LogError("Constraint could not get bullet RigidBody from target rigid body");
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
                    Debug.LogError("Constraint could not get bullet RigidBody from target rigid body");
                    return false;
                }

                BM.Matrix frameInA, frameInOther;
                string errormsg = "";
                if (CreateFramesA_B(m_localConstraintAxisX, m_localConstraintAxisY, m_localConstraintPoint, out frameInA, out frameInOther, ref errormsg))
                {
                    m_constraintPtr = new SliderConstraint(rbb, rba, frameInOther, frameInA, true);
                } else
                {
                    Debug.LogError(errormsg);
                    return false;
                }
            } else
            {
                BulletSharp.Math.Matrix frameInA = BM.Matrix.Identity;
                string errormsg = "";
                if (CreateFrame(m_localConstraintAxisX, m_localConstraintAxisY, m_localConstraintPoint, ref frameInA, ref errormsg))
                {
                    m_constraintPtr = new SliderConstraint(rba, frameInA, true);
                } else
                {
                    Debug.LogError(errormsg);
                    return false;
                }
            }
            SliderConstraint sl = (SliderConstraint)m_constraintPtr;  
            sl.LowerLinearLimit = m_lowerLinearLimit;
            sl.UpperLinearLimit = m_upperLinearLimit;

            sl.LowerAngularLimit = m_lowerAngularLimitRadians;
            sl.UpperAngularLimit = m_upperAngularLimitRadians;
            m_constraintPtr.Userobject = this;
            m_constraintPtr.DebugDrawSize = m_debugDrawSize;
            m_constraintPtr.BreakingImpulseThreshold = m_breakingImpulseThreshold;
            m_constraintPtr.OverrideNumSolverIterations = m_overrideNumSolverIterations;
            return true;
        }
    }
}
