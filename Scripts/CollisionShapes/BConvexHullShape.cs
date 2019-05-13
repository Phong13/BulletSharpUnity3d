using BulletSharp;
using UnityEngine;

namespace BulletUnity
{
    [AddComponentMenu("Physics Bullet/Shapes/Convex Hull")]
    public class BConvexHullShape : BCollisionShape
    {
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
                else
                {
                    hullMesh = value;
                }
            }
        }

        //todo draw the hull when not in the world
        public override void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            //            Gizmos.matrix = 
            ConvexHullShape cs = GetCollisionShape() as ConvexHullShape;
            Matrix4x4 matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
            Gizmos.matrix = matrix;
            int nbEdges = cs.NumEdges;
            for (int i = 0; i < nbEdges; i++)
            {
                BulletSharp.Math.Vector3 vertex1;
                BulletSharp.Math.Vector3 vertex2;
                cs.GetEdge(i, out vertex1, out vertex2);
                Vector3 vertexUnity1 = BSExtensionMethods2.ToUnity(vertex1);
                Vector3 vertexUnity2 = BSExtensionMethods2.ToUnity(vertex2);
                Gizmos.DrawLine(vertexUnity1, vertexUnity2);
            }
            //Gizmos.DrawWireMesh(hullMesh, transform.position, transform.rotation, transform.lossyScale);
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
            cs.Margin = m_Margin;
            return cs;
        }

        public override CollisionShape CopyCollisionShape()
        {
            return _CreateConvexHullShape();
        }

        public override CollisionShape GetCollisionShape()
        {
            if (collisionShapePtr == null)
            {
                collisionShapePtr = _CreateConvexHullShape();
            }
            return collisionShapePtr;
        }
    }
}
