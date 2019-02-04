#include "main.h"

extern "C"
{
	EXPORT btCompoundShape* btCompoundFromGImpact_btCreateCompoundFromGimpactShape(const btGImpactMeshShape* gimpactMesh, btScalar depth);
}
