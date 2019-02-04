#include "main.h"

extern "C"
{
	EXPORT btHingeConstraint* btHingeConstraint_new(btRigidBody* rbA, btRigidBody* rbB, const btScalar* pivotInA, const btScalar* pivotInB, const btScalar* axisInA, const btScalar* axisInB);
	EXPORT btHingeConstraint* btHingeConstraint_new2(btRigidBody* rbA, btRigidBody* rbB, const btScalar* pivotInA, const btScalar* pivotInB, const btScalar* axisInA, const btScalar* axisInB, bool useReferenceFrameA);
	EXPORT btHingeConstraint* btHingeConstraint_new3(btRigidBody* rbA, const btScalar* pivotInA, const btScalar* axisInA);
	EXPORT btHingeConstraint* btHingeConstraint_new4(btRigidBody* rbA, const btScalar* pivotInA, const btScalar* axisInA, bool useReferenceFrameA);
	EXPORT btHingeConstraint* btHingeConstraint_new5(btRigidBody* rbA, btRigidBody* rbB, const btScalar* rbAFrame, const btScalar* rbBFrame);
	EXPORT btHingeConstraint* btHingeConstraint_new6(btRigidBody* rbA, btRigidBody* rbB, const btScalar* rbAFrame, const btScalar* rbBFrame, bool useReferenceFrameA);
	EXPORT btHingeConstraint* btHingeConstraint_new7(btRigidBody* rbA, const btScalar* rbAFrame);
	EXPORT btHingeConstraint* btHingeConstraint_new8(btRigidBody* rbA, const btScalar* rbAFrame, bool useReferenceFrameA);
	EXPORT void btHingeConstraint_enableAngularMotor(btHingeConstraint* obj, bool enableMotor, btScalar targetVelocity, btScalar maxMotorImpulse);
	EXPORT void btHingeConstraint_enableMotor(btHingeConstraint* obj, bool enableMotor);
	EXPORT void btHingeConstraint_getAFrame(btHingeConstraint* obj, btScalar* value);
	EXPORT bool btHingeConstraint_getAngularOnly(btHingeConstraint* obj);
	EXPORT void btHingeConstraint_getBFrame(btHingeConstraint* obj, btScalar* value);
	EXPORT bool btHingeConstraint_getEnableAngularMotor(btHingeConstraint* obj);
	EXPORT int btHingeConstraint_getFlags(btHingeConstraint* obj);
	EXPORT void btHingeConstraint_getFrameOffsetA(btHingeConstraint* obj, btScalar* value);
	EXPORT void btHingeConstraint_getFrameOffsetB(btHingeConstraint* obj, btScalar* value);
	EXPORT btScalar btHingeConstraint_getHingeAngle(btHingeConstraint* obj, const btScalar* transA, const btScalar* transB);
	EXPORT btScalar btHingeConstraint_getHingeAngle2(btHingeConstraint* obj);
	EXPORT void btHingeConstraint_getInfo1NonVirtual(btHingeConstraint* obj, btTypedConstraint_btConstraintInfo1* info);
	EXPORT void btHingeConstraint_getInfo2Internal(btHingeConstraint* obj, btTypedConstraint_btConstraintInfo2* info, const btScalar* transA, const btScalar* transB, const btScalar* angVelA, const btScalar* angVelB);
	EXPORT void btHingeConstraint_getInfo2InternalUsingFrameOffset(btHingeConstraint* obj, btTypedConstraint_btConstraintInfo2* info, const btScalar* transA, const btScalar* transB, const btScalar* angVelA, const btScalar* angVelB);
	EXPORT void btHingeConstraint_getInfo2NonVirtual(btHingeConstraint* obj, btTypedConstraint_btConstraintInfo2* info, const btScalar* transA, const btScalar* transB, const btScalar* angVelA, const btScalar* angVelB);
	EXPORT btScalar btHingeConstraint_getLimitBiasFactor(btHingeConstraint* obj);
	EXPORT btScalar btHingeConstraint_getLimitRelaxationFactor(btHingeConstraint* obj);
	EXPORT btScalar btHingeConstraint_getLimitSign(btHingeConstraint* obj);
	EXPORT btScalar btHingeConstraint_getLimitSoftness(btHingeConstraint* obj);
	EXPORT btScalar btHingeConstraint_getLowerLimit(btHingeConstraint* obj);
	EXPORT btScalar btHingeConstraint_getMaxMotorImpulse(btHingeConstraint* obj);
	EXPORT btScalar btHingeConstraint_getMotorTargetVelosity(btHingeConstraint* obj);
	EXPORT int btHingeConstraint_getSolveLimit(btHingeConstraint* obj);
	EXPORT btScalar btHingeConstraint_getUpperLimit(btHingeConstraint* obj);
	EXPORT bool btHingeConstraint_getUseFrameOffset(btHingeConstraint* obj);
	EXPORT bool btHingeConstraint_getUseReferenceFrameA(btHingeConstraint* obj);
	EXPORT bool btHingeConstraint_hasLimit(btHingeConstraint* obj);
	EXPORT void btHingeConstraint_setAngularOnly(btHingeConstraint* obj, bool angularOnly);
	EXPORT void btHingeConstraint_setAxis(btHingeConstraint* obj, btScalar* axisInA);
	EXPORT void btHingeConstraint_setFrames(btHingeConstraint* obj, const btScalar* frameA, const btScalar* frameB);
	EXPORT void btHingeConstraint_setLimit(btHingeConstraint* obj, btScalar low, btScalar high);
	EXPORT void btHingeConstraint_setLimit2(btHingeConstraint* obj, btScalar low, btScalar high, btScalar _softness);
	EXPORT void btHingeConstraint_setLimit3(btHingeConstraint* obj, btScalar low, btScalar high, btScalar _softness, btScalar _biasFactor);
	EXPORT void btHingeConstraint_setLimit4(btHingeConstraint* obj, btScalar low, btScalar high, btScalar _softness, btScalar _biasFactor, btScalar _relaxationFactor);
	EXPORT void btHingeConstraint_setMaxMotorImpulse(btHingeConstraint* obj, btScalar maxMotorImpulse);
	EXPORT void btHingeConstraint_setMotorTarget(btHingeConstraint* obj, btScalar targetAngle, btScalar dt);
	EXPORT void btHingeConstraint_setMotorTarget2(btHingeConstraint* obj, const btScalar* qAinB, btScalar dt);
	EXPORT void btHingeConstraint_setMotorTargetVelocity(btHingeConstraint* obj, btScalar motorTargetVelocity);
	EXPORT void btHingeConstraint_setUseFrameOffset(btHingeConstraint* obj, bool frameOffsetOnOff);
	EXPORT void btHingeConstraint_setUseReferenceFrameA(btHingeConstraint* obj, bool useReferenceFrameA);
	EXPORT void btHingeConstraint_testLimit(btHingeConstraint* obj, const btScalar* transA, const btScalar* transB);
	EXPORT void btHingeConstraint_updateRHS(btHingeConstraint* obj, btScalar timeStep);

	EXPORT btHingeAccumulatedAngleConstraint* btHingeAccumulatedAngleConstraint_new(btRigidBody* rbA, btRigidBody* rbB, const btScalar* pivotInA, const btScalar* pivotInB, const btScalar* axisInA, const btScalar* axisInB);
	EXPORT btHingeAccumulatedAngleConstraint* btHingeAccumulatedAngleConstraint_new2(btRigidBody* rbA, btRigidBody* rbB, const btScalar* pivotInA, const btScalar* pivotInB, const btScalar* axisInA, const btScalar* axisInB, bool useReferenceFrameA);
	EXPORT btHingeAccumulatedAngleConstraint* btHingeAccumulatedAngleConstraint_new3(btRigidBody* rbA, const btScalar* pivotInA, const btScalar* axisInA);
	EXPORT btHingeAccumulatedAngleConstraint* btHingeAccumulatedAngleConstraint_new4(btRigidBody* rbA, const btScalar* pivotInA, const btScalar* axisInA, bool useReferenceFrameA);
	EXPORT btHingeAccumulatedAngleConstraint* btHingeAccumulatedAngleConstraint_new5(btRigidBody* rbA, btRigidBody* rbB, const btScalar* rbAFrame, const btScalar* rbBFrame);
	EXPORT btHingeAccumulatedAngleConstraint* btHingeAccumulatedAngleConstraint_new6(btRigidBody* rbA, btRigidBody* rbB, const btScalar* rbAFrame, const btScalar* rbBFrame, bool useReferenceFrameA);
	EXPORT btHingeAccumulatedAngleConstraint* btHingeAccumulatedAngleConstraint_new7(btRigidBody* rbA, const btScalar* rbAFrame);
	EXPORT btHingeAccumulatedAngleConstraint* btHingeAccumulatedAngleConstraint_new8(btRigidBody* rbA, const btScalar* rbAFrame, bool useReferenceFrameA);
	EXPORT btScalar btHingeAccumulatedAngleConstraint_getAccumulatedHingeAngle(btHingeAccumulatedAngleConstraint* obj);
	EXPORT void btHingeAccumulatedAngleConstraint_setAccumulatedHingeAngle(btHingeAccumulatedAngleConstraint* obj, btScalar accAngle);
}
