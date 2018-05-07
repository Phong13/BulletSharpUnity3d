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

        public bool m_doDrawGizmos = true;

        [Range(.05f, 10f)]
        public float gizmoScale = .15f;

        [System.NonSerialized]
        protected MultiBodyLinkCollider m_linkCollider;

        [System.NonSerialized]
        protected MultiBodyLink m_link;

        [System.NonSerialized]
        internal int parentIndex;

        [System.NonSerialized]
        internal int index;

        /// <summary>
        /// Joint axis are frozen when multibody is created.
        /// </summary>
        private bool m_axisAreFrozen = false;
        public bool axesAreFrozen { get { return m_axisAreFrozen; } }

        private Vector3 m_rotationAxisInParentFrame;
        public Vector3 rotationAxisInParentFrame
        {
            get { return m_rotationAxisInParentFrame; }
            protected set
            {
                Debug.Assert(m_axisAreFrozen, "Should not be called before axis are frozen.");
                m_rotationAxisInParentFrame = value;
            }
        }

        private Vector3 m_jointToThisCOMInParentFrame;
        public Vector3 jointToThisCOMInParentFrame
        {
            get { return m_jointToThisCOMInParentFrame; }
            protected set
            {
                Debug.Assert(m_axisAreFrozen, "Should not be called before axis are frozen.");
                m_jointToThisCOMInParentFrame = value;
            }
        }

        private Vector3 m_thisPivotToJointCOMOffset;
        public Vector3 thisPivotToJointCOMOffset
        {
            get { return m_thisPivotToJointCOMOffset; }
            protected set
            {
                Debug.Assert(m_axisAreFrozen, "Should not be called before axis are frozen.");
                m_thisPivotToJointCOMOffset = value;
            }
        }

        private Quaternion m_parentToJointRotation;
        public Quaternion parentToJointRotation
        {
            get { return m_parentToJointRotation; }
            protected set
            {
                Debug.Assert(m_axisAreFrozen, "Should not be called before axis are frozen.");
                m_parentToJointRotation = value;
            }
        }

        private Vector3 m_parentCOM2JointPivotOffset;
        public Vector3 parentCOM2JointPivotOffset
        {
            get { return m_parentCOM2JointPivotOffset; }
            protected set
            {
                Debug.Assert(m_axisAreFrozen, "Should not be called before axis are frozen.");
                m_parentCOM2JointPivotOffset = value;
            }
        }

        public MultiBodyLinkCollider GetLinkCollider()
        {
            return m_linkCollider;
        }

        public MultiBodyLink GetMultiBodyLink()
        {
            return m_link;
        }

        /// <summary>
        /// BMultiBodyLinks do not create their own native parts. That happens as part of the MultiBody creation.
        /// After the native MultiBody has been created. The references to the native MultiBodyLinks can be 
        /// obtatined and are assigned using this function.
        /// </summary>
        internal void AssignMultiBodyLinkOnCreation(BMultiBody mb, MultiBodyLink link)
        {
            m_link = link;
        }

        /// <summary>
        /// BMultiBodyLinks do not create their own native parts. That happens as part of the MultiBody creation.
        /// After the native MultiBody has been created. The references to the native MultiBodyLinkColliders can be 
        /// obtatined and are assigned using this function.
        /// </summary>
        internal void AssignMultiBodyLinkColliderOnCreation(BMultiBody mb, MultiBodyLinkCollider linkCollider)
        {
            m_linkCollider = linkCollider;
        }

        internal override void Start()
        {
            //override baseclass because we don't want this collision object auto added to world.
            //the multibody will add it.
            m_startHasBeenCalled = true;
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

        /// <summary>
        /// Should only be called when the multibody is being created. The joint axes are frozen
        /// in the parents frame at that moment.
        /// </summary>
        internal void FreezeJointAxis()
        {
            m_jointToThisCOMInParentFrame = transform.parent.InverseTransformDirection(transform.InverseTransformDirection(-localPivotPosition));
            m_rotationAxisInParentFrame = transform.parent.InverseTransformDirection(transform.TransformDirection(rotationAxis));
            m_parentCOM2JointPivotOffset = transform.parent.InverseTransformPoint(transform.TransformPoint(localPivotPosition));
            m_thisPivotToJointCOMOffset = -localPivotPosition;
            m_parentToJointRotation = UnityEngine.Quaternion.Inverse(transform.localRotation);
            m_axisAreFrozen = true;
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
            if (!m_doDrawGizmos)
            {
                return;
            }

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

                    Vector3 axisOfRotation;
                    if (axesAreFrozen)
                    {
                        axisOfRotation = transform.parent.TransformDirection(rotationAxisInParentFrame);
                        rperp = transform.parent.TransformDirection(jointToThisCOMInParentFrame);
                    }
                    else
                    {
                        axisOfRotation = transform.TransformDirection(rotationAxis);
                        rperp = transform.TransformDirection(-localPivotPosition);
                        if (rperp.magnitude < 10E-7f)
                        {
                            rperp = transform.parent.position - transform.TransformPoint(localPivotPosition);
                        }

                        rperp = Vector3.ProjectOnPlane(rperp, axisOfRotation);
                    }

                    rperp.Normalize();
                }
            }
            else
            {
                rotationAxis = transform.forward;
                rperp = transform.up;
            }
            BUtility.DebugDrawTransform(transform, localPivotPosition, rotationAxis, rperp, gizmoScale);
        }
    }
}
