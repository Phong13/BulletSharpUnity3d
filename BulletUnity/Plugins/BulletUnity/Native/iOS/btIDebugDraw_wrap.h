#include "main.h"

#ifndef BT_IDEBUG_DRAW__H
#define pIDebugDraw_DrawAabb void*
#define pIDebugDraw_DrawArc void*
#define pIDebugDraw_DrawBox void*
#define pIDebugDraw_DrawCapsule void*
#define pIDebugDraw_DrawCone void*
#define pIDebugDraw_DrawContactPoint void*
#define pIDebugDraw_DrawCylinder void*
#define pIDebugDraw_DrawLine void*
#define pIDebugDraw_DrawPlane void*
#define pIDebugDraw_DrawSphere void*
#define pIDebugDraw_DrawSpherePatch void*
#define pIDebugDraw_DrawTransform void*
#define pIDebugDraw_DrawTriangle void*
#define pIDebugDraw_GetDebugMode void*
#define pSimpleCallback void*

#define btIDebugDrawWrapper void
#else
typedef void (*pIDebugDraw_DrawAabb)(const btScalar* from, const btScalar* to, const btScalar* color);
typedef void (*pIDebugDraw_DrawArc)(const btScalar* center, const btScalar* normal, const btScalar* axis,
	btScalar radiusA, btScalar radiusB, btScalar minAngle, btScalar maxAngle, const btScalar* color, bool drawSect, btScalar stepDegrees);
typedef void (*pIDebugDraw_DrawBox)(const btScalar* bbMin, const btScalar* bbMax, const btScalar* trans, const btScalar* color);
typedef void (*pIDebugDraw_DrawCapsule)(btScalar radius, btScalar halfHeight, int upAxis, const btScalar* transform, const btScalar* color);
typedef void (*pIDebugDraw_DrawCone)(btScalar radius, btScalar height, int upAxis, const btScalar* transform, const btScalar* color);
typedef void (*pIDebugDraw_DrawContactPoint)(const btScalar* PointOnB, const btScalar* normalOnB, btScalar distance, int lifeTime, const btScalar* color);
typedef void (*pIDebugDraw_DrawCylinder)(btScalar radius, btScalar halfHeight, int upAxis, const btScalar* transform, const btScalar* color);
typedef void (*pIDebugDraw_DrawLine)(const btScalar* from, const btScalar* to, const btScalar* color);
typedef void (*pIDebugDraw_DrawPlane)(const btScalar* planeNormal, btScalar planeConst, const btScalar* transform, const btScalar* color);
typedef void (*pIDebugDraw_DrawSphere)(btScalar radius, const btScalar* transform, const btScalar* color);
typedef void (*pIDebugDraw_DrawSpherePatch)(const btScalar* center, const btScalar* up, const btScalar* axis, btScalar radius,
	btScalar minTh, btScalar maxTh, btScalar minPs, btScalar maxPs, const btScalar* color, btScalar stepDegrees);
typedef void (*pIDebugDraw_DrawTransform)(const btScalar* transform, btScalar orthoLen);
typedef void (*pIDebugDraw_DrawTriangle)(const btScalar* v0, const btScalar* v1, const btScalar* v2, const btScalar* color, btScalar __unnamed4);
typedef int (*pIDebugDraw_GetDebugMode)();
typedef void (*pSimpleCallback)(int x);

class btIDebugDrawWrapper : public btIDebugDraw
{
private:
	pIDebugDraw_DrawAabb _drawAabbCallback;
	pIDebugDraw_DrawArc _drawArcCallback;
	pIDebugDraw_DrawBox _drawBoxCallback;
	pIDebugDraw_DrawCapsule _drawCapsuleCallback;
	pIDebugDraw_DrawCone _drawConeCallback;
	pIDebugDraw_DrawContactPoint _drawContactPointCallback;
	pIDebugDraw_DrawCylinder _drawCylinderCallback;
	pIDebugDraw_DrawLine _drawLineCallback;
	pIDebugDraw_DrawPlane _drawPlaneCallback;
	pIDebugDraw_DrawSphere _drawSphereCallback;
	pIDebugDraw_DrawSpherePatch _drawSpherePatchCallback;
	pIDebugDraw_DrawTransform _drawTransformCallback;
	pIDebugDraw_DrawTriangle _drawTriangleCallback;
	pIDebugDraw_GetDebugMode _getDebugModeCallback;

public:
	void* _debugDrawGCHandle;
	void* getGCHandle();
	
	pSimpleCallback _cb;

	btIDebugDrawWrapper(void* debugDrawGCHandle, pIDebugDraw_DrawAabb drawAabbCallback, pIDebugDraw_DrawArc drawArcCallback, pIDebugDraw_DrawBox drawBoxCallback,
		pIDebugDraw_DrawCapsule drawCapsuleCallback, pIDebugDraw_DrawCone drawConeCallback, pIDebugDraw_DrawContactPoint drawContactPointCallback, pIDebugDraw_DrawCylinder drawCylinderCallback, pIDebugDraw_DrawLine drawLineCallback,
		pIDebugDraw_DrawPlane drawPlaneCallback, pIDebugDraw_DrawSphere drawSphereCallback, pIDebugDraw_DrawSpherePatch drawSpherePatchCallback, pIDebugDraw_DrawTransform drawTransformCallback,
		pIDebugDraw_DrawTriangle drawTriangleCallback, pIDebugDraw_GetDebugMode getDebugModeCallback, pSimpleCallback cb);

	virtual void draw3dText(const btVector3& location, const char* textString);
	virtual void drawAabb(const btVector3& from, const btVector3& to, const btVector3& color);
	virtual void drawArc(const btVector3& center, const btVector3& normal, const btVector3& axis,
		btScalar radiusA, btScalar radiusB, btScalar minAngle, btScalar maxAngle,
		const btVector3& color, bool drawSect, btScalar stepDegrees);
	virtual void drawArc(const btVector3& center, const btVector3& normal, const btVector3& axis,
		btScalar radiusA, btScalar radiusB, btScalar minAngle, btScalar maxAngle,
		const btVector3& color, bool drawSect);
	virtual void drawBox(const btVector3& bbMin, const btVector3& bbMax, const btTransform& trans, const btVector3& color);
	virtual void drawBox(const btVector3& bbMin, const btVector3& bbMax, const btVector3& color);
	virtual void drawCapsule(btScalar radius, btScalar halfHeight, int upAxis, const btTransform& transform, const btVector3& color);
	virtual void drawCone(btScalar radius, btScalar height, int upAxis, const btTransform& transform, const btVector3& color);
	virtual void drawContactPoint(const btVector3& PointOnB, const btVector3& normalOnB, btScalar distance, int lifeTime, const btVector3& color);
	virtual void drawCylinder(btScalar radius, btScalar halfHeight, int upAxis, const btTransform& transform, const btVector3& color);
	virtual void drawLine(const btVector3& from, const btVector3& to, const btVector3& color);
	virtual void drawPlane(const btVector3& planeNormal, btScalar planeConst, const btTransform& transform, const btVector3& color);
	virtual void drawSphere(const btVector3& p, btScalar radius, const btVector3& color);
	virtual void drawSphere(btScalar radius, const btTransform& transform, const btVector3& color);
	virtual void drawSpherePatch(const btVector3& center, const btVector3& up, const btVector3& axis, btScalar radius,
		btScalar minTh, btScalar maxTh, btScalar minPs, btScalar maxPs, const btVector3& color, btScalar stepDegrees);
	virtual void drawSpherePatch(const btVector3& center, const btVector3& up, const btVector3& axis, btScalar radius,
		btScalar minTh, btScalar maxTh, btScalar minPs, btScalar maxPs, const btVector3& color);
	virtual void drawTransform(const btTransform& transform, btScalar orthoLen);
	virtual void drawTriangle(const btVector3& v0, const btVector3& v1, const btVector3& v2, const btVector3& color, btScalar);
	virtual void drawTriangle(const btVector3& v0, const btVector3& v1, const btVector3& v2,
		const btVector3&, const btVector3&, const btVector3&, const btVector3& color, btScalar alpha);

	virtual void baseDrawAabb(const btVector3& from, const btVector3& to, const btVector3& color);
	virtual void baseDrawCone(btScalar radius, btScalar height, int upAxis, const btTransform& transform, const btVector3& color);
	virtual void baseDrawCylinder(btScalar radius, btScalar halfHeight, int upAxis, const btTransform& transform, const btVector3& color);
	virtual void baseDrawSphere(const btVector3& p, btScalar radius, const btVector3& color);
	virtual void baseDrawTriangle(const btVector3& v0, const btVector3& v1, const btVector3& v2, const btVector3& color, btScalar);
	virtual void baseDrawTriangle(const btVector3& v0, const btVector3& v1, const btVector3& v2,
		const btVector3&, const btVector3&, const btVector3&, const btVector3& color, btScalar alpha);

	virtual void reportErrorWarning(const char* warningString);

	virtual void setDebugMode(int debugMode);
	virtual int	getDebugMode() const;

	// Never called from Bullet
	//virtual void drawLine(const btVector3& from, const btVector3& to, const btVector3& fromColor, const btVector3& toColor);
};
#endif

extern "C"
{
	EXPORT btIDebugDrawWrapper* btIDebugDrawWrapper_new(void* debugDrawGCHandle, pIDebugDraw_DrawAabb drawAabbCallback,
		pIDebugDraw_DrawArc drawArcCallback, pIDebugDraw_DrawBox drawBoxCallback, pIDebugDraw_DrawCapsule drawCapsule, pIDebugDraw_DrawCone drawCone, pIDebugDraw_DrawContactPoint drawContactPointCallback,
		pIDebugDraw_DrawCylinder drawCylinderCallback, pIDebugDraw_DrawLine drawLineCallback, pIDebugDraw_DrawPlane drawPlaneCallback, pIDebugDraw_DrawSphere drawSphereCallback,
		pIDebugDraw_DrawSpherePatch drawSpherePatchCallback, pIDebugDraw_DrawTransform drawTransformCallback, pIDebugDraw_DrawTriangle drawTriangleCallback, pIDebugDraw_GetDebugMode getDebugModeCallback, pSimpleCallback cb);
	EXPORT void* btIDebugDrawWrapper_getGCHandle(btIDebugDrawWrapper* obj);

	EXPORT void btIDebugDraw_delete(btIDebugDraw* obj);
}
