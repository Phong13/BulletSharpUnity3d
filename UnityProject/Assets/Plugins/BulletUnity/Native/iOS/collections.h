#include "main.h"

extern "C"
{
	EXPORT btCompoundShapeChild* btCompoundShapeChild_array_at(btCompoundShapeChild* a, int n);
	EXPORT btSoftBody_Node* btSoftBodyNodePtrArray_at(btSoftBodyNodePtrArray* obj, int n);
	EXPORT void btSoftBodyNodePtrArray_set(btSoftBodyNodePtrArray* obj, btSoftBody_Node* value, int index);
	EXPORT void btVector3_array_at(const btVector3* a, int n, btScalar* value);
	EXPORT void btVector3_array_set(btVector3* obj, int n, const btScalar* value);
	EXPORT btAlignedVector3Array* btAlignedVector3Array_new();
	EXPORT void btAlignedVector3Array_at(btAlignedVector3Array* obj, int n, btScalar* value);
	EXPORT void btAlignedVector3Array_push_back(btAlignedVector3Array* obj, const btScalar* value);
	EXPORT void btAlignedVector3Array_push_back2(btAlignedVector3Array* obj, const btScalar* value); // btVector4
	EXPORT void btAlignedVector3Array_set(btAlignedVector3Array* obj, int n, const btScalar* value);
	EXPORT int btAlignedVector3Array_size(btAlignedVector3Array* obj);
	EXPORT void btAlignedVector3Array_delete(btAlignedVector3Array* obj);
}
