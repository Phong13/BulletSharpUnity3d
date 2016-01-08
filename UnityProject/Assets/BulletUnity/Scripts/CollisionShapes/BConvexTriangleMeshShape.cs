using System;
using UnityEngine;
using System.Collections;
using BulletSharp;

namespace BulletUnity {
    public class BConvexTriangleMeshShape : BCollisionShape {
        public Mesh hullMesh;

        //todo draw the hull when not in the world
        //todo can this be used with Dynamic objects? The manual hints that it is for static only.

        public override void OnDrawGizmosSelected() {
            //BUtility.DebugDrawCapsule(position, rotation, scale, radius, height / 2f, 1, Gizmos.color);  
        }
        
        public override CollisionShape GetCollisionShape() {
            if (collisionShapePtr == null) {
                Vector3[] verts = hullMesh.vertices;
                int[] tris = hullMesh.triangles;
                //todo test for convex. Make convex if not.
                TriangleMesh tm = new TriangleMesh();
                for (int i = 0; i < tris.Length; i += 3) {
                    tm.AddTriangle(verts[tris[i]].ToBullet(),
                                   verts[tris[i + 1]].ToBullet(),
                                   verts[tris[i + 2]].ToBullet(),
                                   true);
                }
                collisionShapePtr = new ConvexTriangleMeshShape(tm);
            }
            return collisionShapePtr;
        }
    }
}
