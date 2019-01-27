#include "main.h"

extern "C"
{
	EXPORT btMultiBodyPoint2Point* btMultiBodyPoint2Point_new(btMultiBody* body, int link, btRigidBody* bodyB, const btScalar* pivotInA, const btScalar* pivotInB);
	EXPORT btMultiBodyPoint2Point* btMultiBodyPoint2Point_new2(btMultiBody* bodyA, int linkA, btMultiBody* bodyB, int linkB, const btScalar* pivotInA, const btScalar* pivotInB);
	EXPORT void btMultiBodyPoint2Point_getPivotInB(btMultiBodyPoint2Point* obj, btScalar* pivotInB);
	EXPORT void btMultiBodyPoint2Point_setPivotInB(btMultiBodyPoint2Point* obj, const btScalar* pivotInB);
}
