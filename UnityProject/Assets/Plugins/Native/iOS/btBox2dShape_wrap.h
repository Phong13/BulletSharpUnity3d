#include "main.h"

extern "C"
{
	EXPORT btBox2dShape* btBox2dShape_new(const btScalar* boxHalfExtents);
	EXPORT btBox2dShape* btBox2dShape_new2(btScalar boxHalfExtent);
	EXPORT btBox2dShape* btBox2dShape_new3(btScalar boxHalfExtentX, btScalar boxHalfExtentY, btScalar boxHalfExtentZ);
	EXPORT void btBox2dShape_getCentroid(btBox2dShape* obj, btScalar* value);
	EXPORT void btBox2dShape_getHalfExtentsWithMargin(btBox2dShape* obj, btScalar* value);
	EXPORT void btBox2dShape_getHalfExtentsWithoutMargin(btBox2dShape* obj, btScalar* value);
	EXPORT const btVector3* btBox2dShape_getNormals(btBox2dShape* obj);
	EXPORT void btBox2dShape_getPlaneEquation(btBox2dShape* obj, btScalar* plane, int i);
	EXPORT int btBox2dShape_getVertexCount(btBox2dShape* obj);
	EXPORT const btVector3* btBox2dShape_getVertices(btBox2dShape* obj);
}
