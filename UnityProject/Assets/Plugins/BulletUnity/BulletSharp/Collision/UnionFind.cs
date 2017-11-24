using System;


namespace BulletSharp
{
	public class Element
	{
		internal IntPtr Native;

		internal Element(IntPtr native)
		{
			Native = native;
		}

		public int Id
		{
			get { return  UnsafeNativeMethods.btElement_getId(Native);}
			set {  UnsafeNativeMethods.btElement_setId(Native, value);}
		}

		public int Sz
		{
			get { return  UnsafeNativeMethods.btElement_getSz(Native);}
			set {  UnsafeNativeMethods.btElement_setSz(Native, value);}
		}
	}

	public class UnionFind
	{
		internal IntPtr Native;

		internal UnionFind(IntPtr native)
		{
			Native = native;
		}

		public void Allocate(int n)
		{
			UnsafeNativeMethods.btUnionFind_allocate(Native, n);
		}

		public int Find(int p, int q)
		{
			return UnsafeNativeMethods.btUnionFind_find(Native, p, q);
		}

		public int Find(int x)
		{
			return UnsafeNativeMethods.btUnionFind_find2(Native, x);
		}

		public void Free()
		{
			UnsafeNativeMethods.btUnionFind_Free(Native);
		}

		public Element GetElement(int index)
		{
			return new Element(UnsafeNativeMethods.btUnionFind_getElement(Native, index));
		}

		public bool IsRoot(int x)
		{
			return UnsafeNativeMethods.btUnionFind_isRoot(Native, x);
		}

		public void Reset(int n)
		{
			UnsafeNativeMethods.btUnionFind_reset(Native, n);
		}

		public void SortIslands()
		{
			UnsafeNativeMethods.btUnionFind_sortIslands(Native);
		}

		public void Unite(int p, int q)
		{
			UnsafeNativeMethods.btUnionFind_unite(Native, p, q);
		}

		public int NumElements{ get { return  UnsafeNativeMethods.btUnionFind_getNumElements(Native);} }
	}
}
