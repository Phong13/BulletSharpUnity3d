using UnityEngine;
using System.Collections;
using BulletSharp;
using System;

namespace BulletUnity
{
    public class BMultiBodyJointLimitConstraint : BMultiBodyConstraint, IDisposable
    {
        public float jointLowerLimit = -1;
        public float jointUpperLimit = -1;

        protected override MultiBodyConstraint _CreateConstraint(MultiBody mb, int linkIndex)
        {
            return new MultiBodyJointLimitConstraint(mb, linkIndex, jointLowerLimit, jointUpperLimit);
        }
    }
}
