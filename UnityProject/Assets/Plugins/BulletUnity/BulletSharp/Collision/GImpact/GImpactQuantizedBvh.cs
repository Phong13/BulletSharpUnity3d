using BulletSharp.Math;
using System;


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
			Native = UnsafeNativeMethods.BT_QUANTIZED_BVH_NODE_new();
		}

		public bool TestQuantizedBoxOverlapp(ushort[] quantizedMin, ushort[] quantizedMax)
		{
			return UnsafeNativeMethods.BT_QUANTIZED_BVH_NODE_testQuantizedBoxOverlapp(Native, quantizedMin, quantizedMax);
		}

		public int DataIndex
		{
			get { return  UnsafeNativeMethods.BT_QUANTIZED_BVH_NODE_getDataIndex(Native);}
			set {  UnsafeNativeMethods.BT_QUANTIZED_BVH_NODE_setDataIndex(Native, value);}
		}

		public int EscapeIndex
		{
			get { return  UnsafeNativeMethods.BT_QUANTIZED_BVH_NODE_getEscapeIndex(Native);}
			set {  UnsafeNativeMethods.BT_QUANTIZED_BVH_NODE_setEscapeIndex(Native, value);}
		}

		public int EscapeIndexOrDataIndex
		{
			get { return  UnsafeNativeMethods.BT_QUANTIZED_BVH_NODE_getEscapeIndexOrDataIndex(Native);}
			set {  UnsafeNativeMethods.BT_QUANTIZED_BVH_NODE_setEscapeIndexOrDataIndex(Native, value);}
		}

		public bool IsLeafNode{ get { return  UnsafeNativeMethods.BT_QUANTIZED_BVH_NODE_isLeafNode(Native);} }
		/*
		public UShortArray QuantizedAabbMax
		{
			get { return  UnsafeNativeMethods.BT_QUANTIZED_BVH_NODE_getQuantizedAabbMax(Native);}
		}

		public UShortArray QuantizedAabbMin
		{
			get { return  UnsafeNativeMethods.BT_QUANTIZED_BVH_NODE_getQuantizedAabbMin(Native);}
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
				UnsafeNativeMethods.BT_QUANTIZED_BVH_NODE_delete(Native);
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
			Native = UnsafeNativeMethods.GIM_QUANTIZED_BVH_NODE_ARRAY_new();
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
			Native = UnsafeNativeMethods.btQuantizedBvhTree_new();
		}

		public void BuildTree(GimBvhDataArray primitiveBoxes)
		{
			UnsafeNativeMethods.btQuantizedBvhTree_build_tree(Native, primitiveBoxes.Native);
		}

		public void ClearNodes()
		{
			UnsafeNativeMethods.btQuantizedBvhTree_clearNodes(Native);
		}
		/*
		public GImpactQuantizedBvhNode GetNodePointer(int index = 0)
		{
			return UnsafeNativeMethods.btQuantizedBvhTree_get_node_pointer(Native, index);
		}
		*/
		public int GetEscapeNodeIndex(int nodeIndex)
		{
			return UnsafeNativeMethods.btQuantizedBvhTree_getEscapeNodeIndex(Native, nodeIndex);
		}

		public int GetLeftNode(int nodeIndex)
		{
			return UnsafeNativeMethods.btQuantizedBvhTree_getLeftNode(Native, nodeIndex);
		}

		public void GetNodeBound(int nodeIndex, Aabb bound)
		{
			UnsafeNativeMethods.btQuantizedBvhTree_getNodeBound(Native, nodeIndex, bound.Native);
		}

		public int GetNodeData(int nodeIndex)
		{
			return UnsafeNativeMethods.btQuantizedBvhTree_getNodeData(Native, nodeIndex);
		}

		public int GetRightNode(int nodeIndex)
		{
			return UnsafeNativeMethods.btQuantizedBvhTree_getRightNode(Native, nodeIndex);
		}

		public bool IsLeafNode(int nodeIndex)
		{
			return UnsafeNativeMethods.btQuantizedBvhTree_isLeafNode(Native, nodeIndex);
		}

		public void QuantizePoint(ushort[] quantizedpoint, Vector3 point)
		{
			UnsafeNativeMethods.btQuantizedBvhTree_quantizePoint(Native, quantizedpoint, ref point);
		}

		public void SetNodeBound(int nodeIndex, Aabb bound)
		{
			UnsafeNativeMethods.btQuantizedBvhTree_setNodeBound(Native, nodeIndex, bound.Native);
		}

		public bool TestQuantizedBoxOverlap(int nodeIndex, ushort[] quantizedMin, ushort[] quantizedMax)
		{
			return UnsafeNativeMethods.btQuantizedBvhTree_testQuantizedBoxOverlapp(Native, nodeIndex, quantizedMin, quantizedMax);
		}

		public int NodeCount{ get { return  UnsafeNativeMethods.btQuantizedBvhTree_getNodeCount(Native);} }

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				UnsafeNativeMethods.btQuantizedBvhTree_delete(Native);
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
			Native = UnsafeNativeMethods.btGImpactQuantizedBvh_new();
		}

		public GImpactQuantizedBvh(PrimitiveManagerBase primitiveManager)
		{
			Native = UnsafeNativeMethods.btGImpactQuantizedBvh_new2(primitiveManager.Native);
			_primitiveManager = primitiveManager;
		}
		/*
		public bool BoxQuery(Aabb box, AlignedIntArray collidedResults)
		{
			return UnsafeNativeMethods.btGImpactQuantizedBvh_boxQuery(Native, box.Native, collidedResults.Native);
		}

		public bool BoxQueryTrans(Aabb box, Matrix transform, AlignedIntArray collidedResults)
		{
			return UnsafeNativeMethods.btGImpactQuantizedBvh_boxQueryTrans(Native, box.Native, ref transform,
				collidedResults.Native);
		}
		*/
		public void BuildSet()
		{
			UnsafeNativeMethods.btGImpactQuantizedBvh_buildSet(Native);
		}

		public static void FindCollision(GImpactQuantizedBvh boxset1, Matrix trans1,
			GImpactQuantizedBvh boxset2, Matrix trans2, PairSet collisionPairs)
		{
			UnsafeNativeMethods.btGImpactQuantizedBvh_find_collision(boxset1.Native, ref trans1, boxset2.Native,
				ref trans2, collisionPairs.Native);
		}
		/*
		public GImpactQuantizedBvhNode GetNodePointer(int index = 0)
		{
			return UnsafeNativeMethods.btGImpactQuantizedBvh_get_node_pointer(Native, index);
		}
		*/
		public int GetEscapeNodeIndex(int nodeIndex)
		{
			return UnsafeNativeMethods.btGImpactQuantizedBvh_getEscapeNodeIndex(Native, nodeIndex);
		}

		public int GetLeftNode(int nodeIndex)
		{
			return UnsafeNativeMethods.btGImpactQuantizedBvh_getLeftNode(Native, nodeIndex);
		}

		public void GetNodeBound(int nodeIndex, Aabb bound)
		{
			UnsafeNativeMethods.btGImpactQuantizedBvh_getNodeBound(Native, nodeIndex, bound.Native);
		}

		public int GetNodeData(int nodeIndex)
		{
			return UnsafeNativeMethods.btGImpactQuantizedBvh_getNodeData(Native, nodeIndex);
		}

		public void GetNodeTriangle(int nodeIndex, PrimitiveTriangle triangle)
		{
			UnsafeNativeMethods.btGImpactQuantizedBvh_getNodeTriangle(Native, nodeIndex, triangle.Native);
		}

		public int GetRightNode(int nodeIndex)
		{
			return UnsafeNativeMethods.btGImpactQuantizedBvh_getRightNode(Native, nodeIndex);
		}

		public bool IsLeafNode(int nodeIndex)
		{
			return UnsafeNativeMethods.btGImpactQuantizedBvh_isLeafNode(Native, nodeIndex);
		}
		/*
		public bool RayQuery(Vector3 rayDir, Vector3 rayOrigin, AlignedIntArray collidedResults)
		{
			return UnsafeNativeMethods.btGImpactQuantizedBvh_rayQuery(Native, ref rayDir, ref rayOrigin,
				collidedResults.Native);
		}
		*/
		public void SetNodeBound(int nodeIndex, Aabb bound)
		{
			UnsafeNativeMethods.btGImpactQuantizedBvh_setNodeBound(Native, nodeIndex, bound.Native);
		}

		public void Update()
		{
			UnsafeNativeMethods.btGImpactQuantizedBvh_update(Native);
		}

		public Aabb GlobalBox{ get { return  new Aabb(UnsafeNativeMethods.btGImpactQuantizedBvh_getGlobalBox(Native));} }

		public bool HasHierarchy{ get { return  UnsafeNativeMethods.btGImpactQuantizedBvh_hasHierarchy(Native);} }

		public bool IsTrimesh{ get { return  UnsafeNativeMethods.btGImpactQuantizedBvh_isTrimesh(Native);} }

		public int NodeCount{ get { return  UnsafeNativeMethods.btGImpactQuantizedBvh_getNodeCount(Native);} }

		public PrimitiveManagerBase PrimitiveManager
		{
			get { return  _primitiveManager;}
			set
			{
				UnsafeNativeMethods.btGImpactQuantizedBvh_setPrimitiveManager(Native, value.Native);
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
				UnsafeNativeMethods.btGImpactQuantizedBvh_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~GImpactQuantizedBvh()
		{
			Dispose(false);
		}
	}
}
