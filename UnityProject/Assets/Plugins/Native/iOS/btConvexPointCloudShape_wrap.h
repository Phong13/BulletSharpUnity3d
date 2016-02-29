#include "main.h"

extern "C"
{
	EXPORT btConvexPointCloudShape* btConvexPointCloudShape_new();
	EXPORT btConvexPointCloudShape* btConvexPointCloudShape_new2(btVector3* points, int numPoints, const btScalar* localScaling);
	EXPORT btConvexPointCloudShape* btConvexPointCloudShape_new3(btVector3* points, int numPoints, const btScalar* localScaling, bool computeAabb);
	EXPORT int btConvexPointCloudShape_getNumPoints(btConvexPointCloudShape* obj);
	EXPORT void btConvexPointCloudShape_getScaledPoint(btConvexPointCloudShape* obj, int index, btScalar* value);
	EXPORT btVector3* btConvexPointCloudShape_getUnscaledPoints(btConvexPointCloudShape* obj);
	EXPORT void btConvexPointCloudShape_setPoints(btConvexPointCloudShape* obj, btVector3* points, int numPoints);
	EXPORT void btConvexPointCloudShape_setPoints2(btConvexPointCloudShape* obj, btVector3* points, int numPoints, bool computeAabb);
	EXPORT void btConvexPointCloudShape_setPoints3(btConvexPointCloudShape* obj, btVector3* points, int numPoints, bool computeAabb, const btScalar* localScaling);
}
