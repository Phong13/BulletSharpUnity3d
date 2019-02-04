using System;
using UnityEngine;
using System.Collections;
using BulletSharp;
using BM = BulletSharp.Math;

namespace BulletUnity {
    [AddComponentMenu("Physics Bullet/Constraints/Hinge")]
    public class BHingedConstraint : BTypedConstraint {

        //todo should be properties so can capture changes and propagate to scene
        public static string HelpMessage = "X (red) is axis of the hinge.\n" +
                                            "\nTIP: To see constraint limits:\n" +
                                            "  - In BulletPhysicsWorld turn on 'Do Debug Draw' and set 'Debug Draw Mode' flags\n" +
                                            "  - On Constraint set 'Debug Draw Size'\n" +
                                            "  - Press play";
        [SerializeField]
        protected bool m_enableMotor;
        public bool enableMotor
        {
            get { return m_enableMotor; }
            set
            {

                if (m_constraintPtr != null)
                {
                    ((HingeConstraint)m_constraintPtr).EnableMotor = value;
                }
                m_enableMotor = value;
            }
        }

        [SerializeField]
        protected float m_targetMotorAngularVelocity = 0f;
        public float targetMotorAngularVelocity
        {
            get { return m_targetMotorAngularVelocity; }
            set
            {
                m_targetMotorAngularVelocity = value;
                if (m_constraintPtr != null)
                {
                    ((HingeConstraint)m_constraintPtr).EnableAngularMotor(m_enableMotor,m_targetMotorAngularVelocity,m_maxMotorImpulse);
                }
            }
        }


        [SerializeField]
        protected float m_maxMotorImpulse = 0f;
        public float maxMotorImpulse
        {
            get { return m_maxMotorImpulse; }
            set
            {

                if (m_constraintPtr != null)
                {
                    ((HingeConstraint)m_constraintPtr).MaxMotorImpulse = value;
                }
                m_maxMotorImpulse = value;
            }
        }

        [SerializeField]
        protected bool m_setLimit;
        public bool setLimit
        {
            get { return m_setLimit; }
            set { m_setLimit = value; }
        }

        [SerializeField]
        protected float m_lowLimitAngleRadians;
        public float lowLimitAngleRadians
        {
            get { return m_lowLimitAngleRadians; }
            set
            {
                m_lowLimitAngleRadians = value;
                if (m_constraintPtr != null)
                {
                    ((HingeConstraint)m_constraintPtr).SetLimit(m_lowLimitAngleRadians,m_highLimitAngleRadians,m_limitSoftness,m_limitBiasFactor);
                }
            }
        }

        [SerializeField]
        protected float m_highLimitAngleRadians;
        public float highLimitAngleRadians
        {
            get { return m_highLimitAngleRadians; }
            set
            {
                m_highLimitAngleRadians = value;
                if (m_constraintPtr != null)
                {
                    ((HingeConstraint)m_constraintPtr).SetLimit(m_lowLimitAngleRadians, m_highLimitAngleRadians, m_limitSoftness, m_limitBiasFactor);
                }
            }
        }

        [SerializeField]
        protected float m_limitSoftness = .9f;
        public float limitSoftness
        {
            get { return m_limitSoftness; }
            set
            {
                m_limitSoftness = value;
                if (m_constraintPtr != null)
                {
                    ((HingeConstraint)m_constraintPtr).SetLimit(m_lowLimitAngleRadians, m_highLimitAngleRadians, m_limitSoftness, m_limitBiasFactor);
                }
            }
        }

        [SerializeField]
        protected float m_limitBiasFactor = .3f;
        public float limitBiasFactor
        {
            get { return m_limitBiasFactor; }
            set
            {
                m_limitBiasFactor = value;
                if (m_constraintPtr != null)
                {
                    ((HingeConstraint)m_constraintPtr).SetLimit(m_lowLimitAngleRadians, m_highLimitAngleRadians, m_limitSoftness, m_limitBiasFactor);
                }
            }
        }


        public float GetAngle() {
            if (m_constraintPtr == null) {
                return 0;
            }
            return ((HingeConstraint)m_constraintPtr).HingeAngle;
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
                Debug.LogError("BHingedConstraint needs to be added to a component with a BRigidBody.");
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
            RigidBody rba = (RigidBody) targetRigidBodyA.GetCollisionObject();
            if (rba == null) {
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
                    //warning the frameInA, frameInB version of the constructor is broken
                    m_constraintPtr = new HingeConstraint(rbb, rba, frameInOther.Origin, frameInA.Origin, (BM.Vector3)frameInOther.Basis.Column1, (BM.Vector3)frameInA.Basis.Column1);
                } else
                {
                    Debug.LogError(errormsg);
                    return false;
                }
            }
            else {
                //BM.Matrix frameInA = BM.Matrix.Identity;
                //CreateFrame(m_localConstraintForwardDir, m_localConstraintUpDir, m_localConstraintPoint, ref frameInA);
                m_constraintPtr = new HingeConstraint(rba, m_localConstraintPoint.ToBullet(),m_localConstraintAxisX.ToBullet(), false);
            }
            if (m_enableMotor)
            {
                ((HingeConstraint)m_constraintPtr).EnableAngularMotor(true, m_targetMotorAngularVelocity, m_maxMotorImpulse);
            }
            if (m_setLimit)
            {
                ((HingeConstraint)m_constraintPtr).SetLimit(m_lowLimitAngleRadians, m_highLimitAngleRadians, m_limitSoftness, m_limitBiasFactor);
            }
            m_constraintPtr.Userobject = this;
            m_constraintPtr.DebugDrawSize = m_debugDrawSize;
            m_constraintPtr.BreakingImpulseThreshold = m_breakingImpulseThreshold;
            m_constraintPtr.OverrideNumSolverIterations = m_overrideNumSolverIterations;
            return true;
        }
    }
}
