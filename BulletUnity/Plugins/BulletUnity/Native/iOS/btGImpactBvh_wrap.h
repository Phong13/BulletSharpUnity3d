#include "main.h"

extern "C"
{
	EXPORT GIM_PAIR* GIM_PAIR_new();
	EXPORT GIM_PAIR* GIM_PAIR_new2(const GIM_PAIR* p);
	EXPORT GIM_PAIR* GIM_PAIR_new3(int index1, int index2);
	EXPORT int GIM_PAIR_getIndex1(GIM_PAIR* obj);
	EXPORT int GIM_PAIR_getIndex2(GIM_PAIR* obj);
	EXPORT void GIM_PAIR_setIndex1(GIM_PAIR* obj, int value);
	EXPORT void GIM_PAIR_setIndex2(GIM_PAIR* obj, int value);
	EXPORT void GIM_PAIR_delete(GIM_PAIR* obj);

	EXPORT btPairSet* btPairSet_new();
	EXPORT void btPairSet_push_pair(btPairSet* obj, int index1, int index2);
	EXPORT void btPairSet_push_pair_inv(btPairSet* obj, int index1, int index2);
	EXPORT void btPairSet_delete(btPairSet* obj);

	EXPORT GIM_BVH_DATA* GIM_BVH_DATA_new();
	EXPORT btAABB* GIM_BVH_DATA_getBound(GIM_BVH_DATA* obj);
	EXPORT int GIM_BVH_DATA_getData(GIM_BVH_DATA* obj);
	EXPORT void GIM_BVH_DATA_setBound(GIM_BVH_DATA* obj, const btAABB* value);
	EXPORT void GIM_BVH_DATA_setData(GIM_BVH_DATA* obj, int value);
	EXPORT void GIM_BVH_DATA_delete(GIM_BVH_DATA* obj);

	EXPORT GIM_BVH_TREE_NODE* GIM_BVH_TREE_NODE_new();
	EXPORT btAABB* GIM_BVH_TREE_NODE_getBound(GIM_BVH_TREE_NODE* obj);
	EXPORT int GIM_BVH_TREE_NODE_getDataIndex(GIM_BVH_TREE_NODE* obj);
	EXPORT int GIM_BVH_TREE_NODE_getEscapeIndex(GIM_BVH_TREE_NODE* obj);
	EXPORT bool GIM_BVH_TREE_NODE_isLeafNode(GIM_BVH_TREE_NODE* obj);
	EXPORT void GIM_BVH_TREE_NODE_setBound(GIM_BVH_TREE_NODE* obj, const btAABB* value);
	EXPORT void GIM_BVH_TREE_NODE_setDataIndex(GIM_BVH_TREE_NODE* obj, int index);
	EXPORT void GIM_BVH_TREE_NODE_setEscapeIndex(GIM_BVH_TREE_NODE* obj, int index);
	EXPORT void GIM_BVH_TREE_NODE_delete(GIM_BVH_TREE_NODE* obj);

	EXPORT GIM_BVH_DATA_ARRAY* GIM_BVH_DATA_ARRAY_new();
	EXPORT void GIM_BVH_DATA_ARRAY_delete(GIM_BVH_DATA_ARRAY* obj);

	EXPORT GIM_BVH_TREE_NODE_ARRAY* GIM_BVH_TREE_NODE_ARRAY_new();
	EXPORT void GIM_BVH_TREE_NODE_ARRAY_delete(GIM_BVH_TREE_NODE_ARRAY* obj);

	EXPORT btBvhTree* btBvhTree_new();
	EXPORT void btBvhTree_build_tree(btBvhTree* obj, GIM_BVH_DATA_ARRAY* primitive_boxes);
	EXPORT void btBvhTree_clearNodes(btBvhTree* obj);
	EXPORT const GIM_BVH_TREE_NODE* btBvhTree_get_node_pointer(btBvhTree* obj);
	EXPORT const GIM_BVH_TREE_NODE* btBvhTree_get_node_pointer2(btBvhTree* obj, int index);
	EXPORT int btBvhTree_getEscapeNodeIndex(btBvhTree* obj, int nodeindex);
	EXPORT int btBvhTree_getLeftNode(btBvhTree* obj, int nodeindex);
	EXPORT void btBvhTree_getNodeBound(btBvhTree* obj, int nodeindex, btAABB* bound);
	EXPORT int btBvhTree_getNodeCount(btBvhTree* obj);
	EXPORT int btBvhTree_getNodeData(btBvhTree* obj, int nodeindex);
	EXPORT int btBvhTree_getRightNode(btBvhTree* obj, int nodeindex);
	EXPORT bool btBvhTree_isLeafNode(btBvhTree* obj, int nodeindex);
	EXPORT void btBvhTree_setNodeBound(btBvhTree* obj, int nodeindex, const btAABB* bound);
	EXPORT void btBvhTree_delete(btBvhTree* obj);

	EXPORT void btPrimitiveManagerBase_get_primitive_box(btPrimitiveManagerBase* obj, int prim_index, btAABB* primbox);
	EXPORT int btPrimitiveManagerBase_get_primitive_count(btPrimitiveManagerBase* obj);
	EXPORT void btPrimitiveManagerBase_get_primitive_triangle(btPrimitiveManagerBase* obj, int prim_index, btPrimitiveTriangle* triangle);
	EXPORT bool btPrimitiveManagerBase_is_trimesh(btPrimitiveManagerBase* obj);
	EXPORT void btPrimitiveManagerBase_delete(btPrimitiveManagerBase* obj);

	EXPORT btGImpactBvh* btGImpactBvh_new();
	EXPORT btGImpactBvh* btGImpactBvh_new2(btPrimitiveManagerBase* primitive_manager);
	EXPORT bool btGImpactBvh_boxQuery(btGImpactBvh* obj, const btAABB* box, btAlignedIntArray* collided_results);
	EXPORT bool btGImpactBvh_boxQueryTrans(btGImpactBvh* obj, const btAABB* box, const btScalar* transform, btAlignedIntArray* collided_results);
	EXPORT void btGImpactBvh_buildSet(btGImpactBvh* obj);
	EXPORT void btGImpactBvh_find_collision(btGImpactBvh* boxset1, const btScalar* trans1, btGImpactBvh* boxset2, const btScalar* trans2, btPairSet* collision_pairs);
	EXPORT const GIM_BVH_TREE_NODE* btGImpactBvh_get_node_pointer(btGImpactBvh* obj);
	EXPORT const GIM_BVH_TREE_NODE* btGImpactBvh_get_node_pointer2(btGImpactBvh* obj, int index);
	EXPORT int btGImpactBvh_getEscapeNodeIndex(btGImpactBvh* obj, int nodeindex);
	EXPORT btAABB* btGImpactBvh_getGlobalBox(btGImpactBvh* obj);
	EXPORT int btGImpactBvh_getLeftNode(btGImpactBvh* obj, int nodeindex);
	EXPORT void btGImpactBvh_getNodeBound(btGImpactBvh* obj, int nodeindex, btAABB* bound);
	EXPORT int btGImpactBvh_getNodeCount(btGImpactBvh* obj);
	EXPORT int btGImpactBvh_getNodeData(btGImpactBvh* obj, int nodeindex);
	EXPORT void btGImpactBvh_getNodeTriangle(btGImpactBvh* obj, int nodeindex, btPrimitiveTriangle* triangle);
	EXPORT btPrimitiveManagerBase* btGImpactBvh_getPrimitiveManager(btGImpactBvh* obj);
	EXPORT int btGImpactBvh_getRightNode(btGImpactBvh* obj, int nodeindex);
	EXPORT bool btGImpactBvh_hasHierarchy(btGImpactBvh* obj);
	EXPORT bool btGImpactBvh_isLeafNode(btGImpactBvh* obj, int nodeindex);
	EXPORT bool btGImpactBvh_isTrimesh(btGImpactBvh* obj);
	EXPORT bool btGImpactBvh_rayQuery(btGImpactBvh* obj, const btScalar* ray_dir, const btScalar* ray_origin, btAlignedIntArray* collided_results);
	EXPORT void btGImpactBvh_setNodeBound(btGImpactBvh* obj, int nodeindex, const btAABB* bound);
	EXPORT void btGImpactBvh_setPrimitiveManager(btGImpactBvh* obj, btPrimitiveManagerBase* primitive_manager);
	EXPORT void btGImpactBvh_update(btGImpactBvh* obj);
	EXPORT void btGImpactBvh_delete(btGImpactBvh* obj);
}
