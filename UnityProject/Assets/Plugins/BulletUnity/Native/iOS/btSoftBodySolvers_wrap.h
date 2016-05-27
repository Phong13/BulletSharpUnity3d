#include "main.h"

extern "C"
{
	EXPORT bool btSoftBodySolver_checkInitialized(btSoftBodySolver* obj);
	EXPORT void btSoftBodySolver_copyBackToSoftBodies(btSoftBodySolver* obj);
	EXPORT void btSoftBodySolver_copyBackToSoftBodies2(btSoftBodySolver* obj, bool bMove);
	EXPORT int btSoftBodySolver_getNumberOfPositionIterations(btSoftBodySolver* obj);
	EXPORT int btSoftBodySolver_getNumberOfVelocityIterations(btSoftBodySolver* obj);
	//EXPORT SolverTypes btSoftBodySolver_getSolverType(btSoftBodySolver* obj);
	EXPORT float btSoftBodySolver_getTimeScale(btSoftBodySolver* obj);
	//EXPORT void btSoftBodySolver_optimize(btSoftBodySolver* obj, btAlignedObjectArray<btSoftBody*>* softBodies);
	//EXPORT void btSoftBodySolver_optimize2(btSoftBodySolver* obj, btAlignedObjectArray<btSoftBody*>* softBodies, bool forceUpdate);
	EXPORT void btSoftBodySolver_predictMotion(btSoftBodySolver* obj, float solverdt);
	//EXPORT void btSoftBodySolver_processCollision(btSoftBodySolver* obj, btSoftBody* __unnamed0, const btCollisionObjectWrapper* __unnamed1);
	//EXPORT void btSoftBodySolver_processCollision2(btSoftBodySolver* obj, btSoftBody* __unnamed0, btSoftBody* __unnamed1);
	EXPORT void btSoftBodySolver_setNumberOfPositionIterations(btSoftBodySolver* obj, int iterations);
	EXPORT void btSoftBodySolver_setNumberOfVelocityIterations(btSoftBodySolver* obj, int iterations);
	EXPORT void btSoftBodySolver_solveConstraints(btSoftBodySolver* obj, float solverdt);
	EXPORT void btSoftBodySolver_updateSoftBodies(btSoftBodySolver* obj);
	EXPORT void btSoftBodySolver_delete(btSoftBodySolver* obj);

	//EXPORT void btSoftBodySolverOutput_copySoftBodyToVertexBuffer(btSoftBodySolverOutput* obj, const btSoftBody* softBody, btVertexBufferDescriptor* vertexBuffer);
	EXPORT void btSoftBodySolverOutput_delete(btSoftBodySolverOutput* obj);
}
