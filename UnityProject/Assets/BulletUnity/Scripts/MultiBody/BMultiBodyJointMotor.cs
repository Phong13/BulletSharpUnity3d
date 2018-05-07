using UnityEngine;
using System.Collections;
using BulletSharp;
using System;

namespace BulletUnity
{
    /// <summary>
    /// A joint can have more than one degree of freedom
    ///   How to handle using components
    ///   What does it mean to have both a constratint and a motor on the same body?
    ///   how to handle changing values in inspector after creating component.
    ///   
    ///         How to handle motor for different types of joints.
    ///         
    /// </summary>
    public class BMultiBodyJointMotor : BMultiBodyConstraint, IDisposable
    {
        protected override MultiBodyConstraint _CreateConstraint(MultiBody mb, int linkIndex)
        {
            return new MultiBodyJointMotor(mb, linkIndex, _targetVelocity, _maxMotorImpulse);
        }

        [SerializeField]
        float _targetVelocity = 0f;
        public float targetVelocity
        {
            get { return _targetVelocity; }
            set
            {
                if (m_multiBodyConstraintPtr != null && _targetVelocity != value)
                {
                    ((MultiBodyJointMotor) m_multiBodyConstraintPtr).SetVelocityTarget(value);
                }
                _targetVelocity = value;
            }
        }

        public void SetVelocityTarget(float velTarget, float kd)
        {
            if (m_multiBodyConstraintPtr != null)
            {
                ((MultiBodyJointMotor)m_multiBodyConstraintPtr).SetVelocityTarget(velTarget, kd);
            }
            _targetVelocity = velTarget;
        }

        public void SetPositionTarget(float posTarget)
        {
            if (m_multiBodyConstraintPtr != null)
            {
                ((MultiBodyJointMotor)m_multiBodyConstraintPtr).SetPositionTarget(posTarget);
            }
        }

        public void SetPositionTarget(float posTarget, float kp)
        {
            if (m_multiBodyConstraintPtr != null)
            {
                ((MultiBodyJointMotor)m_multiBodyConstraintPtr).SetPositionTarget(posTarget, kp);
            }
        }

    }
}
