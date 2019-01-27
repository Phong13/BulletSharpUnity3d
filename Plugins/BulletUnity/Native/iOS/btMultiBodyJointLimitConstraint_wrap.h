#include "main.h"

extern "C"
{
	EXPORT btMultiBodyJointLimitConstraint* btMultiBodyJointLimitConstraint_new(btMultiBody* body, int link, btScalar lower, btScalar upper);
}
