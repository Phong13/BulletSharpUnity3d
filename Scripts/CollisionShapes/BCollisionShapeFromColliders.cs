using BulletSharp;
using System.Collections.Generic;
using UnityEngine;

namespace BulletUnity
{
    [AddComponentMenu("Physics Bullet/Shapes/FromColliders")]
    public class BCollisionShapeFromColliders : BCompoundShape
    {
        CollisionShapeWithTransform[] _collisionShapes;
        CollisionShapeWithTransform[] CollisionShapes
        {
            get
            {
                if (_collisionShapes == null)
                {
                    _collisionShapes = GetCollisionShapesList().ToArray();
                }
                return _collisionShapes;
            }
        }

        private List<CollisionShapeWithTransform> GetCollisionShapesList()
        {
            List<CollisionShapeWithTransform> collisionShapesList = new List<CollisionShapeWithTransform>();
            foreach (Collider collider in GetComponentsInChildren<Collider>())
            {
                CollisionShape collisionShape = BCollisionShapeFromCollider.GetCollisionShapeFromCollider(collider);
                if (collisionShape != null)
                    collisionShapesList.Add(new CollisionShapeWithTransform(collisionShape, collider.transform));
            }

            return collisionShapesList;
        }

        public override void OnDrawGizmosSelected()
        {
            if (!drawGizmo)
                return;
            Gizmos.color = Color.yellow;
            CompoundShape compoundShape = GetCollisionShape() as CompoundShape;

            for (int i = 0; i < compoundShape.NumChildShapes; i++)
            {
                CollisionShape collisionShape = compoundShape.GetChildShape(i);
                ConvexHullShape convexShape = collisionShape as ConvexHullShape;
                if (convexShape != null)
                {
                    BulletSharp.Math.Matrix childShapeTransform = compoundShape.GetChildTransform(i);
                    //childShapeTransform.Invert();
                    BulletSharp.Math.Matrix shapeTransform = childShapeTransform * this.transform.localToWorldMatrix.ToBullet();
                    Gizmos.matrix = shapeTransform.ToUnity();
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
                    /*Mesh collisionMesh = new Mesh();
                    Vector3[] newVertices = new Vector3[convexShape.NumVertices];
                    int[] triangles = new int[convexShape.NumVertices * 3];
                    for (int j = 0; j < convexShape.NumVertices; j++)
                    {
                        BulletSharp.Math.Vector3 vertex1;
                        convexShape.GetVertex(j, out vertex1);
                        newVertices[j] = vertex1.ToUnity();
                        triangles[j] = j;
                    }
                    collisionMesh.vertices = newVertices;
                    collisionMesh.triangles = triangles;
                    collisionMesh.RecalculateNormals();
                    Gizmos.color = Color.blue;
                    Gizmos.DrawMesh(collisionMesh); */
                }
                BvhTriangleMeshShape triangleShape = collisionShape as BvhTriangleMeshShape;
                if (triangleShape != null)
                {
                    BulletSharp.Math.Matrix shapeTransform = this.transform.localToWorldMatrix.ToBullet() * compoundShape.GetChildTransform(i);
                    Gizmos.matrix = BSExtensionMethods2.ToUnity(shapeTransform);
                    /*int nbEdges = triangleShape.;
                     for (int j = 0; j < nbEdges; j++)
                     {
                         BulletSharp.Math.Vector3 vertex1;
                         BulletSharp.Math.Vector3 vertex2;
                         triangleShape.GetEdge(j, out vertex1, out vertex2);
                         Vector3 vertexUnity1 = BSExtensionMethods2.ToUnity(vertex1);
                         Vector3 vertexUnity2 = BSExtensionMethods2.ToUnity(vertex2);
                         Gizmos.DrawLine(vertexUnity1, vertexUnity2);
                     }*/
                }
            }
        }

        protected override CollisionShapeWithTransform[] GetSubCollisionShapes(bool copy)
        {
            if (copy)
            {
                return GetCollisionShapesList().ToArray();
            }
            else
                return CollisionShapes;
        }
    }
}
