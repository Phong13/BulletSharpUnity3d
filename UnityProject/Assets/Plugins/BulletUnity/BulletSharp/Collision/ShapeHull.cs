using System;


namespace BulletSharp
{
	public class ShapeHull : IDisposable
	{
		internal IntPtr Native;

		private ConvexShape _shape;
		private UIntArray _indices;
		private Vector3Array _vertices;

		public ShapeHull(ConvexShape shape)
		{
			Native = UnsafeNativeMethods.btShapeHull_new(shape.Native);
			_shape = shape;
		}

		public bool BuildHull(float margin)
		{
			return UnsafeNativeMethods.btShapeHull_buildHull(Native, margin);
		}

		public IntPtr IndexPointer{ get { return  UnsafeNativeMethods.btShapeHull_getIndexPointer(Native);} }

		public UIntArray Indices
		{
			get
			{
				if (_indices == null)
				{
					_indices = new UIntArray(IndexPointer, NumIndices);
				}
				return _indices;
			}
		}

		public int NumIndices{ get { return  UnsafeNativeMethods.btShapeHull_numIndices(Native);} }

		public int NumTriangles{ get { return  UnsafeNativeMethods.btShapeHull_numTriangles(Native);} }

		public int NumVertices{ get { return  UnsafeNativeMethods.btShapeHull_numVertices(Native);} }

		public IntPtr VertexPointer{ get { return  UnsafeNativeMethods.btShapeHull_getVertexPointer(Native);} }

		public Vector3Array Vertices
		{
			get
			{
				if (_vertices == null || _vertices.Count != NumVertices)
				{
					_vertices = new Vector3Array(VertexPointer, NumVertices);
				}
				return _vertices;
			}
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
				UnsafeNativeMethods.btShapeHull_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~ShapeHull()
		{
			Dispose(false);
		}
	}
}
