#include "main.h"

extern "C"
{
	EXPORT btGImpactCollisionAlgorithm_CreateFunc* btGImpactCollisionAlgorithm_CreateFunc_new();

	EXPORT btGImpactCollisionAlgorithm* btGImpactCollisionAlgorithm_new(const btCollisionAlgorithmConstructionInfo* ci, const btCollisionObjectWrapper* body0Wrap, const btCollisionObjectWrapper* body1Wrap);
	EXPORT int btGImpactCollisionAlgorithm_getFace0(btGImpactCollisionAlgorithm* obj);
	EXPORT int btGImpactCollisionAlgorithm_getFace1(btGImpactCollisionAlgorithm* obj);
	EXPORT int btGImpactCollisionAlgorithm_getPart0(btGImpactCollisionAlgorithm* obj);
	EXPORT int btGImpactCollisionAlgorithm_getPart1(btGImpactCollisionAlgorithm* obj);
	EXPORT void btGImpactCollisionAlgorithm_gimpact_vs_compoundshape(btGImpactCollisionAlgorithm* obj, const btCollisionObjectWrapper* body0Wrap, const btCollisionObjectWrapper* body1Wrap, const btGImpactShapeInterface* shape0, const btCompoundShape* shape1, bool swapped);
	EXPORT void btGImpactCollisionAlgorithm_gimpact_vs_concave(btGImpactCollisionAlgorithm* obj, const btCollisionObjectWrapper* body0Wrap, const btCollisionObjectWrapper* body1Wrap, const btGImpactShapeInterface* shape0, const btConcaveShape* shape1, bool swapped);
	EXPORT void btGImpactCollisionAlgorithm_gimpact_vs_gimpact(btGImpactCollisionAlgorithm* obj, const btCollisionObjectWrapper* body0Wrap, const btCollisionObjectWrapper* body1Wrap, const btGImpactShapeInterface* shape0, const btGImpactShapeInterface* shape1);
	EXPORT void btGImpactCollisionAlgorithm_gimpact_vs_shape(btGImpactCollisionAlgorithm* obj, const btCollisionObjectWrapper* body0Wrap, const btCollisionObjectWrapper* body1Wrap, const btGImpactShapeInterface* shape0, const btCollisionShape* shape1, bool swapped);
	EXPORT btManifoldResult* btGImpactCollisionAlgorithm_internalGetResultOut(btGImpactCollisionAlgorithm* obj);
	EXPORT void btGImpactCollisionAlgorithm_registerAlgorithm(btCollisionDispatcher* dispatcher);
	EXPORT void btGImpactCollisionAlgorithm_setFace0(btGImpactCollisionAlgorithm* obj, int value);
	EXPORT void btGImpactCollisionAlgorithm_setFace1(btGImpactCollisionAlgorithm* obj, int value);
	EXPORT void btGImpactCollisionAlgorithm_setPart0(btGImpactCollisionAlgorithm* obj, int value);
	EXPORT void btGImpactCollisionAlgorithm_setPart1(btGImpactCollisionAlgorithm* obj, int value);
}
