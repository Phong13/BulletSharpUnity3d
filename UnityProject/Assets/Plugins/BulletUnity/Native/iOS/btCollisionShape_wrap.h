#include "main.h"

extern "C"
{
	EXPORT void btCollisionShape_calculateLocalInertia(btCollisionShape* obj, btScalar mass, btScalar* inertia);
	EXPORT int btCollisionShape_calculateSerializeBufferSize(btCollisionShape* obj);
	EXPORT void btCollisionShape_calculateTemporalAabb(btCollisionShape* obj, const btScalar* curTrans, const btScalar* linvel, const btScalar* angvel, btScalar timeStep, btScalar* temporalAabbMin, btScalar* temporalAabbMax);
	EXPORT void btCollisionShape_getAabb(btCollisionShape* obj, const btScalar* t, btScalar* aabbMin, btScalar* aabbMax);
	EXPORT btScalar btCollisionShape_getAngularMotionDisc(btCollisionShape* obj);
	EXPORT void btCollisionShape_getAnisotropicRollingFrictionDirection(btCollisionShape* obj, btScalar* value);
	EXPORT void btCollisionShape_getBoundingSphere(btCollisionShape* obj, btScalar* center, btScalar* radius);
	EXPORT btScalar btCollisionShape_getContactBreakingThreshold(btCollisionShape* obj, btScalar defaultContactThresholdFactor);
	EXPORT void btCollisionShape_getLocalScaling(btCollisionShape* obj, btScalar* scaling);
	EXPORT btScalar btCollisionShape_getMargin(btCollisionShape* obj);
	EXPORT const char* btCollisionShape_getName(btCollisionShape* obj);
	EXPORT int btCollisionShape_getShapeType(btCollisionShape* obj);
	EXPORT int btCollisionShape_getUserIndex(btCollisionShape* obj);
	EXPORT void* btCollisionShape_getUserPointer(btCollisionShape* obj);
	EXPORT bool btCollisionShape_isCompound(btCollisionShape* obj);
	EXPORT bool btCollisionShape_isConcave(btCollisionShape* obj);
	EXPORT bool btCollisionShape_isConvex(btCollisionShape* obj);
	EXPORT bool btCollisionShape_isConvex2d(btCollisionShape* obj);
	EXPORT bool btCollisionShape_isInfinite(btCollisionShape* obj);
	EXPORT bool btCollisionShape_isNonMoving(btCollisionShape* obj);
	EXPORT bool btCollisionShape_isPolyhedral(btCollisionShape* obj);
	EXPORT bool btCollisionShape_isSoftBody(btCollisionShape* obj);
	EXPORT const char* btCollisionShape_serialize(btCollisionShape* obj, void* dataBuffer, btSerializer* serializer);
	EXPORT void btCollisionShape_serializeSingleShape(btCollisionShape* obj, btSerializer* serializer);
	EXPORT void btCollisionShape_setLocalScaling(btCollisionShape* obj, const btScalar* scaling);
	EXPORT void btCollisionShape_setMargin(btCollisionShape* obj, btScalar margin);
	EXPORT void btCollisionShape_setUserIndex(btCollisionShape* obj, int index);
	EXPORT void btCollisionShape_setUserPointer(btCollisionShape* obj, void* userPtr);
	EXPORT void btCollisionShape_delete(btCollisionShape* obj);
}
