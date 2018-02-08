using UnityEngine;
using System.Collections;
using BulletSharp;
using System;

namespace BulletUnity
{
    public abstract class BMultiBodyJointMotor : BMultiBodyConstraint, IDisposable
    {
        public float desiredVelocity;
        public float maxMotorImpulse;
       
        protected override MultiBodyConstraint _CreateConstraint(MultiBody mb, int linkIndex)
        {
            return new MultiBodyJointMotor(mb, linkIndex, desiredVelocity, maxMotorImpulse);
        }
    }
}
