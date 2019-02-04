using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public abstract class OverlappingPairCallback : IDisposable
	{
		internal IntPtr _native;
        private readonly bool _preventDelete;

		internal OverlappingPairCallback(IntPtr native, bool preventDelete = false)
		{
			_native = native;
            _preventDelete = preventDelete;
		}
        /*
        protected OverlappingPairCallback()
        {
            _native = btOverlappingPairCallbackWrapper_new();
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
			if (_native != IntPtr.Zero)
			{
                if (!_preventDelete)
                {
                    btOverlappingPairCallback_delete(_native);
                }
				_native = IntPtr.Zero;
			}
		}

		~OverlappingPairCallback()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		internal static extern IntPtr btOverlappingPairCallback_addOverlappingPair(IntPtr obj, IntPtr proxy0, IntPtr proxy1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		internal static extern IntPtr btOverlappingPairCallback_removeOverlappingPair(IntPtr obj, IntPtr proxy0, IntPtr proxy1, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		internal static extern void btOverlappingPairCallback_removeOverlappingPairsContainingProxy(IntPtr obj, IntPtr proxy0, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btOverlappingPairCallback_delete(IntPtr obj);
	}
}
