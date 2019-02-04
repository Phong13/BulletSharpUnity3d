#include "main.h"

extern "C"
{
	EXPORT void btDynamicsWorld_addAction(btDynamicsWorld* obj, btActionInterface* action);
	EXPORT void btDynamicsWorld_addConstraint(btDynamicsWorld* obj, btTypedConstraint* constraint);
	EXPORT void btDynamicsWorld_addConstraint2(btDynamicsWorld* obj, btTypedConstraint* constraint, bool disableCollisionsBetweenLinkedBodies);
	EXPORT void btDynamicsWorld_addRigidBody(btDynamicsWorld* obj, btRigidBody* body);
	EXPORT void btDynamicsWorld_addRigidBody2(btDynamicsWorld* obj, btRigidBody* body, short group, short mask);
	EXPORT void btDynamicsWorld_clearForces(btDynamicsWorld* obj);
	EXPORT btTypedConstraint* btDynamicsWorld_getConstraint(btDynamicsWorld* obj, int index);
	EXPORT btConstraintSolver* btDynamicsWorld_getConstraintSolver(btDynamicsWorld* obj);
	EXPORT void btDynamicsWorld_getGravity(btDynamicsWorld* obj, btScalar* gravity);
	EXPORT int btDynamicsWorld_getNumConstraints(btDynamicsWorld* obj);
	EXPORT btContactSolverInfo* btDynamicsWorld_getSolverInfo(btDynamicsWorld* obj);
	EXPORT btDynamicsWorldType btDynamicsWorld_getWorldType(btDynamicsWorld* obj);
	EXPORT void* btDynamicsWorld_getWorldUserInfo(btDynamicsWorld* obj);
	EXPORT void btDynamicsWorld_removeAction(btDynamicsWorld* obj, btActionInterface* action);
	EXPORT void btDynamicsWorld_removeConstraint(btDynamicsWorld* obj, btTypedConstraint* constraint);
	EXPORT void btDynamicsWorld_removeRigidBody(btDynamicsWorld* obj, btRigidBody* body);
	EXPORT void btDynamicsWorld_setConstraintSolver(btDynamicsWorld* obj, btConstraintSolver* solver);
	EXPORT void btDynamicsWorld_setGravity(btDynamicsWorld* obj, const btScalar* gravity);
	EXPORT void btDynamicsWorld_setInternalTickCallback(btDynamicsWorld* obj, btInternalTickCallback cb);
	EXPORT void btDynamicsWorld_setInternalTickCallback2(btDynamicsWorld* obj, btInternalTickCallback cb, void* worldUserInfo);
	EXPORT void btDynamicsWorld_setInternalTickCallback3(btDynamicsWorld* obj, btInternalTickCallback cb, void* worldUserInfo, bool isPreTick);
	EXPORT void btDynamicsWorld_setWorldUserInfo(btDynamicsWorld* obj, void* worldUserInfo);
	EXPORT int btDynamicsWorld_stepSimulation(btDynamicsWorld* obj, btScalar timeStep);
	EXPORT int btDynamicsWorld_stepSimulation2(btDynamicsWorld* obj, btScalar timeStep, int maxSubSteps);
	EXPORT int btDynamicsWorld_stepSimulation3(btDynamicsWorld* obj, btScalar timeStep, int maxSubSteps, btScalar fixedTimeStep);
	EXPORT void btDynamicsWorld_synchronizeMotionStates(btDynamicsWorld* obj);
}
