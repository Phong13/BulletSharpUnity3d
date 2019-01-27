#include "main.h"

extern "C"
{
	EXPORT btContactSolverInfoData* btContactSolverInfoData_new();
	EXPORT btScalar btContactSolverInfoData_getDamping(btContactSolverInfoData* obj);
	EXPORT btScalar btContactSolverInfoData_getErp(btContactSolverInfoData* obj);
	EXPORT btScalar btContactSolverInfoData_getErp2(btContactSolverInfoData* obj);
	EXPORT btScalar btContactSolverInfoData_getFriction(btContactSolverInfoData* obj);
	EXPORT btScalar btContactSolverInfoData_getGlobalCfm(btContactSolverInfoData* obj);
	EXPORT btScalar btContactSolverInfoData_getLinearSlop(btContactSolverInfoData* obj);
	EXPORT btScalar btContactSolverInfoData_getMaxErrorReduction(btContactSolverInfoData* obj);
	EXPORT btScalar btContactSolverInfoData_getMaxGyroscopicForce(btContactSolverInfoData* obj);
	EXPORT int btContactSolverInfoData_getMinimumSolverBatchSize(btContactSolverInfoData* obj);
	EXPORT int btContactSolverInfoData_getNumIterations(btContactSolverInfoData* obj);
	EXPORT int btContactSolverInfoData_getRestingContactRestitutionThreshold(btContactSolverInfoData* obj);
	EXPORT btScalar btContactSolverInfoData_getRestitution(btContactSolverInfoData* obj);
	EXPORT btScalar btContactSolverInfoData_getSingleAxisRollingFrictionThreshold(btContactSolverInfoData* obj);
	EXPORT int btContactSolverInfoData_getSolverMode(btContactSolverInfoData* obj);
	EXPORT btScalar btContactSolverInfoData_getSor(btContactSolverInfoData* obj);
	EXPORT int btContactSolverInfoData_getSplitImpulse(btContactSolverInfoData* obj);
	EXPORT btScalar btContactSolverInfoData_getSplitImpulsePenetrationThreshold(btContactSolverInfoData* obj);
	EXPORT btScalar btContactSolverInfoData_getSplitImpulseTurnErp(btContactSolverInfoData* obj);
	EXPORT btScalar btContactSolverInfoData_getTau(btContactSolverInfoData* obj);
	EXPORT btScalar btContactSolverInfoData_getTimeStep(btContactSolverInfoData* obj);
	EXPORT btScalar btContactSolverInfoData_getWarmstartingFactor(btContactSolverInfoData* obj);
	EXPORT void btContactSolverInfoData_setDamping(btContactSolverInfoData* obj, btScalar value);
	EXPORT void btContactSolverInfoData_setErp(btContactSolverInfoData* obj, btScalar value);
	EXPORT void btContactSolverInfoData_setErp2(btContactSolverInfoData* obj, btScalar value);
	EXPORT void btContactSolverInfoData_setFriction(btContactSolverInfoData* obj, btScalar value);
	EXPORT void btContactSolverInfoData_setGlobalCfm(btContactSolverInfoData* obj, btScalar value);
	EXPORT void btContactSolverInfoData_setLinearSlop(btContactSolverInfoData* obj, btScalar value);
	EXPORT void btContactSolverInfoData_setMaxErrorReduction(btContactSolverInfoData* obj, btScalar value);
	EXPORT void btContactSolverInfoData_setMaxGyroscopicForce(btContactSolverInfoData* obj, btScalar value);
	EXPORT void btContactSolverInfoData_setMinimumSolverBatchSize(btContactSolverInfoData* obj, int value);
	EXPORT void btContactSolverInfoData_setNumIterations(btContactSolverInfoData* obj, int value);
	EXPORT void btContactSolverInfoData_setRestingContactRestitutionThreshold(btContactSolverInfoData* obj, int value);
	EXPORT void btContactSolverInfoData_setRestitution(btContactSolverInfoData* obj, btScalar value);
	EXPORT void btContactSolverInfoData_setSingleAxisRollingFrictionThreshold(btContactSolverInfoData* obj, btScalar value);
	EXPORT void btContactSolverInfoData_setSolverMode(btContactSolverInfoData* obj, int value);
	EXPORT void btContactSolverInfoData_setSor(btContactSolverInfoData* obj, btScalar value);
	EXPORT void btContactSolverInfoData_setSplitImpulse(btContactSolverInfoData* obj, int value);
	EXPORT void btContactSolverInfoData_setSplitImpulsePenetrationThreshold(btContactSolverInfoData* obj, btScalar value);
	EXPORT void btContactSolverInfoData_setSplitImpulseTurnErp(btContactSolverInfoData* obj, btScalar value);
	EXPORT void btContactSolverInfoData_setTau(btContactSolverInfoData* obj, btScalar value);
	EXPORT void btContactSolverInfoData_setTimeStep(btContactSolverInfoData* obj, btScalar value);
	EXPORT void btContactSolverInfoData_setWarmstartingFactor(btContactSolverInfoData* obj, btScalar value);
	EXPORT void btContactSolverInfoData_delete(btContactSolverInfoData* obj);

	EXPORT btContactSolverInfo* btContactSolverInfo_new();
}
