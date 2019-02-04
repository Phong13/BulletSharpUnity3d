using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public abstract class BroadphaseAabbCallback : IDisposable
	{
		internal IntPtr _native;

        [UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
        internal delegate bool ProcessUnmanagedDelegate(IntPtr proxy);

        internal ProcessUnmanagedDelegate _process;

		internal BroadphaseAabbCallback(IntPtr native)
		{
			_native = native;
            _process = ProcessUnmanaged;
		}

        protected BroadphaseAabbCallback()
        {
            _process = ProcessUnmanaged;
            _native = btBroadphaseAabbCallbackWrapper_new(
                Marshal.GetFunctionPointerForDelegate(_process));
        }

        private bool ProcessUnmanaged(IntPtr proxy)
        {
            return Process(BroadphaseProxy.GetManaged(proxy));
        }

		public abstract bool Process(BroadphaseProxy proxy);

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				btBroadphaseAabbCallback_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~BroadphaseAabbCallback()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btBroadphaseAabbCallbackWrapper_new(IntPtr process);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseAabbCallback_delete(IntPtr obj);
	}

	public abstract class BroadphaseRayCallback : BroadphaseAabbCallback
	{
        private UIntArray _signs;

        protected BroadphaseRayCallback()
            : base(IntPtr.Zero)
        {
            _native = btBroadphaseRayCallbackWrapper_new(
                Marshal.GetFunctionPointerForDelegate(_process));
        }

		public float LambdaMax
		{
			get { return btBroadphaseRayCallback_getLambda_max(_native); }
			set { btBroadphaseRayCallback_setLambda_max(_native, value); }
		}

		public Vector3 RayDirectionInverse
		{
			get
			{
				Vector3 value;
				btBroadphaseRayCallback_getRayDirectionInverse(_native, out value);
				return value;
			}
			set { btBroadphaseRayCallback_setRayDirectionInverse(_native, ref value); }
		}

		public UIntArray Signs
		{
            get
            {
                if (_signs == null)
                {
                    _signs = new UIntArray(btBroadphaseRayCallback_getSigns(_native), 3);
                }
                return _signs;
            }
		}

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btBroadphaseRayCallbackWrapper_new(IntPtr process);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btBroadphaseRayCallback_getLambda_max(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseRayCallback_getRayDirectionInverse(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBroadphaseRayCallback_getSigns(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseRayCallback_setLambda_max(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseRayCallback_setRayDirectionInverse(IntPtr obj, [In] ref Vector3 value);
	}

	public abstract class BroadphaseInterface : IDisposable
	{
		internal IntPtr _native;

        protected OverlappingPairCache _overlappingPairCache;
        internal List<CollisionWorld> _worldRefs = new List<CollisionWorld>(1);
        internal bool _worldDeferredCleanup;

		internal BroadphaseInterface(IntPtr native)
		{
			_native = native;
		}

        public void AabbTestRef(ref Vector3 aabbMin, ref Vector3 aabbMax, BroadphaseAabbCallback callback)
        {
            btBroadphaseInterface_aabbTest(_native, ref aabbMin, ref aabbMax, callback._native);
        }

		public void AabbTest(Vector3 aabbMin, Vector3 aabbMax, BroadphaseAabbCallback callback)
		{
			btBroadphaseInterface_aabbTest(_native, ref aabbMin, ref aabbMax, callback._native);
		}

		public void CalculateOverlappingPairs(Dispatcher dispatcher)
		{
			btBroadphaseInterface_calculateOverlappingPairs(_native, dispatcher._native);
		}

        public abstract BroadphaseProxy CreateProxy(ref Vector3 aabbMin, ref Vector3 aabbMax, int shapeType, IntPtr userPtr, short collisionFilterGroup, short collisionFilterMask, Dispatcher dispatcher, IntPtr multiSapProxy);

		public void DestroyProxy(BroadphaseProxy proxy, Dispatcher dispatcher)
		{
			btBroadphaseInterface_destroyProxy(_native, proxy._native, dispatcher._native);
		}

		public void GetAabb(BroadphaseProxy proxy, out Vector3 aabbMin, out Vector3 aabbMax)
		{
			btBroadphaseInterface_getAabb(_native, proxy._native, out aabbMin, out aabbMax);
		}

		public void GetBroadphaseAabb(out Vector3 aabbMin, out Vector3 aabbMax)
		{
			btBroadphaseInterface_getBroadphaseAabb(_native, out aabbMin, out aabbMax);
		}

		public void PrintStats()
		{
			btBroadphaseInterface_printStats(_native);
		}

        public void RayTestRef(ref Vector3 rayFrom, ref Vector3 rayTo, BroadphaseRayCallback rayCallback)
        {
            btBroadphaseInterface_rayTest(_native, ref rayFrom, ref rayTo, rayCallback._native);
        }

		public void RayTest(Vector3 rayFrom, Vector3 rayTo, BroadphaseRayCallback rayCallback)
		{
			btBroadphaseInterface_rayTest(_native, ref rayFrom, ref rayTo, rayCallback._native);
		}

        public void RayTestRef(ref Vector3 rayFrom, ref Vector3 rayTo, BroadphaseRayCallback rayCallback, ref Vector3 aabbMin, ref Vector3 aabbMax)
        {
            btBroadphaseInterface_rayTest3(_native, ref rayFrom, ref rayTo, rayCallback._native, ref aabbMin, ref aabbMax);
        }

		public void RayTest(Vector3 rayFrom, Vector3 rayTo, BroadphaseRayCallback rayCallback, Vector3 aabbMin, Vector3 aabbMax)
		{
			btBroadphaseInterface_rayTest3(_native, ref rayFrom, ref rayTo, rayCallback._native, ref aabbMin, ref aabbMax);
		}

		public void ResetPool(Dispatcher dispatcher)
		{
			btBroadphaseInterface_resetPool(_native, dispatcher._native);
		}

        public void SetAabbRef(BroadphaseProxy proxy, ref Vector3 aabbMin, ref Vector3 aabbMax, Dispatcher dispatcher)
        {
            btBroadphaseInterface_setAabb(_native, proxy._native, ref aabbMin, ref aabbMax, dispatcher._native);
        }

		public void SetAabb(BroadphaseProxy proxy, Vector3 aabbMin, Vector3 aabbMax, Dispatcher dispatcher)
		{
            SetAabbRef(proxy, ref aabbMin, ref aabbMax, dispatcher);
		}

		public OverlappingPairCache OverlappingPairCache
		{
			get { return _overlappingPairCache; }
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
                if (_worldRefs.Count == 0)
                {
                    btBroadphaseInterface_delete(_native);
                    _native = IntPtr.Zero;
                }
                else
                {
                    // Can't delete broadphase, because it is referenced by a world,
                    // tell the world to clean up the broadphase later.
                    _worldDeferredCleanup = true;
                }
			}
		}

		~BroadphaseInterface()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseInterface_aabbTest(IntPtr obj, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax, IntPtr callback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseInterface_calculateOverlappingPairs(IntPtr obj, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		internal static extern IntPtr btBroadphaseInterface_createProxy(IntPtr obj, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax, int shapeType, IntPtr userPtr, short collisionFilterGroup, short collisionFilterMask, IntPtr dispatcher, IntPtr multiSapProxy);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseInterface_destroyProxy(IntPtr obj, IntPtr proxy, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseInterface_getAabb(IntPtr obj, IntPtr proxy, [Out] out Vector3 aabbMin, [Out] out Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseInterface_getBroadphaseAabb(IntPtr obj, [Out] out Vector3 aabbMin, [Out] out Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		internal static extern IntPtr btBroadphaseInterface_getOverlappingPairCache(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseInterface_printStats(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseInterface_rayTest(IntPtr obj, [In] ref Vector3 rayFrom, [In] ref Vector3 rayTo, IntPtr rayCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseInterface_rayTest3(IntPtr obj, [In] ref Vector3 rayFrom, [In] ref Vector3 rayTo, IntPtr rayCallback, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseInterface_resetPool(IntPtr obj, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseInterface_setAabb(IntPtr obj, IntPtr proxy, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseInterface_delete(IntPtr obj);
	}
}
