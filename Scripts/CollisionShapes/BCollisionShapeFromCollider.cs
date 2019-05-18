using BulletSharp;
using UnityEngine;

namespace BulletUnity
{
    [AddComponentMenu("Physics Bullet/Shapes/FromCollider")]
    public class BCollisionShapeFromCollider : BCollisionShape
    {


        public static CollisionShape GetCollisionShapeFromCollider(Collider collider)
        {
            MeshCollider meshCollider = collider as MeshCollider;
            if (meshCollider != null)
            {
                Vector3[] vertices = meshCollider.sharedMesh.vertices;
                if (meshCollider.convex)
                {
                    float[] points = new float[vertices.Length * 3];
                    for (int i = 0; i < vertices.Length; ++i)
                    {
                        int idx = i * 3;
                        points[idx] = vertices[i].x;
                        points[idx + 1] = vertices[i].y;
                        points[idx + 2] = vertices[i].z;
                    }
                    ConvexHullShape cs = new ConvexHullShape(points);
                    cs.LocalScaling = meshCollider.transform.lossyScale.ToBullet();
                    cs.Margin = 0f;
                    //TODO
                    // cs.OptimizeConvexHull();
                    return cs;
                }
                else
                {
                    int[] tris = meshCollider.sharedMesh.triangles;
                    TriangleMesh tm = new TriangleMesh();
                    for (int i = 0; i < tris.Length; i += 3)
                    {
                        tm.AddTriangle(vertices[tris[i]].ToBullet(),
                                       vertices[tris[i + 1]].ToBullet(),
                                       vertices[tris[i + 2]].ToBullet(),
                                       true);
                    }
                    BvhTriangleMeshShape ts = new BvhTriangleMeshShape(tm, true);
                    ts.LocalScaling = meshCollider.transform.lossyScale.ToBullet();
                    return ts;
                }
            }
            return null;
        }

        private CollisionShape CreateCollisionShape()
        {
            return GetCollisionShapeFromCollider(this.GetComponent<Collider>());
        }

        public override CollisionShape CopyCollisionShape()
        {
            return CreateCollisionShape();
        }


        public override CollisionShape GetCollisionShape()
        {
            if (collisionShapePtr == null)
            {
                collisionShapePtr = CreateCollisionShape();
            }
            return collisionShapePtr;
        }

        public override void OnDrawGizmosSelected()
        {
        }

    }
}
