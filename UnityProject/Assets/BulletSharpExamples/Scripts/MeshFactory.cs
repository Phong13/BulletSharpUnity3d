using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using BulletSharp;
using BulletSharp.Math;
using BulletUnity;
//using BulletSharp.SoftBody;

namespace DemoFramework
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PositionColored
    {
        public static readonly int Stride = Vector3.SizeInBytes + sizeof(int);

        public Vector3 Position;
        public int Color;

        public PositionColored(Vector3 pos, int col)
        {
            Position = pos;
            Color = col;
        }

        public PositionColored(ref Vector3 pos, int col)
        {
            Position = pos;
            Color = col;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PositionedNormal
    {
        public Vector3 Position;
        public Vector3 Normal;

        public PositionedNormal(Vector3 pos, Vector3 normal)
        {
            Position = pos;
            Normal = normal;
        }

        public PositionedNormal(ref Vector3 pos, ref Vector3 normal)
        {
            Position = pos;
            Normal = normal;
        }
    }

    // Creates platform-agnostic vertex buffers of physical shapes
    // (boxes, cones, cylinders, spheres) for drawing. Includes normals.
    public class MeshFactory
    {
        public void RemoveShape(CollisionShape shape) {
            UnityEngine.Debug.LogError("not implemented");
        }

        public static UnityEngine.Vector3[] CreateShape(CollisionShape shape, out int[] indices)
        {
            switch (shape.ShapeType)
            {
                case BroadphaseNativeType.BoxShape:
                    indices = null;
                    return CreateBox(shape as BoxShape);
                case BroadphaseNativeType.Box2DShape:
                    indices = null;
                    return CreateBox2DShape(shape as Box2DShape);
                case BroadphaseNativeType.CapsuleShape:
                    return CreateCapsule(shape as CapsuleShape, out indices);
                case BroadphaseNativeType.Convex2DShape:
                    return CreateShape((shape as Convex2DShape).ChildShape, out indices);
                case BroadphaseNativeType.ConvexHullShape:
                    indices = null;
                    return CreateConvexHull(shape as ConvexHullShape);
                case BroadphaseNativeType.ConeShape:
                    return CreateCone(shape as ConeShape, out indices);
                case BroadphaseNativeType.CylinderShape:
                    return CreateCylinder(shape as CylinderShape, out indices);
                case BroadphaseNativeType.GImpactShape:
                    indices = null;
                    return CreateTriangleMesh((shape as GImpactMeshShape).MeshInterface);
                case BroadphaseNativeType.MultiSphereShape:
                    return CreateMultiSphere(shape as MultiSphereShape, out indices);
                case BroadphaseNativeType.SphereShape:
                    return CreateSphere(shape as SphereShape, out indices);
                case BroadphaseNativeType.StaticPlaneShape:
                    return CreateStaticPlane(shape as StaticPlaneShape, out indices);
                case BroadphaseNativeType.TriangleMeshShape:
                    indices = null;
                    return CreateTriangleMesh((shape as TriangleMeshShape).MeshInterface);
            }
            if (shape is PolyhedralConvexShape)
            {
                return CreatePolyhedralConvexShape((shape as PolyhedralConvexShape), out indices);
            }
            throw new NotImplementedException();
        }

        public static ushort[] CompactIndexBuffer(uint[] indices)
        {
            if (indices.Length > 65535)
            {
                return null;
            }
            ushort[] ib = new ushort[indices.Length];
            for (int i = 0; i < ib.Length; i++)
            {
                ib[i] = (ushort)indices[i];
            }
            return ib;
        }
        
        public static UnityEngine.Vector3[] CreateBox(BoxShape shape)
        {
            return null;
        }

        private static UnityEngine.Vector3[] CreateBox2DShape(Box2DShape box2DShape)
        {
            Vector3Array v = box2DShape.Vertices;
            return new UnityEngine.Vector3[12]
            {
                v[0].ToUnity(), UnityEngine.Vector3.forward,
                v[1].ToUnity(), UnityEngine.Vector3.forward,
                v[2].ToUnity(), UnityEngine.Vector3.forward,
                v[0].ToUnity(), UnityEngine.Vector3.forward,
                v[2].ToUnity(), UnityEngine.Vector3.forward,
                v[3].ToUnity(), UnityEngine.Vector3.forward,
            };
        }

        public static UnityEngine.Vector3[] CreateCapsule(CapsuleShape shape, out int[] indices)
        {
            int up = shape.UpAxis;
            float radius = shape.Radius;
            float cylinderHalfHeight = shape.HalfHeight;

            int slices = (int)(radius * 10.0f);
            int stacks = (int)(radius * 10.0f);
            slices = (slices > 16) ? 16 : (slices < 3) ? 3 : slices;
            stacks = (stacks > 16) ? 16 : (stacks < 3) ? 3 : stacks;

            float hAngleStep = (float)Math.PI * 2 / slices;
            float vAngleStep = (float)Math.PI / stacks;

            int vertexCount = 2 + slices * (stacks - 1);
            int indexCount = 6 * slices * (stacks - 1);

            UnityEngine.Vector3[] vertices = new UnityEngine.Vector3[vertexCount * 2];
            indices = new int[indexCount];

            int i = 0, v = 0;


            // Vertices
            // Top and bottom
            vertices[v++] = GetVectorByAxis(0, -cylinderHalfHeight - radius, 0, up);
            vertices[v++] = GetVectorByAxis(-UnityEngine.Vector3.up, up);
            vertices[v++] = GetVectorByAxis(0, cylinderHalfHeight + radius, 0, up);
            vertices[v++] = GetVectorByAxis(UnityEngine.Vector3.up, up);

            // Stacks
            int j, k;
            float angle = 0;
            float vAngle = -(float)Math.PI / 2;
            UnityEngine.Vector3 vTemp;
            UnityEngine.Vector3 cylinderOffset = GetVectorByAxis(0, -cylinderHalfHeight, 0, up);
            for (j = 0; j < stacks - 1; j++)
            {
                float prevAngle = vAngle;
                vAngle += vAngleStep;

                if (vAngle > 0 && prevAngle < 0)
                {
                    cylinderOffset = GetVectorByAxis(0, cylinderHalfHeight, 0, up);
                }

                for (k = 0; k < slices; k++)
                {
                    angle += hAngleStep;

                    vTemp = GetVectorByAxis((float)Math.Cos(vAngle) * (float)Math.Sin(angle),
                        (float)Math.Sin(vAngle),
                        (float)Math.Cos(vAngle) * (float)Math.Cos(angle), up);
                    vertices[v++] = vTemp * radius + cylinderOffset;
                    vertices[v++] = UnityEngine.Vector3.Normalize(vTemp);
                }
            }


            // Indices
            // Top cap
            int index = 2;
            for (k = 0; k < slices; k++)
            {
                indices[i++] = index++;
                indices[i++] = 0;
                indices[i++] = index;
            }
            indices[i - 1] = 2;

            // Stacks
            int sliceDiff = slices * 3;
            for (j = 0; j < stacks - 2; j++)
            {
                for (k = 0; k < slices; k++)
                {
                    indices[i] = indices[i - sliceDiff + 2];
                    indices[i + 1] = index++;
                    indices[i + 2] = indices[i - sliceDiff];
                    i += 3;
                }

                for (k = 0; k < slices; k++)
                {
                    indices[i] = indices[i - sliceDiff + 1];
                    indices[i + 1] = indices[i - sliceDiff];
                    indices[i + 2] = indices[i - sliceDiff + 4];
                    i += 3;
                }
                indices[i - 1] = indices[i - sliceDiff];
            }

            // Bottom cap
            index--;
            for (k = 0; k < slices; k++)
            {
                indices[i++] = index--;
                indices[i++] = 1;
                indices[i++] = index;
            }
            indices[i - 1] = indices[i - sliceDiff];

            return vertices;
        }

        public static UnityEngine.Vector3 GetVectorByAxis(UnityEngine.Vector3 vector, int axis)
        {
            switch (axis)
            {
                case 0:
                    return new UnityEngine.Vector3(vector.x, vector.y, vector.z);
                case 1:
                    return vector;
                default:
                    return new UnityEngine.Vector3(vector.x, vector.y, vector.z);
            }
        }

        public static UnityEngine.Vector3 GetVectorByAxis(float x, float y, float z, int axis)
        {
            switch (axis)
            {
                case 0:
                    return new UnityEngine.Vector3(y, z, x);
                case 1:
                    return new UnityEngine.Vector3(x, y, z);
                default:
                    return new UnityEngine.Vector3(z, x, y);
            }
        }

        public static UnityEngine.Vector3[] CreateCone(ConeShape shape, out int[] indices)
        {
            int up = shape.ConeUpIndex;
            float radius = shape.Radius;
            float halfHeight = shape.Height / 2 + shape.Margin;

            const int numSteps = 10;
            const float angleStep = (2 * (float)Math.PI) / numSteps;

            const int vertexCount = 2 + 6 * numSteps;
            const int indexCount = (4 * numSteps + 2) * 3;

            UnityEngine.Vector3[] vertices = new UnityEngine.Vector3[vertexCount * 2];
            indices = new int[indexCount];

            int i = 0, v = 0;
            int index = 0;
            int baseIndex;
            UnityEngine.Vector3 normal;

            // Draw the base
            normal = GetVectorByAxis(-UnityEngine.Vector3.up, up);

            baseIndex = index;
            vertices[v++] = GetVectorByAxis(0, -halfHeight, 0, up);
            vertices[v++] = normal;

            vertices[v++] = GetVectorByAxis(0, -halfHeight, radius, up);
            vertices[v++] = normal;
            index += 2;

            for (int j = 1; j < numSteps; j++)
            {
                float x = radius * (float)Math.Sin(j * angleStep);
                float z = radius * (float)Math.Cos(j * angleStep);

                vertices[v++] = GetVectorByAxis(x, -halfHeight, z, up);
                vertices[v++] = normal;

                indices[i++] = baseIndex;
                indices[i++] = index - 1;
                indices[i++] = index++;
            }
            indices[i++] = baseIndex;
            indices[i++] = index - 1;
            indices[i++] = baseIndex + 1;


            normal = GetVectorByAxis(0, 0, radius, up);
            normal.Normalize();

            baseIndex = index;
            vertices[v++] = GetVectorByAxis(0, halfHeight, 0, up);
            vertices[v++] = normal;

            vertices[v++] = GetVectorByAxis(0, -halfHeight, radius, up);
            vertices[v++] = normal;
            index += 2;

            for (int j = 1; j < numSteps + 1; j++)
            {
                float x = radius * (float)Math.Sin(j * angleStep);
                float z = radius * (float)Math.Cos(j * angleStep);

                normal = GetVectorByAxis(x, 0, z, up);
                normal.Normalize();

                vertices[v++] = GetVectorByAxis(0, halfHeight, 0, up);
                vertices[v++] = normal;

                vertices[v++] = GetVectorByAxis(x, -halfHeight, z, up);
                vertices[v++] = normal;

                indices[i++] = index - 2;
                indices[i++] = index - 1;
                indices[i++] = index;
                indices[i++] = index;
                indices[i++] = index - 1;
                indices[i++] = index + 1;
                index += 2;
            }
            indices[i++] = index - 2;
            indices[i++] = index - 1;
            indices[i++] = baseIndex;
            indices[i++] = baseIndex;
            indices[i++] = index - 1;
            indices[i] = baseIndex + 1;

            return vertices;
        }

        public static UnityEngine.Vector3[] CreateCylinder(CylinderShape shape, out int[] indices)
        {
            int up = shape.UpAxis;
            float radius = shape.Radius;
            float halfHeight = shape.HalfExtentsWithoutMargin[up] + shape.Margin;

            const int numSteps = 10;
            const float angleStep = (2 * (float)Math.PI) / numSteps;

            const int vertexCount = 2 + 6 * numSteps;
            const int indexCount = (4 * numSteps + 2) * 3;

            UnityEngine.Vector3[] vertices = new UnityEngine.Vector3[vertexCount * 2];
            indices = new int[indexCount];

            int i = 0, v = 0;
            int index = 0;
            int baseIndex;
            UnityEngine.Vector3 normal;

            // Draw two sides
            for (int side = 1; side != -3; side -= 2)
            {
                normal = GetVectorByAxis(side * UnityEngine.Vector3.up, up);

                baseIndex = index;
                vertices[v++] = GetVectorByAxis(0, side * halfHeight, 0, up);
                vertices[v++] = normal;

                vertices[v++] = GetVectorByAxis(0, side * halfHeight, radius, up);
                vertices[v++] = normal;
                index += 2;

                for (int j = 1; j < numSteps; j++)
                {
                    float x = radius * (float)Math.Sin(j * angleStep);
                    float z = radius * (float)Math.Cos(j * angleStep);

                    vertices[v++] = GetVectorByAxis(x, side * halfHeight, z, up);
                    vertices[v++] = normal;

                    indices[i++] = baseIndex;
                    if (side == 1)
                    {
                        indices[i++] = index - 1;
                        indices[i++] = index++;
                    }
                    else
                    {
                        indices[i++] = index;
                        indices[i++] = index - 1;
                        index++;
                    }
                }
                indices[i++] = baseIndex;
                if (side == 1)
                {
                    indices[i++] = index - 1;
                    indices[i++] = baseIndex + 1;
                }
                else
                {
                    indices[i++] = baseIndex + 1;
                    indices[i++] = index - 1;
                }
            }


            normal = GetVectorByAxis(0, 0, radius, up);
            normal.Normalize();

            baseIndex = index;
            vertices[v++] = GetVectorByAxis(0, halfHeight, radius, up);
            vertices[v++] = normal;

            vertices[v++] = GetVectorByAxis(0, -halfHeight, radius, up);
            vertices[v++] = normal;
            index += 2;

            for (int j = 1; j < numSteps + 1; j++)
            {
                float x = radius * (float)Math.Sin(j * angleStep);
                float z = radius * (float)Math.Cos(j * angleStep);

                normal = GetVectorByAxis(x, 0, z, up);
                normal.Normalize();

                vertices[v++] = GetVectorByAxis(x, halfHeight, z, up);
                vertices[v++] = normal;

                vertices[v++] = GetVectorByAxis(x, -halfHeight, z, up);
                vertices[v++] = normal;

                indices[i++] = index - 2;
                indices[i++] = index - 1;
                indices[i++] = index;
                indices[i++] = index;
                indices[i++] = index - 1;
                indices[i++] = index + 1;
                index += 2;
            }
            indices[i++] = index - 2;
            indices[i++] = index - 1;
            indices[i++] = baseIndex;
            indices[i++] = baseIndex;
            indices[i++] = index - 1;
            indices[i] = baseIndex + 1;

            return vertices;
        }

        public static UnityEngine.Vector3[] CreateConvexHull(ConvexHullShape shape)
        {
            ShapeHull hull = new ShapeHull(shape);
            hull.BuildHull(shape.Margin);

            int vertexCount = hull.NumIndices;
            UIntArray indices = hull.Indices;
            Vector3Array points = hull.Vertices;

            UnityEngine.Vector3[] vertices = new UnityEngine.Vector3[vertexCount * 2];

            int v = 0, i;
            for (i = 0; i < vertexCount; i += 3)
            {
                UnityEngine.Vector3 v0 = points[(int)indices[i]].ToUnity();
                UnityEngine.Vector3 v1 = points[(int)indices[i + 1]].ToUnity();
                UnityEngine.Vector3 v2 = points[(int)indices[i + 2]].ToUnity();

                UnityEngine.Vector3 v01 = v0 - v1;
                UnityEngine.Vector3 v02 = v0 - v2;
                UnityEngine.Vector3 normal;
                normal = UnityEngine.Vector3.Cross(v01, v02);
                normal.Normalize();

                vertices[v++] = v0;
                vertices[v++] = normal;
                vertices[v++] = v1;
                vertices[v++] = normal;
                vertices[v++] = v2;
                vertices[v++] = normal;
            }

            return vertices;
        }

        public static UnityEngine.Vector3[] CreateMultiSphere(MultiSphereShape shape, out int[] indices)
        {
            List<UnityEngine.Vector3[]> allVertices = new List<UnityEngine.Vector3[]>();
            List<int[]> allIndices = new List<int[]>();
            int vertexCount = 0;
            int indexCount = 0;

            int i;
            for (i = 0; i < shape.SphereCount; i++)
            {
                int[] sphereIndices;
                UnityEngine.Vector3[] sphereVertices = CreateSphere(shape.GetSphereRadius(i), out sphereIndices);

                // Adjust sphere position
                UnityEngine.Vector3 position = shape.GetSpherePosition(i).ToUnity();
                for (int j = 0; j < sphereVertices.Length / 2; j++)
                {
                    sphereVertices[j * 2] += position;
                }

                // Adjust indices
                if (indexCount != 0)
                {
                    int indexOffset = vertexCount / 2;
                    for (int j = 0; j < sphereIndices.Length; j++)
                    {
                        sphereIndices[j] += (int)indexOffset;
                    }
                }

                allVertices.Add(sphereVertices);
                allIndices.Add(sphereIndices);
                vertexCount += sphereVertices.Length;
                indexCount += sphereIndices.Length;
            }

            UnityEngine.Vector3[] finalVertices = new UnityEngine.Vector3[vertexCount];
            int vo = 0;
            foreach (UnityEngine.Vector3[] v in allVertices)
            {
                v.CopyTo(finalVertices, vo);
                vo += v.Length;
            }

            indices = new int[indexCount];
            int io = 0;
            foreach (int[] ind in allIndices)
            {
                ind.CopyTo(indices, io);
                io += ind.Length;
            }

            return finalVertices;
        }

        private static UnityEngine.Vector3[] CreatePolyhedralConvexShape(PolyhedralConvexShape polyhedralConvexShape, out int[] indices)
        {
            int numVertices = polyhedralConvexShape.NumVertices;
            UnityEngine.Vector3[] vertices = new UnityEngine.Vector3[numVertices * 3];
            for (int i = 0; i < numVertices; i += 4)
            {
                Vector3 v0, v1, v2, v3;
                polyhedralConvexShape.GetVertex(i, out v0);
                polyhedralConvexShape.GetVertex(i + 1, out v1);
                polyhedralConvexShape.GetVertex(i + 2, out v2);
                polyhedralConvexShape.GetVertex(i + 3, out v3);

                UnityEngine.Vector3 v01 = (v0 - v1).ToUnity();
                UnityEngine.Vector3 v02 = (v0 - v2).ToUnity();
                UnityEngine.Vector3 normal = UnityEngine.Vector3.Cross(v01, v02);

                int i3 = i * 3;
                vertices[i3] = v0.ToUnity();
                vertices[i3 + 1] = normal;
                vertices[i3 + 2] = v1.ToUnity();
                vertices[i3 + 3] = normal;
                vertices[i3 + 4] = v2.ToUnity();
                vertices[i3 + 5] = normal;
                vertices[i3 + 6] = v0.ToUnity();
                vertices[i3 + 7] = normal;
                vertices[i3 + 8] = v2.ToUnity();
                vertices[i3 + 9] = normal;
                vertices[i3 + 10] = v3.ToUnity();
            }
            indices = null;
            return vertices;
        }

        public static UnityEngine.Vector3[] CreateSphere(SphereShape shape, out int[] indices)
        {
            return CreateSphere(shape.Radius, out indices);
        }

        static UnityEngine.Vector3[] CreateSphere(float radius, out int[] indices)
        {
            int slices = (int)(radius * 10.0f);
            int stacks = (int)(radius * 10.0f);
            slices = (slices > 16) ? 16 : (slices < 3) ? 3 : slices;
            stacks = (stacks > 16) ? 16 : (stacks < 2) ? 2 : stacks;

            float hAngleStep = (float)Math.PI * 2 / slices;
            float vAngleStep = (float)Math.PI / stacks;

            int vertexCount = 2 + slices * (stacks - 1);
            int indexCount = 6 * slices * (stacks - 1);

            UnityEngine.Vector3[] vertices = new UnityEngine.Vector3[vertexCount * 2];
            indices = new int[indexCount];

            int i = 0, v = 0;


            // Vertices
            // Top and bottom
            vertices[v++] = new UnityEngine.Vector3(0, -radius, 0);
            vertices[v++] = -UnityEngine.Vector3.up;
            vertices[v++] = new UnityEngine.Vector3(0, radius, 0);
            vertices[v++] = UnityEngine.Vector3.up;

            // Stacks
            int j, k;
            float angle = 0;
            float vAngle = -(float)Math.PI / 2;
            UnityEngine.Vector3 vTemp;
            for (j = 0; j < stacks - 1; j++)
            {
                vAngle += vAngleStep;

                for (k = 0; k < slices; k++)
                {
                    angle += hAngleStep;

                    vTemp = new UnityEngine.Vector3((float)Math.Cos(vAngle) * (float)Math.Sin(angle), (float)Math.Sin(vAngle), (float)Math.Cos(vAngle) * (float)Math.Cos(angle));
                    vertices[v++] = vTemp * radius;
                    vertices[v++] = UnityEngine.Vector3.Normalize(vTemp);
                }
            }


            // Indices
            // Top cap
            ushort index = 2;
            for (k = 0; k < slices; k++)
            {
                indices[i++] = index++;
                indices[i++] = 0;
                indices[i++] = index;
            }
            indices[i - 1] = 2;

            // Stacks
            //for (j = 0; j < 1; j++)
            int sliceDiff = slices * 3;
            for (j = 0; j < stacks - 2; j++)
            {
                for (k = 0; k < slices; k++)
                {
                    indices[i] = indices[i - sliceDiff + 2];
                    indices[i + 1] = index++;
                    indices[i + 2] = indices[i - sliceDiff];
                    i += 3;
                }

                for (k = 0; k < slices; k++)
                {
                    indices[i] = indices[i - sliceDiff + 1];
                    indices[i + 1] = indices[i - sliceDiff];
                    indices[i + 2] = indices[i - sliceDiff + 4];
                    i += 3;
                }
                indices[i - 1] = indices[i - sliceDiff];
            }

            // Bottom cap
            index--;
            for (k = 0; k < slices; k++)
            {
                indices[i++] = index--;
                indices[i++] = 1;
                indices[i++] = index;
            }
            indices[i - 1] = indices[i - sliceDiff];

            return vertices;
        }

        static void PlaneSpace1(Vector3 n, out Vector3 p, out Vector3 q)
        {
            if (Math.Abs(n[2]) > (Math.Sqrt(2) / 2))
            {
                // choose p in y-z plane
                float a = n[1] * n[1] + n[2] * n[2];
                float k = 1.0f / (float)Math.Sqrt(a);
                p = new Vector3(0, -n[2] * k, n[1] * k);
                // set q = n x p
                q = Vector3.Cross(n, p);
            }
            else
            {
                // choose p in x-y plane
                float a = n[0] * n[0] + n[1] * n[1];
                float k = 1.0f / (float)Math.Sqrt(a);
                p = new Vector3(-n[1] * k, n[0] * k, 0);
                // set q = n x p
                q = Vector3.Cross(n, p);
            }
        }

        public static UnityEngine.Vector3[] CreateStaticPlane(StaticPlaneShape shape, out int[] indices)
        {
            UnityEngine.Vector3 planeOrigin = shape.PlaneNormal.ToUnity() * shape.PlaneConstant;
            Vector3 vec0, vec1;
            PlaneSpace1(shape.PlaneNormal, out vec0, out vec1);
            const float size = 1000;

            indices = new int[] { 0, 2, 1, 0, 1, 3 };

            return new UnityEngine.Vector3[]
            {
                planeOrigin + vec0.ToUnity()*size,
                shape.PlaneNormal.ToUnity(),
                planeOrigin - vec0.ToUnity()*size,
                shape.PlaneNormal.ToUnity(),
                planeOrigin + vec1.ToUnity()*size,
                shape.PlaneNormal.ToUnity(),
                planeOrigin - vec1.ToUnity()*size,
                shape.PlaneNormal.ToUnity()
            };
        }

        static UnityEngine.Vector3[] CreateTriangleMesh(StridingMeshInterface meshInterface)
        {
            // StridingMeshInterface can only be TriangleIndexVertexArray
            var meshes = (meshInterface as TriangleIndexVertexArray).IndexedMeshArray;
            int numTriangles = 0;
            foreach (var mesh in meshes)
            {
                numTriangles += mesh.NumTriangles;
            }
            int numVertices = numTriangles * 3;
            UnityEngine.Vector3[] vertices = new UnityEngine.Vector3[numVertices * 2];

            int v = 0;
            for (int part = 0; part < meshInterface.NumSubParts; part++)
            {
                var mesh = meshes[part];

                var indexStream = mesh.GetTriangleStream();
                var vertexStream = mesh.GetVertexStream();
                var indexReader = new BinaryReader(indexStream);
                var vertexReader = new BinaryReader(vertexStream);

                int vertexStride = mesh.VertexStride;
                int triangleStrideDelta = mesh.TriangleIndexStride - 3 * sizeof(int);

                while (indexStream.Position < indexStream.Length)
                {
                    uint i = indexReader.ReadUInt32();
                    vertexStream.Position = vertexStride * i;
                    float f1 = vertexReader.ReadSingle();
                    float f2 = vertexReader.ReadSingle();
                    float f3 = vertexReader.ReadSingle();
                    UnityEngine.Vector3 v0 = new UnityEngine.Vector3(f1, f2, f3);
                    i = indexReader.ReadUInt32();
                    vertexStream.Position = vertexStride * i;
                    f1 = vertexReader.ReadSingle();
                    f2 = vertexReader.ReadSingle();
                    f3 = vertexReader.ReadSingle();
                    UnityEngine.Vector3 v1 = new UnityEngine.Vector3(f1, f2, f3);
                    i = indexReader.ReadUInt32();
                    vertexStream.Position = vertexStride * i;
                    f1 = vertexReader.ReadSingle();
                    f2 = vertexReader.ReadSingle();
                    f3 = vertexReader.ReadSingle();
                    UnityEngine.Vector3 v2 = new UnityEngine.Vector3(f1, f2, f3);

                    UnityEngine.Vector3 v01 = v0 - v1;
                    UnityEngine.Vector3 v02 = v0 - v2;
                    UnityEngine.Vector3 normal = UnityEngine.Vector3.Cross(v01, v02);
                    normal.Normalize();

                    vertices[v++] = v0;
                    vertices[v++] = normal;
                    vertices[v++] = v1;
                    vertices[v++] = normal;
                    vertices[v++] = v2;
                    vertices[v++] = normal;

                    indexStream.Position += triangleStrideDelta;
                }

                indexStream.Dispose();
                vertexStream.Dispose();
            }

            return vertices;
        }

        /*
        public void UpdateSoftBody(SoftBody softBody, ShapeData shapeData)
        {
            AlignedFaceArray faces = softBody.Faces;

            if (faces.Count != 0)
            {
                shapeData.VertexCount = faces.Count * 3;

                Vector3[] vectors = new Vector3[shapeData.VertexCount * 2];
                int v = 0;

                int i;
                for (i = 0; i < faces.Count; i++)
                {
                    NodePtrArray nodes = faces[i].N;
                    Node n0 = nodes[0];
                    Node n1 = nodes[1];
                    Node n2 = nodes[2];
                    n0.GetX(out vectors[v]);
                    n0.GetNormal(out vectors[v + 1]);
                    n1.GetX(out vectors[v + 2]);
                    n1.GetNormal(out vectors[v + 3]);
                    n2.GetX(out vectors[v + 4]);
                    n2.GetNormal(out vectors[v + 5]);
                    v += 6;
                }

                shapeData.SetDynamicVertexBuffer(device, vectors);
            }
            else
            {
                AlignedTetraArray tetras = softBody.Tetras;
                int tetraCount = tetras.Count;

                if (tetraCount != 0)
                {
                    shapeData.VertexCount = tetraCount * 12;

                    Vector3[] vectors = new Vector3[tetraCount * 24];
                    int v = 0;

                    for (int i = 0; i < tetraCount; i++)
                    {
                        NodePtrArray nodes = tetras[i].Nodes;
                        Vector3 v0 = nodes[0].X;
                        Vector3 v1 = nodes[1].X;
                        Vector3 v2 = nodes[2].X;
                        Vector3 v3 = nodes[3].X;
                        Vector3 v10 = v1 - v0;
                        Vector3 v02 = v0 - v2;

                        Vector3 normal = Vector3.Cross(v10, v02);
                        vectors[v] = v0;
                        vectors[v + 1] = normal;
                        vectors[v + 2] = v1;
                        vectors[v + 3] = normal;
                        vectors[v + 4] = v2;
                        vectors[v + 5] = normal;

                        normal = Vector3.Cross(v10, v3 - v0);
                        vectors[v + 6] = v0;
                        vectors[v + 7] = normal;
                        vectors[v + 8] = v1;
                        vectors[v + 9] = normal;
                        vectors[v + 10] = v3;
                        vectors[v + 11] = normal;

                        normal = Vector3.Cross(v2 - v1, v3 - v1);
                        vectors[v + 12] = v1;
                        vectors[v + 13] = normal;
                        vectors[v + 14] = v2;
                        vectors[v + 15] = normal;
                        vectors[v + 16] = v3;
                        vectors[v + 17] = normal;

                        normal = Vector3.Cross(v02, v3 - v2);
                        vectors[v + 18] = v2;
                        vectors[v + 19] = normal;
                        vectors[v + 20] = v0;
                        vectors[v + 21] = normal;
                        vectors[v + 22] = v3;
                        vectors[v + 23] = normal;
                        v += 24;
                    }

                    shapeData.SetDynamicVertexBuffer(device, vectors);
                }
                else if (softBody.Links.Count != 0)
                {
                    AlignedLinkArray links = softBody.Links;
                    int linkCount = links.Count;
                    shapeData.VertexCount = linkCount * 2;

                    Vector3[] vectors = new Vector3[linkCount * 4];

                    for (int i = 0; i < linkCount; i++)
                    {
                        NodePtrArray nodes = links[i].Nodes;
                        nodes[0].GetX(out vectors[i * 4]);
                        nodes[1].GetX(out vectors[i * 4 + 2]);
                    }

                    shapeData.PrimitiveTopology = PrimitiveTopology.LineList;
                    shapeData.SetDynamicVertexBuffer(device, vectors);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }
        */
        /*
        public void RenderSoftBodyTextured(SoftBody softBody)
        {
            if (!(softBody.UserObject is Array))
                return;

            object[] userObjArr = softBody.UserObject as object[];
            FloatArray vertexBuffer = userObjArr[0] as FloatArray;
            IntArray indexBuffer = userObjArr[1] as IntArray;

            int vertexCount = (vertexBuffer.Count / 8);

            if (vertexCount > 0)
            {
                int faceCount = indexBuffer.Count / 2;

                bool index32 = vertexCount > 65536;

                Mesh mesh = new Mesh(device, faceCount, vertexCount,
                    MeshFlags.SystemMemory | (index32 ? MeshFlags.Use32Bit : 0),
                    VertexFormat.Position | VertexFormat.Normal | VertexFormat.Texture1);

                SlimDX.DataStream indices = mesh.LockIndexBuffer(LockFlags.Discard);
                if (index32)
                {
                    foreach (int i in indexBuffer)
                        indices.Write(i);
                }
                else
                {
                    foreach (int i in indexBuffer)
                        indices.Write((ushort)i);
                }
                mesh.UnlockIndexBuffer();

                SlimDX.DataStream verts = mesh.LockVertexBuffer(LockFlags.Discard);
                foreach (float f in vertexBuffer)
                    verts.Write(f);
                mesh.UnlockVertexBuffer();

                mesh.ComputeNormals();
                mesh.DrawSubset(0);
                mesh.Dispose();
            }
        }
        */
    }
}
