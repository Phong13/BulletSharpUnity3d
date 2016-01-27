using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Collections.Generic;

namespace BulletSharp
{
	[Flags]
	public enum DispatcherFlags
	{
		None = 0,
		StaticStaticReported = 1,
		UseRelativeContactBreakingThreshold = 2,
        DisableContactPoolDynamicAllocation = 4
	}

    public delegate void NearCallback(BroadphasePair collisionPair, CollisionDispatcher dispatcher, DispatcherInfo dispatchInfo);

	public class CollisionDispatcher : Dispatcher
	{
        [UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
        private delegate void NearCallbackUnmanagedDelegate(IntPtr collisionPair, IntPtr dispatcher, IntPtr dispatchInfo);

        protected CollisionConfiguration _collisionConfiguration;
		private NearCallback _nearCallback;
        private List<CollisionAlgorithmCreateFunc> _collisionCreateFuncs;
        private NearCallbackUnmanagedDelegate _nearCallbackUnmanaged;
        private IntPtr _nearCallbackUnmanagedPtr;

		internal CollisionDispatcher(IntPtr native)
			: base(native)
		{
		}

		public CollisionDispatcher(CollisionConfiguration collisionConfiguration)
			: base(btCollisionDispatcher_new(collisionConfiguration._native))
		{
			_collisionConfiguration = collisionConfiguration;
		}

		public static void DefaultNearCallback(BroadphasePair collisionPair, CollisionDispatcher dispatcher, DispatcherInfo dispatchInfo)
		{
			btCollisionDispatcher_defaultNearCallback(collisionPair._native, dispatcher._native, dispatchInfo._native);
		}

        private void NearCallbackUnmanaged(IntPtr collisionPair, IntPtr dispatcher, IntPtr dispatchInfo)
        {
            System.Diagnostics.Debug.Assert(dispatcher == _native);

            _nearCallback(new BroadphasePair(collisionPair), this, new DispatcherInfo(dispatchInfo));
        }

        public void RegisterCollisionCreateFunc(BroadphaseNativeType proxyType0, BroadphaseNativeType proxyType1, CollisionAlgorithmCreateFunc createFunc)
		{
            if (_collisionCreateFuncs == null)
            {
                _collisionCreateFuncs = new List<CollisionAlgorithmCreateFunc>();
            }
            _collisionCreateFuncs.Add(createFunc);

			btCollisionDispatcher_registerCollisionCreateFunc(_native, proxyType0, proxyType1, createFunc._native);
		}

		public CollisionConfiguration CollisionConfiguration
		{
			get { return _collisionConfiguration; }
			set
			{
				btCollisionDispatcher_setCollisionConfiguration(_native, value._native);
				_collisionConfiguration = value;
			}
		}

        public DispatcherFlags DispatcherFlags
		{
			get { return btCollisionDispatcher_getDispatcherFlags(_native); }
			set { btCollisionDispatcher_setDispatcherFlags(_native, value); }
		}

		public NearCallback NearCallback
		{
			get { return _nearCallback; }
			set
			{
				_nearCallback = value;

                if (value == null)
                {
                    btCollisionDispatcher_setNearCallback(_native, IntPtr.Zero);
                    _nearCallbackUnmanaged = null;
                    return;
                }

                if (_nearCallbackUnmanaged == null)
                {
                    _nearCallbackUnmanaged = new NearCallbackUnmanagedDelegate(NearCallbackUnmanaged);
                    _nearCallbackUnmanagedPtr = Marshal.GetFunctionPointerForDelegate(_nearCallbackUnmanaged);
                }
                btCollisionDispatcher_setNearCallback(_native, _nearCallbackUnmanagedPtr);
			}
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCollisionDispatcher_new(IntPtr collisionConfiguration);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionDispatcher_defaultNearCallback(IntPtr collisionPair, IntPtr dispatcher, IntPtr dispatchInfo);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCollisionDispatcher_getCollisionConfiguration(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern DispatcherFlags btCollisionDispatcher_getDispatcherFlags(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCollisionDispatcher_getNearCallback(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionDispatcher_registerCollisionCreateFunc(IntPtr obj, BroadphaseNativeType proxyType0, BroadphaseNativeType proxyType1, IntPtr createFunc);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionDispatcher_setCollisionConfiguration(IntPtr obj, IntPtr config);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionDispatcher_setDispatcherFlags(IntPtr obj, DispatcherFlags flags);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionDispatcher_setNearCallback(IntPtr obj, IntPtr nearCallback);
	}
}
