using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class CollisionAlgorithmConstructionInfo : IDisposable
	{
		internal IntPtr _native;

		private Dispatcher _dispatcher1;
		private PersistentManifold _manifold;

		public CollisionAlgorithmConstructionInfo()
		{
			_native = btCollisionAlgorithmConstructionInfo_new();
		}

		public CollisionAlgorithmConstructionInfo(Dispatcher dispatcher, int temp)
		{
            _dispatcher1 = dispatcher;
            _native = btCollisionAlgorithmConstructionInfo_new2((dispatcher != null) ? dispatcher._native : IntPtr.Zero, temp);
		}

        public Dispatcher Dispatcher
		{
			get { return _dispatcher1; }
			set
			{
                btCollisionAlgorithmConstructionInfo_setDispatcher1(_native, (value != null) ? value._native : IntPtr.Zero);
				_dispatcher1 = value;
			}
		}

		public PersistentManifold Manifold
		{
			get { return _manifold; }
			set
			{
                btCollisionAlgorithmConstructionInfo_setManifold(_native, (value != null) ? value._native : IntPtr.Zero);
				_manifold = value;
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
				btCollisionAlgorithmConstructionInfo_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~CollisionAlgorithmConstructionInfo()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCollisionAlgorithmConstructionInfo_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCollisionAlgorithmConstructionInfo_new2(IntPtr dispatcher, int temp);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern IntPtr btCollisionAlgorithmConstructionInfo_getDispatcher1(IntPtr obj);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern IntPtr btCollisionAlgorithmConstructionInfo_getManifold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionAlgorithmConstructionInfo_setDispatcher1(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionAlgorithmConstructionInfo_setManifold(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionAlgorithmConstructionInfo_delete(IntPtr obj);
	}

	public class CollisionAlgorithm : IDisposable
	{
		internal IntPtr _native;
        private readonly bool _preventDelete;

		internal CollisionAlgorithm(IntPtr native, bool preventDelete = false)
		{
			_native = native;
            _preventDelete = preventDelete;
		}

		public float CalculateTimeOfImpact(CollisionObject body0, CollisionObject body1, DispatcherInfo dispatchInfo, ManifoldResult resultOut)
		{
			return btCollisionAlgorithm_calculateTimeOfImpact(_native, body0._native, body1._native, dispatchInfo._native, resultOut._native);
		}

        public void GetAllContactManifolds(AlignedManifoldArray manifoldArray)
		{
			btCollisionAlgorithm_getAllContactManifolds(_native, manifoldArray._native);
		}

		public void ProcessCollision(CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap, DispatcherInfo dispatchInfo, ManifoldResult resultOut)
		{
			btCollisionAlgorithm_processCollision(_native, body0Wrap._native, body1Wrap._native, dispatchInfo._native, resultOut._native);
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
                    btCollisionAlgorithm_delete(_native);
                }
				_native = IntPtr.Zero;
			}
		}

		~CollisionAlgorithm()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btCollisionAlgorithm_calculateTimeOfImpact(IntPtr obj, IntPtr body0, IntPtr body1, IntPtr dispatchInfo, IntPtr resultOut);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionAlgorithm_getAllContactManifolds(IntPtr obj, IntPtr manifoldArray);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionAlgorithm_processCollision(IntPtr obj, IntPtr body0Wrap, IntPtr body1Wrap, IntPtr dispatchInfo, IntPtr resultOut);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionAlgorithm_delete(IntPtr obj);
	}
}
