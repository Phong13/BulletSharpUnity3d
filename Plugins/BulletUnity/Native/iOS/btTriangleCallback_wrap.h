#include "main.h"

#ifndef BT_TRIANGLE_CALLBACK_H
#define pInternalTriangleIndexCallback_InternalProcessTriangleIndex void*
#define pTriangleCallback_ProcessTriangle void*
#define btInternalTriangleIndexCallbackWrapper void
#define btTriangleCallbackWrapper void
#else
typedef void (*pInternalTriangleIndexCallback_InternalProcessTriangleIndex)(btVector3* triangle, int partId, int triangleIndex);

class btInternalTriangleIndexCallbackWrapper : public btInternalTriangleIndexCallback
{
private:
	pInternalTriangleIndexCallback_InternalProcessTriangleIndex _internalProcessTriangleIndexCallback;

public:
	btInternalTriangleIndexCallbackWrapper(pInternalTriangleIndexCallback_InternalProcessTriangleIndex internalProcessTriangleIndexCallback);

	virtual void internalProcessTriangleIndex(btVector3* triangle, int partId, int triangleIndex);
};

typedef void (*pTriangleCallback_ProcessTriangle)(btVector3* triangle, int partId, int triangleIndex);

class btTriangleCallbackWrapper : public btTriangleCallback
{
private:
	pTriangleCallback_ProcessTriangle _processTriangleCallback;

public:
	btTriangleCallbackWrapper(pTriangleCallback_ProcessTriangle processTriangleCallback);

	virtual void processTriangle(btVector3* triangle, int partId, int triangleIndex);
};
#endif

extern "C"
{
	EXPORT btTriangleCallbackWrapper* btTriangleCallbackWrapper_new(pTriangleCallback_ProcessTriangle processTriangleCallback);

	EXPORT void btTriangleCallback_delete(btTriangleCallback* obj);

	EXPORT btInternalTriangleIndexCallbackWrapper* btInternalTriangleIndexCallbackWrapper_new(pInternalTriangleIndexCallback_InternalProcessTriangleIndex internalProcessTriangleIndexCallback);

	EXPORT void btInternalTriangleIndexCallback_delete(btInternalTriangleIndexCallback* obj);
}
