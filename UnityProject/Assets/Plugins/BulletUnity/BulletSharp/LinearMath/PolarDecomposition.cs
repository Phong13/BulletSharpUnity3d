using System;
using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class PolarDecomposition : IDisposable
	{
		internal IntPtr _native;

		public PolarDecomposition(float tolerance = 0.0001f, int maxIterations = 16)
		{
			_native = btPolarDecomposition_new(tolerance, (uint)maxIterations);
		}

		public uint Decompose(ref Matrix a, out Matrix u, out Matrix h)
		{
			return btPolarDecomposition_decompose(_native, ref a, out u, out h);
		}

		public uint MaxIterations()
		{
			return btPolarDecomposition_maxIterations(_native);
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
				btPolarDecomposition_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~PolarDecomposition()
		{
			Dispose(false);
		}
	}
}
