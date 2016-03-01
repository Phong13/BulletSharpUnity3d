#include "main.h"

extern "C"
{
	EXPORT void btStridingMeshInterface_calculateAabbBruteForce(btStridingMeshInterface* obj, btScalar* aabbMin, btScalar* aabbMax);
	EXPORT int btStridingMeshInterface_calculateSerializeBufferSize(btStridingMeshInterface* obj);
	EXPORT void btStridingMeshInterface_getLockedReadOnlyVertexIndexBase(btStridingMeshInterface* obj, const unsigned char** vertexbase, int* numverts, PHY_ScalarType* type, int* stride, const unsigned char** indexbase, int* indexstride, int* numfaces, PHY_ScalarType* indicestype);
	EXPORT void btStridingMeshInterface_getLockedReadOnlyVertexIndexBase2(btStridingMeshInterface* obj, const unsigned char** vertexbase, int* numverts, PHY_ScalarType* type, int* stride, const unsigned char** indexbase, int* indexstride, int* numfaces, PHY_ScalarType* indicestype, int subpart);
	EXPORT void btStridingMeshInterface_getLockedVertexIndexBase(btStridingMeshInterface* obj, unsigned char** vertexbase, int* numverts, PHY_ScalarType* type, int* stride, unsigned char** indexbase, int* indexstride, int* numfaces, PHY_ScalarType* indicestype);
	EXPORT void btStridingMeshInterface_getLockedVertexIndexBase2(btStridingMeshInterface* obj, unsigned char** vertexbase, int* numverts, PHY_ScalarType* type, int* stride, unsigned char** indexbase, int* indexstride, int* numfaces, PHY_ScalarType* indicestype, int subpart);
	EXPORT int btStridingMeshInterface_getNumSubParts(btStridingMeshInterface* obj);
	EXPORT void btStridingMeshInterface_getPremadeAabb(btStridingMeshInterface* obj, btScalar* aabbMin, btScalar* aabbMax);
	EXPORT void btStridingMeshInterface_getScaling(btStridingMeshInterface* obj, btScalar* scaling);
	EXPORT bool btStridingMeshInterface_hasPremadeAabb(btStridingMeshInterface* obj);
	EXPORT void btStridingMeshInterface_InternalProcessAllTriangles(btStridingMeshInterface* obj, btInternalTriangleIndexCallback* callback, const btScalar* aabbMin, const btScalar* aabbMax);
	EXPORT void btStridingMeshInterface_preallocateIndices(btStridingMeshInterface* obj, int numindices);
	EXPORT void btStridingMeshInterface_preallocateVertices(btStridingMeshInterface* obj, int numverts);
	EXPORT const char* btStridingMeshInterface_serialize(btStridingMeshInterface* obj, void* dataBuffer, btSerializer* serializer);
	EXPORT void btStridingMeshInterface_setPremadeAabb(btStridingMeshInterface* obj, const btScalar* aabbMin, const btScalar* aabbMax);
	EXPORT void btStridingMeshInterface_setScaling(btStridingMeshInterface* obj, const btScalar* scaling);
	EXPORT void btStridingMeshInterface_unLockReadOnlyVertexBase(btStridingMeshInterface* obj, int subpart);
	EXPORT void btStridingMeshInterface_unLockVertexBase(btStridingMeshInterface* obj, int subpart);
	EXPORT void btStridingMeshInterface_delete(btStridingMeshInterface* obj);
}
