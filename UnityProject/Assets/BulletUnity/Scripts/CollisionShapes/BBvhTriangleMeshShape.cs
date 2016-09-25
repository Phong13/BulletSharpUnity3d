using System;
using UnityEngine;
using System.Collections;
using BulletSharp;

namespace BulletUnity {
    public class BBvhTriangleMeshShape : BCollisionShape {
        [SerializeField]
        protected Mesh hullMesh;
        public Mesh HullMesh
        {
            get { return hullMesh; }
            set
            {
                if (collisionShapePtr != null && value != hullMesh)
                {
                    Debug.LogError("Cannot change the Hull Mesh after the bullet shape has been created. This is only the initial value " +
                                    "Use LocalScaling to change the shape of a bullet shape.");
                }
                else {
                    hullMesh = value;
                }
            }
        }

        [SerializeField]
        protected Vector3 m_localScaling = Vector3.one;
        public Vector3 LocalScaling
        {
            get { return m_localScaling; }
            set
            {
                m_localScaling = value;
                if (collisionShapePtr != null)
                {
                    ((BvhTriangleMeshShape)collisionShapePtr).LocalScaling = value.ToBullet();
                }
            }
        }

        //todo draw the hull when not in the world
        //todo can this be used with Dynamic objects? The manual hints that it is for static only.

        public override void OnDrawGizmosSelected() {
            //BUtility.DebugDrawCapsule(position, rotation, scale, radius, height / 2f, 1, Gizmos.color);  
        }

        BvhTriangleMeshShape _CreateBvhTriangleMeshShape()
        {
            Vector3[] verts = hullMesh.vertices;
            int[] tris = hullMesh.triangles;
            //todo test for convex. Make convex if not.
            TriangleMesh tm = new TriangleMesh();
            for (int i = 0; i < tris.Length; i += 3)
            {
                tm.AddTriangle(verts[tris[i]].ToBullet(),
                               verts[tris[i + 1]].ToBullet(),
                               verts[tris[i + 2]].ToBullet(),
                               true);
            }
            BvhTriangleMeshShape ms = new BvhTriangleMeshShape(tm, true);
            ms.LocalScaling = m_localScaling.ToBullet();
            return ms;
        }

        public override CollisionShape CopyCollisionShape() {
            return _CreateBvhTriangleMeshShape();
        }

        public override CollisionShape GetCollisionShape() {
            if (collisionShapePtr == null) {
                collisionShapePtr = _CreateBvhTriangleMeshShape();
            }
            return collisionShapePtr;
        }
    }
}
