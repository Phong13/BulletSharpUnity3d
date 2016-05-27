#include "main.h"

extern "C"
{
	EXPORT btConvexCast_CastResult* btConvexCast_CastResult_new();
	EXPORT void btConvexCast_CastResult_DebugDraw(btConvexCast_CastResult* obj, btScalar fraction);
	EXPORT void btConvexCast_CastResult_drawCoordSystem(btConvexCast_CastResult* obj, const btScalar* trans);
	EXPORT btScalar btConvexCast_CastResult_getAllowedPenetration(btConvexCast_CastResult* obj);
	EXPORT btIDebugDraw* btConvexCast_CastResult_getDebugDrawer(btConvexCast_CastResult* obj);
	EXPORT btScalar btConvexCast_CastResult_getFraction(btConvexCast_CastResult* obj);
	EXPORT void btConvexCast_CastResult_getHitPoint(btConvexCast_CastResult* obj, btScalar* value);
	EXPORT void btConvexCast_CastResult_getHitTransformA(btConvexCast_CastResult* obj, btScalar* value);
	EXPORT void btConvexCast_CastResult_getHitTransformB(btConvexCast_CastResult* obj, btScalar* value);
	EXPORT void btConvexCast_CastResult_getNormal(btConvexCast_CastResult* obj, btScalar* value);
	EXPORT void btConvexCast_CastResult_reportFailure(btConvexCast_CastResult* obj, int errNo, int numIterations);
	EXPORT void btConvexCast_CastResult_setAllowedPenetration(btConvexCast_CastResult* obj, btScalar value);
	EXPORT void btConvexCast_CastResult_setDebugDrawer(btConvexCast_CastResult* obj, btIDebugDraw* value);
	EXPORT void btConvexCast_CastResult_setFraction(btConvexCast_CastResult* obj, btScalar value);
	EXPORT void btConvexCast_CastResult_setHitPoint(btConvexCast_CastResult* obj, const btScalar* value);
	EXPORT void btConvexCast_CastResult_setHitTransformA(btConvexCast_CastResult* obj, const btScalar* value);
	EXPORT void btConvexCast_CastResult_setHitTransformB(btConvexCast_CastResult* obj, const btScalar* value);
	EXPORT void btConvexCast_CastResult_setNormal(btConvexCast_CastResult* obj, const btScalar* value);
	EXPORT void btConvexCast_CastResult_delete(btConvexCast_CastResult* obj);

	EXPORT bool btConvexCast_calcTimeOfImpact(btConvexCast* obj, const btScalar* fromA, const btScalar* toA, const btScalar* fromB, const btScalar* toB, btConvexCast_CastResult* result);
	EXPORT void btConvexCast_delete(btConvexCast* obj);
}
