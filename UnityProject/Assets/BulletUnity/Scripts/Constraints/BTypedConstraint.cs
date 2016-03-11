using System;
using UnityEngine;
using System.Collections;
using BulletSharp;

namespace BulletUnity {
    [System.Serializable]
    public class BConstraintFrame
    {
        public Vector3 pivotPoint = Vector3.zero;
        public Vector3 forward = Vector3.forward;
        public Vector3 up = Vector3.up;

        public BulletSharp.Math.Matrix CreateBSMatrix()
        {
            return BulletSharp.Math.Matrix.AffineTransformation(1f, Quaternion.LookRotation(forward, up).ToBullet(), pivotPoint.ToBullet());
        } 

        public void DrawGizmos(Transform t)
        {
            Vector3 pivotWorld = t.TransformPoint(pivotPoint);
            Vector3 forwardWorld = t.TransformDirection(forward).normalized;
            Vector3 upWorld = t.TransformDirection(up).normalized;
            Vector3 rightWorld = Vector3.Cross(forwardWorld, upWorld);
            upWorld = Vector3.Cross(rightWorld, forwardWorld);
            upWorld.Normalize();
            forwardWorld.Normalize();
            Gizmos.color = Color.red;
            Gizmos.DrawLine(pivotWorld, pivotWorld + rightWorld);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(pivotWorld, pivotWorld + upWorld);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(pivotWorld, pivotWorld + forwardWorld);
        }
    }

    [System.Serializable]
    public abstract class BTypedConstraint : MonoBehaviour, IDisposable {
        protected bool startWasCalled = false;

        public enum ConstraintType
        {
            constrainToPointInSpace,
            constrainToAnotherBody
        }
        public bool disableCollisionsBetweenConstrainedBodies = true;
        public BRigidBody targetRigidBodyA;
        public BRigidBody targetRigidBodyB;

        internal TypedConstraint constraintPtr = null;
        internal bool isInWorld = false;

        protected virtual void AddToBulletWorld()
        {
            if (!isInWorld)
            {
                BPhysicsWorld.Get().AddConstraint(this);
            }
        }

        protected virtual void RemoveFromBulletWorld()
        {
            if (isInWorld)
            {
                BPhysicsWorld.Get().RemoveConstraint(constraintPtr);
            }
        }

        protected virtual void Start()
        {
            startWasCalled = true;
            AddToBulletWorld();
        }

        void OnDestroy() {
            Dispose(false);
        }

        public void OnEnable()
        {
            AddToBulletWorld();
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

        protected virtual void Dispose(bool isdisposing) {
            if (constraintPtr != null) {
                constraintPtr.Dispose();
                constraintPtr = null;
            }
        }

        //called by Physics World just before constraint is added to world.
        //the current constraint properties are used to rebuild the constraint.
        internal abstract bool _BuildConstraint();

        public virtual TypedConstraint GetConstraint()
        {
            if (constraintPtr == null)
            {
                _BuildConstraint();
            }
            return constraintPtr;
        }
    }
}
