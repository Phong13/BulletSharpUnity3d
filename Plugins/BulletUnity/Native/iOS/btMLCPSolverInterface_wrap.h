#include "main.h"

extern "C"
{
	//EXPORT bool btMLCPSolverInterface_solveMLCP(btMLCPSolverInterface* obj, const btMatrixXf* A, const btVectorXf* b, btVectorXf* x, const btVectorXf* lo, const btVectorXf* hi, const btAlignedIntArray* limitDependency, int numIterations);
	//EXPORT bool btMLCPSolverInterface_solveMLCP2(btMLCPSolverInterface* obj, const btMatrixXf* A, const btVectorXf* b, btVectorXf* x, const btVectorXf* lo, const btVectorXf* hi, const btAlignedIntArray* limitDependency, int numIterations, bool useSparsity);
	EXPORT void btMLCPSolverInterface_delete(btMLCPSolverInterface* obj);
}
