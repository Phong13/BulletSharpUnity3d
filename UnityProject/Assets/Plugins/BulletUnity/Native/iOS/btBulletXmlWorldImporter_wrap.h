#include "main.h"

extern "C"
{
	EXPORT btBulletXmlWorldImporter* btBulletXmlWorldImporter_new(btDynamicsWorld* world);
	EXPORT bool btBulletXmlWorldImporter_loadFile(btBulletXmlWorldImporter* obj, const char* fileName);
}
