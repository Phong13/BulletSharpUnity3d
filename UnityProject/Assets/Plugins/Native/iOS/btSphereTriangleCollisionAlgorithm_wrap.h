#include "main.h"

extern "C"
{
	EXPORT btSphereTriangleCollisionAlgorithm_CreateFunc* btSphereTriangleCollisionAlgorithm_CreateFunc_new();

	EXPORT btSphereTriangleCollisionAlgorithm* btSphereTriangleCollisionAlgorithm_new(btPersistentManifold* mf, const btCollisionAlgorithmConstructionInfo* ci, const btCollisionObjectWrapper* body0Wrap, const btCollisionObjectWrapper* body1Wrap, bool swapped);
	EXPORT btSphereTriangleCollisionAlgorithm* btSphereTriangleCollisionAlgorithm_new2(const btCollisionAlgorithmConstructionInfo* ci);
}
