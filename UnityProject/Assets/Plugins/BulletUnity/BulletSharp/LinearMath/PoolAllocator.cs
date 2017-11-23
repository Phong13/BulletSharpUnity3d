using System;
using static BulletSharp.UnsafeNativeMethods;

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
			_native = btPoolAllocator_new(elemSize, maxElements);
		}

		public IntPtr Allocate(int size)
		{
			return btPoolAllocator_allocate(_native, size);
		}

		public void FreeMemory(IntPtr ptr)
		{
			btPoolAllocator_freeMemory(_native, ptr);
		}

		public bool ValidPtr(IntPtr ptr)
		{
			return btPoolAllocator_validPtr(_native, ptr);
		}

		public int ElementSize => btPoolAllocator_getElementSize(_native);

		public int FreeCount => btPoolAllocator_getFreeCount(_native);

		public int MaxCount => btPoolAllocator_getMaxCount(_native);

		public IntPtr PoolAddress => btPoolAllocator_getPoolAddress(_native);

		public int UsedCount => btPoolAllocator_getUsedCount(_native);

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
					btPoolAllocator_delete(_native);
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
