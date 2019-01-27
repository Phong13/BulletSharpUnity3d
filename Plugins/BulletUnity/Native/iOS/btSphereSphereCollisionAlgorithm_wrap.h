#include "main.h"

extern "C"
{
	EXPORT btSphereSphereCollisionAlgorithm_CreateFunc* btSphereSphereCollisionAlgorithm_CreateFunc_new();

	EXPORT btSphereSphereCollisionAlgorithm* btSphereSphereCollisionAlgorithm_new(btPersistentManifold* mf, const btCollisionAlgorithmConstructionInfo* ci, const btCollisionObjectWrapper* col0Wrap, const btCollisionObjectWrapper* col1Wrap);
	EXPORT btSphereSphereCollisionAlgorithm* btSphereSphereCollisionAlgorithm_new2(const btCollisionAlgorithmConstructionInfo* ci);
}
