#include "main.h"

extern "C"
{
	EXPORT btBU_Simplex1to4* btBU_Simplex1to4_new();
	EXPORT btBU_Simplex1to4* btBU_Simplex1to4_new2(const btScalar* pt0);
	EXPORT btBU_Simplex1to4* btBU_Simplex1to4_new3(const btScalar* pt0, const btScalar* pt1);
	EXPORT btBU_Simplex1to4* btBU_Simplex1to4_new4(const btScalar* pt0, const btScalar* pt1, const btScalar* pt2);
	EXPORT btBU_Simplex1to4* btBU_Simplex1to4_new5(const btScalar* pt0, const btScalar* pt1, const btScalar* pt2, const btScalar* pt3);
	EXPORT void btBU_Simplex1to4_addVertex(btBU_Simplex1to4* obj, const btScalar* pt);
	EXPORT int btBU_Simplex1to4_getIndex(btBU_Simplex1to4* obj, int i);
	EXPORT void btBU_Simplex1to4_reset(btBU_Simplex1to4* obj);
}
