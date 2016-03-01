#include "main.h"

extern "C"
{
	EXPORT btSoftRigidDynamicsWorld* btSoftRigidDynamicsWorld_new(btDispatcher* dispatcher, btBroadphaseInterface* pairCache, btConstraintSolver* constraintSolver, btCollisionConfiguration* collisionConfiguration);
	EXPORT btSoftRigidDynamicsWorld* btSoftRigidDynamicsWorld_new2(btDispatcher* dispatcher, btBroadphaseInterface* pairCache, btConstraintSolver* constraintSolver, btCollisionConfiguration* collisionConfiguration, btSoftBodySolver* softBodySolver);
	EXPORT void btSoftRigidDynamicsWorld_addSoftBody(btSoftRigidDynamicsWorld* obj, btSoftBody* body);
	EXPORT void btSoftRigidDynamicsWorld_addSoftBody2(btSoftRigidDynamicsWorld* obj, btSoftBody* body, short collisionFilterGroup);
	EXPORT void btSoftRigidDynamicsWorld_addSoftBody3(btSoftRigidDynamicsWorld* obj, btSoftBody* body, short collisionFilterGroup, short collisionFilterMask);
	EXPORT int btSoftRigidDynamicsWorld_getDrawFlags(btSoftRigidDynamicsWorld* obj);
	EXPORT btSoftBodyArray* btSoftRigidDynamicsWorld_getSoftBodyArray(btSoftRigidDynamicsWorld* obj);
	EXPORT btSoftBodyWorldInfo* btSoftRigidDynamicsWorld_getWorldInfo(btSoftRigidDynamicsWorld* obj);
	EXPORT void btSoftRigidDynamicsWorld_removeSoftBody(btSoftRigidDynamicsWorld* obj, btSoftBody* body);
	EXPORT void btSoftRigidDynamicsWorld_setDrawFlags(btSoftRigidDynamicsWorld* obj, int f);
}
