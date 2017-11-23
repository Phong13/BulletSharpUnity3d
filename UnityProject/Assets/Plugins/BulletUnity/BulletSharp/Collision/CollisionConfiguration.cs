using System;

namespace BulletSharp
{
	public abstract class CollisionConfiguration : IDisposable
	{
		internal IntPtr Native;

		internal CollisionConfiguration(IntPtr native)
		{
			Native = native;
		}

		public abstract CollisionAlgorithmCreateFunc GetCollisionAlgorithmCreateFunc(BroadphaseNativeType proxyType0,
			BroadphaseNativeType proxyType1);

		public abstract CollisionAlgorithmCreateFunc GetClosestPointsAlgorithmCreateFunc(BroadphaseNativeType proxyType0,
			BroadphaseNativeType proxyType1);

		public abstract PoolAllocator CollisionAlgorithmPool { get; }

		public abstract PoolAllocator PersistentManifoldPool { get; }

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
                UnsafeNativeMethods.btCollisionConfiguration_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~CollisionConfiguration()
		{
			Dispose(false);
		}
	}
}
