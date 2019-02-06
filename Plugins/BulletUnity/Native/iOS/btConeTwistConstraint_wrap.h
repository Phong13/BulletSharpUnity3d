#include "main.h"

extern "C"
{
	EXPORT btConeTwistConstraint* btConeTwistConstraint_new(btRigidBody* rbA, btRigidBody* rbB, const btScalar* rbAFrame, const btScalar* rbBFrame);
	EXPORT btConeTwistConstraint* btConeTwistConstraint_new2(btRigidBody* rbA, const btScalar* rbAFrame);
	EXPORT void btConeTwistConstraint_calcAngleInfo(btConeTwistConstraint* obj);
	EXPORT void btConeTwistConstraint_calcAngleInfo2(btConeTwistConstraint* obj, const btScalar* transA, const btScalar* transB, const btScalar* invInertiaWorldA, const btScalar* invInertiaWorldB);
	EXPORT void btConeTwistConstraint_enableMotor(btConeTwistConstraint* obj, bool b);
	EXPORT void btConeTwistConstraint_getAFrame(btConeTwistConstraint* obj, btScalar* value);
	EXPORT bool btConeTwistConstraint_getAngularOnly(btConeTwistConstraint* obj);
	EXPORT void btConeTwistConstraint_getBFrame(btConeTwistConstraint* obj, btScalar* value);
	EXPORT btScalar btConeTwistConstraint_getBiasFactor(btConeTwistConstraint* obj);
	EXPORT btScalar btConeTwistConstraint_getDamping(btConeTwistConstraint* obj);
	EXPORT btScalar btConeTwistConstraint_getFixThresh(btConeTwistConstraint* obj);
	EXPORT int btConeTwistConstraint_getFlags(btConeTwistConstraint* obj);
	EXPORT void btConeTwistConstraint_getFrameOffsetA(btConeTwistConstraint* obj, btScalar* value);
	EXPORT void btConeTwistConstraint_getFrameOffsetB(btConeTwistConstraint* obj, btScalar* value);
	EXPORT void btConeTwistConstraint_getInfo1NonVirtual(btConeTwistConstraint* obj, btTypedConstraint_btConstraintInfo1* info);
	EXPORT void btConeTwistConstraint_getInfo2NonVirtual(btConeTwistConstraint* obj, btTypedConstraint_btConstraintInfo2* info, const btScalar* transA, const btScalar* transB, const btScalar* invInertiaWorldA, const btScalar* invInertiaWorldB);
	EXPORT btScalar btConeTwistConstraint_getLimit(btConeTwistConstraint* obj, int limitIndex);
	EXPORT btScalar btConeTwistConstraint_getLimitSoftness(btConeTwistConstraint* obj);
	EXPORT btScalar btConeTwistConstraint_getMaxMotorImpulse(btConeTwistConstraint* obj);
	EXPORT void btConeTwistConstraint_getMotorTarget(btConeTwistConstraint* obj, btScalar* q);
	EXPORT void btConeTwistConstraint_GetPointForAngle(btConeTwistConstraint* obj, btScalar fAngleInRadians, btScalar fLength, btScalar* value);
	EXPORT btScalar btConeTwistConstraint_getRelaxationFactor(btConeTwistConstraint* obj);
	EXPORT int btConeTwistConstraint_getSolveSwingLimit(btConeTwistConstraint* obj);
	EXPORT int btConeTwistConstraint_getSolveTwistLimit(btConeTwistConstraint* obj);
	EXPORT btScalar btConeTwistConstraint_getSwingSpan1(btConeTwistConstraint* obj);
	EXPORT btScalar btConeTwistConstraint_getSwingSpan2(btConeTwistConstraint* obj);
	EXPORT btScalar btConeTwistConstraint_getTwistAngle(btConeTwistConstraint* obj);
	EXPORT btScalar btConeTwistConstraint_getTwistLimitSign(btConeTwistConstraint* obj);
	EXPORT btScalar btConeTwistConstraint_getTwistSpan(btConeTwistConstraint* obj);
	EXPORT bool btConeTwistConstraint_isMaxMotorImpulseNormalized(btConeTwistConstraint* obj);
	EXPORT bool btConeTwistConstraint_isMotorEnabled(btConeTwistConstraint* obj);
	EXPORT bool btConeTwistConstraint_isPastSwingLimit(btConeTwistConstraint* obj);
	EXPORT void btConeTwistConstraint_setAngularOnly(btConeTwistConstraint* obj, bool angularOnly);
	EXPORT void btConeTwistConstraint_setDamping(btConeTwistConstraint* obj, btScalar damping);
	EXPORT void btConeTwistConstraint_setFixThresh(btConeTwistConstraint* obj, btScalar fixThresh);
	EXPORT void btConeTwistConstraint_setFrames(btConeTwistConstraint* obj, const btScalar* frameA, const btScalar* frameB);
	EXPORT void btConeTwistConstraint_setLimit(btConeTwistConstraint* obj, int limitIndex, btScalar limitValue);
	EXPORT void btConeTwistConstraint_setLimit2(btConeTwistConstraint* obj, btScalar _swingSpan1, btScalar _swingSpan2, btScalar _twistSpan);
	EXPORT void btConeTwistConstraint_setLimit3(btConeTwistConstraint* obj, btScalar _swingSpan1, btScalar _swingSpan2, btScalar _twistSpan, btScalar _softness);
	EXPORT void btConeTwistConstraint_setLimit4(btConeTwistConstraint* obj, btScalar _swingSpan1, btScalar _swingSpan2, btScalar _twistSpan, btScalar _softness, btScalar _biasFactor);
	EXPORT void btConeTwistConstraint_setLimit5(btConeTwistConstraint* obj, btScalar _swingSpan1, btScalar _swingSpan2, btScalar _twistSpan, btScalar _softness, btScalar _biasFactor, btScalar _relaxationFactor);
	EXPORT void btConeTwistConstraint_setMaxMotorImpulse(btConeTwistConstraint* obj, btScalar maxMotorImpulse);
	EXPORT void btConeTwistConstraint_setMaxMotorImpulseNormalized(btConeTwistConstraint* obj, btScalar maxMotorImpulse);
	EXPORT void btConeTwistConstraint_setMotorTarget(btConeTwistConstraint* obj, const btScalar* q);
	EXPORT void btConeTwistConstraint_setMotorTargetInConstraintSpace(btConeTwistConstraint* obj, const btScalar* q);
	EXPORT void btConeTwistConstraint_updateRHS(btConeTwistConstraint* obj, btScalar timeStep);
}
