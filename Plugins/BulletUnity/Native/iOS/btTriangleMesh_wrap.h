#include "main.h"

extern "C"
{
	EXPORT btTriangleMesh* btTriangleMesh_new();
	EXPORT btTriangleMesh* btTriangleMesh_new2(bool use32bitIndices);
	EXPORT btTriangleMesh* btTriangleMesh_new3(bool use32bitIndices, bool use4componentVertices);
	EXPORT void btTriangleMesh_addIndex(btTriangleMesh* obj, int index);
	EXPORT void btTriangleMesh_addTriangle(btTriangleMesh* obj, const btScalar* vertex0, const btScalar* vertex1, const btScalar* vertex2);
	EXPORT void btTriangleMesh_addTriangle2(btTriangleMesh* obj, const btScalar* vertex0, const btScalar* vertex1, const btScalar* vertex2, bool removeDuplicateVertices);
	EXPORT void btTriangleMesh_addTriangleIndices(btTriangleMesh* obj, int index1, int index2, int index3);
	EXPORT int btTriangleMesh_findOrAddVertex(btTriangleMesh* obj, const btScalar* vertex, bool removeDuplicateVertices);
	EXPORT int btTriangleMesh_getNumTriangles(btTriangleMesh* obj);
	EXPORT bool btTriangleMesh_getUse32bitIndices(btTriangleMesh* obj);
	EXPORT bool btTriangleMesh_getUse4componentVertices(btTriangleMesh* obj);
	EXPORT btScalar btTriangleMesh_getWeldingThreshold(btTriangleMesh* obj);
	EXPORT void btTriangleMesh_setWeldingThreshold(btTriangleMesh* obj, btScalar value);
}
