#include "main.h"

extern "C"
{
	EXPORT btBoxBoxCollisionAlgorithm_CreateFunc* btBoxBoxCollisionAlgorithm_CreateFunc_new();

	EXPORT btBoxBoxCollisionAlgorithm* btBoxBoxCollisionAlgorithm_new(const btCollisionAlgorithmConstructionInfo* ci);
	EXPORT btBoxBoxCollisionAlgorithm* btBoxBoxCollisionAlgorithm_new2(btPersistentManifold* mf, const btCollisionAlgorithmConstructionInfo* ci, const btCollisionObjectWrapper* body0Wrap, const btCollisionObjectWrapper* body1Wrap);
}
