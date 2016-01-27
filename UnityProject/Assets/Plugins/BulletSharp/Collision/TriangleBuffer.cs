using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class Triangle : IDisposable
	{
		internal IntPtr _native;
        private bool _preventDelete;

		internal Triangle(IntPtr native, bool preventDelete)
		{
			_native = native;
            _preventDelete = preventDelete;
		}

		public Triangle()
		{
			_native = btTriangle_new();
		}

		public int PartId
		{
			get { return btTriangle_getPartId(_native); }
			set { btTriangle_setPartId(_native, value); }
		}

		public int TriangleIndex
		{
			get { return btTriangle_getTriangleIndex(_native); }
			set { btTriangle_setTriangleIndex(_native, value); }
		}

		public Vector3 Vertex0
		{
			get
			{
				Vector3 value;
				btTriangle_getVertex0(_native, out value);
				return value;
			}
			set { btTriangle_setVertex0(_native, ref value); }
		}

		public Vector3 Vertex1
		{
			get
			{
				Vector3 value;
				btTriangle_getVertex1(_native, out value);
				return value;
			}
			set { btTriangle_setVertex1(_native, ref value); }
		}

		public Vector3 Vertex2
		{
			get
			{
				Vector3 value;
				btTriangle_getVertex2(_native, out value);
				return value;
			}
			set { btTriangle_setVertex2(_native, ref value); }
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
                if (!_preventDelete)
                {
                    btTriangle_delete(_native);
                }
				_native = IntPtr.Zero;
			}
		}

		~Triangle()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btTriangle_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btTriangle_getPartId(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btTriangle_getTriangleIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTriangle_getVertex0(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTriangle_getVertex1(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTriangle_getVertex2(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTriangle_setPartId(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTriangle_setTriangleIndex(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTriangle_setVertex0(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTriangle_setVertex1(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTriangle_setVertex2(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTriangle_delete(IntPtr obj);
	}

	public class TriangleBuffer : TriangleCallback
	{
        /*
		public TriangleBuffer()
			: base(btTriangleBuffer_new())
		{
		}
        */
        public TriangleBuffer()
        {
        }

		public void ClearBuffer()
		{
			btTriangleBuffer_clearBuffer(_native);
		}

		public Triangle GetTriangle(int index)
		{
            return new Triangle(btTriangleBuffer_getTriangle(_native, index), true);
		}

        public override void ProcessTriangle(ref Vector3 vector0, ref Vector3 vector1, ref Vector3 vector2, int partId, int triangleIndex)
        {
            throw new NotImplementedException();
        }

		public int NumTriangles
		{
			get { return btTriangleBuffer_getNumTriangles(_native); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btTriangleBuffer_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTriangleBuffer_clearBuffer(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btTriangleBuffer_getNumTriangles(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btTriangleBuffer_getTriangle(IntPtr obj, int index);
	}
}
