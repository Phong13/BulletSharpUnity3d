#include "main.h"

extern "C"
{
	EXPORT btLemkeSolver* btLemkeSolver_new();
	EXPORT int btLemkeSolver_getDebugLevel(btLemkeSolver* obj);
	EXPORT int btLemkeSolver_getMaxLoops(btLemkeSolver* obj);
	EXPORT btScalar btLemkeSolver_getMaxValue(btLemkeSolver* obj);
	EXPORT bool btLemkeSolver_getUseLoHighBounds(btLemkeSolver* obj);
	EXPORT void btLemkeSolver_setDebugLevel(btLemkeSolver* obj, int value);
	EXPORT void btLemkeSolver_setMaxLoops(btLemkeSolver* obj, int value);
	EXPORT void btLemkeSolver_setMaxValue(btLemkeSolver* obj, btScalar value);
	EXPORT void btLemkeSolver_setUseLoHighBounds(btLemkeSolver* obj, bool value);
}
