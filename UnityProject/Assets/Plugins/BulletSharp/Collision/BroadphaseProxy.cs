using System;
using System.Runtime.InteropServices;
using System.Security;
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
        SCALED_TRIANGLE_MESH_SHAPE_PROXYTYPE,
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
		internal IntPtr _native;
        private Object _clientObject;

		internal BroadphaseProxy(IntPtr native)
		{
			_native = native;
		}

        internal static BroadphaseProxy GetManaged(IntPtr native)
        {
            if (native == IntPtr.Zero)
            {
                return null;
            }

            IntPtr clientObjectPtr = btBroadphaseProxy_getClientObject(native);
            if (clientObjectPtr != IntPtr.Zero) {
                CollisionObject clientObject = CollisionObject.GetManaged(clientObjectPtr);
                return clientObject.BroadphaseHandle;
            }

            throw new InvalidOperationException("Unknown broadphase proxy!");
            //return new BroadphaseProxy(native);
        }

        public static bool IsCompound(BroadphaseNativeType proxyType)
		{
			return btBroadphaseProxy_isCompound(proxyType);
		}

        public static bool IsConcave(BroadphaseNativeType proxyType)
		{
			return btBroadphaseProxy_isConcave(proxyType);
		}

        public static bool IsConvex(BroadphaseNativeType proxyType)
		{
			return btBroadphaseProxy_isConvex(proxyType);
		}

        public static bool IsConvex2D(BroadphaseNativeType proxyType)
		{
			return btBroadphaseProxy_isConvex2d(proxyType);
		}

        public static bool IsInfinite(BroadphaseNativeType proxyType)
		{
			return btBroadphaseProxy_isInfinite(proxyType);
		}

        public static bool IsNonMoving(BroadphaseNativeType proxyType)
		{
			return btBroadphaseProxy_isNonMoving(proxyType);
		}

        public static bool IsPolyhedral(BroadphaseNativeType proxyType)
		{
			return btBroadphaseProxy_isPolyhedral(proxyType);
		}

        public static bool IsSoftBody(BroadphaseNativeType proxyType)
		{
			return btBroadphaseProxy_isSoftBody(proxyType);
		}

		public Vector3 AabbMax
		{
			get
			{
				Vector3 value;
				btBroadphaseProxy_getAabbMax(_native, out value);
				return value;
			}
			set { btBroadphaseProxy_setAabbMax(_native, ref value); }
		}

		public Vector3 AabbMin
		{
			get
			{
				Vector3 value;
				btBroadphaseProxy_getAabbMin(_native, out value);
				return value;
			}
			set { btBroadphaseProxy_setAabbMin(_native, ref value); }
		}

        public Object ClientObject
		{
            get
            {
                IntPtr clientObjectPtr = btBroadphaseProxy_getClientObject(_native);
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
                    btBroadphaseProxy_setClientObject(_native, collisionObject._native);
                }
                else if (value == null)
                {
                    btBroadphaseProxy_setClientObject(_native, IntPtr.Zero);
                }
                _clientObject = value;
            }
		}

		public short CollisionFilterGroup
		{
			get { return btBroadphaseProxy_getCollisionFilterGroup(_native); }
			set { btBroadphaseProxy_setCollisionFilterGroup(_native, value); }
		}

		public short CollisionFilterMask
		{
			get { return btBroadphaseProxy_getCollisionFilterMask(_native); }
			set { btBroadphaseProxy_setCollisionFilterMask(_native, value); }
		}

		public IntPtr MultiSapParentProxy
		{
			get { return btBroadphaseProxy_getMultiSapParentProxy(_native); }
			set { btBroadphaseProxy_setMultiSapParentProxy(_native, value); }
		}

		public int Uid
		{
			get { return btBroadphaseProxy_getUid(_native); }
		}

		public int UniqueId
		{
			get { return btBroadphaseProxy_getUniqueId(_native); }
			set { btBroadphaseProxy_setUniqueId(_native, value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseProxy_getAabbMax(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseProxy_getAabbMin(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBroadphaseProxy_getClientObject(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern short btBroadphaseProxy_getCollisionFilterGroup(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern short btBroadphaseProxy_getCollisionFilterMask(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBroadphaseProxy_getMultiSapParentProxy(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btBroadphaseProxy_getUid(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btBroadphaseProxy_getUniqueId(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
        static extern bool btBroadphaseProxy_isCompound(BroadphaseNativeType proxyType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
        static extern bool btBroadphaseProxy_isConcave(BroadphaseNativeType proxyType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
        static extern bool btBroadphaseProxy_isConvex(BroadphaseNativeType proxyType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
        static extern bool btBroadphaseProxy_isConvex2d(BroadphaseNativeType proxyType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
        static extern bool btBroadphaseProxy_isInfinite(BroadphaseNativeType proxyType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
        static extern bool btBroadphaseProxy_isNonMoving(BroadphaseNativeType proxyType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
        static extern bool btBroadphaseProxy_isPolyhedral(BroadphaseNativeType proxyType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
        static extern bool btBroadphaseProxy_isSoftBody(BroadphaseNativeType proxyType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseProxy_setAabbMax(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseProxy_setAabbMin(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseProxy_setClientObject(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseProxy_setCollisionFilterGroup(IntPtr obj, short value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseProxy_setCollisionFilterMask(IntPtr obj, short value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseProxy_setMultiSapParentProxy(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseProxy_setUniqueId(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseProxy_delete(IntPtr obj);
	}

	public class BroadphasePair
	{
		internal IntPtr _native;

		internal BroadphasePair(IntPtr native)
		{
			_native = native;
		}

		public CollisionAlgorithm Algorithm
		{
            get
            {
                IntPtr valuePtr = btBroadphasePair_getAlgorithm(_native);
                return (valuePtr == IntPtr.Zero) ? null : new CollisionAlgorithm(valuePtr, true);
            }
            set { btBroadphasePair_setAlgorithm(_native, (value._native == IntPtr.Zero) ? IntPtr.Zero : value._native); }
		}

        public BroadphaseProxy Proxy0
		{
			get { return BroadphaseProxy.GetManaged(btBroadphasePair_getPProxy0(_native)); }
			set { btBroadphasePair_setPProxy0(_native, value._native); }
		}

        public BroadphaseProxy Proxy1
		{
			get { return BroadphaseProxy.GetManaged(btBroadphasePair_getPProxy1(_native)); }
			set { btBroadphasePair_setPProxy1(_native, value._native); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBroadphasePair_getAlgorithm(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBroadphasePair_getPProxy0(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBroadphasePair_getPProxy1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphasePair_setAlgorithm(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphasePair_setPProxy0(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphasePair_setPProxy1(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphasePair_delete(IntPtr obj);
	}
}
