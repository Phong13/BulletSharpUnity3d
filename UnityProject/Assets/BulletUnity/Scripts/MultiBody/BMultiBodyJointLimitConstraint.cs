using UnityEngine;
using System.Collections;
using BulletSharp;
using System;

namespace BulletUnity
{
    public class BMultiBodyJointLimitConstraint : BMultiBodyConstraint, IDisposable
    {
        [Tooltip("Degrees")]
        public float m_jointLowerLimit = -1;

        [Tooltip("Degrees")]
        public float m_jointUpperLimit = -1;

        public bool m_doDrawGizmos = true;

        private void OnValidate()
        {
            if (m_jointLowerLimit > m_jointUpperLimit)
            {
                Debug.LogError("jointLowerLimit must not be greater than jointUpperLimit");
                m_jointLowerLimit = m_jointUpperLimit;
            }
            if (m_jointLowerLimit < -180f)
            {
                m_jointLowerLimit = 180f;
            }
            if (m_jointUpperLimit > 180f)
            {
                m_jointUpperLimit = 180f;
            }
        }

        protected override MultiBodyConstraint _CreateConstraint(MultiBody mb, int linkIndex)
        {
            return new MultiBodyJointLimitConstraint(mb, linkIndex, m_jointLowerLimit * Mathf.Deg2Rad, m_jointUpperLimit * Mathf.Deg2Rad);
        }

        protected void OnDrawGizmosSelected()
        {
            if (!m_doDrawGizmos)
            {
                return;
            }

            BMultiBodyLink link = GetComponent<BMultiBodyLink>();
            if (link != null)
            {
                if (link.rotationAxis != Vector3.zero)
                {
                    float arcLen = m_jointUpperLimit - m_jointLowerLimit;
                    UnityEditor.Handles.color = new Color(.7f, .5f, .5f, .6f);
                    Vector3 p = transform.TransformPoint(link.localPivotPosition);
                    Vector3 from;
                    Vector3 axisOfRotation;
                    if (link.axesAreFrozen)
                    {
                        axisOfRotation = transform.parent.TransformDirection(link.rotationAxisInParentFrame);
                        from = transform.parent.TransformDirection(link.jointToThisCOMInParentFrame);
                    }
                    else
                    {
                        axisOfRotation = transform.TransformDirection(link.rotationAxis);
                        from = transform.TransformDirection(-link.localPivotPosition);
                        if (from.magnitude < 10E-7f)
                        {
                            from = transform.parent.position - p;
                        }

                        from = Vector3.ProjectOnPlane(from, axisOfRotation);
                    }
                    if (from.magnitude > 10E-7f)
                    {
                        from.Normalize();
                        Quaternion q = Quaternion.AngleAxis(m_jointLowerLimit, axisOfRotation);
                        from = q * from;
                        UnityEditor.Handles.DrawSolidArc(p, axisOfRotation, from, arcLen, 1f * link.gizmoScale);
                    }
                }
            }
        }
    }
}
