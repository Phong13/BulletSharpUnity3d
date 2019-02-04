using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp.SoftBody
{
	public class SparseSdf
	{
		internal IntPtr _native;

		internal SparseSdf(IntPtr native)
		{
			_native = native;
		}

        public void GarbageCollect(int lifetime)
        {
            btSparseSdf3_GarbageCollect(_native, lifetime);
        }

        public void GarbageCollect()
        {
            btSparseSdf3_GarbageCollect2(_native);
        }

        public void Initialize(int hashSize, int clampCells)
        {
            btSparseSdf3_Initialize(_native, hashSize, clampCells);
        }

        public void Initialize(int hashSize)
        {
            btSparseSdf3_Initialize2(_native, hashSize);
        }

        public void Initialize()
        {
            btSparseSdf3_Initialize3(_native);
        }

        public int RemoveReferences(CollisionShape pcs)
        {
            return btSparseSdf3_RemoveReferences(_native, (pcs != null) ? pcs._native : IntPtr.Zero);
        }

        public void Reset()
        {
            btSparseSdf3_Reset(_native);
        }

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSparseSdf_new();
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btSparseSdf3_GarbageCollect(IntPtr obj, int lifetime);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btSparseSdf3_GarbageCollect2(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btSparseSdf3_Initialize(IntPtr obj, int hashsize, int clampCells);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btSparseSdf3_Initialize2(IntPtr obj, int hashsize);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btSparseSdf3_Initialize3(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btSparseSdf3_RemoveReferences(IntPtr obj, IntPtr pcs);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btSparseSdf3_Reset(IntPtr obj);
	}
}
