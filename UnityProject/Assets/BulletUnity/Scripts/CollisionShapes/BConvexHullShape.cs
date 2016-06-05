using System;
using UnityEngine;
using System.Collections;
using BulletSharp;

namespace BulletUnity {
	[AddComponentMenu("Physics Bullet/Shapes/Convex Hull")]
    public class BConvexHullShape : BCollisionShape {
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
                    ((ConvexHullShape) collisionShapePtr).LocalScaling = value.ToBullet();
                }
            }
        }

        //todo draw the hull when not in the world
        public override void OnDrawGizmosSelected() {
              
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
                ((ConvexHullShape)collisionShapePtr).LocalScaling = m_localScaling.ToBullet();
            }
            return collisionShapePtr;
        }
    }
}
