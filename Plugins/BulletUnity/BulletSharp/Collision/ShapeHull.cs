using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class ShapeHull : IDisposable
	{
		internal IntPtr _native;

        ConvexShape _shape;
        UIntArray _indices;
        Vector3Array _vertices;

		public ShapeHull(ConvexShape shape)
		{
			_native = btShapeHull_new(shape._native);
            _shape = shape;
		}

		public bool BuildHull(float margin)
		{
			return btShapeHull_buildHull(_native, margin);
		}

        public IntPtr IndexPointer
        {
            get { return btShapeHull_getIndexPointer(_native); }
        }

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

		public int NumIndices
		{
            get { return btShapeHull_numIndices(_native); }
		}

		public int NumTriangles
		{
            get { return btShapeHull_numTriangles(_native); }
		}

		public int NumVertices
		{
            get { return btShapeHull_numVertices(_native); }
		}

        public IntPtr VertexPointer
		{
            get { return btShapeHull_getVertexPointer(_native); }
		}

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
			if (_native != IntPtr.Zero)
			{
				btShapeHull_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~ShapeHull()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btShapeHull_new(IntPtr shape);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btShapeHull_buildHull(IntPtr obj, float margin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btShapeHull_getIndexPointer(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btShapeHull_getVertexPointer(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btShapeHull_numIndices(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btShapeHull_numTriangles(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btShapeHull_numVertices(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btShapeHull_delete(IntPtr obj);
	}
}
