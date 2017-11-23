using BulletSharp.Math;
using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class GImpactQuantizedBvhNode : IDisposable
	{
		internal IntPtr Native;

		internal GImpactQuantizedBvhNode(IntPtr native)
		{
			Native = native;
		}

		public GImpactQuantizedBvhNode()
		{
			Native = BT_QUANTIZED_BVH_NODE_new();
		}

		public bool TestQuantizedBoxOverlapp(ushort[] quantizedMin, ushort[] quantizedMax)
		{
			return BT_QUANTIZED_BVH_NODE_testQuantizedBoxOverlapp(Native, quantizedMin, quantizedMax);
		}

		public int DataIndex
		{
			get => BT_QUANTIZED_BVH_NODE_getDataIndex(Native);
			set => BT_QUANTIZED_BVH_NODE_setDataIndex(Native, value);
		}

		public int EscapeIndex
		{
			get => BT_QUANTIZED_BVH_NODE_getEscapeIndex(Native);
			set => BT_QUANTIZED_BVH_NODE_setEscapeIndex(Native, value);
		}

		public int EscapeIndexOrDataIndex
		{
			get => BT_QUANTIZED_BVH_NODE_getEscapeIndexOrDataIndex(Native);
			set => BT_QUANTIZED_BVH_NODE_setEscapeIndexOrDataIndex(Native, value);
		}

		public bool IsLeafNode => BT_QUANTIZED_BVH_NODE_isLeafNode(Native);
		/*
		public UShortArray QuantizedAabbMax
		{
			get => BT_QUANTIZED_BVH_NODE_getQuantizedAabbMax(Native);
		}

		public UShortArray QuantizedAabbMin
		{
			get => BT_QUANTIZED_BVH_NODE_getQuantizedAabbMin(Native);
		}
		*/
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				BT_QUANTIZED_BVH_NODE_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~GImpactQuantizedBvhNode()
		{
			Dispose(false);
		}
	}

	public class GImpactQuantizedBvhNodeArray
	{
		internal IntPtr Native;

		internal GImpactQuantizedBvhNodeArray(IntPtr native)
		{
			Native = native;
		}
		/*
		public GimGImpactQuantizedBvhNodeArray()
		{
			Native = GIM_QUANTIZED_BVH_NODE_ARRAY_new();
		}
		*/
	}

	public class QuantizedBvhTree : IDisposable
	{
		internal IntPtr Native;

		internal QuantizedBvhTree(IntPtr native)
		{
			Native = native;
		}

		public QuantizedBvhTree()
		{
			Native = btQuantizedBvhTree_new();
		}

		public void BuildTree(GimBvhDataArray primitiveBoxes)
		{
			btQuantizedBvhTree_build_tree(Native, primitiveBoxes.Native);
		}

		public void ClearNodes()
		{
			btQuantizedBvhTree_clearNodes(Native);
		}
		/*
		public GImpactQuantizedBvhNode GetNodePointer(int index = 0)
		{
			return btQuantizedBvhTree_get_node_pointer(Native, index);
		}
		*/
		public int GetEscapeNodeIndex(int nodeIndex)
		{
			return btQuantizedBvhTree_getEscapeNodeIndex(Native, nodeIndex);
		}

		public int GetLeftNode(int nodeIndex)
		{
			return btQuantizedBvhTree_getLeftNode(Native, nodeIndex);
		}

		public void GetNodeBound(int nodeIndex, Aabb bound)
		{
			btQuantizedBvhTree_getNodeBound(Native, nodeIndex, bound.Native);
		}

		public int GetNodeData(int nodeIndex)
		{
			return btQuantizedBvhTree_getNodeData(Native, nodeIndex);
		}

		public int GetRightNode(int nodeIndex)
		{
			return btQuantizedBvhTree_getRightNode(Native, nodeIndex);
		}

		public bool IsLeafNode(int nodeIndex)
		{
			return btQuantizedBvhTree_isLeafNode(Native, nodeIndex);
		}

		public void QuantizePoint(ushort[] quantizedpoint, Vector3 point)
		{
			btQuantizedBvhTree_quantizePoint(Native, quantizedpoint, ref point);
		}

		public void SetNodeBound(int nodeIndex, Aabb bound)
		{
			btQuantizedBvhTree_setNodeBound(Native, nodeIndex, bound.Native);
		}

		public bool TestQuantizedBoxOverlap(int nodeIndex, ushort[] quantizedMin, ushort[] quantizedMax)
		{
			return btQuantizedBvhTree_testQuantizedBoxOverlapp(Native, nodeIndex, quantizedMin, quantizedMax);
		}

		public int NodeCount => btQuantizedBvhTree_getNodeCount(Native);

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				btQuantizedBvhTree_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~QuantizedBvhTree()
		{
			Dispose(false);
		}
	}

	public class GImpactQuantizedBvh : IDisposable
	{
		internal IntPtr Native;

		private PrimitiveManagerBase _primitiveManager;

		internal GImpactQuantizedBvh(IntPtr native)
		{
			Native = native;
		}

		public GImpactQuantizedBvh()
		{
			Native = btGImpactQuantizedBvh_new();
		}

		public GImpactQuantizedBvh(PrimitiveManagerBase primitiveManager)
		{
			Native = btGImpactQuantizedBvh_new2(primitiveManager.Native);
			_primitiveManager = primitiveManager;
		}
		/*
		public bool BoxQuery(Aabb box, AlignedIntArray collidedResults)
		{
			return btGImpactQuantizedBvh_boxQuery(Native, box.Native, collidedResults.Native);
		}

		public bool BoxQueryTrans(Aabb box, Matrix transform, AlignedIntArray collidedResults)
		{
			return btGImpactQuantizedBvh_boxQueryTrans(Native, box.Native, ref transform,
				collidedResults.Native);
		}
		*/
		public void BuildSet()
		{
			btGImpactQuantizedBvh_buildSet(Native);
		}

		public static void FindCollision(GImpactQuantizedBvh boxset1, Matrix trans1,
			GImpactQuantizedBvh boxset2, Matrix trans2, PairSet collisionPairs)
		{
			btGImpactQuantizedBvh_find_collision(boxset1.Native, ref trans1, boxset2.Native,
				ref trans2, collisionPairs.Native);
		}
		/*
		public GImpactQuantizedBvhNode GetNodePointer(int index = 0)
		{
			return btGImpactQuantizedBvh_get_node_pointer(Native, index);
		}
		*/
		public int GetEscapeNodeIndex(int nodeIndex)
		{
			return btGImpactQuantizedBvh_getEscapeNodeIndex(Native, nodeIndex);
		}

		public int GetLeftNode(int nodeIndex)
		{
			return btGImpactQuantizedBvh_getLeftNode(Native, nodeIndex);
		}

		public void GetNodeBound(int nodeIndex, Aabb bound)
		{
			btGImpactQuantizedBvh_getNodeBound(Native, nodeIndex, bound.Native);
		}

		public int GetNodeData(int nodeIndex)
		{
			return btGImpactQuantizedBvh_getNodeData(Native, nodeIndex);
		}

		public void GetNodeTriangle(int nodeIndex, PrimitiveTriangle triangle)
		{
			btGImpactQuantizedBvh_getNodeTriangle(Native, nodeIndex, triangle.Native);
		}

		public int GetRightNode(int nodeIndex)
		{
			return btGImpactQuantizedBvh_getRightNode(Native, nodeIndex);
		}

		public bool IsLeafNode(int nodeIndex)
		{
			return btGImpactQuantizedBvh_isLeafNode(Native, nodeIndex);
		}
		/*
		public bool RayQuery(Vector3 rayDir, Vector3 rayOrigin, AlignedIntArray collidedResults)
		{
			return btGImpactQuantizedBvh_rayQuery(Native, ref rayDir, ref rayOrigin,
				collidedResults.Native);
		}
		*/
		public void SetNodeBound(int nodeIndex, Aabb bound)
		{
			btGImpactQuantizedBvh_setNodeBound(Native, nodeIndex, bound.Native);
		}

		public void Update()
		{
			btGImpactQuantizedBvh_update(Native);
		}

		public Aabb GlobalBox => new Aabb(btGImpactQuantizedBvh_getGlobalBox(Native));

		public bool HasHierarchy => btGImpactQuantizedBvh_hasHierarchy(Native);

		public bool IsTrimesh => btGImpactQuantizedBvh_isTrimesh(Native);

		public int NodeCount => btGImpactQuantizedBvh_getNodeCount(Native);

		public PrimitiveManagerBase PrimitiveManager
		{
			get => _primitiveManager;
			set
			{
				btGImpactQuantizedBvh_setPrimitiveManager(Native, value.Native);
				_primitiveManager = value;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				btGImpactQuantizedBvh_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~GImpactQuantizedBvh()
		{
			Dispose(false);
		}
	}
}
