using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class DbvtAabbMm
	{
		internal IntPtr _native;

		internal DbvtAabbMm(IntPtr native)
		{
			_native = native;
		}

		public int Classify(Vector3 n, float o, int s)
		{
			return btDbvtAabbMm_Classify(_native, ref n, o, s);
		}

		public bool Contain(DbvtAabbMm a)
		{
			return btDbvtAabbMm_Contain(_native, a._native);
		}

		public void Expand(Vector3 e)
		{
			btDbvtAabbMm_Expand(_native, ref e);
		}
        /*
		public static DbvtAabbMm FromCE(Vector3 c, Vector3 e)
		{
			return btDbvtAabbMm_FromCE(ref c, ref e);
		}

		public static DbvtAabbMm FromCR(Vector3 c, float r)
		{
			return btDbvtAabbMm_FromCR(ref c, r);
		}

		public static DbvtAabbMm FromMM(Vector3 mi, Vector3 mx)
		{
			return btDbvtAabbMm_FromMM(ref mi, ref mx);
		}

		public static DbvtAabbMm FromPoints(Vector3 ppts, int n)
		{
			return btDbvtAabbMm_FromPoints(ref ppts, n);
		}

		public static DbvtAabbMm FromPoints(Vector3 pts, int n)
		{
			return btDbvtAabbMm_FromPoints2(ref pts, n);
		}
        */
		public float ProjectMinimum(Vector3 v, uint signs)
		{
			return btDbvtAabbMm_ProjectMinimum(_native, ref v, signs);
		}

		public void SignedExpand(Vector3 e)
		{
			btDbvtAabbMm_SignedExpand(_native, ref e);
		}

		public Vector3 Center
		{
            get
            {
                Vector3 value;
                btDbvtAabbMm_Center(_native, out value);
                return value;
            }
		}

		public Vector3 Extents
		{
            get
            {
                Vector3 value;
                btDbvtAabbMm_Extents(_native, out value);
                return value;
            }
		}

		public Vector3 Lengths
        {
            get
            {
                Vector3 value;
                btDbvtAabbMm_Lengths(_native, out value);
                return value;
            }
		}

		public Vector3 Maxs
		{
            get
            {
                Vector3 value;
                btDbvtAabbMm_Maxs(_native, out value);
                return value;
            }
		}

		public Vector3 Mins
		{
            get
            {
                Vector3 value;
                btDbvtAabbMm_Mins(_native, out value);
                return value;
            }
		}

		public Vector3 TMaxs
		{
            get
            {
                Vector3 value;
                btDbvtAabbMm_tMaxs(_native, out value);
                return value;
            }
		}

		public Vector3 TMins
		{
            get
            {
                Vector3 value;
                btDbvtAabbMm_tMins(_native, out value);
                return value;
            }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDbvtAabbMm_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtAabbMm_Center(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btDbvtAabbMm_Classify(IntPtr obj, [In] ref Vector3 n, float o, int s);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btDbvtAabbMm_Contain(IntPtr obj, IntPtr a);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtAabbMm_Expand(IntPtr obj, [In] ref Vector3 e);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtAabbMm_Extents(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtAabbMm_FromCE([In] ref Vector3 c, [In] ref Vector3 e);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtAabbMm_FromCR([In] ref Vector3 c, float r);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtAabbMm_FromMM([In] ref Vector3 mi, [In] ref Vector3 mx);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtAabbMm_FromPoints([In] ref Vector3 ppts, int n);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtAabbMm_FromPoints2([In] ref Vector3 pts, int n);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtAabbMm_Lengths(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtAabbMm_Maxs(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtAabbMm_Mins(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btDbvtAabbMm_ProjectMinimum(IntPtr obj, [In] ref Vector3 v, uint signs);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtAabbMm_SignedExpand(IntPtr obj, [In] ref Vector3 e);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtAabbMm_tMaxs(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtAabbMm_tMins(IntPtr obj, [Out] out Vector3 value);
	}

	public class DbvtNode
	{
		internal IntPtr _native;

		internal DbvtNode(IntPtr native)
		{
			_native = native;
		}
        /*

		public DbvtNodePtrArray Childs
		{
			get { return btDbvtNode_getChilds(_native); }
		}
        */
		public IntPtr Data
		{
			get { return btDbvtNode_getData(_native); }
			set { btDbvtNode_setData(_native, value); }
		}

		public int DataAsInt
		{
			get { return btDbvtNode_getDataAsInt(_native); }
			set { btDbvtNode_setDataAsInt(_native, value); }
		}

		public bool Isinternal
		{
			get { return btDbvtNode_isinternal(_native); }
		}

		public bool Isleaf
		{
			get { return btDbvtNode_isleaf(_native); }
		}

		public DbvtNode Parent
		{
            get
            {
                IntPtr ptr = btDbvtNode_getParent(_native);
                return (ptr != IntPtr.Zero) ? new DbvtNode(ptr) : null;
            }
            set { btDbvtNode_setParent(_native, (value != null) ? value._native : IntPtr.Zero); }
		}

		public DbvtVolume Volume
		{
            get
            {
                IntPtr ptr = btDbvtNode_getVolume(_native);
                return (ptr != IntPtr.Zero) ? new DbvtVolume(ptr) : null;
            }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDbvtNode_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDbvtNode_getChilds(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDbvtNode_getData(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btDbvtNode_getDataAsInt(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDbvtNode_getParent(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDbvtNode_getVolume(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btDbvtNode_isinternal(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btDbvtNode_isleaf(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtNode_setData(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtNode_setDataAsInt(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtNode_setParent(IntPtr obj, IntPtr value);
	}

    public class DbvtVolume : DbvtAabbMm
    {
        internal DbvtVolume(IntPtr native)
            : base(native)
        {
        }
    }

	public class Dbvt : IDisposable
	{
		public class IClone : IDisposable
		{
			internal IntPtr _native;

			internal IClone(IntPtr native)
			{
				_native = native;
			}

			public IClone()
			{
				_native = btDbvt_IClone_new();
			}

			public void CloneLeaf(DbvtNode __unnamed0)
			{
				btDbvt_IClone_CloneLeaf(_native, __unnamed0._native);
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
					btDbvt_IClone_delete(_native);
					_native = IntPtr.Zero;
				}
			}

			~IClone()
			{
				Dispose(false);
			}

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btDbvt_IClone_new();
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btDbvt_IClone_CloneLeaf(IntPtr obj, IntPtr __unnamed0);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btDbvt_IClone_delete(IntPtr obj);
		}

		public class ICollide : IDisposable
		{
			internal IntPtr _native;

			internal ICollide(IntPtr native)
			{
				_native = native;
			}

			public ICollide()
			{
				_native = btDbvt_ICollide_new();
			}

			public bool AllLeaves(DbvtNode __unnamed0)
			{
				return btDbvt_ICollide_AllLeaves(_native, __unnamed0._native);
			}

			public bool Descent(DbvtNode __unnamed0)
			{
				return btDbvt_ICollide_Descent(_native, __unnamed0._native);
			}

			public void Process(DbvtNode __unnamed0, DbvtNode __unnamed1)
			{
				btDbvt_ICollide_Process(_native, __unnamed0._native, __unnamed1._native);
			}

			public void Process(DbvtNode __unnamed0)
			{
				btDbvt_ICollide_Process2(_native, __unnamed0._native);
			}

			public void Process(DbvtNode n, float __unnamed1)
			{
				btDbvt_ICollide_Process3(_native, n._native, __unnamed1);
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
					btDbvt_ICollide_delete(_native);
					_native = IntPtr.Zero;
				}
			}

			~ICollide()
			{
				Dispose(false);
			}

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btDbvt_ICollide_new();
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			[return: MarshalAs(UnmanagedType.I1)]
			static extern bool btDbvt_ICollide_AllLeaves(IntPtr obj, IntPtr __unnamed0);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			[return: MarshalAs(UnmanagedType.I1)]
			static extern bool btDbvt_ICollide_Descent(IntPtr obj, IntPtr __unnamed0);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btDbvt_ICollide_Process(IntPtr obj, IntPtr __unnamed0, IntPtr __unnamed1);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btDbvt_ICollide_Process2(IntPtr obj, IntPtr __unnamed0);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btDbvt_ICollide_Process3(IntPtr obj, IntPtr n, float __unnamed1);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btDbvt_ICollide_delete(IntPtr obj);
		}

		public abstract class IWriter : IDisposable
		{
			internal IntPtr _native;

			internal IWriter(IntPtr native)
			{
				_native = native;
			}

			public void Prepare(DbvtNode root, int numnodes)
			{
				btDbvt_IWriter_Prepare(_native, root._native, numnodes);
			}

			public void WriteLeaf(DbvtNode __unnamed0, int index, int parent)
			{
				btDbvt_IWriter_WriteLeaf(_native, __unnamed0._native, index, parent);
			}

			public void WriteNode(DbvtNode __unnamed0, int index, int parent, int child0, int child1)
			{
				btDbvt_IWriter_WriteNode(_native, __unnamed0._native, index, parent, child0, child1);
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
					btDbvt_IWriter_delete(_native);
					_native = IntPtr.Zero;
				}
			}

			~IWriter()
			{
				Dispose(false);
			}

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btDbvt_IWriter_Prepare(IntPtr obj, IntPtr root, int numnodes);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btDbvt_IWriter_WriteLeaf(IntPtr obj, IntPtr __unnamed0, int index, int parent);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btDbvt_IWriter_WriteNode(IntPtr obj, IntPtr __unnamed0, int index, int parent, int child0, int child1);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btDbvt_IWriter_delete(IntPtr obj);
		}

		public class StkCln : IDisposable
		{
			internal IntPtr _native;

			internal StkCln(IntPtr native)
			{
				_native = native;
			}

			public StkCln(DbvtNode n, DbvtNode p)
			{
				_native = btDbvt_sStkCLN_new(n._native, p._native);
			}

			public DbvtNode Node
			{
                get
                {
                    IntPtr ptr = btDbvt_sStkCLN_getNode(_native);
                    return (ptr != IntPtr.Zero) ? new DbvtNode(ptr) : null;
                }
                set { btDbvt_sStkCLN_setNode(_native, (value != null) ? value._native : IntPtr.Zero); }
			}

			public DbvtNode Parent
			{
				get
                {
                    IntPtr ptr = btDbvt_sStkCLN_getParent(_native);
                    return (ptr != IntPtr.Zero) ? new DbvtNode(ptr) : null;
                }
                set { btDbvt_sStkCLN_setParent(_native, (value != null) ? value._native : IntPtr.Zero); }
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
					btDbvt_sStkCLN_delete(_native);
					_native = IntPtr.Zero;
				}
			}

			~StkCln()
			{
				Dispose(false);
			}

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btDbvt_sStkCLN_new(IntPtr n, IntPtr p);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btDbvt_sStkCLN_getNode(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btDbvt_sStkCLN_getParent(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btDbvt_sStkCLN_setNode(IntPtr obj, IntPtr value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btDbvt_sStkCLN_setParent(IntPtr obj, IntPtr value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btDbvt_sStkCLN_delete(IntPtr obj);
		}

		public class StkNN : IDisposable
		{
			internal IntPtr _native;

			internal StkNN(IntPtr native)
			{
				_native = native;
			}

			public StkNN()
			{
				_native = btDbvt_sStkNN_new();
			}

			public StkNN(DbvtNode na, DbvtNode nb)
			{
				_native = btDbvt_sStkNN_new2(na._native, nb._native);
			}

			public DbvtNode A
			{
                get
                {
                    IntPtr ptr = btDbvt_sStkNN_getA(_native);
                    return (ptr != IntPtr.Zero) ? new DbvtNode(ptr) : null;
                }
                set { btDbvt_sStkNN_setA(_native, (value != null) ? value._native : IntPtr.Zero); }
			}

			public DbvtNode B
			{
				get
		        {
		            IntPtr ptr = btDbvt_sStkNN_getB(_native);
		            return (ptr != IntPtr.Zero) ? new DbvtNode(ptr) : null;
		        }
		        set { btDbvt_sStkNN_setB(_native, (value != null) ? value._native : IntPtr.Zero); }
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
					btDbvt_sStkNN_delete(_native);
					_native = IntPtr.Zero;
				}
			}

			~StkNN()
			{
				Dispose(false);
			}

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btDbvt_sStkNN_new();
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btDbvt_sStkNN_new2(IntPtr na, IntPtr nb);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btDbvt_sStkNN_getA(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btDbvt_sStkNN_getB(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btDbvt_sStkNN_setA(IntPtr obj, IntPtr value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btDbvt_sStkNN_setB(IntPtr obj, IntPtr value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btDbvt_sStkNN_delete(IntPtr obj);
		}

		public class StkNP : IDisposable
		{
			internal IntPtr _native;

			internal StkNP(IntPtr native)
			{
				_native = native;
			}

			public StkNP(DbvtNode n, uint m)
			{
				_native = btDbvt_sStkNP_new(n._native, m);
			}

			public int Mask
			{
				get { return btDbvt_sStkNP_getMask(_native); }
				set { btDbvt_sStkNP_setMask(_native, value); }
			}

			public DbvtNode Node
			{
                get
                {
                    IntPtr ptr = btDbvt_sStkNP_getNode(_native);
                    return (ptr != IntPtr.Zero) ? new DbvtNode(ptr) : null;
                }
                set { btDbvt_sStkNP_setNode(_native, (value != null) ? value._native : IntPtr.Zero); }
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
					btDbvt_sStkNP_delete(_native);
					_native = IntPtr.Zero;
				}
			}

			~StkNP()
			{
				Dispose(false);
			}

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btDbvt_sStkNP_new(IntPtr n, uint m);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern int btDbvt_sStkNP_getMask(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btDbvt_sStkNP_getNode(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btDbvt_sStkNP_setMask(IntPtr obj, int value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btDbvt_sStkNP_setNode(IntPtr obj, IntPtr value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btDbvt_sStkNP_delete(IntPtr obj);
		}

		public class StkNps : IDisposable
		{
			internal IntPtr _native;

			internal StkNps(IntPtr native)
			{
				_native = native;
			}

			public StkNps()
			{
				_native = btDbvt_sStkNPS_new();
			}

			public StkNps(DbvtNode n, uint m, float v)
			{
				_native = btDbvt_sStkNPS_new2(n._native, m, v);
			}

			public int Mask
			{
				get { return btDbvt_sStkNPS_getMask(_native); }
				set { btDbvt_sStkNPS_setMask(_native, value); }
			}

			public DbvtNode Node
			{
                get
                {
                    IntPtr ptr = btDbvt_sStkNPS_getNode(_native);
                    return (ptr != IntPtr.Zero) ? new DbvtNode(ptr) : null;
                }
                set { btDbvt_sStkNPS_setNode(_native, (value != null) ? value._native : IntPtr.Zero); }
			}

			public float Value
			{
				get { return btDbvt_sStkNPS_getValue(_native); }
				set { btDbvt_sStkNPS_setValue(_native, value); }
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
					btDbvt_sStkNPS_delete(_native);
					_native = IntPtr.Zero;
				}
			}

			~StkNps()
			{
				Dispose(false);
			}

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btDbvt_sStkNPS_new();
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btDbvt_sStkNPS_new2(IntPtr n, uint m, float v);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern int btDbvt_sStkNPS_getMask(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btDbvt_sStkNPS_getNode(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern float btDbvt_sStkNPS_getValue(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btDbvt_sStkNPS_setMask(IntPtr obj, int value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btDbvt_sStkNPS_setNode(IntPtr obj, IntPtr value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btDbvt_sStkNPS_setValue(IntPtr obj, float value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btDbvt_sStkNPS_delete(IntPtr obj);
		}

		internal IntPtr _native;
		bool _preventDelete;

		internal Dbvt(IntPtr native, bool preventDelete)
		{
			_native = native;
			_preventDelete = preventDelete;
		}

		public Dbvt()
		{
			_native = btDbvt_new();
		}
        /*
        public static int Allocate(AlignedIntArray ifree, AlignedStkNpsArray stock, StkNps value)
		{
			return btDbvt_allocate(ifree._native, stock._native, value._native);
		}
        */
		public static void Benchmark()
		{
			btDbvt_benchmark();
		}

		public void Clear()
		{
			btDbvt_clear(_native);
		}

		public void Clone(Dbvt dest)
		{
			btDbvt_clone(_native, dest._native);
		}

		public void Clone(Dbvt dest, IClone iclone)
		{
			btDbvt_clone2(_native, dest._native, iclone._native);
		}
        /*
		public static void CollideKdop(DbvtNode root, Vector3 normals, float offsets, int count, ICollide policy)
		{
			btDbvt_collideKDOP(root._native, ref normals, offsets._native, count, policy._native);
		}

		public static void CollideOcl(DbvtNode root, Vector3 normals, float offsets, Vector3 sortaxis, int count, ICollide policy)
		{
			btDbvt_collideOCL(root._native, ref normals, offsets._native, ref sortaxis, count, policy._native);
		}

		public static void CollideOcl(DbvtNode root, Vector3 normals, float offsets, Vector3 sortaxis, int count, ICollide policy, bool fullsort)
		{
			btDbvt_collideOCL2(root._native, ref normals, offsets._native, ref sortaxis, count, policy._native, fullsort);
		}

		public void CollideTT(DbvtNode root0, DbvtNode root1, ICollide policy)
		{
			btDbvt_collideTT(_native, root0._native, root1._native, policy._native);
		}

		public void CollideTTPersistentStack(DbvtNode root0, DbvtNode root1, ICollide policy)
		{
			btDbvt_collideTTpersistentStack(_native, root0._native, root1._native, policy._native);
		}

		public static void CollideTU(DbvtNode root, ICollide policy)
		{
			btDbvt_collideTU(root._native, policy._native);
		}

		public void CollideTV(DbvtNode root, DbvtVolume volume, ICollide policy)
		{
			btDbvt_collideTV(_native, root._native, volume._native, policy._native);
		}
        */
		public static int CountLeaves(DbvtNode node)
		{
			return btDbvt_countLeaves(node._native);
		}

		public bool Empty()
		{
			return btDbvt_empty(_native);
		}
        /*
		public static void EnumLeaves(DbvtNode root, ICollide policy)
		{
			btDbvt_enumLeaves(root._native, policy._native);
		}

		public static void EnumNodes(DbvtNode root, ICollide policy)
		{
			btDbvt_enumNodes(root._native, policy._native);
		}

		public static void ExtractLeaves(DbvtNode node, AlignedDbvtNodeArray leaves)
		{
			btDbvt_extractLeaves(node._native, leaves._native);
		}
        */
		public DbvtNode Insert(DbvtVolume box, IntPtr data)
		{
            return new DbvtNode(btDbvt_insert(_native, box._native, data));
		}

		public static int MaxDepth(DbvtNode node)
		{
			return btDbvt_maxdepth(node._native);
		}

		public static int Nearest(int[] i, StkNps a, float v, int l, int h)
		{
			return btDbvt_nearest(i, a._native, v, l, h);
		}

		public void OptimizeBottomUp()
		{
			btDbvt_optimizeBottomUp(_native);
		}

		public void OptimizeIncremental(int passes)
		{
			btDbvt_optimizeIncremental(_native, passes);
		}

		public void OptimizeTopDown()
		{
			btDbvt_optimizeTopDown(_native);
		}

		public void OptimizeTopDown(int buTreshold)
		{
			btDbvt_optimizeTopDown2(_native, buTreshold);
		}
        /*
		public static void RayTest(DbvtNode root, Vector3 rayFrom, Vector3 rayTo, ICollide policy)
		{
			btDbvt_rayTest(root._native, ref rayFrom, ref rayTo, policy._native);
		}

		public void RayTestInternal(DbvtNode root, Vector3 rayFrom, Vector3 rayTo, Vector3 rayDirectionInverse, uint[] signs, float lambdaMax, Vector3 aabbMin, Vector3 aabbMax, ICollide policy)
		{
			btDbvt_rayTestInternal(_native, root._native, ref rayFrom, ref rayTo, ref rayDirectionInverse, signs, lambdaMax, ref aabbMin, ref aabbMax, policy._native);
		}
        */
		public void Remove(DbvtNode leaf)
		{
			btDbvt_remove(_native, leaf._native);
		}

		public void Update(DbvtNode leaf, DbvtVolume volume)
		{
			btDbvt_update(_native, leaf._native, volume._native);
		}

		public void Update(DbvtNode leaf)
		{
			btDbvt_update2(_native, leaf._native);
		}

		public void Update(DbvtNode leaf, int lookahead)
		{
			btDbvt_update3(_native, leaf._native, lookahead);
		}

		public bool Update(DbvtNode leaf, DbvtVolume volume, float margin)
		{
			return btDbvt_update4(_native, leaf._native, volume._native, margin);
		}

		public bool Update(DbvtNode leaf, DbvtVolume volume, Vector3 velocity)
		{
			return btDbvt_update5(_native, leaf._native, volume._native, ref velocity);
		}

		public bool Update(DbvtNode leaf, DbvtVolume volume, Vector3 velocity, float margin)
		{
			return btDbvt_update6(_native, leaf._native, volume._native, ref velocity, margin);
		}

		public void Write(IWriter iwriter)
		{
			btDbvt_write(_native, iwriter._native);
		}

		public DbvtNode Free
		{
            get
            {
                IntPtr ptr = btDbvt_getFree(_native);
                return (ptr != IntPtr.Zero) ? new DbvtNode(ptr) : null;
            }
            set { btDbvt_setFree(_native, (value != null) ? value._native : IntPtr.Zero); }
		}

		public int Leaves
		{
			get { return btDbvt_getLeaves(_native); }
			set { btDbvt_setLeaves(_native, value); }
		}

		public int Lkhd
		{
			get { return btDbvt_getLkhd(_native); }
			set { btDbvt_setLkhd(_native, value); }
		}

		public uint Opath
		{
			get { return btDbvt_getOpath(_native); }
			set { btDbvt_setOpath(_native, value); }
		}
        /*
		public AlignedObjectArray<DbvtNode> RayTestStack
		{
			get { return btDbvt_getRayTestStack(_native); }
		}
        */
		public DbvtNode Root
		{
            get
            {
                IntPtr ptr = btDbvt_getRoot(_native);
                return (ptr != IntPtr.Zero) ? new DbvtNode(ptr) : null;
            }
            set { btDbvt_setRoot(_native, (value != null) ? value._native : IntPtr.Zero); }
		}
        /*
        public AlignedStkNNArray StkStack
		{
			get { return btDbvt_getStkStack(_native); }
		}
        */
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
					btDbvt_delete(_native);
				}
				_native = IntPtr.Zero;
			}
		}

		~Dbvt()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDbvt_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btDbvt_allocate(IntPtr ifree, IntPtr stock, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvt_benchmark();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvt_clear(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvt_clone(IntPtr obj, IntPtr dest);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvt_clone2(IntPtr obj, IntPtr dest, IntPtr iclone);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern void btDbvt_collideKDOP(IntPtr root, [In] ref Vector3 normals, IntPtr offsets, int count, IntPtr policy);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern void btDbvt_collideOCL(IntPtr root, [In] ref Vector3 normals, IntPtr offsets, [In] ref Vector3 sortaxis, int count, IntPtr policy);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern void btDbvt_collideOCL2(IntPtr root, [In] ref Vector3 normals, IntPtr offsets, [In] ref Vector3 sortaxis, int count, IntPtr policy, bool fullsort);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern void btDbvt_collideTT(IntPtr obj, IntPtr root0, IntPtr root1, IntPtr policy);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern void btDbvt_collideTTpersistentStack(IntPtr obj, IntPtr root0, IntPtr root1, IntPtr policy);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern void btDbvt_collideTU(IntPtr root, IntPtr policy);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern void btDbvt_collideTV(IntPtr obj, IntPtr root, IntPtr volume, IntPtr policy);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btDbvt_countLeaves(IntPtr node);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btDbvt_empty(IntPtr obj);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern void btDbvt_enumLeaves(IntPtr root, IntPtr policy);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern void btDbvt_enumNodes(IntPtr root, IntPtr policy);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvt_extractLeaves(IntPtr node, IntPtr leaves);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDbvt_getFree(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btDbvt_getLeaves(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btDbvt_getLkhd(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern uint btDbvt_getOpath(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvt_getRayTestStack(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDbvt_getRoot(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvt_getStkStack(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDbvt_insert(IntPtr obj, IntPtr box, IntPtr data);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btDbvt_maxdepth(IntPtr node);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btDbvt_nearest(int[] i, IntPtr a, float v, int l, int h);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvt_optimizeBottomUp(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvt_optimizeIncremental(IntPtr obj, int passes);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvt_optimizeTopDown(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvt_optimizeTopDown2(IntPtr obj, int bu_treshold);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern void btDbvt_rayTest(IntPtr root, [In] ref Vector3 rayFrom, [In] ref Vector3 rayTo, IntPtr policy);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern void btDbvt_rayTestInternal(IntPtr obj, IntPtr root, [In] ref Vector3 rayFrom, [In] ref Vector3 rayTo, [In] ref Vector3 rayDirectionInverse, uint[] signs, float lambda_max, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax, IntPtr policy);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvt_remove(IntPtr obj, IntPtr leaf);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvt_setFree(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvt_setLeaves(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvt_setLkhd(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvt_setOpath(IntPtr obj, uint value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvt_setRoot(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvt_update(IntPtr obj, IntPtr leaf, IntPtr volume);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvt_update2(IntPtr obj, IntPtr leaf);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvt_update3(IntPtr obj, IntPtr leaf, int lookahead);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btDbvt_update4(IntPtr obj, IntPtr leaf, IntPtr volume, float margin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btDbvt_update5(IntPtr obj, IntPtr leaf, IntPtr volume, [In] ref Vector3 velocity);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btDbvt_update6(IntPtr obj, IntPtr leaf, IntPtr volume, [In] ref Vector3 velocity, float margin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvt_write(IntPtr obj, IntPtr iwriter);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvt_delete(IntPtr obj);
	}
}
