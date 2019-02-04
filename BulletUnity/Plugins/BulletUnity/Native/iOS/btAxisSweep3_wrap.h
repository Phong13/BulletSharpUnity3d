#include "main.h"

extern "C"
{
	EXPORT btAxisSweep3* btAxisSweep3_new(const btScalar* worldAabbMin, const btScalar* worldAabbMax);
	EXPORT btAxisSweep3* btAxisSweep3_new2(const btScalar* worldAabbMin, const btScalar* worldAabbMax, unsigned short maxHandles);
	EXPORT btAxisSweep3* btAxisSweep3_new3(const btScalar* worldAabbMin, const btScalar* worldAabbMax, unsigned short maxHandles, btOverlappingPairCache* pairCache);
	EXPORT btAxisSweep3* btAxisSweep3_new4(const btScalar* worldAabbMin, const btScalar* worldAabbMax, unsigned short maxHandles, btOverlappingPairCache* pairCache, bool disableRaycastAccelerator);
	EXPORT unsigned short btAxisSweep3_addHandle(btAxisSweep3* obj, const btScalar* aabbMin, const btScalar* aabbMax, void* pOwner, unsigned short collisionFilterGroup, unsigned short collisionFilterMask, btDispatcher* dispatcher, void* multiSapProxy);
	EXPORT btAxisSweep3_Handle* btAxisSweep3_getHandle(btAxisSweep3* obj, unsigned short index);
	EXPORT unsigned short btAxisSweep3_getNumHandles(btAxisSweep3* obj, btOverlapCallback* callback);
	EXPORT btOverlappingPairCallback* btAxisSweep3_getOverlappingPairUserCallback(btAxisSweep3* obj);
	//EXPORT void btAxisSweep3_processAllOverlappingPairs(btAxisSweep3* obj, btOverlapCallback* callback);
	EXPORT void btAxisSweep3_quantize(btAxisSweep3* obj, unsigned short* out, const btScalar* point, int isMax);
	EXPORT void btAxisSweep3_removeHandle(btAxisSweep3* obj, unsigned short handle, btDispatcher* dispatcher);
	EXPORT void btAxisSweep3_setOverlappingPairUserCallback(btAxisSweep3* obj, btOverlappingPairCallback* pairCallback);
	EXPORT bool btAxisSweep3_testAabbOverlap(btAxisSweep3* obj, btBroadphaseProxy* proxy0, btBroadphaseProxy* proxy1);
	EXPORT void btAxisSweep3_unQuantize(btAxisSweep3* obj, btBroadphaseProxy* proxy, btScalar* aabbMin, btScalar* aabbMax);
	EXPORT void btAxisSweep3_updateHandle(btAxisSweep3* obj, unsigned short handle, const btScalar* aabbMin, const btScalar* aabbMax, btDispatcher* dispatcher);

	EXPORT bt32BitAxisSweep3* bt32BitAxisSweep3_new(const btScalar* worldAabbMin, const btScalar* worldAabbMax);
	EXPORT bt32BitAxisSweep3* bt32BitAxisSweep3_new2(const btScalar* worldAabbMin, const btScalar* worldAabbMax, unsigned int maxHandles);
	EXPORT bt32BitAxisSweep3* bt32BitAxisSweep3_new3(const btScalar* worldAabbMin, const btScalar* worldAabbMax, unsigned int maxHandles, btOverlappingPairCache* pairCache);
	EXPORT bt32BitAxisSweep3* bt32BitAxisSweep3_new4(const btScalar* worldAabbMin, const btScalar* worldAabbMax, unsigned int maxHandles, btOverlappingPairCache* pairCache, bool disableRaycastAccelerator);
	EXPORT unsigned int bt32BitAxisSweep3_addHandle(bt32BitAxisSweep3* obj, const btScalar* aabbMin, const btScalar* aabbMax, void* pOwner, unsigned short collisionFilterGroup, unsigned short collisionFilterMask, btDispatcher* dispatcher, void* multiSapProxy);
	EXPORT bt32BitAxisSweep3_Handle* bt32BitAxisSweep3_getHandle(bt32BitAxisSweep3* obj, unsigned int index);
	EXPORT unsigned int bt32BitAxisSweep3_getNumHandles(bt32BitAxisSweep3* obj, btOverlapCallback* callback);
	EXPORT btOverlappingPairCallback* bt32BitAxisSweep3_getOverlappingPairUserCallback(bt32BitAxisSweep3* obj);
	//EXPORT void bt32BitAxisSweep3_processAllOverlappingPairs(bt32BitAxisSweep3* obj, btOverlapCallback* callback);
	EXPORT void bt32BitAxisSweep3_quantize(bt32BitAxisSweep3* obj, unsigned int* out, const btScalar* point, int isMax);
	EXPORT void bt32BitAxisSweep3_removeHandle(bt32BitAxisSweep3* obj, unsigned int handle, btDispatcher* dispatcher);
	EXPORT void bt32BitAxisSweep3_setOverlappingPairUserCallback(bt32BitAxisSweep3* obj, btOverlappingPairCallback* pairCallback);
	EXPORT bool bt32BitAxisSweep3_testAabbOverlap(bt32BitAxisSweep3* obj, btBroadphaseProxy* proxy0, btBroadphaseProxy* proxy1);
	EXPORT void bt32BitAxisSweep3_unQuantize(bt32BitAxisSweep3* obj, btBroadphaseProxy* proxy, btScalar* aabbMin, btScalar* aabbMax);
	EXPORT void bt32BitAxisSweep3_updateHandle(bt32BitAxisSweep3* obj, unsigned int handle, const btScalar* aabbMin, const btScalar* aabbMax, btDispatcher* dispatcher);
}
