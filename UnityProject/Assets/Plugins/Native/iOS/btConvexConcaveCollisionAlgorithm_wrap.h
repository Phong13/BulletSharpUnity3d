#include "main.h"

extern "C"
{
	EXPORT btConvexTriangleCallback* btConvexTriangleCallback_new(btDispatcher* dispatcher, const btCollisionObjectWrapper* body0Wrap, const btCollisionObjectWrapper* body1Wrap, bool isSwapped);
	EXPORT void btConvexTriangleCallback_clearCache(btConvexTriangleCallback* obj);
	EXPORT void btConvexTriangleCallback_clearWrapperData(btConvexTriangleCallback* obj);
	EXPORT void btConvexTriangleCallback_getAabbMax(btConvexTriangleCallback* obj, btScalar* value);
	EXPORT void btConvexTriangleCallback_getAabbMin(btConvexTriangleCallback* obj, btScalar* value);
	EXPORT btPersistentManifold* btConvexTriangleCallback_getManifoldPtr(btConvexTriangleCallback* obj);
	EXPORT int btConvexTriangleCallback_getTriangleCount(btConvexTriangleCallback* obj);
	EXPORT void btConvexTriangleCallback_setManifoldPtr(btConvexTriangleCallback* obj, btPersistentManifold* value);
	EXPORT void btConvexTriangleCallback_setTimeStepAndCounters(btConvexTriangleCallback* obj, btScalar collisionMarginTriangle, const btDispatcherInfo* dispatchInfo, const btCollisionObjectWrapper* convexBodyWrap, const btCollisionObjectWrapper* triBodyWrap, btManifoldResult* resultOut);
	EXPORT void btConvexTriangleCallback_setTriangleCount(btConvexTriangleCallback* obj, int value);

	EXPORT btConvexConcaveCollisionAlgorithm_CreateFunc* btConvexConcaveCollisionAlgorithm_CreateFunc_new();

	EXPORT btConvexConcaveCollisionAlgorithm_SwappedCreateFunc* btConvexConcaveCollisionAlgorithm_SwappedCreateFunc_new();

	EXPORT btConvexConcaveCollisionAlgorithm* btConvexConcaveCollisionAlgorithm_new(const btCollisionAlgorithmConstructionInfo* ci, const btCollisionObjectWrapper* body0Wrap, const btCollisionObjectWrapper* body1Wrap, bool isSwapped);
	EXPORT void btConvexConcaveCollisionAlgorithm_clearCache(btConvexConcaveCollisionAlgorithm* obj);
}
