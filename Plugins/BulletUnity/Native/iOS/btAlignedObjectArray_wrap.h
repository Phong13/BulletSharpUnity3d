#include "main.h"

extern "C"
{
	EXPORT btBroadphasePair* btAlignedBroadphasePairArray_at(btAlignedBroadphasePairArray* obj, int n);
	EXPORT void btAlignedBroadphasePairArray_push_back(btAlignedBroadphasePairArray* obj, btBroadphasePair* val);
	EXPORT void btAlignedBroadphasePairArray_resizeNoInitialize(btAlignedBroadphasePairArray* obj, int newSize);
	EXPORT int btAlignedBroadphasePairArray_size(btAlignedBroadphasePairArray* obj);

	EXPORT btCollisionObject* btAlignedCollisionObjectArray_at(btAlignedCollisionObjectArray* obj, int n);
	EXPORT void btAlignedCollisionObjectArray_push_back(btAlignedCollisionObjectArray* obj, btCollisionObject* val);
	EXPORT void btAlignedCollisionObjectArray_resizeNoInitialize(btAlignedCollisionObjectArray* obj, int newSize);
	EXPORT int btAlignedCollisionObjectArray_size(btAlignedCollisionObjectArray* obj);

	EXPORT btIndexedMesh* btAlignedIndexedMeshArray_at(btAlignedIndexedMeshArray* obj, int n);
	EXPORT void btAlignedIndexedMeshArray_push_back(btAlignedIndexedMeshArray* obj, btIndexedMesh* val);
	EXPORT void btAlignedIndexedMeshArray_resizeNoInitialize(btAlignedIndexedMeshArray* obj, int newSize);
	EXPORT int btAlignedIndexedMeshArray_size(btAlignedIndexedMeshArray* obj);

	EXPORT btAlignedManifoldArray* btAlignedManifoldArray_new();
	EXPORT btPersistentManifold* btAlignedManifoldArray_at(btAlignedManifoldArray* obj, int n);
	EXPORT void btAlignedManifoldArray_push_back(btAlignedManifoldArray* obj, btPersistentManifold* val);
	EXPORT void btAlignedManifoldArray_resizeNoInitialize(btAlignedManifoldArray* obj, int newSize);
	EXPORT int btAlignedManifoldArray_size(btAlignedManifoldArray* obj);
	EXPORT void btAlignedManifoldArray_delete(btAlignedManifoldArray* obj);

	EXPORT btSoftBody* btAlignedSoftBodyArray_at(btAlignedSoftBodyArray* obj, int n);
	EXPORT void btAlignedSoftBodyArray_push_back(btAlignedSoftBodyArray* obj, btSoftBody* val);
	EXPORT void btAlignedSoftBodyArray_resizeNoInitialize(btAlignedSoftBodyArray* obj, int newSize);
	EXPORT int btAlignedSoftBodyArray_size(btAlignedSoftBodyArray* obj);

	EXPORT btSoftBody_Anchor* btAlignedSoftBodyAnchorArray_at(btAlignedSoftBodyAnchorArray* obj, int n);
	EXPORT void btAlignedSoftBodyAnchorArray_push_back(btAlignedSoftBodyAnchorArray* obj, btSoftBody_Anchor* val);
	EXPORT void btAlignedSoftBodyAnchorArray_resizeNoInitialize(btAlignedSoftBodyAnchorArray* obj, int newSize);
	EXPORT int btAlignedSoftBodyAnchorArray_size(btAlignedSoftBodyAnchorArray* obj);

	EXPORT btSoftBody_Cluster* btAlignedSoftBodyClusterArray_at(btAlignedSoftBodyClusterArray* obj, int n);
	EXPORT void btAlignedSoftBodyClusterArray_push_back(btAlignedSoftBodyClusterArray* obj, btSoftBody_Cluster* val);
	EXPORT void btAlignedSoftBodyClusterArray_resizeNoInitialize(btAlignedSoftBodyClusterArray* obj, int newSize);
	EXPORT int btAlignedSoftBodyClusterArray_size(btAlignedSoftBodyClusterArray* obj);

	EXPORT btSoftBody_Face* btAlignedSoftBodyFaceArray_at(btAlignedSoftBodyFaceArray* obj, int n);
	EXPORT void btAlignedSoftBodyFaceArray_push_back(btAlignedSoftBodyFaceArray* obj, btSoftBody_Face* val);
	EXPORT void btAlignedSoftBodyFaceArray_resizeNoInitialize(btAlignedSoftBodyFaceArray* obj, int newSize);
	EXPORT int btAlignedSoftBodyFaceArray_size(btAlignedSoftBodyFaceArray* obj);

	EXPORT btSoftBody_Joint* btAlignedSoftBodyJointArray_at(btAlignedSoftBodyJointArray* obj, int n);
	EXPORT void btAlignedSoftBodyJointArray_push_back(btAlignedSoftBodyJointArray* obj, btSoftBody_Joint* val);
	EXPORT void btAlignedSoftBodyJointArray_resizeNoInitialize(btAlignedSoftBodyJointArray* obj, int newSize);
	EXPORT int btAlignedSoftBodyJointArray_size(btAlignedSoftBodyJointArray* obj);

	EXPORT btSoftBody_Link* btAlignedSoftBodyLinkArray_at(btAlignedSoftBodyLinkArray* obj, int n);
	EXPORT void btAlignedSoftBodyLinkArray_push_back(btAlignedSoftBodyLinkArray* obj, btSoftBody_Link* val);
	EXPORT void btAlignedSoftBodyLinkArray_resizeNoInitialize(btAlignedSoftBodyLinkArray* obj, int newSize);
	EXPORT void btAlignedSoftBodyLinkArray_set(btAlignedSoftBodyLinkArray* obj, btSoftBody_Link* val, int index);
	EXPORT int btAlignedSoftBodyLinkArray_size(btAlignedSoftBodyLinkArray* obj);

	EXPORT btSoftBody_Material* btAlignedSoftBodyMaterialArray_at(btAlignedSoftBodyMaterialArray* obj, int n);
	EXPORT void btAlignedSoftBodyMaterialArray_push_back(btAlignedSoftBodyMaterialArray* obj, btSoftBody_Material* val);
	EXPORT void btAlignedSoftBodyMaterialArray_resizeNoInitialize(btAlignedSoftBodyMaterialArray* obj, int newSize);
	EXPORT int btAlignedSoftBodyMaterialArray_size(btAlignedSoftBodyMaterialArray* obj);

	EXPORT btSoftBody_Node* btAlignedSoftBodyNodeArray_at(btAlignedSoftBodyNodeArray* obj, int n);
	EXPORT int btAlignedSoftBodyNodeArray_index_of(btAlignedSoftBodyNodeArray* obj, btSoftBody_Node* val);
	EXPORT void btAlignedSoftBodyNodeArray_push_back(btAlignedSoftBodyNodeArray* obj, btSoftBody_Node* val);
	EXPORT void btAlignedSoftBodyNodeArray_resizeNoInitialize(btAlignedSoftBodyNodeArray* obj, int newSize);
	EXPORT int btAlignedSoftBodyNodeArray_size(btAlignedSoftBodyNodeArray* obj);

	EXPORT btSoftBody_Tetra* btAlignedSoftBodyTetraArray_at(btAlignedSoftBodyTetraArray* obj, int n);
	EXPORT void btAlignedSoftBodyTetraArray_push_back(btAlignedSoftBodyTetraArray* obj, btSoftBody_Tetra* val);
	EXPORT void btAlignedSoftBodyTetraArray_resizeNoInitialize(btAlignedSoftBodyTetraArray* obj, int newSize);
	EXPORT int btAlignedSoftBodyTetraArray_size(btAlignedSoftBodyTetraArray* obj);
}
