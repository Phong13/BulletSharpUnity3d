#include "main.h"

extern "C"
{
	EXPORT btOptimizedBvh* btOptimizedBvh_new();
	EXPORT void btOptimizedBvh_build(btOptimizedBvh* obj, btStridingMeshInterface* triangles, bool useQuantizedAabbCompression, const btScalar* bvhAabbMin, const btScalar* bvhAabbMax);
	EXPORT btOptimizedBvh* btOptimizedBvh_deSerializeInPlace(void* i_alignedDataBuffer, unsigned int i_dataBufferSize, bool i_swapEndian);
	EXPORT void btOptimizedBvh_refit(btOptimizedBvh* obj, btStridingMeshInterface* triangles, const btScalar* aabbMin, const btScalar* aabbMax);
	EXPORT void btOptimizedBvh_refitPartial(btOptimizedBvh* obj, btStridingMeshInterface* triangles, const btScalar* aabbMin, const btScalar* aabbMax);
	EXPORT bool btOptimizedBvh_serializeInPlace(btOptimizedBvh* obj, void* o_alignedDataBuffer, unsigned int i_dataBufferSize, bool i_swapEndian);
	EXPORT void btOptimizedBvh_updateBvhNodes(btOptimizedBvh* obj, btStridingMeshInterface* meshInterface, int firstNode, int endNode, int index);
}
