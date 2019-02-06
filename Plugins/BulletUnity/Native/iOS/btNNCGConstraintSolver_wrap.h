#include "main.h"

extern "C"
{
	EXPORT btNNCGConstraintSolver* btNNCGConstraintSolver_new();
	EXPORT bool btNNCGConstraintSolver_getOnlyForNoneContact(btNNCGConstraintSolver* obj);
	EXPORT void btNNCGConstraintSolver_setOnlyForNoneContact(btNNCGConstraintSolver* obj, bool value);
}
