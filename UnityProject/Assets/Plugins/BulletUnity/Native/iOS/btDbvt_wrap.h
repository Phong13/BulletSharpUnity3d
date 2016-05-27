#include "main.h"

extern "C"
{
	EXPORT btDbvtAabbMm* btDbvtAabbMm_new();
	EXPORT void btDbvtAabbMm_Center(btDbvtAabbMm* obj, btScalar* value);
	EXPORT int btDbvtAabbMm_Classify(btDbvtAabbMm* obj, const btScalar* n, btScalar o, int s);
	EXPORT bool btDbvtAabbMm_Contain(btDbvtAabbMm* obj, const btDbvtAabbMm* a);
	EXPORT void btDbvtAabbMm_Expand(btDbvtAabbMm* obj, const btScalar* e);
	EXPORT void btDbvtAabbMm_Extents(btDbvtAabbMm* obj, btScalar* value);
	EXPORT btDbvtAabbMm* btDbvtAabbMm_FromCE(const btScalar* c, const btScalar* e);
	EXPORT btDbvtAabbMm* btDbvtAabbMm_FromCR(const btScalar* c, btScalar r);
	EXPORT btDbvtAabbMm* btDbvtAabbMm_FromMM(const btScalar* mi, const btScalar* mx);
	EXPORT btDbvtAabbMm* btDbvtAabbMm_FromPoints(const btVector3** ppts, int n);
	EXPORT btDbvtAabbMm* btDbvtAabbMm_FromPoints2(const btVector3* pts, int n);
	EXPORT void btDbvtAabbMm_Lengths(btDbvtAabbMm* obj, btScalar* value);
	EXPORT void btDbvtAabbMm_Maxs(btDbvtAabbMm* obj, btScalar* value);
	EXPORT void btDbvtAabbMm_Mins(btDbvtAabbMm* obj, btScalar* value);
	EXPORT btScalar btDbvtAabbMm_ProjectMinimum(btDbvtAabbMm* obj, const btScalar* v, unsigned int signs);
	EXPORT void btDbvtAabbMm_SignedExpand(btDbvtAabbMm* obj, const btScalar* e);
	EXPORT void btDbvtAabbMm_tMaxs(btDbvtAabbMm* obj, btScalar* value);
	EXPORT void btDbvtAabbMm_tMins(btDbvtAabbMm* obj, btScalar* value);
	EXPORT void btDbvtAabbMm_delete(btDbvtAabbMm* obj);

	EXPORT btDbvtNode* btDbvtNode_new();
	EXPORT btDbvtNode** btDbvtNode_getChilds(btDbvtNode* obj);
	EXPORT void* btDbvtNode_getData(btDbvtNode* obj);
	EXPORT int btDbvtNode_getDataAsInt(btDbvtNode* obj);
	EXPORT btDbvtNode* btDbvtNode_getParent(btDbvtNode* obj);
	EXPORT btDbvtVolume* btDbvtNode_getVolume(btDbvtNode* obj);
	EXPORT bool btDbvtNode_isinternal(btDbvtNode* obj);
	EXPORT bool btDbvtNode_isleaf(btDbvtNode* obj);
	EXPORT void btDbvtNode_setData(btDbvtNode* obj, void* value);
	EXPORT void btDbvtNode_setDataAsInt(btDbvtNode* obj, int value);
	EXPORT void btDbvtNode_setParent(btDbvtNode* obj, btDbvtNode* value);
	EXPORT void btDbvtNode_delete(btDbvtNode* obj);

	EXPORT btDbvt_IClone* btDbvt_IClone_new();
	EXPORT void btDbvt_IClone_CloneLeaf(btDbvt_IClone* obj, btDbvtNode* __unnamed0);
	EXPORT void btDbvt_IClone_delete(btDbvt_IClone* obj);

	EXPORT btDbvt_ICollide* btDbvt_ICollide_new();
	EXPORT bool btDbvt_ICollide_AllLeaves(btDbvt_ICollide* obj, const btDbvtNode* __unnamed0);
	EXPORT bool btDbvt_ICollide_Descent(btDbvt_ICollide* obj, const btDbvtNode* __unnamed0);
	EXPORT void btDbvt_ICollide_Process(btDbvt_ICollide* obj, const btDbvtNode* __unnamed0, const btDbvtNode* __unnamed1);
	EXPORT void btDbvt_ICollide_Process2(btDbvt_ICollide* obj, const btDbvtNode* __unnamed0);
	EXPORT void btDbvt_ICollide_Process3(btDbvt_ICollide* obj, const btDbvtNode* n, btScalar __unnamed1);
	EXPORT void btDbvt_ICollide_delete(btDbvt_ICollide* obj);

	EXPORT void btDbvt_IWriter_Prepare(btDbvt_IWriter* obj, const btDbvtNode* root, int numnodes);
	EXPORT void btDbvt_IWriter_WriteLeaf(btDbvt_IWriter* obj, const btDbvtNode* __unnamed0, int index, int parent);
	EXPORT void btDbvt_IWriter_WriteNode(btDbvt_IWriter* obj, const btDbvtNode* __unnamed0, int index, int parent, int child0, int child1);
	EXPORT void btDbvt_IWriter_delete(btDbvt_IWriter* obj);

	EXPORT btDbvt_sStkCLN* btDbvt_sStkCLN_new(const btDbvtNode* n, btDbvtNode* p);
	EXPORT const btDbvtNode* btDbvt_sStkCLN_getNode(btDbvt_sStkCLN* obj);
	EXPORT btDbvtNode* btDbvt_sStkCLN_getParent(btDbvt_sStkCLN* obj);
	EXPORT void btDbvt_sStkCLN_setNode(btDbvt_sStkCLN* obj, const btDbvtNode* value);
	EXPORT void btDbvt_sStkCLN_setParent(btDbvt_sStkCLN* obj, btDbvtNode* value);
	EXPORT void btDbvt_sStkCLN_delete(btDbvt_sStkCLN* obj);

	EXPORT btDbvt_sStkNN* btDbvt_sStkNN_new();
	EXPORT btDbvt_sStkNN* btDbvt_sStkNN_new2(const btDbvtNode* na, const btDbvtNode* nb);
	EXPORT const btDbvtNode* btDbvt_sStkNN_getA(btDbvt_sStkNN* obj);
	EXPORT const btDbvtNode* btDbvt_sStkNN_getB(btDbvt_sStkNN* obj);
	EXPORT void btDbvt_sStkNN_setA(btDbvt_sStkNN* obj, const btDbvtNode* value);
	EXPORT void btDbvt_sStkNN_setB(btDbvt_sStkNN* obj, const btDbvtNode* value);
	EXPORT void btDbvt_sStkNN_delete(btDbvt_sStkNN* obj);

	EXPORT btDbvt_sStkNP* btDbvt_sStkNP_new(const btDbvtNode* n, unsigned int m);
	EXPORT int btDbvt_sStkNP_getMask(btDbvt_sStkNP* obj);
	EXPORT const btDbvtNode* btDbvt_sStkNP_getNode(btDbvt_sStkNP* obj);
	EXPORT void btDbvt_sStkNP_setMask(btDbvt_sStkNP* obj, int value);
	EXPORT void btDbvt_sStkNP_setNode(btDbvt_sStkNP* obj, const btDbvtNode* value);
	EXPORT void btDbvt_sStkNP_delete(btDbvt_sStkNP* obj);

	EXPORT btDbvt_sStkNPS* btDbvt_sStkNPS_new();
	EXPORT btDbvt_sStkNPS* btDbvt_sStkNPS_new2(const btDbvtNode* n, unsigned int m, btScalar v);
	EXPORT int btDbvt_sStkNPS_getMask(btDbvt_sStkNPS* obj);
	EXPORT const btDbvtNode* btDbvt_sStkNPS_getNode(btDbvt_sStkNPS* obj);
	EXPORT btScalar btDbvt_sStkNPS_getValue(btDbvt_sStkNPS* obj);
	EXPORT void btDbvt_sStkNPS_setMask(btDbvt_sStkNPS* obj, int value);
	EXPORT void btDbvt_sStkNPS_setNode(btDbvt_sStkNPS* obj, const btDbvtNode* value);
	EXPORT void btDbvt_sStkNPS_setValue(btDbvt_sStkNPS* obj, btScalar value);
	EXPORT void btDbvt_sStkNPS_delete(btDbvt_sStkNPS* obj);

	EXPORT btDbvt* btDbvt_new();
	EXPORT int btDbvt_allocate(btAlignedIntArray* ifree, btAlignedStkNpsArray* stock, const btDbvt_sStkNPS* value);
	EXPORT void btDbvt_benchmark();
	EXPORT void btDbvt_clear(btDbvt* obj);
	EXPORT void btDbvt_clone(btDbvt* obj, btDbvt* dest);
	EXPORT void btDbvt_clone2(btDbvt* obj, btDbvt* dest, btDbvt_IClone* iclone);
	//EXPORT void btDbvt_collideKDOP(const btDbvtNode* root, const btVector3* normals, const btScalar* offsets, int count, const btDbvt_ICollide* policy);
	//EXPORT void btDbvt_collideOCL(const btDbvtNode* root, const btVector3* normals, const btScalar* offsets, const btScalar* sortaxis, int count, const btDbvt_ICollide* policy);
	//EXPORT void btDbvt_collideOCL2(const btDbvtNode* root, const btVector3* normals, const btScalar* offsets, const btScalar* sortaxis, int count, const btDbvt_ICollide* policy, bool fullsort);
	//EXPORT void btDbvt_collideTT(btDbvt* obj, const btDbvtNode* root0, const btDbvtNode* root1, const btDbvt_ICollide* policy);
	//EXPORT void btDbvt_collideTTpersistentStack(btDbvt* obj, const btDbvtNode* root0, const btDbvtNode* root1, const btDbvt_ICollide* policy);
	//EXPORT void btDbvt_collideTU(const btDbvtNode* root, const btDbvt_ICollide* policy);
	//EXPORT void btDbvt_collideTV(btDbvt* obj, const btDbvtNode* root, const btDbvtVolume* volume, const btDbvt_ICollide* policy);
	EXPORT int btDbvt_countLeaves(const btDbvtNode* node);
	EXPORT bool btDbvt_empty(btDbvt* obj);
	//EXPORT void btDbvt_enumLeaves(const btDbvtNode* root, const btDbvt_ICollide* policy);
	//EXPORT void btDbvt_enumNodes(const btDbvtNode* root, const btDbvt_ICollide* policy);
	EXPORT void btDbvt_extractLeaves(const btDbvtNode* node, btAlignedDbvtNodeArray* leaves);
	EXPORT btDbvtNode* btDbvt_getFree(btDbvt* obj);
	EXPORT int btDbvt_getLeaves(btDbvt* obj);
	EXPORT int btDbvt_getLkhd(btDbvt* obj);
	EXPORT unsigned int btDbvt_getOpath(btDbvt* obj);
	EXPORT btAlignedDbvtNodeArray* btDbvt_getRayTestStack(btDbvt* obj);
	EXPORT btDbvtNode* btDbvt_getRoot(btDbvt* obj);
	EXPORT btAlignedStkNNArray* btDbvt_getStkStack(btDbvt* obj);
	EXPORT btDbvtNode* btDbvt_insert(btDbvt* obj, const btDbvtVolume* box, void* data);
	EXPORT int btDbvt_maxdepth(const btDbvtNode* node);
	EXPORT int btDbvt_nearest(const int* i, const btDbvt_sStkNPS* a, btScalar v, int l, int h);
	EXPORT void btDbvt_optimizeBottomUp(btDbvt* obj);
	EXPORT void btDbvt_optimizeIncremental(btDbvt* obj, int passes);
	EXPORT void btDbvt_optimizeTopDown(btDbvt* obj);
	EXPORT void btDbvt_optimizeTopDown2(btDbvt* obj, int bu_treshold);
	EXPORT void btDbvt_rayTest(const btDbvtNode* root, const btScalar* rayFrom, const btScalar* rayTo, const btDbvt_ICollide* policy);
	EXPORT void btDbvt_rayTestInternal(btDbvt* obj, const btDbvtNode* root, const btScalar* rayFrom, const btScalar* rayTo, const btScalar* rayDirectionInverse, unsigned int* signs, btScalar lambda_max, const btScalar* aabbMin, const btScalar* aabbMax, const btDbvt_ICollide* policy);
	EXPORT void btDbvt_remove(btDbvt* obj, btDbvtNode* leaf);
	EXPORT void btDbvt_setFree(btDbvt* obj, btDbvtNode* value);
	EXPORT void btDbvt_setLeaves(btDbvt* obj, int value);
	EXPORT void btDbvt_setLkhd(btDbvt* obj, int value);
	EXPORT void btDbvt_setOpath(btDbvt* obj, unsigned int value);
	EXPORT void btDbvt_setRoot(btDbvt* obj, btDbvtNode* value);
	EXPORT void btDbvt_update(btDbvt* obj, btDbvtNode* leaf, btDbvtVolume* volume);
	EXPORT void btDbvt_update2(btDbvt* obj, btDbvtNode* leaf);
	EXPORT void btDbvt_update3(btDbvt* obj, btDbvtNode* leaf, int lookahead);
	EXPORT bool btDbvt_update4(btDbvt* obj, btDbvtNode* leaf, btDbvtVolume* volume, btScalar margin);
	EXPORT bool btDbvt_update5(btDbvt* obj, btDbvtNode* leaf, btDbvtVolume* volume, const btScalar* velocity);
	EXPORT bool btDbvt_update6(btDbvt* obj, btDbvtNode* leaf, btDbvtVolume* volume, const btScalar* velocity, btScalar margin);
	EXPORT void btDbvt_write(btDbvt* obj, btDbvt_IWriter* iwriter);
	EXPORT void btDbvt_delete(btDbvt* obj);
}
