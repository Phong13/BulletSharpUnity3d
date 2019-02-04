#include "main.h"

extern "C"
{
	EXPORT btMultiBodyConstraintSolver* btMultiBodyConstraintSolver_new();
	EXPORT btScalar btMultiBodyConstraintSolver_solveGroupCacheFriendlyFinish(btMultiBodyConstraintSolver* obj, btCollisionObject** bodies, int numBodies, const btContactSolverInfo* infoGlobal);
	EXPORT void btMultiBodyConstraintSolver_solveMultiBodyGroup(btMultiBodyConstraintSolver* obj, btCollisionObject** bodies, int numBodies, btPersistentManifold** manifold, int numManifolds, btTypedConstraint** constraints, int numConstraints, btMultiBodyConstraint** multiBodyConstraints, int numMultiBodyConstraints, const btContactSolverInfo* info, btIDebugDraw* debugDrawer, btDispatcher* dispatcher);
}
