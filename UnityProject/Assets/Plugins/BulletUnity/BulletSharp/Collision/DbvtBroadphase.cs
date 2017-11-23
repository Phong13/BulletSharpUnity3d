using BulletSharp.Math;
using System;
using static BulletSharp.UnsafeNativeMethods;

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
				IntPtr ptr = btDbvtProxy_getLeaf(Native);
				return (ptr != IntPtr.Zero) ? new DbvtNode(ptr) : null;
			}
			set => btDbvtProxy_setLeaf(Native, (value != null) ? value.Native : IntPtr.Zero);
		}

		//public DbvtProxyPtrArray Links => btDbvtProxy_getLinks(Native);

		public int Stage
		{
			get => btDbvtProxy_getStage(Native);
			set => btDbvtProxy_setStage(Native, value);
		}
	}

	public class DbvtBroadphase : BroadphaseInterface
	{
		public DbvtBroadphase(OverlappingPairCache pairCache = null)
			: base(btDbvtBroadphase_new((pairCache != null) ? pairCache.Native : IntPtr.Zero))
		{
			_overlappingPairCache = (pairCache != null) ? pairCache : new HashedOverlappingPairCache(
				btBroadphaseInterface_getOverlappingPairCache(Native), true);
		}

		public static void Benchmark(BroadphaseInterface broadphase)
		{
			btDbvtBroadphase_benchmark(broadphase.Native);
		}

		public void Collide(Dispatcher dispatcher)
		{
			btDbvtBroadphase_collide(Native, dispatcher.Native);
		}

		public override BroadphaseProxy CreateProxy(ref Vector3 aabbMin, ref Vector3 aabbMax, int shapeType, IntPtr userPtr, int collisionFilterGroup, int collisionFilterMask, Dispatcher dispatcher)
		{
			return new DbvtProxy(btBroadphaseInterface_createProxy(Native, ref aabbMin, ref aabbMax, shapeType, userPtr, collisionFilterGroup, collisionFilterMask, dispatcher.Native));
		}

		public void Optimize()
		{
			btDbvtBroadphase_optimize(Native);
		}

		public void PerformDeferredRemoval(Dispatcher dispatcher)
		{
			btDbvtBroadphase_performDeferredRemoval(Native, dispatcher.Native);
		}

		public void SetAabbForceUpdateRef(BroadphaseProxy absproxy, ref Vector3 aabbMin,
			ref Vector3 aabbMax, Dispatcher __unnamed3)
		{
			btDbvtBroadphase_setAabbForceUpdate(Native, absproxy.Native, ref aabbMin,
				ref aabbMax, __unnamed3.Native);
		}

		public void SetAabbForceUpdate(BroadphaseProxy absproxy, Vector3 aabbMin,
			Vector3 aabbMax, Dispatcher __unnamed3)
		{
			btDbvtBroadphase_setAabbForceUpdate(Native, absproxy.Native, ref aabbMin,
				ref aabbMax, __unnamed3.Native);
		}

		public int CId
		{
			get => btDbvtBroadphase_getCid(Native);
			set => btDbvtBroadphase_setCid(Native, value);
		}

		public int CUpdates
		{
			get => btDbvtBroadphase_getCupdates(Native);
			set => btDbvtBroadphase_setCupdates(Native, value);
		}

		public bool DeferredCollide
		{
			get => btDbvtBroadphase_getDeferedcollide(Native);
			set => btDbvtBroadphase_setDeferedcollide(Native, value);
		}

		public int DUpdates
		{
			get => btDbvtBroadphase_getDupdates(Native);
			set => btDbvtBroadphase_setDupdates(Native, value);
		}

		public int FixedLeft
		{
			get => btDbvtBroadphase_getFixedleft(Native);
			set => btDbvtBroadphase_setFixedleft(Native, value);
		}

		public int FUpdates
		{
			get => btDbvtBroadphase_getFupdates(Native);
			set => btDbvtBroadphase_setFupdates(Native, value);
		}

		public int GId
		{
			get => btDbvtBroadphase_getGid(Native);
			set => btDbvtBroadphase_setGid(Native, value);
		}

		public bool NeedCleanup
		{
			get => btDbvtBroadphase_getNeedcleanup(Native);
			set => btDbvtBroadphase_setNeedcleanup(Native, value);
		}

		public int NewPairs
		{
			get => btDbvtBroadphase_getNewpairs(Native);
			set => btDbvtBroadphase_setNewpairs(Native, value);
		}

		public OverlappingPairCache PairCache
		{
			get => OverlappingPairCache;
			set
			{
				_overlappingPairCache = value;
				btDbvtBroadphase_setPaircache(Native, value.Native);
			}
		}

		public int PId
		{
			get => btDbvtBroadphase_getPid(Native);
			set => btDbvtBroadphase_setPid(Native, value);
		}

		public float Prediction
		{
			get => btDbvtBroadphase_getPrediction(Native);
			set => btDbvtBroadphase_setPrediction(Native, value);
		}

		public bool ReleasePairCache
		{
			get => btDbvtBroadphase_getReleasepaircache(Native);
			set => btDbvtBroadphase_setReleasepaircache(Native, value);
		}

        //public DbvtArray Sets => btDbvtBroadphase_getSets(Native);

        public int StageCurrent
		{
			get => btDbvtBroadphase_getStageCurrent(Native);
			set => btDbvtBroadphase_setStageCurrent(Native, value);
		}

        //public DbvtProxyPtrArray StageRoots => btDbvtBroadphase_getStageRoots(Native);

        public uint UpdatesCall
		{
			get => btDbvtBroadphase_getUpdates_call(Native);
			set => btDbvtBroadphase_setUpdates_call(Native, value);
		}

		public uint UpdatesDone
		{
			get => btDbvtBroadphase_getUpdates_done(Native);
			set => btDbvtBroadphase_setUpdates_done(Native, value);
		}

		public float UpdatesRatio
		{
			get => btDbvtBroadphase_getUpdates_ratio(Native);
			set => btDbvtBroadphase_setUpdates_ratio(Native, value);
		}

		public float VelocityPrediction
		{
			get => btDbvtBroadphase_getVelocityPrediction(Native);
			set => btDbvtBroadphase_setVelocityPrediction(Native, value);
		}
	}
}
