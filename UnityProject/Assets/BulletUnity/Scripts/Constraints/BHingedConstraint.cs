using System;
using UnityEngine;
using System.Collections;
using BulletSharp;

namespace BulletUnity
{
    [System.Serializable]
    public class BHingedConstraint : BTypedConstraint
    {
        //todo should be properties so can capture changes and propagate to scene
        //public Vector3 pointOnHinge = Vector3 .zero;
        //public Vector3 pivotAxis = new Vector3(0, 1, 0);

        public Vector3 pivotInA = new Vector3(0, 1, 0);
        public Vector3 pivotInB = new Vector3(0, 1, 0);

        public Vector3 AxisInA = new Vector3(-5, 0, 0);
        public Vector3 AxisInB = new Vector3(5, 0, 0);

        void Start()
        //public void OnEnable()
        {
            isInWorld = BPhysicsWorld.Get().AddConstraint(this);
        }

        public void OnDisable()
        {
            if (isInWorld)
            {
                BPhysicsWorld.Get().RemoveConstraint(constraintPtr);
            }
            isInWorld = false;
        }

        //called by Physics World just before constraint is added to world.
        //the current constraint properties are used to rebuild the constraint.
        internal override bool _BuildConstraint()
        {
            BPhysicsWorld world = BPhysicsWorld.Get();
            if (constraintPtr != null)
            {
                if (isInWorld && world != null)
                {
                    isInWorld = false;
                    world.RemoveConstraint(constraintPtr);
                }
            }
            if (RigidBodyA == null)
            {
                Debug.LogError("Must be attahced to object with a BRigidBody.");
                return false;
            }

            if (pivotInA == Vector3.zero)
            {
                Debug.LogError("Constaint axis cannot be zero vector");
                return false;
            }



            if (RigidBodyB == null) //attach to world
            {
                RigidBodyA.ActivationState = ActivationState.DisableDeactivation;
                constraintPtr = new HingeConstraint(RigidBodyA, pivotInA.ToBullet(), AxisInA.ToBullet());
                //Debug.LogError("Constraint could not get bullet RigidBody from target rigid body");
                //return false;
            }
            else  //Attach to other rb
            {
                //public HingeConstraint(RigidBody rigidBodyA, RigidBody rigidBodyB, Vector3 pivotInA, Vector3 pivotInB, Vector3 axisInA, Vector3 axisInB, bool useReferenceFrameA = false)
                RigidBodyA.ActivationState = ActivationState.DisableDeactivation;
                RigidBodyB.ActivationState = ActivationState.DisableDeactivation;
                constraintPtr = new HingeConstraint(RigidBodyA, RigidBodyB, pivotInA.ToBullet(), pivotInB.ToBullet(), AxisInA.ToBullet(), AxisInB.ToBullet(), true);
            }



            return true;
        }

        public override TypedConstraint GetConstraint()
        {
            if (constraintPtr == null)
            {
                _BuildConstraint();
            }
            return constraintPtr;
        }
    }
}
