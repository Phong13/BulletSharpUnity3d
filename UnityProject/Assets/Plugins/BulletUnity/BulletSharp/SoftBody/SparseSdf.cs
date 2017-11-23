using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp.SoftBody
{
	public class SparseSdf
	{
		internal IntPtr _native;

		internal SparseSdf(IntPtr native)
		{
			_native = native;
		}

		public void GarbageCollect(int lifetime = 256)
		{
			btSparseSdf3_GarbageCollect(_native, lifetime);
		}

		public void Initialize(int hashSize = 2383, int clampCells = 256 * 1024)
		{
			btSparseSdf3_Initialize(_native, hashSize, clampCells);
		}

		public int RemoveReferences(CollisionShape pcs)
		{
			return btSparseSdf3_RemoveReferences(_native, (pcs != null) ? pcs.Native : IntPtr.Zero);
		}

		public void Reset()
		{
			btSparseSdf3_Reset(_native);
		}
	}
}
