#include "main.h"

extern "C"
{
	EXPORT btPointCollector* btPointCollector_new();
	EXPORT btScalar btPointCollector_getDistance(btPointCollector* obj);
	EXPORT bool btPointCollector_getHasResult(btPointCollector* obj);
	EXPORT void btPointCollector_getNormalOnBInWorld(btPointCollector* obj, btScalar* value);
	EXPORT void btPointCollector_getPointInWorld(btPointCollector* obj, btScalar* value);
	EXPORT void btPointCollector_setDistance(btPointCollector* obj, btScalar value);
	EXPORT void btPointCollector_setHasResult(btPointCollector* obj, bool value);
	EXPORT void btPointCollector_setNormalOnBInWorld(btPointCollector* obj, const btScalar* value);
	EXPORT void btPointCollector_setPointInWorld(btPointCollector* obj, const btScalar* value);
}
