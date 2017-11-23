using BulletSharp.Math;
using System;


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
				IntPtr ptr = UnsafeNativeMethods.btDbvtProxy_getLeaf(Native);
				return (ptr != IntPtr.Zero) ? new DbvtNode(ptr) : null;
			}
			set => UnsafeNativeMethods.btDbvtProxy_setLeaf(Native, (value != null) ? value.Native : IntPtr.Zero);
		}

		//public DbvtProxyPtrArray Links => UnsafeNativeMethods.btDbvtProxy_getLinks(Native);

		public int Stage
		{
			get => UnsafeNativeMethods.btDbvtProxy_getStage(Native);
			set => UnsafeNativeMethods.btDbvtProxy_setStage(Native, value);
		}
	}

	public class DbvtBroadphase : BroadphaseInterface
	{
		public DbvtBroadphase(OverlappingPairCache pairCache = null)
			: base(UnsafeNativeMethods.btDbvtBroadphase_new((pairCache != null) ? pairCache.Native : IntPtr.Zero))
		{
			_overlappingPairCache = (pairCache != null) ? pairCache : new HashedOverlappingPairCache(
				UnsafeNativeMethods.btBroadphaseInterface_getOverlappingPairCache(Native), true);
		}

		public static void Benchmark(BroadphaseInterface broadphase)
		{
			UnsafeNativeMethods.btDbvtBroadphase_benchmark(broadphase.Native);
		}

		public void Collide(Dispatcher dispatcher)
		{
			UnsafeNativeMethods.btDbvtBroadphase_collide(Native, dispatcher.Native);
		}

		public override BroadphaseProxy CreateProxy(ref Vector3 aabbMin, ref Vector3 aabbMax, int shapeType, IntPtr userPtr, int collisionFilterGroup, int collisionFilterMask, Dispatcher dispatcher)
		{
			return new DbvtProxy(UnsafeNativeMethods.btBroadphaseInterface_createProxy(Native, ref aabbMin, ref aabbMax, shapeType, userPtr, collisionFilterGroup, collisionFilterMask, dispatcher.Native));
		}

		public void Optimize()
		{
			UnsafeNativeMethods.btDbvtBroadphase_optimize(Native);
		}

		public void PerformDeferredRemoval(Dispatcher dispatcher)
		{
			UnsafeNativeMethods.btDbvtBroadphase_performDeferredRemoval(Native, dispatcher.Native);
		}

		public void SetAabbForceUpdateRef(BroadphaseProxy absproxy, ref Vector3 aabbMin,
			ref Vector3 aabbMax, Dispatcher __unnamed3)
		{
			UnsafeNativeMethods.btDbvtBroadphase_setAabbForceUpdate(Native, absproxy.Native, ref aabbMin,
				ref aabbMax, __unnamed3.Native);
		}

		public void SetAabbForceUpdate(BroadphaseProxy absproxy, Vector3 aabbMin,
			Vector3 aabbMax, Dispatcher __unnamed3)
		{
			UnsafeNativeMethods.btDbvtBroadphase_setAabbForceUpdate(Native, absproxy.Native, ref aabbMin,
				ref aabbMax, __unnamed3.Native);
		}

		public int CId
		{
			get => UnsafeNativeMethods.btDbvtBroadphase_getCid(Native);
			set => UnsafeNativeMethods.btDbvtBroadphase_setCid(Native, value);
		}

		public int CUpdates
		{
			get => UnsafeNativeMethods.btDbvtBroadphase_getCupdates(Native);
			set => UnsafeNativeMethods.btDbvtBroadphase_setCupdates(Native, value);
		}

		public bool DeferredCollide
		{
			get => UnsafeNativeMethods.btDbvtBroadphase_getDeferedcollide(Native);
			set => UnsafeNativeMethods.btDbvtBroadphase_setDeferedcollide(Native, value);
		}

		public int DUpdates
		{
			get => UnsafeNativeMethods.btDbvtBroadphase_getDupdates(Native);
			set => UnsafeNativeMethods.btDbvtBroadphase_setDupdates(Native, value);
		}

		public int FixedLeft
		{
			get => UnsafeNativeMethods.btDbvtBroadphase_getFixedleft(Native);
			set => UnsafeNativeMethods.btDbvtBroadphase_setFixedleft(Native, value);
		}

		public int FUpdates
		{
			get => UnsafeNativeMethods.btDbvtBroadphase_getFupdates(Native);
			set => UnsafeNativeMethods.btDbvtBroadphase_setFupdates(Native, value);
		}

		public int GId
		{
			get => UnsafeNativeMethods.btDbvtBroadphase_getGid(Native);
			set => UnsafeNativeMethods.btDbvtBroadphase_setGid(Native, value);
		}

		public bool NeedCleanup
		{
			get => UnsafeNativeMethods.btDbvtBroadphase_getNeedcleanup(Native);
			set => UnsafeNativeMethods.btDbvtBroadphase_setNeedcleanup(Native, value);
		}

		public int NewPairs
		{
			get => UnsafeNativeMethods.btDbvtBroadphase_getNewpairs(Native);
			set => UnsafeNativeMethods.btDbvtBroadphase_setNewpairs(Native, value);
		}

		public OverlappingPairCache PairCache
		{
			get => OverlappingPairCache;
			set
			{
				_overlappingPairCache = value;
				UnsafeNativeMethods.btDbvtBroadphase_setPaircache(Native, value.Native);
			}
		}

		public int PId
		{
			get => UnsafeNativeMethods.btDbvtBroadphase_getPid(Native);
			set => UnsafeNativeMethods.btDbvtBroadphase_setPid(Native, value);
		}

		public float Prediction
		{
			get => UnsafeNativeMethods.btDbvtBroadphase_getPrediction(Native);
			set => UnsafeNativeMethods.btDbvtBroadphase_setPrediction(Native, value);
		}

		public bool ReleasePairCache
		{
			get => UnsafeNativeMethods.btDbvtBroadphase_getReleasepaircache(Native);
			set => UnsafeNativeMethods.btDbvtBroadphase_setReleasepaircache(Native, value);
		}

        //public DbvtArray Sets => UnsafeNativeMethods.btDbvtBroadphase_getSets(Native);

        public int StageCurrent
		{
			get => UnsafeNativeMethods.btDbvtBroadphase_getStageCurrent(Native);
			set => UnsafeNativeMethods.btDbvtBroadphase_setStageCurrent(Native, value);
		}

        //public DbvtProxyPtrArray StageRoots => UnsafeNativeMethods.btDbvtBroadphase_getStageRoots(Native);

        public uint UpdatesCall
		{
			get => UnsafeNativeMethods.btDbvtBroadphase_getUpdates_call(Native);
			set => UnsafeNativeMethods.btDbvtBroadphase_setUpdates_call(Native, value);
		}

		public uint UpdatesDone
		{
			get => UnsafeNativeMethods.btDbvtBroadphase_getUpdates_done(Native);
			set => UnsafeNativeMethods.btDbvtBroadphase_setUpdates_done(Native, value);
		}

		public float UpdatesRatio
		{
			get => UnsafeNativeMethods.btDbvtBroadphase_getUpdates_ratio(Native);
			set => UnsafeNativeMethods.btDbvtBroadphase_setUpdates_ratio(Native, value);
		}

		public float VelocityPrediction
		{
			get => UnsafeNativeMethods.btDbvtBroadphase_getVelocityPrediction(Native);
			set => UnsafeNativeMethods.btDbvtBroadphase_setVelocityPrediction(Native, value);
		}
	}
}
