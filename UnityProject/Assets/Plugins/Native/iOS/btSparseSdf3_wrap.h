#include "main.h"

extern "C"
{
	EXPORT btSparseSdf3* btSparseSdf_new();
	EXPORT void btSparseSdf3_GarbageCollect(btSparseSdf3* obj, int lifetime);
	EXPORT void btSparseSdf3_GarbageCollect2(btSparseSdf3* obj);
	EXPORT void btSparseSdf3_Initialize(btSparseSdf3* obj, int hashsize, int clampCells);
	EXPORT void btSparseSdf3_Initialize2(btSparseSdf3* obj, int hashsize);
	EXPORT void btSparseSdf3_Initialize3(btSparseSdf3* obj);
	EXPORT int btSparseSdf3_RemoveReferences(btSparseSdf3* obj, btCollisionShape* pcs);
	EXPORT void btSparseSdf3_Reset(btSparseSdf3* obj);
	EXPORT void btSparseSdf_delete(btSparseSdf3* obj);
}
