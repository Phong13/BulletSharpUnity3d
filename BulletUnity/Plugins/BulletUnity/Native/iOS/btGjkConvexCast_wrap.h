#include "main.h"

extern "C"
{
	EXPORT btGjkConvexCast* btGjkConvexCast_new(const btConvexShape* convexA, const btConvexShape* convexB, btVoronoiSimplexSolver* simplexSolver);
}
