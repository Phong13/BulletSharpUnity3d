using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class Element : IDisposable
	{
		internal IntPtr _native;

		internal Element(IntPtr native)
		{
			_native = native;
		}

		public Element()
		{
			_native = btElement_new();
		}

		public int Id
		{
			get { return btElement_getId(_native); }
			set { btElement_setId(_native, value); }
		}

		public int Sz
		{
			get { return btElement_getSz(_native); }
			set { btElement_setSz(_native, value); }
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
				btElement_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~Element()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btElement_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btElement_getId(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btElement_getSz(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btElement_setId(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btElement_setSz(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btElement_delete(IntPtr obj);
	}

	public class UnionFind : IDisposable
	{
		internal IntPtr _native;

		internal UnionFind(IntPtr native)
		{
			_native = native;
		}

		public UnionFind()
		{
			_native = btUnionFind_new();
		}

		public void Allocate(int n)
		{
			btUnionFind_allocate(_native, n);
		}

		public int Find(int p, int q)
		{
			return btUnionFind_find(_native, p, q);
		}

		public int Find(int x)
		{
			return btUnionFind_find2(_native, x);
		}

		public void Free()
		{
			btUnionFind_Free(_native);
		}

		public Element GetElement(int index)
		{
            return new Element(btUnionFind_getElement(_native, index));
		}

		public bool IsRoot(int x)
		{
			return btUnionFind_isRoot(_native, x);
		}

		public void Reset(int n)
		{
			btUnionFind_reset(_native, n);
		}

		public void SortIslands()
		{
			btUnionFind_sortIslands(_native);
		}

		public void Unite(int p, int q)
		{
			btUnionFind_unite(_native, p, q);
		}

		public int NumElements
		{
			get { return btUnionFind_getNumElements(_native); }
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
				btUnionFind_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~UnionFind()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btUnionFind_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btUnionFind_allocate(IntPtr obj, int N);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btUnionFind_find(IntPtr obj, int p, int q);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btUnionFind_find2(IntPtr obj, int x);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btUnionFind_Free(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btUnionFind_getElement(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btUnionFind_getNumElements(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btUnionFind_isRoot(IntPtr obj, int x);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btUnionFind_reset(IntPtr obj, int N);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btUnionFind_sortIslands(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btUnionFind_unite(IntPtr obj, int p, int q);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btUnionFind_delete(IntPtr obj);
	}
}
