#include "main.h"

extern "C"
{
	EXPORT void btConstraintSolver_allSolved(btConstraintSolver* obj, const btContactSolverInfo* __unnamed0, btIDebugDraw* __unnamed1);
	EXPORT btConstraintSolverType btConstraintSolver_getSolverType(btConstraintSolver* obj);
	EXPORT void btConstraintSolver_prepareSolve(btConstraintSolver* obj, int __unnamed0, int __unnamed1);
	EXPORT void btConstraintSolver_reset(btConstraintSolver* obj);
	EXPORT btScalar btConstraintSolver_solveGroup(btConstraintSolver* obj, btCollisionObject** bodies, int numBodies, btPersistentManifold** manifold, int numManifolds, btTypedConstraint** constraints, int numConstraints, const btContactSolverInfo* info, btIDebugDraw* debugDrawer, btDispatcher* dispatcher);
	EXPORT void btConstraintSolver_delete(btConstraintSolver* obj);
}
