using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class CollisionAlgorithmCreateFunc : IDisposable
	{
		internal IntPtr _native;
		bool _preventDelete;

		internal CollisionAlgorithmCreateFunc(IntPtr native, bool preventDelete)
		{
			_native = native;
			_preventDelete = preventDelete;
		}

		public CollisionAlgorithmCreateFunc()
		{
			_native = btCollisionAlgorithmCreateFunc_new();
		}

		public CollisionAlgorithm CreateCollisionAlgorithm(CollisionAlgorithmConstructionInfo __unnamed0, CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap)
		{
            return new CollisionAlgorithm(btCollisionAlgorithmCreateFunc_CreateCollisionAlgorithm(_native, __unnamed0._native, body0Wrap._native, body1Wrap._native));
		}

		public bool Swapped
		{
			get { return btCollisionAlgorithmCreateFunc_getSwapped(_native); }
			set { btCollisionAlgorithmCreateFunc_setSwapped(_native, value); }
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
					btCollisionAlgorithmCreateFunc_delete(_native);
				}
				_native = IntPtr.Zero;
			}
		}

		~CollisionAlgorithmCreateFunc()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCollisionAlgorithmCreateFunc_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCollisionAlgorithmCreateFunc_CreateCollisionAlgorithm(IntPtr obj, IntPtr __unnamed0, IntPtr body0Wrap, IntPtr body1Wrap);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btCollisionAlgorithmCreateFunc_getSwapped(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionAlgorithmCreateFunc_setSwapped(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionAlgorithmCreateFunc_delete(IntPtr obj);
	}
}
