using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class OptimizedBvh : QuantizedBvh
	{
		internal OptimizedBvh(IntPtr native, bool preventDelete)
            : base(native, preventDelete)
		{
		}

		public OptimizedBvh()
			: base(btOptimizedBvh_new(), false)
		{
		}

		public void Build(StridingMeshInterface triangles, bool useQuantizedAabbCompression, Vector3 bvhAabbMin, Vector3 bvhAabbMax)
		{
			btOptimizedBvh_build(_native, triangles._native, useQuantizedAabbCompression, ref bvhAabbMin, ref bvhAabbMax);
		}

		public static OptimizedBvh DeSerializeInPlace(IntPtr alignedDataBuffer, uint dataBufferSize, bool swapEndian)
		{
            return new OptimizedBvh(btOptimizedBvh_deSerializeInPlace(alignedDataBuffer, dataBufferSize, swapEndian), true);
		}

		public void Refit(StridingMeshInterface triangles, Vector3 aabbMin, Vector3 aabbMax)
		{
			btOptimizedBvh_refit(_native, triangles._native, ref aabbMin, ref aabbMax);
		}

		public void RefitPartial(StridingMeshInterface triangles, Vector3 aabbMin, Vector3 aabbMax)
		{
			btOptimizedBvh_refitPartial(_native, triangles._native, ref aabbMin, ref aabbMax);
		}

		public bool SerializeInPlace(IntPtr alignedDataBuffer, uint dataBufferSize, bool swapEndian)
		{
			return btOptimizedBvh_serializeInPlace(_native, alignedDataBuffer, dataBufferSize, swapEndian);
		}

		public void UpdateBvhNodes(StridingMeshInterface meshInterface, int firstNode, int endNode, int index)
		{
			btOptimizedBvh_updateBvhNodes(_native, meshInterface._native, firstNode, endNode, index);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btOptimizedBvh_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btOptimizedBvh_build(IntPtr obj, IntPtr triangles, bool useQuantizedAabbCompression, [In] ref Vector3 bvhAabbMin, [In] ref Vector3 bvhAabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btOptimizedBvh_deSerializeInPlace(IntPtr i_alignedDataBuffer, uint i_dataBufferSize, bool i_swapEndian);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btOptimizedBvh_refit(IntPtr obj, IntPtr triangles, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btOptimizedBvh_refitPartial(IntPtr obj, IntPtr triangles, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btOptimizedBvh_serializeInPlace(IntPtr obj, IntPtr o_alignedDataBuffer, uint i_dataBufferSize, bool i_swapEndian);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btOptimizedBvh_updateBvhNodes(IntPtr obj, IntPtr meshInterface, int firstNode, int endNode, int index);
	}
}
