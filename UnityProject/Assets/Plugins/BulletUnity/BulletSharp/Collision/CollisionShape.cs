using System;
using System.Runtime.InteropServices;
using BulletSharp.Math;

namespace BulletSharp
{
    public abstract class CollisionShape : IDisposable
    {
        internal IntPtr Native;
        private bool _preventDelete;
        private bool _isDisposed;

        internal static CollisionShape GetManaged(IntPtr obj)
        {
            if (obj == IntPtr.Zero)
            {
                return null;
            }

            IntPtr userPtr = UnsafeNativeMethods.btCollisionShape_getUserPointer(obj);
            return GCHandle.FromIntPtr(userPtr).Target as CollisionShape;
        }

        internal CollisionShape(IntPtr native, bool preventDelete = false)
        {
            Native = native;
            if (preventDelete)
            {
                _preventDelete = true;
            }
            else
            {
                GCHandle handle = GCHandle.Alloc(this, GCHandleType.Normal);
                UnsafeNativeMethods.btCollisionShape_setUserPointer(native, GCHandle.ToIntPtr(handle));
            }
        }

        public Vector3 CalculateLocalInertia(float mass)
        {
            Vector3 inertia;
            UnsafeNativeMethods.btCollisionShape_calculateLocalInertia(Native, mass, out inertia);
            return inertia;
        }

        public void CalculateLocalInertia(float mass, out Vector3 inertia)
        {
            UnsafeNativeMethods.btCollisionShape_calculateLocalInertia(Native, mass, out inertia);
        }

        public int CalculateSerializeBufferSize()
        {
            return UnsafeNativeMethods.btCollisionShape_calculateSerializeBufferSize(Native);
        }

        public void CalculateTemporalAabbRef(ref Matrix curTrans, ref Vector3 linvel, ref Vector3 angvel,
            float timeStep, out Vector3 temporalAabbMin, out Vector3 temporalAabbMax)
        {
            UnsafeNativeMethods.btCollisionShape_calculateTemporalAabb(Native, ref curTrans, ref linvel, ref angvel, timeStep, out temporalAabbMin, out temporalAabbMax);
        }

        public void CalculateTemporalAabb(Matrix curTrans, Vector3 linvel, Vector3 angvel,
            float timeStep, out Vector3 temporalAabbMin, out Vector3 temporalAabbMax)
        {
            UnsafeNativeMethods.btCollisionShape_calculateTemporalAabb(Native, ref curTrans, ref linvel,
                ref angvel, timeStep, out temporalAabbMin, out temporalAabbMax);
        }

        public void GetAabbRef(ref Matrix t, out Vector3 aabbMin, out Vector3 aabbMax)
        {
            UnsafeNativeMethods.btCollisionShape_getAabb(Native, ref t, out aabbMin, out aabbMax);
        }

        public void GetAabb(Matrix t, out Vector3 aabbMin, out Vector3 aabbMax)
        {
            UnsafeNativeMethods.btCollisionShape_getAabb(Native, ref t, out aabbMin, out aabbMax);
        }

        public void GetBoundingSphere(out Vector3 center, out float radius)
        {
            UnsafeNativeMethods.btCollisionShape_getBoundingSphere(Native, out center, out radius);
        }

        public float GetContactBreakingThreshold(float defaultContactThresholdFactor)
        {
            return UnsafeNativeMethods.btCollisionShape_getContactBreakingThreshold(Native, defaultContactThresholdFactor);
        }

        public virtual string Serialize(IntPtr dataBuffer, Serializer serializer)
        {
            return Marshal.PtrToStringAnsi(UnsafeNativeMethods.btCollisionShape_serialize(Native, dataBuffer, serializer._native));
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
            serializer.FinalizeChunk(chunk, structType, DnaID.Shape, Native);
        }

        public float AngularMotionDisc { get { return UnsafeNativeMethods.btCollisionShape_getAngularMotionDisc(Native); } }

        public Vector3 AnisotropicRollingFrictionDirection
        {
            get
            {
                Vector3 value;
                UnsafeNativeMethods.btCollisionShape_getAnisotropicRollingFrictionDirection(Native, out value);
                return value;
            }
        }

        public bool IsCompound { get { return UnsafeNativeMethods.btCollisionShape_isCompound(Native); } }

        public bool IsConcave
        {
            get
            {
                return UnsafeNativeMethods.btCollisionShape_isConcave(Native);
            }
        }

        public bool IsConvex
        {
            get
            {
                return UnsafeNativeMethods.btCollisionShape_isConvex(Native);
            }
        }

        public bool IsConvex2D
        {
            get
            {
                return UnsafeNativeMethods.btCollisionShape_isConvex2d(Native);
            }
        }

        public bool IsDisposed
        {
            get
            {
                return _isDisposed;
            }
        }

        public bool IsInfinite
        {
            get
            {
                return UnsafeNativeMethods.btCollisionShape_isInfinite(Native);
            }
        }

        public bool IsNonMoving
        {
            get
            {
                return UnsafeNativeMethods.btCollisionShape_isNonMoving(Native);
            }
        }

        public bool IsPolyhedral
        {
            get
            {
                return UnsafeNativeMethods.btCollisionShape_isPolyhedral(Native);
            }
        }

        public bool IsSoftBody
        {
            get
            {
                return UnsafeNativeMethods.btCollisionShape_isSoftBody(Native);
            }
        }

        public Vector3 LocalScaling
        {
            get
            {
                Vector3 value;
                UnsafeNativeMethods.btCollisionShape_getLocalScaling(Native, out value);
				return value; }

        
        set { UnsafeNativeMethods.btCollisionShape_setLocalScaling(Native, ref value);}
		}

		public float Margin
		{
			get { return UnsafeNativeMethods.btCollisionShape_getMargin(Native); }
			set { UnsafeNativeMethods.btCollisionShape_setMargin(Native, value); }
		}

		public string Name { get { return Marshal.PtrToStringAnsi(UnsafeNativeMethods.btCollisionShape_getName(Native)); } }

		public BroadphaseNativeType ShapeType { get { return UnsafeNativeMethods.btCollisionShape_getShapeType(Native); } }

		public object UserObject { get; set; }

		public int UserIndex
		{
			get { return UnsafeNativeMethods.btCollisionShape_getUserIndex(Native); }
			set { UnsafeNativeMethods.btCollisionShape_setUserIndex(Native, value); }
		}

		public override bool Equals(object obj)
		{
			return ReferenceEquals(this, obj);
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
				_isDisposed = true;

				if (!_preventDelete)
				{
					IntPtr userPtr = UnsafeNativeMethods.btCollisionShape_getUserPointer(Native);
					GCHandle.FromIntPtr(userPtr).Free();

                    UnsafeNativeMethods.btCollisionShape_delete(Native);
				}
			}
		}

		~CollisionShape()
		{
			Dispose(false);
		}
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
