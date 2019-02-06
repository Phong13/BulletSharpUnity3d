#include "main.h"

extern "C"
{
	EXPORT btGearConstraint* btGearConstraint_new(btRigidBody* rbA, btRigidBody* rbB, const btScalar* axisInA, const btScalar* axisInB);
	EXPORT btGearConstraint* btGearConstraint_new2(btRigidBody* rbA, btRigidBody* rbB, const btScalar* axisInA, const btScalar* axisInB, btScalar ratio);
	EXPORT void btGearConstraint_getAxisA(btGearConstraint* obj, btScalar* axisA);
	EXPORT void btGearConstraint_getAxisB(btGearConstraint* obj, btScalar* axisB);
	EXPORT btScalar btGearConstraint_getRatio(btGearConstraint* obj);
	EXPORT void btGearConstraint_setAxisA(btGearConstraint* obj, btScalar* axisA);
	EXPORT void btGearConstraint_setAxisB(btGearConstraint* obj, btScalar* axisB);
	EXPORT void btGearConstraint_setRatio(btGearConstraint* obj, btScalar ratio);
}
