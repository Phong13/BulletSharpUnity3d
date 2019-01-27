#include "main.h"

extern "C"
{
	EXPORT void btConvexShape_batchedUnitVectorGetSupportingVertexWithoutMargin(btConvexShape* obj, const btVector3* vectors, btVector3* supportVerticesOut, int numVectors);
	EXPORT void btConvexShape_getAabbNonVirtual(btConvexShape* obj, const btScalar* t, btScalar* aabbMin, btScalar* aabbMax);
	EXPORT void btConvexShape_getAabbSlow(btConvexShape* obj, const btScalar* t, btScalar* aabbMin, btScalar* aabbMax);
	EXPORT btScalar btConvexShape_getMarginNonVirtual(btConvexShape* obj);
	EXPORT int btConvexShape_getNumPreferredPenetrationDirections(btConvexShape* obj);
	EXPORT void btConvexShape_getPreferredPenetrationDirection(btConvexShape* obj, int index, btScalar* penetrationVector);
	EXPORT void btConvexShape_localGetSupportingVertex(btConvexShape* obj, const btScalar* vec, btScalar* value);
	EXPORT void btConvexShape_localGetSupportingVertexWithoutMargin(btConvexShape* obj, const btScalar* vec, btScalar* value);
	EXPORT void btConvexShape_localGetSupportVertexNonVirtual(btConvexShape* obj, const btScalar* vec, btScalar* value);
	EXPORT void btConvexShape_localGetSupportVertexWithoutMarginNonVirtual(btConvexShape* obj, const btScalar* vec, btScalar* value);
	EXPORT void btConvexShape_project(btConvexShape* obj, const btScalar* trans, const btScalar* dir, btScalar* minProj, btScalar* maxProj, btScalar* witnesPtMin, btScalar* witnesPtMax);
}
