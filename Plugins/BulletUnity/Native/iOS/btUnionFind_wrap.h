#include "main.h"

extern "C"
{
	EXPORT btElement* btElement_new();
	EXPORT int btElement_getId(btElement* obj);
	EXPORT int btElement_getSz(btElement* obj);
	EXPORT void btElement_setId(btElement* obj, int value);
	EXPORT void btElement_setSz(btElement* obj, int value);
	EXPORT void btElement_delete(btElement* obj);

	EXPORT btUnionFind* btUnionFind_new();
	EXPORT void btUnionFind_allocate(btUnionFind* obj, int N);
	EXPORT int btUnionFind_find(btUnionFind* obj, int p, int q);
	EXPORT int btUnionFind_find2(btUnionFind* obj, int x);
	EXPORT void btUnionFind_Free(btUnionFind* obj);
	EXPORT btElement* btUnionFind_getElement(btUnionFind* obj, int index);
	EXPORT int btUnionFind_getNumElements(btUnionFind* obj);
	EXPORT bool btUnionFind_isRoot(btUnionFind* obj, int x);
	EXPORT void btUnionFind_reset(btUnionFind* obj, int N);
	EXPORT void btUnionFind_sortIslands(btUnionFind* obj);
	EXPORT void btUnionFind_unite(btUnionFind* obj, int p, int q);
	EXPORT void btUnionFind_delete(btUnionFind* obj);
}
