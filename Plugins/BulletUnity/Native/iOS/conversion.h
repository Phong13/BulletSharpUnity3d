#pragma once

#include <LinearMath/btAlignedAllocator.h>
#include <LinearMath/btVector3.h>
#include <LinearMath/btMatrix3x3.h>
#include <LinearMath/btQuickprof.h>
#include "LinearMath/btTransform.h"

#define BTTRANSFORM_TRANSPOSE
#define BTTRANSFORM_TO4X4

inline void btVector3ToVector3(const btVector3* v, btScalar* s)
{
	s[0] = v->getX();
	s[1] = v->getY();
	s[2] = v->getZ();
}

inline void btVector3ToVector3(const btVector3& v, btScalar* s)
{
	s[0] = v.getX();
	s[1] = v.getY();
	s[2] = v.getZ();
}

inline void Vector3TobtVector3(const btScalar* s, btVector3* v)
{
	v->setX(s[0]);
	v->setY(s[1]);
	v->setZ(s[2]);
}

inline btVector3* Vector3ArrayIn(const btScalar* va, int n)
{
	btVector3* vertices = new btVector3[n];
	for (int i = 0; i < n; i++) {
		Vector3TobtVector3(&va[i * 3], &vertices[i]);
	}
	return vertices;
}

inline void btVector4ToVector4(const btVector4* v, btScalar* s)
{
	s[0] = v->getX();
	s[1] = v->getY();
	s[2] = v->getZ();
	s[3] = v->getW();
}

inline void btVector4ToVector4(const btVector4& v, btScalar* s)
{
	s[0] = v.getX();
	s[1] = v.getY();
	s[2] = v.getZ();
	s[3] = v.getW();
}

inline void Vector4TobtVector4(const btScalar* s, btVector4* v)
{
	v->setX(s[0]);
	v->setY(s[1]);
	v->setZ(s[2]);
	v->setW(s[3]);
}

inline void btQuaternionToQuaternion(const btQuaternion* q, btScalar* s)
{
	s[0] = q->getX();
	s[1] = q->getY();
	s[2] = q->getZ();
	s[3] = q->getW();
}

inline void btQuaternionToQuaternion(const btQuaternion& q, btScalar* s)
{
	s[0] = q.getX();
	s[1] = q.getY();
	s[2] = q.getZ();
	s[3] = q.getW();
}

inline void QuaternionTobtQuaternion(const btScalar* s, btQuaternion* v)
{
	v->setX(s[0]);
	v->setY(s[1]);
	v->setZ(s[2]);
	v->setW(s[3]);
}

inline void btTransformToMatrix(const btTransform* t, btScalar* m)
{
#ifdef BTTRANSFORM_TO4X4
#ifdef BTTRANSFORM_TRANSPOSE
	m[0] = t->getBasis().getRow(0).getX();
	m[4] = t->getBasis().getRow(0).getY();
	m[8] = t->getBasis().getRow(0).getZ();
	m[1] = t->getBasis().getRow(1).getX();
	m[5] = t->getBasis().getRow(1).getY();
	m[9] = t->getBasis().getRow(1).getZ();
	m[2] = t->getBasis().getRow(2).getX();
	m[6] = t->getBasis().getRow(2).getY();
	m[10] = t->getBasis().getRow(2).getZ();
#else
	m[0] = t->getBasis().getRow(0).getX();
	m[1] = t->getBasis().getRow(0).getY();
	m[2] = t->getBasis().getRow(0).getZ();
	m[4] = t->getBasis().getRow(1).getX();
	m[5] = t->getBasis().getRow(1).getY();
	m[6] = t->getBasis().getRow(1).getZ();
	m[8] = t->getBasis().getRow(2).getX();
	m[9] = t->getBasis().getRow(2).getY();
	m[10] = t->getBasis().getRow(2).getZ();
#endif
	m[3] = 0;
	m[7] = 0;
	m[11] = 0;
	m[12] = t->getOrigin().getX();
	m[13] = t->getOrigin().getY();
	m[14] = t->getOrigin().getZ();
	m[15] = 1;
#else
#ifdef BTTRANSFORM_TRANSPOSE
	m[0] = t->getBasis().getRow(0).getX();
	m[3] = t->getBasis().getRow(0).getY();
	m[6] = t->getBasis().getRow(0).getZ();
	m[1] = t->getBasis().getRow(1).getX();
	m[4] = t->getBasis().getRow(1).getY();
	m[7] = t->getBasis().getRow(1).getZ();
	m[2] = t->getBasis().getRow(2).getX();
	m[5] = t->getBasis().getRow(2).getY();
	m[8] = t->getBasis().getRow(2).getZ();
#else
	m[0] = t->getBasis().getRow(0).getX();
	m[1] = t->getBasis().getRow(0).getY();
	m[2] = t->getBasis().getRow(0).getZ();
	m[3] = t->getBasis().getRow(1).getX();
	m[4] = t->getBasis().getRow(1).getY();
	m[5] = t->getBasis().getRow(1).getZ();
	m[6] = t->getBasis().getRow(2).getX();
	m[7] = t->getBasis().getRow(2).getY();
	m[8] = t->getBasis().getRow(2).getZ();
#endif
	m[9] = t->getOrigin().getX();
	m[10] = t->getOrigin().getY();
	m[11] = t->getOrigin().getZ();
#endif
}

inline void btTransformToMatrix(const btTransform& t, btScalar* m)
{
	btTransformToMatrix(&t, m);
}

inline void MatrixTobtTransform(const btScalar* m, btTransform* t)
{
#ifdef BTTRANSFORM_TO4X4
#ifdef BTTRANSFORM_TRANSPOSE
	t->getBasis().setValue(m[0],m[4],m[8],m[1],m[5],m[9],m[2],m[6],m[10]);
#else
	t->getBasis().setValue(m[0],m[1],m[2],m[4],m[5],m[6],m[8],m[9],m[10]);
#endif
	t->getOrigin().setX(m[12]);
	t->getOrigin().setY(m[13]);
	t->getOrigin().setZ(m[14]);
#else
#ifdef BTTRANSFORM_TRANSPOSE
	t->getBasis().setValue(m[0],m[3],m[6],m[1],m[4],m[7],m[2],m[5],m[8]);
#else
	t->getBasis().setValue(m[0],m[1],m[2],m[3],m[4],m[5],m[6],m[7],m[8]);
#endif
	t->getOrigin().setX(m[9]);
	t->getOrigin().setY(m[10]);
	t->getOrigin().setZ(m[11]);
#endif
}


inline void btMatrix3x3ToMatrix(const btMatrix3x3* t, btScalar* m)
{
#ifdef BTTRANSFORM_TO4X4
#ifdef BTTRANSFORM_TRANSPOSE
	m[0] = t->getRow(0).getX();
	m[4] = t->getRow(0).getY();
	m[8] = t->getRow(0).getZ();
	m[1] = t->getRow(1).getX();
	m[5] = t->getRow(1).getY();
	m[9] = t->getRow(1).getZ();
	m[2] = t->getRow(2).getX();
	m[6] = t->getRow(2).getY();
	m[10] = t->getRow(2).getZ();
#else
	m[0] = t->getRow(0).getX();
	m[1] = t->getRow(0).getY();
	m[2] = t->getRow(0).getZ();
	m[4] = t->getRow(1).getX();
	m[5] = t->getRow(1).getY();
	m[6] = t->getRow(1).getZ();
	m[8] = t->getRow(2).getX();
	m[9] = t->getRow(2).getY();
	m[10] = t->getRow(2).getZ();
#endif
	m[12] = 0;
	m[13] = 0;
	m[14] = 0;
	m[15] = 1;
#else
#ifdef BTTRANSFORM_TRANSPOSE
	m[0] = t->getRow(0).getX();
	m[3] = t->getRow(0).getY();
	m[6] = t->getRow(0).getZ();
	m[1] = t->getRow(1).getX();
	m[4] = t->getRow(1).getY();
	m[7] = t->getRow(1).getZ();
	m[2] = t->getRow(2).getX();
	m[5] = t->getRow(2).getY();
	m[8] = t->getRow(2).getZ();
#else
	m[0] = t->getRow(0).getX();
	m[1] = t->getRow(0).getY();
	m[2] = t->getRow(0).getZ();
	m[3] = t->getRow(1).getX();
	m[4] = t->getRow(1).getY();
	m[5] = t->getRow(1).getZ();
	m[6] = t->getRow(2).getX();
	m[7] = t->getRow(2).getY();
	m[8] = t->getRow(2).getZ();
#endif
	m[9] = 0;
	m[10] = 0;
	m[11] = 0;
#endif
}

inline void btMatrix3x3ToMatrix(const btMatrix3x3& t, btScalar* m)
{
	btMatrix3x3ToMatrix(&t, m);
}

inline void MatrixTobtMatrix3x3(const btScalar* m, btMatrix3x3* t)
{
#ifdef BTTRANSFORM_TO4X4
#ifdef BTTRANSFORM_TRANSPOSE
	t->setValue(m[0],m[4],m[8],m[1],m[5],m[9],m[2],m[6],m[10]);
#else
	t->setValue(m[0],m[1],m[2],m[4],m[5],m[6],m[8],m[9],m[10]);
#endif
#else
#ifdef BTTRANSFORM_TRANSPOSE
	t->.setValue(m[0],m[3],m[6],m[1],m[4],m[7],m[2],m[5],m[8]);
#else
	t->setValue(m[0],m[1],m[2],m[3],m[4],m[5],m[6],m[7],m[8]);
#endif
#endif
}


// SSE requires math structs to be aligned to 16-byte boundaries.
// Alignment cannot be guaranteed in .NET, so aligned temporary intermediate variables
// must be used to exchange vectors and transforms with Bullet (if SSE is enabled).
#define TEMP(var) var ## Temp
#if defined(BT_USE_SSE) //&& defined(BT_USE_SSE_IN_API) && defined(BT_USE_SIMD_VECTOR3)
#define VECTOR3_DEF(vec) ATTRIBUTE_ALIGNED16(btVector3) TEMP(vec)
#define VECTOR3_IN(from, to) Vector3TobtVector3(from, to)
#define VECTOR3_CONV(vec) VECTOR3_DEF(vec); VECTOR3_IN(vec, &TEMP(vec))
#define VECTOR3_USE(vec) TEMP(vec)
#define VECTOR3_OUT(from, to) btVector3ToVector3(from, to)
#define VECTOR3_OUT_VAL(from, to) btVector3ToVector3(from, to)
#define VECTOR3_DEF_OUT(vec) VECTOR3_OUT(&TEMP(vec), vec)
#define VECTOR4_DEF(vec) ATTRIBUTE_ALIGNED16(btVector4) TEMP(vec)
#define VECTOR4_IN(from, to) Vector4TobtVector4(from, to)
#define VECTOR4_CONV(vec) VECTOR4_DEF(vec); VECTOR4_IN(vec, &TEMP(vec))
#define VECTOR4_USE(vec) TEMP(vec)
#define VECTOR4_OUT(from, to) btVector4ToVector4(from, to)
#define VECTOR4_OUT_VAL(from, to) btVector4ToVector4(from, to)
#define VECTOR4_DEF_OUT(vec) VECTOR4_OUT(&TEMP(vec), vec)
#define TRANSFORM_DEF(tr) ATTRIBUTE_ALIGNED16(btTransform) TEMP(tr)
#define MATRIX3X3_DEF(tr) ATTRIBUTE_ALIGNED16(btMatrix3x3) TEMP(tr)
#define QUATERNION_DEF(quat) ATTRIBUTE_ALIGNED16(btQuaternion) TEMP(quat)
#define QUATERNION_IN(from, to) QuaternionTobtQuaternion(from, to)
#define QUATERNION_CONV(quat) QUATERNION_DEF(quat); QUATERNION_IN(quat, &TEMP(quat))
#define QUATERNION_USE(quat) TEMP(quat)
#define QUATERNION_OUT(from, to) btQuaternionToQuaternion(from, to)
#define QUATERNION_OUT_VAL(from, to) btQuaternionToQuaternion(from, to)
#else
// Cant use a pinned pointer to a Vector3 in case sizeof(Vector3) != sizeof(btVector3)
#if VECTOR3_16B
#define VECTOR3_DEF(vec)
#define VECTOR3_IN(from, to) *to = *(btVector3*)from
#define VECTOR3_CONV(vec)
#define VECTOR3_USE(vec) *(btVector3*)vec
#define VECTOR3_OUT(from, to) *(btVector3*)to = *from
#define VECTOR3_OUT_VAL(from, to) *(btVector3*)to = from
#define VECTOR3_DEF_OUT(vec)
#else
#define VECTOR3_DEF(vec) ATTRIBUTE_ALIGNED16(btVector3) TEMP(vec)
#define VECTOR3_IN(from, to) Vector3TobtVector3(from, to)
#define VECTOR3_CONV(vec) VECTOR3_DEF(vec); VECTOR3_IN(vec, &TEMP(vec))
#define VECTOR3_USE(vec) TEMP(vec)
#define VECTOR3_OUT(from, to) btVector3ToVector3(from, to)
#define VECTOR3_OUT_VAL(from, to) btVector3ToVector3(from, to)
#define VECTOR3_DEF_OUT(vec) VECTOR3_OUT(&TEMP(vec), vec)
#endif
#define VECTOR4_DEF(vec)
#define VECTOR4_IN(from, to) *to = *(btVector4*)from
#define VECTOR4_CONV(vec)
#define VECTOR4_USE(vec) *(btVector4*)vec
#define VECTOR4_OUT(from, to) *(btVector4*)to = *from
#define VECTOR4_OUT_VAL(from, to) *(btVector4*)to = from
#define VECTOR4_DEF_OUT(vec)
#define TRANSFORM_DEF(tr) btTransform TEMP(tr)
#define MATRIX3X3_DEF(tr) btMatrix3x3 TEMP(tr)
#define QUATERNION_DEF(quat)
#define QUATERNION_IN(from, to) *to = *(btQuaternion*)from
#define QUATERNION_CONV(quat)
#define QUATERNION_USE(quat) *(btQuaternion*)quat
#define QUATERNION_OUT(from, to) *(btQuaternion*)to = *from
#define QUATERNION_OUT_VAL(from, to) *(btQuaternion*)to = from
#endif
#define TRANSFORM_IN(from, to) MatrixTobtTransform(from, to)
#define TRANSFORM_CONV(tr) TRANSFORM_DEF(tr); TRANSFORM_IN(tr, &TEMP(tr))
#define TRANSFORM_USE(tr) TEMP(tr)
#define TRANSFORM_OUT(from, to) btTransformToMatrix(from, to)
#define TRANSFORM_OUT_VAL(from, to) btTransformToMatrix(from, to)
#define TRANSFORM_DEF_OUT(tr) TRANSFORM_OUT(&TEMP(tr), tr)
#define MATRIX3X3_IN(from, to) MatrixTobtMatrix3x3(from, to)
#define MATRIX3X3_CONV(tr) MATRIX3X3_DEF(tr); MATRIX3X3_IN(tr, &TEMP(tr))
#define MATRIX3X3_USE(tr) TEMP(tr)
#define MATRIX3X3_OUT(from, to) btMatrix3x3ToMatrix(from, to)
#define MATRIX3X3_DEF_OUT(tr) MATRIX3X3_OUT(&TEMP(tr), tr)
