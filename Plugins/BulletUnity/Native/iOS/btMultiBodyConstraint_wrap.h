#include "main.h"

extern "C"
{
	EXPORT void btMultiBodyConstraint_allocateJacobiansMultiDof(btMultiBodyConstraint* obj);
	EXPORT void btMultiBodyConstraint_createConstraintRows(btMultiBodyConstraint* obj, btMultiBodyConstraintArray* constraintRows, btMultiBodyJacobianData* data, const btContactSolverInfo* infoGlobal);
	EXPORT void btMultiBodyConstraint_debugDraw(btMultiBodyConstraint* obj, btIDebugDraw* drawer);
	EXPORT void btMultiBodyConstraint_finalizeMultiDof(btMultiBodyConstraint* obj);
	EXPORT btScalar btMultiBodyConstraint_getAppliedImpulse(btMultiBodyConstraint* obj, int dof);
	EXPORT int btMultiBodyConstraint_getIslandIdA(btMultiBodyConstraint* obj);
	EXPORT int btMultiBodyConstraint_getIslandIdB(btMultiBodyConstraint* obj);
	EXPORT btScalar btMultiBodyConstraint_getMaxAppliedImpulse(btMultiBodyConstraint* obj);
	EXPORT btMultiBody* btMultiBodyConstraint_getMultiBodyA(btMultiBodyConstraint* obj);
	EXPORT btMultiBody* btMultiBodyConstraint_getMultiBodyB(btMultiBodyConstraint* obj);
	EXPORT int btMultiBodyConstraint_getNumRows(btMultiBodyConstraint* obj);
	EXPORT btScalar btMultiBodyConstraint_getPosition(btMultiBodyConstraint* obj, int row);
	EXPORT void btMultiBodyConstraint_internalSetAppliedImpulse(btMultiBodyConstraint* obj, int dof, btScalar appliedImpulse);
	EXPORT bool btMultiBodyConstraint_isUnilateral(btMultiBodyConstraint* obj);
	EXPORT btScalar* btMultiBodyConstraint_jacobianA(btMultiBodyConstraint* obj, int row);
	EXPORT btScalar* btMultiBodyConstraint_jacobianB(btMultiBodyConstraint* obj, int row);
	EXPORT void btMultiBodyConstraint_setMaxAppliedImpulse(btMultiBodyConstraint* obj, btScalar maxImp);
	EXPORT void btMultiBodyConstraint_setPosition(btMultiBodyConstraint* obj, int row, btScalar pos);
	EXPORT void btMultiBodyConstraint_updateJacobianSizes(btMultiBodyConstraint* obj);
	EXPORT void btMultiBodyConstraint_delete(btMultiBodyConstraint* obj);
}
