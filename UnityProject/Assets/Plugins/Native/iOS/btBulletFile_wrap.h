#include "main.h"

extern "C"
{
	EXPORT bParse_btBulletFile* btBulletFile_new();
	EXPORT bParse_btBulletFile* btBulletFile_new2(const char* fileName);
	EXPORT bParse_btBulletFile* btBulletFile_new3(char* memoryBuffer, int len);
	EXPORT void btBulletFile_addStruct(bParse_btBulletFile* obj, const char* structType, void* data, int len, void* oldPtr, int code);
	EXPORT btAlignedStructHandleArray* btBulletFile_getBvhs(bParse_btBulletFile* obj);
	EXPORT btAlignedStructHandleArray* btBulletFile_getCollisionObjects(bParse_btBulletFile* obj);
	EXPORT btAlignedStructHandleArray* btBulletFile_getCollisionShapes(bParse_btBulletFile* obj);
	EXPORT btAlignedStructHandleArray* btBulletFile_getConstraints(bParse_btBulletFile* obj);
	EXPORT btAligendCharPtrArray* btBulletFile_getDataBlocks(bParse_btBulletFile* obj);
	EXPORT btAlignedStructHandleArray* btBulletFile_getDynamicsWorldInfo(bParse_btBulletFile* obj);
	EXPORT btAlignedStructHandleArray* btBulletFile_getRigidBodies(bParse_btBulletFile* obj);
	EXPORT btAlignedStructHandleArray* btBulletFile_getSoftBodies(bParse_btBulletFile* obj);
	EXPORT btAlignedStructHandleArray* btBulletFile_getTriangleInfoMaps(bParse_btBulletFile* obj);
	EXPORT void btBulletFile_parseData(bParse_btBulletFile* obj);
}
