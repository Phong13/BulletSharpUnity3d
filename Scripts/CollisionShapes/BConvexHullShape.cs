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

        ConvexHullShape _CreateConvexHullShape()
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
            ConvexHullShape cs = new ConvexHullShape(points);
            cs.LocalScaling = m_localScaling.ToBullet();
            return cs;
        }

        public override CollisionShape CopyCollisionShape()
        {
            return _CreateConvexHullShape();
        }

        public override CollisionShape GetCollisionShape() {
            if (collisionShapePtr == null) {
                collisionShapePtr = _CreateConvexHullShape();
            }
            return collisionShapePtr;
        }
    }
}
