#include "main.h"

extern "C"
{
	EXPORT btCapsuleShape* btCapsuleShape_new(btScalar radius, btScalar height);
	EXPORT void btCapsuleShape_deSerializeFloat(btCapsuleShape* obj, btCapsuleShapeData* dataBuffer);
	EXPORT btScalar btCapsuleShape_getHalfHeight(btCapsuleShape* obj);
	EXPORT btScalar btCapsuleShape_getRadius(btCapsuleShape* obj);
	EXPORT int btCapsuleShape_getUpAxis(btCapsuleShape* obj);

	EXPORT btCapsuleShapeX* btCapsuleShapeX_new(btScalar radius, btScalar height);

	EXPORT btCapsuleShapeZ* btCapsuleShapeZ_new(btScalar radius, btScalar height);
}
