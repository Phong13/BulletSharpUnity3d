#include "main.h"

extern "C"
{
	EXPORT btCollisionAlgorithmCreateFunc* btCollisionConfiguration_getCollisionAlgorithmCreateFunc(btCollisionConfiguration* obj, int proxyType0, int proxyType1);
	EXPORT btPoolAllocator* btCollisionConfiguration_getCollisionAlgorithmPool(btCollisionConfiguration* obj);
	EXPORT btPoolAllocator* btCollisionConfiguration_getPersistentManifoldPool(btCollisionConfiguration* obj);
	EXPORT void btCollisionConfiguration_delete(btCollisionConfiguration* obj);
}
