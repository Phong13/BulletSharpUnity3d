#include "main.h"

extern "C"
{
	EXPORT btSphereShape* btSphereShape_new(btScalar radius);
	EXPORT btScalar btSphereShape_getRadius(btSphereShape* obj);
	EXPORT void btSphereShape_setUnscaledRadius(btSphereShape* obj, btScalar radius);
}
