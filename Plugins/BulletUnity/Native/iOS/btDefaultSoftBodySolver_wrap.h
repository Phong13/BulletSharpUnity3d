#include "main.h"

extern "C"
{
	EXPORT btDefaultSoftBodySolver* btDefaultSoftBodySolver_new();
	EXPORT void btDefaultSoftBodySolver_copySoftBodyToVertexBuffer(btDefaultSoftBodySolver* obj, const btSoftBody* softBody, btVertexBufferDescriptor* vertexBuffer);
}
