using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Runtime.InteropServices;

public class LoadMeshFromSTL
{
    [StructLayout(LayoutKind.Sequential)]
    unsafe struct MySTLTriangle
    {
        public fixed float normal[3];
        public fixed float vertex0[3];
        public fixed float vertex1[3];
        public fixed float vertex2[3];
    };

    public static object GetObjectFromBytes(byte[] buffer, Type objType)
    {
        object obj = null;
        if ((buffer != null) && (buffer.Length > 0))
        {
            IntPtr ptrObj = IntPtr.Zero;
            try
            {
                int objSize = Marshal.SizeOf(objType);
                if (objSize > 0)
                {
                    if (buffer.Length < objSize)
                        throw new Exception(String.Format("Buffer smaller than needed for creation of object of type {0}", objType));
                    ptrObj = Marshal.AllocHGlobal(objSize);
                    if (ptrObj != IntPtr.Zero)
                    {
                        Marshal.Copy(buffer, 0, ptrObj, objSize);
                        obj = Marshal.PtrToStructure(ptrObj, objType);
                    }
                    else
                        throw new Exception(String.Format("Couldn't allocate memory to create object of type {0}", objType));
                }
            }
            finally
            {
                if (ptrObj != IntPtr.Zero)
                    Marshal.FreeHGlobal(ptrObj);
            }
        }
        return obj;
    }

    unsafe public static Mesh LoadMesh(string relativeFileName, BulletSharp.Math.Vector3 scale)
    {

        Mesh shape = null;
        byte[] memoryBuffer = File.ReadAllBytes(relativeFileName);
        int numTriangles = (int)BitConverter.ToInt32(memoryBuffer, 80);

        if (numTriangles != 0)
        {
            {
                //perform a sanity check instead of crashing on invalid triangles/STL files
                int expectedBinaryFileSize = numTriangles * 50 + 84;
                if (expectedBinaryFileSize != memoryBuffer.Length)
                {

                    return null;
                }

            }
            List<int> shapeIndices = new List<int>();
            List<Vector3> shapeVerts = new List<Vector3>();
            List<Vector3> shapeNorms = new List<Vector3>();
            shape = new Mesh();
            //						b3AlignedObjectArray<GLInstanceVertex>*	m_vertices;
            //						int				m_numvertices;
            //						b3AlignedObjectArray<int>* 		m_indices;
            //						int				m_numIndices;
            //						float			m_scaling[4];

            int index = 0;
            for (int i = 0; i < numTriangles; i++)
            {
                int curPtr = 84 + i * 50;
                MySTLTriangle tmp;

                byte[] triBytes = new byte[sizeof(MySTLTriangle)];
                Array.Copy(memoryBuffer, curPtr, triBytes, 0, sizeof(MySTLTriangle));
                tmp = (MySTLTriangle) GetObjectFromBytes(triBytes, typeof(MySTLTriangle));
                

                Vector3 v0, v1, v2;
                Vector3 n0, n1, n2;
                Vector2 v0uv, v1uv, v2uv;
                v0uv = v1uv = v2uv = new Vector2(0.5f,.5f);
                v0 = v1 = v2 = new Vector3();
                n0 = n1 = n2 = new Vector3();
                for (int v = 0; v < 3; v++)
                {
                    v0[v] = tmp.vertex0[v] * scale[v];
                    v1[v] = tmp.vertex1[v] * scale[v];
                    v2[v] = tmp.vertex2[v] * scale[v];
                    n0[v] = tmp.normal[v];
                    n1[v] = tmp.normal[v];
                    n2[v] = tmp.normal[v];
                }

                shapeVerts.Add(v0);
                shapeVerts.Add(v1);
                shapeVerts.Add(v2);

                shapeIndices.Add(index++);
                shapeIndices.Add(index++);
                shapeIndices.Add(index++);
            }
            shape.vertices = shapeVerts.ToArray();
            shape.normals = shapeNorms.ToArray();
            shape.triangles = shapeIndices.ToArray();
            shape.RecalculateNormals();
            shape.RecalculateBounds();
        }
        Debug.Log("vertexCount=" + shape.vertexCount);
        return shape;
    }
}

