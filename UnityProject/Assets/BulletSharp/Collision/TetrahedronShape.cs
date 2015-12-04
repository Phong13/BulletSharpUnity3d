using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class BuSimplex1To4 : PolyhedralConvexAabbCachingShape
	{
		internal BuSimplex1To4(IntPtr native)
			: base(native)
		{
		}

		public BuSimplex1To4()
			: base(btBU_Simplex1to4_new())
		{
		}

		public BuSimplex1To4(Vector3 pt0)
			: base(btBU_Simplex1to4_new2(ref pt0))
		{
		}

		public BuSimplex1To4(Vector3 pt0, Vector3 pt1)
			: base(btBU_Simplex1to4_new3(ref pt0, ref pt1))
		{
		}

		public BuSimplex1To4(Vector3 pt0, Vector3 pt1, Vector3 pt2)
			: base(btBU_Simplex1to4_new4(ref pt0, ref pt1, ref pt2))
		{
		}

		public BuSimplex1To4(Vector3 pt0, Vector3 pt1, Vector3 pt2, Vector3 pt3)
			: base(btBU_Simplex1to4_new5(ref pt0, ref pt1, ref pt2, ref pt3))
		{
		}

        public void AddVertexRef(ref Vector3 pt)
        {
            btBU_Simplex1to4_addVertex(_native, ref pt);
        }

		public void AddVertex(Vector3 pt)
		{
			btBU_Simplex1to4_addVertex(_native, ref pt);
		}

		public int GetIndex(int i)
		{
			return btBU_Simplex1to4_getIndex(_native, i);
		}

		public void Reset()
		{
			btBU_Simplex1to4_reset(_native);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBU_Simplex1to4_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBU_Simplex1to4_new2([In] ref Vector3 pt0);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBU_Simplex1to4_new3([In] ref Vector3 pt0, [In] ref Vector3 pt1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBU_Simplex1to4_new4([In] ref Vector3 pt0, [In] ref Vector3 pt1, [In] ref Vector3 pt2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBU_Simplex1to4_new5([In] ref Vector3 pt0, [In] ref Vector3 pt1, [In] ref Vector3 pt2, [In] ref Vector3 pt3);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBU_Simplex1to4_addVertex(IntPtr obj, [In] ref Vector3 pt);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btBU_Simplex1to4_getIndex(IntPtr obj, int i);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBU_Simplex1to4_reset(IntPtr obj);
	}
}
