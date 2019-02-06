#include "main.h"

extern "C"
{
	EXPORT btSoftBodyRigidBodyCollisionConfiguration* btSoftBodyRigidBodyCollisionConfiguration_new();
	EXPORT btSoftBodyRigidBodyCollisionConfiguration* btSoftBodyRigidBodyCollisionConfiguration_new2(const btDefaultCollisionConstructionInfo* constructionInfo);
}
