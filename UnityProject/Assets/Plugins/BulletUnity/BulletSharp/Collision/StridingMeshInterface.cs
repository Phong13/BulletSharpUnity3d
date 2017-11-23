using System;
using System.Runtime.InteropServices;
using System.IO;
using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class StridingMeshInterface : IDisposable // abstract
	{
		internal IntPtr Native;

		internal StridingMeshInterface(IntPtr native)
		{
			Native = native;
		}

		public unsafe UnmanagedMemoryStream GetIndexStream(int subpart = 0)
		{
			IntPtr vertexBase, indexBase;
			int numVerts, numFaces;
			PhyScalarType vertsType, indicesType;
			int vertexStride, indexStride;
			btStridingMeshInterface_getLockedReadOnlyVertexIndexBase(Native, out vertexBase, out numVerts, out vertsType, out vertexStride, out indexBase, out indexStride, out numFaces, out indicesType, subpart);

			int length = numFaces * indexStride;
			return new UnmanagedMemoryStream((byte*)indexBase.ToPointer(), length, length, FileAccess.ReadWrite);
		}

		public unsafe UnmanagedMemoryStream GetVertexStream(int subpart = 0)
		{
			IntPtr vertexBase, indexBase;
			int numVerts, numFaces;
			PhyScalarType vertsType, indicesType;
			int vertexStride, indexStride;
			btStridingMeshInterface_getLockedReadOnlyVertexIndexBase(Native, out vertexBase, out numVerts, out vertsType, out vertexStride, out indexBase, out indexStride, out numFaces, out indicesType, subpart);

			int length = numVerts * vertexStride;
			return new UnmanagedMemoryStream((byte*)vertexBase.ToPointer(), length, length, FileAccess.ReadWrite);
		}

		public void CalculateAabbBruteForce(out Vector3 aabbMin, out Vector3 aabbMax)
		{
			btStridingMeshInterface_calculateAabbBruteForce(Native, out aabbMin,
				out aabbMax);
		}

		public int CalculateSerializeBufferSize()
		{
			return btStridingMeshInterface_calculateSerializeBufferSize(Native);
		}

		public void GetLockedReadOnlyVertexIndexBase(out IntPtr vertexBase, out int numVerts,
			out PhyScalarType type, out int vertexStride, out IntPtr indexbase, out int indexStride,
			out int numFaces, out PhyScalarType indicesType, int subpart = 0)
		{
			btStridingMeshInterface_getLockedReadOnlyVertexIndexBase(Native, out vertexBase,
				out numVerts, out type, out vertexStride, out indexbase, out indexStride,
				out numFaces, out indicesType, subpart);
		}

		public void GetLockedVertexIndexBase(out IntPtr vertexBase, out int numVerts,
			out PhyScalarType type, out int vertexStride, out IntPtr indexbase, out int indexStride,
			out int numFaces, out PhyScalarType indicesType, int subpart = 0)
		{
			btStridingMeshInterface_getLockedVertexIndexBase(Native, out vertexBase,
				out numVerts, out type, out vertexStride, out indexbase, out indexStride,
				out numFaces, out indicesType, subpart);
		}

		public void GetPremadeAabb(out Vector3 aabbMin, out Vector3 aabbMax)
		{
			btStridingMeshInterface_getPremadeAabb(Native, out aabbMin, out aabbMax);
		}

		public void InternalProcessAllTriangles(InternalTriangleIndexCallback callback,
			Vector3 aabbMin, Vector3 aabbMax)
		{
			btStridingMeshInterface_InternalProcessAllTriangles(Native, callback._native,
				ref aabbMin, ref aabbMax);
		}

		public void PreallocateIndices(int numIndices)
		{
			btStridingMeshInterface_preallocateIndices(Native, numIndices);
		}

		public void PreallocateVertices(int numVerts)
		{
			btStridingMeshInterface_preallocateVertices(Native, numVerts);
		}

		public string Serialize(IntPtr dataBuffer, Serializer serializer)
		{
			return Marshal.PtrToStringAnsi(btStridingMeshInterface_serialize(Native, dataBuffer, serializer._native));
		}

		public void SetPremadeAabb(ref Vector3 aabbMin, ref Vector3 aabbMax)
		{
			btStridingMeshInterface_setPremadeAabb(Native, ref aabbMin, ref aabbMax);
		}

		public void SetPremadeAabb(Vector3 aabbMin, Vector3 aabbMax)
		{
			btStridingMeshInterface_setPremadeAabb(Native, ref aabbMin, ref aabbMax);
		}

		public void UnLockReadOnlyVertexBase(int subpart)
		{
			btStridingMeshInterface_unLockReadOnlyVertexBase(Native, subpart);
		}

		public void UnLockVertexBase(int subpart)
		{
			btStridingMeshInterface_unLockVertexBase(Native, subpart);
		}

		public bool HasPremadeAabb => btStridingMeshInterface_hasPremadeAabb(Native);

		public int NumSubParts => btStridingMeshInterface_getNumSubParts(Native);

		public Vector3 Scaling
		{
			get
			{
				Vector3 value;
				btStridingMeshInterface_getScaling(Native, out value);
				return value;
			}
            set => btStridingMeshInterface_setScaling(Native, ref value);
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
				btStridingMeshInterface_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~StridingMeshInterface()
		{
			Dispose(false);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct MeshPartData
	{
		public IntPtr Vertices3F;
		public IntPtr Vertices3D;
		public IntPtr Indices32;
		public IntPtr Indices16;
		public IntPtr Indices8;
		public IntPtr Indices16_2;
		public int NumTriangles;
		public int NumVertices;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(MeshPartData), fieldName).ToInt32(); }
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct StridingMeshInterfaceData
	{
		public IntPtr MeshPartsPtr;
		public Vector3FloatData Scaling;
		public int NumMeshParts;
		public int Padding;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(StridingMeshInterfaceData), fieldName).ToInt32(); }
	}
}
