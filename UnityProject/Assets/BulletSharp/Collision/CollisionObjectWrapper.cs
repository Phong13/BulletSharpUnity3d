using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class CollisionObjectWrapper
	{
		internal IntPtr _native;

		internal CollisionObjectWrapper(IntPtr native)
		{
			_native = native;
		}

		public CollisionObject CollisionObject
		{
			get { return CollisionObject.GetManaged(btCollisionObjectWrapper_getCollisionObject(_native)); }
			set { btCollisionObjectWrapper_setCollisionObject(_native, value._native); }
		}

		public CollisionShape CollisionShape
		{
			get { return CollisionShape.GetManaged(btCollisionObjectWrapper_getCollisionShape(_native)); }
            set { btCollisionObjectWrapper_setShape(_native, value._native); }
		}

		public int Index
		{
			get { return btCollisionObjectWrapper_getIndex(_native); }
			set { btCollisionObjectWrapper_setIndex(_native, value); }
		}

		public CollisionObjectWrapper Parent
		{
			get { return new CollisionObjectWrapper(btCollisionObjectWrapper_getParent(_native)); }
			set { btCollisionObjectWrapper_setParent(_native, value._native); }
		}

		public int PartId
		{
			get { return btCollisionObjectWrapper_getPartId(_native); }
			set { btCollisionObjectWrapper_setPartId(_native, value); }
		}

		public Matrix WorldTransform
		{
			get
			{
				Matrix value;
				btCollisionObjectWrapper_getWorldTransform(_native, out value);
				return value;
			}
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCollisionObjectWrapper_getCollisionObject(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCollisionObjectWrapper_getCollisionShape(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btCollisionObjectWrapper_getIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCollisionObjectWrapper_getParent(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btCollisionObjectWrapper_getPartId(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionObjectWrapper_getWorldTransform(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionObjectWrapper_setCollisionObject(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionObjectWrapper_setIndex(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionObjectWrapper_setParent(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionObjectWrapper_setPartId(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionObjectWrapper_setShape(IntPtr obj, IntPtr value);
	}
}
