#include "main.h"

extern "C"
{
	EXPORT bool btGeometryUtil_areVerticesBehindPlane(const btScalar* planeNormal, const btAlignedVector3Array* vertices, btScalar margin);
	EXPORT void btGeometryUtil_getPlaneEquationsFromVertices(btAlignedVector3Array* vertices, btAlignedVector3Array* planeEquationsOut);
	EXPORT void btGeometryUtil_getVerticesFromPlaneEquations(const btAlignedVector3Array* planeEquations, btAlignedVector3Array* verticesOut);
	//EXPORT bool btGeometryUtil_isInside(const btAlignedVector3Array* vertices, const btScalar* planeNormal, btScalar margin);
	EXPORT bool btGeometryUtil_isPointInsidePlanes(const btAlignedVector3Array* planeEquations, const btScalar* point, btScalar margin);
}
