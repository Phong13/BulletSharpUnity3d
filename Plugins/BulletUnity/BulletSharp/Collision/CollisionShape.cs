using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public abstract class CollisionShape : IDisposable
	{
		internal IntPtr _native;
		bool _preventDelete;
        bool _isDisposed;

        internal static CollisionShape GetManaged(IntPtr obj)
        {
            if (obj == IntPtr.Zero)
            {
                return null;
            }

            IntPtr userPtr = btCollisionShape_getUserPointer(obj);
            return GCHandle.FromIntPtr(userPtr).Target as CollisionShape;
        }

        internal CollisionShape(IntPtr native, bool preventDelete = false)
		{
			_native = native;
            if (preventDelete)
            {
                _preventDelete = true;
            }
            else
            {
                GCHandle handle = GCHandle.Alloc(this, GCHandleType.Normal);
                btCollisionShape_setUserPointer(native, GCHandle.ToIntPtr(handle));
            }
		}

        public Vector3 CalculateLocalInertia(float mass)
        {
            Vector3 inertia;
            btCollisionShape_calculateLocalInertia(_native, mass, out inertia);
            return inertia;
        }

		public void CalculateLocalInertia(float mass, out Vector3 inertia)
		{
			btCollisionShape_calculateLocalInertia(_native, mass, out inertia);
		}

		public int CalculateSerializeBufferSize()
		{
			return btCollisionShape_calculateSerializeBufferSize(_native);
		}

        public void CalculateTemporalAabb(ref Matrix curTrans, ref Vector3 linvel, ref Vector3 angvel, float timeStep, out Vector3 temporalAabbMin, out Vector3 temporalAabbMax)
		{
			btCollisionShape_calculateTemporalAabb(_native, ref curTrans, ref linvel, ref angvel, timeStep, out temporalAabbMin, out temporalAabbMax);
		}

        public void GetAabbRef(ref Matrix t, out Vector3 aabbMin, out Vector3 aabbMax)
        {
            btCollisionShape_getAabb(_native, ref t, out aabbMin, out aabbMax);
        }

		public void GetAabb(Matrix t, out Vector3 aabbMin, out Vector3 aabbMax)
		{
			btCollisionShape_getAabb(_native, ref t, out aabbMin, out aabbMax);
		}

		public void GetBoundingSphere(out Vector3 center, out float radius)
		{
			btCollisionShape_getBoundingSphere(_native, out center, out radius);
		}

		public float GetContactBreakingThreshold(float defaultContactThresholdFactor)
		{
			return btCollisionShape_getContactBreakingThreshold(_native, defaultContactThresholdFactor);
		}

		public virtual string Serialize(IntPtr dataBuffer, Serializer serializer)
		{
            return Marshal.PtrToStringAnsi(btCollisionShape_serialize(_native, dataBuffer, serializer._native));
            /*
            IntPtr name = serializer.FindNameForPointer(_native);
            IntPtr namePtr = serializer.GetUniquePointer(name);
            Marshal.WriteIntPtr(dataBuffer, namePtr);
            if (namePtr != IntPtr.Zero)
            {
                serializer.SerializeName(name);
            }
            Marshal.WriteInt32(dataBuffer, IntPtr.Size, (int)ShapeType);
            //Marshal.WriteInt32(dataBuffer, IntPtr.Size + sizeof(int), 0); //padding
            return "btCollisionShapeData";
            */
		}

		public void SerializeSingleShape(Serializer serializer)
		{
            int len = CalculateSerializeBufferSize();
            Chunk chunk = serializer.Allocate((uint)len, 1);
            string structType = Serialize(chunk.OldPtr, serializer);
            serializer.FinalizeChunk(chunk, structType, DnaID.Shape, _native);
		}

		public float AngularMotionDisc
		{
			get { return btCollisionShape_getAngularMotionDisc(_native); }
		}

		public Vector3 AnisotropicRollingFrictionDirection
		{
			get
			{
				Vector3 value;
				btCollisionShape_getAnisotropicRollingFrictionDirection(_native, out value);
				return value;
			}
		}

		public bool IsCompound
		{
			get { return btCollisionShape_isCompound(_native); }
		}

		public bool IsConcave
		{
			get { return btCollisionShape_isConcave(_native); }
		}

		public bool IsConvex
		{
			get { return btCollisionShape_isConvex(_native); }
		}

		public bool IsConvex2D
		{
			get { return btCollisionShape_isConvex2d(_native); }
		}

        public bool IsDisposed
        {
            get { return _isDisposed; }
        }

		public bool IsInfinite
		{
			get { return btCollisionShape_isInfinite(_native); }
		}

		public bool IsNonMoving
		{
			get { return btCollisionShape_isNonMoving(_native); }
		}

		public bool IsPolyhedral
		{
			get { return btCollisionShape_isPolyhedral(_native); }
		}

		public bool IsSoftBody
		{
			get { return btCollisionShape_isSoftBody(_native); }
		}

		public Vector3 LocalScaling
		{
			get
			{
				Vector3 value;
				btCollisionShape_getLocalScaling(_native, out value);
				return value;
			}
			set { btCollisionShape_setLocalScaling(_native, ref value); }
		}

		public float Margin
		{
			get { return btCollisionShape_getMargin(_native); }
			set { btCollisionShape_setMargin(_native, value); }
		}

		public string Name
		{
            get { return Marshal.PtrToStringAnsi(btCollisionShape_getName(_native)); }
		}

        public BroadphaseNativeType ShapeType
		{
			get { return btCollisionShape_getShapeType(_native); }
		}

        public Object UserObject { get; set; }

		public int UserIndex
		{
			get { return btCollisionShape_getUserIndex(_native); }
			set { btCollisionShape_setUserIndex(_native, value); }
		}

        public override bool Equals(object obj)
        {
            return object.ReferenceEquals(this, obj);
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
                _isDisposed = true;

                if (!_preventDelete)
                {
                    IntPtr userPtr = btCollisionShape_getUserPointer(_native);
                    GCHandle.FromIntPtr(userPtr).Free();

                    btCollisionShape_delete(_native);
                }
			}
		}

		~CollisionShape()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionShape_calculateLocalInertia(IntPtr obj, float mass, [Out] out Vector3 inertia);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btCollisionShape_calculateSerializeBufferSize(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionShape_calculateTemporalAabb(IntPtr obj, [In] ref Matrix curTrans, [In] ref Vector3 linvel, [In] ref Vector3 angvel, float timeStep, [Out] out Vector3 temporalAabbMin, [Out] out Vector3 temporalAabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionShape_getAabb(IntPtr obj, [In] ref Matrix t, [Out] out Vector3 aabbMin, [Out] out Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btCollisionShape_getAngularMotionDisc(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionShape_getAnisotropicRollingFrictionDirection(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionShape_getBoundingSphere(IntPtr obj, [Out] out Vector3 center, [Out] out float radius);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btCollisionShape_getContactBreakingThreshold(IntPtr obj, float defaultContactThresholdFactor);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionShape_getLocalScaling(IntPtr obj, [Out] out Vector3 scaling);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btCollisionShape_getMargin(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCollisionShape_getName(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern BroadphaseNativeType btCollisionShape_getShapeType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btCollisionShape_getUserIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCollisionShape_getUserPointer(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btCollisionShape_isCompound(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btCollisionShape_isConcave(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btCollisionShape_isConvex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btCollisionShape_isConvex2d(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btCollisionShape_isInfinite(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btCollisionShape_isNonMoving(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btCollisionShape_isPolyhedral(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btCollisionShape_isSoftBody(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCollisionShape_serialize(IntPtr obj, IntPtr dataBuffer, IntPtr serializer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionShape_serializeSingleShape(IntPtr obj, IntPtr serializer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionShape_setLocalScaling(IntPtr obj, [In] ref Vector3 scaling);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionShape_setMargin(IntPtr obj, float margin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionShape_setUserIndex(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionShape_setUserPointer(IntPtr obj, IntPtr userPtr);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionShape_delete(IntPtr obj);
	}

    [StructLayout(LayoutKind.Sequential)]
    internal struct CollisionShapeFloatData
    {
        public IntPtr Name;
        public int ShapeType;
        public int Padding;

        public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(CollisionShapeFloatData), fieldName).ToInt32(); }
    }
}
