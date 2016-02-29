#include "main.h"

extern "C"
{
	EXPORT btConeShape* btConeShape_new(btScalar radius, btScalar height);
	EXPORT int btConeShape_getConeUpIndex(btConeShape* obj);
	EXPORT btScalar btConeShape_getHeight(btConeShape* obj);
	EXPORT btScalar btConeShape_getRadius(btConeShape* obj);
	EXPORT void btConeShape_setConeUpIndex(btConeShape* obj, int upIndex);

	EXPORT btConeShapeX* btConeShapeX_new(btScalar radius, btScalar height);

	EXPORT btConeShapeZ* btConeShapeZ_new(btScalar radius, btScalar height);
}
