#include "main.h"

extern "C"
{
	//EXPORT void btSimulationIslandManager_IslandCallback_processIsland(btSimulationIslandManager_IslandCallback* obj, * bodies, int numBodies, * manifolds, int numManifolds, int islandId);
	EXPORT void btSimulationIslandManager_IslandCallback_delete(btSimulationIslandManager_IslandCallback* obj);

	EXPORT btSimulationIslandManager* btSimulationIslandManager_new();
	EXPORT void btSimulationIslandManager_buildAndProcessIslands(btSimulationIslandManager* obj, btDispatcher* dispatcher, btCollisionWorld* collisionWorld, btSimulationIslandManager_IslandCallback* callback);
	EXPORT void btSimulationIslandManager_buildIslands(btSimulationIslandManager* obj, btDispatcher* dispatcher, btCollisionWorld* colWorld);
	EXPORT void btSimulationIslandManager_findUnions(btSimulationIslandManager* obj, btDispatcher* dispatcher, btCollisionWorld* colWorld);
	EXPORT bool btSimulationIslandManager_getSplitIslands(btSimulationIslandManager* obj);
	EXPORT btUnionFind* btSimulationIslandManager_getUnionFind(btSimulationIslandManager* obj);
	EXPORT void btSimulationIslandManager_initUnionFind(btSimulationIslandManager* obj, int n);
	EXPORT void btSimulationIslandManager_setSplitIslands(btSimulationIslandManager* obj, bool doSplitIslands);
	EXPORT void btSimulationIslandManager_storeIslandActivationState(btSimulationIslandManager* obj, btCollisionWorld* world);
	EXPORT void btSimulationIslandManager_updateActivationState(btSimulationIslandManager* obj, btCollisionWorld* colWorld, btDispatcher* dispatcher);
	EXPORT void btSimulationIslandManager_delete(btSimulationIslandManager* obj);
}
