#include "main.h"

extern "C"
{
	EXPORT btScalar btCompoundShapeChild_getChildMargin(btCompoundShapeChild* obj);
	EXPORT btCollisionShape* btCompoundShapeChild_getChildShape(btCompoundShapeChild* obj);
	EXPORT int btCompoundShapeChild_getChildShapeType(btCompoundShapeChild* obj);
	EXPORT btDbvtNode* btCompoundShapeChild_getNode(btCompoundShapeChild* obj);
	EXPORT void btCompoundShapeChild_getTransform(btCompoundShapeChild* obj, btScalar* value);
	EXPORT void btCompoundShapeChild_setChildMargin(btCompoundShapeChild* obj, btScalar value);
	EXPORT void btCompoundShapeChild_setChildShape(btCompoundShapeChild* obj, btCollisionShape* value);
	EXPORT void btCompoundShapeChild_setChildShapeType(btCompoundShapeChild* obj, int value);
	EXPORT void btCompoundShapeChild_setNode(btCompoundShapeChild* obj, btDbvtNode* value);
	EXPORT void btCompoundShapeChild_setTransform(btCompoundShapeChild* obj, const btScalar* value);
	EXPORT void btCompoundShapeChild_delete(btCompoundShapeChild* obj);

	EXPORT btCompoundShape* btCompoundShape_new();
	EXPORT btCompoundShape* btCompoundShape_new2(bool enableDynamicAabbTree);
	EXPORT btCompoundShape* btCompoundShape_new3(bool enableDynamicAabbTree, int initialChildCapacity);
	EXPORT void btCompoundShape_addChildShape(btCompoundShape* obj, const btScalar* localTransform, btCollisionShape* shape);
	EXPORT void btCompoundShape_calculatePrincipalAxisTransform(btCompoundShape* obj, btScalar* masses, btScalar* principal, btScalar* inertia);
	EXPORT void btCompoundShape_createAabbTreeFromChildren(btCompoundShape* obj);
	EXPORT btCompoundShapeChild* btCompoundShape_getChildList(btCompoundShape* obj);
	EXPORT const btCollisionShape* btCompoundShape_getChildShape(btCompoundShape* obj, int index);
	EXPORT void btCompoundShape_getChildTransform(btCompoundShape* obj, int index, btScalar* value);
	EXPORT btDbvt* btCompoundShape_getDynamicAabbTree(btCompoundShape* obj);
	EXPORT int btCompoundShape_getNumChildShapes(btCompoundShape* obj);
	EXPORT int btCompoundShape_getUpdateRevision(btCompoundShape* obj);
	EXPORT void btCompoundShape_recalculateLocalAabb(btCompoundShape* obj);
	EXPORT void btCompoundShape_removeChildShape(btCompoundShape* obj, btCollisionShape* shape);
	EXPORT void btCompoundShape_removeChildShapeByIndex(btCompoundShape* obj, int childShapeindex);
	EXPORT void btCompoundShape_updateChildTransform(btCompoundShape* obj, int childIndex, const btScalar* newChildTransform);
	EXPORT void btCompoundShape_updateChildTransform2(btCompoundShape* obj, int childIndex, const btScalar* newChildTransform, bool shouldRecalculateLocalAabb);
}
