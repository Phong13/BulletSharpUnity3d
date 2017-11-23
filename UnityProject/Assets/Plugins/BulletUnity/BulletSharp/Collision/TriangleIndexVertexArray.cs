using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using BulletSharp.Math;


namespace BulletSharp
{
	public class IndexedMesh : IDisposable
	{
		internal IntPtr Native;
		private bool _preventDelete;
		private bool _ownsData;

		internal IndexedMesh(IntPtr native, bool preventDelete)
		{
			Native = native;
			_preventDelete = preventDelete;
		}

		public IndexedMesh()
		{
			Native = UnsafeNativeMethods.btIndexedMesh_new();
		}

		public void Allocate(int numTriangles, int numVertices, int triangleIndexStride = sizeof(int) * 3, int vertexStride = sizeof(float) * 3)
		{
			if (_ownsData)
			{
				Free();
			}
			else
			{
				_ownsData = true;
			}

			switch (triangleIndexStride)
			{
				case sizeof(byte) * 3:
					IndexType = PhyScalarType.Byte;
					break;
				case sizeof(short) * 3:
					IndexType = PhyScalarType.Int16;
					break;
				case sizeof(int) * 3:
				default:
					IndexType = PhyScalarType.Int32;
					break;
			}
			VertexType = PhyScalarType.Single;

			NumTriangles = numTriangles;
			TriangleIndexBase = Marshal.AllocHGlobal(numTriangles * triangleIndexStride);
			TriangleIndexStride = triangleIndexStride;
			NumVertices = numVertices;
			VertexBase = Marshal.AllocHGlobal(numVertices * vertexStride);
			VertexStride = vertexStride;
		}

		public void Free()
		{
			if (_ownsData)
			{
				Marshal.FreeHGlobal(TriangleIndexBase);
				Marshal.FreeHGlobal(VertexBase);
				_ownsData = false;
			}
		}

		public unsafe UnmanagedMemoryStream GetTriangleStream()
		{
			int length = NumTriangles * TriangleIndexStride;
			return new UnmanagedMemoryStream((byte*)TriangleIndexBase.ToPointer(), length, length, FileAccess.ReadWrite);
		}

		public unsafe UnmanagedMemoryStream GetVertexStream()
		{
			int length = NumVertices * VertexStride;
			return new UnmanagedMemoryStream((byte*)UnsafeNativeMethods.btIndexedMesh_getVertexBase(Native).ToPointer(), length, length, FileAccess.ReadWrite);
		}

		public void SetData(ICollection<int> triangles, ICollection<float> vertices)
		{
			SetTriangles(triangles);

			float[] vertexArray = vertices as float[];
			if (vertexArray == null)
			{
				vertexArray = new float[vertices.Count];
				vertices.CopyTo(vertexArray, 0);
			}
			Marshal.Copy(vertexArray, 0, VertexBase, vertices.Count);
		}

		public void SetData(ICollection<int> triangles, ICollection<Vector3> vertices)
		{
			SetTriangles(triangles);

			float[] vertexArray = new float[vertices.Count * 3];
			int i = 0;
			foreach (Vector3 v in vertices)
			{
				vertexArray[i] = v.X;
				vertexArray[i + 1] = v.Y;
				vertexArray[i + 2] = v.Z;
				i += 3;
			}
			Marshal.Copy(vertexArray, 0, VertexBase, vertexArray.Length);
		}

		private void SetTriangles(ICollection<int> triangles)
		{
			int[] triangleArray = triangles as int[];
			if (triangleArray == null)
			{
				triangleArray = new int[triangles.Count];
				triangles.CopyTo(triangleArray, 0);
			}
			Marshal.Copy(triangleArray, 0, TriangleIndexBase, triangleArray.Length);
		}

		public PhyScalarType IndexType
		{
			get => UnsafeNativeMethods.btIndexedMesh_getIndexType(Native);
			set => UnsafeNativeMethods.btIndexedMesh_setIndexType(Native, value);
		}

		public int NumTriangles
		{
			get => UnsafeNativeMethods.btIndexedMesh_getNumTriangles(Native);
			set => UnsafeNativeMethods.btIndexedMesh_setNumTriangles(Native, value);
		}

		public int NumVertices
		{
			get => UnsafeNativeMethods.btIndexedMesh_getNumVertices(Native);
			set => UnsafeNativeMethods.btIndexedMesh_setNumVertices(Native, value);
		}

		public IntPtr TriangleIndexBase
		{
			get => UnsafeNativeMethods.btIndexedMesh_getTriangleIndexBase(Native);
			set => UnsafeNativeMethods.btIndexedMesh_setTriangleIndexBase(Native, value);
		}

		public int TriangleIndexStride
		{
			get => UnsafeNativeMethods.btIndexedMesh_getTriangleIndexStride(Native);
			set => UnsafeNativeMethods.btIndexedMesh_setTriangleIndexStride(Native, value);
		}

		public IntPtr VertexBase
		{
			get => UnsafeNativeMethods.btIndexedMesh_getVertexBase(Native);
			set => UnsafeNativeMethods.btIndexedMesh_setVertexBase(Native, value);
		}

		public int VertexStride
		{
			get => UnsafeNativeMethods.btIndexedMesh_getVertexStride(Native);
			set => UnsafeNativeMethods.btIndexedMesh_setVertexStride(Native, value);
		}

		public PhyScalarType VertexType
		{
			get => UnsafeNativeMethods.btIndexedMesh_getVertexType(Native);
			set => UnsafeNativeMethods.btIndexedMesh_setVertexType(Native, value);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				Free();
				if (!_preventDelete)
				{
					UnsafeNativeMethods.btIndexedMesh_delete(Native);
				}
				Native = IntPtr.Zero;
			}
		}

		~IndexedMesh()
		{
			Dispose(false);
		}
	}

	public class TriangleIndexVertexArray : StridingMeshInterface
	{
		private List<IndexedMesh> _meshes = new List<IndexedMesh>();
		private IndexedMesh _initialMesh;
		private AlignedIndexedMeshArray _indexedMeshArray;

		internal TriangleIndexVertexArray(IntPtr native)
			: base(native)
		{
		}

		public TriangleIndexVertexArray()
			: base(UnsafeNativeMethods.btTriangleIndexVertexArray_new())
		{
		}

		public TriangleIndexVertexArray(ICollection<int> triangles, ICollection<float> vertices)
			: base(UnsafeNativeMethods.btTriangleIndexVertexArray_new())
		{
			_initialMesh = new IndexedMesh();
			_initialMesh.Allocate(triangles.Count / 3, vertices.Count / 3);
			_initialMesh.SetData(triangles, vertices);
			AddIndexedMesh(_initialMesh);
		}

		public TriangleIndexVertexArray(ICollection<int> triangles, ICollection<Vector3> vertices)
			: base(UnsafeNativeMethods.btTriangleIndexVertexArray_new())
		{
			_initialMesh = new IndexedMesh();
			_initialMesh.Allocate(triangles.Count / 3, vertices.Count);
			_initialMesh.SetData(triangles, vertices);
			AddIndexedMesh(_initialMesh);
		}

		public TriangleIndexVertexArray(int numTriangles, IntPtr triangleIndexBase, int triangleIndexStride, int numVertices, IntPtr vertexBase, int vertexStride)
			: base(UnsafeNativeMethods.btTriangleIndexVertexArray_new2(numTriangles, triangleIndexBase, triangleIndexStride, numVertices, vertexBase, vertexStride))
		{
		}

		public void AddIndexedMesh(IndexedMesh mesh, PhyScalarType indexType = PhyScalarType.Int32)
		{
			_meshes.Add(mesh);
			UnsafeNativeMethods.btTriangleIndexVertexArray_addIndexedMesh(Native, mesh.Native, indexType);
		}

		public AlignedIndexedMeshArray IndexedMeshArray
		{
			get
			{
				// TODO: link _indexedMeshArray to _meshes
				if (_indexedMeshArray == null)
				{
					_indexedMeshArray = new AlignedIndexedMeshArray(UnsafeNativeMethods.btTriangleIndexVertexArray_getIndexedMeshArray(Native));
				}
				return _indexedMeshArray;
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_initialMesh != null)
				{
					_initialMesh.Dispose();
					_initialMesh = null;
				}
			}
			base.Dispose(disposing);
		}
	}
}
