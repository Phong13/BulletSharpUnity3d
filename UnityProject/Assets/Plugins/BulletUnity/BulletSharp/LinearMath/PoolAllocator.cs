using System;


namespace BulletSharp
{
	public class PoolAllocator : IDisposable
	{
		internal IntPtr _native;
		private bool _preventDelete;

		internal PoolAllocator(IntPtr native)
		{
			_native = native;
			_preventDelete = true;
		}

		public PoolAllocator(int elemSize, int maxElements)
		{
			_native = UnsafeNativeMethods.btPoolAllocator_new(elemSize, maxElements);
		}

		public IntPtr Allocate(int size)
		{
			return UnsafeNativeMethods.btPoolAllocator_allocate(_native, size);
		}

		public void FreeMemory(IntPtr ptr)
		{
			UnsafeNativeMethods.btPoolAllocator_freeMemory(_native, ptr);
		}

		public bool ValidPtr(IntPtr ptr)
		{
			return UnsafeNativeMethods.btPoolAllocator_validPtr(_native, ptr);
		}

		public int ElementSize{ get { return  UnsafeNativeMethods.btPoolAllocator_getElementSize(_native);} }

		public int FreeCount{ get { return  UnsafeNativeMethods.btPoolAllocator_getFreeCount(_native);} }

		public int MaxCount{ get { return  UnsafeNativeMethods.btPoolAllocator_getMaxCount(_native);} }

		public IntPtr PoolAddress{ get { return  UnsafeNativeMethods.btPoolAllocator_getPoolAddress(_native);} }

		public int UsedCount{ get { return  UnsafeNativeMethods.btPoolAllocator_getUsedCount(_native);} }

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
					UnsafeNativeMethods.btPoolAllocator_delete(_native);
				}
				_native = IntPtr.Zero;
			}
		}

		~PoolAllocator()
		{
			Dispose(false);
		}
	}
}
