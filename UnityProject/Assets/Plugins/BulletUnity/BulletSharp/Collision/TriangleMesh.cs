using System;
using BulletSharp.Math;


namespace BulletSharp
{
	public class TriangleMesh : TriangleIndexVertexArray
	{
		internal TriangleMesh(IntPtr native)
			: base(native)
		{
		}

		public TriangleMesh(bool use32BitIndices = true, bool use4ComponentVertices = true)
			: base(UnsafeNativeMethods.btTriangleMesh_new(use32BitIndices, use4ComponentVertices))
		{
		}

		public void AddIndex(int index)
		{
			UnsafeNativeMethods.btTriangleMesh_addIndex(Native, index);
		}

	   public void AddTriangleRef(ref Vector3 vertex0, ref Vector3 vertex1, ref Vector3 vertex2,
		   bool removeDuplicateVertices = false)
	   {
		   UnsafeNativeMethods.btTriangleMesh_addTriangle(Native, ref vertex0, ref vertex1, ref vertex2,
			   removeDuplicateVertices);
	   }

		public void AddTriangle(Vector3 vertex0, Vector3 vertex1, Vector3 vertex2,
			bool removeDuplicateVertices = false)
		{
			UnsafeNativeMethods.btTriangleMesh_addTriangle(Native, ref vertex0, ref vertex1, ref vertex2,
				removeDuplicateVertices);
		}

		public void AddTriangleIndices(int index1, int index2, int index3)
		{
			UnsafeNativeMethods.btTriangleMesh_addTriangleIndices(Native, index1, index2, index3);
		}

		public int FindOrAddVertexRef(Vector3 vertex, bool removeDuplicateVertices)
		{
			return UnsafeNativeMethods.btTriangleMesh_findOrAddVertex(Native, ref vertex, removeDuplicateVertices);
		}

		public int FindOrAddVertex(Vector3 vertex, bool removeDuplicateVertices)
		{
			return UnsafeNativeMethods.btTriangleMesh_findOrAddVertex(Native, ref vertex, removeDuplicateVertices);
		}

		public int NumTriangles => UnsafeNativeMethods.btTriangleMesh_getNumTriangles(Native);

		public bool Use32BitIndices => UnsafeNativeMethods.btTriangleMesh_getUse32bitIndices(Native);

		public bool Use4ComponentVertices => UnsafeNativeMethods.btTriangleMesh_getUse4componentVertices(Native);

		public float WeldingThreshold
		{
			get => UnsafeNativeMethods.btTriangleMesh_getWeldingThreshold(Native);
			set => UnsafeNativeMethods.btTriangleMesh_setWeldingThreshold(Native, value);
		}
	}
}
