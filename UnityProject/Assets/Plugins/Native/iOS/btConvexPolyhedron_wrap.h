#include "main.h"

extern "C"
{
	EXPORT btFace* btFace_new();
	EXPORT btAlignedIntArray* btFace_getIndices(btFace* obj);
	EXPORT btScalar* btFace_getPlane(btFace* obj);
	EXPORT void btFace_delete(btFace* obj);

	EXPORT btConvexPolyhedron* btConvexPolyhedron_new();
	EXPORT void btConvexPolyhedron_getExtents(btConvexPolyhedron* obj, btScalar* value);
	EXPORT btAlignedFaceArray* btConvexPolyhedron_getFaces(btConvexPolyhedron* obj);
	EXPORT void btConvexPolyhedron_getLocalCenter(btConvexPolyhedron* obj, btScalar* value);
	EXPORT void btConvexPolyhedron_getMC(btConvexPolyhedron* obj, btScalar* value);
	EXPORT void btConvexPolyhedron_getME(btConvexPolyhedron* obj, btScalar* value);
	EXPORT btScalar btConvexPolyhedron_getRadius(btConvexPolyhedron* obj);
	EXPORT btAlignedVector3Array* btConvexPolyhedron_getUniqueEdges(btConvexPolyhedron* obj);
	EXPORT btAlignedVector3Array* btConvexPolyhedron_getVertices(btConvexPolyhedron* obj);
	EXPORT void btConvexPolyhedron_initialize(btConvexPolyhedron* obj);
	EXPORT void btConvexPolyhedron_project(btConvexPolyhedron* obj, const btScalar* trans, const btScalar* dir, btScalar* minProj, btScalar* maxProj, btScalar* witnesPtMin, btScalar* witnesPtMax);
	EXPORT void btConvexPolyhedron_setExtents(btConvexPolyhedron* obj, const btScalar* value);
	EXPORT void btConvexPolyhedron_setLocalCenter(btConvexPolyhedron* obj, const btScalar* value);
	EXPORT void btConvexPolyhedron_setMC(btConvexPolyhedron* obj, const btScalar* value);
	EXPORT void btConvexPolyhedron_setME(btConvexPolyhedron* obj, const btScalar* value);
	EXPORT void btConvexPolyhedron_setRadius(btConvexPolyhedron* obj, btScalar value);
	EXPORT bool btConvexPolyhedron_testContainment(btConvexPolyhedron* obj);
	EXPORT void btConvexPolyhedron_delete(btConvexPolyhedron* obj);
}
