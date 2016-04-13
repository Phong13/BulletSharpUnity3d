#include "main.h"

extern "C"
{
	EXPORT HACD_HACD* HACD_HACD_new();
	EXPORT bool HACD_HACD_Compute(HACD_HACD* obj);
	EXPORT bool HACD_HACD_Compute2(HACD_HACD* obj, bool fullCH);
	EXPORT bool HACD_HACD_Compute3(HACD_HACD* obj, bool fullCH, bool exportDistPoints);
	EXPORT void HACD_HACD_DenormalizeData(HACD_HACD* obj);
	EXPORT bool HACD_HACD_GetAddExtraDistPoints(HACD_HACD* obj);
	EXPORT bool HACD_HACD_GetAddFacesPoints(HACD_HACD* obj);
	EXPORT bool HACD_HACD_GetAddNeighboursDistPoints(HACD_HACD* obj);
	EXPORT const HACD_CallBackFunction HACD_HACD_GetCallBack(HACD_HACD* obj);
	EXPORT bool HACD_HACD_GetCH(HACD_HACD* obj, int numCH, HACD_Vec3_Real* points, HACD_Vec3_long* triangles);
	EXPORT double HACD_HACD_GetCompacityWeight(HACD_HACD* obj);
	EXPORT double HACD_HACD_GetConcavity(HACD_HACD* obj);
	EXPORT double HACD_HACD_GetConnectDist(HACD_HACD* obj);
	EXPORT size_t HACD_HACD_GetNClusters(HACD_HACD* obj);
	EXPORT size_t HACD_HACD_GetNPoints(HACD_HACD* obj);
	EXPORT size_t HACD_HACD_GetNPointsCH(HACD_HACD* obj, int numCH);
	EXPORT size_t HACD_HACD_GetNTriangles(HACD_HACD* obj);
	EXPORT size_t HACD_HACD_GetNTrianglesCH(HACD_HACD* obj, int numCH);
	EXPORT size_t HACD_HACD_GetNVerticesPerCH(HACD_HACD* obj);
	EXPORT const long* HACD_HACD_GetPartition(HACD_HACD* obj);
	EXPORT const HACD_Vec3_Real* HACD_HACD_GetPoints(HACD_HACD* obj);
	EXPORT double HACD_HACD_GetScaleFactor(HACD_HACD* obj);
	EXPORT const HACD_Vec3_long* HACD_HACD_GetTriangles(HACD_HACD* obj);
	EXPORT double HACD_HACD_GetVolumeWeight(HACD_HACD* obj);
	EXPORT void HACD_HACD_NormalizeData(HACD_HACD* obj);
	EXPORT bool HACD_HACD_Save(HACD_HACD* obj, const char* fileName, bool uniColor);
	EXPORT bool HACD_HACD_Save2(HACD_HACD* obj, const char* fileName, bool uniColor, long numCluster);
	EXPORT void HACD_HACD_SetAddExtraDistPoints(HACD_HACD* obj, bool addExtraDistPoints);
	EXPORT void HACD_HACD_SetAddFacesPoints(HACD_HACD* obj, bool addFacesPoints);
	EXPORT void HACD_HACD_SetAddNeighboursDistPoints(HACD_HACD* obj, bool addNeighboursDistPoints);
	EXPORT void HACD_HACD_SetCallBack(HACD_HACD* obj, HACD_CallBackFunction callBack);
	EXPORT void HACD_HACD_SetCompacityWeight(HACD_HACD* obj, double alpha);
	EXPORT void HACD_HACD_SetConcavity(HACD_HACD* obj, double concavity);
	EXPORT void HACD_HACD_SetConnectDist(HACD_HACD* obj, double ccConnectDist);
	EXPORT void HACD_HACD_SetNClusters(HACD_HACD* obj, int nClusters);
	EXPORT void HACD_HACD_SetNPoints(HACD_HACD* obj, int nPoints);
	EXPORT void HACD_HACD_SetNTriangles(HACD_HACD* obj, int nTriangles);
	EXPORT void HACD_HACD_SetNVerticesPerCH(HACD_HACD* obj, int nVerticesPerCH);
	EXPORT void HACD_HACD_SetPoints(HACD_HACD* obj, HACD_Vec3_Real* points);
	EXPORT void HACD_HACD_SetScaleFactor(HACD_HACD* obj, double scale);
	EXPORT void HACD_HACD_SetTriangles(HACD_HACD* obj, HACD_Vec3_long* triangles);
	EXPORT void HACD_HACD_SetVolumeWeight(HACD_HACD* obj, double beta);
	EXPORT void HACD_HACD_delete(HACD_HACD* obj);
}
