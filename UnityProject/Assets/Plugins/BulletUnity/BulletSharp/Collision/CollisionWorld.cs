using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;
using AOT;

namespace BulletSharp
{
	public class AllHitsRayResultCallback : RayResultCallback
	{
		public AllHitsRayResultCallback(Vector3 rayFromWorld, Vector3 rayToWorld)
		{
			RayFromWorld = rayFromWorld;
			RayToWorld = rayToWorld;

			CollisionObjects = new List<CollisionObject>();
			HitFractions = new List<float>();
			HitNormalWorld = new List<Vector3>();
			HitPointWorld = new List<Vector3>();
		}

		public override float AddSingleResult(LocalRayResult rayResult, bool normalInWorldSpace)
		{
			CollisionObject = rayResult.CollisionObject;
			CollisionObjects.Add(rayResult.CollisionObject);
			if (normalInWorldSpace)
			{
				HitNormalWorld.Add(rayResult.HitNormalLocal);
			}
			else
			{
				// need to transform normal into worldspace
				HitNormalWorld.Add(Vector3.TransformCoordinate(rayResult.HitNormalLocal, CollisionObject.WorldTransform.Basis));
			}
			HitPointWorld.Add(Vector3.Lerp(RayFromWorld, RayToWorld, rayResult.HitFraction));
			HitFractions.Add(rayResult.HitFraction);
			return ClosestHitFraction;
		}

		public List<CollisionObject> CollisionObjects { get; set; }
		public List<float> HitFractions { get; set; }
		public List<Vector3> HitNormalWorld { get; set; }
		public List<Vector3> HitPointWorld { get; set; }
		public Vector3 RayFromWorld { get; set; }
		public Vector3 RayToWorld { get; set; }
	}

	public class ClosestConvexResultCallback : ConvexResultCallback
	{
		public ClosestConvexResultCallback()
		{
		}

		public ClosestConvexResultCallback(ref Vector3 convexFromWorld, ref Vector3 convexToWorld)
		{
			ConvexFromWorld = convexFromWorld;
			ConvexToWorld = convexToWorld;
		}

		public override float AddSingleResult(LocalConvexResult convexResult, bool normalInWorldSpace)
		{
			//caller already does the filter on the m_closestHitFraction
			Debug.Assert(convexResult.HitFraction <= ClosestHitFraction);

			ClosestHitFraction = convexResult.HitFraction;
			HitCollisionObject = convexResult.HitCollisionObject;
			if (normalInWorldSpace)
			{
				HitNormalWorld = convexResult.HitNormalLocal;
			}
			else
			{
				// need to transform normal into worldspace
				HitNormalWorld = Vector3.TransformCoordinate(convexResult.HitNormalLocal, HitCollisionObject.WorldTransform.Basis);
			}
			HitPointWorld = convexResult.HitPointLocal;
			return convexResult.HitFraction;
		}

		public Vector3 ConvexFromWorld { get; set; }
		public Vector3 ConvexToWorld { get; set; }
		public CollisionObject HitCollisionObject { get; set; }
		public Vector3 HitNormalWorld { get; set; }
		public Vector3 HitPointWorld { get; set; }
	}

	public class ClosestRayResultCallback : RayResultCallback
	{
		public ClosestRayResultCallback(ref Vector3 rayFromWorld, ref Vector3 rayToWorld)
		{
			RayFromWorld = rayFromWorld;
			RayToWorld = rayToWorld;
		}

		public override float AddSingleResult(LocalRayResult rayResult, bool normalInWorldSpace)
		{
			//caller already does the filter on the m_closestHitFraction
			Debug.Assert(rayResult.HitFraction <= ClosestHitFraction);

			ClosestHitFraction = rayResult.HitFraction;
			CollisionObject = rayResult.CollisionObject;
			if (normalInWorldSpace)
			{
				HitNormalWorld = rayResult.HitNormalLocal;
			}
			else
			{
				// need to transform normal into worldspace
				HitNormalWorld = Vector3.TransformCoordinate(rayResult.HitNormalLocal, CollisionObject.WorldTransform.Basis);
			}
			HitPointWorld = Vector3.Lerp(RayFromWorld, RayToWorld, rayResult.HitFraction);
			return rayResult.HitFraction;
		}

		public Vector3 RayFromWorld { get; set; } //used to calculate hitPointWorld from hitFraction
		public Vector3 RayToWorld { get; set; }

		public Vector3 HitNormalWorld { get; set; }
		public Vector3 HitPointWorld { get; set; }
	}

	public abstract class ContactResultCallback : IDisposable
	{
		internal IntPtr Native;

		[UnmanagedFunctionPointer(BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
		private delegate float AddSingleResultUnmanagedDelegate(IntPtr thisPtr, IntPtr cp, IntPtr colObj0Wrap, int partId0, int index0, IntPtr colObj1Wrap, int partId1, int index1);
		[UnmanagedFunctionPointer(BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
		private delegate bool NeedsCollisionUnmanagedDelegate(IntPtr thisPtr, IntPtr proxy0);

		private AddSingleResultUnmanagedDelegate _addSingleResult;
		private NeedsCollisionUnmanagedDelegate _needsCollision;

		public ContactResultCallback()
		{
			_addSingleResult = AddSingleResultUnmanaged;
			_needsCollision = NeedsCollisionUnmanaged;
            GCHandle handle = GCHandle.Alloc(this, GCHandleType.Normal);
            Native = UnsafeNativeMethods.btCollisionWorld_ContactResultCallbackWrapper_new(
				Marshal.GetFunctionPointerForDelegate(_addSingleResult),
				Marshal.GetFunctionPointerForDelegate(_needsCollision),
                GCHandle.ToIntPtr(handle));
		}

        [MonoPInvokeCallback(typeof(AddSingleResultUnmanagedDelegate))]
        static private float AddSingleResultUnmanaged(IntPtr thisPtr, IntPtr cp, IntPtr colObj0Wrap, int partId0, int index0, IntPtr colObj1Wrap, int partId1, int index1)
		{
            ContactResultCallback ai = GCHandle.FromIntPtr(thisPtr).Target as ContactResultCallback;
            return ai.AddSingleResult(new ManifoldPoint(cp, true),
				new CollisionObjectWrapper(colObj0Wrap), partId0, index0,
				new CollisionObjectWrapper(colObj1Wrap), partId1, index1);
		}

		public abstract float AddSingleResult(ManifoldPoint cp, CollisionObjectWrapper colObj0Wrap, int partId0, int index0, CollisionObjectWrapper colObj1Wrap, int partId1, int index1);

        [MonoPInvokeCallback(typeof(NeedsCollisionUnmanagedDelegate))]
        static private bool NeedsCollisionUnmanaged(IntPtr thisPtr, IntPtr proxy0)
		{
            ContactResultCallback ai = GCHandle.FromIntPtr(thisPtr).Target as ContactResultCallback;
            return ai.NeedsCollision(BroadphaseProxy.GetManaged(proxy0));
		}

		public virtual bool NeedsCollision(BroadphaseProxy proxy0)
		{
			return UnsafeNativeMethods.btCollisionWorld_ContactResultCallbackWrapper_needsCollision(Native, proxy0.Native);
		}

		public float ClosestDistanceThreshold
		{
			get { return UnsafeNativeMethods.btCollisionWorld_ContactResultCallback_getClosestDistanceThreshold(Native); } 
			set { UnsafeNativeMethods.btCollisionWorld_ContactResultCallback_setClosestDistanceThreshold(Native, value); }
		}

		public int CollisionFilterGroup
		{
			get { return UnsafeNativeMethods.btCollisionWorld_ContactResultCallback_getCollisionFilterGroup(Native); } 
			set { UnsafeNativeMethods.btCollisionWorld_ContactResultCallback_setCollisionFilterGroup(Native, value); }
		}

		public int CollisionFilterMask
		{
			get { return UnsafeNativeMethods.btCollisionWorld_ContactResultCallback_getCollisionFilterMask(Native); } 
			set { UnsafeNativeMethods.btCollisionWorld_ContactResultCallback_setCollisionFilterMask(Native, value); }
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
                UnsafeNativeMethods.btCollisionWorld_ContactResultCallback_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~ContactResultCallback()
		{
			Dispose(false);
		}
	}

	public abstract class ConvexResultCallback : IDisposable
	{
		internal IntPtr Native;

		[UnmanagedFunctionPointer(BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
		private delegate float AddSingleResultUnmanagedDelegate(IntPtr thisPtr, IntPtr convexResult, bool normalInWorldSpace);
		[UnmanagedFunctionPointer(BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
		private delegate bool NeedsCollisionUnmanagedDelegate(IntPtr thisPtr, IntPtr proxy0);

		private AddSingleResultUnmanagedDelegate _addSingleResult;
		private NeedsCollisionUnmanagedDelegate _needsCollision;

		protected ConvexResultCallback()
		{
			_addSingleResult = AddSingleResultUnmanaged;
			_needsCollision = NeedsCollisionUnmanaged;
            GCHandle handle = GCHandle.Alloc(this, GCHandleType.Normal);
            Native = UnsafeNativeMethods.btCollisionWorld_ConvexResultCallbackWrapper_new(
				Marshal.GetFunctionPointerForDelegate(_addSingleResult),
				Marshal.GetFunctionPointerForDelegate(_needsCollision),
                GCHandle.ToIntPtr(handle));
		}

        [MonoPInvokeCallback(typeof(AddSingleResultUnmanagedDelegate))]
        static private float AddSingleResultUnmanaged(IntPtr ptrThis, IntPtr convexResult, bool normalInWorldSpace)
		{
            ConvexResultCallback ptr = GCHandle.FromIntPtr(ptrThis).Target as ConvexResultCallback;
            return ptr.AddSingleResult(new LocalConvexResult(convexResult), normalInWorldSpace);
		}

		public abstract float AddSingleResult(LocalConvexResult convexResult, bool normalInWorldSpace);

        [MonoPInvokeCallback(typeof(NeedsCollisionUnmanagedDelegate))]
        static private bool NeedsCollisionUnmanaged(IntPtr ptrThis, IntPtr proxy0)
		{
            ConvexResultCallback ptr = GCHandle.FromIntPtr(ptrThis).Target as ConvexResultCallback;
            return ptr.NeedsCollision(BroadphaseProxy.GetManaged(proxy0));
		}

		public virtual bool NeedsCollision(BroadphaseProxy proxy0)
		{
			return UnsafeNativeMethods.btCollisionWorld_ConvexResultCallbackWrapper_needsCollision(Native,
				proxy0.Native);
		}

		public float ClosestHitFraction
		{
			get { return UnsafeNativeMethods.btCollisionWorld_ConvexResultCallback_getClosestHitFraction(Native); } 
			set { UnsafeNativeMethods.btCollisionWorld_ConvexResultCallback_setClosestHitFraction(Native, value); }
		}

		public int CollisionFilterGroup
		{
			get { return UnsafeNativeMethods.btCollisionWorld_ConvexResultCallback_getCollisionFilterGroup(Native); } 
			set { UnsafeNativeMethods.btCollisionWorld_ConvexResultCallback_setCollisionFilterGroup(Native, value); }
		}

		public int CollisionFilterMask
		{
			get { return UnsafeNativeMethods.btCollisionWorld_ConvexResultCallback_getCollisionFilterMask(Native); } 
			set { UnsafeNativeMethods.btCollisionWorld_ConvexResultCallback_setCollisionFilterMask(Native, value); }
		}

		public bool HasHit { get { return UnsafeNativeMethods.btCollisionWorld_ConvexResultCallback_hasHit(Native); } }

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
                UnsafeNativeMethods.btCollisionWorld_ConvexResultCallback_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~ConvexResultCallback()
		{
			Dispose(false);
		}
	}

	public class LocalConvexResult : IDisposable
	{
		internal IntPtr Native;
		private bool _preventDelete;

		private CollisionObject _hitCollisionObject;
		private LocalShapeInfo _localShapeInfo;

		internal LocalConvexResult(IntPtr native)
		{
			Native = native;
			_preventDelete = true;
			_hitCollisionObject = CollisionObject.GetManaged(UnsafeNativeMethods.btCollisionWorld_LocalConvexResult_getHitCollisionObject(Native));
			_localShapeInfo = new LocalShapeInfo(UnsafeNativeMethods.btCollisionWorld_LocalConvexResult_getLocalShapeInfo(Native), true);
		}

		public LocalConvexResult(CollisionObject hitCollisionObject, LocalShapeInfo localShapeInfo,
			Vector3 hitNormalLocal, Vector3 hitPointLocal, float hitFraction)
		{
			Native = UnsafeNativeMethods.btCollisionWorld_LocalConvexResult_new(hitCollisionObject.Native,
				localShapeInfo.Native, ref hitNormalLocal, ref hitPointLocal,
				hitFraction);
			_hitCollisionObject = hitCollisionObject;
			_localShapeInfo = localShapeInfo;
		}

		public CollisionObject HitCollisionObject
		{
			get { return _hitCollisionObject; } 
			set
			{
                        UnsafeNativeMethods.btCollisionWorld_LocalConvexResult_setHitCollisionObject(Native, value.Native);
				_hitCollisionObject = value;
			}
		}

		public float HitFraction
		{
			get { return UnsafeNativeMethods.btCollisionWorld_LocalConvexResult_getHitFraction(Native); }
			set { UnsafeNativeMethods.btCollisionWorld_LocalConvexResult_setHitFraction(Native, value); }
		}

		public Vector3 HitNormalLocal
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btCollisionWorld_LocalConvexResult_getHitNormalLocal(Native, out value);
				return value;
			}
			set { UnsafeNativeMethods.btCollisionWorld_LocalConvexResult_setHitNormalLocal(Native, ref value); }
		}

		public Vector3 HitPointLocal
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btCollisionWorld_LocalConvexResult_getHitPointLocal(Native, out value);
				return value;
			}
			set { UnsafeNativeMethods.btCollisionWorld_LocalConvexResult_setHitPointLocal(Native, ref value); }
		}

		public LocalShapeInfo LocalShapeInfo
		{
			get { return _localShapeInfo; }
			set
			{
				UnsafeNativeMethods.btCollisionWorld_LocalConvexResult_setLocalShapeInfo(Native, (value != null) ? value.Native : IntPtr.Zero);
				_localShapeInfo = value;
			}
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
					UnsafeNativeMethods.btCollisionWorld_LocalConvexResult_delete(Native);
				}
				Native = IntPtr.Zero;
			}
		}

		~LocalConvexResult()
		{
			Dispose(false);
		}
	}

	public class LocalRayResult : IDisposable
	{
		internal IntPtr Native;
		private bool _preventDelete;

		private CollisionObject _collisionObject;
		private LocalShapeInfo _localShapeInfo;

		internal LocalRayResult(IntPtr native)
		{
			Native = native;
			_preventDelete = true;
			_collisionObject = CollisionObject.GetManaged(UnsafeNativeMethods.btCollisionWorld_LocalRayResult_getCollisionObject(Native));
			_localShapeInfo = new LocalShapeInfo(UnsafeNativeMethods.btCollisionWorld_LocalRayResult_getLocalShapeInfo(Native), true);
		}

		public LocalRayResult(CollisionObject collisionObject, LocalShapeInfo localShapeInfo,
			Vector3 hitNormalLocal, float hitFraction)
		{
			Native = UnsafeNativeMethods.btCollisionWorld_LocalRayResult_new(collisionObject.Native,
				localShapeInfo.Native, ref hitNormalLocal, hitFraction);
			_collisionObject = collisionObject;
			_localShapeInfo = localShapeInfo;
		}

		public CollisionObject CollisionObject
		{
			get { return _collisionObject; }
			set
			{
				UnsafeNativeMethods.btCollisionWorld_LocalRayResult_setCollisionObject(Native, value.Native);
				_collisionObject = value;
			}
		}

		public float HitFraction
		{
			get { return UnsafeNativeMethods.btCollisionWorld_LocalRayResult_getHitFraction(Native); }
			set { UnsafeNativeMethods.btCollisionWorld_LocalRayResult_setHitFraction(Native, value); }
		}

		public Vector3 HitNormalLocal
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btCollisionWorld_LocalRayResult_getHitNormalLocal(Native, out value);
				return value;
			}
			set { UnsafeNativeMethods.btCollisionWorld_LocalRayResult_setHitNormalLocal(Native, ref value); }
		}

		public LocalShapeInfo LocalShapeInfo
		{
			get { return _localShapeInfo; }
			set
			{
				UnsafeNativeMethods.btCollisionWorld_LocalRayResult_setLocalShapeInfo(Native, (value != null) ? value.Native : IntPtr.Zero);
				_localShapeInfo = value;
			}
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
					UnsafeNativeMethods.btCollisionWorld_LocalRayResult_delete(Native);
				}
				Native = IntPtr.Zero;
			}
		}

		~LocalRayResult()
		{
			Dispose(false);
		}
	}

	public class LocalShapeInfo : IDisposable
	{
		internal IntPtr Native;
		bool _preventDelete;

		internal LocalShapeInfo(IntPtr native, bool preventDelete)
		{
			Native = native;
			_preventDelete = preventDelete;
		}

		public LocalShapeInfo()
		{
			Native = UnsafeNativeMethods.btCollisionWorld_LocalShapeInfo_new();
		}

		public int ShapePart
		{
			get { return UnsafeNativeMethods.btCollisionWorld_LocalShapeInfo_getShapePart(Native); }
			set { UnsafeNativeMethods.btCollisionWorld_LocalShapeInfo_setShapePart(Native, value); }
		}

		public int TriangleIndex
		{
			get { return UnsafeNativeMethods.btCollisionWorld_LocalShapeInfo_getTriangleIndex(Native); }
			set { UnsafeNativeMethods.btCollisionWorld_LocalShapeInfo_setTriangleIndex(Native, value); }
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
					UnsafeNativeMethods.btCollisionWorld_LocalShapeInfo_delete(Native);
				}
				Native = IntPtr.Zero;
			}
		}

		~LocalShapeInfo()
		{
			Dispose(false);
		}
	}

	public abstract class RayResultCallback : IDisposable
	{
		internal IntPtr Native;

		[UnmanagedFunctionPointer(BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
		private delegate float AddSingleResultUnmanagedDelegate(IntPtr thisPtr, IntPtr rayResult, bool normalInWorldSpace);
		[UnmanagedFunctionPointer(BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
		private delegate bool NeedsCollisionUnmanagedDelegate(IntPtr thisPtr, IntPtr proxy0);

		private AddSingleResultUnmanagedDelegate _addSingleResult;
		private NeedsCollisionUnmanagedDelegate _needsCollision;

		protected RayResultCallback()
		{
 
			_needsCollision = NeedsCollisionUnmanaged;
            GCHandle handle = GCHandle.Alloc(this, GCHandleType.Normal);
            Native = UnsafeNativeMethods.btCollisionWorld_RayResultCallbackWrapper_new(
				Marshal.GetFunctionPointerForDelegate(_addSingleResult),
				Marshal.GetFunctionPointerForDelegate(_needsCollision),
                GCHandle.ToIntPtr(handle));
		}

        [MonoPInvokeCallback(typeof(AddSingleResultUnmanagedDelegate))]
        static private float AddSingleResultUnmanaged(IntPtr thisPtr, IntPtr rayResult, bool normalInWorldSpace)
		{
            RayResultCallback ai = GCHandle.FromIntPtr(thisPtr).Target as RayResultCallback;
            return ai.AddSingleResult(new LocalRayResult(rayResult), normalInWorldSpace);
		}

		public abstract float AddSingleResult(LocalRayResult rayResult, bool normalInWorldSpace);

        [MonoPInvokeCallback(typeof(NeedsCollisionUnmanagedDelegate))]
        static private bool NeedsCollisionUnmanaged(IntPtr thisPtr, IntPtr proxy0)
		{
            RayResultCallback ai = GCHandle.FromIntPtr(thisPtr).Target as RayResultCallback;
            return ai.NeedsCollision(BroadphaseProxy.GetManaged(proxy0));
		}

		public virtual bool NeedsCollision(BroadphaseProxy proxy0)
		{
			return UnsafeNativeMethods.btCollisionWorld_RayResultCallbackWrapper_needsCollision(Native, proxy0.Native);
		}

		public float ClosestHitFraction
		{
			get { return UnsafeNativeMethods.btCollisionWorld_RayResultCallback_getClosestHitFraction(Native); }
			set { UnsafeNativeMethods.btCollisionWorld_RayResultCallback_setClosestHitFraction(Native, value); }
		}

		public int CollisionFilterGroup
		{
			get { return UnsafeNativeMethods.btCollisionWorld_RayResultCallback_getCollisionFilterGroup(Native); }
			set { UnsafeNativeMethods.btCollisionWorld_RayResultCallback_setCollisionFilterGroup(Native, value); }
		}

		public int CollisionFilterMask
		{
			get { return UnsafeNativeMethods.btCollisionWorld_RayResultCallback_getCollisionFilterMask(Native); }
			set { UnsafeNativeMethods.btCollisionWorld_RayResultCallback_setCollisionFilterMask(Native, value); }
		}

		public CollisionObject CollisionObject
		{
			get { return CollisionObject.GetManaged(UnsafeNativeMethods.btCollisionWorld_RayResultCallback_getCollisionObject(Native)); }
			set { UnsafeNativeMethods.btCollisionWorld_RayResultCallback_setCollisionObject(Native, (value != null) ? value.Native : IntPtr.Zero); }
		}

		public uint Flags
		{
			get { return UnsafeNativeMethods.btCollisionWorld_RayResultCallback_getFlags(Native); }
			set { UnsafeNativeMethods.btCollisionWorld_RayResultCallback_setFlags(Native, value); }
		}

		public bool HasHit { get { return UnsafeNativeMethods.btCollisionWorld_RayResultCallback_hasHit(Native); } }

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				UnsafeNativeMethods.btCollisionWorld_RayResultCallback_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~RayResultCallback()
		{
			Dispose(false);
		}
	}

	public class CollisionWorld : IDisposable
	{
		internal IntPtr Native;

		internal IDebugDraw _debugDrawer;
		protected BroadphaseInterface _broadphase;
        protected Dispatcher _dispatcher;
        protected DispatcherInfo _dispatchInfo;

        /// <summary>
        /// Use this static factory method to create instances instead of the Constructors.
        /// 
        /// It is tricky to use constructors to create CollisionWorld and its derived types, because, idealy we want to
        /// initialize all the fields, then create a native instance of the appropriate type, then create anything else that needs
        /// a reference to the native type. This ordered structure is hard to control when base class constructors are
        /// called before inheriting type constructors.
        /// 
        /// This is a static factory method that calls several methods in sequence to create the world in an orderly way.
        /// </summary>
        public static CollisionWorld CreateCollisionWorld(Dispatcher dispatcher, BroadphaseInterface pairCache, CollisionConfiguration collisionConfiguration)
        {
            CollisionWorld w = new CollisionWorld(dispatcher, pairCache);
            w.CreateNativePart(collisionConfiguration);
            return w;
        }

        protected CollisionWorld(Dispatcher dispatcher, BroadphaseInterface broadphasePairCache)
		{
            _dispatcher = dispatcher;
            Broadphase = broadphasePairCache;
        }

        protected virtual void CreateNativePart(CollisionConfiguration collisionConfiguration)
        {
            Native = UnsafeNativeMethods.btCollisionWorld_new(_dispatcher.Native, _broadphase.Native, collisionConfiguration.Native);
            CollisionObjectArray = new AlignedCollisionObjectArray(UnsafeNativeMethods.btCollisionWorld_getCollisionObjectArray(Native), this);
        }

		public void AddCollisionObject(CollisionObject collisionObject)
		{
			CollisionObjectArray.Add(collisionObject);
		}

		public void AddCollisionObject(CollisionObject collisionObject, CollisionFilterGroups collisionFilterGroup,
			CollisionFilterGroups collisionFilterMask)
		{
			CollisionObjectArray.Add(collisionObject, (int)collisionFilterGroup, (int)collisionFilterMask);
		}

		public void AddCollisionObject(CollisionObject collisionObject, int collisionFilterGroup,
			int collisionFilterMask)
		{
			CollisionObjectArray.Add(collisionObject, collisionFilterGroup, collisionFilterMask);
		}

		public void ComputeOverlappingPairs()
		{
			UnsafeNativeMethods.btCollisionWorld_computeOverlappingPairs(Native);
		}

		public void ContactPairTest(CollisionObject colObjA, CollisionObject colObjB,
			ContactResultCallback resultCallback)
		{
			UnsafeNativeMethods.btCollisionWorld_contactPairTest(Native, colObjA.Native, colObjB.Native,
				resultCallback.Native);
		}

		public void ContactTest(CollisionObject colObj, ContactResultCallback resultCallback)
		{
			UnsafeNativeMethods.btCollisionWorld_contactTest(Native, colObj.Native, resultCallback.Native);
		}

		public void ConvexSweepTestRef(ConvexShape castShape, ref Matrix from, ref Matrix to,
			ConvexResultCallback resultCallback, float allowedCcdPenetration = 0)
		{
			UnsafeNativeMethods.btCollisionWorld_convexSweepTest(Native, castShape.Native, ref from, ref to, resultCallback.Native, allowedCcdPenetration);
		}

		public void ConvexSweepTest(ConvexShape castShape, Matrix from, Matrix to,
			ConvexResultCallback resultCallback, float allowedCcdPenetration = 0)
		{
			UnsafeNativeMethods.btCollisionWorld_convexSweepTest(Native, castShape.Native, ref from,
				ref to, resultCallback.Native, allowedCcdPenetration);
		}

		public void DebugDrawObjectRef(ref Matrix worldTransform, CollisionShape shape, ref Vector3 color)
		{
			UnsafeNativeMethods.btCollisionWorld_debugDrawObject(Native, ref worldTransform, shape.Native, ref color);
		}

		public void DebugDrawObject(Matrix worldTransform, CollisionShape shape,
			Vector3 color)
		{
			UnsafeNativeMethods.btCollisionWorld_debugDrawObject(Native, ref worldTransform, shape.Native,
				ref color);
		}

		public void DebugDrawWorld()
		{
			UnsafeNativeMethods.btCollisionWorld_debugDrawWorld(Native);
		}

		public static void ObjectQuerySingleRef(ConvexShape castShape, ref Matrix rayFromTrans,
			ref Matrix rayToTrans, CollisionObject collisionObject, CollisionShape collisionShape,
			ref Matrix colObjWorldTransform, ConvexResultCallback resultCallback, float allowedPenetration)
		{
			UnsafeNativeMethods.btCollisionWorld_objectQuerySingle(castShape.Native, ref rayFromTrans,
				ref rayToTrans, collisionObject.Native, collisionShape.Native, ref colObjWorldTransform,
				resultCallback.Native, allowedPenetration);
		}

		public static void ObjectQuerySingle(ConvexShape castShape, Matrix rayFromTrans,
			Matrix rayToTrans, CollisionObject collisionObject, CollisionShape collisionShape,
			Matrix colObjWorldTransform, ConvexResultCallback resultCallback, float allowedPenetration)
		{
			UnsafeNativeMethods.btCollisionWorld_objectQuerySingle(castShape.Native, ref rayFromTrans,
				ref rayToTrans, collisionObject.Native, collisionShape.Native, ref colObjWorldTransform,
				resultCallback.Native, allowedPenetration);
		}

		public static void ObjectQuerySingleInternalRef(ConvexShape castShape, ref Matrix convexFromTrans,
			ref Matrix convexToTrans, CollisionObjectWrapper colObjWrap, ConvexResultCallback resultCallback,
			float allowedPenetration)
		{
			UnsafeNativeMethods.btCollisionWorld_objectQuerySingleInternal(castShape.Native, ref convexFromTrans,
				ref convexToTrans, colObjWrap.Native, resultCallback.Native, allowedPenetration);
		}

		public static void ObjectQuerySingleInternal(ConvexShape castShape, Matrix convexFromTrans,
			Matrix convexToTrans, CollisionObjectWrapper colObjWrap, ConvexResultCallback resultCallback,
			float allowedPenetration)
		{
			UnsafeNativeMethods.btCollisionWorld_objectQuerySingleInternal(castShape.Native, ref convexFromTrans,
				ref convexToTrans, colObjWrap.Native, resultCallback.Native, allowedPenetration);
		}

		public void PerformDiscreteCollisionDetection()
		{
			UnsafeNativeMethods.btCollisionWorld_performDiscreteCollisionDetection(Native);
		}

		public void RayTestRef(ref Vector3 rayFromWorld, ref Vector3 rayToWorld, RayResultCallback resultCallback)
		{
			UnsafeNativeMethods.btCollisionWorld_rayTest(Native, ref rayFromWorld, ref rayToWorld, resultCallback.Native);
		}

		public void RayTest(Vector3 rayFromWorld, Vector3 rayToWorld, RayResultCallback resultCallback)
		{
			UnsafeNativeMethods.btCollisionWorld_rayTest(Native, ref rayFromWorld, ref rayToWorld, resultCallback.Native);
		}

		public static void RayTestSingleRef(ref Matrix rayFromTrans, ref Matrix rayToTrans,
			CollisionObject collisionObject, CollisionShape collisionShape, ref Matrix colObjWorldTransform,
			RayResultCallback resultCallback)
		{
			UnsafeNativeMethods.btCollisionWorld_rayTestSingle(ref rayFromTrans, ref rayToTrans, collisionObject.Native, collisionShape.Native, ref colObjWorldTransform, resultCallback.Native);
		}

		public static void RayTestSingle(Matrix rayFromTrans, Matrix rayToTrans,
			CollisionObject collisionObject, CollisionShape collisionShape, Matrix colObjWorldTransform,
			RayResultCallback resultCallback)
		{
			UnsafeNativeMethods.btCollisionWorld_rayTestSingle(ref rayFromTrans, ref rayToTrans, collisionObject.Native,
				collisionShape.Native, ref colObjWorldTransform, resultCallback.Native);
		}

		public static void RayTestSingleInternalRef(ref Matrix rayFromTrans, ref Matrix rayToTrans,
			CollisionObjectWrapper collisionObjectWrap, RayResultCallback resultCallback)
		{
			UnsafeNativeMethods.btCollisionWorld_rayTestSingleInternal(ref rayFromTrans, ref rayToTrans,
				collisionObjectWrap.Native, resultCallback.Native);
		}

		public static void RayTestSingleInternal(Matrix rayFromTrans, Matrix rayToTrans,
			CollisionObjectWrapper collisionObjectWrap, RayResultCallback resultCallback)
		{
			UnsafeNativeMethods.btCollisionWorld_rayTestSingleInternal(ref rayFromTrans, ref rayToTrans,
				collisionObjectWrap.Native, resultCallback.Native);
		}

		public void RemoveCollisionObject(CollisionObject collisionObject)
		{
			CollisionObjectArray.Remove(collisionObject);
		}

		protected void SerializeCollisionObjects(Serializer serializer)
		{
			// keep track of shapes already serialized
			var serializedShapes = new Dictionary<CollisionShape, int>();

			foreach (var colObj in CollisionObjectArray)
			{
				var shape = colObj.CollisionShape;
				if (!serializedShapes.ContainsKey(shape))
				{
					serializedShapes.Add(shape, 0);
					shape.SerializeSingleShape(serializer);
				}
			}

			// serialize all collision objects
			foreach (var colObj in CollisionObjectArray)
			{
				if (colObj.InternalType == CollisionObjectTypes.CollisionObject)
				{
					colObj.SerializeSingleObject(serializer);
				}
			}
		}

		public virtual void Serialize(Serializer serializer)
		{
			serializer.StartSerialization();
			SerializeCollisionObjects(serializer);
			serializer.FinishSerialization();
		}

		public void UpdateAabbs()
		{
			UnsafeNativeMethods.btCollisionWorld_updateAabbs(Native);
		}

		public void UpdateSingleAabb(CollisionObject colObj)
		{
			UnsafeNativeMethods.btCollisionWorld_updateSingleAabb(Native, colObj.Native);
		}

		public BroadphaseInterface Broadphase
		{
			get { return _broadphase; }
			set
			{
				if (_broadphase != null)
				{
					_broadphase._worldRefs.Remove(this);
				}
				// Native can be zero from a constructor argument
				if (Native != IntPtr.Zero)
				{
					UnsafeNativeMethods.btCollisionWorld_setBroadphase(Native, value.Native);
				}
				_broadphase = value;
				value._worldRefs.Add(this);
			}
		}

		public AlignedCollisionObjectArray CollisionObjectArray { get; protected set; }

		public IDebugDraw DebugDrawer
		{
			get { return _debugDrawer; }
			set
			{
				if (_debugDrawer != null)
				{
					if (_debugDrawer == value) {
						return;
					}

					// Clear IDebugDraw wrapper
					if (!(_debugDrawer is DebugDraw)) {
						//btIDebugDrawer_delete(UnsafeNativeMethods.btCollisionWorld_getDebugDrawer(Native));
					}
				}

				_debugDrawer = value;
				if (value == null) {
					UnsafeNativeMethods.btCollisionWorld_setDebugDrawer(Native, IntPtr.Zero);
					return;
				}

				DebugDraw cast = value as DebugDraw;
				if (cast != null) {
					UnsafeNativeMethods.btCollisionWorld_setDebugDrawer(Native, cast._native);
				} else {
					// Create IDebugDraw wrapper, remember to delete it
					IntPtr wrapper = DebugDraw.CreateWrapper(value, false);
					UnsafeNativeMethods.btCollisionWorld_setDebugDrawer(Native, wrapper);
				}
			}
		}

		public Dispatcher Dispatcher
		{
			get { return _dispatcher; }
			internal set
			{
				_dispatcher = value;
				_dispatcher._worldRefs.Add(this);
			}
		}

		public DispatcherInfo DispatchInfo
		{
			get
			{
				if (_dispatchInfo == null)
				{
					_dispatchInfo = new DispatcherInfo(UnsafeNativeMethods.btCollisionWorld_getDispatchInfo(Native));
				}
				return _dispatchInfo;
			}
		}

		public bool ForceUpdateAllAabbs
		{
			get { return UnsafeNativeMethods.btCollisionWorld_getForceUpdateAllAabbs(Native); }
			set { UnsafeNativeMethods.btCollisionWorld_setForceUpdateAllAabbs(Native, value); }
		}

		public int NumCollisionObjects { get { return UnsafeNativeMethods.btCollisionWorld_getNumCollisionObjects(Native); } }

		public OverlappingPairCache PairCache { get { return Broadphase.OverlappingPairCache; } }

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				UnsafeNativeMethods.btCollisionWorld_delete(Native);
				Native = IntPtr.Zero;

				_broadphase._worldRefs.Remove(this);
				if (_broadphase._worldDeferredCleanup && _broadphase._worldRefs.Count == 0)
				{
					_broadphase.Dispose();
				}

				_dispatcher._worldRefs.Remove(this);
				if (_dispatcher._worldDeferredCleanup && _dispatcher._worldRefs.Count == 0)
				{
					_dispatcher.Dispose();
				}
			}
		}

		~CollisionWorld()
		{
			Dispose(false);
		}
	}
}
