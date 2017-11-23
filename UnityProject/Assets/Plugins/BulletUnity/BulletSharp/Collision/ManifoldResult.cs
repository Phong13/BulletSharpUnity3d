using System;


namespace BulletSharp
{
	public class ManifoldResult : DiscreteCollisionDetectorInterface.Result
	{
		internal ManifoldResult(IntPtr native)
			: base(native)
		{
		}

		public ManifoldResult()
			: base(UnsafeNativeMethods.btManifoldResult_new())
		{
		}

		public ManifoldResult(CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap)
			: base(UnsafeNativeMethods.btManifoldResult_new2(body0Wrap.Native, body1Wrap.Native))
		{
		}

		public static float CalculateCombinedContactDamping(CollisionObject body0,
			CollisionObject body1)
		{
			return UnsafeNativeMethods.btManifoldResult_calculateCombinedContactDamping(body0.Native,
				body1.Native);
		}

		public static float CalculateCombinedContactStiffness(CollisionObject body0,
			CollisionObject body1)
		{
			return UnsafeNativeMethods.btManifoldResult_calculateCombinedContactStiffness(body0.Native,
				body1.Native);
		}

		public static float CalculateCombinedFriction(CollisionObject body0, CollisionObject body1)
		{
			return UnsafeNativeMethods.btManifoldResult_calculateCombinedFriction(body0.Native, body1.Native);
		}

		public static float CalculateCombinedRestitution(CollisionObject body0, CollisionObject body1)
		{
			return UnsafeNativeMethods.btManifoldResult_calculateCombinedRestitution(body0.Native, body1.Native);
		}

		public static float CalculateCombinedRollingFriction(CollisionObject body0,
			CollisionObject body1)
		{
			return UnsafeNativeMethods.btManifoldResult_calculateCombinedRollingFriction(body0.Native,
				body1.Native);
		}

		public void RefreshContactPoints()
		{
			UnsafeNativeMethods.btManifoldResult_refreshContactPoints(Native);
		}

		public CollisionObject Body0Internal => CollisionObject.GetManaged(UnsafeNativeMethods.btManifoldResult_getBody0Internal(Native));

		public CollisionObjectWrapper Body0Wrap
		{
			get => new CollisionObjectWrapper(UnsafeNativeMethods.btManifoldResult_getBody0Wrap(Native));
			set => UnsafeNativeMethods.btManifoldResult_setBody0Wrap(Native, value.Native);
		}

		public CollisionObject Body1Internal => CollisionObject.GetManaged(UnsafeNativeMethods.btManifoldResult_getBody1Internal(Native));

		public CollisionObjectWrapper Body1Wrap
		{
			get => new CollisionObjectWrapper(UnsafeNativeMethods.btManifoldResult_getBody1Wrap(Native));
			set => UnsafeNativeMethods.btManifoldResult_setBody1Wrap(Native, value.Native);
		}

		public float ClosestPointDistanceThreshold
		{
			get => UnsafeNativeMethods.btManifoldResult_getClosestPointDistanceThreshold(Native);
			set => UnsafeNativeMethods.btManifoldResult_setClosestPointDistanceThreshold(Native, value);
		}

		public PersistentManifold PersistentManifold
		{
			get => new PersistentManifold(UnsafeNativeMethods.btManifoldResult_getPersistentManifold(Native), true);
			set => UnsafeNativeMethods.btManifoldResult_setPersistentManifold(Native, value.Native);
		}
	}
}
