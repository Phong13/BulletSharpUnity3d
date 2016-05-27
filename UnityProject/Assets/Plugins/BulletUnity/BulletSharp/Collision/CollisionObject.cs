using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
    public enum ActivationState
    {
        Undefined = 0,
        ActiveTag = 1,
        IslandSleeping = 2,
        WantsDeactivation = 3,
        DisableDeactivation = 4,
        DisableSimulation = 5
    }

	[Flags]
	public enum AnisotropicFrictionFlags
	{
		FrictionDisabled = 0,
		Friction = 1,
		RollingFriction = 2
	}

	[Flags]
	public enum CollisionFlags
	{
		None = 0,
		StaticObject = 1,
		KinematicObject = 2,
		NoContactResponse = 4,
		CustomMaterialCallback = 8,
		CharacterObject = 16,
		DisableVisualizeObject = 32,
		DisableSpuCollisionProcessing = 64
	}

	[Flags]
	public enum CollisionObjectTypes
	{
		None = 0,
		CollisionObject = 1,
		RigidBody = 2,
		GhostObject = 4,
		SoftBody = 8,
		HFFluid = 16,
		UserType = 32,
		FeatherstoneLink = 64
	}

	public class CollisionObject : IDisposable
	{
		internal IntPtr _native;
        private bool _isDisposed;
		private BroadphaseProxy _broadphaseHandle;
		protected CollisionShape _collisionShape;

        internal static CollisionObject GetManaged(IntPtr obj)
        {
            if (obj == IntPtr.Zero)
            {
                return null;
            }

            IntPtr userPtr = btCollisionObject_getUserPointer(obj);
            if (userPtr != IntPtr.Zero)
            {
                return GCHandle.FromIntPtr(userPtr).Target as CollisionObject;
            }

            throw new InvalidOperationException("Unknown collision object!");
        }

		internal CollisionObject(IntPtr native)
		{
			_native = native;
            GCHandle handle = GCHandle.Alloc(this, GCHandleType.Normal);
            btCollisionObject_setUserPointer(_native, GCHandle.ToIntPtr(handle));
		}

		public CollisionObject()
            : this(btCollisionObject_new())
		{
		}

		public void Activate()
		{
			btCollisionObject_activate(_native);
		}

		public void Activate(bool forceActivation)
		{
			btCollisionObject_activate2(_native, forceActivation);
		}

		public int CalculateSerializeBufferSize()
		{
			return btCollisionObject_calculateSerializeBufferSize(_native);
		}

		public bool CheckCollideWith(CollisionObject co)
		{
			return btCollisionObject_checkCollideWith(_native, co._native);
		}

		public bool CheckCollideWithOverride(CollisionObject co)
		{
			return btCollisionObject_checkCollideWithOverride(_native, co._native);
		}

        public void ForceActivationState(ActivationState newState)
		{
			btCollisionObject_forceActivationState(_native, newState);
		}

        public void GetWorldTransform(out Matrix transform)
        {
            btCollisionObject_getWorldTransform(_native, out transform);
        }

		public bool HasAnisotropicFriction()
		{
			return btCollisionObject_hasAnisotropicFriction(_native);
		}

        public bool HasAnisotropicFriction(AnisotropicFrictionFlags frictionMode)
		{
			return btCollisionObject_hasAnisotropicFriction2(_native, frictionMode);
		}

		public IntPtr InternalGetExtensionPointer()
		{
			return btCollisionObject_internalGetExtensionPointer(_native);
		}

		public void InternalSetExtensionPointer(IntPtr pointer)
		{
			btCollisionObject_internalSetExtensionPointer(_native, pointer);
		}

		public bool MergesSimulationIslands()
		{
			return btCollisionObject_mergesSimulationIslands(_native);
		}

		public string Serialize(IntPtr dataBuffer, Serializer serializer)
		{
			return Marshal.PtrToStringAnsi(btCollisionObject_serialize(_native, dataBuffer, serializer._native));
		}

		public void SerializeSingleObject(Serializer serializer)
		{
			btCollisionObject_serializeSingleObject(_native, serializer._native);
		}

        public void SetAnisotropicFrictionRef(ref Vector3 anisotropicFriction)
        {
            btCollisionObject_setAnisotropicFriction(_native, ref anisotropicFriction);
        }

		public void SetAnisotropicFriction(Vector3 anisotropicFriction)
		{
			btCollisionObject_setAnisotropicFriction(_native, ref anisotropicFriction);
		}

        public void SetAnisotropicFrictionRef(ref Vector3 anisotropicFriction, AnisotropicFrictionFlags frictionMode)
        {
            btCollisionObject_setAnisotropicFriction2(_native, ref anisotropicFriction, frictionMode);
        }

        public void SetAnisotropicFriction(Vector3 anisotropicFriction, AnisotropicFrictionFlags frictionMode)
		{
			btCollisionObject_setAnisotropicFriction2(_native, ref anisotropicFriction, frictionMode);
		}

		public void SetIgnoreCollisionCheck(CollisionObject co, bool ignoreCollisionCheck)
		{
			btCollisionObject_setIgnoreCollisionCheck(_native, co._native, ignoreCollisionCheck);
		}

        public ActivationState ActivationState
		{
			get { return btCollisionObject_getActivationState(_native); }
			set { btCollisionObject_setActivationState(_native, value); }
		}

		public Vector3 AnisotropicFriction
		{
			get
			{
				Vector3 value;
				btCollisionObject_getAnisotropicFriction(_native, out value);
				return value;
			}
            set { btCollisionObject_setAnisotropicFriction(_native, ref value); }
		}

		public BroadphaseProxy BroadphaseHandle
		{
			get { return _broadphaseHandle; }
			set
			{
                btCollisionObject_setBroadphaseHandle(_native, (value != null) ? value._native : IntPtr.Zero);
				_broadphaseHandle = value;
			}
		}

		public float CcdMotionThreshold
		{
			get { return btCollisionObject_getCcdMotionThreshold(_native); }
			set { btCollisionObject_setCcdMotionThreshold(_native, value); }
		}

		public float CcdSquareMotionThreshold
		{
			get { return btCollisionObject_getCcdSquareMotionThreshold(_native); }
		}

		public float CcdSweptSphereRadius
		{
			get { return btCollisionObject_getCcdSweptSphereRadius(_native); }
			set { btCollisionObject_setCcdSweptSphereRadius(_native, value); }
		}

        public CollisionFlags CollisionFlags
		{
			get { return btCollisionObject_getCollisionFlags(_native); }
			set { btCollisionObject_setCollisionFlags(_native, value); }
		}

		public CollisionShape CollisionShape
		{
			get { return _collisionShape; }
			set
			{
				btCollisionObject_setCollisionShape(_native, value._native);
				_collisionShape = value;
			}
		}

		public int CompanionId
		{
			get { return btCollisionObject_getCompanionId(_native); }
			set { btCollisionObject_setCompanionId(_native, value); }
		}

		public float ContactProcessingThreshold
		{
			get { return btCollisionObject_getContactProcessingThreshold(_native); }
			set { btCollisionObject_setContactProcessingThreshold(_native, value); }
		}

		public float DeactivationTime
		{
			get { return btCollisionObject_getDeactivationTime(_native); }
			set { btCollisionObject_setDeactivationTime(_native, value); }
		}

		public float Friction
		{
			get { return btCollisionObject_getFriction(_native); }
			set { btCollisionObject_setFriction(_native, value); }
		}

		public bool HasContactResponse
		{
			get { return btCollisionObject_hasContactResponse(_native); }
		}

		public float HitFraction
		{
			get { return btCollisionObject_getHitFraction(_native); }
			set { btCollisionObject_setHitFraction(_native, value); }
		}

        public CollisionObjectTypes InternalType
		{
			get { return btCollisionObject_getInternalType(_native); }
		}

		public Vector3 InterpolationAngularVelocity
		{
			get
			{
				Vector3 value;
				btCollisionObject_getInterpolationAngularVelocity(_native, out value);
				return value;
			}
			set { btCollisionObject_setInterpolationAngularVelocity(_native, ref value); }
		}

		public Vector3 InterpolationLinearVelocity
		{
			get
			{
				Vector3 value;
				btCollisionObject_getInterpolationLinearVelocity(_native, out value);
				return value;
			}
			set { btCollisionObject_setInterpolationLinearVelocity(_native, ref value); }
		}

		public Matrix InterpolationWorldTransform
		{
			get
			{
				Matrix value;
				btCollisionObject_getInterpolationWorldTransform(_native, out value);
				return value;
			}
			set { btCollisionObject_setInterpolationWorldTransform(_native, ref value); }
		}

		public bool IsActive
		{
			get { return btCollisionObject_isActive(_native); }
		}

		public bool IsKinematicObject
		{
			get { return btCollisionObject_isKinematicObject(_native); }
		}

		public int IslandTag
		{
			get { return btCollisionObject_getIslandTag(_native); }
			set { btCollisionObject_setIslandTag(_native, value); }
		}

		public bool IsStaticObject
		{
			get { return btCollisionObject_isStaticObject(_native); }
		}

		public bool IsStaticOrKinematicObject
		{
			get { return btCollisionObject_isStaticOrKinematicObject(_native); }
		}

		public float Restitution
		{
			get { return btCollisionObject_getRestitution(_native); }
			set { btCollisionObject_setRestitution(_native, value); }
		}

		public float RollingFriction
		{
			get { return btCollisionObject_getRollingFriction(_native); }
			set { btCollisionObject_setRollingFriction(_native, value); }
		}

        public object UserObject { get; set; }

		public int UserIndex
		{
			get { return btCollisionObject_getUserIndex(_native); }
			set { btCollisionObject_setUserIndex(_native, value); }
		}

		public Matrix WorldTransform
		{
			get
			{
				Matrix value;
				btCollisionObject_getWorldTransform(_native, out value);
				return value;
			}
			set { btCollisionObject_setWorldTransform(_native, ref value); }
		}

        public override bool Equals(object obj)
        {
            CollisionObject colObj = obj as CollisionObject;
            if (colObj == null)
            {
                return false;
            }
            return _native == colObj._native;
        }

        public override int GetHashCode()
        {
            return _native.GetHashCode();
        }

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
            if (!_isDisposed)
			{
                // Is the object added to a world?
                if (btCollisionObject_getBroadphaseHandle(_native) != IntPtr.Zero)
                {
                    BroadphaseHandle = null;
                    //System.Diagnostics.Debugger.Break();
                    return;
                }

                _isDisposed = true;

                IntPtr userPtr = btCollisionObject_getUserPointer(_native);
                GCHandle.FromIntPtr(userPtr).Free();
                btCollisionObject_delete(_native);
			}
		}

		~CollisionObject()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCollisionObject_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionObject_activate(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionObject_activate2(IntPtr obj, bool forceActivation);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btCollisionObject_calculateSerializeBufferSize(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btCollisionObject_checkCollideWith(IntPtr obj, IntPtr co);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btCollisionObject_checkCollideWithOverride(IntPtr obj, IntPtr co);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionObject_forceActivationState(IntPtr obj, ActivationState newState);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern ActivationState btCollisionObject_getActivationState(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionObject_getAnisotropicFriction(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCollisionObject_getBroadphaseHandle(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btCollisionObject_getCcdMotionThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btCollisionObject_getCcdSquareMotionThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btCollisionObject_getCcdSweptSphereRadius(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern CollisionFlags btCollisionObject_getCollisionFlags(IntPtr obj);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern IntPtr btCollisionObject_getCollisionShape(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btCollisionObject_getCompanionId(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btCollisionObject_getContactProcessingThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btCollisionObject_getDeactivationTime(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btCollisionObject_getFriction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btCollisionObject_getHitFraction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern CollisionObjectTypes btCollisionObject_getInternalType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionObject_getInterpolationAngularVelocity(IntPtr obj, [Out] out Vector3 angvel);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionObject_getInterpolationLinearVelocity(IntPtr obj, [Out] out Vector3 linvel);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionObject_getInterpolationWorldTransform(IntPtr obj, [Out] out Matrix trans);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btCollisionObject_getIslandTag(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btCollisionObject_getRestitution(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btCollisionObject_getRollingFriction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btCollisionObject_getUserIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCollisionObject_getUserPointer(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionObject_getWorldTransform(IntPtr obj, [Out] out Matrix worldTrans);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btCollisionObject_hasAnisotropicFriction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
        static extern bool btCollisionObject_hasAnisotropicFriction2(IntPtr obj, AnisotropicFrictionFlags frictionMode);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btCollisionObject_hasContactResponse(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCollisionObject_internalGetExtensionPointer(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionObject_internalSetExtensionPointer(IntPtr obj, IntPtr pointer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btCollisionObject_isActive(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btCollisionObject_isKinematicObject(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btCollisionObject_isStaticObject(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btCollisionObject_isStaticOrKinematicObject(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btCollisionObject_mergesSimulationIslands(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCollisionObject_serialize(IntPtr obj, IntPtr dataBuffer, IntPtr serializer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionObject_serializeSingleObject(IntPtr obj, IntPtr serializer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionObject_setActivationState(IntPtr obj, ActivationState newState);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionObject_setAnisotropicFriction(IntPtr obj, [In] ref Vector3 anisotropicFriction);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionObject_setAnisotropicFriction2(IntPtr obj, [In] ref Vector3 anisotropicFriction, AnisotropicFrictionFlags frictionMode);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionObject_setBroadphaseHandle(IntPtr obj, IntPtr handle);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionObject_setCcdMotionThreshold(IntPtr obj, float ccdMotionThreshold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionObject_setCcdSweptSphereRadius(IntPtr obj, float radius);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionObject_setCollisionFlags(IntPtr obj, CollisionFlags flags);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionObject_setCollisionShape(IntPtr obj, IntPtr collisionShape);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionObject_setCompanionId(IntPtr obj, int id);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionObject_setContactProcessingThreshold(IntPtr obj, float contactProcessingThreshold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionObject_setDeactivationTime(IntPtr obj, float time);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionObject_setFriction(IntPtr obj, float frict);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionObject_setHitFraction(IntPtr obj, float hitFraction);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionObject_setIgnoreCollisionCheck(IntPtr obj, IntPtr co, bool ignoreCollisionCheck);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionObject_setInterpolationAngularVelocity(IntPtr obj, [In] ref Vector3 angvel);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionObject_setInterpolationLinearVelocity(IntPtr obj, [In] ref Vector3 linvel);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionObject_setInterpolationWorldTransform(IntPtr obj, [In] ref Matrix trans);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionObject_setIslandTag(IntPtr obj, int tag);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionObject_setRestitution(IntPtr obj, float rest);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionObject_setRollingFriction(IntPtr obj, float frict);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionObject_setUserIndex(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionObject_setUserPointer(IntPtr obj, IntPtr userPointer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionObject_setWorldTransform(IntPtr obj, [In] ref Matrix worldTrans);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionObject_delete(IntPtr obj);
	}

    [StructLayout(LayoutKind.Sequential)]
    internal struct CollisionObjectFloatData
    {
        public IntPtr BroadphaseHandle;
        public IntPtr CollisionShape;
        public IntPtr RootCollisionShape;
        public IntPtr Name;
        public TransformFloatData WorldTransform;
        public TransformFloatData InterpolationWorldTransform;
        public Vector3FloatData InterpolationLinearVelocity;
        public Vector3FloatData InterpolationAngularVelocity;
        public Vector3FloatData AnisotropicFriction;
        public float ContactProcessingThreshold;	
        public float DeactivationTime;
        public float Friction;
        public float RollingFriction;
        public float Restitution;
        public float HitFraction; 
        public float CcdSweptSphereRadius;
        public float CcdMotionThreshold;
        public int HasAnisotropicFriction;
        public int CollisionFlags;
        public int IslandTag1;
        public int CompanionId;
        public int ActivationState1;
        public int InternalType;
        public int CheckCollideWith;
        public int Padding;

        public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(CollisionObjectFloatData), fieldName).ToInt32(); }
    }
}
