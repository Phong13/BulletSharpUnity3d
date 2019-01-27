#include "main.h"

extern "C"
{
	EXPORT btEmptyAlgorithm_CreateFunc* btEmptyAlgorithm_CreateFunc_new();

	EXPORT btEmptyAlgorithm* btEmptyAlgorithm_new(const btCollisionAlgorithmConstructionInfo* ci);
}
