#include "main.h"

#ifndef BT_BROADPHASE_INTERFACE_H
#define pBroadphaseAabbCallback_Process void*
#define btBroadphaseAabbCallbackWrapper void
#define btBroadphaseRayCallbackWrapper void
#else
typedef bool (*pBroadphaseAabbCallback_Process)(const btBroadphaseProxy* proxy);

class btBroadphaseAabbCallbackWrapper : public btBroadphaseAabbCallback
{
private:
	pBroadphaseAabbCallback_Process _processCallback;

public:
	btBroadphaseAabbCallbackWrapper(pBroadphaseAabbCallback_Process processCallback);

	virtual bool process(const btBroadphaseProxy* proxy);
};

class btBroadphaseRayCallbackWrapper : public btBroadphaseRayCallback
{
private:
	pBroadphaseAabbCallback_Process _processCallback;

public:
	btBroadphaseRayCallbackWrapper(pBroadphaseAabbCallback_Process processCallback);

	virtual bool process(const btBroadphaseProxy* proxy);
};
#endif

extern "C"
{
	EXPORT btBroadphaseAabbCallbackWrapper* btBroadphaseAabbCallbackWrapper_new(pBroadphaseAabbCallback_Process processCallback);

	EXPORT bool btBroadphaseAabbCallback_process(btBroadphaseAabbCallback* obj, const btBroadphaseProxy* proxy);
	EXPORT void btBroadphaseAabbCallback_delete(btBroadphaseAabbCallback* obj);

	EXPORT btBroadphaseRayCallbackWrapper* btBroadphaseRayCallbackWrapper_new(pBroadphaseAabbCallback_Process processCallback);

	EXPORT btScalar btBroadphaseRayCallback_getLambda_max(btBroadphaseRayCallback* obj);
	EXPORT void btBroadphaseRayCallback_getRayDirectionInverse(btBroadphaseRayCallback* obj, btScalar* value);
	EXPORT unsigned int* btBroadphaseRayCallback_getSigns(btBroadphaseRayCallback* obj);
	EXPORT void btBroadphaseRayCallback_setLambda_max(btBroadphaseRayCallback* obj, btScalar value);
	EXPORT void btBroadphaseRayCallback_setRayDirectionInverse(btBroadphaseRayCallback* obj, const btScalar* value);

	EXPORT void btBroadphaseInterface_aabbTest(btBroadphaseInterface* obj, const btScalar* aabbMin, const btScalar* aabbMax, btBroadphaseAabbCallback* callback);
	EXPORT void btBroadphaseInterface_calculateOverlappingPairs(btBroadphaseInterface* obj, btDispatcher* dispatcher);
	EXPORT btBroadphaseProxy* btBroadphaseInterface_createProxy(btBroadphaseInterface* obj, const btScalar* aabbMin, const btScalar* aabbMax, int shapeType, void* userPtr, short collisionFilterGroup, short collisionFilterMask, btDispatcher* dispatcher, void* multiSapProxy);
	EXPORT void btBroadphaseInterface_destroyProxy(btBroadphaseInterface* obj, btBroadphaseProxy* proxy, btDispatcher* dispatcher);
	EXPORT void btBroadphaseInterface_getAabb(btBroadphaseInterface* obj, btBroadphaseProxy* proxy, btScalar* aabbMin, btScalar* aabbMax);
	EXPORT void btBroadphaseInterface_getBroadphaseAabb(btBroadphaseInterface* obj, btScalar* aabbMin, btScalar* aabbMax);
	EXPORT btOverlappingPairCache* btBroadphaseInterface_getOverlappingPairCache(btBroadphaseInterface* obj);
	EXPORT void btBroadphaseInterface_printStats(btBroadphaseInterface* obj);
	EXPORT void btBroadphaseInterface_rayTest(btBroadphaseInterface* obj, const btScalar* rayFrom, const btScalar* rayTo, btBroadphaseRayCallback* rayCallback);
	EXPORT void btBroadphaseInterface_rayTest2(btBroadphaseInterface* obj, const btScalar* rayFrom, const btScalar* rayTo, btBroadphaseRayCallback* rayCallback, const btScalar* aabbMin);
	EXPORT void btBroadphaseInterface_rayTest3(btBroadphaseInterface* obj, const btScalar* rayFrom, const btScalar* rayTo, btBroadphaseRayCallback* rayCallback, const btScalar* aabbMin, const btScalar* aabbMax);
	EXPORT void btBroadphaseInterface_resetPool(btBroadphaseInterface* obj, btDispatcher* dispatcher);
	EXPORT void btBroadphaseInterface_setAabb(btBroadphaseInterface* obj, btBroadphaseProxy* proxy, const btScalar* aabbMin, const btScalar* aabbMax, btDispatcher* dispatcher);
	EXPORT void btBroadphaseInterface_delete(btBroadphaseInterface* obj);
}
