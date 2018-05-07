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
        public float desiredVelocity;
        public float maxMotorImpulse;
       
        protected override MultiBodyConstraint _CreateConstraint(MultiBody mb, int linkIndex)
        {
            return new MultiBodyJointMotor(mb, linkIndex, desiredVelocity, maxMotorImpulse);
        }
    }
}
