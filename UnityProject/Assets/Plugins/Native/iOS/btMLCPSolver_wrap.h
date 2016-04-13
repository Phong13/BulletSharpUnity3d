#include "main.h"

extern "C"
{
	EXPORT btMLCPSolver* btMLCPSolver_new(btMLCPSolverInterface* solver);
	EXPORT int btMLCPSolver_getNumFallbacks(btMLCPSolver* obj);
	EXPORT void btMLCPSolver_setMLCPSolver(btMLCPSolver* obj, btMLCPSolverInterface* solver);
	EXPORT void btMLCPSolver_setNumFallbacks(btMLCPSolver* obj, int num);
}
