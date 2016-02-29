#include "main.h"

extern "C"
{
	EXPORT btGhostObject* btGhostObject_new();
	EXPORT void btGhostObject_addOverlappingObjectInternal(btGhostObject* obj, btBroadphaseProxy* otherProxy);
	EXPORT void btGhostObject_addOverlappingObjectInternal2(btGhostObject* obj, btBroadphaseProxy* otherProxy, btBroadphaseProxy* thisProxy);
	EXPORT void btGhostObject_convexSweepTest(btGhostObject* obj, const btConvexShape* castShape, const btScalar* convexFromWorld, const btScalar* convexToWorld, btCollisionWorld_ConvexResultCallback* resultCallback);
	EXPORT void btGhostObject_convexSweepTest2(btGhostObject* obj, const btConvexShape* castShape, const btScalar* convexFromWorld, const btScalar* convexToWorld, btCollisionWorld_ConvexResultCallback* resultCallback, btScalar allowedCcdPenetration);
	EXPORT int btGhostObject_getNumOverlappingObjects(btGhostObject* obj);
	EXPORT btCollisionObject* btGhostObject_getOverlappingObject(btGhostObject* obj, int index);
	EXPORT btAlignedCollisionObjectArray* btGhostObject_getOverlappingPairs(btGhostObject* obj);
	EXPORT void btGhostObject_rayTest(btGhostObject* obj, const btScalar* rayFromWorld, const btScalar* rayToWorld, btCollisionWorld_RayResultCallback* resultCallback);
	EXPORT void btGhostObject_removeOverlappingObjectInternal(btGhostObject* obj, btBroadphaseProxy* otherProxy, btDispatcher* dispatcher);
	EXPORT void btGhostObject_removeOverlappingObjectInternal2(btGhostObject* obj, btBroadphaseProxy* otherProxy, btDispatcher* dispatcher, btBroadphaseProxy* thisProxy);
	EXPORT btGhostObject* btGhostObject_upcast(btCollisionObject* colObj);

	EXPORT btPairCachingGhostObject* btPairCachingGhostObject_new();
	EXPORT btHashedOverlappingPairCache* btPairCachingGhostObject_getOverlappingPairCache(btPairCachingGhostObject* obj);

	EXPORT btGhostPairCallback* btGhostPairCallback_new();
}
