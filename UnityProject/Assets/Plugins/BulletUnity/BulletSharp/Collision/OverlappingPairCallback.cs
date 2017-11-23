using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public abstract class OverlappingPairCallback : IDisposable
	{
		internal IntPtr Native;
		private readonly bool _preventDelete;

		internal OverlappingPairCallback(IntPtr native, bool preventDelete = false)
		{
			Native = native;
			_preventDelete = preventDelete;
		}
		/*
		protected OverlappingPairCallback()
		{
			Native = btOverlappingPairCallbackWrapper_new();
		}
		*/
		public abstract BroadphasePair AddOverlappingPair(BroadphaseProxy proxy0, BroadphaseProxy proxy1);
		public abstract IntPtr RemoveOverlappingPair(BroadphaseProxy proxy0, BroadphaseProxy proxy1, Dispatcher dispatcher);
		public abstract void RemoveOverlappingPairsContainingProxy(BroadphaseProxy proxy0, Dispatcher dispatcher);

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
					btOverlappingPairCallback_delete(Native);
				}
				Native = IntPtr.Zero;
			}
		}

		~OverlappingPairCallback()
		{
			Dispose(false);
		}
	}
}
