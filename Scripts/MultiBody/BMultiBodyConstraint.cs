using System;
using BulletSharp;
using BulletUnity;
using UnityEngine;

public abstract class BMultiBodyConstraint : MonoBehaviour
{

    internal BMultiBody multiBody;
    internal int linkId;

    protected MultiBodyConstraint constraint;

    protected abstract MultiBodyConstraint SetupConstraint();

    public bool AddConstraintToMultiBody(BMultiBody mb, int linkId)
    {
        if (mb == null)
            throw new ArgumentNullException(nameof(mb));

        multiBody = mb;
        this.linkId = linkId;

        constraint = SetupConstraint();
        if (constraint == null)
            return false;

        BPhysicsWorld.Get().AddMultiBodyConstraint(constraint);

        return true;
    }
}

