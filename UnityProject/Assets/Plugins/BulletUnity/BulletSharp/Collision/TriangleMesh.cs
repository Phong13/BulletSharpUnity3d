using System;
using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class TriangleMesh : TriangleIndexVertexArray
	{
		internal TriangleMesh(IntPtr native)
			: base(native)
		{
		}

		public TriangleMesh(bool use32BitIndices = true, bool use4ComponentVertices = true)
			: base(btTriangleMesh_new(use32BitIndices, use4ComponentVertices))
		{
		}

		public void AddIndex(int index)
		{
			btTriangleMesh_addIndex(Native, index);
		}

	   public void AddTriangleRef(ref Vector3 vertex0, ref Vector3 vertex1, ref Vector3 vertex2,
		   bool removeDuplicateVertices = false)
	   {
		   btTriangleMesh_addTriangle(Native, ref vertex0, ref vertex1, ref vertex2,
			   removeDuplicateVertices);
	   }

		public void AddTriangle(Vector3 vertex0, Vector3 vertex1, Vector3 vertex2,
			bool removeDuplicateVertices = false)
		{
			btTriangleMesh_addTriangle(Native, ref vertex0, ref vertex1, ref vertex2,
				removeDuplicateVertices);
		}

		public void AddTriangleIndices(int index1, int index2, int index3)
		{
			btTriangleMesh_addTriangleIndices(Native, index1, index2, index3);
		}

		public int FindOrAddVertexRef(Vector3 vertex, bool removeDuplicateVertices)
		{
			return btTriangleMesh_findOrAddVertex(Native, ref vertex, removeDuplicateVertices);
		}

		public int FindOrAddVertex(Vector3 vertex, bool removeDuplicateVertices)
		{
			return btTriangleMesh_findOrAddVertex(Native, ref vertex, removeDuplicateVertices);
		}

		public int NumTriangles => btTriangleMesh_getNumTriangles(Native);

		public bool Use32BitIndices => btTriangleMesh_getUse32bitIndices(Native);

		public bool Use4ComponentVertices => btTriangleMesh_getUse4componentVertices(Native);

		public float WeldingThreshold
		{
			get => btTriangleMesh_getWeldingThreshold(Native);
			set => btTriangleMesh_setWeldingThreshold(Native, value);
		}
	}
}
