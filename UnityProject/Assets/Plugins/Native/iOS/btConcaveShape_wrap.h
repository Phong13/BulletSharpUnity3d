#include "main.h"

extern "C"
{
	EXPORT void btConcaveShape_processAllTriangles(btConcaveShape* obj, btTriangleCallback* callback, const btScalar* aabbMin, const btScalar* aabbMax);
}
