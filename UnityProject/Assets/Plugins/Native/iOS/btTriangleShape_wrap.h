#include "main.h"

extern "C"
{
	EXPORT btTriangleShape* btTriangleShape_new();
	EXPORT btTriangleShape* btTriangleShape_new2(const btScalar* p0, const btScalar* p1, const btScalar* p2);
	EXPORT void btTriangleShape_calcNormal(btTriangleShape* obj, btScalar* normal);
	EXPORT void btTriangleShape_getPlaneEquation(btTriangleShape* obj, int i, btScalar* planeNormal, btScalar* planeSupport);
	EXPORT const btScalar* btTriangleShape_getVertexPtr(btTriangleShape* obj, int index);
	EXPORT btVector3* btTriangleShape_getVertices1(btTriangleShape* obj);
}
