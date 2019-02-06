#include "main.h"

extern "C"
{
	EXPORT btSphereBoxCollisionAlgorithm_CreateFunc* btSphereBoxCollisionAlgorithm_CreateFunc_new();

	EXPORT btSphereBoxCollisionAlgorithm* btSphereBoxCollisionAlgorithm_new(btPersistentManifold* mf, const btCollisionAlgorithmConstructionInfo* ci, const btCollisionObjectWrapper* body0Wrap, const btCollisionObjectWrapper* body1Wrap, bool isSwapped);
	EXPORT bool btSphereBoxCollisionAlgorithm_getSphereDistance(btSphereBoxCollisionAlgorithm* obj, const btCollisionObjectWrapper* boxObjWrap, btScalar* v3PointOnBox, btScalar* normal, btScalar* penetrationDepth, const btScalar* v3SphereCenter, btScalar fRadius, btScalar maxContactDistance);
	EXPORT btScalar btSphereBoxCollisionAlgorithm_getSpherePenetration(btSphereBoxCollisionAlgorithm* obj, const btScalar* boxHalfExtent, const btScalar* sphereRelPos, btScalar* closestPoint, btScalar* normal);
}
