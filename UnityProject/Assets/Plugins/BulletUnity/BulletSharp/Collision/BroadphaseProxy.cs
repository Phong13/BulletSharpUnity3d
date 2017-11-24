using System;
using BulletSharp.Math;

namespace BulletSharp
{
	public enum BroadphaseNativeType
	{
		BoxShape,
		TriangleShape,
		TetrahedralShape,
		ConvexTriangleMeshShape,
		ConvexHullShape,
		CONVEX_POINT_CLOUD_SHAPE_PROXYTYPE,
		CUSTOM_POLYHEDRAL_SHAPE_TYPE,
		IMPLICIT_CONVEX_SHAPES_START_HERE,
		SphereShape,
		MultiSphereShape,
		CapsuleShape,
		ConeShape,
		ConvexShape,
		CylinderShape,
		UniformScalingShape,
		MinkowskiSumShape,
		MinkowskiDifferenceShape,
		Box2DShape,
		Convex2DShape,
		CUSTOM_CONVEX_SHAPE_TYPE,
		CONCAVE_SHAPES_START_HERE,
		TriangleMeshShape,
		ScaledTriangleMeshShape,
		FAST_CONCAVE_MESH_PROXYTYPE,
		TerrainShape,
		GImpactShape,
		MultiMaterialTriangleMesh,
		EmptyShape,
		StaticPlaneShape,
		CUSTOM_CONCAVE_SHAPE_TYPE,
		CONCAVE_SHAPES_END_HERE,
		CompoundShape,
		SoftBodyShape,
		HFFLUID_SHAPE_PROXYTYPE,
		HFFLUID_BUOYANT_CONVEX_SHAPE_PROXYTYPE,
		INVALID_SHAPE_PROXYTYPE,
		MAX_BROADPHASE_COLLISION_TYPES
	}

	[Flags]
	public enum CollisionFilterGroups
	{
        Everything = -1,
        None = 0,
		DefaultFilter = 1,
		StaticFilter = 2,
		KinematicFilter = 4,
		DebrisFilter = 8,
		SensorTrigger = 16,
		CharacterFilter = 32,
		AllFilter = -1
	}

	public class BroadphaseProxy
	{
		internal IntPtr Native;
		private Object _clientObject;

		internal BroadphaseProxy(IntPtr native)
		{
			Native = native;
		}

		internal static BroadphaseProxy GetManaged(IntPtr native)
		{
			if (native == IntPtr.Zero)
			{
				return null;
			}

			IntPtr clientObjectPtr = UnsafeNativeMethods.btBroadphaseProxy_getClientObject(native);
			if (clientObjectPtr != IntPtr.Zero) {
				CollisionObject clientObject = CollisionObject.GetManaged(clientObjectPtr);
				return clientObject.BroadphaseHandle;
			}

			throw new InvalidOperationException("Unknown broadphase proxy!");
			//return new BroadphaseProxy(native);
		}

		public static bool IsCompound(BroadphaseNativeType proxyType)
		{
			return UnsafeNativeMethods.btBroadphaseProxy_isCompound(proxyType);
		}

		public static bool IsConcave(BroadphaseNativeType proxyType)
		{
			return UnsafeNativeMethods.btBroadphaseProxy_isConcave(proxyType);
		}

		public static bool IsConvex(BroadphaseNativeType proxyType)
		{
			return UnsafeNativeMethods.btBroadphaseProxy_isConvex(proxyType);
		}

		public static bool IsConvex2D(BroadphaseNativeType proxyType)
		{
			return UnsafeNativeMethods.btBroadphaseProxy_isConvex2d(proxyType);
		}

		public static bool IsInfinite(BroadphaseNativeType proxyType)
		{
			return UnsafeNativeMethods.btBroadphaseProxy_isInfinite(proxyType);
		}

		public static bool IsNonMoving(BroadphaseNativeType proxyType)
		{
			return UnsafeNativeMethods.btBroadphaseProxy_isNonMoving(proxyType);
		}

		public static bool IsPolyhedral(BroadphaseNativeType proxyType)
		{
			return UnsafeNativeMethods.btBroadphaseProxy_isPolyhedral(proxyType);
		}

		public static bool IsSoftBody(BroadphaseNativeType proxyType)
		{
			return UnsafeNativeMethods.btBroadphaseProxy_isSoftBody(proxyType);
		}

		public Vector3 AabbMax
		{
			get
			{
				Vector3 value;
                UnsafeNativeMethods.btBroadphaseProxy_getAabbMax(Native, out value);
				return value;
			}
			set { UnsafeNativeMethods.btBroadphaseProxy_setAabbMax(Native, ref value); }
		}

		public Vector3 AabbMin
		{
			get
			{
				Vector3 value;
                UnsafeNativeMethods.btBroadphaseProxy_getAabbMin(Native, out value);
				return value;
			}
			set { UnsafeNativeMethods.btBroadphaseProxy_setAabbMin(Native, ref value); }
		}

		public Object ClientObject
		{
			get
			{
				IntPtr clientObjectPtr = UnsafeNativeMethods.btBroadphaseProxy_getClientObject(Native);
				if (clientObjectPtr != IntPtr.Zero)
				{
					_clientObject = CollisionObject.GetManaged(clientObjectPtr);
				}
				return _clientObject;
			}
			set
			{
				CollisionObject collisionObject = value as CollisionObject;
				if (collisionObject != null)
				{
                    UnsafeNativeMethods.btBroadphaseProxy_setClientObject(Native, collisionObject.Native);
				}
				else if (value == null)
				{
                    UnsafeNativeMethods.btBroadphaseProxy_setClientObject(Native, IntPtr.Zero);
				}
				_clientObject = value;
			}
		}

		public int CollisionFilterGroup
		{
			get { return UnsafeNativeMethods.btBroadphaseProxy_getCollisionFilterGroup(Native); }
			set { UnsafeNativeMethods.btBroadphaseProxy_setCollisionFilterGroup(Native, value); }
		}

		public int CollisionFilterMask
		{
			get { return UnsafeNativeMethods.btBroadphaseProxy_getCollisionFilterMask(Native); }
			set { UnsafeNativeMethods.btBroadphaseProxy_setCollisionFilterMask(Native, value); }
		}

        public int Uid() {
            return UnsafeNativeMethods.btBroadphaseProxy_getUid(Native);
        }

		public int UniqueId
		{
			get { return UnsafeNativeMethods.btBroadphaseProxy_getUniqueId(Native); }
			set { UnsafeNativeMethods.btBroadphaseProxy_setUniqueId(Native, value); }
		}
	}

	public class BroadphasePair
	{
		internal IntPtr Native;

		internal BroadphasePair(IntPtr native)
		{
			Native = native;
		}

		public CollisionAlgorithm Algorithm
		{
			get
			{
				IntPtr valuePtr = UnsafeNativeMethods.btBroadphasePair_getAlgorithm(Native);
				return (valuePtr == IntPtr.Zero) ? null : new CollisionAlgorithm(valuePtr, true);
			}
			set { UnsafeNativeMethods.btBroadphasePair_setAlgorithm(Native, (value.Native == IntPtr.Zero) ? IntPtr.Zero : value.Native); }
		}

		public BroadphaseProxy Proxy0
		{
			get { return BroadphaseProxy.GetManaged(UnsafeNativeMethods.btBroadphasePair_getPProxy0(Native)); }
			set { UnsafeNativeMethods.btBroadphasePair_setPProxy0(Native, value.Native); }
		}

		public BroadphaseProxy Proxy1
		{
			get { return BroadphaseProxy.GetManaged(UnsafeNativeMethods.btBroadphasePair_getPProxy1(Native)); }
			set { UnsafeNativeMethods.btBroadphasePair_setPProxy1(Native, value.Native); }
		}
	}
}
