#include "main.h"

extern "C"
{
	EXPORT bool btCharacterControllerInterface_canJump(btCharacterControllerInterface* obj);
	EXPORT void btCharacterControllerInterface_jump(btCharacterControllerInterface* obj);
	EXPORT bool btCharacterControllerInterface_onGround(btCharacterControllerInterface* obj);
	EXPORT void btCharacterControllerInterface_playerStep(btCharacterControllerInterface* obj, btCollisionWorld* collisionWorld, btScalar dt);
	EXPORT void btCharacterControllerInterface_preStep(btCharacterControllerInterface* obj, btCollisionWorld* collisionWorld);
	EXPORT void btCharacterControllerInterface_reset(btCharacterControllerInterface* obj, btCollisionWorld* collisionWorld);
	EXPORT void btCharacterControllerInterface_setUpInterpolate(btCharacterControllerInterface* obj, bool value);
	EXPORT void btCharacterControllerInterface_setWalkDirection(btCharacterControllerInterface* obj, const btScalar* walkDirection);
	EXPORT void btCharacterControllerInterface_setVelocityForTimeInterval(btCharacterControllerInterface* obj, const btScalar* velocity, btScalar timeInterval);
	EXPORT void btCharacterControllerInterface_warp(btCharacterControllerInterface* obj, const btScalar* origin);
}
