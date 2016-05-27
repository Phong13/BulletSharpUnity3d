using System;
using UnityEngine;
using System.Collections;
using BulletSharp;

namespace BulletUnity {
	[AddComponentMenu("Physics Bullet/Shapes/Convex Hull")]
    public class BConvexHullShape : BCollisionShape {
        public Mesh hullMesh;

        //todo draw the hull when not in the world

        public override void OnDrawGizmosSelected() {
            //BUtility.DebugDrawCapsule(position, rotation, scale, radius, height / 2f, 1, Gizmos.color);  
        }


        public override CollisionShape GetCollisionShape() {
            if (collisionShapePtr == null) {

                /*
                btShapeHull* hull = new btShapeHull(originalConvexShape);
                btScalar margin = originalConvexShape->getMargin();
                hull->buildHull(margin);
                btConvexHullShape* simplifiedConvexShape = new btConvexHullShape(hull->getVertexPointer(), hull->numVertices());
                */

                Vector3[] verts = hullMesh.vertices;
                //todo remove duplicate verts
                //todo use vertex reduction utility
                float[] points = new float[verts.Length * 3];
                for (int i = 0; i < verts.Length; i++) {
                    int idx = i * 3;
                    points[idx] = verts[i].x;
                    points[idx + 1] = verts[i].y;
                    points[idx + 2] = verts[i].z;
                }
                collisionShapePtr = new ConvexHullShape(points);

                // some of the verts are coming out as 1.23 * e 34 Something is wrong.
                /*
                ConvexHullShape chs = (ConvexHullShape)collisionShapePtr;
                string str = "";
                for (int i = 0; i < chs.NumPoints; i++  )
                {
                    str += chs.GetScaledPoint(i).ToString() + ", ";
                }
                Debug.Log(str);
                */
            }
            return collisionShapePtr;
        }
    }
}
