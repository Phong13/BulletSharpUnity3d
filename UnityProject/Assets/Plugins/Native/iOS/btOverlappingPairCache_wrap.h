#include "main.h"

extern "C"
{
	EXPORT bool btOverlapCallback_processOverlap(btOverlapCallback* obj, btBroadphasePair* pair);
	EXPORT void btOverlapCallback_delete(btOverlapCallback* obj);

	EXPORT bool btOverlapFilterCallback_needBroadphaseCollision(btOverlapFilterCallback* obj, btBroadphaseProxy* proxy0, btBroadphaseProxy* proxy1);
	EXPORT void btOverlapFilterCallback_delete(btOverlapFilterCallback* obj);

	EXPORT void btOverlappingPairCache_cleanOverlappingPair(btOverlappingPairCache* obj, btBroadphasePair* pair, btDispatcher* dispatcher);
	EXPORT void btOverlappingPairCache_cleanProxyFromPairs(btOverlappingPairCache* obj, btBroadphaseProxy* proxy, btDispatcher* dispatcher);
	EXPORT btBroadphasePair* btOverlappingPairCache_findPair(btOverlappingPairCache* obj, btBroadphaseProxy* proxy0, btBroadphaseProxy* proxy1);
	EXPORT int btOverlappingPairCache_getNumOverlappingPairs(btOverlappingPairCache* obj);
	EXPORT btAlignedBroadphasePairArray* btOverlappingPairCache_getOverlappingPairArray(btOverlappingPairCache* obj);
	EXPORT btBroadphasePair* btOverlappingPairCache_getOverlappingPairArrayPtr(btOverlappingPairCache* obj);
	EXPORT bool btOverlappingPairCache_hasDeferredRemoval(btOverlappingPairCache* obj);
	EXPORT void btOverlappingPairCache_processAllOverlappingPairs(btOverlappingPairCache* obj, btOverlapCallback* __unnamed0, btDispatcher* dispatcher);
	EXPORT void btOverlappingPairCache_setInternalGhostPairCallback(btOverlappingPairCache* obj, btOverlappingPairCallback* ghostPairCallback);
	EXPORT void btOverlappingPairCache_setOverlapFilterCallback(btOverlappingPairCache* obj, btOverlapFilterCallback* callback);
	EXPORT void btOverlappingPairCache_sortOverlappingPairs(btOverlappingPairCache* obj, btDispatcher* dispatcher);

	EXPORT btHashedOverlappingPairCache* btHashedOverlappingPairCache_new();
	EXPORT int btHashedOverlappingPairCache_GetCount(btHashedOverlappingPairCache* obj);
	EXPORT btOverlapFilterCallback* btHashedOverlappingPairCache_getOverlapFilterCallback(btHashedOverlappingPairCache* obj);
	EXPORT bool btHashedOverlappingPairCache_needsBroadphaseCollision(btHashedOverlappingPairCache* obj, btBroadphaseProxy* proxy0, btBroadphaseProxy* proxy1);

	EXPORT btSortedOverlappingPairCache* btSortedOverlappingPairCache_new();
	EXPORT btOverlapFilterCallback* btSortedOverlappingPairCache_getOverlapFilterCallback(btSortedOverlappingPairCache* obj);
	EXPORT bool btSortedOverlappingPairCache_needsBroadphaseCollision(btSortedOverlappingPairCache* obj, btBroadphaseProxy* proxy0, btBroadphaseProxy* proxy1);

	EXPORT btNullPairCache* btNullPairCache_new();
}
