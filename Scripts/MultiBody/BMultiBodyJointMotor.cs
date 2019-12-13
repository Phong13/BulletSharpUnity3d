using BulletSharp;
using UnityEngine;

public class BMultiBodyJointMotor : BMultiBodyConstraint
{
    [SerializeField]
    float desiredVelocity;
    public float DesiredVelocity
    {
        get
        {
            return desiredVelocity;
        }
        set
        {
            desiredVelocity = value;
            if (jointMotor != null)
                jointMotor.SetVelocityTarget(desiredVelocity);
        }
    }

    [SerializeField]
    float maxMotorImpulse;
    public float MaxMotorImpulse
    {
        get
        {
            return maxMotorImpulse;
        }
        set
        {
            if (jointMotor != null)
                Debug.LogErrorFormat(this, "Cannot change {0} on constraint {1} when it's already created.", nameof(MaxMotorImpulse), name);
            else
                maxMotorImpulse = value;

        }
    }

    internal MultiBodyJointMotor jointMotor;
    public MultiBodyJointMotor JoinMotor
    {
        get
        {
            return jointMotor;
        }
    }

    protected override MultiBodyConstraint SetupConstraint()
    {
        jointMotor = new MultiBodyJointMotor(multiBody.MultiBody, linkId, DesiredVelocity, MaxMotorImpulse);
        return jointMotor;
    }
}

