using System;
using BulletSharp;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

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
                try
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
                catch (NullReferenceException e)
                {
                    Debug.LogError("Could not get collision shape from collider. Maybe it's not a mesh collider ?", collider);
                }
            }
            return null;
        }

        private CollisionShape CreateCollisionShape()
        {
            return GetCollisionShapeFromCollider(GetComponent<Collider>());
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
            if (!drawGizmo)
            {
                return;
            }
            Gizmos.color = Color.yellow;
            CollisionShape shape = GetCollisionShape();
            ConvexHullShape convexShape = shape as ConvexHullShape;
            Gizmos.matrix = transform.localToWorldMatrix * Matrix4x4.Scale(transform.lossyScale).inverse;
            if (convexShape != null)
            {
                //BulletSharp.Math.Matrix childShapeTransform = this.transform;
                //childShapeTransform.Invert();
                //BulletSharp.Math.Matrix shapeTransform = childShapeTransform * this.transform.localToWorldMatrix.ToBullet();
                int nbEdges = convexShape.NumEdges;
                for (int j = 0; j < nbEdges; j++)
                {
                    BulletSharp.Math.Vector3 vertex1;
                    BulletSharp.Math.Vector3 vertex2;
                    convexShape.GetEdge(j, out vertex1, out vertex2);
                    Vector3 vertexUnity1 = BSExtensionMethods2.ToUnity(vertex1);
                    Vector3 vertexUnity2 = BSExtensionMethods2.ToUnity(vertex2);
                    Gizmos.DrawLine(vertexUnity1, vertexUnity2);
                }
            }
            BvhTriangleMeshShape triangleShape = shape as BvhTriangleMeshShape;
            if (triangleShape != null)
            {
                DisplayTriangleCallback cb = new DisplayTriangleCallback();
                triangleShape.MeshInterface.InternalProcessAllTriangles(cb, triangleShape.LocalAabbMin, triangleShape.LocalAabbMax);
            }
        }

        class DisplayTriangleCallback : InternalTriangleIndexCallback
        {
            public DisplayTriangleCallback()
            { }

            public override void InternalProcessTriangleIndex(ref BulletSharp.Math.Vector3 point0, ref BulletSharp.Math.Vector3 point1, ref BulletSharp.Math.Vector3 point2, int partId, int triangleIndex)
            {
                Vector3 point0Unity = BSExtensionMethods2.ToUnity(point0);
                Vector3 point1Unity = BSExtensionMethods2.ToUnity(point1);
                Vector3 point2Unity = BSExtensionMethods2.ToUnity(point2);
                Gizmos.DrawLine(point0Unity, point1Unity);
                Gizmos.DrawLine(point1Unity, point2Unity);
                Gizmos.DrawLine(point2Unity, point0Unity);
            }

            /*   public override void InternalProcessTriangleIndex(ref BulletSharp.Math.Vector3 point0, ref BulletSharp.Math.Vector3 point1, ref BulletSharp.Math.Vector3 point2, int partId, int triangleIndex)
               {
                   throw new System.NotImplementedException();
               }*/
        }
    }
}

