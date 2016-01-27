using System;
using System.Runtime.InteropServices;
using System.Security;
using System.IO;
using BulletSharp.Math;

namespace BulletSharp
{
    public class StridingMeshInterface : IDisposable // abstract
	{
		internal IntPtr _native;

		internal StridingMeshInterface(IntPtr native)
		{
			_native = native;
		}

        public unsafe UnmanagedMemoryStream GetIndexStream(int subpart = 0)
        {
            IntPtr vertexBase, indexBase;
            int numVerts, numFaces;
            PhyScalarType vertsType, indicesType;
            int vertexStride, indexStride;
            btStridingMeshInterface_getLockedReadOnlyVertexIndexBase2(_native, out vertexBase, out numVerts, out vertsType, out vertexStride, out indexBase, out indexStride, out numFaces, out indicesType, subpart);

            int length = numFaces * indexStride;
            return new UnmanagedMemoryStream((byte*)indexBase.ToPointer(), length, length, FileAccess.ReadWrite);
        }

        public unsafe UnmanagedMemoryStream GetVertexStream(int subpart = 0)
        {
            IntPtr vertexBase, indexBase;
            int numVerts, numFaces;
            PhyScalarType vertsType, indicesType;
            int vertexStride, indexStride;
            btStridingMeshInterface_getLockedReadOnlyVertexIndexBase2(_native, out vertexBase, out numVerts, out vertsType, out vertexStride, out indexBase, out indexStride, out numFaces, out indicesType, subpart);

            int length = numVerts * vertexStride;
            return new UnmanagedMemoryStream((byte*)vertexBase.ToPointer(), length, length, FileAccess.ReadWrite);
        }

		public void CalculateAabbBruteForce(out Vector3 aabbMin, out Vector3 aabbMax)
		{
			btStridingMeshInterface_calculateAabbBruteForce(_native, out aabbMin, out aabbMax);
		}

		public int CalculateSerializeBufferSize()
		{
			return btStridingMeshInterface_calculateSerializeBufferSize(_native);
		}

        public void GetLockedReadOnlyVertexIndexBase(out IntPtr vertexBase, out int numVerts, out PhyScalarType type, out int stride, out IntPtr indexbase, out int indexStride, int numFaces, out PhyScalarType indicesType, int subpart = 0)
		{
            btStridingMeshInterface_getLockedReadOnlyVertexIndexBase2(_native, out vertexBase, out numVerts, out type, out stride, out indexbase, out indexStride, out numFaces, out indicesType, subpart);
		}

        public void GetLockedVertexIndexBase(out IntPtr vertexBase, out int numVerts, out PhyScalarType type, out int stride, out IntPtr indexbase, out int indexStride, int numFaces, out PhyScalarType indicesType, int subpart = 0)
		{
            btStridingMeshInterface_getLockedVertexIndexBase2(_native, out vertexBase, out numVerts, out type, out stride, out indexbase, out indexStride, out numFaces, out indicesType, subpart);
		}

        public void GetPremadeAabb(out Vector3 aabbMin, out Vector3 aabbMax)
		{
            btStridingMeshInterface_getPremadeAabb(_native, out aabbMin, out aabbMax);
		}

		public void InternalProcessAllTriangles(InternalTriangleIndexCallback callback, Vector3 aabbMin, Vector3 aabbMax)
		{
			btStridingMeshInterface_InternalProcessAllTriangles(_native, callback._native, ref aabbMin, ref aabbMax);
		}

		public void PreallocateIndices(int numIndices)
		{
			btStridingMeshInterface_preallocateIndices(_native, numIndices);
		}

		public void PreallocateVertices(int numVerts)
		{
			btStridingMeshInterface_preallocateVertices(_native, numVerts);
		}

		public string Serialize(IntPtr dataBuffer, Serializer serializer)
		{
			return Marshal.PtrToStringAnsi(btStridingMeshInterface_serialize(_native, dataBuffer, serializer._native));
		}

        public void SetPremadeAabb(ref Vector3 aabbMin, ref Vector3 aabbMax)
		{
			btStridingMeshInterface_setPremadeAabb(_native, ref aabbMin, ref aabbMax);
		}

		public void UnLockReadOnlyVertexBase(int subpart)
		{
			btStridingMeshInterface_unLockReadOnlyVertexBase(_native, subpart);
		}

		public void UnLockVertexBase(int subpart)
		{
			btStridingMeshInterface_unLockVertexBase(_native, subpart);
		}

		public bool HasPremadeAabb
		{
			get { return btStridingMeshInterface_hasPremadeAabb(_native); }
		}

		public int NumSubParts
		{
			get { return btStridingMeshInterface_getNumSubParts(_native); }
		}

		public Vector3 Scaling
		{
			get
			{
				Vector3 value;
				btStridingMeshInterface_getScaling(_native, out value);
				return value;
			}
			set { btStridingMeshInterface_setScaling(_native, ref value); }
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				btStridingMeshInterface_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~StridingMeshInterface()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btStridingMeshInterface_calculateAabbBruteForce(IntPtr obj, [Out] out Vector3 aabbMin, [Out] out Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btStridingMeshInterface_calculateSerializeBufferSize(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btStridingMeshInterface_getLockedReadOnlyVertexIndexBase(IntPtr obj, [Out] out IntPtr vertexbase, [Out] out int numverts, [Out] out PhyScalarType type, [Out] out int stride, [Out] out IntPtr indexbase, [Out] out int indexstride, [Out] out int numfaces, [Out] out PhyScalarType indicestype);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btStridingMeshInterface_getLockedReadOnlyVertexIndexBase2(IntPtr obj, [Out] out IntPtr vertexbase, [Out] out int numverts, [Out] out PhyScalarType type, [Out] out int stride, [Out] out IntPtr indexbase, [Out] out int indexstride, [Out] out int numfaces, [Out] out PhyScalarType indicestype, int subpart);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btStridingMeshInterface_getLockedVertexIndexBase(IntPtr obj, [Out] out IntPtr vertexbase, [Out] out int numverts, [Out] out PhyScalarType type, [Out] out int stride, [Out] out IntPtr indexbase, [Out] out int indexstride, [Out] out int numfaces, [Out] out PhyScalarType indicestype);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btStridingMeshInterface_getLockedVertexIndexBase2(IntPtr obj, [Out] out IntPtr vertexbase, [Out] out int numverts, [Out] out PhyScalarType type, [Out] out int stride, [Out] out IntPtr indexbase, [Out] out int indexstride, [Out] out int numfaces, [Out] out PhyScalarType indicestype, int subpart);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btStridingMeshInterface_getNumSubParts(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btStridingMeshInterface_getPremadeAabb(IntPtr obj, [Out] out Vector3 aabbMin, [Out] out Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btStridingMeshInterface_getScaling(IntPtr obj, [Out] out Vector3 scaling);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btStridingMeshInterface_hasPremadeAabb(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btStridingMeshInterface_InternalProcessAllTriangles(IntPtr obj, IntPtr callback, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btStridingMeshInterface_preallocateIndices(IntPtr obj, int numindices);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btStridingMeshInterface_preallocateVertices(IntPtr obj, int numverts);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btStridingMeshInterface_serialize(IntPtr obj, IntPtr dataBuffer, IntPtr serializer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btStridingMeshInterface_setPremadeAabb(IntPtr obj, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btStridingMeshInterface_setScaling(IntPtr obj, [In] ref Vector3 scaling);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btStridingMeshInterface_unLockReadOnlyVertexBase(IntPtr obj, int subpart);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btStridingMeshInterface_unLockVertexBase(IntPtr obj, int subpart);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btStridingMeshInterface_delete(IntPtr obj);
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
