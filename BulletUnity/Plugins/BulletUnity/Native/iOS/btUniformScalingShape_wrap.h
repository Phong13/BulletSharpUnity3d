#include "main.h"

extern "C"
{
	EXPORT btUniformScalingShape* btUniformScalingShape_new(btConvexShape* convexChildShape, btScalar uniformScalingFactor);
	EXPORT btConvexShape* btUniformScalingShape_getChildShape(btUniformScalingShape* obj);
	EXPORT btScalar btUniformScalingShape_getUniformScalingFactor(btUniformScalingShape* obj);
}
