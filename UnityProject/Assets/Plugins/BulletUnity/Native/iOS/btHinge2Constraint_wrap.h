#include "main.h"

extern "C"
{
	EXPORT btHinge2Constraint* btHinge2Constraint_new(btRigidBody* rbA, btRigidBody* rbB, btScalar* anchor, btScalar* axis1, btScalar* axis2);
	EXPORT void btHinge2Constraint_getAnchor(btHinge2Constraint* obj, btScalar* value);
	EXPORT void btHinge2Constraint_getAnchor2(btHinge2Constraint* obj, btScalar* value);
	EXPORT btScalar btHinge2Constraint_getAngle1(btHinge2Constraint* obj);
	EXPORT btScalar btHinge2Constraint_getAngle2(btHinge2Constraint* obj);
	EXPORT void btHinge2Constraint_getAxis1(btHinge2Constraint* obj, btScalar* value);
	EXPORT void btHinge2Constraint_getAxis2(btHinge2Constraint* obj, btScalar* value);
	EXPORT void btHinge2Constraint_setLowerLimit(btHinge2Constraint* obj, btScalar ang1min);
	EXPORT void btHinge2Constraint_setUpperLimit(btHinge2Constraint* obj, btScalar ang1max);
}
