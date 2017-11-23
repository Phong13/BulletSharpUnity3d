using BulletSharp.Math;
using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class GImpactPair : IDisposable
	{
		internal IntPtr Native;

		public GImpactPair()
		{
			Native = GIM_PAIR_new();
		}

		public GImpactPair(GImpactPair pair)
		{
			Native = GIM_PAIR_new2(pair.Native);
		}

		public GImpactPair(int index1, int index2)
		{
			Native = GIM_PAIR_new3(index1, index2);
		}

		public int Index1
		{
			get => GIM_PAIR_getIndex1(Native);
			set => GIM_PAIR_setIndex1(Native, value);
		}

		public int Index2
		{
			get => GIM_PAIR_getIndex2(Native);
			set => GIM_PAIR_setIndex2(Native, value);
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
				GIM_PAIR_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~GImpactPair()
		{
			Dispose(false);
		}
	}

	public class PairSet : IDisposable
	{
		internal IntPtr Native;

		public PairSet()
		{
			Native = btPairSet_new();
		}

		public void PushPair(int index1, int index2)
		{
			btPairSet_push_pair(Native, index1, index2);
		}

		public void PushPairInv(int index1, int index2)
		{
			btPairSet_push_pair_inv(Native, index1, index2);
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
				btPairSet_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~PairSet()
		{
			Dispose(false);
		}
	}

	public sealed class GImpactBvhData : IDisposable
	{
		internal IntPtr Native;
		private bool _preventDelete;

		internal GImpactBvhData(IntPtr native)
		{
			Native = native;
			_preventDelete = true;
		}

		public GImpactBvhData()
		{
			Native = GIM_BVH_DATA_new();
		}

		public Aabb Bound
		{
			get => new Aabb(GIM_BVH_DATA_getBound(Native));
			set => GIM_BVH_DATA_setBound(Native, value.Native);
		}

		public int Data
		{
			get => GIM_BVH_DATA_getData(Native);
			set => GIM_BVH_DATA_setData(Native, value);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				if (!_preventDelete)
				{
					GIM_BVH_DATA_delete(Native);
				}
				Native = IntPtr.Zero;
			}
		}

		~GImpactBvhData()
		{
			Dispose(false);
		}
	}

	public class GimBvhTreeNode : IDisposable
	{
		internal IntPtr Native;

		internal GimBvhTreeNode(IntPtr native)
		{
			Native = native;
		}

		public GimBvhTreeNode()
		{
			Native = GIM_BVH_TREE_NODE_new();
		}

		public Aabb Bound
		{
			get => new Aabb(GIM_BVH_TREE_NODE_getBound(Native));
			set => GIM_BVH_TREE_NODE_setBound(Native, value.Native);
		}

		public int DataIndex
		{
			get => GIM_BVH_TREE_NODE_getDataIndex(Native);
			set => GIM_BVH_TREE_NODE_setDataIndex(Native, value);
		}

		public int EscapeIndex
		{
			get => GIM_BVH_TREE_NODE_getEscapeIndex(Native);
			set => GIM_BVH_TREE_NODE_setEscapeIndex(Native, value);
		}

		public bool IsLeafNode => GIM_BVH_TREE_NODE_isLeafNode(Native);

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				GIM_BVH_TREE_NODE_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~GimBvhTreeNode()
		{
			Dispose(false);
		}
	}

	public class GimBvhDataArray
	{
		internal IntPtr Native;

		internal GimBvhDataArray(IntPtr native)
		{
			Native = native;
		}
		/*
		public GimBvhDataArray()
		{
			Native = GIM_BVH_DATA_ARRAY_new();
		}
		*/
	}

	public class GimBvhTreeNodeArray
	{
		internal IntPtr Native;

		internal GimBvhTreeNodeArray(IntPtr native)
		{
			Native = native;
		}
		/*
		public GimBvhTreeNodeArray()
		{
			Native = GIM_BVH_TREE_NODE_ARRAY_new();
		}
		*/
	}

	public class BvhTree : IDisposable
	{
		internal IntPtr Native;

		internal BvhTree(IntPtr native)
		{
			Native = native;
		}

		public BvhTree()
		{
			Native = btBvhTree_new();
		}

		public void BuildTree(GimBvhDataArray primitiveBoxes)
		{
			btBvhTree_build_tree(Native, primitiveBoxes.Native);
		}

		public void ClearNodes()
		{
			btBvhTree_clearNodes(Native);
		}

		public GimBvhTreeNode GetNodePointer()
		{
			return new GimBvhTreeNode(btBvhTree_get_node_pointer(Native));
		}

		public GimBvhTreeNode GetNodePointer(int index)
		{
			return new GimBvhTreeNode(btBvhTree_get_node_pointer2(Native, index));
		}

		public int GetEscapeNodeIndex(int nodeIndex)
		{
			return btBvhTree_getEscapeNodeIndex(Native, nodeIndex);
		}

		public int GetLeftNode(int nodeIndex)
		{
			return btBvhTree_getLeftNode(Native, nodeIndex);
		}

		public void GetNodeBound(int nodeIndex, Aabb bound)
		{
			btBvhTree_getNodeBound(Native, nodeIndex, bound.Native);
		}

		public int GetNodeData(int nodeIndex)
		{
			return btBvhTree_getNodeData(Native, nodeIndex);
		}

		public int GetRightNode(int nodeIndex)
		{
			return btBvhTree_getRightNode(Native, nodeIndex);
		}

		public bool IsLeafNode(int nodeIndex)
		{
			return btBvhTree_isLeafNode(Native, nodeIndex);
		}

		public void SetNodeBound(int nodeIndex, Aabb bound)
		{
			btBvhTree_setNodeBound(Native, nodeIndex, bound.Native);
		}

		public int NodeCount => btBvhTree_getNodeCount(Native);

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				btBvhTree_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~BvhTree()
		{
			Dispose(false);
		}
	}

	public class PrimitiveManagerBase : IDisposable
	{
		internal IntPtr Native;

		internal PrimitiveManagerBase(IntPtr native)
		{
			Native = native;
		}

		public void GetPrimitiveBox(int primitiveIndex, Aabb primitiveBox)
		{
			btPrimitiveManagerBase_get_primitive_box(Native, primitiveIndex, primitiveBox.Native);
		}

		public void GetPrimitiveTriangle(int primitiveIndex, PrimitiveTriangle triangle)
		{
			btPrimitiveManagerBase_get_primitive_triangle(Native, primitiveIndex, triangle.Native);
		}

		public bool IsTrimesh => btPrimitiveManagerBase_is_trimesh(Native);

		public int PrimitiveCount => btPrimitiveManagerBase_get_primitive_count(Native);

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				btPrimitiveManagerBase_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~PrimitiveManagerBase()
		{
			Dispose(false);
		}
	}

	public class GImpactBvh : IDisposable
	{
		internal IntPtr Native;

		private PrimitiveManagerBase _primitiveManager;

		internal GImpactBvh(IntPtr native)
		{
			Native = native;
		}

		public GImpactBvh()
		{
			Native = btGImpactBvh_new();
		}

		public GImpactBvh(PrimitiveManagerBase primitiveManager)
		{
			Native = btGImpactBvh_new2(primitiveManager.Native);
			_primitiveManager = primitiveManager;
		}
		/*
		public bool BoxQuery(Aabb box, AlignedIntArray collidedResults)
		{
			return btGImpactBvh_boxQuery(Native, box.Native, collidedResults.Native);
		}

		public bool BoxQueryTrans(Aabb box, Matrix transform, AlignedIntArray collidedResults)
		{
			return btGImpactBvh_boxQueryTrans(Native, box.Native, ref transform,
				collidedResults.Native);
		}
		*/
		public void BuildSet()
		{
			btGImpactBvh_buildSet(Native);
		}

		public static void FindCollision(GImpactBvh boxSet1, ref Matrix transform1, GImpactBvh boxSet2,
			ref Matrix transform2, PairSet collisionPairs)
		{
			btGImpactBvh_find_collision(boxSet1.Native, ref transform1, boxSet2.Native,
				ref transform2, collisionPairs.Native);
		}

		public GimBvhTreeNode GetNodePointer(int index = 0)
		{
			return new GimBvhTreeNode(btGImpactBvh_get_node_pointer(Native, index));
		}

		public int GetEscapeNodeIndex(int nodeIndex)
		{
			return btGImpactBvh_getEscapeNodeIndex(Native, nodeIndex);
		}

		public int GetLeftNode(int nodeIndex)
		{
			return btGImpactBvh_getLeftNode(Native, nodeIndex);
		}

		public void GetNodeBound(int nodeIndex, Aabb bound)
		{
			btGImpactBvh_getNodeBound(Native, nodeIndex, bound.Native);
		}

		public int GetNodeData(int nodeIndex)
		{
			return btGImpactBvh_getNodeData(Native, nodeIndex);
		}

		public void GetNodeTriangle(int nodeIndex, PrimitiveTriangle triangle)
		{
			btGImpactBvh_getNodeTriangle(Native, nodeIndex, triangle.Native);
		}

		public int GetRightNode(int nodeIndex)
		{
			return btGImpactBvh_getRightNode(Native, nodeIndex);
		}

		public bool IsLeafNode(int nodeIndex)
		{
			return btGImpactBvh_isLeafNode(Native, nodeIndex);
		}
		/*
		public bool RayQuery(Vector3 rayDir, Vector3 rayOrigin, AlignedIntArray collidedResults)
		{
			return btGImpactBvh_rayQuery(Native, ref rayDir, ref rayOrigin, collidedResults.Native);
		}
		*/
		public void SetNodeBound(int nodeIndex, Aabb bound)
		{
			btGImpactBvh_setNodeBound(Native, nodeIndex, bound.Native);
		}

		public void Update()
		{
			btGImpactBvh_update(Native);
		}

		public Aabb GlobalBox => new Aabb(btGImpactBvh_getGlobalBox(Native));

		public bool HasHierarchy => btGImpactBvh_hasHierarchy(Native);

		public bool IsTrimesh => btGImpactBvh_isTrimesh(Native);

		public int NodeCount => btGImpactBvh_getNodeCount(Native);

		public PrimitiveManagerBase PrimitiveManager
		{
			get => new PrimitiveManagerBase(btGImpactBvh_getPrimitiveManager(Native));
			set => btGImpactBvh_setPrimitiveManager(Native, value.Native);
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
				btGImpactBvh_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~GImpactBvh()
		{
			Dispose(false);
		}
	}
}
