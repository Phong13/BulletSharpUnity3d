#include "main.h"

extern "C"
{
	EXPORT btKinematicCharacterController* btKinematicCharacterController_new(btPairCachingGhostObject* ghostObject, btConvexShape* convexShape, btScalar stepHeight);
	EXPORT btKinematicCharacterController* btKinematicCharacterController_new2(btPairCachingGhostObject* ghostObject, btConvexShape* convexShape, btScalar stepHeight, int upAxis);
	EXPORT btPairCachingGhostObject* btKinematicCharacterController_getGhostObject(btKinematicCharacterController* obj);
	EXPORT btScalar btKinematicCharacterController_getGravity(btKinematicCharacterController* obj);
	EXPORT btScalar btKinematicCharacterController_getMaxSlope(btKinematicCharacterController* obj);
	EXPORT void btKinematicCharacterController_setFallSpeed(btKinematicCharacterController* obj, btScalar fallSpeed);
	EXPORT void btKinematicCharacterController_setGravity(btKinematicCharacterController* obj, btScalar gravity);
	EXPORT void btKinematicCharacterController_setJumpSpeed(btKinematicCharacterController* obj, btScalar jumpSpeed);
	EXPORT void btKinematicCharacterController_setMaxJumpHeight(btKinematicCharacterController* obj, btScalar maxJumpHeight);
	EXPORT void btKinematicCharacterController_setMaxSlope(btKinematicCharacterController* obj, btScalar slopeRadians);
	EXPORT void btKinematicCharacterController_setUpAxis(btKinematicCharacterController* obj, int axis);
	EXPORT void btKinematicCharacterController_setUseGhostSweepTest(btKinematicCharacterController* obj, bool useGhostObjectSweepTest);
}
