using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class DbvtProxy : BroadphaseProxy
	{
		internal DbvtProxy(IntPtr native)
			: base(native)
		{
		}

		public DbvtNode Leaf
		{
            get
            {
                IntPtr ptr = btDbvtProxy_getLeaf(_native);
                return (ptr != IntPtr.Zero) ? new DbvtNode(ptr) : null;
            }
            set { btDbvtProxy_setLeaf(_native, (value != null) ? value._native : IntPtr.Zero); }
		}
        /*
		public DbvtProxyPtrArray Links
		{
			get { return btDbvtProxy_getLinks(_native); }
		}
        */
		public int Stage
		{
			get { return btDbvtProxy_getStage(_native); }
			set { btDbvtProxy_setStage(_native, value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDbvtProxy_getLeaf(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDbvtProxy_getLinks(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btDbvtProxy_getStage(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtProxy_setLeaf(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtProxy_setStage(IntPtr obj, int value);
	}

	public class DbvtBroadphase : BroadphaseInterface
	{
		public DbvtBroadphase()
			: base(btDbvtBroadphase_new())
		{
            _overlappingPairCache = new HashedOverlappingPairCache(btBroadphaseInterface_getOverlappingPairCache(_native), true);
		}

		public DbvtBroadphase(OverlappingPairCache pairCache)
            : base(btDbvtBroadphase_new2((pairCache != null) ? pairCache._native : IntPtr.Zero))
		{
            _overlappingPairCache = (pairCache != null) ? pairCache : new HashedOverlappingPairCache(
                btBroadphaseInterface_getOverlappingPairCache(_native), true);
		}

		public static void Benchmark(BroadphaseInterface broadphase)
		{
            btDbvtBroadphase_benchmark(broadphase._native);
		}

		public void Collide(Dispatcher dispatcher)
		{
			btDbvtBroadphase_collide(_native, dispatcher._native);
		}

        public override BroadphaseProxy CreateProxy(ref Vector3 aabbMin, ref Vector3 aabbMax, int shapeType, IntPtr userPtr, short collisionFilterGroup, short collisionFilterMask, Dispatcher dispatcher, IntPtr multiSapProxy)
        {
            return new DbvtProxy(btBroadphaseInterface_createProxy(_native, ref aabbMin, ref aabbMax, shapeType, userPtr, collisionFilterGroup, collisionFilterMask, dispatcher._native, multiSapProxy));
        }

		public void Optimize()
		{
			btDbvtBroadphase_optimize(_native);
		}

		public void PerformDeferredRemoval(Dispatcher dispatcher)
		{
			btDbvtBroadphase_performDeferredRemoval(_native, dispatcher._native);
		}

        public void SetAabbForceUpdate(BroadphaseProxy absproxy, ref Vector3 aabbMin, ref Vector3 aabbMax, Dispatcher __unnamed3)
		{
			btDbvtBroadphase_setAabbForceUpdate(_native, absproxy._native, ref aabbMin, ref aabbMax, __unnamed3._native);
		}

        public int CId
		{
			get { return btDbvtBroadphase_getCid(_native); }
			set { btDbvtBroadphase_setCid(_native, value); }
		}

        public int CUpdates
		{
			get { return btDbvtBroadphase_getCupdates(_native); }
			set { btDbvtBroadphase_setCupdates(_native, value); }
		}

        public bool DeferredCollide
		{
			get { return btDbvtBroadphase_getDeferedcollide(_native); }
			set { btDbvtBroadphase_setDeferedcollide(_native, value); }
		}

        public int DUpdates
		{
			get { return btDbvtBroadphase_getDupdates(_native); }
			set { btDbvtBroadphase_setDupdates(_native, value); }
		}

        public int FixedLeft
		{
			get { return btDbvtBroadphase_getFixedleft(_native); }
			set { btDbvtBroadphase_setFixedleft(_native, value); }
		}

        public int FUpdates
		{
			get { return btDbvtBroadphase_getFupdates(_native); }
			set { btDbvtBroadphase_setFupdates(_native, value); }
		}

        public int GId
		{
			get { return btDbvtBroadphase_getGid(_native); }
			set { btDbvtBroadphase_setGid(_native, value); }
		}

        public bool NeedCleanup
		{
			get { return btDbvtBroadphase_getNeedcleanup(_native); }
			set { btDbvtBroadphase_setNeedcleanup(_native, value); }
		}

        public int NewPairs
		{
			get { return btDbvtBroadphase_getNewpairs(_native); }
			set { btDbvtBroadphase_setNewpairs(_native, value); }
		}

        public OverlappingPairCache PairCache
		{
            get
            {
                return OverlappingPairCache;
            }
            set
            {
                _overlappingPairCache = value;
                btDbvtBroadphase_setPaircache(_native, value._native);
            }
		}

        public int PId
		{
			get { return btDbvtBroadphase_getPid(_native); }
			set { btDbvtBroadphase_setPid(_native, value); }
		}

		public float Prediction
		{
			get { return btDbvtBroadphase_getPrediction(_native); }
			set { btDbvtBroadphase_setPrediction(_native, value); }
		}

        public bool ReleasePairCache
		{
			get { return btDbvtBroadphase_getReleasepaircache(_native); }
			set { btDbvtBroadphase_setReleasepaircache(_native, value); }
		}
        /*
		public DbvtArray Sets
		{
			get { return btDbvtBroadphase_getSets(_native); }
		}
        */
		public int StageCurrent
		{
			get { return btDbvtBroadphase_getStageCurrent(_native); }
			set { btDbvtBroadphase_setStageCurrent(_native, value); }
		}
        /*
		public DbvtProxyPtrArray StageRoots
		{
			get { return btDbvtBroadphase_getStageRoots(_native); }
		}
        */
		public uint UpdatesCall
		{
			get { return btDbvtBroadphase_getUpdates_call(_native); }
			set { btDbvtBroadphase_setUpdates_call(_native, value); }
		}

		public uint UpdatesDone
		{
			get { return btDbvtBroadphase_getUpdates_done(_native); }
			set { btDbvtBroadphase_setUpdates_done(_native, value); }
		}

		public float UpdatesRatio
		{
			get { return btDbvtBroadphase_getUpdates_ratio(_native); }
			set { btDbvtBroadphase_setUpdates_ratio(_native, value); }
		}

		public float VelocityPrediction
		{
			get { return btDbvtBroadphase_getVelocityPrediction(_native); }
			set { btDbvtBroadphase_setVelocityPrediction(_native, value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDbvtBroadphase_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDbvtBroadphase_new2(IntPtr paircache);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_benchmark(IntPtr __unnamed0);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_collide(IntPtr obj, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btDbvtBroadphase_getCid(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btDbvtBroadphase_getCupdates(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btDbvtBroadphase_getDeferedcollide(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btDbvtBroadphase_getDupdates(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btDbvtBroadphase_getFixedleft(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btDbvtBroadphase_getFupdates(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btDbvtBroadphase_getGid(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btDbvtBroadphase_getNeedcleanup(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btDbvtBroadphase_getNewpairs(IntPtr obj);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern IntPtr btDbvtBroadphase_getPaircache(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btDbvtBroadphase_getPid(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btDbvtBroadphase_getPrediction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btDbvtBroadphase_getReleasepaircache(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDbvtBroadphase_getSets(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btDbvtBroadphase_getStageCurrent(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDbvtBroadphase_getStageRoots(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern uint btDbvtBroadphase_getUpdates_call(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern uint btDbvtBroadphase_getUpdates_done(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btDbvtBroadphase_getUpdates_ratio(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btDbvtBroadphase_getVelocityPrediction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_optimize(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_performDeferredRemoval(IntPtr obj, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setAabbForceUpdate(IntPtr obj, IntPtr absproxy, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax, IntPtr __unnamed3);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setCid(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setCupdates(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setDeferedcollide(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setDupdates(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setFixedleft(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setFupdates(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setGid(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setNeedcleanup(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setNewpairs(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setPaircache(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setPid(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setPrediction(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setReleasepaircache(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setStageCurrent(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setUpdates_call(IntPtr obj, uint value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setUpdates_done(IntPtr obj, uint value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setUpdates_ratio(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setVelocityPrediction(IntPtr obj, float prediction);
	}
}
