#include "main.h"

extern "C"
{
	EXPORT btIndexedMesh* btIndexedMesh_new();
	EXPORT PHY_ScalarType btIndexedMesh_getIndexType(btIndexedMesh* obj);
	EXPORT int btIndexedMesh_getNumTriangles(btIndexedMesh* obj);
	EXPORT int btIndexedMesh_getNumVertices(btIndexedMesh* obj);
	EXPORT const unsigned char* btIndexedMesh_getTriangleIndexBase(btIndexedMesh* obj);
	EXPORT int btIndexedMesh_getTriangleIndexStride(btIndexedMesh* obj);
	EXPORT const unsigned char* btIndexedMesh_getVertexBase(btIndexedMesh* obj);
	EXPORT int btIndexedMesh_getVertexStride(btIndexedMesh* obj);
	EXPORT PHY_ScalarType btIndexedMesh_getVertexType(btIndexedMesh* obj);
	EXPORT void btIndexedMesh_setIndexType(btIndexedMesh* obj, PHY_ScalarType value);
	EXPORT void btIndexedMesh_setNumTriangles(btIndexedMesh* obj, int value);
	EXPORT void btIndexedMesh_setNumVertices(btIndexedMesh* obj, int value);
	EXPORT void btIndexedMesh_setTriangleIndexBase(btIndexedMesh* obj, const unsigned char* value);
	EXPORT void btIndexedMesh_setTriangleIndexStride(btIndexedMesh* obj, int value);
	EXPORT void btIndexedMesh_setVertexBase(btIndexedMesh* obj, const unsigned char* value);
	EXPORT void btIndexedMesh_setVertexStride(btIndexedMesh* obj, int value);
	EXPORT void btIndexedMesh_setVertexType(btIndexedMesh* obj, PHY_ScalarType value);
	EXPORT void btIndexedMesh_delete(btIndexedMesh* obj);

	EXPORT btTriangleIndexVertexArray* btTriangleIndexVertexArray_new();
	EXPORT btTriangleIndexVertexArray* btTriangleIndexVertexArray_new2(int numTriangles, int* triangleIndexBase, int triangleIndexStride, int numVertices, btScalar* vertexBase, int vertexStride);
	EXPORT void btTriangleIndexVertexArray_addIndexedMesh(btTriangleIndexVertexArray* obj, const btIndexedMesh* mesh);
	EXPORT void btTriangleIndexVertexArray_addIndexedMesh2(btTriangleIndexVertexArray* obj, const btIndexedMesh* mesh, PHY_ScalarType indexType);
	EXPORT IndexedMeshArray* btTriangleIndexVertexArray_getIndexedMeshArray(btTriangleIndexVertexArray* obj);
}
