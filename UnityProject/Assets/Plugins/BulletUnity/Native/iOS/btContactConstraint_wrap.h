#include "main.h"

extern "C"
{
	EXPORT const btPersistentManifold* btContactConstraint_getContactManifold(btContactConstraint* obj);
	EXPORT void btContactConstraint_setContactManifold(btContactConstraint* obj, btPersistentManifold* contactManifold);
}
