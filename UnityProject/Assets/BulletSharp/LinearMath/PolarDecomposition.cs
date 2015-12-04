using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class PolarDecomposition : IDisposable
	{
		internal IntPtr _native;

		internal PolarDecomposition(IntPtr native)
		{
			_native = native;
		}

		public PolarDecomposition()
		{
			_native = btPolarDecomposition_new();
		}

		public PolarDecomposition(float tolerance)
		{
			_native = btPolarDecomposition_new2(tolerance);
		}

		public PolarDecomposition(float tolerance, int maxIterations)
		{
			_native = btPolarDecomposition_new3(tolerance, (uint)maxIterations);
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

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btPolarDecomposition_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btPolarDecomposition_new2(float tolerance);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btPolarDecomposition_new3(float tolerance, uint maxIterations);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern uint btPolarDecomposition_decompose(IntPtr obj, [In] ref Matrix a, [Out] out Matrix u, [Out] out Matrix h);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern uint btPolarDecomposition_maxIterations(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPolarDecomposition_delete(IntPtr obj);
	}
}
