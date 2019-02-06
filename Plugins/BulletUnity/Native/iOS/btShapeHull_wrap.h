#include "main.h"

extern "C"
{
	EXPORT btShapeHull* btShapeHull_new(const btConvexShape* shape);
	EXPORT bool btShapeHull_buildHull(btShapeHull* obj, btScalar margin);
	EXPORT const unsigned int* btShapeHull_getIndexPointer(btShapeHull* obj);
	EXPORT const btVector3* btShapeHull_getVertexPointer(btShapeHull* obj);
	EXPORT int btShapeHull_numIndices(btShapeHull* obj);
	EXPORT int btShapeHull_numTriangles(btShapeHull* obj);
	EXPORT int btShapeHull_numVertices(btShapeHull* obj);
	EXPORT void btShapeHull_delete(btShapeHull* obj);
}
