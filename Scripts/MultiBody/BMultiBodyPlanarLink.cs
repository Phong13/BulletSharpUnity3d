using BulletUnity;
using UnityEngine;
public class BMultiBodyPlanarLink : BMultiBodyLink
{


    public Vector3 RotationAxis = Vector3.up;
    public bool DisableParentCollision;

    public override void SetupLink(BulletSharp.Math.Vector3 linkInertia)
    {
        Quaternion parentToThisRotation = parentTransform.rotation * Quaternion.Inverse(transform.rotation);
        Vector3 linkPointInParent = parentTransform.InverseTransformPoint(transform.position);
        MultiBody.MultiBody.SetupPlanar(LinkId, Mass, linkInertia, ParentLinkId, parentToThisRotation.ToBullet(), RotationAxis.ToBullet(),
           linkPointInParent.ToBullet(), DisableParentCollision);
    }
}

