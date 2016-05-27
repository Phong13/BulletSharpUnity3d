#include "main.h"

extern "C"
{
	EXPORT btSoftSoftCollisionAlgorithm_CreateFunc* btSoftSoftCollisionAlgorithm_CreateFunc_new();

	EXPORT btSoftSoftCollisionAlgorithm* btSoftSoftCollisionAlgorithm_new(const btCollisionAlgorithmConstructionInfo* ci);
	EXPORT btSoftSoftCollisionAlgorithm* btSoftSoftCollisionAlgorithm_new2(btPersistentManifold* mf, const btCollisionAlgorithmConstructionInfo* ci, const btCollisionObjectWrapper* body0Wrap, const btCollisionObjectWrapper* body1Wrap);
}
