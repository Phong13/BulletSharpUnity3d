using System;
using UnityEngine;
using System.Collections;
using BulletSharp;

public class BBoxShape : BCollisionShape {
    public Vector3 extents = Vector3.one;

    public override CollisionShape GetCollisionShape()
    {
        if (collisionShapePtr == null)
        {
            collisionShapePtr = new BoxShape(extents.ToBullet());
        }
        return collisionShapePtr;
    }
}
