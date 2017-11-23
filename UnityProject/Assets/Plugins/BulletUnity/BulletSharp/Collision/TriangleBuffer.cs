using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class Triangle : IDisposable
	{
		internal IntPtr Native;
		private bool _preventDelete;

		internal Triangle(IntPtr native, bool preventDelete)
		{
			Native = native;
			_preventDelete = preventDelete;
		}

		public Triangle()
		{
			Native = btTriangle_new();
		}

		public int PartId
		{
			get => btTriangle_getPartId(Native);
			set => btTriangle_setPartId(Native, value);
		}

		public int TriangleIndex
		{
			get => btTriangle_getTriangleIndex(Native);
			set => btTriangle_setTriangleIndex(Native, value);
		}

		public Vector3 Vertex0
		{
			get
			{
				Vector3 value;
				btTriangle_getVertex0(Native, out value);
				return value;
			}
			set => btTriangle_setVertex0(Native, ref value);
		}

		public Vector3 Vertex1
		{
			get
			{
				Vector3 value;
				btTriangle_getVertex1(Native, out value);
				return value;
			}
			set => btTriangle_setVertex1(Native, ref value);
		}

		public Vector3 Vertex2
		{
			get
			{
				Vector3 value;
				btTriangle_getVertex2(Native, out value);
				return value;
			}
			set => btTriangle_setVertex2(Native, ref value);
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
				if (!_preventDelete)
				{
					btTriangle_delete(Native);
				}
				Native = IntPtr.Zero;
			}
		}

		~Triangle()
		{
			Dispose(false);
		}
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
			btTriangleBuffer_clearBuffer(Native);
		}

		public Triangle GetTriangle(int index)
		{
			return new Triangle(btTriangleBuffer_getTriangle(Native, index), true);
		}

		public override void ProcessTriangle(ref Vector3 vector0, ref Vector3 vector1, ref Vector3 vector2, int partId, int triangleIndex)
		{
			throw new NotImplementedException();
		}

		public int NumTriangles => btTriangleBuffer_getNumTriangles(Native);
	}
}
