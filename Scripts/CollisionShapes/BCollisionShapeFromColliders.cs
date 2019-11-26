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
                if (collisionShape != null && collider.GetComponent<BCollisionShape>() == null)
                    collisionShapesList.Add(new CollisionShapeWithTransform(collisionShape, collider.transform));
            }
            foreach (BCollisionShape shape in GetComponentsInChildren<BCollisionShape>())
            {
                if (shape != this)
                    collisionShapesList.Add(new CollisionShapeWithTransform(shape.GetCollisionShape(), shape.transform));
            }

            return collisionShapesList;
        }

        public override void OnDrawGizmosSelected()
        {
            if (!drawGizmo)
                return;
            Gizmos.color = Color.yellow;
            CompoundShape compoundShape = GetCollisionShape() as CompoundShape;

            Matrix4x4 parentMatrix = this.transform.localToWorldMatrix * Matrix4x4.Scale(transform.lossyScale).inverse;
            for (int i = 0; i < compoundShape.NumChildShapes; i++)
            {
                CollisionShape collisionShape = compoundShape.GetChildShape(i);
                BulletSharp.Math.Matrix childShapeTransform = compoundShape.GetChildTransform(i);

                //childShapeTransform.Invert();
                Gizmos.matrix = parentMatrix * childShapeTransform.ToUnity();


                ConvexHullShape convexShape = collisionShape as ConvexHullShape;
                if (convexShape != null)
                {
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
                BvhTriangleMeshShape triangleShape = collisionShape as BvhTriangleMeshShape;
                if (triangleShape != null)
                {
                    DisplayTriangleCallback cb = new DisplayTriangleCallback();
                    triangleShape.MeshInterface.InternalProcessAllTriangles(cb, triangleShape.LocalAabbMin, triangleShape.LocalAabbMax);
                }
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
