using System;


namespace BulletSharp
{
	public class MaterialProperties : IDisposable
	{
		internal IntPtr Native;

		public MaterialProperties()
		{
			Native = UnsafeNativeMethods.btMaterialProperties_new();
		}

		public IntPtr MaterialBase
		{
			get => UnsafeNativeMethods.btMaterialProperties_getMaterialBase(Native);
			set => UnsafeNativeMethods.btMaterialProperties_setMaterialBase(Native, value);
		}

		public int MaterialStride
		{
			get => UnsafeNativeMethods.btMaterialProperties_getMaterialStride(Native);
			set => UnsafeNativeMethods.btMaterialProperties_setMaterialStride(Native, value);
		}

		public PhyScalarType MaterialType
		{
			get => UnsafeNativeMethods.btMaterialProperties_getMaterialType(Native);
			set => UnsafeNativeMethods.btMaterialProperties_setMaterialType(Native, value);
		}

		public int NumMaterials
		{
			get => UnsafeNativeMethods.btMaterialProperties_getNumMaterials(Native);
			set => UnsafeNativeMethods.btMaterialProperties_setNumMaterials(Native, value);
		}

		public int NumTriangles
		{
			get => UnsafeNativeMethods.btMaterialProperties_getNumTriangles(Native);
			set => UnsafeNativeMethods.btMaterialProperties_setNumTriangles(Native, value);
		}

		public IntPtr TriangleMaterialsBase
		{
			get => UnsafeNativeMethods.btMaterialProperties_getTriangleMaterialsBase(Native);
			set => UnsafeNativeMethods.btMaterialProperties_setTriangleMaterialsBase(Native, value);
		}

		public int TriangleMaterialStride
		{
			get => UnsafeNativeMethods.btMaterialProperties_getTriangleMaterialStride(Native);
			set => UnsafeNativeMethods.btMaterialProperties_setTriangleMaterialStride(Native, value);
		}

		public PhyScalarType TriangleType
		{
			get => UnsafeNativeMethods.btMaterialProperties_getTriangleType(Native);
			set => UnsafeNativeMethods.btMaterialProperties_setTriangleType(Native, value);
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
				UnsafeNativeMethods.btMaterialProperties_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~MaterialProperties()
		{
			Dispose(false);
		}
	}

	public class TriangleIndexVertexMaterialArray : TriangleIndexVertexArray
	{
		internal TriangleIndexVertexMaterialArray(IntPtr native)
			: base(native)
		{
		}

		public TriangleIndexVertexMaterialArray()
			: base(UnsafeNativeMethods.btTriangleIndexVertexMaterialArray_new())
		{
		}

		public TriangleIndexVertexMaterialArray(int numTriangles, IntPtr triangleIndexBase,
			int triangleIndexStride, int numVertices, IntPtr vertexBase, int vertexStride,
			int numMaterials, IntPtr materialBase, int materialStride, IntPtr triangleMaterialsBase,
			int materialIndexStride)
			: base(UnsafeNativeMethods.btTriangleIndexVertexMaterialArray_new2(numTriangles, triangleIndexBase,
				triangleIndexStride, numVertices, vertexBase, vertexStride,
				numMaterials, materialBase, materialStride, triangleMaterialsBase,
				materialIndexStride))
		{
		}

		public void AddMaterialProperties(MaterialProperties mat, PhyScalarType triangleType = PhyScalarType.Int32)
		{
			UnsafeNativeMethods.btTriangleIndexVertexMaterialArray_addMaterialProperties(Native, mat.Native,
				triangleType);
		}

		public void GetLockedMaterialBase(out IntPtr materialBase, out int numMaterials,
			out PhyScalarType materialType, out int materialStride, out IntPtr triangleMaterialBase,
			out int numTriangles, out int triangleMaterialStride, out PhyScalarType triangleType,
			int subpart = 0)
		{
			UnsafeNativeMethods.btTriangleIndexVertexMaterialArray_getLockedMaterialBase(Native, out materialBase,
				out numMaterials, out materialType, out materialStride, out triangleMaterialBase,
				out numTriangles, out triangleMaterialStride, out triangleType, subpart);
		}

		public void GetLockedReadOnlyMaterialBase(out IntPtr materialBase, out int numMaterials,
			out PhyScalarType materialType, out int materialStride, out IntPtr triangleMaterialBase,
			out int numTriangles, out int triangleMaterialStride, out PhyScalarType triangleType,
			int subpart = 0)
		{
			UnsafeNativeMethods.btTriangleIndexVertexMaterialArray_getLockedReadOnlyMaterialBase(Native,
				out materialBase, out numMaterials, out materialType, out materialStride,
				out triangleMaterialBase, out numTriangles, out triangleMaterialStride,
				out triangleType, subpart);
		}
	}
}
