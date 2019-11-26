using BulletUnity;
using UnityEngine;
public class BMultiBodyFixedLink : BMultiBodyLink
{


    public Transform Pivot;

    public override void SetupLink(BulletSharp.Math.Vector3 linkInertia)
    {
        Quaternion parentToThisRotation = parentTransform.rotation * Quaternion.Inverse(transform.rotation);
        Vector3 linkPointInParent = parentTransform.InverseTransformPoint(transform.position);
        Vector3 linkPointInPivot = Pivot.transform.InverseTransformPoint(transform.position);
        MultiBody.MultiBody.SetupFixed(LinkId, Mass, linkInertia, ParentLinkId, parentToThisRotation.ToBullet(),
           (linkPointInParent - linkPointInPivot).ToBullet(), linkPointInPivot.ToBullet());


    }
}

