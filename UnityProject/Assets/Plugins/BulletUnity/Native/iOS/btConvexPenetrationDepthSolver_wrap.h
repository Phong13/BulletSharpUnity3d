#include "main.h"

extern "C"
{
	EXPORT bool btConvexPenetrationDepthSolver_calcPenDepth(btConvexPenetrationDepthSolver* obj, btVoronoiSimplexSolver* simplexSolver, const btConvexShape* convexA, const btConvexShape* convexB, const btScalar* transA, const btScalar* transB, btScalar* v, btScalar* pa, btScalar* pb, btIDebugDraw* debugDraw);
	EXPORT void btConvexPenetrationDepthSolver_delete(btConvexPenetrationDepthSolver* obj);
}
