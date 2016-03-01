#include "main.h"

extern "C"
{
	EXPORT const btCollisionObject* btCollisionObjectWrapper_getCollisionObject(btCollisionObjectWrapper* obj);
	EXPORT const btCollisionShape* btCollisionObjectWrapper_getCollisionShape(btCollisionObjectWrapper* obj);
	EXPORT int btCollisionObjectWrapper_getIndex(btCollisionObjectWrapper* obj);
	EXPORT const btCollisionObjectWrapper* btCollisionObjectWrapper_getParent(btCollisionObjectWrapper* obj);
	EXPORT int btCollisionObjectWrapper_getPartId(btCollisionObjectWrapper* obj);
	EXPORT void btCollisionObjectWrapper_getWorldTransform(btCollisionObjectWrapper* obj, btScalar* value);
	EXPORT void btCollisionObjectWrapper_setCollisionObject(btCollisionObjectWrapper* obj, const btCollisionObject* value);
	EXPORT void btCollisionObjectWrapper_setIndex(btCollisionObjectWrapper* obj, int value);
	EXPORT void btCollisionObjectWrapper_setParent(btCollisionObjectWrapper* obj, const btCollisionObjectWrapper* value);
	EXPORT void btCollisionObjectWrapper_setPartId(btCollisionObjectWrapper* obj, int value);
	EXPORT void btCollisionObjectWrapper_setShape(btCollisionObjectWrapper* obj, const btCollisionShape* value);
}
