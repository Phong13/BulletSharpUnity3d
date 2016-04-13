using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;
using AOT;

namespace BulletSharp
{
	[Flags]
	public enum ContactPointFlags
	{
		None = 0,
		LateralFrictionInitialized = 1,
		HasContactCfm = 2,
		HasContactErp = 4
	}

    public delegate void ContactAddedEventHandler(ManifoldPoint cp, CollisionObjectWrapper colObj0Wrap, int partId0, int index0, CollisionObjectWrapper colObj1Wrap, int partId1, int index1);

	public class ManifoldPoint : IDisposable
	{
		internal IntPtr _native;
		bool _preventDelete;

        static ContactAddedEventHandler _contactAdded;
        static ContactAddedUnmanagedDelegate _contactAddedUnmanaged;
        static IntPtr _contactAddedUnmanagedPtr;

        [UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
        private delegate bool ContactAddedUnmanagedDelegate(IntPtr cp, IntPtr colObj0Wrap, int partId0, int index0, IntPtr colObj1Wrap, int partId1, int index1);

		[MonoPInvokeCallback(typeof(ContactAddedUnmanagedDelegate))]
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
			_native = native;
			_preventDelete = preventDelete;
		}

		public ManifoldPoint()
		{
			_native = btManifoldPoint_new();
		}

		public ManifoldPoint(Vector3 pointA, Vector3 pointB, Vector3 normal, float distance)
		{
			_native = btManifoldPoint_new2(ref pointA, ref pointB, ref normal, distance);
		}

		public float AppliedImpulse
		{
			get { return btManifoldPoint_getAppliedImpulse(_native); }
			set { btManifoldPoint_setAppliedImpulse(_native, value); }
		}

		public float AppliedImpulseLateral1
		{
			get { return btManifoldPoint_getAppliedImpulseLateral1(_native); }
			set { btManifoldPoint_setAppliedImpulseLateral1(_native, value); }
		}

		public float AppliedImpulseLateral2
		{
			get { return btManifoldPoint_getAppliedImpulseLateral2(_native); }
			set { btManifoldPoint_setAppliedImpulseLateral2(_native, value); }
		}

		public float CombinedFriction
		{
			get { return btManifoldPoint_getCombinedFriction(_native); }
			set { btManifoldPoint_setCombinedFriction(_native, value); }
		}

		public float CombinedRestitution
		{
			get { return btManifoldPoint_getCombinedRestitution(_native); }
			set { btManifoldPoint_setCombinedRestitution(_native, value); }
		}

		public float CombinedRollingFriction
		{
			get { return btManifoldPoint_getCombinedRollingFriction(_native); }
			set { btManifoldPoint_setCombinedRollingFriction(_native, value); }
		}

		public float ContactCfm
		{
			get { return btManifoldPoint_getContactCFM(_native); }
			set { btManifoldPoint_setContactCFM(_native, value); }
		}

		public float ContactErp
		{
			get { return btManifoldPoint_getContactERP(_native); }
			set { btManifoldPoint_setContactERP(_native, value); }
		}

		public float ContactMotion1
		{
			get { return btManifoldPoint_getContactMotion1(_native); }
			set { btManifoldPoint_setContactMotion1(_native, value); }
		}

		public float ContactMotion2
		{
			get { return btManifoldPoint_getContactMotion2(_native); }
			set { btManifoldPoint_setContactMotion2(_native, value); }
		}

		public ContactPointFlags ContactPointFlags
		{
			get { return btManifoldPoint_getContactPointFlags(_native); }
			set { btManifoldPoint_setContactPointFlags(_native, value); }
		}

		public float Distance
		{
			get { return btManifoldPoint_getDistance(_native); }
			set { btManifoldPoint_setDistance(_native, value); }
		}

		public float Distance1
		{
			get { return btManifoldPoint_getDistance1(_native); }
			set { btManifoldPoint_setDistance1(_native, value); }
		}

		public float FrictionCfm
		{
			get { return btManifoldPoint_getFrictionCFM(_native); }
			set { btManifoldPoint_setFrictionCFM(_native, value); }
		}

		public int Index0
		{
			get { return btManifoldPoint_getIndex0(_native); }
			set { btManifoldPoint_setIndex0(_native, value); }
		}

		public int Index1
		{
			get { return btManifoldPoint_getIndex1(_native); }
			set { btManifoldPoint_setIndex1(_native, value); }
		}

		public Vector3 LateralFrictionDir1
		{
			get
			{
				Vector3 value;
				btManifoldPoint_getLateralFrictionDir1(_native, out value);
				return value;
			}
			set { btManifoldPoint_setLateralFrictionDir1(_native, ref value); }
		}

		public Vector3 LateralFrictionDir2
		{
			get
			{
				Vector3 value;
				btManifoldPoint_getLateralFrictionDir2(_native, out value);
				return value;
			}
			set { btManifoldPoint_setLateralFrictionDir2(_native, ref value); }
		}

		public int LifeTime
		{
			get { return btManifoldPoint_getLifeTime(_native); }
			set { btManifoldPoint_setLifeTime(_native, value); }
		}

		public Vector3 LocalPointA
		{
			get
			{
				Vector3 value;
				btManifoldPoint_getLocalPointA(_native, out value);
				return value;
			}
			set { btManifoldPoint_setLocalPointA(_native, ref value); }
		}

		public Vector3 LocalPointB
		{
			get
			{
				Vector3 value;
				btManifoldPoint_getLocalPointB(_native, out value);
				return value;
			}
			set { btManifoldPoint_setLocalPointB(_native, ref value); }
		}

		public Vector3 NormalWorldOnB
		{
			get
			{
				Vector3 value;
				btManifoldPoint_getNormalWorldOnB(_native, out value);
				return value;
			}
			set { btManifoldPoint_setNormalWorldOnB(_native, ref value); }
		}

		public int PartId0
		{
			get { return btManifoldPoint_getPartId0(_native); }
			set { btManifoldPoint_setPartId0(_native, value); }
		}

		public int PartId1
		{
			get { return btManifoldPoint_getPartId1(_native); }
			set { btManifoldPoint_setPartId1(_native, value); }
		}

		public Vector3 PositionWorldOnA
		{
			get
			{
				Vector3 value;
				btManifoldPoint_getPositionWorldOnA(_native, out value);
				return value;
			}
			set { btManifoldPoint_setPositionWorldOnA(_native, ref value); }
		}

		public Vector3 PositionWorldOnB
		{
			get
			{
				Vector3 value;
				btManifoldPoint_getPositionWorldOnB(_native, out value);
				return value;
			}
			set { btManifoldPoint_setPositionWorldOnB(_native, ref value); }
		}

		public Object UserPersistentData
		{
			get
            {
                IntPtr valuePtr = btManifoldPoint_getUserPersistentData(_native);
                return (valuePtr != IntPtr.Zero) ? GCHandle.FromIntPtr(valuePtr).Target : null;
            }
			set
            {
                IntPtr prevPtr = btManifoldPoint_getUserPersistentData(_native);
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
                    btManifoldPoint_setUserPersistentData(_native, GCHandle.ToIntPtr(handle));
                }
                else
                {
                    btManifoldPoint_setUserPersistentData(_native, IntPtr.Zero);
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
			if (_native != IntPtr.Zero)
			{
				if (!_preventDelete)
				{
					btManifoldPoint_delete(_native);
				}
				_native = IntPtr.Zero;
			}
		}

		~ManifoldPoint()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btManifoldPoint_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btManifoldPoint_new2([In] ref Vector3 pointA, [In] ref Vector3 pointB, [In] ref Vector3 normal, float distance);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btManifoldPoint_getAppliedImpulse(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btManifoldPoint_getAppliedImpulseLateral1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btManifoldPoint_getAppliedImpulseLateral2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btManifoldPoint_getCombinedFriction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btManifoldPoint_getCombinedRestitution(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btManifoldPoint_getCombinedRollingFriction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btManifoldPoint_getContactCFM(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btManifoldPoint_getContactERP(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btManifoldPoint_getContactMotion1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btManifoldPoint_getContactMotion2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern ContactPointFlags btManifoldPoint_getContactPointFlags(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btManifoldPoint_getDistance(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btManifoldPoint_getDistance1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btManifoldPoint_getFrictionCFM(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btManifoldPoint_getIndex0(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btManifoldPoint_getIndex1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_getLateralFrictionDir1(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_getLateralFrictionDir2(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btManifoldPoint_getLifeTime(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_getLocalPointA(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_getLocalPointB(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_getNormalWorldOnB(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btManifoldPoint_getPartId0(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btManifoldPoint_getPartId1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_getPositionWorldOnA(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_getPositionWorldOnB(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btManifoldPoint_getUserPersistentData(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setAppliedImpulse(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setAppliedImpulseLateral1(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setAppliedImpulseLateral2(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setCombinedFriction(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setCombinedRestitution(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setCombinedRollingFriction(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setContactCFM(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setContactERP(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setContactMotion1(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setContactMotion2(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setContactPointFlags(IntPtr obj, ContactPointFlags value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setDistance(IntPtr obj, float dist);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setDistance1(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setFrictionCFM(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setIndex0(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setIndex1(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setLateralFrictionDir1(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setLateralFrictionDir2(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setLifeTime(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setLocalPointA(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setLocalPointB(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setNormalWorldOnB(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setPartId0(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setPartId1(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setPositionWorldOnA(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setPositionWorldOnB(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setUserPersistentData(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_delete(IntPtr obj);

        //[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        //static extern IntPtr getGContactAddedCallback();
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void setGContactAddedCallback(IntPtr value);
	}
}
