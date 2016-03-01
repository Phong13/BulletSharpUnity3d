#include "main.h"

extern "C"
{
	EXPORT btSoftRigidCollisionAlgorithm_CreateFunc* btSoftRigidCollisionAlgorithm_CreateFunc_new();

	EXPORT btSoftRigidCollisionAlgorithm* btSoftRigidCollisionAlgorithm_new(btPersistentManifold* mf, const btCollisionAlgorithmConstructionInfo* ci, const btCollisionObjectWrapper* col0, const btCollisionObjectWrapper* col1Wrap, bool isSwapped);
}
