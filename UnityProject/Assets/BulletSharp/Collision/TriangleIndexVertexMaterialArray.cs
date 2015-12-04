using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class MaterialProperties : IDisposable
	{
		internal IntPtr _native;

		public MaterialProperties()
		{
			_native = btMaterialProperties_new();
		}

		public IntPtr MaterialBase
		{
			get { return btMaterialProperties_getMaterialBase(_native); }
			set { btMaterialProperties_setMaterialBase(_native, value); }
		}

		public int MaterialStride
		{
			get { return btMaterialProperties_getMaterialStride(_native); }
			set { btMaterialProperties_setMaterialStride(_native, value); }
		}

		public PhyScalarType MaterialType
		{
			get { return btMaterialProperties_getMaterialType(_native); }
			set { btMaterialProperties_setMaterialType(_native, value); }
		}

		public int NumMaterials
		{
			get { return btMaterialProperties_getNumMaterials(_native); }
			set { btMaterialProperties_setNumMaterials(_native, value); }
		}

		public int NumTriangles
		{
			get { return btMaterialProperties_getNumTriangles(_native); }
			set { btMaterialProperties_setNumTriangles(_native, value); }
		}

		public IntPtr TriangleMaterialsBase
		{
			get { return btMaterialProperties_getTriangleMaterialsBase(_native); }
			set { btMaterialProperties_setTriangleMaterialsBase(_native, value); }
		}

		public int TriangleMaterialStride
		{
			get { return btMaterialProperties_getTriangleMaterialStride(_native); }
			set { btMaterialProperties_setTriangleMaterialStride(_native, value); }
		}

		public PhyScalarType TriangleType
		{
			get { return btMaterialProperties_getTriangleType(_native); }
			set { btMaterialProperties_setTriangleType(_native, value); }
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
				btMaterialProperties_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~MaterialProperties()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMaterialProperties_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMaterialProperties_getMaterialBase(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btMaterialProperties_getMaterialStride(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern PhyScalarType btMaterialProperties_getMaterialType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btMaterialProperties_getNumMaterials(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btMaterialProperties_getNumTriangles(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMaterialProperties_getTriangleMaterialsBase(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btMaterialProperties_getTriangleMaterialStride(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern PhyScalarType btMaterialProperties_getTriangleType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMaterialProperties_setMaterialBase(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMaterialProperties_setMaterialStride(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btMaterialProperties_setMaterialType(IntPtr obj, PhyScalarType value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMaterialProperties_setNumMaterials(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMaterialProperties_setNumTriangles(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMaterialProperties_setTriangleMaterialsBase(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMaterialProperties_setTriangleMaterialStride(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btMaterialProperties_setTriangleType(IntPtr obj, PhyScalarType value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMaterialProperties_delete(IntPtr obj);
	}

	public class TriangleIndexVertexMaterialArray : TriangleIndexVertexArray
	{
		internal TriangleIndexVertexMaterialArray(IntPtr native)
			: base(native)
		{
		}

		public TriangleIndexVertexMaterialArray()
			: base(btTriangleIndexVertexMaterialArray_new())
		{
		}

        public TriangleIndexVertexMaterialArray(int numTriangles, IntPtr triangleIndexBase, int triangleIndexStride, int numVertices, IntPtr vertexBase, int vertexStride, int numMaterials, IntPtr materialBase, int materialStride, IntPtr triangleMaterialsBase, int materialIndexStride)
			: base(btTriangleIndexVertexMaterialArray_new2(numTriangles, triangleIndexBase, triangleIndexStride, numVertices, vertexBase, vertexStride, numMaterials, materialBase, materialStride, triangleMaterialsBase, materialIndexStride))
		{
		}

		public void AddMaterialProperties(MaterialProperties mat)
		{
			btTriangleIndexVertexMaterialArray_addMaterialProperties(_native, mat._native);
		}

        public void AddMaterialProperties(MaterialProperties mat, PhyScalarType triangleType)
		{
			btTriangleIndexVertexMaterialArray_addMaterialProperties2(_native, mat._native, triangleType);
		}

        public void GetLockedMaterialBase(out IntPtr materialBase, out int numMaterials, out PhyScalarType materialType, out int materialStride, out IntPtr triangleMaterialBase, out int numTriangles, out int triangleMaterialStride, out PhyScalarType triangleType)
		{
			btTriangleIndexVertexMaterialArray_getLockedMaterialBase(_native, out materialBase, out numMaterials, out materialType, out materialStride, out triangleMaterialBase, out numTriangles, out triangleMaterialStride, out triangleType);
		}

        public void GetLockedMaterialBase(out IntPtr materialBase, out int numMaterials, out PhyScalarType materialType, out int materialStride, out IntPtr triangleMaterialBase, out int numTriangles, out int triangleMaterialStride, out PhyScalarType triangleType, int subpart)
		{
            btTriangleIndexVertexMaterialArray_getLockedMaterialBase2(_native, out materialBase, out numMaterials, out materialType, out materialStride, out triangleMaterialBase, out numTriangles, out triangleMaterialStride, out triangleType, subpart);
		}

        public void GetLockedReadOnlyMaterialBase(out IntPtr materialBase, out int numMaterials, out PhyScalarType materialType, out int materialStride, out IntPtr triangleMaterialBase, out int numTriangles, out int triangleMaterialStride, out PhyScalarType triangleType)
		{
            btTriangleIndexVertexMaterialArray_getLockedReadOnlyMaterialBase(_native, out materialBase, out numMaterials, out materialType, out materialStride, out triangleMaterialBase, out numTriangles, out triangleMaterialStride, out triangleType);
		}

        public void GetLockedReadOnlyMaterialBase(out IntPtr materialBase, out int numMaterials, out PhyScalarType materialType, out int materialStride, out IntPtr triangleMaterialBase, out int numTriangles, out int triangleMaterialStride, out PhyScalarType triangleType, int subpart)
		{
            btTriangleIndexVertexMaterialArray_getLockedReadOnlyMaterialBase2(_native, out materialBase, out numMaterials, out materialType, out materialStride, out triangleMaterialBase, out numTriangles, out triangleMaterialStride, out triangleType, subpart);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btTriangleIndexVertexMaterialArray_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btTriangleIndexVertexMaterialArray_new2(int numTriangles, IntPtr triangleIndexBase, int triangleIndexStride, int numVertices, IntPtr vertexBase, int vertexStride, int numMaterials, IntPtr materialBase, int materialStride, IntPtr triangleMaterialsBase, int materialIndexStride);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTriangleIndexVertexMaterialArray_addMaterialProperties(IntPtr obj, IntPtr mat);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btTriangleIndexVertexMaterialArray_addMaterialProperties2(IntPtr obj, IntPtr mat, PhyScalarType triangleType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btTriangleIndexVertexMaterialArray_getLockedMaterialBase(IntPtr obj, [Out] out IntPtr materialBase, [Out] out int numMaterials, [Out] out PhyScalarType materialType, [Out] out int materialStride, [Out] out IntPtr triangleMaterialBase, [Out] out int numTriangles, [Out] out int triangleMaterialStride, [Out] out PhyScalarType triangleType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btTriangleIndexVertexMaterialArray_getLockedMaterialBase2(IntPtr obj, [Out] out IntPtr materialBase, [Out] out int numMaterials, [Out] out PhyScalarType materialType, [Out] out int materialStride, [Out] out IntPtr triangleMaterialBase, [Out] out int numTriangles, [Out] out int triangleMaterialStride, [Out] out PhyScalarType triangleType, int subpart);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btTriangleIndexVertexMaterialArray_getLockedReadOnlyMaterialBase(IntPtr obj, [Out] out IntPtr materialBase, [Out] out int numMaterials, [Out] out PhyScalarType materialType, [Out] out int materialStride, [Out] out IntPtr triangleMaterialBase, [Out] out int numTriangles, [Out] out int triangleMaterialStride, [Out] out PhyScalarType triangleType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btTriangleIndexVertexMaterialArray_getLockedReadOnlyMaterialBase2(IntPtr obj, [Out] out IntPtr materialBase, [Out] out int numMaterials, [Out] out PhyScalarType materialType, [Out] out int materialStride, [Out] out IntPtr triangleMaterialBase, [Out] out int numTriangles, [Out] out int triangleMaterialStride, [Out] out PhyScalarType triangleType, int subpart);
	}
}
