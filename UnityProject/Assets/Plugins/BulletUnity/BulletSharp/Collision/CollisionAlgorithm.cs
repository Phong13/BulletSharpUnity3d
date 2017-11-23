using System;

namespace BulletSharp
{
	public class CollisionAlgorithmConstructionInfo : IDisposable
	{
		internal IntPtr Native;

		private Dispatcher _dispatcher1;
		private PersistentManifold _manifold;

		public CollisionAlgorithmConstructionInfo()
		{
			Native = UnsafeNativeMethods.btCollisionAlgorithmConstructionInfo_new();
		}

		public CollisionAlgorithmConstructionInfo(Dispatcher dispatcher, int temp)
		{
			Native = UnsafeNativeMethods.btCollisionAlgorithmConstructionInfo_new2((dispatcher != null) ? dispatcher.Native : IntPtr.Zero,
				temp);
			_dispatcher1 = dispatcher;
		}

		public Dispatcher Dispatcher
		{
			get { return _dispatcher1; }
			set
			{
                UnsafeNativeMethods.btCollisionAlgorithmConstructionInfo_setDispatcher1(Native, (value != null) ? value.Native : IntPtr.Zero);
				_dispatcher1 = value;
			}
		}

		public PersistentManifold Manifold
		{
			get { return _manifold; }
			set
			{
                UnsafeNativeMethods.btCollisionAlgorithmConstructionInfo_setManifold(Native, (value != null) ? value.Native : IntPtr.Zero);
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
			if (Native != IntPtr.Zero)
			{
                UnsafeNativeMethods.btCollisionAlgorithmConstructionInfo_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~CollisionAlgorithmConstructionInfo()
		{
			Dispose(false);
		}
	}

	public class CollisionAlgorithm : IDisposable
	{
		internal IntPtr Native;
		private readonly bool _preventDelete;

		internal CollisionAlgorithm(IntPtr native, bool preventDelete = false)
		{
			Native = native;
			_preventDelete = preventDelete;
		}

		public float CalculateTimeOfImpact(CollisionObject body0, CollisionObject body1,
			DispatcherInfo dispatchInfo, ManifoldResult resultOut)
		{
			return UnsafeNativeMethods.btCollisionAlgorithm_calculateTimeOfImpact(Native, body0.Native,
				body1.Native, dispatchInfo.Native, resultOut.Native);
		}

		public void GetAllContactManifolds(AlignedManifoldArray manifoldArray)
		{
            UnsafeNativeMethods.btCollisionAlgorithm_getAllContactManifolds(Native, manifoldArray._native);
		}

		public void ProcessCollision(CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap,
			DispatcherInfo dispatchInfo, ManifoldResult resultOut)
		{
            UnsafeNativeMethods.btCollisionAlgorithm_processCollision(Native, body0Wrap.Native, body1Wrap.Native,
				dispatchInfo.Native, resultOut.Native);
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
                    UnsafeNativeMethods.btCollisionAlgorithm_delete(Native);
				}
				Native = IntPtr.Zero;
			}
		}

		~CollisionAlgorithm()
		{
			Dispose(false);
		}
	}
}
