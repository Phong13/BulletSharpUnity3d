#include "main.h"

extern "C"
{
	EXPORT void btConvexInternalShape_getImplicitShapeDimensions(btConvexInternalShape* obj, btScalar* dimensions);
	EXPORT void btConvexInternalShape_getLocalScalingNV(btConvexInternalShape* obj, btScalar* value);
	EXPORT btScalar btConvexInternalShape_getMarginNV(btConvexInternalShape* obj);
	EXPORT void btConvexInternalShape_setImplicitShapeDimensions(btConvexInternalShape* obj, const btScalar* dimensions);
	EXPORT void btConvexInternalShape_setSafeMargin(btConvexInternalShape* obj, btScalar minDimension);
	EXPORT void btConvexInternalShape_setSafeMargin2(btConvexInternalShape* obj, btScalar minDimension, btScalar defaultMarginMultiplier);
	EXPORT void btConvexInternalShape_setSafeMargin3(btConvexInternalShape* obj, const btScalar* halfExtents);
	EXPORT void btConvexInternalShape_setSafeMargin4(btConvexInternalShape* obj, const btScalar* halfExtents, btScalar defaultMarginMultiplier);

	EXPORT void btConvexInternalAabbCachingShape_recalcLocalAabb(btConvexInternalAabbCachingShape* obj);
}
