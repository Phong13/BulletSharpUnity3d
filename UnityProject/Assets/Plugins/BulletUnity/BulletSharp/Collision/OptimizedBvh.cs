using System;
using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

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

		public void Build(StridingMeshInterface triangles, bool useQuantizedAabbCompression,
			Vector3 bvhAabbMin, Vector3 bvhAabbMax)
		{
			btOptimizedBvh_build(_native, triangles.Native, useQuantizedAabbCompression,
				ref bvhAabbMin, ref bvhAabbMax);
		}

		public static OptimizedBvh DeSerializeInPlace(IntPtr alignedDataBuffer, uint dataBufferSize,
			bool swapEndian)
		{
            return new OptimizedBvh(btOptimizedBvh_deSerializeInPlace(alignedDataBuffer, dataBufferSize,
                swapEndian), true);
		}

		public void Refit(StridingMeshInterface triangles, Vector3 aabbMin, Vector3 aabbMax)
		{
			btOptimizedBvh_refit(_native, triangles.Native, ref aabbMin, ref aabbMax);
		}

		public void RefitPartial(StridingMeshInterface triangles, Vector3 aabbMin,
			Vector3 aabbMax)
		{
			btOptimizedBvh_refitPartial(_native, triangles.Native, ref aabbMin,
				ref aabbMax);
		}

		public bool SerializeInPlace(IntPtr alignedDataBuffer, uint dataBufferSize,
			bool swapEndian)
		{
			return btOptimizedBvh_serializeInPlace(_native, alignedDataBuffer, dataBufferSize,
				swapEndian);
		}

		public void UpdateBvhNodes(StridingMeshInterface meshInterface, int firstNode,
			int endNode, int index)
		{
			btOptimizedBvh_updateBvhNodes(_native, meshInterface.Native, firstNode,
				endNode, index);
		}
	}
}
