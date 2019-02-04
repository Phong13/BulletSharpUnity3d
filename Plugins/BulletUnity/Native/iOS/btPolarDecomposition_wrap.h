#include "main.h"

extern "C"
{
	EXPORT btPolarDecomposition* btPolarDecomposition_new();
	EXPORT btPolarDecomposition* btPolarDecomposition_new2(btScalar tolerance);
	EXPORT btPolarDecomposition* btPolarDecomposition_new3(btScalar tolerance, unsigned int maxIterations);
	EXPORT unsigned int btPolarDecomposition_decompose(btPolarDecomposition* obj, const btScalar* a, btScalar* u, btScalar* h);
	EXPORT unsigned int btPolarDecomposition_maxIterations(btPolarDecomposition* obj);
	EXPORT void btPolarDecomposition_delete(btPolarDecomposition* obj);
}
