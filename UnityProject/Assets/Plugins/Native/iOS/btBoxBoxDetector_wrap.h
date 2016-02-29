#include "main.h"

extern "C"
{
	EXPORT btBoxBoxDetector* btBoxBoxDetector_new(const btBoxShape* box1, const btBoxShape* box2);
	EXPORT const btBoxShape* btBoxBoxDetector_getBox1(btBoxBoxDetector* obj);
	EXPORT const btBoxShape* btBoxBoxDetector_getBox2(btBoxBoxDetector* obj);
	EXPORT void btBoxBoxDetector_setBox1(btBoxBoxDetector* obj, const btBoxShape* value);
	EXPORT void btBoxBoxDetector_setBox2(btBoxBoxDetector* obj, const btBoxShape* value);
}
