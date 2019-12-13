using BulletUnity;
using UnityEngine;
public class BMultiBodyRevoluteLink : BMultiBodyLink
{


    public Transform Pivot;

    public Vector3 JointAxis = Vector3.up;
    public bool DisableParentCollision;

    public override void SetupLink(BulletSharp.Math.Vector3 linkInertia)
    {
        Quaternion parentToThisRotation = parentTransform.rotation * Quaternion.Inverse(transform.rotation);
        Vector3 linkPointInParent = parentTransform.InverseTransformPoint(transform.position);
        Vector3 linkPointInPivot = Pivot.transform.InverseTransformPoint(transform.position);
        MultiBody.MultiBody.SetupRevolute(LinkId, Mass, linkInertia, ParentLinkId, parentToThisRotation.ToBullet(), JointAxis.ToBullet(),
           (linkPointInParent - linkPointInPivot).ToBullet(), linkPointInPivot.ToBullet(), DisableParentCollision);

    }
}

