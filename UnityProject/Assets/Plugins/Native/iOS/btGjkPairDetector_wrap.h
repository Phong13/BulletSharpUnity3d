#include "main.h"

extern "C"
{
	EXPORT btGjkPairDetector* btGjkPairDetector_new(const btConvexShape* objectA, const btConvexShape* objectB, btVoronoiSimplexSolver* simplexSolver, btConvexPenetrationDepthSolver* penetrationDepthSolver);
	EXPORT btGjkPairDetector* btGjkPairDetector_new2(const btConvexShape* objectA, const btConvexShape* objectB, int shapeTypeA, int shapeTypeB, btScalar marginA, btScalar marginB, btVoronoiSimplexSolver* simplexSolver, btConvexPenetrationDepthSolver* penetrationDepthSolver);
	EXPORT void btGjkPairDetector_getCachedSeparatingAxis(btGjkPairDetector* obj, btScalar* value);
	EXPORT btScalar btGjkPairDetector_getCachedSeparatingDistance(btGjkPairDetector* obj);
	EXPORT int btGjkPairDetector_getCatchDegeneracies(btGjkPairDetector* obj);
	EXPORT void btGjkPairDetector_getClosestPointsNonVirtual(btGjkPairDetector* obj, const btDiscreteCollisionDetectorInterface_ClosestPointInput* input, btDiscreteCollisionDetectorInterface_Result* output, btIDebugDraw* debugDraw);
	EXPORT int btGjkPairDetector_getCurIter(btGjkPairDetector* obj);
	EXPORT int btGjkPairDetector_getDegenerateSimplex(btGjkPairDetector* obj);
	EXPORT int btGjkPairDetector_getFixContactNormalDirection(btGjkPairDetector* obj);
	EXPORT int btGjkPairDetector_getLastUsedMethod(btGjkPairDetector* obj);
	EXPORT void btGjkPairDetector_setCachedSeparatingAxis(btGjkPairDetector* obj, const btScalar* seperatingAxis);
	EXPORT void btGjkPairDetector_setCatchDegeneracies(btGjkPairDetector* obj, int value);
	EXPORT void btGjkPairDetector_setCurIter(btGjkPairDetector* obj, int value);
	EXPORT void btGjkPairDetector_setDegenerateSimplex(btGjkPairDetector* obj, int value);
	EXPORT void btGjkPairDetector_setFixContactNormalDirection(btGjkPairDetector* obj, int value);
	EXPORT void btGjkPairDetector_setIgnoreMargin(btGjkPairDetector* obj, bool ignoreMargin);
	EXPORT void btGjkPairDetector_setLastUsedMethod(btGjkPairDetector* obj, int value);
	EXPORT void btGjkPairDetector_setMinkowskiA(btGjkPairDetector* obj, const btConvexShape* minkA);
	EXPORT void btGjkPairDetector_setMinkowskiB(btGjkPairDetector* obj, const btConvexShape* minkB);
	EXPORT void btGjkPairDetector_setPenetrationDepthSolver(btGjkPairDetector* obj, btConvexPenetrationDepthSolver* penetrationDepthSolver);
}
