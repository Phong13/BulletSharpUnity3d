using System;
using UnityEngine;
using System.Collections;
using BulletSharp;

namespace BulletUnity
{
    public class BConvexHullShape : BCollisionShape
    {
        public Mesh hullMesh;

        //todo draw the hull when not in the world

        public override void OnDrawGizmosSelected()
        {
            //BUtility.DebugDrawCapsule(position, rotation, scale, radius, height / 2f, 1, Gizmos.color);  
        }


        public override CollisionShape GetCollisionShape()
        {
            if (collisionShapePtr == null)
            {
                Vector3[] verts = hullMesh.vertices;
                //todo remove duplicate verts
                //todo use vertex reduction utility
                float[] points = new float[verts.Length * 3];
                for (int i = 0; i < verts.Length; i++)
                {
                    int idx = i * 3;
                    points[idx] = verts[i].x;
                    points[idx + 1] = verts[i].y;
                    points[idx + 2] = verts[i].z;
                }
                collisionShapePtr = new ConvexHullShape(points);
            }
            return collisionShapePtr;
        }
    }
}
