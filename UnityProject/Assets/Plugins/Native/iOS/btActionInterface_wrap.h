#include "main.h"

#ifndef _BT_ACTION_INTERFACE_H
#define pIAction_DebugDraw void*
#define pIAction_UpdateAction void*
#define btActionInterfaceWrapper void
#else
typedef void (*pIAction_DebugDraw)(btIDebugDraw* debugDrawer);
typedef void (*pIAction_UpdateAction)(btCollisionWorld* collisionWorld, btScalar deltaTimeStep);

class btActionInterfaceWrapper : public btActionInterface
{
private:
	pIAction_DebugDraw _debugDrawCallback;
	pIAction_UpdateAction _updateActionCallback;

public:
	btActionInterfaceWrapper(pIAction_DebugDraw debugDrawCallback, pIAction_UpdateAction updateActionCallback);

	virtual void debugDraw(btIDebugDraw* debugDrawer);
	virtual void updateAction(btCollisionWorld* collisionWorld, btScalar deltaTimeStep);
};
#endif

extern "C"
{
	EXPORT btActionInterfaceWrapper* btActionInterfaceWrapper_new(pIAction_DebugDraw debugDrawCallback, pIAction_UpdateAction updateActionCallback);

	EXPORT void btActionInterface_debugDraw(btActionInterface* obj, btIDebugDraw* debugDrawer);
	EXPORT void btActionInterface_updateAction(btActionInterface* obj, btCollisionWorld* collisionWorld, btScalar deltaTimeStep);
	EXPORT void btActionInterface_delete(btActionInterface* obj);
}
