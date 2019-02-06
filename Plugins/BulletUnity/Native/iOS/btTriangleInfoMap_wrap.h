#include "main.h"

extern "C"
{
	EXPORT btTriangleInfo* btTriangleInfo_new();
	EXPORT btScalar btTriangleInfo_getEdgeV0V1Angle(btTriangleInfo* obj);
	EXPORT btScalar btTriangleInfo_getEdgeV1V2Angle(btTriangleInfo* obj);
	EXPORT btScalar btTriangleInfo_getEdgeV2V0Angle(btTriangleInfo* obj);
	EXPORT int btTriangleInfo_getFlags(btTriangleInfo* obj);
	EXPORT void btTriangleInfo_setEdgeV0V1Angle(btTriangleInfo* obj, btScalar value);
	EXPORT void btTriangleInfo_setEdgeV1V2Angle(btTriangleInfo* obj, btScalar value);
	EXPORT void btTriangleInfo_setEdgeV2V0Angle(btTriangleInfo* obj, btScalar value);
	EXPORT void btTriangleInfo_setFlags(btTriangleInfo* obj, int value);
	EXPORT void btTriangleInfo_delete(btTriangleInfo* obj);

	EXPORT btTriangleInfoMap* btTriangleInfoMap_new();
	EXPORT int btTriangleInfoMap_calculateSerializeBufferSize(btTriangleInfoMap* obj);
	//EXPORT void btTriangleInfoMap_deSerialize(btTriangleInfoMap* obj, btTriangleInfoMapData* data);
	EXPORT btScalar btTriangleInfoMap_getConvexEpsilon(btTriangleInfoMap* obj);
	EXPORT btScalar btTriangleInfoMap_getEdgeDistanceThreshold(btTriangleInfoMap* obj);
	EXPORT btScalar btTriangleInfoMap_getEqualVertexThreshold(btTriangleInfoMap* obj);
	EXPORT btScalar btTriangleInfoMap_getMaxEdgeAngleThreshold(btTriangleInfoMap* obj);
	EXPORT btScalar btTriangleInfoMap_getPlanarEpsilon(btTriangleInfoMap* obj);
	EXPORT btScalar btTriangleInfoMap_getZeroAreaThreshold(btTriangleInfoMap* obj);
	EXPORT const char* btTriangleInfoMap_serialize(btTriangleInfoMap* obj, void* dataBuffer, btSerializer* serializer);
	EXPORT void btTriangleInfoMap_setConvexEpsilon(btTriangleInfoMap* obj, btScalar value);
	EXPORT void btTriangleInfoMap_setEdgeDistanceThreshold(btTriangleInfoMap* obj, btScalar value);
	EXPORT void btTriangleInfoMap_setEqualVertexThreshold(btTriangleInfoMap* obj, btScalar value);
	EXPORT void btTriangleInfoMap_setMaxEdgeAngleThreshold(btTriangleInfoMap* obj, btScalar value);
	EXPORT void btTriangleInfoMap_setPlanarEpsilon(btTriangleInfoMap* obj, btScalar value);
	EXPORT void btTriangleInfoMap_setZeroAreaThreshold(btTriangleInfoMap* obj, btScalar value);
	EXPORT void btTriangleInfoMap_delete(btTriangleInfoMap* obj);
}
