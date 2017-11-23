using System;
using System.Runtime.InteropServices;
using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

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
		DisableSpuCollisionProcessing = 64,
		HasContactStiffnessDamping = 128,
		HasCustomDebugRenderingColor = 256,
		HasFrictionAnchor = 512,
		HasCollisionSoundTrigger = 1024
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
		internal IntPtr Native;
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
			Native = native;
			GCHandle handle = GCHandle.Alloc(this, GCHandleType.Weak);
			btCollisionObject_setUserPointer(Native, GCHandle.ToIntPtr(handle));
		}

		public CollisionObject()
			: this(btCollisionObject_new())
		{
		}

		public void Activate(bool forceActivation = false)
		{
			btCollisionObject_activate(Native, forceActivation);
		}

		public int CalculateSerializeBufferSize()
		{
			return btCollisionObject_calculateSerializeBufferSize(Native);
		}

		public bool CheckCollideWith(CollisionObject co)
		{
			return btCollisionObject_checkCollideWith(Native, co.Native);
		}

		public bool CheckCollideWithOverride(CollisionObject co)
		{
			return btCollisionObject_checkCollideWithOverride(Native, co.Native);
		}

		public void ForceActivationState(ActivationState newState)
		{
			btCollisionObject_forceActivationState(Native, newState);
		}

		public bool GetCustomDebugColor(out Vector3 colorRgb)
		{
			return btCollisionObject_getCustomDebugColor(Native, out colorRgb);
		}

		public void GetWorldTransform(out Matrix transform)
		{
			btCollisionObject_getWorldTransform(Native, out transform);
		}

		public bool HasAnisotropicFriction(AnisotropicFrictionFlags frictionMode = AnisotropicFrictionFlags.Friction)
		{
			return btCollisionObject_hasAnisotropicFriction(Native, frictionMode);
		}

		public IntPtr InternalGetExtensionPointer()
		{
			return btCollisionObject_internalGetExtensionPointer(Native);
		}

		public void InternalSetExtensionPointer(IntPtr pointer)
		{
			btCollisionObject_internalSetExtensionPointer(Native, pointer);
		}

		public bool MergesSimulationIslands()
		{
			return btCollisionObject_mergesSimulationIslands(Native);
		}

		public void RemoveCustomDebugColor()
		{
			btCollisionObject_removeCustomDebugColor(Native);
		}

		public string Serialize(IntPtr dataBuffer, Serializer serializer)
		{
			return Marshal.PtrToStringAnsi(btCollisionObject_serialize(Native, dataBuffer, serializer._native));
		}

		public void SerializeSingleObject(Serializer serializer)
		{
			btCollisionObject_serializeSingleObject(Native, serializer._native);
		}

		public void SetAnisotropicFrictionRef(ref Vector3 anisotropicFriction,
			AnisotropicFrictionFlags frictionMode = AnisotropicFrictionFlags.Friction)
		{
			btCollisionObject_setAnisotropicFriction(Native, ref anisotropicFriction, frictionMode);
		}

		public void SetAnisotropicFriction(Vector3 anisotropicFriction,
			AnisotropicFrictionFlags frictionMode = AnisotropicFrictionFlags.Friction)
		{
			btCollisionObject_setAnisotropicFriction(Native, ref anisotropicFriction,
				frictionMode);
		}

		public void SetContactStiffnessAndDamping(float stiffness, float damping)
		{
			btCollisionObject_setContactStiffnessAndDamping(Native, stiffness, damping);
		}

		public void SetCustomDebugColor(Vector3 colorRgb)
		{
			btCollisionObject_setCustomDebugColor(Native, ref colorRgb);
		}

		public void SetIgnoreCollisionCheck(CollisionObject co, bool ignoreCollisionCheck)
		{
			btCollisionObject_setIgnoreCollisionCheck(Native, co.Native, ignoreCollisionCheck);
		}

		public ActivationState ActivationState
		{
			get => btCollisionObject_getActivationState(Native);
			set => btCollisionObject_setActivationState(Native, value);
		}

		public Vector3 AnisotropicFriction
		{
			get
			{
				Vector3 value;
				btCollisionObject_getAnisotropicFriction(Native, out value);
				return value;
			}
			set => btCollisionObject_setAnisotropicFriction(Native, ref value, AnisotropicFrictionFlags.Friction);
		}

		public BroadphaseProxy BroadphaseHandle
		{
			get => _broadphaseHandle;
			set
			{
				btCollisionObject_setBroadphaseHandle(Native, (value != null) ? value.Native : IntPtr.Zero);
				_broadphaseHandle = value;
			}
		}

		public float CcdMotionThreshold
		{
			get => btCollisionObject_getCcdMotionThreshold(Native);
			set => btCollisionObject_setCcdMotionThreshold(Native, value);
		}

		public float CcdSquareMotionThreshold => btCollisionObject_getCcdSquareMotionThreshold(Native);

		public float CcdSweptSphereRadius
		{
			get => btCollisionObject_getCcdSweptSphereRadius(Native);
			set => btCollisionObject_setCcdSweptSphereRadius(Native, value);
		}

		public CollisionFlags CollisionFlags
		{
			get => btCollisionObject_getCollisionFlags(Native);
			set => btCollisionObject_setCollisionFlags(Native, value);
		}

		public CollisionShape CollisionShape
		{
			get => _collisionShape;
			set
			{
				btCollisionObject_setCollisionShape(Native, value.Native);
				_collisionShape = value;
			}
		}

		public int CompanionId
		{
			get => btCollisionObject_getCompanionId(Native);
			set => btCollisionObject_setCompanionId(Native, value);
		}

		public float ContactDamping => btCollisionObject_getContactDamping(Native);

		public float ContactProcessingThreshold
		{
			get => btCollisionObject_getContactProcessingThreshold(Native);
			set => btCollisionObject_setContactProcessingThreshold(Native, value);
		}

		public float ContactStiffness => btCollisionObject_getContactStiffness(Native);

		public float DeactivationTime
		{
			get => btCollisionObject_getDeactivationTime(Native);
			set => btCollisionObject_setDeactivationTime(Native, value);
		}

		public float Friction
		{
			get => btCollisionObject_getFriction(Native);
			set => btCollisionObject_setFriction(Native, value);
		}

		public bool HasContactResponse => btCollisionObject_hasContactResponse(Native);

		public float HitFraction
		{
			get => btCollisionObject_getHitFraction(Native);
			set => btCollisionObject_setHitFraction(Native, value);
		}

		public CollisionObjectTypes InternalType => btCollisionObject_getInternalType(Native);

		public Vector3 InterpolationAngularVelocity
		{
			get
			{
				Vector3 value;
				btCollisionObject_getInterpolationAngularVelocity(Native, out value);
				return value;
			}
			set => btCollisionObject_setInterpolationAngularVelocity(Native, ref value);
		}

		public Vector3 InterpolationLinearVelocity
		{
			get
			{
				Vector3 value;
				btCollisionObject_getInterpolationLinearVelocity(Native, out value);
				return value;
			}
			set => btCollisionObject_setInterpolationLinearVelocity(Native, ref value);
		}

		public Matrix InterpolationWorldTransform
		{
			get
			{
				Matrix value;
				btCollisionObject_getInterpolationWorldTransform(Native, out value);
				return value;
			}
			set => btCollisionObject_setInterpolationWorldTransform(Native, ref value);
		}

		public bool IsActive => btCollisionObject_isActive(Native);

		public bool IsKinematicObject => btCollisionObject_isKinematicObject(Native);

		public int IslandTag
		{
			get => btCollisionObject_getIslandTag(Native);
			set => btCollisionObject_setIslandTag(Native, value);
		}

		public bool IsStaticObject => btCollisionObject_isStaticObject(Native);

		public bool IsStaticOrKinematicObject => btCollisionObject_isStaticOrKinematicObject(Native);

		public float Restitution
		{
			get => btCollisionObject_getRestitution(Native);
			set => btCollisionObject_setRestitution(Native, value);
		}

		public float RollingFriction
		{
			get => btCollisionObject_getRollingFriction(Native);
			set => btCollisionObject_setRollingFriction(Native, value);
		}

		public float SpinningFriction
		{
			get => btCollisionObject_getSpinningFriction(Native);
			set => btCollisionObject_setSpinningFriction(Native, value);
		}

		public object UserObject { get; set; }

		public int UserIndex
		{
			get => btCollisionObject_getUserIndex(Native);
			set => btCollisionObject_setUserIndex(Native, value);
		}

		public int UserIndex2
		{
			get => btCollisionObject_getUserIndex2(Native);
			set => btCollisionObject_setUserIndex2(Native, value);
		}

		public int WorldArrayIndex
		{
			get => btCollisionObject_getWorldArrayIndex(Native);
			set => btCollisionObject_setWorldArrayIndex(Native, value);
		}

		public Matrix WorldTransform
		{
			get
			{
				Matrix value;
				btCollisionObject_getWorldTransform(Native, out value);
				return value;
			}
			set => btCollisionObject_setWorldTransform(Native, ref value);
		}

		public override bool Equals(object obj)
		{
			CollisionObject colObj = obj as CollisionObject;
			if (colObj == null)
			{
				return false;
			}
			return Native == colObj.Native;
		}

		public override int GetHashCode()
		{
			return Native.GetHashCode();
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
				if (btCollisionObject_getBroadphaseHandle(Native) != IntPtr.Zero)
				{
					BroadphaseHandle = null;
					//System.Diagnostics.Debugger.Break();
					return;
				}

				_isDisposed = true;

				IntPtr userPtr = btCollisionObject_getUserPointer(Native);
				GCHandle.FromIntPtr(userPtr).Free();
				btCollisionObject_delete(Native);
			}
		}

		~CollisionObject()
		{
			Dispose(false);
		}
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
		public float ContactDamping;
		public float ContactStiffness;
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

	[StructLayout(LayoutKind.Sequential)]
	internal struct CollisionObjectDoubleData
	{
		public IntPtr BroadphaseHandle;
		public IntPtr CollisionShape;
		public IntPtr RootCollisionShape;
		public IntPtr Name;
		public TransformDoubleData WorldTransform;
		public TransformDoubleData InterpolationWorldTransform;
		public Vector3DoubleData InterpolationLinearVelocity;
		public Vector3DoubleData InterpolationAngularVelocity;
		public Vector3DoubleData AnisotropicFriction;
		public double ContactProcessingThreshold;
		public double DeactivationTime;
		public double Friction;
		public double RollingFriction;
		public double ContactDamping;
		public double ContactStiffness;
		public double Restitution;
		public double HitFraction;
		public double CcdSweptSphereRadius;
		public double CcdMotionThreshold;
		public int HasAnisotropicFriction;
		public int CollisionFlags;
		public int IslandTag1;
		public int CompanionId;
		public int ActivationState1;
		public int InternalType;
		public int CheckCollideWith;
		public int Padding;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(CollisionObjectDoubleData), fieldName).ToInt32(); }
	}
}
