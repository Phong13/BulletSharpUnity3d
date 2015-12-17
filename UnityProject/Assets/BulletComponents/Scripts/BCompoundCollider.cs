﻿using System;
using UnityEngine;
using System.Collections;
using BulletSharp;

/*
Doesn't check for changes in the transforms on the child objects or the transforms between 
the child colliders and the compound collider. If the child colliders change then 
updateChildTransform must explicitly be called 


todo should handle
    - scaling of itself and children even just to warn
    - children being removed, destroyed
    - children being moved to different possibly invalid locations in hierarchy 
*/
public class BCompoundCollider : BCollisionShape {
    BCollisionShape[] colliders;

    public override void OnDrawGizmosSelected()
    {
        if (colliders != null)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].OnDrawGizmosSelected();
            }
        }
    }

    public override CollisionShape GetCollisionShape()
    {
        if (collisionShapePtr == null)
        {
            BCollisionShape[] css = GetComponentsInChildren<BCollisionShape>();
            colliders = new BCollisionShape[css.Length - 1];
            int ii = 0;
            for (int i = 0; i < css.Length; i++)
            {
                if (css[i] == this)
                {
                    //skip
                } else
                {
                    colliders[ii] = css[i];
                    ii++;
                }
            }
            if (colliders.Length == 0){
            	Debug.LogError("Compound collider");
            }
            
            //TODO
            // some of the collider types (non-finite and other compound colliders) are probably not
            // allowed should check for these.
            // what about scaling not sure if it is handled correctly
            CompoundShape cs = new CompoundShape();
            collisionShapePtr = cs;
            for (int i = 0; i < colliders.Length; i++)
            {
                CollisionShape chcs = colliders[i].GetCollisionShape();

                Vector3 up = Vector3.up;
                Vector3 origin = Vector3.zero;
                Vector3 forward = Vector3.forward;
                //to world
                up = colliders[i].transform.TransformPoint(up);
                origin = colliders[i].transform.TransformPoint(origin);
                forward = colliders[i].transform.TransformPoint(forward);
                //to compound collider
                up = transform.InverseTransformPoint(up);
                origin = transform.InverseTransformPoint(origin);
                forward = transform.InverseTransformPoint(forward);
                Quaternion q = Quaternion.LookRotation(forward, up);

                BulletSharp.Math.Matrix m = BulletSharp.Math.Matrix.AffineTransformation(1f,q.ToBullet(),origin.ToBullet());

                cs.AddChildShape(m, chcs);
            }
        }
        return collisionShapePtr;
    }
}