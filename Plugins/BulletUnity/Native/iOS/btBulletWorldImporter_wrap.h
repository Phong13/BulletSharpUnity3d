#include "main.h"

extern "C"
{
	EXPORT btBulletWorldImporter* btBulletWorldImporter_new();
	EXPORT btBulletWorldImporter* btBulletWorldImporter_new2(btDynamicsWorld* world);
	EXPORT bool btBulletWorldImporter_convertAllObjects(btBulletWorldImporter* obj, bParse_btBulletFile* file);
	EXPORT bool btBulletWorldImporter_loadFile(btBulletWorldImporter* obj, const char* fileName);
	EXPORT bool btBulletWorldImporter_loadFile2(btBulletWorldImporter* obj, const char* fileName, const char* preSwapFilenameOut);
	EXPORT bool btBulletWorldImporter_loadFileFromMemory(btBulletWorldImporter* obj, char* memoryBuffer, int len);
	EXPORT bool btBulletWorldImporter_loadFileFromMemory2(btBulletWorldImporter* obj, bParse_btBulletFile* file);
}
