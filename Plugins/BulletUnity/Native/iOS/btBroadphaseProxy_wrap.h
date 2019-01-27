#include "main.h"

extern "C"
{
	EXPORT btBroadphaseProxy* btBroadphaseProxy_new();
	EXPORT btBroadphaseProxy* btBroadphaseProxy_new2(const btScalar* aabbMin, const btScalar* aabbMax, void* userPtr, short collisionFilterGroup, short collisionFilterMask);
	EXPORT btBroadphaseProxy* btBroadphaseProxy_new3(const btScalar* aabbMin, const btScalar* aabbMax, void* userPtr, short collisionFilterGroup, short collisionFilterMask, void* multiSapParentProxy);
	EXPORT void btBroadphaseProxy_getAabbMax(btBroadphaseProxy* obj, btScalar* value);
	EXPORT void btBroadphaseProxy_getAabbMin(btBroadphaseProxy* obj, btScalar* value);
	EXPORT void* btBroadphaseProxy_getClientObject(btBroadphaseProxy* obj);
	EXPORT short btBroadphaseProxy_getCollisionFilterGroup(btBroadphaseProxy* obj);
	EXPORT short btBroadphaseProxy_getCollisionFilterMask(btBroadphaseProxy* obj);
	EXPORT void* btBroadphaseProxy_getMultiSapParentProxy(btBroadphaseProxy* obj);
	EXPORT int btBroadphaseProxy_getUid(btBroadphaseProxy* obj);
	EXPORT int btBroadphaseProxy_getUniqueId(btBroadphaseProxy* obj);
	EXPORT bool btBroadphaseProxy_isCompound(int proxyType);
	EXPORT bool btBroadphaseProxy_isConcave(int proxyType);
	EXPORT bool btBroadphaseProxy_isConvex(int proxyType);
	EXPORT bool btBroadphaseProxy_isConvex2d(int proxyType);
	EXPORT bool btBroadphaseProxy_isInfinite(int proxyType);
	EXPORT bool btBroadphaseProxy_isNonMoving(int proxyType);
	EXPORT bool btBroadphaseProxy_isPolyhedral(int proxyType);
	EXPORT bool btBroadphaseProxy_isSoftBody(int proxyType);
	EXPORT void btBroadphaseProxy_setAabbMax(btBroadphaseProxy* obj, const btScalar* value);
	EXPORT void btBroadphaseProxy_setAabbMin(btBroadphaseProxy* obj, const btScalar* value);
	EXPORT void btBroadphaseProxy_setClientObject(btBroadphaseProxy* obj, void* value);
	EXPORT void btBroadphaseProxy_setCollisionFilterGroup(btBroadphaseProxy* obj, short value);
	EXPORT void btBroadphaseProxy_setCollisionFilterMask(btBroadphaseProxy* obj, short value);
	EXPORT void btBroadphaseProxy_setMultiSapParentProxy(btBroadphaseProxy* obj, void* value);
	EXPORT void btBroadphaseProxy_setUniqueId(btBroadphaseProxy* obj, int value);
	EXPORT void btBroadphaseProxy_delete(btBroadphaseProxy* obj);

	EXPORT btBroadphasePair* btBroadphasePair_new();
	EXPORT btBroadphasePair* btBroadphasePair_new2(const btBroadphasePair* other);
	EXPORT btBroadphasePair* btBroadphasePair_new3(btBroadphaseProxy* proxy0, btBroadphaseProxy* proxy1);
	EXPORT btCollisionAlgorithm* btBroadphasePair_getAlgorithm(btBroadphasePair* obj);
	EXPORT btBroadphaseProxy* btBroadphasePair_getPProxy0(btBroadphasePair* obj);
	EXPORT btBroadphaseProxy* btBroadphasePair_getPProxy1(btBroadphasePair* obj);
	EXPORT void btBroadphasePair_setAlgorithm(btBroadphasePair* obj, btCollisionAlgorithm* value);
	EXPORT void btBroadphasePair_setPProxy0(btBroadphasePair* obj, btBroadphaseProxy* value);
	EXPORT void btBroadphasePair_setPProxy1(btBroadphasePair* obj, btBroadphaseProxy* value);
	EXPORT void btBroadphasePair_delete(btBroadphasePair* obj);
}
