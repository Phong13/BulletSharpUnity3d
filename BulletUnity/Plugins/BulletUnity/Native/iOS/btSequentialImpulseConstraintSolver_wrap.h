#include "main.h"

extern "C"
{
	EXPORT btSequentialImpulseConstraintSolver* btSequentialImpulseConstraintSolver_new();
	EXPORT unsigned long btSequentialImpulseConstraintSolver_btRand2(btSequentialImpulseConstraintSolver* obj);
	EXPORT int btSequentialImpulseConstraintSolver_btRandInt2(btSequentialImpulseConstraintSolver* obj, int n);
	EXPORT unsigned long btSequentialImpulseConstraintSolver_getRandSeed(btSequentialImpulseConstraintSolver* obj);
	EXPORT void btSequentialImpulseConstraintSolver_setRandSeed(btSequentialImpulseConstraintSolver* obj, unsigned long seed);
}
