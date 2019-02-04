#include "main.h"

extern "C"
{
	EXPORT btUniversalConstraint* btUniversalConstraint_new(btRigidBody* rbA, btRigidBody* rbB, const btScalar* anchor, const btScalar* axis1, const btScalar* axis2);
	EXPORT void btUniversalConstraint_getAnchor(btUniversalConstraint* obj, btScalar* value);
	EXPORT void btUniversalConstraint_getAnchor2(btUniversalConstraint* obj, btScalar* value);
	EXPORT btScalar btUniversalConstraint_getAngle1(btUniversalConstraint* obj);
	EXPORT btScalar btUniversalConstraint_getAngle2(btUniversalConstraint* obj);
	EXPORT void btUniversalConstraint_getAxis1(btUniversalConstraint* obj, btScalar* value);
	EXPORT void btUniversalConstraint_getAxis2(btUniversalConstraint* obj, btScalar* value);
	EXPORT void btUniversalConstraint_setLowerLimit(btUniversalConstraint* obj, btScalar ang1min, btScalar ang2min);
	EXPORT void btUniversalConstraint_setUpperLimit(btUniversalConstraint* obj, btScalar ang1max, btScalar ang2max);
}
