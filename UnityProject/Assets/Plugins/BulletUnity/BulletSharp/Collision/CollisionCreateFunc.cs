using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class CollisionAlgorithmCreateFunc : IDisposable
	{
		internal IntPtr Native;
		private bool _preventDelete;

		internal CollisionAlgorithmCreateFunc(IntPtr native, bool preventDelete)
		{
			Native = native;
			_preventDelete = preventDelete;
		}

		public CollisionAlgorithmCreateFunc()
		{
			Native = btCollisionAlgorithmCreateFunc_new();
		}

		public virtual CollisionAlgorithm CreateCollisionAlgorithm(CollisionAlgorithmConstructionInfo __unnamed0,
			CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap)
		{
			return null;
		}

		public bool Swapped
		{
			get => btCollisionAlgorithmCreateFunc_getSwapped(Native);
			set => btCollisionAlgorithmCreateFunc_setSwapped(Native, value);
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
					btCollisionAlgorithmCreateFunc_delete(Native);
				}
				Native = IntPtr.Zero;
			}
		}

		~CollisionAlgorithmCreateFunc()
		{
			Dispose(false);
		}
	}
}
