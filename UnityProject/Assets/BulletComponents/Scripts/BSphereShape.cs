using System;
using UnityEngine;
using System.Collections;
using BulletSharp;

public class BSphereShape : BCollisionShape {
    public float radius = 1f;

    public override void OnDrawGizmosSelected()
    {
        UnityEngine.Vector3 position = transform.position;
        UnityEngine.Quaternion rotation = transform.rotation;
        UnityEngine.Vector3 scale = Vector3.one;
        BUtility.DebugDrawSphere(position, rotation, scale, Vector3.one * radius, Color.yellow);
    }

    public override CollisionShape GetCollisionShape()
    {
        if (collisionShapePtr == null)
        {
            collisionShapePtr = new SphereShape(radius);
        }
        return collisionShapePtr;
    }
}
