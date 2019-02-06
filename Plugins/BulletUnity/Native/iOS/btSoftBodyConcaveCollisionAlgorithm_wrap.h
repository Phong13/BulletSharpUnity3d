#include "main.h"

extern "C"
{
	EXPORT btSoftBodyConcaveCollisionAlgorithm_CreateFunc* btSoftBodyConcaveCollisionAlgorithm_CreateFunc_new();

	EXPORT btSoftBodyConcaveCollisionAlgorithm_SwappedCreateFunc* btSoftBodyConcaveCollisionAlgorithm_SwappedCreateFunc_new();

	EXPORT btSoftBodyConcaveCollisionAlgorithm* btSoftBodyConcaveCollisionAlgorithm_new(const btCollisionAlgorithmConstructionInfo* ci, const btCollisionObjectWrapper* body0Wrap, const btCollisionObjectWrapper* body1Wrap, bool isSwapped);
	EXPORT void btSoftBodyConcaveCollisionAlgorithm_clearCache(btSoftBodyConcaveCollisionAlgorithm* obj);
}
