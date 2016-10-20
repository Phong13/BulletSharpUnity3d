using System;
using UnityEngine;
using System.Collections;
using BulletSharp;
using BM = BulletSharp.Math;

namespace BulletUnity {

    [System.Serializable]
    public abstract class BTypedConstraint : MonoBehaviour, IDisposable {
        protected bool m_startWasCalled = false;

        public enum ConstraintType
        {
            constrainToPointInSpace,
            constrainToAnotherBody
        }

        [Header("Reference Frame Local To This Object")]
        [SerializeField]
        protected Vector3 m_localConstraintPoint = Vector3.zero;
        public Vector3 localConstraintPoint
        {
            get { return m_localConstraintPoint; }
            set
            {
                if (m_constraintPtr != null && m_localConstraintPoint != value)
                {
                    Debug.LogError("Cannot change the local constraint point once the constraint has been created");
                }
                else
                {
                    m_localConstraintPoint = value;
                }

            }
        }

        [SerializeField]
        protected Vector3 m_localConstraintAxisX = Vector3.forward;
        public Vector3 localConstraintAxisX
        {
            get { return m_localConstraintAxisX; }
            set
            {
                if (m_constraintPtr != null && m_localConstraintAxisX != value)
                {
                    Debug.LogError("Cannot change the local constraint axis once the constraint has been created");
                }
                else
                {
                    m_localConstraintAxisX = value;
                }

            }
        }

        [SerializeField]
        protected Vector3 m_localConstraintAxisY = Vector3.up;
        public Vector3 localConstraintAxisY
        {
            get { return m_localConstraintAxisY; }
            set
            {
                if (m_constraintPtr != null && m_localConstraintAxisY != value)
                {
                    Debug.LogError("Cannot change the local constraint axis once the constraint has been created");
                }
                else
                {
                    m_localConstraintAxisY = value;
                }

            }
        }


        [SerializeField]
        protected float m_breakingImpulseThreshold = Mathf.Infinity;
        public float breakingImpulseThreshold
        {
            get { return m_breakingImpulseThreshold; }
            set
            {
                if (m_constraintPtr != null)
                {
                    m_constraintPtr.BreakingImpulseThreshold = value;
                }
                m_breakingImpulseThreshold = value;

            }
        }

        [SerializeField]
        protected bool m_disableCollisionsBetweenConstrainedBodies = true;
        public bool disableCollisionsBetweenConstrainedBodies
        {
            get { return m_disableCollisionsBetweenConstrainedBodies; }
            set
            {
                if (m_constraintPtr != null && m_disableCollisionsBetweenConstrainedBodies != value)
                {
                    Debug.LogError("Cannot only set disableCollisionsBetweenConstrainedBodies before the constraint has been created");
                } else
                {
                    m_disableCollisionsBetweenConstrainedBodies = value;
                }

            }
        }

        [SerializeField]
        protected ConstraintType m_constraintType;
        public ConstraintType constraintType
        {
            get { return m_constraintType; }
            set
            {
                if (m_constraintPtr != null && m_constraintType != value)
                {
                    Debug.LogError("Cannot set the constraint type once the constraint has been created");
                }
                else
                {
                    m_constraintType = value;
                }
            }
        }

        [HideInInspector]
        [SerializeField]
        protected BRigidBody m_thisRigidBody;
        public BRigidBody thisRigidBody
        {
            get { return m_thisRigidBody; }
            set
            {
                if (m_constraintPtr != null && m_thisRigidBody != value)
                {
                    Debug.LogError("Cannot change the rigidbody once once the constraint has been created");
                }
                else
                {
                    m_thisRigidBody = value;
                }
            }
        }

        [SerializeField]
        protected BRigidBody m_otherRigidBody;
        public BRigidBody otherRigidBody
        {
            get { return m_otherRigidBody; }
            set
            {
                if (m_constraintPtr != null && m_otherRigidBody != value)
                {
                    Debug.LogError("Cannot change the rigidbody once once the constraint has been created");
                }
                else
                {
                    m_otherRigidBody = value;
                }
            }
        }

        [SerializeField]
        protected float m_debugDrawSize;
        public float debugDrawSize
        {
            get { return m_debugDrawSize; }
            set
            {
                if (m_constraintPtr != null)
                {
                    m_constraintPtr.DebugDrawSize = value;
                }
                else
                {
                    m_debugDrawSize = value;
                }
            }
        }

        [SerializeField]
        protected int m_overrideNumSolverIterations = 20;
        public int overrideNumSolverIterations
        {
            get { return m_overrideNumSolverIterations; }
            set
            {
                if (value < 1) value = 1;
                if (m_constraintPtr != null)
                {
                    m_constraintPtr.OverrideNumSolverIterations = value;
                }
                else
                {
                    m_overrideNumSolverIterations = value;
                }
            }
        }

        internal TypedConstraint m_constraintPtr = null;
        internal bool m_isInWorld = false;

        public void DrawTransformGizmos(Transform t, Vector3 pivotPoint, Vector3 forward, Vector3 up)
        {
            Vector3 pivotWorld = t.TransformPoint(pivotPoint);
            Vector3 xWorld = t.TransformDirection(forward).normalized;
            Vector3 yWorld = t.TransformDirection(up).normalized;
            Vector3 zWorld = Vector3.Cross(xWorld, yWorld);
            yWorld = Vector3.Cross(zWorld, xWorld);
            yWorld.Normalize();
            xWorld.Normalize();
            Gizmos.color = Color.red;
            Gizmos.DrawLine(pivotWorld, pivotWorld + xWorld);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(pivotWorld, pivotWorld + yWorld);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(pivotWorld, pivotWorld + zWorld);
        }

        public bool CreateFrame(UnityEngine.Vector3 forward, UnityEngine.Vector3 up, UnityEngine.Vector3 constraintPoint, ref BulletSharp.Math.Matrix m, ref string errorMsg)
        {
            BulletSharp.Math.Vector4 x;
            BulletSharp.Math.Vector4 y;
            BulletSharp.Math.Vector4 z;
            if (forward == Vector3.zero)
            {
                errorMsg = "forward vector must not be zero";
                return false;
            }
            forward.Normalize();
            if (up == Vector3.zero)
            {
                errorMsg = "up vector must not be zero";
                return false;
            }
            Vector3 right = Vector3.Cross(forward, up);
            if (right == Vector3.zero)
            {
                errorMsg = "forward and up vector must not be colinear";
                return false;
            }
            up = Vector3.Cross(right, forward);
            right.Normalize();
            up.Normalize();
            x.X = forward.x; x.Y = forward.y; x.Z = forward.z; x.W = 0f;
            y.X = up.x; y.Y = up.y; y.Z = up.z; y.W = 0f;
            z.X = right.x; z.Y = right.y; z.Z = right.z; z.W = 0f;
            m.Row1 = x;
            m.Row2 = y;
            m.Row3 = z;
            m.Origin = constraintPoint.ToBullet();
            return true;
        }


        // Object A has the constraint compontent and is constrainted to object B
        // The constraint frame is defined relative to A by three vectors.
        // This method calculates the frames A and B that need to be passed to bullet
        public bool CreateFramesA_B(UnityEngine.Vector3 forwardInA, UnityEngine.Vector3 upInA, UnityEngine.Vector3 constraintPivotInA, out BM.Matrix frameInA, out BM.Matrix frameInB, ref string errorMsg)
        {
            frameInA = BM.Matrix.Identity;
            if (!CreateFrame(forwardInA, upInA, constraintPivotInA, ref frameInA, ref errorMsg))
            {
                frameInB = BM.Matrix.Identity;
                return false;
            }
            BM.Vector4 x = frameInA.Row1;
            BM.Vector4 y = frameInA.Row2;
            BM.Vector4 z = frameInA.Row3;
            Vector3 xx = new Vector3(x.X, x.Y, x.Z);
            Vector3 yy = new Vector3(y.X, y.Y, y.Z);
            Vector3 zz = new Vector3(z.X, z.Y, z.Z);
            Quaternion q = transform.localRotation * Quaternion.Inverse(m_otherRigidBody.transform.localRotation);
            xx = q * xx;
            yy = q * yy;
            zz = q * zz;
            frameInB = BM.Matrix.Identity;
            frameInB.Row1 = new BM.Vector4(xx.ToBullet(), 0f);
            frameInB.Row2 = new BM.Vector4(yy.ToBullet(), 0f);
            frameInB.Row3 = new BM.Vector4(zz.ToBullet(), 0f);
            frameInB.Origin = m_otherRigidBody.transform.InverseTransformPoint(transform.TransformPoint(constraintPivotInA)).ToBullet();
            return true;
        }

        public virtual void OnDrawGizmosSelected()
        {
            DrawTransformGizmos(transform, m_localConstraintPoint, m_localConstraintAxisX, m_localConstraintAxisY);
        }



        protected virtual void AddToBulletWorld()
        {
            if (!m_isInWorld)
            {
                Debug.Assert(m_thisRigidBody.isInWorld, "Constrained bodies must be added to world before constraints " + this);
                if (m_constraintType == ConstraintType.constrainToAnotherBody)
                {
                    Debug.Assert(m_otherRigidBody.isInWorld, "Constrained bodies must be added to world before constraints " + this);
                }
                BPhysicsWorld.Get().AddConstraint(this);
            }
        }

        protected virtual void RemoveFromBulletWorld()
        {
            if (m_isInWorld)
            {
                BPhysicsWorld.Get().RemoveConstraint(m_constraintPtr);
            }
        }

        protected virtual void Start()
        {
            m_startWasCalled = true;
            //deal with nasty script order of execution bug. Order of Start call is random so force rigid bodies to be in the world before constraint is added
            if (!m_thisRigidBody.isInWorld)
            {
                m_thisRigidBody.Start();
            }
            if (m_constraintType == ConstraintType.constrainToAnotherBody && !m_otherRigidBody.isInWorld)
            {
                m_otherRigidBody.Start();
            }
            AddToBulletWorld();
        }

        void OnDestroy() {
            Dispose(false);
        }

        public void OnEnable()
        {
            m_thisRigidBody = GetComponent<BRigidBody>();
            if (m_thisRigidBody == null) Debug.LogError("Constraint must be added to a game object with a BRigidBody.");
            if (m_startWasCalled)
            {
                AddToBulletWorld();
            }
        }

        public void OnDisable()
        {
            RemoveFromBulletWorld();
        }

        //do not override this
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        //override this one
        protected virtual void Dispose(bool isdisposing) {
            if (m_constraintPtr != null) {
                m_constraintPtr.Dispose();
                m_constraintPtr = null;
            }
        }

        //called by Physics World just before constraint is added to world.
        //the current constraint properties are used to rebuild the constraint.
        internal abstract bool _BuildConstraint();

        public virtual TypedConstraint GetConstraint()
        {
            if (m_constraintPtr == null)
            {
                _BuildConstraint();
            }
            return m_constraintPtr;
        }
    }
}
