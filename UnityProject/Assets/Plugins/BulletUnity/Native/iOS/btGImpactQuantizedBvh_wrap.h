#include "main.h"

extern "C"
{
	EXPORT BT_QUANTIZED_BVH_NODE* BT_QUANTIZED_BVH_NODE_new();
	EXPORT int BT_QUANTIZED_BVH_NODE_getDataIndex(BT_QUANTIZED_BVH_NODE* obj);
	EXPORT int BT_QUANTIZED_BVH_NODE_getEscapeIndex(BT_QUANTIZED_BVH_NODE* obj);
	EXPORT int BT_QUANTIZED_BVH_NODE_getEscapeIndexOrDataIndex(BT_QUANTIZED_BVH_NODE* obj);
	EXPORT unsigned short* BT_QUANTIZED_BVH_NODE_getQuantizedAabbMax(BT_QUANTIZED_BVH_NODE* obj);
	EXPORT unsigned short* BT_QUANTIZED_BVH_NODE_getQuantizedAabbMin(BT_QUANTIZED_BVH_NODE* obj);
	EXPORT bool BT_QUANTIZED_BVH_NODE_isLeafNode(BT_QUANTIZED_BVH_NODE* obj);
	EXPORT void BT_QUANTIZED_BVH_NODE_setDataIndex(BT_QUANTIZED_BVH_NODE* obj, int index);
	EXPORT void BT_QUANTIZED_BVH_NODE_setEscapeIndex(BT_QUANTIZED_BVH_NODE* obj, int index);
	EXPORT void BT_QUANTIZED_BVH_NODE_setEscapeIndexOrDataIndex(BT_QUANTIZED_BVH_NODE* obj, int value);
	EXPORT bool BT_QUANTIZED_BVH_NODE_testQuantizedBoxOverlapp(BT_QUANTIZED_BVH_NODE* obj, unsigned short* quantizedMin, unsigned short* quantizedMax);
	EXPORT void BT_QUANTIZED_BVH_NODE_delete(BT_QUANTIZED_BVH_NODE* obj);

	EXPORT GIM_QUANTIZED_BVH_NODE_ARRAY* GIM_QUANTIZED_BVH_NODE_ARRAY_new();
	EXPORT void GIM_QUANTIZED_BVH_NODE_ARRAY_delete(GIM_QUANTIZED_BVH_NODE_ARRAY* obj);

	EXPORT btQuantizedBvhTree* btQuantizedBvhTree_new();
	EXPORT void btQuantizedBvhTree_build_tree(btQuantizedBvhTree* obj, GIM_BVH_DATA_ARRAY* primitive_boxes);
	EXPORT void btQuantizedBvhTree_clearNodes(btQuantizedBvhTree* obj);
	EXPORT const BT_QUANTIZED_BVH_NODE* btQuantizedBvhTree_get_node_pointer(btQuantizedBvhTree* obj);
	EXPORT const BT_QUANTIZED_BVH_NODE* btQuantizedBvhTree_get_node_pointer2(btQuantizedBvhTree* obj, int index);
	EXPORT int btQuantizedBvhTree_getEscapeNodeIndex(btQuantizedBvhTree* obj, int nodeindex);
	EXPORT int btQuantizedBvhTree_getLeftNode(btQuantizedBvhTree* obj, int nodeindex);
	EXPORT void btQuantizedBvhTree_getNodeBound(btQuantizedBvhTree* obj, int nodeindex, btAABB* bound);
	EXPORT int btQuantizedBvhTree_getNodeCount(btQuantizedBvhTree* obj);
	EXPORT int btQuantizedBvhTree_getNodeData(btQuantizedBvhTree* obj, int nodeindex);
	EXPORT int btQuantizedBvhTree_getRightNode(btQuantizedBvhTree* obj, int nodeindex);
	EXPORT bool btQuantizedBvhTree_isLeafNode(btQuantizedBvhTree* obj, int nodeindex);
	EXPORT void btQuantizedBvhTree_quantizePoint(btQuantizedBvhTree* obj, unsigned short* quantizedpoint, const btScalar* point);
	EXPORT void btQuantizedBvhTree_setNodeBound(btQuantizedBvhTree* obj, int nodeindex, const btAABB* bound);
	EXPORT bool btQuantizedBvhTree_testQuantizedBoxOverlapp(btQuantizedBvhTree* obj, int node_index, unsigned short* quantizedMin, unsigned short* quantizedMax);
	EXPORT void btQuantizedBvhTree_delete(btQuantizedBvhTree* obj);

	EXPORT btGImpactQuantizedBvh* btGImpactQuantizedBvh_new();
	EXPORT btGImpactQuantizedBvh* btGImpactQuantizedBvh_new2(btPrimitiveManagerBase* primitive_manager);
	EXPORT bool btGImpactQuantizedBvh_boxQuery(btGImpactQuantizedBvh* obj, const btAABB* box, btAlignedIntArray* collided_results);
	EXPORT bool btGImpactQuantizedBvh_boxQueryTrans(btGImpactQuantizedBvh* obj, const btAABB* box, const btScalar* transform, btAlignedIntArray* collided_results);
	EXPORT void btGImpactQuantizedBvh_buildSet(btGImpactQuantizedBvh* obj);
	EXPORT void btGImpactQuantizedBvh_find_collision(const btGImpactQuantizedBvh* boxset1, const btScalar* trans1, const btGImpactQuantizedBvh* boxset2, const btScalar* trans2, btPairSet* collision_pairs);
	EXPORT const BT_QUANTIZED_BVH_NODE* btGImpactQuantizedBvh_get_node_pointer(btGImpactQuantizedBvh* obj);
	EXPORT const BT_QUANTIZED_BVH_NODE* btGImpactQuantizedBvh_get_node_pointer2(btGImpactQuantizedBvh* obj, int index);
	EXPORT int btGImpactQuantizedBvh_getEscapeNodeIndex(btGImpactQuantizedBvh* obj, int nodeindex);
	EXPORT btAABB* btGImpactQuantizedBvh_getGlobalBox(btGImpactQuantizedBvh* obj);
	EXPORT int btGImpactQuantizedBvh_getLeftNode(btGImpactQuantizedBvh* obj, int nodeindex);
	EXPORT void btGImpactQuantizedBvh_getNodeBound(btGImpactQuantizedBvh* obj, int nodeindex, btAABB* bound);
	EXPORT int btGImpactQuantizedBvh_getNodeCount(btGImpactQuantizedBvh* obj);
	EXPORT int btGImpactQuantizedBvh_getNodeData(btGImpactQuantizedBvh* obj, int nodeindex);
	EXPORT void btGImpactQuantizedBvh_getNodeTriangle(btGImpactQuantizedBvh* obj, int nodeindex, btPrimitiveTriangle* triangle);
	EXPORT btPrimitiveManagerBase* btGImpactQuantizedBvh_getPrimitiveManager(btGImpactQuantizedBvh* obj);
	EXPORT int btGImpactQuantizedBvh_getRightNode(btGImpactQuantizedBvh* obj, int nodeindex);
	EXPORT bool btGImpactQuantizedBvh_hasHierarchy(btGImpactQuantizedBvh* obj);
	EXPORT bool btGImpactQuantizedBvh_isLeafNode(btGImpactQuantizedBvh* obj, int nodeindex);
	EXPORT bool btGImpactQuantizedBvh_isTrimesh(btGImpactQuantizedBvh* obj);
	EXPORT bool btGImpactQuantizedBvh_rayQuery(btGImpactQuantizedBvh* obj, const btScalar* ray_dir, const btScalar* ray_origin, btAlignedIntArray* collided_results);
	EXPORT void btGImpactQuantizedBvh_setNodeBound(btGImpactQuantizedBvh* obj, int nodeindex, const btAABB* bound);
	EXPORT void btGImpactQuantizedBvh_setPrimitiveManager(btGImpactQuantizedBvh* obj, btPrimitiveManagerBase* primitive_manager);
	EXPORT void btGImpactQuantizedBvh_update(btGImpactQuantizedBvh* obj);
	EXPORT void btGImpactQuantizedBvh_delete(btGImpactQuantizedBvh* obj);
}
