#include "main.h"

extern "C"
{
	EXPORT btHeightfieldTerrainShape* btHeightfieldTerrainShape_new(int heightStickWidth, int heightStickLength, const void* heightfieldData, btScalar heightScale, btScalar minHeight, btScalar maxHeight, int upAxis, PHY_ScalarType heightDataType, bool flipQuadEdges);
	EXPORT btHeightfieldTerrainShape* btHeightfieldTerrainShape_new2(int heightStickWidth, int heightStickLength, const void* heightfieldData, btScalar maxHeight, int upAxis, bool useFloatData, bool flipQuadEdges);
	EXPORT void btHeightfieldTerrainShape_setUseDiamondSubdivision(btHeightfieldTerrainShape* obj);
	EXPORT void btHeightfieldTerrainShape_setUseDiamondSubdivision2(btHeightfieldTerrainShape* obj, bool useDiamondSubdivision);
	EXPORT void btHeightfieldTerrainShape_setUseZigzagSubdivision(btHeightfieldTerrainShape* obj);
	EXPORT void btHeightfieldTerrainShape_setUseZigzagSubdivision2(btHeightfieldTerrainShape* obj, bool useZigzagSubdivision);
}
