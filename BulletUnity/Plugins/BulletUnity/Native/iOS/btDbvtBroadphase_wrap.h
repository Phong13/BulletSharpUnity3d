#include "main.h"

extern "C"
{
	EXPORT btDbvtNode* btDbvtProxy_getLeaf(btDbvtProxy* obj);
	EXPORT btDbvtProxy** btDbvtProxy_getLinks(btDbvtProxy* obj);
	EXPORT int btDbvtProxy_getStage(btDbvtProxy* obj);
	EXPORT void btDbvtProxy_setLeaf(btDbvtProxy* obj, btDbvtNode* value);
	EXPORT void btDbvtProxy_setStage(btDbvtProxy* obj, int value);

	EXPORT btDbvtBroadphase* btDbvtBroadphase_new();
	EXPORT btDbvtBroadphase* btDbvtBroadphase_new2(btOverlappingPairCache* paircache);
	EXPORT void btDbvtBroadphase_benchmark(btBroadphaseInterface* __unnamed0);
	EXPORT void btDbvtBroadphase_collide(btDbvtBroadphase* obj, btDispatcher* dispatcher);
	EXPORT int btDbvtBroadphase_getCid(btDbvtBroadphase* obj);
	EXPORT int btDbvtBroadphase_getCupdates(btDbvtBroadphase* obj);
	EXPORT bool btDbvtBroadphase_getDeferedcollide(btDbvtBroadphase* obj);
	EXPORT int btDbvtBroadphase_getDupdates(btDbvtBroadphase* obj);
	EXPORT int btDbvtBroadphase_getFixedleft(btDbvtBroadphase* obj);
	EXPORT int btDbvtBroadphase_getFupdates(btDbvtBroadphase* obj);
	EXPORT int btDbvtBroadphase_getGid(btDbvtBroadphase* obj);
	EXPORT bool btDbvtBroadphase_getNeedcleanup(btDbvtBroadphase* obj);
	EXPORT int btDbvtBroadphase_getNewpairs(btDbvtBroadphase* obj);
	EXPORT btOverlappingPairCache* btDbvtBroadphase_getPaircache(btDbvtBroadphase* obj);
	EXPORT int btDbvtBroadphase_getPid(btDbvtBroadphase* obj);
	EXPORT btScalar btDbvtBroadphase_getPrediction(btDbvtBroadphase* obj);
	EXPORT bool btDbvtBroadphase_getReleasepaircache(btDbvtBroadphase* obj);
	EXPORT btDbvt* btDbvtBroadphase_getSets(btDbvtBroadphase* obj);
	EXPORT int btDbvtBroadphase_getStageCurrent(btDbvtBroadphase* obj);
	EXPORT btDbvtProxy** btDbvtBroadphase_getStageRoots(btDbvtBroadphase* obj);
	EXPORT unsigned int btDbvtBroadphase_getUpdates_call(btDbvtBroadphase* obj);
	EXPORT unsigned int btDbvtBroadphase_getUpdates_done(btDbvtBroadphase* obj);
	EXPORT btScalar btDbvtBroadphase_getUpdates_ratio(btDbvtBroadphase* obj);
	EXPORT btScalar btDbvtBroadphase_getVelocityPrediction(btDbvtBroadphase* obj);
	EXPORT void btDbvtBroadphase_optimize(btDbvtBroadphase* obj);
	EXPORT void btDbvtBroadphase_performDeferredRemoval(btDbvtBroadphase* obj, btDispatcher* dispatcher);
	EXPORT void btDbvtBroadphase_setAabbForceUpdate(btDbvtBroadphase* obj, btBroadphaseProxy* absproxy, const btScalar* aabbMin, const btScalar* aabbMax, btDispatcher* __unnamed3);
	EXPORT void btDbvtBroadphase_setCid(btDbvtBroadphase* obj, int value);
	EXPORT void btDbvtBroadphase_setCupdates(btDbvtBroadphase* obj, int value);
	EXPORT void btDbvtBroadphase_setDeferedcollide(btDbvtBroadphase* obj, bool value);
	EXPORT void btDbvtBroadphase_setDupdates(btDbvtBroadphase* obj, int value);
	EXPORT void btDbvtBroadphase_setFixedleft(btDbvtBroadphase* obj, int value);
	EXPORT void btDbvtBroadphase_setFupdates(btDbvtBroadphase* obj, int value);
	EXPORT void btDbvtBroadphase_setGid(btDbvtBroadphase* obj, int value);
	EXPORT void btDbvtBroadphase_setNeedcleanup(btDbvtBroadphase* obj, bool value);
	EXPORT void btDbvtBroadphase_setNewpairs(btDbvtBroadphase* obj, int value);
	EXPORT void btDbvtBroadphase_setPaircache(btDbvtBroadphase* obj, btOverlappingPairCache* value);
	EXPORT void btDbvtBroadphase_setPid(btDbvtBroadphase* obj, int value);
	EXPORT void btDbvtBroadphase_setPrediction(btDbvtBroadphase* obj, btScalar value);
	EXPORT void btDbvtBroadphase_setReleasepaircache(btDbvtBroadphase* obj, bool value);
	EXPORT void btDbvtBroadphase_setStageCurrent(btDbvtBroadphase* obj, int value);
	EXPORT void btDbvtBroadphase_setUpdates_call(btDbvtBroadphase* obj, unsigned int value);
	EXPORT void btDbvtBroadphase_setUpdates_done(btDbvtBroadphase* obj, unsigned int value);
	EXPORT void btDbvtBroadphase_setUpdates_ratio(btDbvtBroadphase* obj, btScalar value);
	EXPORT void btDbvtBroadphase_setVelocityPrediction(btDbvtBroadphase* obj, btScalar prediction);
}
