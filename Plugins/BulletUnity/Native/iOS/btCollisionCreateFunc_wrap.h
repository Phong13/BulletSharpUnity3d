#include "main.h"

extern "C"
{
	EXPORT btCollisionAlgorithmCreateFunc* btCollisionAlgorithmCreateFunc_new();
	EXPORT btCollisionAlgorithm* btCollisionAlgorithmCreateFunc_CreateCollisionAlgorithm(btCollisionAlgorithmCreateFunc* obj, btCollisionAlgorithmConstructionInfo* __unnamed0, const btCollisionObjectWrapper* body0Wrap, const btCollisionObjectWrapper* body1Wrap);
	EXPORT bool btCollisionAlgorithmCreateFunc_getSwapped(btCollisionAlgorithmCreateFunc* obj);
	EXPORT void btCollisionAlgorithmCreateFunc_setSwapped(btCollisionAlgorithmCreateFunc* obj, bool value);
	EXPORT void btCollisionAlgorithmCreateFunc_delete(btCollisionAlgorithmCreateFunc* obj);
}
