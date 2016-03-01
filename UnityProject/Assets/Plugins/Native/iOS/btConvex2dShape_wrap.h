#include "main.h"

extern "C"
{
	EXPORT btConvex2dShape* btConvex2dShape_new(btConvexShape* convexChildShape);
	EXPORT const btConvexShape* btConvex2dShape_getChildShape(btConvex2dShape* obj);
}
