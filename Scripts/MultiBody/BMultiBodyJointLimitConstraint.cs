using BulletSharp;
using UnityEngine;

public class BMultiBodyJointLimitConstraint : BMultiBodyConstraint
{
    [SerializeField]
    float lower;
    public float Lower
    {
        get
        {
            return lower;
        }
        set
        {
            if (jointLimit != null)
                Debug.LogErrorFormat(this, "Cannot change {0} on constraint {1} when it's already created.", nameof(Lower), name);
            else
                lower = value;
        }
    }

    [SerializeField]
    float upper;
    public float Upper
    {
        get
        {
            return upper;
        }
        set
        {
            if (jointLimit != null)
                Debug.LogErrorFormat(this, "Cannot change {0} on constraint {1} when it's already created.", nameof(Upper), name);
            else
                upper = value;

        }
    }

    internal MultiBodyJointLimitConstraint jointLimit;
    public MultiBodyJointLimitConstraint JoinLimit
    {
        get
        {
            return jointLimit;
        }
    }

    protected override MultiBodyConstraint SetupConstraint()
    {
        jointLimit = new MultiBodyJointLimitConstraint(multiBody.MultiBody, linkId, Lower, Upper);
        return jointLimit;
    }
}

