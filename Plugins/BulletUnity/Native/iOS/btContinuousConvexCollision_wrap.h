#include "main.h"

extern "C"
{
	EXPORT btContinuousConvexCollision* btContinuousConvexCollision_new(const btConvexShape* shapeA, const btConvexShape* shapeB, btVoronoiSimplexSolver* simplexSolver, btConvexPenetrationDepthSolver* penetrationDepthSolver);
	EXPORT btContinuousConvexCollision* btContinuousConvexCollision_new2(const btConvexShape* shapeA, const btStaticPlaneShape* plane);
}
