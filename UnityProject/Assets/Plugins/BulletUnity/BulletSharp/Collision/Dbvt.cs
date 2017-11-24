using System;
using BulletSharp.Math;


namespace BulletSharp
{
	public class DbvtAabbMm
	{
		internal IntPtr Native;

		internal DbvtAabbMm(IntPtr native)
		{
			Native = native;
		}

		public int Classify(Vector3 n, float o, int s)
		{
			return UnsafeNativeMethods.btDbvtAabbMm_Classify(Native, ref n, o, s);
		}

		public bool Contain(DbvtAabbMm a)
		{
			return UnsafeNativeMethods.btDbvtAabbMm_Contain(Native, a.Native);
		}

		public void Expand(Vector3 e)
		{
			UnsafeNativeMethods.btDbvtAabbMm_Expand(Native, ref e);
		}
		/*
		public static DbvtAabbMm FromCE(Vector3 c, Vector3 e)
		{
			return UnsafeNativeMethods.btDbvtAabbMm_FromCE(ref c, ref e);
		}

		public static DbvtAabbMm FromCR(Vector3 c, float r)
		{
			return UnsafeNativeMethods.btDbvtAabbMm_FromCR(ref c, r);
		}

		public static DbvtAabbMm FromMM(Vector3 mi, Vector3 mx)
		{
			return UnsafeNativeMethods.btDbvtAabbMm_FromMM(ref mi, ref mx);
		}

		public static DbvtAabbMm FromPoints(Vector3 ppts, int n)
		{
			return UnsafeNativeMethods.btDbvtAabbMm_FromPoints(ref ppts, n);
		}

		public static DbvtAabbMm FromPoints(Vector3 pts, int n)
		{
			return UnsafeNativeMethods.btDbvtAabbMm_FromPoints2(ref pts, n);
		}
		*/
		public float ProjectMinimum(Vector3 v, uint signs)
		{
			return UnsafeNativeMethods.btDbvtAabbMm_ProjectMinimum(Native, ref v, signs);
		}

		public void SignedExpand(Vector3 e)
		{
			UnsafeNativeMethods.btDbvtAabbMm_SignedExpand(Native, ref e);
		}

		public Vector3 Center
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btDbvtAabbMm_Center(Native, out value);
				return value;
			}
		}

		public Vector3 Extents
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btDbvtAabbMm_Extents(Native, out value);
				return value;
			}
		}

		public Vector3 Lengths
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btDbvtAabbMm_Lengths(Native, out value);
				return value;
			}
		}

		public Vector3 Maxs
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btDbvtAabbMm_Maxs(Native, out value);
				return value;
			}
		}

		public Vector3 Mins
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btDbvtAabbMm_Mins(Native, out value);
				return value;
			}
		}

		public Vector3 TMaxs
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btDbvtAabbMm_tMaxs(Native, out value);
				return value;
			}
		}

		public Vector3 TMins
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btDbvtAabbMm_tMins(Native, out value);
				return value;
			}
		}
	}

	public class DbvtNode
	{
		internal IntPtr Native;

		internal DbvtNode(IntPtr native)
		{
			Native = native;
		}
		/*

		public DbvtNodePtrArray Childs
		{
			get { return UnsafeNativeMethods.btDbvtNode_getChilds(Native); }
		}
		*/
		public IntPtr Data
		{
			get { return  UnsafeNativeMethods.btDbvtNode_getData(Native);}
			set {  UnsafeNativeMethods.btDbvtNode_setData(Native, value);}
		}

		public int DataAsInt
		{
			get { return  UnsafeNativeMethods.btDbvtNode_getDataAsInt(Native);}
			set {  UnsafeNativeMethods.btDbvtNode_setDataAsInt(Native, value);}
		}

		public bool IsInternal{ get { return  UnsafeNativeMethods.btDbvtNode_isinternal(Native);} }

		public bool Isleaf{ get { return  UnsafeNativeMethods.btDbvtNode_isleaf(Native);} }

		public DbvtNode Parent
		{
			get
			{
				IntPtr ptr = UnsafeNativeMethods.btDbvtNode_getParent(Native);
				return (ptr != IntPtr.Zero) ? new DbvtNode(ptr) : null;
			}
			set {  UnsafeNativeMethods.btDbvtNode_setParent(Native, (value != null) ? value.Native : IntPtr.Zero);}
		}

		public DbvtVolume Volume
		{
			get
			{
				IntPtr ptr = UnsafeNativeMethods.btDbvtNode_getVolume(Native);
				return (ptr != IntPtr.Zero) ? new DbvtVolume(ptr) : null;
			}
		}
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
			internal IntPtr Native;

			internal IClone(IntPtr native)
			{
				Native = native;
			}

			public IClone()
			{
				Native = UnsafeNativeMethods.btDbvt_IClone_new();
			}

			public void CloneLeaf(DbvtNode __unnamed0)
			{
				UnsafeNativeMethods.btDbvt_IClone_CloneLeaf(Native, __unnamed0.Native);
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
					UnsafeNativeMethods.btDbvt_IClone_delete(Native);
					Native = IntPtr.Zero;
				}
			}

			~IClone()
			{
				Dispose(false);
			}
		}

		public class ICollide : IDisposable
		{
			internal IntPtr Native;

			internal ICollide(IntPtr native)
			{
				Native = native;
			}

			public ICollide()
			{
				Native = UnsafeNativeMethods.btDbvt_ICollide_new();
			}

			public bool AllLeaves(DbvtNode __unnamed0)
			{
				return UnsafeNativeMethods.btDbvt_ICollide_AllLeaves(Native, __unnamed0.Native);
			}

			public bool Descent(DbvtNode __unnamed0)
			{
				return UnsafeNativeMethods.btDbvt_ICollide_Descent(Native, __unnamed0.Native);
			}

			public void Process(DbvtNode __unnamed0, DbvtNode __unnamed1)
			{
				UnsafeNativeMethods.btDbvt_ICollide_Process(Native, __unnamed0.Native, __unnamed1.Native);
			}

			public void Process(DbvtNode __unnamed0)
			{
				UnsafeNativeMethods.btDbvt_ICollide_Process2(Native, __unnamed0.Native);
			}

			public void Process(DbvtNode n, float __unnamed1)
			{
				UnsafeNativeMethods.btDbvt_ICollide_Process3(Native, n.Native, __unnamed1);
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
					UnsafeNativeMethods.btDbvt_ICollide_delete(Native);
					Native = IntPtr.Zero;
				}
			}

			~ICollide()
			{
				Dispose(false);
			}
		}

		public abstract class IWriter : IDisposable
		{
			internal IntPtr Native;

			internal IWriter(IntPtr native)
			{
				Native = native;
			}

			public void Prepare(DbvtNode root, int numnodes)
			{
				UnsafeNativeMethods.btDbvt_IWriter_Prepare(Native, root.Native, numnodes);
			}

			public void WriteLeaf(DbvtNode __unnamed0, int index, int parent)
			{
				UnsafeNativeMethods.btDbvt_IWriter_WriteLeaf(Native, __unnamed0.Native, index, parent);
			}

			public void WriteNode(DbvtNode __unnamed0, int index, int parent, int child0,
				int child1)
			{
				UnsafeNativeMethods.btDbvt_IWriter_WriteNode(Native, __unnamed0.Native, index, parent,
					child0, child1);
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
					UnsafeNativeMethods.btDbvt_IWriter_delete(Native);
					Native = IntPtr.Zero;
				}
			}

			~IWriter()
			{
				Dispose(false);
			}
		}

		public class StkCln : IDisposable
		{
			internal IntPtr Native;

			internal StkCln(IntPtr native)
			{
				Native = native;
			}

			public StkCln(DbvtNode n, DbvtNode p)
			{
				Native = UnsafeNativeMethods.btDbvt_sStkCLN_new(n.Native, p.Native);
			}

			public DbvtNode Node
			{
				get
				{
					IntPtr ptr = UnsafeNativeMethods.btDbvt_sStkCLN_getNode(Native);
					return (ptr != IntPtr.Zero) ? new DbvtNode(ptr) : null;
				}
				set {  UnsafeNativeMethods.btDbvt_sStkCLN_setNode(Native, (value != null) ? value.Native : IntPtr.Zero);}
			}

			public DbvtNode Parent
			{
				get
				{
					IntPtr ptr = UnsafeNativeMethods.btDbvt_sStkCLN_getParent(Native);
					return (ptr != IntPtr.Zero) ? new DbvtNode(ptr) : null;
				}
				set {  UnsafeNativeMethods.btDbvt_sStkCLN_setParent(Native, (value != null) ? value.Native : IntPtr.Zero);}
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
					UnsafeNativeMethods.btDbvt_sStkCLN_delete(Native);
					Native = IntPtr.Zero;
				}
			}

			~StkCln()
			{
				Dispose(false);
			}
		}

		public class StkNN : IDisposable
		{
			internal IntPtr Native;

			internal StkNN(IntPtr native)
			{
				Native = native;
			}

			public StkNN()
			{
				Native = UnsafeNativeMethods.btDbvt_sStkNN_new();
			}

			public StkNN(DbvtNode na, DbvtNode nb)
			{
				Native = UnsafeNativeMethods.btDbvt_sStkNN_new2(na.Native, nb.Native);
			}

			public DbvtNode A
			{
				get
				{
					IntPtr ptr = UnsafeNativeMethods.btDbvt_sStkNN_getA(Native);
					return (ptr != IntPtr.Zero) ? new DbvtNode(ptr) : null;
				}
				set {  UnsafeNativeMethods.btDbvt_sStkNN_setA(Native, (value != null) ? value.Native : IntPtr.Zero);}
			}

			public DbvtNode B
			{
				get
				{
					IntPtr ptr = UnsafeNativeMethods.btDbvt_sStkNN_getB(Native);
					return (ptr != IntPtr.Zero) ? new DbvtNode(ptr) : null;
				}
				set {  UnsafeNativeMethods.btDbvt_sStkNN_setB(Native, (value != null) ? value.Native : IntPtr.Zero);}
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
					UnsafeNativeMethods.btDbvt_sStkNN_delete(Native);
					Native = IntPtr.Zero;
				}
			}

			~StkNN()
			{
				Dispose(false);
			}
		}

		public class StkNP : IDisposable
		{
			internal IntPtr Native;

			internal StkNP(IntPtr native)
			{
				Native = native;
			}

			public StkNP(DbvtNode n, uint m)
			{
				Native = UnsafeNativeMethods.btDbvt_sStkNP_new(n.Native, m);
			}

			public int Mask
			{
				get { return  UnsafeNativeMethods.btDbvt_sStkNP_getMask(Native);}
				set {  UnsafeNativeMethods.btDbvt_sStkNP_setMask(Native, value);}
			}

			public DbvtNode Node
			{
				get
				{
					IntPtr ptr = UnsafeNativeMethods.btDbvt_sStkNP_getNode(Native);
					return (ptr != IntPtr.Zero) ? new DbvtNode(ptr) : null;
				}
				set {  UnsafeNativeMethods.btDbvt_sStkNP_setNode(Native, (value != null) ? value.Native : IntPtr.Zero);}
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
					UnsafeNativeMethods.btDbvt_sStkNP_delete(Native);
					Native = IntPtr.Zero;
				}
			}

			~StkNP()
			{
				Dispose(false);
			}
		}

		public class StkNps : IDisposable
		{
			internal IntPtr Native;

			internal StkNps(IntPtr native)
			{
				Native = native;
			}

			public StkNps()
			{
				Native = UnsafeNativeMethods.btDbvt_sStkNPS_new();
			}

			public StkNps(DbvtNode n, uint m, float v)
			{
				Native = UnsafeNativeMethods.btDbvt_sStkNPS_new2(n.Native, m, v);
			}

			public int Mask
			{
				get { return  UnsafeNativeMethods.btDbvt_sStkNPS_getMask(Native);}
				set {  UnsafeNativeMethods.btDbvt_sStkNPS_setMask(Native, value);}
			}

			public DbvtNode Node
			{
				get
				{
					IntPtr ptr = UnsafeNativeMethods.btDbvt_sStkNPS_getNode(Native);
					return (ptr != IntPtr.Zero) ? new DbvtNode(ptr) : null;
				}
				set {  UnsafeNativeMethods.btDbvt_sStkNPS_setNode(Native, (value != null) ? value.Native : IntPtr.Zero);}
			}

			public float Value
			{
				get { return  UnsafeNativeMethods.btDbvt_sStkNPS_getValue(Native);}
				set {  UnsafeNativeMethods.btDbvt_sStkNPS_setValue(Native, value);}
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
					UnsafeNativeMethods.btDbvt_sStkNPS_delete(Native);
					Native = IntPtr.Zero;
				}
			}

			~StkNps()
			{
				Dispose(false);
			}
		}

		internal IntPtr Native;
		bool _preventDelete;

		internal Dbvt(IntPtr native, bool preventDelete)
		{
			Native = native;
			_preventDelete = preventDelete;
		}

		public Dbvt()
		{
			Native = UnsafeNativeMethods.btDbvt_new();
		}
		/*
		public static int Allocate(AlignedIntArray ifree, AlignedStkNpsArray stock,
			StkNps value)
		{
			return UnsafeNativeMethods.btDbvt_allocate(ifree.Native, stock.Native, value.Native);
		}
		*/
		public static void Benchmark()
		{
			UnsafeNativeMethods.btDbvt_benchmark();
		}

		public void Clear()
		{
			UnsafeNativeMethods.btDbvt_clear(Native);
		}

		public void Clone(Dbvt dest)
		{
			UnsafeNativeMethods.btDbvt_clone(Native, dest.Native);
		}

		public void Clone(Dbvt dest, IClone iclone)
		{
			UnsafeNativeMethods.btDbvt_clone2(Native, dest.Native, iclone.Native);
		}
		/*
		public static void CollideKdop(DbvtNode root, Vector3 normals, float offsets,
			int count, ICollide policy)
		{
			btDbvt_collideKDOP(root.Native, normals.Native, offsets.Native, count,
				policy.Native);
		}

		public static void CollideOcl(DbvtNode root, Vector3 normals, float offsets,
			Vector3 sortaxis, int count, ICollide policy)
		{
			btDbvt_collideOCL(root.Native, normals.Native, offsets.Native, ref sortaxis,
				count, policy.Native);
		}

		public static void CollideOcl(DbvtNode root, Vector3 normals, float offsets,
			Vector3 sortaxis, int count, ICollide policy, bool fullsort)
		{
			btDbvt_collideOCL2(root.Native, normals.Native, offsets.Native, ref sortaxis,
				count, policy.Native, fullsort);
		}

		public void CollideTT(DbvtNode root0, DbvtNode root1, ICollide policy)
		{
			btDbvt_collideTT(Native, root0.Native, root1.Native, policy.Native);
		}

		public void CollideTTPersistentStack(DbvtNode root0, DbvtNode root1, ICollide policy)
		{
			btDbvt_collideTTpersistentStack(Native, root0.Native, root1.Native,
				policy.Native);
		}

		public static void CollideTU(DbvtNode root, ICollide policy)
		{
			btDbvt_collideTU(root.Native, policy.Native);
		}

		public void CollideTV(DbvtNode root, DbvtVolume volume, ICollide policy)
		{
			btDbvt_collideTV(Native, root.Native, volume.Native, policy.Native);
		}
		*/
		public static int CountLeaves(DbvtNode node)
		{
			return UnsafeNativeMethods.btDbvt_countLeaves(node.Native);
		}

		public bool Empty()
		{
			return UnsafeNativeMethods.btDbvt_empty(Native);
		}
		/*
		public static void EnumLeaves(DbvtNode root, ICollide policy)
		{
			btDbvt_enumLeaves(root.Native, policy.Native);
		}

		public static void EnumNodes(DbvtNode root, ICollide policy)
		{
			btDbvt_enumNodes(root.Native, policy.Native);
		}

		public static void ExtractLeaves(DbvtNode node, AlignedDbvtNodeArray leaves)
		{
			UnsafeNativeMethods.btDbvt_extractLeaves(node.Native, leaves.Native);
		}
		*/
		public DbvtNode Insert(DbvtVolume box, IntPtr data)
		{
			return new DbvtNode(UnsafeNativeMethods.btDbvt_insert(Native, box.Native, data));
		}

		public static int MaxDepth(DbvtNode node)
		{
			return UnsafeNativeMethods.btDbvt_maxdepth(node.Native);
		}

		public static int Nearest(int[] i, StkNps a, float v, int l, int h)
		{
			return UnsafeNativeMethods.btDbvt_nearest(i, a.Native, v, l, h);
		}

		public void OptimizeBottomUp()
		{
			UnsafeNativeMethods.btDbvt_optimizeBottomUp(Native);
		}

		public void OptimizeIncremental(int passes)
		{
			UnsafeNativeMethods.btDbvt_optimizeIncremental(Native, passes);
		}

		public void OptimizeTopDown()
		{
			UnsafeNativeMethods.btDbvt_optimizeTopDown(Native);
		}

		public void OptimizeTopDown(int buTreshold)
		{
			UnsafeNativeMethods.btDbvt_optimizeTopDown2(Native, buTreshold);
		}
		/*
		public static void RayTest(DbvtNode root, Vector3 rayFrom, Vector3 rayTo,
			ICollide policy)
		{
			btDbvt_rayTest(root.Native, ref rayFrom, ref rayTo, policy.Native);
		}

		public void RayTestInternal(DbvtNode root, Vector3 rayFrom, Vector3 rayTo,
			Vector3 rayDirectionInverse, UIntArray signs, float lambdaMax, Vector3 aabbMin,
			Vector3 aabbMax, ICollide policy)
		{
			btDbvt_rayTestInternal2(Native, root.Native, ref rayFrom, ref rayTo,
				ref rayDirectionInverse, signs.Native, lambdaMax, ref aabbMin, ref aabbMax,
				policy.Native);
		}
		*/
		public void Remove(DbvtNode leaf)
		{
			UnsafeNativeMethods.btDbvt_remove(Native, leaf.Native);
		}

		public void Update(DbvtNode leaf, DbvtVolume volume)
		{
			UnsafeNativeMethods.btDbvt_update(Native, leaf.Native, volume.Native);
		}

		public void Update(DbvtNode leaf)
		{
			UnsafeNativeMethods.btDbvt_update2(Native, leaf.Native);
		}

		public void Update(DbvtNode leaf, int lookahead)
		{
			UnsafeNativeMethods.btDbvt_update3(Native, leaf.Native, lookahead);
		}

		public bool Update(DbvtNode leaf, DbvtVolume volume, float margin)
		{
			return UnsafeNativeMethods.btDbvt_update4(Native, leaf.Native, volume.Native, margin);
		}

		public bool Update(DbvtNode leaf, DbvtVolume volume, Vector3 velocity)
		{
			return UnsafeNativeMethods.btDbvt_update5(Native, leaf.Native, volume.Native, ref velocity);
		}

		public bool Update(DbvtNode leaf, DbvtVolume volume, Vector3 velocity, float margin)
		{
			return UnsafeNativeMethods.btDbvt_update6(Native, leaf.Native, volume.Native, ref velocity,
				margin);
		}

		public void Write(IWriter iwriter)
		{
			UnsafeNativeMethods.btDbvt_write(Native, iwriter.Native);
		}

		public DbvtNode Free
		{
			get
			{
				IntPtr ptr = UnsafeNativeMethods.btDbvt_getFree(Native);
				return (ptr != IntPtr.Zero) ? new DbvtNode(ptr) : null;
			}
			set {  UnsafeNativeMethods.btDbvt_setFree(Native, (value != null) ? value.Native : IntPtr.Zero);}
		}

		public int Leaves
		{
			get { return  UnsafeNativeMethods.btDbvt_getLeaves(Native);}
			set {  UnsafeNativeMethods.btDbvt_setLeaves(Native, value);}
		}

		public int Lkhd
		{
			get { return  UnsafeNativeMethods.btDbvt_getLkhd(Native);}
			set {  UnsafeNativeMethods.btDbvt_setLkhd(Native, value);}
		}

		public uint Opath
		{
			get { return  UnsafeNativeMethods.btDbvt_getOpath(Native);}
			set {  UnsafeNativeMethods.btDbvt_setOpath(Native, value);}
		}

		public DbvtNode Root
		{
			get
			{
				IntPtr ptr = UnsafeNativeMethods.btDbvt_getRoot(Native);
				return (ptr != IntPtr.Zero) ? new DbvtNode(ptr) : null;
			}
			set {  UnsafeNativeMethods.btDbvt_setRoot(Native, (value != null) ? value.Native : IntPtr.Zero);}
		}
		/*
		public AlignedStkNNArray StkStack
		{
			get { return UnsafeNativeMethods.btDbvt_getStkStack(Native); }
		}
		*/
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
					UnsafeNativeMethods.btDbvt_delete(Native);
				}
				Native = IntPtr.Zero;
			}
		}

		~Dbvt()
		{
			Dispose(false);
		}
	}
}
