#include "main.h"

#ifndef BT_COLLISION_WORLD_H
#define pContactResultCallback_AddSingleResult void*
#define pContactResultCallback_NeedsCollision void*
#define pConvexResultCallback_AddSingleResult void*
#define pConvexResultCallback_NeedsCollision void*
#define pRayResultCallback_AddSingleResult void*
#define pRayResultCallback_NeedsCollision void*
#define btCollisionWorld_ContactResultCallbackWrapper void
#define btCollisionWorld_ConvexResultCallbackWrapper void
#define btCollisionWorld_RayResultCallbackWrapper void
#else
typedef btScalar (*pContactResultCallback_AddSingleResult)(void* objPtr, btManifoldPoint& cp,
	const btCollisionObjectWrapper* colObj0Wrap, int partId0, int index0,
	const btCollisionObjectWrapper* colObj1Wrap, int partId1, int index1);
typedef bool (*pContactResultCallback_NeedsCollision)(void* objPtr, btBroadphaseProxy* proxy0);

class btCollisionWorld_ContactResultCallbackWrapper : public btCollisionWorld_ContactResultCallback
{
private:
	pContactResultCallback_AddSingleResult _addSingleResultCallback;
	pContactResultCallback_NeedsCollision _needsCollisionCallback;
    void* _managedWrapper;
public:
	btCollisionWorld_ContactResultCallbackWrapper(pContactResultCallback_AddSingleResult addSingleResultCallback,
		pContactResultCallback_NeedsCollision needsCollisionCallback,
        void* managedWrapper);

	virtual btScalar addSingleResult(btManifoldPoint& cp,
		const btCollisionObjectWrapper* colObj0Wrap, int partId0, int index0,
		const btCollisionObjectWrapper* colObj1Wrap, int partId1, int index1);
	virtual bool needsCollision(btBroadphaseProxy* proxy0) const;

	virtual bool baseNeedsCollision(btBroadphaseProxy* proxy0) const;
};

typedef btScalar (*pConvexResultCallback_AddSingleResult)(void* objPtr, btCollisionWorld_LocalConvexResult& convexResult,
	bool normalInWorldSpace);
typedef bool (*pConvexResultCallback_NeedsCollision)(void* objPtr, btBroadphaseProxy* proxy0);

class btCollisionWorld_ConvexResultCallbackWrapper : public btCollisionWorld_ConvexResultCallback
{
private:
	pConvexResultCallback_AddSingleResult _addSingleResultCallback;
	pConvexResultCallback_NeedsCollision _needsCollisionCallback;
    void* _managedWrapper;
public:
	btCollisionWorld_ConvexResultCallbackWrapper(pConvexResultCallback_AddSingleResult addSingleResultCallback,
		pConvexResultCallback_NeedsCollision needsCollisionCallback,
                                                 void* managedWrapper);

	virtual btScalar addSingleResult(btCollisionWorld_LocalConvexResult& convexResult, bool normalInWorldSpace);
	virtual bool needsCollision(btBroadphaseProxy* proxy0) const;

	virtual bool baseNeedsCollision(btBroadphaseProxy* proxy0) const;
};

typedef btScalar (*pRayResultCallback_AddSingleResult)(void* objPtr, btCollisionWorld_LocalRayResult& rayResult,
	bool normalInWorldSpace);
typedef bool (*pRayResultCallback_NeedsCollision)(void* objPtr, btBroadphaseProxy* proxy0);

class btCollisionWorld_RayResultCallbackWrapper : public btCollisionWorld_RayResultCallback
{
private:
	pRayResultCallback_AddSingleResult _addSingleResultCallback;
	pRayResultCallback_NeedsCollision _needsCollisionCallback;
    void* _managedWrapper;
public:
	btCollisionWorld_RayResultCallbackWrapper(pRayResultCallback_AddSingleResult addSingleResultCallback,
		pRayResultCallback_NeedsCollision needsCollisionCallback,
        void* objPtr);

	virtual btScalar addSingleResult(btCollisionWorld_LocalRayResult& rayResult, bool normalInWorldSpace);
	virtual bool needsCollision(btBroadphaseProxy* proxy0) const;

	virtual bool baseNeedsCollision(btBroadphaseProxy* proxy0) const;
};
#endif

extern "C"
{
	EXPORT btCollisionWorld_AllHitsRayResultCallback* btCollisionWorld_AllHitsRayResultCallback_new(const btScalar* rayFromWorld, const btScalar* rayToWorld, void* managedWrapper);
	EXPORT btAlignedCollisionObjectArray* btCollisionWorld_AllHitsRayResultCallback_getCollisionObjects(btCollisionWorld_AllHitsRayResultCallback* obj);
	EXPORT btAlignedScalarArray* btCollisionWorld_AllHitsRayResultCallback_getHitFractions(btCollisionWorld_AllHitsRayResultCallback* obj);
	EXPORT btAlignedVector3Array* btCollisionWorld_AllHitsRayResultCallback_getHitNormalWorld(btCollisionWorld_AllHitsRayResultCallback* obj);
	EXPORT btAlignedVector3Array* btCollisionWorld_AllHitsRayResultCallback_getHitPointWorld(btCollisionWorld_AllHitsRayResultCallback* obj);
	EXPORT void btCollisionWorld_AllHitsRayResultCallback_getRayFromWorld(btCollisionWorld_AllHitsRayResultCallback* obj, btScalar* value);
	EXPORT void btCollisionWorld_AllHitsRayResultCallback_getRayToWorld(btCollisionWorld_AllHitsRayResultCallback* obj, btScalar* value);
	EXPORT void btCollisionWorld_AllHitsRayResultCallback_setRayFromWorld(btCollisionWorld_AllHitsRayResultCallback* obj, const btScalar* value);
	EXPORT void btCollisionWorld_AllHitsRayResultCallback_setRayToWorld(btCollisionWorld_AllHitsRayResultCallback* obj, const btScalar* value);

	EXPORT btCollisionWorld_ClosestConvexResultCallback* btCollisionWorld_ClosestConvexResultCallback_new(const btScalar* convexFromWorld, const btScalar* convexToWorld, void* managedWrapper);
	EXPORT void btCollisionWorld_ClosestConvexResultCallback_getConvexFromWorld(btCollisionWorld_ClosestConvexResultCallback* obj, btScalar* value);
	EXPORT void btCollisionWorld_ClosestConvexResultCallback_getConvexToWorld(btCollisionWorld_ClosestConvexResultCallback* obj, btScalar* value);
	EXPORT const btCollisionObject* btCollisionWorld_ClosestConvexResultCallback_getHitCollisionObject(btCollisionWorld_ClosestConvexResultCallback* obj);
	EXPORT void btCollisionWorld_ClosestConvexResultCallback_getHitNormalWorld(btCollisionWorld_ClosestConvexResultCallback* obj, btScalar* value);
	EXPORT void btCollisionWorld_ClosestConvexResultCallback_getHitPointWorld(btCollisionWorld_ClosestConvexResultCallback* obj, btScalar* value);
	EXPORT void btCollisionWorld_ClosestConvexResultCallback_setConvexFromWorld(btCollisionWorld_ClosestConvexResultCallback* obj, const btScalar* value);
	EXPORT void btCollisionWorld_ClosestConvexResultCallback_setConvexToWorld(btCollisionWorld_ClosestConvexResultCallback* obj, const btScalar* value);
	EXPORT void btCollisionWorld_ClosestConvexResultCallback_setHitCollisionObject(btCollisionWorld_ClosestConvexResultCallback* obj, const btCollisionObject* value);
	EXPORT void btCollisionWorld_ClosestConvexResultCallback_setHitNormalWorld(btCollisionWorld_ClosestConvexResultCallback* obj, const btScalar* value);
	EXPORT void btCollisionWorld_ClosestConvexResultCallback_setHitPointWorld(btCollisionWorld_ClosestConvexResultCallback* obj, const btScalar* value);

	EXPORT btCollisionWorld_ClosestRayResultCallback* btCollisionWorld_ClosestRayResultCallback_new(const btScalar* rayFromWorld, const btScalar* rayToWorld, void* managedWrapper);
	EXPORT void btCollisionWorld_ClosestRayResultCallback_getHitNormalWorld(btCollisionWorld_ClosestRayResultCallback* obj, btScalar* value);
	EXPORT void btCollisionWorld_ClosestRayResultCallback_getHitPointWorld(btCollisionWorld_ClosestRayResultCallback* obj, btScalar* value);
	EXPORT void btCollisionWorld_ClosestRayResultCallback_getRayFromWorld(btCollisionWorld_ClosestRayResultCallback* obj, btScalar* value);
	EXPORT void btCollisionWorld_ClosestRayResultCallback_getRayToWorld(btCollisionWorld_ClosestRayResultCallback* obj, btScalar* value);
	EXPORT void btCollisionWorld_ClosestRayResultCallback_setHitNormalWorld(btCollisionWorld_ClosestRayResultCallback* obj, const btScalar* value);
	EXPORT void btCollisionWorld_ClosestRayResultCallback_setHitPointWorld(btCollisionWorld_ClosestRayResultCallback* obj, const btScalar* value);
	EXPORT void btCollisionWorld_ClosestRayResultCallback_setRayFromWorld(btCollisionWorld_ClosestRayResultCallback* obj, const btScalar* value);
	EXPORT void btCollisionWorld_ClosestRayResultCallback_setRayToWorld(btCollisionWorld_ClosestRayResultCallback* obj, const btScalar* value);

	EXPORT btCollisionWorld_ContactResultCallbackWrapper* btCollisionWorld_ContactResultCallbackWrapper_new(pContactResultCallback_AddSingleResult addSingleResultCallback, pContactResultCallback_NeedsCollision needsCollisionCallback, void* managedWrapper);
	EXPORT bool btCollisionWorld_ContactResultCallbackWrapper_needsCollision(btCollisionWorld_ContactResultCallbackWrapper* obj, btBroadphaseProxy* proxy0);

	EXPORT btScalar btCollisionWorld_ContactResultCallback_addSingleResult(btCollisionWorld_ContactResultCallback* obj, btManifoldPoint* cp, const btCollisionObjectWrapper* colObj0Wrap, int partId0, int index0, const btCollisionObjectWrapper* colObj1Wrap, int partId1, int index1);
	EXPORT short btCollisionWorld_ContactResultCallback_getCollisionFilterGroup(btCollisionWorld_ContactResultCallback* obj);
	EXPORT short btCollisionWorld_ContactResultCallback_getCollisionFilterMask(btCollisionWorld_ContactResultCallback* obj);
	EXPORT bool btCollisionWorld_ContactResultCallback_needsCollision(btCollisionWorld_ContactResultCallback* obj, btBroadphaseProxy* proxy0);
	EXPORT void btCollisionWorld_ContactResultCallback_setCollisionFilterGroup(btCollisionWorld_ContactResultCallback* obj, short value);
	EXPORT void btCollisionWorld_ContactResultCallback_setCollisionFilterMask(btCollisionWorld_ContactResultCallback* obj, short value);
	EXPORT void btCollisionWorld_ContactResultCallback_delete(btCollisionWorld_ContactResultCallback* obj);

	EXPORT btCollisionWorld_ConvexResultCallbackWrapper* btCollisionWorld_ConvexResultCallbackWrapper_new(pConvexResultCallback_AddSingleResult addSingleResultCallback, pConvexResultCallback_NeedsCollision needsCollisionCallback, void* managedWrapper);
	EXPORT bool btCollisionWorld_ConvexResultCallbackWrapper_needsCollision(btCollisionWorld_ConvexResultCallbackWrapper* obj, btBroadphaseProxy* proxy0);

	EXPORT btScalar btCollisionWorld_ConvexResultCallback_addSingleResult(btCollisionWorld_ConvexResultCallback* obj, btCollisionWorld_LocalConvexResult* convexResult, bool normalInWorldSpace);
	EXPORT btScalar btCollisionWorld_ConvexResultCallback_getClosestHitFraction(btCollisionWorld_ConvexResultCallback* obj);
	EXPORT short btCollisionWorld_ConvexResultCallback_getCollisionFilterGroup(btCollisionWorld_ConvexResultCallback* obj);
	EXPORT short btCollisionWorld_ConvexResultCallback_getCollisionFilterMask(btCollisionWorld_ConvexResultCallback* obj);
	EXPORT bool btCollisionWorld_ConvexResultCallback_hasHit(btCollisionWorld_ConvexResultCallback* obj);
	EXPORT bool btCollisionWorld_ConvexResultCallback_needsCollision(btCollisionWorld_ConvexResultCallback* obj, btBroadphaseProxy* proxy0);
	EXPORT void btCollisionWorld_ConvexResultCallback_setClosestHitFraction(btCollisionWorld_ConvexResultCallback* obj, btScalar value);
	EXPORT void btCollisionWorld_ConvexResultCallback_setCollisionFilterGroup(btCollisionWorld_ConvexResultCallback* obj, short value);
	EXPORT void btCollisionWorld_ConvexResultCallback_setCollisionFilterMask(btCollisionWorld_ConvexResultCallback* obj, short value);
	EXPORT void btCollisionWorld_ConvexResultCallback_delete(btCollisionWorld_ConvexResultCallback* obj);

	EXPORT btCollisionWorld_LocalConvexResult* btCollisionWorld_LocalConvexResult_new(const btCollisionObject* hitCollisionObject, btCollisionWorld_LocalShapeInfo* localShapeInfo, const btScalar* hitNormalLocal, const btScalar* hitPointLocal, btScalar hitFraction);
	EXPORT const btCollisionObject* btCollisionWorld_LocalConvexResult_getHitCollisionObject(btCollisionWorld_LocalConvexResult* obj);
	EXPORT btScalar btCollisionWorld_LocalConvexResult_getHitFraction(btCollisionWorld_LocalConvexResult* obj);
	EXPORT void btCollisionWorld_LocalConvexResult_getHitNormalLocal(btCollisionWorld_LocalConvexResult* obj, btScalar* value);
	EXPORT void btCollisionWorld_LocalConvexResult_getHitPointLocal(btCollisionWorld_LocalConvexResult* obj, btScalar* value);
	EXPORT btCollisionWorld_LocalShapeInfo* btCollisionWorld_LocalConvexResult_getLocalShapeInfo(btCollisionWorld_LocalConvexResult* obj);
	EXPORT void btCollisionWorld_LocalConvexResult_setHitCollisionObject(btCollisionWorld_LocalConvexResult* obj, const btCollisionObject* value);
	EXPORT void btCollisionWorld_LocalConvexResult_setHitFraction(btCollisionWorld_LocalConvexResult* obj, btScalar value);
	EXPORT void btCollisionWorld_LocalConvexResult_setHitNormalLocal(btCollisionWorld_LocalConvexResult* obj, const btScalar* value);
	EXPORT void btCollisionWorld_LocalConvexResult_setHitPointLocal(btCollisionWorld_LocalConvexResult* obj, const btScalar* value);
	EXPORT void btCollisionWorld_LocalConvexResult_setLocalShapeInfo(btCollisionWorld_LocalConvexResult* obj, btCollisionWorld_LocalShapeInfo* value);
	EXPORT void btCollisionWorld_LocalConvexResult_delete(btCollisionWorld_LocalConvexResult* obj);

	EXPORT btCollisionWorld_LocalRayResult* btCollisionWorld_LocalRayResult_new(const btCollisionObject* collisionObject, btCollisionWorld_LocalShapeInfo* localShapeInfo, const btScalar* hitNormalLocal, btScalar hitFraction);
	EXPORT const btCollisionObject* btCollisionWorld_LocalRayResult_getCollisionObject(btCollisionWorld_LocalRayResult* obj);
	EXPORT btScalar btCollisionWorld_LocalRayResult_getHitFraction(btCollisionWorld_LocalRayResult* obj);
	EXPORT void btCollisionWorld_LocalRayResult_getHitNormalLocal(btCollisionWorld_LocalRayResult* obj, btScalar* value);
	EXPORT btCollisionWorld_LocalShapeInfo* btCollisionWorld_LocalRayResult_getLocalShapeInfo(btCollisionWorld_LocalRayResult* obj);
	EXPORT void btCollisionWorld_LocalRayResult_setCollisionObject(btCollisionWorld_LocalRayResult* obj, const btCollisionObject* value);
	EXPORT void btCollisionWorld_LocalRayResult_setHitFraction(btCollisionWorld_LocalRayResult* obj, btScalar value);
	EXPORT void btCollisionWorld_LocalRayResult_setHitNormalLocal(btCollisionWorld_LocalRayResult* obj, const btScalar* value);
	EXPORT void btCollisionWorld_LocalRayResult_setLocalShapeInfo(btCollisionWorld_LocalRayResult* obj, btCollisionWorld_LocalShapeInfo* value);
	EXPORT void btCollisionWorld_LocalRayResult_delete(btCollisionWorld_LocalRayResult* obj);

	EXPORT btCollisionWorld_LocalShapeInfo* btCollisionWorld_LocalShapeInfo_new();
	EXPORT int btCollisionWorld_LocalShapeInfo_getShapePart(btCollisionWorld_LocalShapeInfo* obj);
	EXPORT int btCollisionWorld_LocalShapeInfo_getTriangleIndex(btCollisionWorld_LocalShapeInfo* obj);
	EXPORT void btCollisionWorld_LocalShapeInfo_setShapePart(btCollisionWorld_LocalShapeInfo* obj, int value);
	EXPORT void btCollisionWorld_LocalShapeInfo_setTriangleIndex(btCollisionWorld_LocalShapeInfo* obj, int value);
	EXPORT void btCollisionWorld_LocalShapeInfo_delete(btCollisionWorld_LocalShapeInfo* obj);

	EXPORT btCollisionWorld_RayResultCallbackWrapper* btCollisionWorld_RayResultCallbackWrapper_new(pRayResultCallback_AddSingleResult addSingleResultCallback, pRayResultCallback_NeedsCollision needsCollisionCallback, void* managedWrapper);
	EXPORT bool btCollisionWorld_RayResultCallbackWrapper_needsCollision(btCollisionWorld_RayResultCallbackWrapper* obj, btBroadphaseProxy* proxy0);

	EXPORT btScalar btCollisionWorld_RayResultCallback_addSingleResult(btCollisionWorld_RayResultCallback* obj, btCollisionWorld_LocalRayResult* rayResult, bool normalInWorldSpace);
	EXPORT btScalar btCollisionWorld_RayResultCallback_getClosestHitFraction(btCollisionWorld_RayResultCallback* obj);
	EXPORT short btCollisionWorld_RayResultCallback_getCollisionFilterGroup(btCollisionWorld_RayResultCallback* obj);
	EXPORT short btCollisionWorld_RayResultCallback_getCollisionFilterMask(btCollisionWorld_RayResultCallback* obj);
	EXPORT const btCollisionObject* btCollisionWorld_RayResultCallback_getCollisionObject(btCollisionWorld_RayResultCallback* obj);
	EXPORT unsigned int btCollisionWorld_RayResultCallback_getFlags(btCollisionWorld_RayResultCallback* obj);
	EXPORT bool btCollisionWorld_RayResultCallback_hasHit(btCollisionWorld_RayResultCallback* obj);
	EXPORT bool btCollisionWorld_RayResultCallback_needsCollision(btCollisionWorld_RayResultCallback* obj, btBroadphaseProxy* proxy0);
	EXPORT void btCollisionWorld_RayResultCallback_setClosestHitFraction(btCollisionWorld_RayResultCallback* obj, btScalar value);
	EXPORT void btCollisionWorld_RayResultCallback_setCollisionFilterGroup(btCollisionWorld_RayResultCallback* obj, short value);
	EXPORT void btCollisionWorld_RayResultCallback_setCollisionFilterMask(btCollisionWorld_RayResultCallback* obj, short value);
	EXPORT void btCollisionWorld_RayResultCallback_setCollisionObject(btCollisionWorld_RayResultCallback* obj, const btCollisionObject* value);
	EXPORT void btCollisionWorld_RayResultCallback_setFlags(btCollisionWorld_RayResultCallback* obj, unsigned int value);
	EXPORT void btCollisionWorld_RayResultCallback_delete(btCollisionWorld_RayResultCallback* obj);

	EXPORT btCollisionWorld* btCollisionWorld_new(btDispatcher* dispatcher, btBroadphaseInterface* broadphasePairCache, btCollisionConfiguration* collisionConfiguration);
	EXPORT void btCollisionWorld_addCollisionObject(btCollisionWorld* obj, btCollisionObject* collisionObject);
	EXPORT void btCollisionWorld_addCollisionObject2(btCollisionWorld* obj, btCollisionObject* collisionObject, short collisionFilterGroup);
	EXPORT void btCollisionWorld_addCollisionObject3(btCollisionWorld* obj, btCollisionObject* collisionObject, short collisionFilterGroup, short collisionFilterMask);
	EXPORT void btCollisionWorld_computeOverlappingPairs(btCollisionWorld* obj);
	EXPORT void btCollisionWorld_contactPairTest(btCollisionWorld* obj, btCollisionObject* colObjA, btCollisionObject* colObjB, btCollisionWorld_ContactResultCallback* resultCallback);
	EXPORT void btCollisionWorld_contactTest(btCollisionWorld* obj, btCollisionObject* colObj, btCollisionWorld_ContactResultCallback* resultCallback);
	EXPORT void btCollisionWorld_convexSweepTest(btCollisionWorld* obj, const btConvexShape* castShape, const btScalar* from, const btScalar* to, btCollisionWorld_ConvexResultCallback* resultCallback);
	EXPORT void btCollisionWorld_convexSweepTest2(btCollisionWorld* obj, const btConvexShape* castShape, const btScalar* from, const btScalar* to, btCollisionWorld_ConvexResultCallback* resultCallback, btScalar allowedCcdPenetration);
	EXPORT void btCollisionWorld_debugDrawObject(btCollisionWorld* obj, const btScalar* worldTransform, const btCollisionShape* shape, const btScalar* color);
	EXPORT void btCollisionWorld_debugDrawWorld(btCollisionWorld* obj);
	EXPORT btBroadphaseInterface* btCollisionWorld_getBroadphase(btCollisionWorld* obj);
	EXPORT const btCollisionObjectArray* btCollisionWorld_getCollisionObjectArray(btCollisionWorld* obj);
	EXPORT btIDebugDraw* btCollisionWorld_getDebugDrawer(btCollisionWorld* obj);
	EXPORT const btDispatcher* btCollisionWorld_getDispatcher(btCollisionWorld* obj);
	EXPORT const btDispatcherInfo* btCollisionWorld_getDispatchInfo(btCollisionWorld* obj);
	EXPORT bool btCollisionWorld_getForceUpdateAllAabbs(btCollisionWorld* obj);
	EXPORT int btCollisionWorld_getNumCollisionObjects(btCollisionWorld* obj);
	EXPORT btOverlappingPairCache* btCollisionWorld_getPairCache(btCollisionWorld* obj);
	EXPORT void btCollisionWorld_objectQuerySingle(const btConvexShape* castShape, const btScalar* rayFromTrans, const btScalar* rayToTrans, btCollisionObject* collisionObject, const btCollisionShape* collisionShape, const btScalar* colObjWorldTransform, btCollisionWorld_ConvexResultCallback* resultCallback, btScalar allowedPenetration);
	EXPORT void btCollisionWorld_objectQuerySingleInternal(const btConvexShape* castShape, const btScalar* convexFromTrans, const btScalar* convexToTrans, const btCollisionObjectWrapper* colObjWrap, btCollisionWorld_ConvexResultCallback* resultCallback, btScalar allowedPenetration);
	EXPORT void btCollisionWorld_performDiscreteCollisionDetection(btCollisionWorld* obj);
	EXPORT void btCollisionWorld_rayTest(btCollisionWorld* obj, const btScalar* rayFromWorld, const btScalar* rayToWorld, btCollisionWorld_RayResultCallback* resultCallback);
	EXPORT void btCollisionWorld_rayTestSingle(const btScalar* rayFromTrans, const btScalar* rayToTrans, btCollisionObject* collisionObject, const btCollisionShape* collisionShape, const btScalar* colObjWorldTransform, btCollisionWorld_RayResultCallback* resultCallback);
	EXPORT void btCollisionWorld_rayTestSingleInternal(const btScalar* rayFromTrans, const btScalar* rayToTrans, const btCollisionObjectWrapper* collisionObjectWrap, btCollisionWorld_RayResultCallback* resultCallback);
	EXPORT void btCollisionWorld_removeCollisionObject(btCollisionWorld* obj, btCollisionObject* collisionObject);
	EXPORT void btCollisionWorld_serialize(btCollisionWorld* obj, btSerializer* serializer);
	EXPORT void btCollisionWorld_setBroadphase(btCollisionWorld* obj, btBroadphaseInterface* pairCache);
	EXPORT void btCollisionWorld_setDebugDrawer(btCollisionWorld* obj, btIDebugDraw* debugDrawer);
	EXPORT void btCollisionWorld_setForceUpdateAllAabbs(btCollisionWorld* obj, bool forceUpdateAllAabbs);
	EXPORT void btCollisionWorld_updateAabbs(btCollisionWorld* obj);
	EXPORT void btCollisionWorld_updateSingleAabb(btCollisionWorld* obj, btCollisionObject* colObj);
	EXPORT void btCollisionWorld_delete(btCollisionWorld* obj);
}
