#include "main.h"

extern "C"
{
	EXPORT void btTransformUtil_calculateDiffAxisAngle(const btScalar* transform0, const btScalar* transform1, btScalar* axis, btScalar* angle);
	EXPORT void btTransformUtil_calculateDiffAxisAngleQuaternion(const btScalar* orn0, const btScalar* orn1a, btScalar* axis, btScalar* angle);
	EXPORT void btTransformUtil_calculateVelocity(const btScalar* transform0, const btScalar* transform1, btScalar timeStep, btScalar* linVel, btScalar* angVel);
	EXPORT void btTransformUtil_calculateVelocityQuaternion(const btScalar* pos0, const btScalar* pos1, const btScalar* orn0, const btScalar* orn1, btScalar timeStep, btScalar* linVel, btScalar* angVel);
	EXPORT void btTransformUtil_integrateTransform(const btScalar* curTrans, const btScalar* linvel, const btScalar* angvel, btScalar timeStep, btScalar* predictedTransform);

	EXPORT btConvexSeparatingDistanceUtil* btConvexSeparatingDistanceUtil_new(btScalar boundingRadiusA, btScalar boundingRadiusB);
	EXPORT btScalar btConvexSeparatingDistanceUtil_getConservativeSeparatingDistance(btConvexSeparatingDistanceUtil* obj);
	EXPORT void btConvexSeparatingDistanceUtil_initSeparatingDistance(btConvexSeparatingDistanceUtil* obj, const btScalar* separatingVector, btScalar separatingDistance, const btScalar* transA, const btScalar* transB);
	EXPORT void btConvexSeparatingDistanceUtil_updateSeparatingDistance(btConvexSeparatingDistanceUtil* obj, const btScalar* transA, const btScalar* transB);
	EXPORT void btConvexSeparatingDistanceUtil_delete(btConvexSeparatingDistanceUtil* obj);
}
