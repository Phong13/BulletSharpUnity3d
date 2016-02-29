#include "main.h"

extern "C"
{
	EXPORT btCollisionDispatcher* btCollisionDispatcher_new(btCollisionConfiguration* collisionConfiguration);
	EXPORT void btCollisionDispatcher_defaultNearCallback(btBroadphasePair* collisionPair, btCollisionDispatcher* dispatcher, const btDispatcherInfo* dispatchInfo);
	EXPORT const btCollisionConfiguration* btCollisionDispatcher_getCollisionConfiguration(btCollisionDispatcher* obj);
	EXPORT int btCollisionDispatcher_getDispatcherFlags(btCollisionDispatcher* obj);
	EXPORT btNearCallback btCollisionDispatcher_getNearCallback(btCollisionDispatcher* obj);
	EXPORT void btCollisionDispatcher_registerCollisionCreateFunc(btCollisionDispatcher* obj, int proxyType0, int proxyType1, btCollisionAlgorithmCreateFunc* createFunc);
	EXPORT void btCollisionDispatcher_setCollisionConfiguration(btCollisionDispatcher* obj, btCollisionConfiguration* config);
	EXPORT void btCollisionDispatcher_setDispatcherFlags(btCollisionDispatcher* obj, int flags);
	EXPORT void btCollisionDispatcher_setNearCallback(btCollisionDispatcher* obj, btNearCallback nearCallback);
}
