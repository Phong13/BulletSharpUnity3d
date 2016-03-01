#include "main.h"

extern "C"
{
	EXPORT btMinkowskiSumShape* btMinkowskiSumShape_new(const btConvexShape* shapeA, const btConvexShape* shapeB);
	EXPORT const btConvexShape* btMinkowskiSumShape_getShapeA(btMinkowskiSumShape* obj);
	EXPORT const btConvexShape* btMinkowskiSumShape_getShapeB(btMinkowskiSumShape* obj);
	EXPORT void btMinkowskiSumShape_getTransformA(btMinkowskiSumShape* obj, btScalar* transA);
	EXPORT void btMinkowskiSumShape_GetTransformB(btMinkowskiSumShape* obj, btScalar* transB);
	EXPORT void btMinkowskiSumShape_setTransformA(btMinkowskiSumShape* obj, const btScalar* transA);
	EXPORT void btMinkowskiSumShape_setTransformB(btMinkowskiSumShape* obj, const btScalar* transB);
}
