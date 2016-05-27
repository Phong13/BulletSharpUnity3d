#include "main.h"

extern "C"
{
	EXPORT btQuantizedBvhNode* btQuantizedBvhNode_new();
	EXPORT int btQuantizedBvhNode_getEscapeIndex(btQuantizedBvhNode* obj);
	EXPORT int btQuantizedBvhNode_getEscapeIndexOrTriangleIndex(btQuantizedBvhNode* obj);
	EXPORT int btQuantizedBvhNode_getPartId(btQuantizedBvhNode* obj);
	EXPORT unsigned short* btQuantizedBvhNode_getQuantizedAabbMax(btQuantizedBvhNode* obj);
	EXPORT unsigned short* btQuantizedBvhNode_getQuantizedAabbMin(btQuantizedBvhNode* obj);
	EXPORT int btQuantizedBvhNode_getTriangleIndex(btQuantizedBvhNode* obj);
	EXPORT bool btQuantizedBvhNode_isLeafNode(btQuantizedBvhNode* obj);
	EXPORT void btQuantizedBvhNode_setEscapeIndexOrTriangleIndex(btQuantizedBvhNode* obj, int value);
	EXPORT void btQuantizedBvhNode_delete(btQuantizedBvhNode* obj);

	EXPORT btOptimizedBvhNode* btOptimizedBvhNode_new();
	EXPORT void btOptimizedBvhNode_getAabbMaxOrg(btOptimizedBvhNode* obj, btScalar* value);
	EXPORT void btOptimizedBvhNode_getAabbMinOrg(btOptimizedBvhNode* obj, btScalar* value);
	EXPORT int btOptimizedBvhNode_getEscapeIndex(btOptimizedBvhNode* obj);
	EXPORT int btOptimizedBvhNode_getSubPart(btOptimizedBvhNode* obj);
	EXPORT int btOptimizedBvhNode_getTriangleIndex(btOptimizedBvhNode* obj);
	EXPORT void btOptimizedBvhNode_setAabbMaxOrg(btOptimizedBvhNode* obj, const btScalar* value);
	EXPORT void btOptimizedBvhNode_setAabbMinOrg(btOptimizedBvhNode* obj, const btScalar* value);
	EXPORT void btOptimizedBvhNode_setEscapeIndex(btOptimizedBvhNode* obj, int value);
	EXPORT void btOptimizedBvhNode_setSubPart(btOptimizedBvhNode* obj, int value);
	EXPORT void btOptimizedBvhNode_setTriangleIndex(btOptimizedBvhNode* obj, int value);
	EXPORT void btOptimizedBvhNode_delete(btOptimizedBvhNode* obj);

	EXPORT void btNodeOverlapCallback_processNode(btNodeOverlapCallback* obj, int subPart, int triangleIndex);
	EXPORT void btNodeOverlapCallback_delete(btNodeOverlapCallback* obj);

	EXPORT btQuantizedBvh* btQuantizedBvh_new();
	EXPORT void btQuantizedBvh_buildInternal(btQuantizedBvh* obj);
	EXPORT unsigned int btQuantizedBvh_calculateSerializeBufferSize(btQuantizedBvh* obj);
	EXPORT int btQuantizedBvh_calculateSerializeBufferSizeNew(btQuantizedBvh* obj);
	EXPORT void btQuantizedBvh_deSerializeDouble(btQuantizedBvh* obj, btQuantizedBvhDoubleData* quantizedBvhDoubleData);
	EXPORT void btQuantizedBvh_deSerializeFloat(btQuantizedBvh* obj, btQuantizedBvhFloatData* quantizedBvhFloatData);
	EXPORT btQuantizedBvh* btQuantizedBvh_deSerializeInPlace(void* i_alignedDataBuffer, unsigned int i_dataBufferSize, bool i_swapEndian);
	EXPORT unsigned int btQuantizedBvh_getAlignmentSerializationPadding();
	EXPORT QuantizedNodeArray* btQuantizedBvh_getLeafNodeArray(btQuantizedBvh* obj);
	EXPORT QuantizedNodeArray* btQuantizedBvh_getQuantizedNodeArray(btQuantizedBvh* obj);
	EXPORT BvhSubtreeInfoArray* btQuantizedBvh_getSubtreeInfoArray(btQuantizedBvh* obj);
	EXPORT bool btQuantizedBvh_isQuantized(btQuantizedBvh* obj);
	EXPORT void btQuantizedBvh_quantize(btQuantizedBvh* obj, unsigned short* out, const btScalar* point, int isMax);
	EXPORT void btQuantizedBvh_quantizeWithClamp(btQuantizedBvh* obj, unsigned short* out, const btScalar* point2, int isMax);
	EXPORT void btQuantizedBvh_reportAabbOverlappingNodex(btQuantizedBvh* obj, btNodeOverlapCallback* nodeCallback, const btScalar* aabbMin, const btScalar* aabbMax);
	EXPORT void btQuantizedBvh_reportBoxCastOverlappingNodex(btQuantizedBvh* obj, btNodeOverlapCallback* nodeCallback, const btScalar* raySource, const btScalar* rayTarget, const btScalar* aabbMin, const btScalar* aabbMax);
	EXPORT void btQuantizedBvh_reportRayOverlappingNodex(btQuantizedBvh* obj, btNodeOverlapCallback* nodeCallback, const btScalar* raySource, const btScalar* rayTarget);
	EXPORT bool btQuantizedBvh_serialize(btQuantizedBvh* obj, void* o_alignedDataBuffer, unsigned int i_dataBufferSize, bool i_swapEndian);
	EXPORT const char* btQuantizedBvh_serialize2(btQuantizedBvh* obj, void* dataBuffer, btSerializer* serializer);
	EXPORT void btQuantizedBvh_setQuantizationValues(btQuantizedBvh* obj, const btScalar* bvhAabbMin, const btScalar* bvhAabbMax);
	EXPORT void btQuantizedBvh_setQuantizationValues2(btQuantizedBvh* obj, const btScalar* bvhAabbMin, const btScalar* bvhAabbMax, btScalar quantizationMargin);
	EXPORT void btQuantizedBvh_setTraversalMode(btQuantizedBvh* obj, btQuantizedBvh_btTraversalMode traversalMode);
	EXPORT void btQuantizedBvh_unQuantize(btQuantizedBvh* obj, const unsigned short* vecIn, btScalar* value);
	EXPORT void btQuantizedBvh_delete(btQuantizedBvh* obj);
}
