using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	[Flags]
	public enum ContactPointFlags
	{
		None = 0,
		LateralFrictionInitialized = 1,
		HasContactCfm = 2,
		HasContactErp = 4,
		ContactStiffnessDamping = 8,
		FrictionAnchor = 16
	}

	public delegate void ContactAddedEventHandler(ManifoldPoint cp, CollisionObjectWrapper colObj0Wrap, int partId0, int index0, CollisionObjectWrapper colObj1Wrap, int partId1, int index1);

	public class ManifoldPoint : IDisposable
	{
		internal IntPtr Native;
		private bool _preventDelete;

		private static ContactAddedEventHandler _contactAdded;
		private static ContactAddedUnmanagedDelegate _contactAddedUnmanaged;
		private static IntPtr _contactAddedUnmanagedPtr;

		[UnmanagedFunctionPointer(BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
		private delegate bool ContactAddedUnmanagedDelegate(IntPtr cp, IntPtr colObj0Wrap, int partId0, int index0, IntPtr colObj1Wrap, int partId1, int index1);

		static bool ContactAddedUnmanaged(IntPtr cp, IntPtr colObj0Wrap, int partId0, int index0, IntPtr colObj1Wrap, int partId1, int index1)
		{
			_contactAdded.Invoke(new ManifoldPoint(cp, true), new CollisionObjectWrapper(colObj0Wrap), partId0, index0, new CollisionObjectWrapper(colObj1Wrap), partId1, index1);
			return false;
		}

		public static event ContactAddedEventHandler ContactAdded
		{
			add
			{
				if (_contactAddedUnmanaged == null)
				{
					_contactAddedUnmanaged = new ContactAddedUnmanagedDelegate(ContactAddedUnmanaged);
					_contactAddedUnmanagedPtr = Marshal.GetFunctionPointerForDelegate(_contactAddedUnmanaged);
				}
				setGContactAddedCallback(_contactAddedUnmanagedPtr);
				_contactAdded += value;
			}
			remove
			{
				_contactAdded -= value;
				if (_contactAdded == null)
				{
					setGContactAddedCallback(IntPtr.Zero);
				}
			}
		}

		internal ManifoldPoint(IntPtr native, bool preventDelete)
		{
			Native = native;
			_preventDelete = preventDelete;
		}

		public ManifoldPoint()
		{
			Native = btManifoldPoint_new();
		}

		public ManifoldPoint(Vector3 pointA, Vector3 pointB, Vector3 normal, float distance)
		{
			Native = btManifoldPoint_new2(ref pointA, ref pointB, ref normal, distance);
		}

		public float AppliedImpulse
		{
			get => btManifoldPoint_getAppliedImpulse(Native);
			set => btManifoldPoint_setAppliedImpulse(Native, value);
		}

		public float AppliedImpulseLateral1
		{
			get => btManifoldPoint_getAppliedImpulseLateral1(Native);
			set => btManifoldPoint_setAppliedImpulseLateral1(Native, value);
		}

		public float AppliedImpulseLateral2
		{
			get => btManifoldPoint_getAppliedImpulseLateral2(Native);
			set => btManifoldPoint_setAppliedImpulseLateral2(Native, value);
		}

		public float CombinedContactDamping1
		{
			get => btManifoldPoint_getCombinedContactDamping1(Native);
			set => btManifoldPoint_setCombinedContactDamping1(Native, value);
		}

		public float CombinedContactStiffness1
		{
			get => btManifoldPoint_getCombinedContactStiffness1(Native);
			set => btManifoldPoint_setCombinedContactStiffness1(Native, value);
		}

		public float CombinedFriction
		{
			get => btManifoldPoint_getCombinedFriction(Native);
			set => btManifoldPoint_setCombinedFriction(Native, value);
		}

		public float CombinedRestitution
		{
			get => btManifoldPoint_getCombinedRestitution(Native);
			set => btManifoldPoint_setCombinedRestitution(Native, value);
		}

		public float CombinedRollingFriction
		{
			get => btManifoldPoint_getCombinedRollingFriction(Native);
			set => btManifoldPoint_setCombinedRollingFriction(Native, value);
		}

		public float ContactCfm
		{
			get => btManifoldPoint_getContactCFM(Native);
			set => btManifoldPoint_setContactCFM(Native, value);
		}

		public float ContactErp
		{
			get => btManifoldPoint_getContactERP(Native);
			set => btManifoldPoint_setContactERP(Native, value);
		}

		public float ContactMotion1
		{
			get => btManifoldPoint_getContactMotion1(Native);
			set => btManifoldPoint_setContactMotion1(Native, value);
		}

		public float ContactMotion2
		{
			get => btManifoldPoint_getContactMotion2(Native);
			set => btManifoldPoint_setContactMotion2(Native, value);
		}

		public ContactPointFlags ContactPointFlags
		{
			get => btManifoldPoint_getContactPointFlags(Native);
			set => btManifoldPoint_setContactPointFlags(Native, value);
		}

		public float Distance
		{
			get => btManifoldPoint_getDistance(Native);
			set => btManifoldPoint_setDistance(Native, value);
		}

		public float Distance1
		{
			get => btManifoldPoint_getDistance1(Native);
			set => btManifoldPoint_setDistance1(Native, value);
		}

		public float FrictionCfm
		{
			get => btManifoldPoint_getFrictionCFM(Native);
			set => btManifoldPoint_setFrictionCFM(Native, value);
		}

		public int Index0
		{
			get => btManifoldPoint_getIndex0(Native);
			set => btManifoldPoint_setIndex0(Native, value);
		}

		public int Index1
		{
			get => btManifoldPoint_getIndex1(Native);
			set => btManifoldPoint_setIndex1(Native, value);
		}

		public Vector3 LateralFrictionDir1
		{
			get
			{
				Vector3 value;
				btManifoldPoint_getLateralFrictionDir1(Native, out value);
				return value;
			}
			set => btManifoldPoint_setLateralFrictionDir1(Native, ref value);
		}

		public Vector3 LateralFrictionDir2
		{
			get
			{
				Vector3 value;
				btManifoldPoint_getLateralFrictionDir2(Native, out value);
				return value;
			}
			set => btManifoldPoint_setLateralFrictionDir2(Native, ref value);
		}

		public int LifeTime
		{
			get => btManifoldPoint_getLifeTime(Native);
            set => btManifoldPoint_setLifeTime(Native, value);
		}

		public Vector3 LocalPointA
		{
			get
			{
				Vector3 value;
				btManifoldPoint_getLocalPointA(Native, out value);
				return value;
			}
			set => btManifoldPoint_setLocalPointA(Native, ref value);
		}

		public Vector3 LocalPointB
		{
			get
			{
				Vector3 value;
				btManifoldPoint_getLocalPointB(Native, out value);
				return value;
			}
			set => btManifoldPoint_setLocalPointB(Native, ref value);
		}

		public Vector3 NormalWorldOnB
		{
			get
			{
				Vector3 value;
				btManifoldPoint_getNormalWorldOnB(Native, out value);
				return value;
			}
			set => btManifoldPoint_setNormalWorldOnB(Native, ref value);
		}

		public int PartId0
		{
			get => btManifoldPoint_getPartId0(Native);
			set => btManifoldPoint_setPartId0(Native, value);
		}

		public int PartId1
		{
			get => btManifoldPoint_getPartId1(Native);
			set => btManifoldPoint_setPartId1(Native, value);
		}

		public Vector3 PositionWorldOnA
		{
			get
			{
				Vector3 value;
				btManifoldPoint_getPositionWorldOnA(Native, out value);
				return value;
			}
			set => btManifoldPoint_setPositionWorldOnA(Native, ref value);
		}

		public Vector3 PositionWorldOnB
		{
			get
			{
				Vector3 value;
				btManifoldPoint_getPositionWorldOnB(Native, out value);
				return value;
			}
			set => btManifoldPoint_setPositionWorldOnB(Native, ref value);
		}

		public Object UserPersistentData
		{
			get
			{
				IntPtr valuePtr = btManifoldPoint_getUserPersistentData(Native);
				return (valuePtr != IntPtr.Zero) ? GCHandle.FromIntPtr(valuePtr).Target : null;
			}
			set
			{
				IntPtr prevPtr = btManifoldPoint_getUserPersistentData(Native);
				if (prevPtr != IntPtr.Zero)
				{
					GCHandle prevHandle = GCHandle.FromIntPtr(prevPtr);
					if (ReferenceEquals(value, prevHandle.Target))
					{
						return;
					}
					prevHandle.Free();
				}
				if (value != null)
				{
					GCHandle handle = GCHandle.Alloc(value);
					btManifoldPoint_setUserPersistentData(Native, GCHandle.ToIntPtr(handle));
				}
				else
				{
					btManifoldPoint_setUserPersistentData(Native, IntPtr.Zero);
				}
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
					btManifoldPoint_delete(Native);
				}
				Native = IntPtr.Zero;
			}
		}

		~ManifoldPoint()
		{
			Dispose(false);
		}
	}
}
