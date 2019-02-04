#include "main.h"

extern "C"
{
	EXPORT btConstraintSetting* btConstraintSetting_new();
	EXPORT btScalar btConstraintSetting_getDamping(btConstraintSetting* obj);
	EXPORT btScalar btConstraintSetting_getImpulseClamp(btConstraintSetting* obj);
	EXPORT btScalar btConstraintSetting_getTau(btConstraintSetting* obj);
	EXPORT void btConstraintSetting_setDamping(btConstraintSetting* obj, btScalar value);
	EXPORT void btConstraintSetting_setImpulseClamp(btConstraintSetting* obj, btScalar value);
	EXPORT void btConstraintSetting_setTau(btConstraintSetting* obj, btScalar value);
	EXPORT void btConstraintSetting_delete(btConstraintSetting* obj);

	EXPORT btPoint2PointConstraint* btPoint2PointConstraint_new(btRigidBody* rbA, btRigidBody* rbB, const btScalar* pivotInA, const btScalar* pivotInB);
	EXPORT btPoint2PointConstraint* btPoint2PointConstraint_new2(btRigidBody* rbA, const btScalar* pivotInA);
	EXPORT int btPoint2PointConstraint_getFlags(btPoint2PointConstraint* obj);
	EXPORT void btPoint2PointConstraint_getInfo1NonVirtual(btPoint2PointConstraint* obj, btTypedConstraint_btConstraintInfo1* info);
	EXPORT void btPoint2PointConstraint_getInfo2NonVirtual(btPoint2PointConstraint* obj, btTypedConstraint_btConstraintInfo2* info, const btScalar* body0_trans, const btScalar* body1_trans);
	EXPORT void btPoint2PointConstraint_getPivotInA(btPoint2PointConstraint* obj, btScalar* value);
	EXPORT void btPoint2PointConstraint_getPivotInB(btPoint2PointConstraint* obj, btScalar* value);
	EXPORT btConstraintSetting* btPoint2PointConstraint_getSetting(btPoint2PointConstraint* obj);
	EXPORT bool btPoint2PointConstraint_getUseSolveConstraintObsolete(btPoint2PointConstraint* obj);
	EXPORT void btPoint2PointConstraint_setPivotA(btPoint2PointConstraint* obj, const btScalar* pivotA);
	EXPORT void btPoint2PointConstraint_setPivotB(btPoint2PointConstraint* obj, const btScalar* pivotB);
	EXPORT void btPoint2PointConstraint_setUseSolveConstraintObsolete(btPoint2PointConstraint* obj, bool value);
	EXPORT void btPoint2PointConstraint_updateRHS(btPoint2PointConstraint* obj, btScalar timeStep);
}
