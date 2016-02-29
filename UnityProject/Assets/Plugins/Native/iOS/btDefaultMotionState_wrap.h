#include "main.h"

extern "C"
{
	EXPORT btDefaultMotionState* btDefaultMotionState_new();
	EXPORT btDefaultMotionState* btDefaultMotionState_new2(const btScalar* startTrans);
	EXPORT btDefaultMotionState* btDefaultMotionState_new3(const btScalar* startTrans, const btScalar* centerOfMassOffset);
	EXPORT void btDefaultMotionState_getCenterOfMassOffset(btDefaultMotionState* obj, btScalar* value);
	EXPORT void btDefaultMotionState_getGraphicsWorldTrans(btDefaultMotionState* obj, btScalar* value);
	EXPORT void btDefaultMotionState_getStartWorldTrans(btDefaultMotionState* obj, btScalar* value);
	EXPORT void* btDefaultMotionState_getUserPointer(btDefaultMotionState* obj);
	EXPORT void btDefaultMotionState_setCenterOfMassOffset(btDefaultMotionState* obj, const btScalar* value);
	EXPORT void btDefaultMotionState_setGraphicsWorldTrans(btDefaultMotionState* obj, const btScalar* value);
	EXPORT void btDefaultMotionState_setStartWorldTrans(btDefaultMotionState* obj, const btScalar* value);
	EXPORT void btDefaultMotionState_setUserPointer(btDefaultMotionState* obj, void* value);
}
