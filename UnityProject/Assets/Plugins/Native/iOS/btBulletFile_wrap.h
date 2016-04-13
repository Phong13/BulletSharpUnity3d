#include "main.h"

extern "C"
{
	EXPORT bParse_btBulletFile* bParse_btBulletFile_new();
	EXPORT bParse_btBulletFile* bParse_btBulletFile_new2(const char* fileName);
	EXPORT bParse_btBulletFile* bParse_btBulletFile_new3(char* memoryBuffer, int len);
	EXPORT void bParse_btBulletFile_addDataBlock(bParse_btBulletFile* obj, char* dataBlock);
	EXPORT void bParse_btBulletFile_addStruct(bParse_btBulletFile* obj, const char* structType, void* data, int len, void* oldPtr, int code);
	EXPORT btAlignedStructHandleArray* bParse_btBulletFile_getBvhs(bParse_btBulletFile* obj);
	EXPORT btAlignedStructHandleArray* bParse_btBulletFile_getCollisionObjects(bParse_btBulletFile* obj);
	EXPORT btAlignedStructHandleArray* bParse_btBulletFile_getCollisionShapes(bParse_btBulletFile* obj);
	EXPORT btAlignedStructHandleArray* bParse_btBulletFile_getConstraints(bParse_btBulletFile* obj);
	EXPORT btAligendCharPtrArray* bParse_btBulletFile_getDataBlocks(bParse_btBulletFile* obj);
	EXPORT btAlignedStructHandleArray* bParse_btBulletFile_getDynamicsWorldInfo(bParse_btBulletFile* obj);
	EXPORT btAlignedStructHandleArray* bParse_btBulletFile_getMultiBodies(bParse_btBulletFile* obj);
	EXPORT btAlignedStructHandleArray* bParse_btBulletFile_getRigidBodies(bParse_btBulletFile* obj);
	EXPORT btAlignedStructHandleArray* bParse_btBulletFile_getSoftBodies(bParse_btBulletFile* obj);
	EXPORT btAlignedStructHandleArray* bParse_btBulletFile_getTriangleInfoMaps(bParse_btBulletFile* obj);
	EXPORT void bParse_btBulletFile_parse(bParse_btBulletFile* obj, int verboseMode);
	EXPORT void bParse_btBulletFile_parseData(bParse_btBulletFile* obj);
	EXPORT int bParse_btBulletFile_write(bParse_btBulletFile* obj, const char* fileName);
	EXPORT int bParse_btBulletFile_write2(bParse_btBulletFile* obj, const char* fileName, bool fixupPointers);
	EXPORT void bParse_btBulletFile_writeDNA(bParse_btBulletFile* obj, FILE* fp);
	EXPORT void bParse_btBulletFile_delete(bParse_btBulletFile* obj);
}
