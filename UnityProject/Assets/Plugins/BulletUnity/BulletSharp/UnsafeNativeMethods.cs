using BulletSharp.Math;
using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	[SuppressUnmanagedCodeSecurity]
	internal static class UnsafeNativeMethods
	{
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr BT_BOX_BOX_TRANSFORM_CACHE_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void BT_BOX_BOX_TRANSFORM_CACHE_calc_absolute_matrix(IntPtr native);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void BT_BOX_BOX_TRANSFORM_CACHE_calc_from_full_invert(IntPtr native, [In] ref Matrix transform0, [In] ref Matrix transform1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void BT_BOX_BOX_TRANSFORM_CACHE_calc_from_homogenic(IntPtr native, [In] ref Matrix transform0, [In] ref Matrix transform1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void BT_BOX_BOX_TRANSFORM_CACHE_getAR(IntPtr native, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void BT_BOX_BOX_TRANSFORM_CACHE_getR1to0(IntPtr native, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void BT_BOX_BOX_TRANSFORM_CACHE_getT1to0(IntPtr native, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void BT_BOX_BOX_TRANSFORM_CACHE_setAR(IntPtr native, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void BT_BOX_BOX_TRANSFORM_CACHE_setR1to0(IntPtr native, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void BT_BOX_BOX_TRANSFORM_CACHE_setT1to0(IntPtr native, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void BT_BOX_BOX_TRANSFORM_CACHE_transform(IntPtr obj, [In] ref Vector3 point, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void BT_BOX_BOX_TRANSFORM_CACHE_delete(IntPtr native);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr BT_QUANTIZED_BVH_NODE_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int BT_QUANTIZED_BVH_NODE_getDataIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int BT_QUANTIZED_BVH_NODE_getEscapeIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int BT_QUANTIZED_BVH_NODE_getEscapeIndexOrDataIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr BT_QUANTIZED_BVH_NODE_getQuantizedAabbMax(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr BT_QUANTIZED_BVH_NODE_getQuantizedAabbMin(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool BT_QUANTIZED_BVH_NODE_isLeafNode(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void BT_QUANTIZED_BVH_NODE_setDataIndex(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void BT_QUANTIZED_BVH_NODE_setEscapeIndex(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void BT_QUANTIZED_BVH_NODE_setEscapeIndexOrDataIndex(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool BT_QUANTIZED_BVH_NODE_testQuantizedBoxOverlapp(IntPtr obj, ushort[] quantizedMin, ushort[] quantizedMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void BT_QUANTIZED_BVH_NODE_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr bt32BitAxisSweep3_new([In] ref Vector3 worldAabbMin, [In] ref Vector3 worldAabbMax, uint maxHandles, IntPtr pairCache, bool disableRaycastAccelerator);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern uint bt32BitAxisSweep3_addHandle(IntPtr obj, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax, IntPtr pOwner, int collisionFilterGroup, int collisionFilterMask, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr bt32BitAxisSweep3_getHandle(IntPtr obj, uint index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern uint bt32BitAxisSweep3_getNumHandles(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void bt32BitAxisSweep3_quantize(IntPtr obj, uint o, [In] ref Vector3 point, int isMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void bt32BitAxisSweep3_removeHandle(IntPtr obj, uint handle, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void bt32BitAxisSweep3_setOverlappingPairUserCallback(IntPtr obj, IntPtr pairCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool bt32BitAxisSweep3_testAabbOverlap(IntPtr obj, IntPtr proxy0, IntPtr proxy1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void bt32BitAxisSweep3_unQuantize(IntPtr obj, IntPtr proxy, [Out] out Vector3 aabbMin, [Out] out Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void bt32BitAxisSweep3_updateHandle(IntPtr obj, uint handle, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax, IntPtr dispatcher);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btAABB_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btAABB_new2([In] ref Vector3 V1, [In] ref Vector3 V2, [In] ref Vector3 V3);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btAABB_new3([In] ref Vector3 V1, [In] ref Vector3 V2, [In] ref Vector3 V3, float margin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btAABB_new4(IntPtr other);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btAABB_new5(IntPtr other, float margin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAABB_appy_transform(IntPtr obj, [In] ref Matrix trans);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAABB_appy_transform_trans_cache(IntPtr obj, IntPtr trans);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btAABB_collide_plane(IntPtr obj, [In] ref Vector4 plane);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btAABB_collide_ray(IntPtr obj, [In] ref Vector3 vorigin, [In] ref Vector3 vdir);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btAABB_collide_triangle_exact(IntPtr obj, [In] ref Vector3 p1, [In] ref Vector3 p2, [In] ref Vector3 p3, [In] ref Vector4 triangle_plane);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAABB_copy_with_margin(IntPtr obj, IntPtr other, float margin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAABB_find_intersection(IntPtr obj, IntPtr other, IntPtr intersection);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAABB_get_center_extend(IntPtr obj, out Vector3 center, out Vector3 extend);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAABB_getMax(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAABB_getMin(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btAABB_has_collision(IntPtr obj, IntPtr other);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAABB_increment_margin(IntPtr obj, float margin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAABB_invalidate(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAABB_merge(IntPtr obj, IntPtr box);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btAABB_overlapping_trans_cache(IntPtr obj, IntPtr box, IntPtr transcache, bool fulltest);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btAABB_overlapping_trans_conservative(IntPtr obj, IntPtr box, [In] ref Matrix trans1_to_0);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btAABB_overlapping_trans_conservative2(IntPtr obj, IntPtr box, IntPtr trans1_to_0);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern PlaneIntersectionType btAABB_plane_classify(IntPtr obj, [In] ref Vector4 plane);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAABB_projection_interval(IntPtr obj, [In] ref Vector3 direction, out float vmin, out float vmax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAABB_setMax(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAABB_setMin(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAABB_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btActionInterfaceWrapper_new(IntPtr debugDrawCallback, IntPtr updateActionCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btActionInterface_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btAlignedObjectArray_btBroadphasePair_at(IntPtr obj, int n);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAlignedObjectArray_btBroadphasePair_push_back(IntPtr obj, IntPtr val);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAlignedObjectArray_btBroadphasePair_resizeNoInitialize(IntPtr obj, int newSize);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btAlignedObjectArray_btBroadphasePair_size(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btAlignedObjectArray_btCollisionObjectPtr_at(IntPtr obj, int n);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btAlignedObjectArray_btCollisionObjectPtr_findLinearSearch2(IntPtr obj, IntPtr key);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAlignedObjectArray_btCollisionObjectPtr_push_back(IntPtr obj, IntPtr val);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAlignedObjectArray_btCollisionObjectPtr_resizeNoInitialize(IntPtr obj, int newSize);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btAlignedObjectArray_btCollisionObjectPtr_size(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btIDebugDrawWrapper_new(IntPtr debugDrawGCHandle, IntPtr drawAabbCallback,
			IntPtr drawArcCallback, IntPtr drawBoxCallback, IntPtr drawCapsuleCallback, IntPtr drawConeCallback, IntPtr drawContactPointCallback,
			IntPtr drawCylinderCallback, IntPtr drawLineCallback, IntPtr drawPlaneCallback, IntPtr drawSphereCallback, IntPtr drawSpherePatchCallback, IntPtr drawTransformCallback, IntPtr drawTriangleCallback, IntPtr getDebugModeCallback, IntPtr cb);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btIDebugDrawWrapper_getGCHandle(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btAlignedObjectArray_btIndexedMesh_at(IntPtr obj, int n);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAlignedObjectArray_btIndexedMesh_push_back(IntPtr obj, IntPtr val);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAlignedObjectArray_btIndexedMesh_resizeNoInitialize(IntPtr obj, int newSize);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btAlignedObjectArray_btIndexedMesh_size(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btAlignedObjectArray_btPersistentManifoldPtr_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btAlignedObjectArray_btPersistentManifoldPtr_at(IntPtr obj, int n);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAlignedObjectArray_btPersistentManifoldPtr_push_back(IntPtr obj, IntPtr val);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAlignedObjectArray_btPersistentManifoldPtr_resizeNoInitialize(IntPtr obj, int newSize);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btAlignedObjectArray_btPersistentManifoldPtr_size(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAlignedObjectArray_btPersistentManifoldPtr_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btAlignedObjectArray_btSoftBody_Anchor_at(IntPtr obj, int n);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAlignedObjectArray_btSoftBody_Anchor_push_back(IntPtr obj, IntPtr val);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAlignedObjectArray_btSoftBody_Anchor_resizeNoInitialize(IntPtr obj, int newSize);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btAlignedObjectArray_btSoftBody_Anchor_size(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btAlignedObjectArray_btSoftBody_ClusterPtr_at(IntPtr obj, int n);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAlignedObjectArray_btSoftBody_ClusterPtr_push_back(IntPtr obj, IntPtr val);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAlignedObjectArray_btSoftBody_ClusterPtr_resizeNoInitialize(IntPtr obj, int newSize);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btAlignedObjectArray_btSoftBody_ClusterPtr_size(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btAlignedObjectArray_btSoftBody_Face_at(IntPtr obj, int n);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAlignedObjectArray_btSoftBody_Face_push_back(IntPtr obj, IntPtr val);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAlignedObjectArray_btSoftBody_Face_resizeNoInitialize(IntPtr obj, int newSize);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btAlignedObjectArray_btSoftBody_Face_size(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btAlignedObjectArray_btSoftBody_JointPtr_at(IntPtr obj, int n);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAlignedObjectArray_btSoftBody_JointPtr_push_back(IntPtr obj, IntPtr val);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAlignedObjectArray_btSoftBody_JointPtr_resizeNoInitialize(IntPtr obj, int newSize);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btAlignedObjectArray_btSoftBody_JointPtr_size(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btAlignedObjectArray_btSoftBody_Link_at(IntPtr obj, int n);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAlignedObjectArray_btSoftBody_Link_push_back(IntPtr obj, IntPtr val);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAlignedObjectArray_btSoftBody_Link_resizeNoInitialize(IntPtr obj, int newSize);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAlignedObjectArray_btSoftBody_Link_set(IntPtr obj, IntPtr val, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btAlignedObjectArray_btSoftBody_Link_size(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btAlignedObjectArray_btSoftBody_MaterialPtr_at(IntPtr obj, int n);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAlignedObjectArray_btSoftBody_MaterialPtr_push_back(IntPtr obj, IntPtr val);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAlignedObjectArray_btSoftBody_MaterialPtr_resizeNoInitialize(IntPtr obj, int newSize);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btAlignedObjectArray_btSoftBody_MaterialPtr_size(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btAlignedObjectArray_btSoftBody_Node_at(IntPtr obj, int n);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btAlignedObjectArray_btSoftBody_Node_index_of(IntPtr obj, IntPtr val);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAlignedObjectArray_btSoftBody_Node_push_back(IntPtr obj, IntPtr val);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAlignedObjectArray_btSoftBody_Node_resizeNoInitialize(IntPtr obj, int newSize);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btAlignedObjectArray_btSoftBody_Node_size(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btAlignedObjectArray_btSoftBody_Note_at(IntPtr obj, int n);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btAlignedObjectArray_btSoftBody_Note_index_of(IntPtr obj, IntPtr val);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAlignedObjectArray_btSoftBody_Note_push_back(IntPtr obj, IntPtr val);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAlignedObjectArray_btSoftBody_Note_resizeNoInitialize(IntPtr obj, int newSize);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btAlignedObjectArray_btSoftBody_Note_size(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btAlignedObjectArray_btSoftBodyPtr_at(IntPtr obj, int n);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAlignedObjectArray_btSoftBodyPtr_push_back(IntPtr obj, IntPtr val);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAlignedObjectArray_btSoftBodyPtr_resizeNoInitialize(IntPtr obj, int newSize);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btAlignedObjectArray_btSoftBodyPtr_size(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btAlignedObjectArray_btSoftBody_Tetra_at(IntPtr obj, int n);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAlignedObjectArray_btSoftBody_Tetra_push_back(IntPtr obj, IntPtr val);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAlignedObjectArray_btSoftBody_Tetra_resizeNoInitialize(IntPtr obj, int newSize);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btAlignedObjectArray_btSoftBody_Tetra_size(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBodyNodePtrArray_at(IntPtr obj, int n);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBodyNodePtrArray_set(IntPtr obj, IntPtr value, int index);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btAlignedObjectArray_btVector3_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAlignedObjectArray_btVector3_at(IntPtr obj, int n, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAlignedObjectArray_btVector3_push_back(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAlignedObjectArray_btVector3_push_back2(IntPtr obj, [In] ref Vector4 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAlignedObjectArray_btVector3_set(IntPtr obj, int n, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btAlignedObjectArray_btVector3_size(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAlignedObjectArray_btVector3_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btAngularLimit_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAngularLimit_fit(IntPtr obj, ref float angle);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btAngularLimit_getBiasFactor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btAngularLimit_getCorrection(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btAngularLimit_getError(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btAngularLimit_getHalfRange(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btAngularLimit_getHigh(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btAngularLimit_getLow(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btAngularLimit_getRelaxationFactor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btAngularLimit_getSign(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btAngularLimit_getSoftness(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btAngularLimit_isLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAngularLimit_set(IntPtr obj, float low, float high, float _softness, float _biasFactor, float _relaxationFactor);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAngularLimit_test(IntPtr obj, float angle);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAngularLimit_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btAxisSweep3_new([In] ref Vector3 worldAabbMin, [In] ref Vector3 worldAabbMax, ushort maxHandles, IntPtr pairCache, bool disableRaycastAccelerator);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern ushort btAxisSweep3_addHandle(IntPtr obj, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax, IntPtr pOwner, int collisionFilterGroup, int collisionFilterMask, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btAxisSweep3_getHandle(IntPtr obj, ushort index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern ushort btAxisSweep3_getNumHandles(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAxisSweep3_quantize(IntPtr obj, ushort o, [In] ref Vector3 point, int isMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAxisSweep3_removeHandle(IntPtr obj, ushort handle, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAxisSweep3_setOverlappingPairUserCallback(IntPtr obj, IntPtr pairCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btAxisSweep3_testAabbOverlap(IntPtr obj, IntPtr proxy0, IntPtr proxy1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAxisSweep3_unQuantize(IntPtr obj, IntPtr proxy, [Out] out Vector3 aabbMin, [Out] out Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAxisSweep3_updateHandle(IntPtr obj, ushort handle, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax, IntPtr dispatcher);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBox2dBox2dCollisionAlgorithm_CreateFunc_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBox2dBox2dCollisionAlgorithm_new(IntPtr ci);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBox2dBox2dCollisionAlgorithm_new2(IntPtr mf, IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBox2dShape_new([In] ref Vector3 boxHalfExtents);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBox2dShape_new2(float boxHalfExtent);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBox2dShape_new3(float boxHalfExtentX, float boxHalfExtentY, float boxHalfExtentZ);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBox2dShape_getCentroid(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBox2dShape_getHalfExtentsWithMargin(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBox2dShape_getHalfExtentsWithoutMargin(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBox2dShape_getNormals(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBox2dShape_getPlaneEquation(IntPtr obj, out Vector4 plane, int i);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBox2dShape_getVertices(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBoxBoxCollisionAlgorithm_CreateFunc_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBoxBoxCollisionAlgorithm_new(IntPtr ci);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBoxBoxCollisionAlgorithm_new2(IntPtr mf, IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBoxBoxDetector_new(IntPtr box1, IntPtr box2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBoxBoxDetector_setBox1(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBoxBoxDetector_setBox2(IntPtr obj, IntPtr value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBoxShape_new([In] ref Vector3 boxHalfExtents);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBoxShape_new2(float boxHalfExtent);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBoxShape_new3(float boxHalfExtentX, float boxHalfExtentY, float boxHalfExtentZ);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBoxShape_getHalfExtentsWithMargin(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBoxShape_getHalfExtentsWithoutMargin(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBoxShape_getPlaneEquation(IntPtr obj, out Vector4 plane, int i);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBroadphaseAabbCallbackWrapper_new(IntPtr process);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseAabbCallback_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseInterface_aabbTest(IntPtr obj, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax, IntPtr callback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseInterface_calculateOverlappingPairs(IntPtr obj, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBroadphaseInterface_createProxy(IntPtr obj, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax, int shapeType, IntPtr userPtr, int collisionFilterGroup, int collisionFilterMask, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseInterface_destroyProxy(IntPtr obj, IntPtr proxy, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseInterface_getAabb(IntPtr obj, IntPtr proxy, out Vector3 aabbMin, out Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseInterface_getBroadphaseAabb(IntPtr obj, out Vector3 aabbMin, out Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBroadphaseInterface_getOverlappingPairCache(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseInterface_printStats(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseInterface_rayTest(IntPtr obj, [In] ref Vector3 rayFrom, [In] ref Vector3 rayTo, IntPtr rayCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseInterface_rayTest3(IntPtr obj, [In] ref Vector3 rayFrom, [In] ref Vector3 rayTo, IntPtr rayCallback, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseInterface_resetPool(IntPtr obj, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseInterface_setAabb(IntPtr obj, IntPtr proxy, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseInterface_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBroadphasePair_getAlgorithm(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBroadphasePair_getPProxy0(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBroadphasePair_getPProxy1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphasePair_setAlgorithm(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphasePair_setPProxy0(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphasePair_setPProxy1(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphasePair_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseProxy_getAabbMax(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseProxy_getAabbMin(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBroadphaseProxy_getClientObject(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btBroadphaseProxy_getCollisionFilterGroup(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btBroadphaseProxy_getCollisionFilterMask(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btBroadphaseProxy_getUid(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btBroadphaseProxy_getUniqueId(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btBroadphaseProxy_isCompound(BroadphaseNativeType proxyType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btBroadphaseProxy_isConcave(BroadphaseNativeType proxyType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btBroadphaseProxy_isConvex(BroadphaseNativeType proxyType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btBroadphaseProxy_isConvex2d(BroadphaseNativeType proxyType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btBroadphaseProxy_isInfinite(BroadphaseNativeType proxyType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btBroadphaseProxy_isNonMoving(BroadphaseNativeType proxyType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btBroadphaseProxy_isPolyhedral(BroadphaseNativeType proxyType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btBroadphaseProxy_isSoftBody(BroadphaseNativeType proxyType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseProxy_setAabbMax(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseProxy_setAabbMin(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseProxy_setClientObject(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseProxy_setCollisionFilterGroup(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseProxy_setCollisionFilterMask(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseProxy_setUniqueId(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseProxy_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBroadphaseRayCallbackWrapper_new(IntPtr process);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btBroadphaseRayCallback_getLambda_max(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseRayCallback_getRayDirectionInverse(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBroadphaseRayCallback_getSigns(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseRayCallback_setLambda_max(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseRayCallback_setRayDirectionInverse(IntPtr obj, [In] ref Vector3 value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBU_Simplex1to4_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBU_Simplex1to4_new2([In] ref Vector3 pt0);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBU_Simplex1to4_new3([In] ref Vector3 pt0, [In] ref Vector3 pt1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBU_Simplex1to4_new4([In] ref Vector3 pt0, [In] ref Vector3 pt1, [In] ref Vector3 pt2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBU_Simplex1to4_new5([In] ref Vector3 pt0, [In] ref Vector3 pt1, [In] ref Vector3 pt2, [In] ref Vector3 pt3);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBU_Simplex1to4_addVertex(IntPtr obj, [In] ref Vector3 pt);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btBU_Simplex1to4_getIndex(IntPtr obj, int i);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBU_Simplex1to4_reset(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBvhTree_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBvhTree_build_tree(IntPtr obj, IntPtr primitive_boxes);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBvhTree_clearNodes(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBvhTree_get_node_pointer(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBvhTree_get_node_pointer2(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btBvhTree_getEscapeNodeIndex(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btBvhTree_getLeftNode(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBvhTree_getNodeBound(IntPtr obj, int nodeindex, IntPtr bound);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btBvhTree_getNodeCount(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btBvhTree_getNodeData(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btBvhTree_getRightNode(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btBvhTree_isLeafNode(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBvhTree_setNodeBound(IntPtr obj, int nodeindex, IntPtr bound);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBvhTree_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBvhTriangleMeshShape_new(IntPtr meshInterface, bool useQuantizedAabbCompression, bool buildBvh);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBvhTriangleMeshShape_new2(IntPtr meshInterface, bool useQuantizedAabbCompression, [In] ref Vector3 bvhAabbMin, [In] ref Vector3 bvhAabbMax, bool buildBvh);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBvhTriangleMeshShape_buildOptimizedBvh(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBvhTriangleMeshShape_getOptimizedBvh(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btBvhTriangleMeshShape_getOwnsBvh(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBvhTriangleMeshShape_getTriangleInfoMap(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBvhTriangleMeshShape_partialRefitTree(IntPtr obj, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBvhTriangleMeshShape_performConvexcast(IntPtr obj, IntPtr callback, [In] ref Vector3 boxSource, [In] ref Vector3 boxTarget, [In] ref Vector3 boxMin, [In] ref Vector3 boxMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBvhTriangleMeshShape_performRaycast(IntPtr obj, IntPtr callback, [In] ref Vector3 raySource, [In] ref Vector3 rayTarget);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBvhTriangleMeshShape_refitTree(IntPtr obj, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBvhTriangleMeshShape_serializeSingleBvh(IntPtr obj, IntPtr serializer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBvhTriangleMeshShape_serializeSingleTriangleInfoMap(IntPtr obj, IntPtr serializer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBvhTriangleMeshShape_setOptimizedBvh(IntPtr obj, IntPtr bvh);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBvhTriangleMeshShape_setOptimizedBvh2(IntPtr obj, IntPtr bvh, [In] ref Vector3 localScaling);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBvhTriangleMeshShape_setTriangleInfoMap(IntPtr obj, IntPtr triangleInfoMap);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btBvhTriangleMeshShape_usesQuantizedAabbCompression(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCapsuleShape_new(float radius, float height);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCapsuleShape_getHalfHeight(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCapsuleShape_getRadius(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCapsuleShape_getUpAxis(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCapsuleShapeX_new(float radius, float height);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCapsuleShapeZ_new(float radius, float height);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionAlgorithm_calculateTimeOfImpact(IntPtr obj, IntPtr body0, IntPtr body1, IntPtr dispatchInfo, IntPtr resultOut);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionAlgorithm_getAllContactManifolds(IntPtr obj, IntPtr manifoldArray);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionAlgorithm_processCollision(IntPtr obj, IntPtr body0Wrap, IntPtr body1Wrap, IntPtr dispatchInfo, IntPtr resultOut);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionAlgorithm_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionAlgorithmConstructionInfo_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionAlgorithmConstructionInfo_new2(IntPtr dispatcher, int temp);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionAlgorithmConstructionInfo_setDispatcher1(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionAlgorithmConstructionInfo_setManifold(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionAlgorithmConstructionInfo_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionAlgorithmCreateFunc_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionAlgorithmCreateFunc_CreateCollisionAlgorithm(IntPtr obj, IntPtr __unnamed0, IntPtr body0Wrap, IntPtr body1Wrap);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionAlgorithmCreateFunc_getSwapped(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionAlgorithmCreateFunc_setSwapped(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionAlgorithmCreateFunc_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionConfiguration_getClosestPointsAlgorithmCreateFunc(IntPtr obj, int proxyType0, int proxyType1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionConfiguration_getCollisionAlgorithmCreateFunc(IntPtr obj, int proxyType0, int proxyType1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionConfiguration_getCollisionAlgorithmPool(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionConfiguration_getPersistentManifoldPool(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionConfiguration_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionDispatcher_new(IntPtr collisionConfiguration);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionDispatcher_defaultNearCallback(IntPtr collisionPair, IntPtr dispatcher, IntPtr dispatchInfo);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionDispatcher_getCollisionConfiguration(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern DispatcherFlags btCollisionDispatcher_getDispatcherFlags(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionDispatcher_getNearCallback(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionDispatcher_registerCollisionCreateFunc(IntPtr obj, BroadphaseNativeType proxyType0, BroadphaseNativeType proxyType1, IntPtr createFunc);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionDispatcher_registerClosestPointsCreateFunc(IntPtr obj, BroadphaseNativeType proxyType0, BroadphaseNativeType proxyType1, IntPtr createFunc);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionDispatcher_setCollisionConfiguration(IntPtr obj, IntPtr config);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionDispatcher_setDispatcherFlags(IntPtr obj, DispatcherFlags flags);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionDispatcher_setNearCallback(IntPtr obj, IntPtr nearCallback);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionDispatcherMt_new(IntPtr collisionConfiguration, int grainSize);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionObject_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_activate(IntPtr obj, bool forceActivation);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCollisionObject_calculateSerializeBufferSize(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionObject_checkCollideWith(IntPtr obj, IntPtr co);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionObject_checkCollideWithOverride(IntPtr obj, IntPtr co);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_forceActivationState(IntPtr obj, ActivationState newState);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern ActivationState btCollisionObject_getActivationState(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_getAnisotropicFriction(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionObject_getBroadphaseHandle(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionObject_getCcdMotionThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionObject_getCcdSquareMotionThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionObject_getCcdSweptSphereRadius(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern CollisionFlags btCollisionObject_getCollisionFlags(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionObject_getCollisionShape(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCollisionObject_getCompanionId(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionObject_getContactDamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionObject_getContactProcessingThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionObject_getContactStiffness(IntPtr obj);
		[return: MarshalAs(UnmanagedType.I1)]
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern bool btCollisionObject_getCustomDebugColor(IntPtr obj, out Vector3 colorRGB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionObject_getDeactivationTime(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionObject_getFriction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionObject_getHitFraction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern CollisionObjectTypes btCollisionObject_getInternalType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_getInterpolationAngularVelocity(IntPtr obj, out Vector3 angvel);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_getInterpolationLinearVelocity(IntPtr obj, out Vector3 linvel);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_getInterpolationWorldTransform(IntPtr obj, out Matrix trans);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCollisionObject_getIslandTag(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionObject_getRestitution(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionObject_getRollingFriction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionObject_getSpinningFriction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCollisionObject_getWorldArrayIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCollisionObject_getUserIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCollisionObject_getUserIndex2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionObject_getUserPointer(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_getWorldTransform(IntPtr obj, out Matrix worldTrans);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionObject_hasAnisotropicFriction(IntPtr obj, AnisotropicFrictionFlags frictionMode);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionObject_hasContactResponse(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionObject_internalGetExtensionPointer(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_internalSetExtensionPointer(IntPtr obj, IntPtr pointer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionObject_isActive(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionObject_isKinematicObject(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionObject_isStaticObject(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionObject_isStaticOrKinematicObject(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionObject_mergesSimulationIslands(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_removeCustomDebugColor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionObject_serialize(IntPtr obj, IntPtr dataBuffer, IntPtr serializer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_serializeSingleObject(IntPtr obj, IntPtr serializer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setActivationState(IntPtr obj, ActivationState newState);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setAnisotropicFriction(IntPtr obj, [In] ref Vector3 anisotropicFriction, AnisotropicFrictionFlags frictionMode);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setBroadphaseHandle(IntPtr obj, IntPtr handle);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setCcdMotionThreshold(IntPtr obj, float ccdMotionThreshold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setCcdSweptSphereRadius(IntPtr obj, float radius);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setCollisionFlags(IntPtr obj, CollisionFlags flags);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setCollisionShape(IntPtr obj, IntPtr collisionShape);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setCompanionId(IntPtr obj, int id);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setContactProcessingThreshold(IntPtr obj, float contactProcessingThreshold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setContactStiffnessAndDamping(IntPtr obj, float stiffness, float damping);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setCustomDebugColor(IntPtr obj, ref Vector3 colorRGB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setDeactivationTime(IntPtr obj, float time);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setFriction(IntPtr obj, float frict);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setHitFraction(IntPtr obj, float hitFraction);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setIgnoreCollisionCheck(IntPtr obj, IntPtr co, bool ignoreCollisionCheck);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setInterpolationAngularVelocity(IntPtr obj, [In] ref Vector3 angvel);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setInterpolationLinearVelocity(IntPtr obj, [In] ref Vector3 linvel);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setInterpolationWorldTransform(IntPtr obj, [In] ref Matrix trans);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setIslandTag(IntPtr obj, int tag);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setRestitution(IntPtr obj, float rest);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setRollingFriction(IntPtr obj, float frict);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setSpinningFriction(IntPtr obj, float frict);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setWorldArrayIndex(IntPtr obj, int id);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setUserIndex(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setUserIndex2(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setUserPointer(IntPtr obj, IntPtr userPointer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setWorldTransform(IntPtr obj, [In] ref Matrix worldTrans);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionObjectWrapper_getCollisionObject(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionObjectWrapper_getCollisionShape(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCollisionObjectWrapper_getIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionObjectWrapper_getParent(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCollisionObjectWrapper_getPartId(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObjectWrapper_getWorldTransform(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObjectWrapper_setCollisionObject(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObjectWrapper_setIndex(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObjectWrapper_setParent(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObjectWrapper_setPartId(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObjectWrapper_setShape(IntPtr obj, IntPtr value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionShape_calculateLocalInertia(IntPtr obj, float mass, out Vector3 inertia);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCollisionShape_calculateSerializeBufferSize(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionShape_calculateTemporalAabb(IntPtr obj, [In] ref Matrix curTrans, [In] ref Vector3 linvel, [In] ref Vector3 angvel, float timeStep, out Vector3 temporalAabbMin, out Vector3 temporalAabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionShape_getAabb(IntPtr obj, [In] ref Matrix t, out Vector3 aabbMin, out Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionShape_getAngularMotionDisc(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionShape_getAnisotropicRollingFrictionDirection(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionShape_getBoundingSphere(IntPtr obj, out Vector3 center, out float radius);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionShape_getContactBreakingThreshold(IntPtr obj, float defaultContactThresholdFactor);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionShape_getLocalScaling(IntPtr obj, out Vector3 scaling);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionShape_getMargin(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionShape_getName(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern BroadphaseNativeType btCollisionShape_getShapeType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCollisionShape_getUserIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionShape_getUserPointer(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionShape_isCompound(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionShape_isConcave(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionShape_isConvex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionShape_isConvex2d(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionShape_isInfinite(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionShape_isNonMoving(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionShape_isPolyhedral(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionShape_isSoftBody(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionShape_serialize(IntPtr obj, IntPtr dataBuffer, IntPtr serializer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionShape_serializeSingleShape(IntPtr obj, IntPtr serializer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionShape_setLocalScaling(IntPtr obj, [In] ref Vector3 scaling);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionShape_setMargin(IntPtr obj, float margin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionShape_setUserIndex(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionShape_setUserPointer(IntPtr obj, IntPtr userPtr);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionShape_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionWorld_ContactResultCallback_getClosestDistanceThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCollisionWorld_ContactResultCallback_getCollisionFilterGroup(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCollisionWorld_ContactResultCallback_getCollisionFilterMask(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_ContactResultCallback_setClosestDistanceThreshold(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_ContactResultCallback_setCollisionFilterGroup(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_ContactResultCallback_setCollisionFilterMask(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_ContactResultCallback_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionWorld_ContactResultCallbackWrapper_new(IntPtr addSingleResult, IntPtr needsCollision);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionWorld_ContactResultCallbackWrapper_needsCollision(IntPtr obj, IntPtr proxy0);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionWorld_ConvexResultCallback_getClosestHitFraction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCollisionWorld_ConvexResultCallback_getCollisionFilterGroup(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCollisionWorld_ConvexResultCallback_getCollisionFilterMask(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionWorld_ConvexResultCallback_hasHit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_ConvexResultCallback_setClosestHitFraction(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_ConvexResultCallback_setCollisionFilterGroup(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_ConvexResultCallback_setCollisionFilterMask(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_ConvexResultCallback_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionWorld_ConvexResultCallbackWrapper_new(IntPtr addSingleResult, IntPtr needsCollision);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionWorld_ConvexResultCallbackWrapper_needsCollision(IntPtr obj, IntPtr proxy0);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionWorld_LocalConvexResult_new(IntPtr hitCollisionObject, IntPtr localShapeInfo, [In] ref Vector3 hitNormalLocal, [In] ref Vector3 hitPointLocal, float hitFraction);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionWorld_LocalConvexResult_getHitCollisionObject(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionWorld_LocalConvexResult_getHitFraction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_LocalConvexResult_getHitNormalLocal(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_LocalConvexResult_getHitPointLocal(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionWorld_LocalConvexResult_getLocalShapeInfo(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_LocalConvexResult_setHitCollisionObject(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_LocalConvexResult_setHitFraction(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_LocalConvexResult_setHitNormalLocal(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_LocalConvexResult_setHitPointLocal(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_LocalConvexResult_setLocalShapeInfo(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_LocalConvexResult_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionWorld_LocalRayResult_new(IntPtr collisionObject, IntPtr localShapeInfo, [In] ref Vector3 hitNormalLocal, float hitFraction);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionWorld_LocalRayResult_getCollisionObject(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionWorld_LocalRayResult_getHitFraction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_LocalRayResult_getHitNormalLocal(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionWorld_LocalRayResult_getLocalShapeInfo(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_LocalRayResult_setCollisionObject(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_LocalRayResult_setHitFraction(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_LocalRayResult_setHitNormalLocal(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_LocalRayResult_setLocalShapeInfo(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_LocalRayResult_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionWorld_LocalShapeInfo_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCollisionWorld_LocalShapeInfo_getShapePart(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCollisionWorld_LocalShapeInfo_getTriangleIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_LocalShapeInfo_setShapePart(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_LocalShapeInfo_setTriangleIndex(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_LocalShapeInfo_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionWorld_RayResultCallback_getClosestHitFraction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCollisionWorld_RayResultCallback_getCollisionFilterGroup(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCollisionWorld_RayResultCallback_getCollisionFilterMask(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionWorld_RayResultCallback_getCollisionObject(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern uint btCollisionWorld_RayResultCallback_getFlags(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionWorld_RayResultCallback_hasHit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_RayResultCallback_setClosestHitFraction(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_RayResultCallback_setCollisionFilterGroup(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_RayResultCallback_setCollisionFilterMask(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_RayResultCallback_setCollisionObject(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_RayResultCallback_setFlags(IntPtr obj, uint value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_RayResultCallback_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionWorld_RayResultCallbackWrapper_new(IntPtr addSingleResult, IntPtr needsCollision);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionWorld_RayResultCallbackWrapper_needsCollision(IntPtr obj, IntPtr proxy0);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionWorld_new(IntPtr dispatcher, IntPtr broadphasePairCache, IntPtr collisionConfiguration);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_addCollisionObject(IntPtr obj, IntPtr collisionObject);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_addCollisionObject3(IntPtr obj, IntPtr collisionObject, int collisionFilterGroup, int collisionFilterMask);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_computeOverlappingPairs(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_contactPairTest(IntPtr obj, IntPtr colObjA, IntPtr colObjB, IntPtr resultCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_contactTest(IntPtr obj, IntPtr colObj, IntPtr resultCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_convexSweepTest(IntPtr obj, IntPtr castShape, [In] ref Matrix from, [In] ref Matrix to, IntPtr resultCallback, float allowedCcdPenetration);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_debugDrawObject(IntPtr obj, [In] ref Matrix worldTransform, IntPtr shape, [In] ref Vector3 color);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_debugDrawWorld(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionWorld_getBroadphase(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		internal static extern IntPtr btCollisionWorld_getCollisionObjectArray(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionWorld_getDebugDrawer(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionWorld_getDispatcher(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionWorld_getDispatchInfo(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionWorld_getForceUpdateAllAabbs(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCollisionWorld_getNumCollisionObjects(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionWorld_getPairCache(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_objectQuerySingle(IntPtr castShape, [In] ref Matrix rayFromTrans, [In] ref Matrix rayToTrans, IntPtr collisionObject, IntPtr collisionShape, [In] ref Matrix colObjWorldTransform, IntPtr resultCallback, float allowedPenetration);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_objectQuerySingleInternal(IntPtr castShape, [In] ref Matrix convexFromTrans, [In] ref Matrix convexToTrans, IntPtr colObjWrap, IntPtr resultCallback, float allowedPenetration);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_performDiscreteCollisionDetection(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_rayTest(IntPtr obj, [In] ref Vector3 rayFromWorld, [In] ref Vector3 rayToWorld, IntPtr resultCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_rayTestSingle([In] ref Matrix rayFromTrans, [In] ref Matrix rayToTrans, IntPtr collisionObject, IntPtr collisionShape, [In] ref Matrix colObjWorldTransform, IntPtr resultCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_rayTestSingleInternal([In] ref Matrix rayFromTrans, [In] ref Matrix rayToTrans, IntPtr collisionObjectWrap, IntPtr resultCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_removeCollisionObject(IntPtr obj, IntPtr collisionObject);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_setBroadphase(IntPtr obj, IntPtr pairCache);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_setDebugDrawer(IntPtr obj, IntPtr debugDrawer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_setForceUpdateAllAabbs(IntPtr obj, bool forceUpdateAllAabbs);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_updateAabbs(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_updateSingleAabb(IntPtr obj, IntPtr colObj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCompoundCollisionAlgorithm_CreateFunc_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCompoundCollisionAlgorithm_SwappedCreateFunc_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCompoundCollisionAlgorithm_new(IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap, bool isSwapped);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCompoundCollisionAlgorithm_getChildAlgorithm(IntPtr obj, int n);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCompoundCompoundCollisionAlgorithm_CreateFunc_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCompoundCompoundCollisionAlgorithm_SwappedCreateFunc_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCompoundCompoundCollisionAlgorithm_new(IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap, bool isSwapped);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCompoundShape_new(bool enableDynamicAabbTree, int initialChildCapacity);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCompoundShape_calculatePrincipalAxisTransform(IntPtr obj, float[] masses, ref Matrix principal, out Vector3 inertia);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCompoundShape_createAabbTreeFromChildren(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCompoundShape_getChildShape(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCompoundShape_getChildTransform(IntPtr obj, int index, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCompoundShape_getDynamicAabbTree(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCompoundShape_getUpdateRevision(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCompoundShape_recalculateLocalAabb(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCompoundShape_updateChildTransform(IntPtr obj, int childIndex, [In] ref Matrix newChildTransform, bool shouldRecalculateLocalAabb);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCompoundShape_addChildShape(IntPtr obj, [In] ref Matrix localTransform, IntPtr shape);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCompoundShape_getChildList(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCompoundShapeChild_getChildMargin(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern BroadphaseNativeType btCompoundShapeChild_getChildShapeType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCompoundShapeChild_getNode(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCompoundShapeChild_getTransform(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCompoundShape_removeChildShapeByIndex(IntPtr obj, int childShapeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCompoundShapeChild_setChildMargin(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCompoundShapeChild_setChildShape(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCompoundShapeChild_setChildShapeType(IntPtr obj, BroadphaseNativeType value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCompoundShapeChild_setNode(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCompoundShapeChild_setTransform(IntPtr obj, [In] ref Matrix value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCompoundShapeChild_array_at(IntPtr obj, int n);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConcaveShape_processAllTriangles(IntPtr obj, IntPtr callback, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConeShape_new(float radius, float height);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btConeShape_getConeUpIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btConeShape_getHeight(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btConeShape_getRadius(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConeShape_setConeUpIndex(IntPtr obj, int upIndex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConeShape_setHeight(IntPtr obj, float height);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConeShape_setRadius(IntPtr obj, float radius);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConeShapeX_new(float radius, float height);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConeShapeZ_new(float radius, float height);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConeTwistConstraint_new(IntPtr rbA, IntPtr rbB, [In] ref Matrix rbAFrame, [In] ref Matrix rbBFrame);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConeTwistConstraint_new2(IntPtr rbA, [In] ref Matrix rbAFrame);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConeTwistConstraint_calcAngleInfo(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConeTwistConstraint_calcAngleInfo2(IntPtr obj, [In] ref Matrix transA, [In] ref Matrix transB, [In] ref Matrix invInertiaWorldA, [In] ref Matrix invInertiaWorldB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConeTwistConstraint_enableMotor(IntPtr obj, bool b);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConeTwistConstraint_getAFrame(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btConeTwistConstraint_getAngularOnly(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConeTwistConstraint_getBFrame(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btConeTwistConstraint_getBiasFactor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btConeTwistConstraint_getDamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btConeTwistConstraint_getFixThresh(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern ConeTwistFlags btConeTwistConstraint_getFlags(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConeTwistConstraint_getFrameOffsetA(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConeTwistConstraint_getFrameOffsetB(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConeTwistConstraint_getInfo1NonVirtual(IntPtr obj, IntPtr info);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConeTwistConstraint_getInfo2NonVirtual(IntPtr obj, IntPtr info, [In] ref Matrix transA, [In] ref Matrix transB, [In] ref Matrix invInertiaWorldA, [In] ref Matrix invInertiaWorldB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btConeTwistConstraint_getLimit(IntPtr obj, int limitIndex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btConeTwistConstraint_getLimitSoftness(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btConeTwistConstraint_getMaxMotorImpulse(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConeTwistConstraint_getMotorTarget(IntPtr obj, out Quaternion q);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConeTwistConstraint_GetPointForAngle(IntPtr obj, float fAngleInRadians, float fLength, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btConeTwistConstraint_getRelaxationFactor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btConeTwistConstraint_getSolveSwingLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btConeTwistConstraint_getSolveTwistLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btConeTwistConstraint_getSwingSpan1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btConeTwistConstraint_getSwingSpan2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btConeTwistConstraint_getTwistAngle(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btConeTwistConstraint_getTwistLimitSign(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btConeTwistConstraint_getTwistSpan(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btConeTwistConstraint_isMaxMotorImpulseNormalized(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btConeTwistConstraint_isMotorEnabled(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btConeTwistConstraint_isPastSwingLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConeTwistConstraint_setAngularOnly(IntPtr obj, bool angularOnly);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConeTwistConstraint_setDamping(IntPtr obj, float damping);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConeTwistConstraint_setFixThresh(IntPtr obj, float fixThresh);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConeTwistConstraint_setFrames(IntPtr obj, [In] ref Matrix frameA, [In] ref Matrix frameB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConeTwistConstraint_setLimit(IntPtr obj, int limitIndex, float limitValue);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConeTwistConstraint_setLimit2(IntPtr obj, float _swingSpan1, float _swingSpan2, float _twistSpan, float _softness, float _biasFactor, float _relaxationFactor);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConeTwistConstraint_setMaxMotorImpulse(IntPtr obj, float maxMotorImpulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConeTwistConstraint_setMaxMotorImpulseNormalized(IntPtr obj, float maxMotorImpulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConeTwistConstraint_setMotorTarget(IntPtr obj, [In] ref Quaternion q);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConeTwistConstraint_setMotorTargetInConstraintSpace(IntPtr obj, [In] ref Quaternion q);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConeTwistConstraint_updateRHS(IntPtr obj, float timeStep);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConstraintSetting_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btConstraintSetting_getDamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btConstraintSetting_getImpulseClamp(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btConstraintSetting_getTau(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConstraintSetting_setDamping(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConstraintSetting_setImpulseClamp(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConstraintSetting_setTau(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConstraintSetting_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConstraintSolver_allSolved(IntPtr obj, IntPtr __unnamed0, IntPtr __unnamed1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern ConstraintSolverType btConstraintSolver_getSolverType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConstraintSolver_prepareSolve(IntPtr obj, int __unnamed0, int __unnamed1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConstraintSolver_reset(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btConstraintSolver_solveGroup(IntPtr obj, IntPtr bodies, int numBodies, IntPtr manifold, int numManifolds, IntPtr constraints, int numConstraints, IntPtr info, IntPtr debugDrawer, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConstraintSolver_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConstraintSolverPoolMt_new(int numSolvers);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConstraintSolverPoolMt_new2(IntPtr solvers, int numSolvers);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btContactSolverInfo_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btContactSolverInfoData_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btContactSolverInfoData_getDamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btContactSolverInfoData_getErp(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btContactSolverInfoData_getErp2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btContactSolverInfoData_getFriction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btContactSolverInfoData_getFrictionCfm(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btContactSolverInfoData_getFrictionErp(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btContactSolverInfoData_getGlobalCfm(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btContactSolverInfoData_getLinearSlop(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btContactSolverInfoData_getMaxErrorReduction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btContactSolverInfoData_getMaxGyroscopicForce(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btContactSolverInfoData_getMinimumSolverBatchSize(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btContactSolverInfoData_getNumIterations(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btContactSolverInfoData_getRestingContactRestitutionThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btContactSolverInfoData_getRestitution(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btContactSolverInfoData_getSingleAxisRollingFrictionThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern SolverModes btContactSolverInfoData_getSolverMode(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btContactSolverInfoData_getSor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btContactSolverInfoData_getSplitImpulse(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btContactSolverInfoData_getSplitImpulsePenetrationThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btContactSolverInfoData_getSplitImpulseTurnErp(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btContactSolverInfoData_getTau(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btContactSolverInfoData_getTimeStep(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btContactSolverInfoData_getWarmstartingFactor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btContactSolverInfoData_setDamping(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btContactSolverInfoData_setErp(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btContactSolverInfoData_setErp2(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btContactSolverInfoData_setFriction(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btContactSolverInfoData_setFrictionCfm(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btContactSolverInfoData_setFrictionErp(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btContactSolverInfoData_setGlobalCfm(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btContactSolverInfoData_setLinearSlop(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btContactSolverInfoData_setMaxErrorReduction(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btContactSolverInfoData_setMaxGyroscopicForce(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btContactSolverInfoData_setMinimumSolverBatchSize(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btContactSolverInfoData_setNumIterations(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btContactSolverInfoData_setRestingContactRestitutionThreshold(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btContactSolverInfoData_setRestitution(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btContactSolverInfoData_setSingleAxisRollingFrictionThreshold(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btContactSolverInfoData_setSolverMode(IntPtr obj, SolverModes value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btContactSolverInfoData_setSor(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btContactSolverInfoData_setSplitImpulse(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btContactSolverInfoData_setSplitImpulsePenetrationThreshold(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btContactSolverInfoData_setSplitImpulseTurnErp(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btContactSolverInfoData_setTau(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btContactSolverInfoData_setTimeStep(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btContactSolverInfoData_setWarmstartingFactor(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btContactSolverInfoData_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btContinuousConvexCollision_new(IntPtr shapeA, IntPtr shapeB, IntPtr simplexSolver, IntPtr penetrationDepthSolver);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btContinuousConvexCollision_new2(IntPtr shapeA, IntPtr plane);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvex2dConvex2dAlgorithm_CreateFunc_new(IntPtr simplexSolver, IntPtr pdSolver);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btConvex2dConvex2dAlgorithm_CreateFunc_getMinimumPointsPerturbationThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btConvex2dConvex2dAlgorithm_CreateFunc_getNumPerturbationIterations(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvex2dConvex2dAlgorithm_CreateFunc_setMinimumPointsPerturbationThreshold(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvex2dConvex2dAlgorithm_CreateFunc_setNumPerturbationIterations(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvex2dConvex2dAlgorithm_CreateFunc_setPdSolver(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvex2dConvex2dAlgorithm_CreateFunc_setSimplexSolver(IntPtr obj, IntPtr value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvex2dConvex2dAlgorithm_new(IntPtr mf, IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap, IntPtr simplexSolver, IntPtr pdSolver, int numPerturbationIterations, int minimumPointsPerturbationThreshold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvex2dConvex2dAlgorithm_getManifold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvex2dConvex2dAlgorithm_setLowLevelOfDetail(IntPtr obj, bool useLowLevel);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvex2dShape_new(IntPtr convexChildShape);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexCast_CastResult_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexCast_CastResult_DebugDraw(IntPtr obj, float fraction);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexCast_CastResult_drawCoordSystem(IntPtr obj, [In] ref Matrix trans);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btConvexCast_CastResult_getAllowedPenetration(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexCast_CastResult_getDebugDrawer(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btConvexCast_CastResult_getFraction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexCast_CastResult_getHitPoint(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexCast_CastResult_getHitTransformA(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexCast_CastResult_getHitTransformB(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexCast_CastResult_getNormal(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexCast_CastResult_reportFailure(IntPtr obj, int errNo, int numIterations);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexCast_CastResult_setAllowedPenetration(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexCast_CastResult_setDebugDrawer(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexCast_CastResult_setFraction(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexCast_CastResult_setHitPoint(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexCast_CastResult_setHitTransformA(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexCast_CastResult_setHitTransformB(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexCast_CastResult_setNormal(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexCast_CastResult_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btConvexCast_calcTimeOfImpact(IntPtr obj, [In] ref Matrix fromA, [In] ref Matrix toA, [In] ref Matrix fromB, [In] ref Matrix toB, IntPtr result);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexCast_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexConcaveCollisionAlgorithm_CreateFunc_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexConcaveCollisionAlgorithm_SwappedCreateFunc_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexConcaveCollisionAlgorithm_new(IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap, bool isSwapped);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexConcaveCollisionAlgorithm_clearCache(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexConvexAlgorithm_CreateFunc_new(IntPtr pdSolver);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btConvexConvexAlgorithm_CreateFunc_getMinimumPointsPerturbationThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btConvexConvexAlgorithm_CreateFunc_getNumPerturbationIterations(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexConvexAlgorithm_CreateFunc_setMinimumPointsPerturbationThreshold(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexConvexAlgorithm_CreateFunc_setNumPerturbationIterations(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexConvexAlgorithm_CreateFunc_setPdSolver(IntPtr obj, IntPtr value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexConvexAlgorithm_new(IntPtr mf, IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap, IntPtr simplexSolver, IntPtr pdSolver, int numPerturbationIterations, int minimumPointsPerturbationThreshold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexConvexAlgorithm_getManifold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexConvexAlgorithm_setLowLevelOfDetail(IntPtr obj, bool useLowLevel);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexHullShape_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexHullShape_new4(float[] points, int numPoints, int stride);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexHullShape_addPoint(IntPtr obj, [In] ref Vector3 point, bool recalculateLocalAabb);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btConvexHullShape_getNumPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexHullShape_getPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexHullShape_getScaledPoint(IntPtr obj, int i, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexHullShape_getUnscaledPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexHullShape_optimizeConvexHull(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexInternalAabbCachingShape_recalcLocalAabb(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexInternalShape_getImplicitShapeDimensions(IntPtr obj, out Vector3 dimensions);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexInternalShape_getLocalScalingNV(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btConvexInternalShape_getMarginNV(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexInternalShape_setImplicitShapeDimensions(IntPtr obj, [In] ref Vector3 dimensions);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexInternalShape_setSafeMargin(IntPtr obj, float minDimension, float defaultMarginMultiplier);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexInternalShape_setSafeMargin2(IntPtr obj, [In] ref Vector3 halfExtents, float defaultMarginMultiplier);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btConvexPenetrationDepthSolver_calcPenDepth(IntPtr obj, IntPtr simplexSolver, IntPtr convexA, IntPtr convexB, [In] ref Matrix transA, [In] ref Matrix transB, out Vector3 v, out Vector3 pa, out Vector3 pb, IntPtr debugDraw);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexPenetrationDepthSolver_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexPlaneCollisionAlgorithm_CreateFunc_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btConvexPlaneCollisionAlgorithm_CreateFunc_getMinimumPointsPerturbationThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btConvexPlaneCollisionAlgorithm_CreateFunc_getNumPerturbationIterations(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexPlaneCollisionAlgorithm_CreateFunc_setMinimumPointsPerturbationThreshold(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexPlaneCollisionAlgorithm_CreateFunc_setNumPerturbationIterations(IntPtr obj, int value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexPlaneCollisionAlgorithm_new(IntPtr mf, IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap, bool isSwapped, int numPerturbationIterations, int minimumPointsPerturbationThreshold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexPlaneCollisionAlgorithm_collideSingleContact(IntPtr obj, [In] ref Quaternion perturbeRot, IntPtr body0Wrap, IntPtr body1Wrap, IntPtr dispatchInfo, IntPtr resultOut);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexPointCloudShape_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexPointCloudShape_new2(IntPtr points, int numPoints, [In] ref Vector3 localScaling, bool computeAabb);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btConvexPointCloudShape_getNumPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexPointCloudShape_getScaledPoint(IntPtr obj, int index, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexPointCloudShape_getUnscaledPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexPointCloudShape_setPoints(IntPtr obj, IntPtr points, int numPoints, bool computeAabb);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexPointCloudShape_setPoints2(IntPtr obj, IntPtr points, int numPoints, bool computeAabb, [In] ref Vector3 localScaling);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexPolyhedron_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexPolyhedron_getExtents(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexPolyhedron_getFaces(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexPolyhedron_getLocalCenter(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexPolyhedron_getMC(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexPolyhedron_getME(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btConvexPolyhedron_getRadius(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexPolyhedron_getUniqueEdges(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexPolyhedron_getVertices(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexPolyhedron_initialize(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexPolyhedron_project(IntPtr obj, [In] ref Matrix trans, [In] ref Vector3 dir, out float minProj, out float maxProj, out Vector3 witnesPtMin, out Vector3 witnesPtMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexPolyhedron_setExtents(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexPolyhedron_setLocalCenter(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexPolyhedron_setMC(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexPolyhedron_setME(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexPolyhedron_setRadius(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btConvexPolyhedron_testContainment(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexPolyhedron_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexSeparatingDistanceUtil_new(float boundingRadiusA, float boundingRadiusB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btConvexSeparatingDistanceUtil_getConservativeSeparatingDistance(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexSeparatingDistanceUtil_initSeparatingDistance(IntPtr obj, [In] ref Vector3 separatingVector, float separatingDistance, [In] ref Matrix transA, [In] ref Matrix transB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexSeparatingDistanceUtil_updateSeparatingDistance(IntPtr obj, [In] ref Matrix transA, [In] ref Matrix transB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexSeparatingDistanceUtil_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexShape_getAabbNonVirtual(IntPtr obj, [In] ref Matrix t, out Vector3 aabbMin, out Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexShape_getAabbSlow(IntPtr obj, [In] ref Matrix t, out Vector3 aabbMin, out Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btConvexShape_getMarginNonVirtual(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btConvexShape_getNumPreferredPenetrationDirections(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexShape_getPreferredPenetrationDirection(IntPtr obj, int index, out Vector3 penetrationVector);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexShape_localGetSupportingVertex(IntPtr obj, [In] ref Vector3 vec, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexShape_localGetSupportingVertexWithoutMargin(IntPtr obj, [In] ref Vector3 vec, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexShape_localGetSupportVertexNonVirtual(IntPtr obj, [In] ref Vector3 vec, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexShape_localGetSupportVertexWithoutMarginNonVirtual(IntPtr obj, [In] ref Vector3 vec, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexShape_project(IntPtr obj, [In] ref Matrix trans, [In] ref Vector3 dir, out float minProj, out float maxProj, out Vector3 witnesPtMin, out Vector3 witnesPtMax);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexTriangleMeshShape_new(IntPtr meshInterface, bool calcAabb);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexTriangleMeshShape_calculatePrincipalAxisTransform(IntPtr obj, ref Matrix principal, out Vector3 inertia, out float volume);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern CpuFeatures btCpuFeatureUtility_getCpuFeatures();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCylinderShape_new([In] ref Vector3 halfExtents);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCylinderShape_new2(float halfExtentX, float halfExtentY, float halfExtentZ);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCylinderShape_getHalfExtentsWithMargin(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCylinderShape_getHalfExtentsWithoutMargin(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCylinderShape_getRadius(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCylinderShape_getUpAxis(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCylinderShapeX_new([In] ref Vector3 halfExtents);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCylinderShapeX_new2(float halfExtentX, float halfExtentY, float halfExtentZ);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCylinderShapeZ_new([In] ref Vector3 halfExtents);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCylinderShapeZ_new2(float halfExtentX, float halfExtentY, float halfExtentZ);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDantzigSolver_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvtBroadphase_new(IntPtr paircache);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_benchmark(IntPtr __unnamed0);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_collide(IntPtr obj, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvtBroadphase_getCid(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvtBroadphase_getCupdates(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDbvtBroadphase_getDeferedcollide(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvtBroadphase_getDupdates(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvtBroadphase_getFixedleft(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvtBroadphase_getFupdates(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvtBroadphase_getGid(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDbvtBroadphase_getNeedcleanup(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvtBroadphase_getNewpairs(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvtBroadphase_getPid(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btDbvtBroadphase_getPrediction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDbvtBroadphase_getReleasepaircache(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvtBroadphase_getSets(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvtBroadphase_getStageCurrent(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvtBroadphase_getStageRoots(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern uint btDbvtBroadphase_getUpdates_call(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern uint btDbvtBroadphase_getUpdates_done(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btDbvtBroadphase_getUpdates_ratio(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btDbvtBroadphase_getVelocityPrediction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_optimize(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_performDeferredRemoval(IntPtr obj, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_setAabbForceUpdate(IntPtr obj, IntPtr absproxy, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax, IntPtr __unnamed3);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_setCid(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_setCupdates(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_setDeferedcollide(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_setDupdates(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_setFixedleft(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_setFupdates(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_setGid(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_setNeedcleanup(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_setNewpairs(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_setPaircache(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_setPid(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_setPrediction(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_setReleasepaircache(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_setStageCurrent(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_setUpdates_call(IntPtr obj, uint value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_setUpdates_done(IntPtr obj, uint value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_setUpdates_ratio(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_setVelocityPrediction(IntPtr obj, float prediction);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvtNode_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvtNode_getChilds(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvtNode_getData(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvtNode_getDataAsInt(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvtNode_getParent(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvtNode_getVolume(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDbvtNode_isinternal(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDbvtNode_isleaf(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtNode_setData(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtNode_setDataAsInt(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtNode_setParent(IntPtr obj, IntPtr value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvtProxy_getLeaf(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvtProxy_getLinks(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvtProxy_getStage(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtProxy_setLeaf(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtProxy_setStage(IntPtr obj, int value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvt_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvt_allocate(IntPtr ifree, IntPtr stock, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_benchmark();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_clear(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_clone(IntPtr obj, IntPtr dest);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_clone2(IntPtr obj, IntPtr dest, IntPtr iclone);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvt_countLeaves(IntPtr node);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDbvt_empty(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_extractLeaves(IntPtr node, IntPtr leaves);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvt_getFree(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvt_getLeaves(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvt_getLkhd(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern uint btDbvt_getOpath(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvt_getRoot(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_getStkStack(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvt_insert(IntPtr obj, IntPtr box, IntPtr data);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvt_maxdepth(IntPtr node);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvt_nearest(int[] i, IntPtr a, float v, int l, int h);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_optimizeBottomUp(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_optimizeIncremental(IntPtr obj, int passes);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_optimizeTopDown(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_optimizeTopDown2(IntPtr obj, int bu_treshold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_remove(IntPtr obj, IntPtr leaf);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_setFree(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_setLeaves(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_setLkhd(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_setOpath(IntPtr obj, uint value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_setRoot(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_update(IntPtr obj, IntPtr leaf, IntPtr volume);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_update2(IntPtr obj, IntPtr leaf);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_update3(IntPtr obj, IntPtr leaf, int lookahead);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDbvt_update4(IntPtr obj, IntPtr leaf, IntPtr volume, float margin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDbvt_update5(IntPtr obj, IntPtr leaf, IntPtr volume, [In] ref Vector3 velocity);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDbvt_update6(IntPtr obj, IntPtr leaf, IntPtr volume, [In] ref Vector3 velocity, float margin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_write(IntPtr obj, IntPtr iwriter);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvtAabbMm_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtAabbMm_Center(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvtAabbMm_Classify(IntPtr obj, [In] ref Vector3 n, float o, int s);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDbvtAabbMm_Contain(IntPtr obj, IntPtr a);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtAabbMm_Expand(IntPtr obj, [In] ref Vector3 e);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtAabbMm_Extents(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtAabbMm_FromCE([In] ref Vector3 c, [In] ref Vector3 e);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtAabbMm_FromCR([In] ref Vector3 c, float r);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtAabbMm_FromMM([In] ref Vector3 mi, [In] ref Vector3 mx);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtAabbMm_FromPoints([In] ref Vector3 ppts, int n);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtAabbMm_FromPoints2([In] ref Vector3 pts, int n);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtAabbMm_Lengths(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtAabbMm_Maxs(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtAabbMm_Mins(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btDbvtAabbMm_ProjectMinimum(IntPtr obj, [In] ref Vector3 v, uint signs);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtAabbMm_SignedExpand(IntPtr obj, [In] ref Vector3 e);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtAabbMm_tMaxs(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtAabbMm_tMins(IntPtr obj, out Vector3 value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvt_IClone_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_IClone_CloneLeaf(IntPtr obj, IntPtr __unnamed0);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_IClone_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvt_ICollide_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDbvt_ICollide_AllLeaves(IntPtr obj, IntPtr __unnamed0);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDbvt_ICollide_Descent(IntPtr obj, IntPtr __unnamed0);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_ICollide_Process(IntPtr obj, IntPtr __unnamed0, IntPtr __unnamed1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_ICollide_Process2(IntPtr obj, IntPtr __unnamed0);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_ICollide_Process3(IntPtr obj, IntPtr n, float __unnamed1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_ICollide_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_IWriter_Prepare(IntPtr obj, IntPtr root, int numnodes);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_IWriter_WriteLeaf(IntPtr obj, IntPtr __unnamed0, int index, int parent);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_IWriter_WriteNode(IntPtr obj, IntPtr __unnamed0, int index, int parent, int child0, int child1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_IWriter_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvt_sStkCLN_new(IntPtr n, IntPtr p);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvt_sStkCLN_getNode(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvt_sStkCLN_getParent(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_sStkCLN_setNode(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_sStkCLN_setParent(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_sStkCLN_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvt_sStkNN_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvt_sStkNN_new2(IntPtr na, IntPtr nb);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvt_sStkNN_getA(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvt_sStkNN_getB(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_sStkNN_setA(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_sStkNN_setB(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_sStkNN_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvt_sStkNP_new(IntPtr n, uint m);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvt_sStkNP_getMask(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvt_sStkNP_getNode(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_sStkNP_setMask(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_sStkNP_setNode(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_sStkNP_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvt_sStkNPS_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvt_sStkNPS_new2(IntPtr n, uint m, float v);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvt_sStkNPS_getMask(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvt_sStkNPS_getNode(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btDbvt_sStkNPS_getValue(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_sStkNPS_setMask(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_sStkNPS_setNode(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_sStkNPS_setValue(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_sStkNPS_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDefaultCollisionConfiguration_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDefaultCollisionConfiguration_new2(IntPtr constructionInfo);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDefaultCollisionConfiguration_setConvexConvexMultipointIterations(IntPtr obj, int numPerturbationIterations, int minimumPointsPerturbationThreshold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDefaultCollisionConfiguration_setPlaneConvexMultipointIterations(IntPtr obj, int numPerturbationIterations, int minimumPointsPerturbationThreshold);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDefaultCollisionConstructionInfo_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDefaultCollisionConstructionInfo_getCollisionAlgorithmPool(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDefaultCollisionConstructionInfo_getCustomCollisionAlgorithmMaxElementSize(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDefaultCollisionConstructionInfo_getDefaultMaxCollisionAlgorithmPoolSize(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDefaultCollisionConstructionInfo_getDefaultMaxPersistentManifoldPoolSize(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDefaultCollisionConstructionInfo_getPersistentManifoldPool(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDefaultCollisionConstructionInfo_getUseEpaPenetrationAlgorithm(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDefaultCollisionConstructionInfo_setCollisionAlgorithmPool(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDefaultCollisionConstructionInfo_setCustomCollisionAlgorithmMaxElementSize(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDefaultCollisionConstructionInfo_setDefaultMaxCollisionAlgorithmPoolSize(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDefaultCollisionConstructionInfo_setDefaultMaxPersistentManifoldPoolSize(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDefaultCollisionConstructionInfo_setPersistentManifoldPool(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDefaultCollisionConstructionInfo_setUseEpaPenetrationAlgorithm(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDefaultCollisionConstructionInfo_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDefaultMotionState_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDefaultMotionState_new2([In] ref Matrix startTrans);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDefaultMotionState_new3([In] ref Matrix startTrans, [In] ref Matrix centerOfMassOffset);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDefaultMotionState_getCenterOfMassOffset(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDefaultMotionState_getGraphicsWorldTrans(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDefaultMotionState_getStartWorldTrans(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDefaultMotionState_getUserPointer(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDefaultMotionState_setCenterOfMassOffset(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDefaultMotionState_setGraphicsWorldTrans(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDefaultMotionState_setStartWorldTrans(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDefaultMotionState_setUserPointer(IntPtr obj, IntPtr value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDefaultSoftBodySolver_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDefaultSoftBodySolver_copySoftBodyToVertexBuffer(IntPtr obj, IntPtr softBody, IntPtr vertexBuffer);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDiscreteCollisionDetectorInterface_ClosestPointInput_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btDiscreteCollisionDetectorInterface_ClosestPointInput_getMaximumDistanceSquared(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteCollisionDetectorInterface_ClosestPointInput_getTransformA(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteCollisionDetectorInterface_ClosestPointInput_getTransformB(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteCollisionDetectorInterface_ClosestPointInput_setMaximumDistanceSquared(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteCollisionDetectorInterface_ClosestPointInput_setTransformA(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteCollisionDetectorInterface_ClosestPointInput_setTransformB(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteCollisionDetectorInterface_ClosestPointInput_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteCollisionDetectorInterface_Result_addContactPoint(IntPtr obj, [In] ref Vector3 normalOnBInWorld, [In] ref Vector3 pointInWorld, float depth);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteCollisionDetectorInterface_Result_setShapeIdentifiersA(IntPtr obj, int partId0, int index0);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteCollisionDetectorInterface_Result_setShapeIdentifiersB(IntPtr obj, int partId1, int index1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteCollisionDetectorInterface_Result_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteCollisionDetectorInterface_getClosestPoints(IntPtr obj, IntPtr input, IntPtr output, IntPtr debugDraw, bool swapResults);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteCollisionDetectorInterface_delete(IntPtr obj);


		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDiscreteDynamicsWorld_new(IntPtr dispatcher, IntPtr pairCache, IntPtr constraintSolver, IntPtr collisionConfiguration);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteDynamicsWorld_applyGravity(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteDynamicsWorld_debugDrawConstraint(IntPtr obj, IntPtr constraint);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDiscreteDynamicsWorld_getApplySpeculativeContactRestitution(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDiscreteDynamicsWorld_getLatencyMotionStateInterpolation(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDiscreteDynamicsWorld_getSimulationIslandManager(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDiscreteDynamicsWorld_getSynchronizeAllMotionStates(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteDynamicsWorld_setApplySpeculativeContactRestitution(IntPtr obj, bool enable);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteDynamicsWorld_setLatencyMotionStateInterpolation(IntPtr obj, bool latencyInterpolation);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteDynamicsWorld_setNumTasks(IntPtr obj, int numTasks);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteDynamicsWorld_setSynchronizeAllMotionStates(IntPtr obj, bool synchronizeAll);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteDynamicsWorld_synchronizeSingleMotionState(IntPtr obj, IntPtr body);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteDynamicsWorld_updateVehicles(IntPtr obj, float timeStep);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDiscreteDynamicsWorldMt_new(IntPtr dispatcher, IntPtr pairCache, IntPtr constraintSolver, IntPtr collisionConfiguration);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDispatcher_allocateCollisionAlgorithm(IntPtr obj, int size);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDispatcher_clearManifold(IntPtr obj, IntPtr manifold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDispatcher_dispatchAllCollisionPairs(IntPtr obj, IntPtr pairCache, IntPtr dispatchInfo, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDispatcher_findAlgorithm(IntPtr obj, IntPtr body0Wrap, IntPtr body1Wrap, IntPtr sharedManifold, DispatcherQueryType queryType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDispatcher_freeCollisionAlgorithm(IntPtr obj, IntPtr ptr);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDispatcher_getInternalManifoldPointer(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDispatcher_getInternalManifoldPool(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDispatcher_getManifoldByIndexInternal(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDispatcher_getNewManifold(IntPtr obj, IntPtr b0, IntPtr b1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDispatcher_getNumManifolds(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDispatcher_needsCollision(IntPtr obj, IntPtr body0, IntPtr body1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDispatcher_needsResponse(IntPtr obj, IntPtr body0, IntPtr body1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDispatcher_releaseManifold(IntPtr obj, IntPtr manifold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDispatcher_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btDispatcherInfo_getAllowedCcdPenetration(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btDispatcherInfo_getConvexConservativeDistanceThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDispatcherInfo_getDebugDraw(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern DispatchFunc btDispatcherInfo_getDispatchFunc(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDispatcherInfo_getEnableSatConvex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDispatcherInfo_getEnableSPU(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDispatcherInfo_getStepCount(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btDispatcherInfo_getTimeOfImpact(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btDispatcherInfo_getTimeStep(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDispatcherInfo_getUseContinuous(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDispatcherInfo_getUseConvexConservativeDistanceUtil(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDispatcherInfo_getUseEpa(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDispatcherInfo_setAllowedCcdPenetration(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDispatcherInfo_setConvexConservativeDistanceThreshold(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDispatcherInfo_setDebugDraw(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDispatcherInfo_setDispatchFunc(IntPtr obj, DispatchFunc value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDispatcherInfo_setEnableSatConvex(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDispatcherInfo_setEnableSPU(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDispatcherInfo_setStepCount(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDispatcherInfo_setTimeOfImpact(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDispatcherInfo_setTimeStep(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDispatcherInfo_setUseContinuous(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDispatcherInfo_setUseConvexConservativeDistanceUtil(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDispatcherInfo_setUseEpa(IntPtr obj, bool value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDynamicsWorld_addAction(IntPtr obj, IntPtr action);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDynamicsWorld_addConstraint(IntPtr obj, IntPtr constraint, bool disableCollisionsBetweenLinkedBodies);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDynamicsWorld_addRigidBody(IntPtr obj, IntPtr body);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDynamicsWorld_addRigidBody2(IntPtr obj, IntPtr body, int group, int mask);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDynamicsWorld_clearForces(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDynamicsWorld_getConstraint(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDynamicsWorld_getConstraintSolver(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDynamicsWorld_getGravity(IntPtr obj, out Vector3 gravity);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDynamicsWorld_getNumConstraints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDynamicsWorld_getSolverInfo(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern DynamicsWorldType btDynamicsWorld_getWorldType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDynamicsWorld_removeAction(IntPtr obj, IntPtr action);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDynamicsWorld_removeConstraint(IntPtr obj, IntPtr constraint);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDynamicsWorld_removeRigidBody(IntPtr obj, IntPtr body);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDynamicsWorld_setConstraintSolver(IntPtr obj, IntPtr solver);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDynamicsWorld_setGravity(IntPtr obj, [In] ref Vector3 gravity);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDynamicsWorld_setInternalTickCallback(IntPtr obj, IntPtr cb, IntPtr worldUserInfo, bool isPreTick);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDynamicsWorld_stepSimulation(IntPtr obj, float timeStep, int maxSubSteps, float fixedTimeStep);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDynamicsWorld_synchronizeMotionStates(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btElement_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btElement_getId(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btElement_getSz(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btElement_setId(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btElement_setSz(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btElement_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btEmptyAlgorithm_CreateFunc_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btEmptyAlgorithm_new(IntPtr ci);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btEmptyShape_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btFace_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btFace_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btFixedConstraint_new(IntPtr rbA, IntPtr rbB, [In] ref Matrix frameInA, [In] ref Matrix frameInB);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGearConstraint_new(IntPtr rbA, IntPtr rbB, [In] ref Vector3 axisInA, [In] ref Vector3 axisInB, float ratio);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGearConstraint_getAxisA(IntPtr obj, out Vector3 axisA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGearConstraint_getAxisB(IntPtr obj, out Vector3 axisB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btGearConstraint_getRatio(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGearConstraint_setAxisA(IntPtr obj, ref Vector3 axisA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGearConstraint_setAxisB(IntPtr obj, ref Vector3 axisB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGearConstraint_setRatio(IntPtr obj, float ratio);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGeneric6DofConstraint_new(IntPtr rbA, IntPtr rbB, [In] ref Matrix frameInA, [In] ref Matrix frameInB, bool useLinearReferenceFrameA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGeneric6DofConstraint_new2(IntPtr rbB, [In] ref Matrix frameInB, bool useLinearReferenceFrameB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofConstraint_calcAnchorPos(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofConstraint_calculateTransforms(IntPtr obj, [In] ref Matrix transA, [In] ref Matrix transB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofConstraint_calculateTransforms2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGeneric6DofConstraint_get_limit_motor_info2(IntPtr obj, IntPtr limot, [In] ref Matrix transA, [In] ref Matrix transB, [In] ref Vector3 linVelA, [In] ref Vector3 linVelB, [In] ref Vector3 angVelA, [In] ref Vector3 angVelB, IntPtr info, int row, [In] ref Vector3 ax1, int rotational, int rotAllowed);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btGeneric6DofConstraint_getAngle(IntPtr obj, int axis_index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofConstraint_getAngularLowerLimit(IntPtr obj, out Vector3 angularLower);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofConstraint_getAngularUpperLimit(IntPtr obj, out Vector3 angularUpper);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofConstraint_getAxis(IntPtr obj, int axis_index, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofConstraint_getCalculatedTransformA(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofConstraint_getCalculatedTransformB(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern SixDofFlags btGeneric6DofConstraint_getFlags(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofConstraint_getFrameOffsetA(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofConstraint_getFrameOffsetB(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofConstraint_getInfo1NonVirtual(IntPtr obj, IntPtr info);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofConstraint_getInfo2NonVirtual(IntPtr obj, IntPtr info, [In] ref Matrix transA, [In] ref Matrix transB, [In] ref Vector3 linVelA, [In] ref Vector3 linVelB, [In] ref Vector3 angVelA, [In] ref Vector3 angVelB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofConstraint_getLinearLowerLimit(IntPtr obj, out Vector3 linearLower);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofConstraint_getLinearUpperLimit(IntPtr obj, out Vector3 linearUpper);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btGeneric6DofConstraint_getRelativePivotPosition(IntPtr obj, int axis_index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGeneric6DofConstraint_getRotationalLimitMotor(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGeneric6DofConstraint_getTranslationalLimitMotor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGeneric6DofConstraint_getUseFrameOffset(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGeneric6DofConstraint_getUseLinearReferenceFrameA(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGeneric6DofConstraint_getUseSolveConstraintObsolete(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGeneric6DofConstraint_isLimited(IntPtr obj, int limitIndex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofConstraint_setAngularLowerLimit(IntPtr obj, [In] ref Vector3 angularLower);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofConstraint_setAngularUpperLimit(IntPtr obj, [In] ref Vector3 angularUpper);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofConstraint_setAxis(IntPtr obj, [In] ref Vector3 axis1, [In] ref Vector3 axis2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofConstraint_setFrames(IntPtr obj, [In] ref Matrix frameA, [In] ref Matrix frameB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofConstraint_setLimit(IntPtr obj, int axis, float lo, float hi);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofConstraint_setLinearLowerLimit(IntPtr obj, [In] ref Vector3 linearLower);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofConstraint_setLinearUpperLimit(IntPtr obj, [In] ref Vector3 linearUpper);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofConstraint_setUseFrameOffset(IntPtr obj, bool frameOffsetOnOff);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofConstraint_setUseLinearReferenceFrameA(IntPtr obj, bool linearReferenceFrameA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofConstraint_setUseSolveConstraintObsolete(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGeneric6DofConstraint_testAngularLimitMotor(IntPtr obj, int axis_index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofConstraint_updateRHS(IntPtr obj, float timeStep);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGeneric6DofSpring2Constraint_new(IntPtr rbA, IntPtr rbB, [In] ref Matrix frameInA, [In] ref Matrix frameInB, RotateOrder rotOrder);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGeneric6DofSpring2Constraint_new2(IntPtr rbB, [In] ref Matrix frameInB, RotateOrder rotOrder);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btGeneric6DofSpring2Constraint_btGetMatrixElem([In] ref Matrix mat, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpring2Constraint_calculateTransforms(IntPtr obj, [In] ref Matrix transA, [In] ref Matrix transB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpring2Constraint_calculateTransforms2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpring2Constraint_enableMotor(IntPtr obj, int index, bool onOff);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpring2Constraint_enableSpring(IntPtr obj, int index, bool onOff);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btGeneric6DofSpring2Constraint_getAngle(IntPtr obj, int axis_index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpring2Constraint_getAngularLowerLimit(IntPtr obj, out Vector3 angularLower);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpring2Constraint_getAngularLowerLimitReversed(IntPtr obj, out Vector3 angularLower);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpring2Constraint_getAngularUpperLimit(IntPtr obj, out Vector3 angularUpper);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpring2Constraint_getAngularUpperLimitReversed(IntPtr obj, out Vector3 angularUpper);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpring2Constraint_getAxis(IntPtr obj, int axis_index, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpring2Constraint_getCalculatedTransformA(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpring2Constraint_getCalculatedTransformB(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpring2Constraint_getFrameOffsetA(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpring2Constraint_getFrameOffsetB(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpring2Constraint_getLinearLowerLimit(IntPtr obj, out Vector3 linearLower);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpring2Constraint_getLinearUpperLimit(IntPtr obj, out Vector3 linearUpper);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btGeneric6DofSpring2Constraint_getRelativePivotPosition(IntPtr obj, int axis_index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGeneric6DofSpring2Constraint_getRotationalLimitMotor(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern RotateOrder btGeneric6DofSpring2Constraint_getRotationOrder(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGeneric6DofSpring2Constraint_getTranslationalLimitMotor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGeneric6DofSpring2Constraint_isLimited(IntPtr obj, int limitIndex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGeneric6DofSpring2Constraint_matrixToEulerZXY([In] ref Matrix mat, ref Vector3 xyz);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGeneric6DofSpring2Constraint_matrixToEulerZYX([In] ref Matrix mat, ref Vector3 xyz);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGeneric6DofSpring2Constraint_matrixToEulerXZY([In] ref Matrix mat, ref Vector3 xyz);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGeneric6DofSpring2Constraint_matrixToEulerXYZ([In] ref Matrix mat, ref Vector3 xyz);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGeneric6DofSpring2Constraint_matrixToEulerYZX([In] ref Matrix mat, ref Vector3 xyz);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGeneric6DofSpring2Constraint_matrixToEulerYXZ([In] ref Matrix mat, ref Vector3 xyz);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpring2Constraint_setAngularLowerLimit(IntPtr obj, [In] ref Vector3 angularLower);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpring2Constraint_setAngularLowerLimitReversed(IntPtr obj, [In] ref Vector3 angularLower);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpring2Constraint_setAngularUpperLimit(IntPtr obj, [In] ref Vector3 angularUpper);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpring2Constraint_setAngularUpperLimitReversed(IntPtr obj, [In] ref Vector3 angularUpper);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpring2Constraint_setAxis(IntPtr obj, [In] ref Vector3 axis1, [In] ref Vector3 axis2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpring2Constraint_setBounce(IntPtr obj, int index, float bounce);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpring2Constraint_setDamping(IntPtr obj, int index, float damping, bool limitIfNeeded);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpring2Constraint_setEquilibriumPoint(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpring2Constraint_setEquilibriumPoint2(IntPtr obj, int index, float val);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpring2Constraint_setEquilibriumPoint3(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpring2Constraint_setFrames(IntPtr obj, [In] ref Matrix frameA, [In] ref Matrix frameB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpring2Constraint_setLimit(IntPtr obj, int axis, float lo, float hi);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpring2Constraint_setLimitReversed(IntPtr obj, int axis, float lo, float hi);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpring2Constraint_setLinearLowerLimit(IntPtr obj, [In] ref Vector3 linearLower);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpring2Constraint_setLinearUpperLimit(IntPtr obj, [In] ref Vector3 linearUpper);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpring2Constraint_setMaxMotorForce(IntPtr obj, int index, float force);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpring2Constraint_setRotationOrder(IntPtr obj, RotateOrder order);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpring2Constraint_setServo(IntPtr obj, int index, bool onOff);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpring2Constraint_setServoTarget(IntPtr obj, int index, float target);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpring2Constraint_setStiffness(IntPtr obj, int index, float stiffness, bool limitIfNeeded);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpring2Constraint_setTargetVelocity(IntPtr obj, int index, float velocity);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGeneric6DofSpringConstraint_new(IntPtr rbA, IntPtr rbB, [In] ref Matrix frameInA, [In] ref Matrix frameInB, bool useLinearReferenceFrameA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGeneric6DofSpringConstraint_new2(IntPtr rbB, [In] ref Matrix frameInB, bool useLinearReferenceFrameB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpringConstraint_enableSpring(IntPtr obj, int index, bool onOff);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btGeneric6DofSpringConstraint_getDamping(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btGeneric6DofSpringConstraint_getEquilibriumPoint(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btGeneric6DofSpringConstraint_getStiffness(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGeneric6DofSpringConstraint_isSpringEnabled(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpringConstraint_setDamping(IntPtr obj, int index, float damping);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpringConstraint_setEquilibriumPoint(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpringConstraint_setEquilibriumPoint2(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpringConstraint_setEquilibriumPoint3(IntPtr obj, int index, float val);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGeneric6DofSpringConstraint_setStiffness(IntPtr obj, int index, float stiffness);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGhostObject_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGhostObject_addOverlappingObjectInternal(IntPtr obj, IntPtr otherProxy, IntPtr thisProxy);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGhostObject_convexSweepTest(IntPtr obj, IntPtr castShape, [In] ref Matrix convexFromWorld, [In] ref Matrix convexToWorld, IntPtr resultCallback, float allowedCcdPenetration);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGhostObject_getNumOverlappingObjects(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGhostObject_getOverlappingObject(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGhostObject_getOverlappingPairs(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGhostObject_rayTest(IntPtr obj, [In] ref Vector3 rayFromWorld, [In] ref Vector3 rayToWorld, IntPtr resultCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGhostObject_removeOverlappingObjectInternal(IntPtr obj, IntPtr otherProxy, IntPtr dispatcher, IntPtr thisProxy);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGhostObject_upcast(IntPtr colObj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGhostPairCallback_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactBvh_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactBvh_new2(IntPtr primitive_manager);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGImpactBvh_boxQuery(IntPtr obj, IntPtr box, IntPtr collided_results);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGImpactBvh_boxQueryTrans(IntPtr obj, IntPtr box, [In] ref Matrix transform, IntPtr collided_results);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactBvh_buildSet(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactBvh_find_collision(IntPtr boxset1, [In] ref Matrix trans1, IntPtr boxset2, [In] ref Matrix trans2, IntPtr collision_pairs);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactBvh_get_node_pointer(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactBvh_getEscapeNodeIndex(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactBvh_getGlobalBox(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactBvh_getLeftNode(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactBvh_getNodeBound(IntPtr obj, int nodeindex, IntPtr bound);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactBvh_getNodeCount(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactBvh_getNodeData(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactBvh_getNodeTriangle(IntPtr obj, int nodeindex, IntPtr triangle);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactBvh_getPrimitiveManager(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactBvh_getRightNode(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGImpactBvh_hasHierarchy(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGImpactBvh_isLeafNode(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGImpactBvh_isTrimesh(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGImpactBvh_rayQuery(IntPtr obj, [In] ref Vector3 ray_dir, [In] ref Vector3 ray_origin, IntPtr collided_results);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactBvh_setNodeBound(IntPtr obj, int nodeindex, IntPtr bound);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactBvh_setPrimitiveManager(IntPtr obj, IntPtr primitive_manager);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactBvh_update(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactBvh_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactCollisionAlgorithm_CreateFunc_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactCollisionAlgorithm_new(IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactCollisionAlgorithm_getFace0(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactCollisionAlgorithm_getFace1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactCollisionAlgorithm_getPart0(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactCollisionAlgorithm_getPart1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactCollisionAlgorithm_gimpact_vs_compoundshape(IntPtr obj, IntPtr body0Wrap, IntPtr body1Wrap, IntPtr shape0, IntPtr shape1, bool swapped);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactCollisionAlgorithm_gimpact_vs_concave(IntPtr obj, IntPtr body0Wrap, IntPtr body1Wrap, IntPtr shape0, IntPtr shape1, bool swapped);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactCollisionAlgorithm_gimpact_vs_gimpact(IntPtr obj, IntPtr body0Wrap, IntPtr body1Wrap, IntPtr shape0, IntPtr shape1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactCollisionAlgorithm_gimpact_vs_shape(IntPtr obj, IntPtr body0Wrap, IntPtr body1Wrap, IntPtr shape0, IntPtr shape1, bool swapped);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactCollisionAlgorithm_internalGetResultOut(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactCollisionAlgorithm_registerAlgorithm(IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactCollisionAlgorithm_setFace0(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactCollisionAlgorithm_setFace1(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactCollisionAlgorithm_setPart0(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactCollisionAlgorithm_setPart1(IntPtr obj, int value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactCompoundShape_new(bool children_has_transform);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactCompoundShape_addChildShape(IntPtr obj, [In] ref Matrix localTransform, IntPtr shape);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactCompoundShape_addChildShape2(IntPtr obj, IntPtr shape);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactCompoundShape_getCompoundPrimitiveManager(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactMeshShape_new(IntPtr meshInterface);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactMeshShape_getMeshInterface(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactMeshShape_getMeshPart(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactMeshShape_getMeshPartCount(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactMeshShapePart_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactMeshShapePart_new2(IntPtr meshInterface, int part);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactMeshShapePart_getPart(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactMeshShapePart_getTrimeshPrimitiveManager(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_getVertex(IntPtr obj, int vertex_index, out Vector3 vertex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactMeshShapePart_getVertexCount(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactMeshShapePart_TrimeshPrimitiveManager_new(IntPtr meshInterface, int part);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactMeshShapePart_TrimeshPrimitiveManager_new2(IntPtr manager);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactMeshShapePart_TrimeshPrimitiveManager_new3();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_get_bullet_triangle(IntPtr obj, int prim_index, IntPtr triangle);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_get_indices(IntPtr obj, int face_index, out uint i0, out uint i1, out uint i2b);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_get_vertex(IntPtr obj, uint vertex_index, out Vector3 vertex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactMeshShapePart_TrimeshPrimitiveManager_get_vertex_count(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactMeshShapePart_TrimeshPrimitiveManager_getIndexbase(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactMeshShapePart_TrimeshPrimitiveManager_getIndexstride(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern PhyScalarType btGImpactMeshShapePart_TrimeshPrimitiveManager_getIndicestype(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactMeshShapePart_TrimeshPrimitiveManager_getLock_count(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btGImpactMeshShapePart_TrimeshPrimitiveManager_getMargin(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactMeshShapePart_TrimeshPrimitiveManager_getMeshInterface(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactMeshShapePart_TrimeshPrimitiveManager_getNumfaces(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactMeshShapePart_TrimeshPrimitiveManager_getNumverts(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactMeshShapePart_TrimeshPrimitiveManager_getPart(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_getScale(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactMeshShapePart_TrimeshPrimitiveManager_getStride(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern PhyScalarType btGImpactMeshShapePart_TrimeshPrimitiveManager_getType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactMeshShapePart_TrimeshPrimitiveManager_getVertexbase(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_lock(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setIndexbase(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setIndexstride(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setIndicestype(IntPtr obj, PhyScalarType value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setLock_count(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setMargin(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setMeshInterface(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setNumfaces(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setNumverts(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setPart(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setScale(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setStride(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setType(IntPtr obj, PhyScalarType value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setVertexbase(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_unlock(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactQuantizedBvh_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactQuantizedBvh_new2(IntPtr primitive_manager);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGImpactQuantizedBvh_boxQuery(IntPtr obj, IntPtr box, IntPtr collided_results);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGImpactQuantizedBvh_boxQueryTrans(IntPtr obj, IntPtr box, [In] ref Matrix transform, IntPtr collided_results);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactQuantizedBvh_buildSet(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactQuantizedBvh_find_collision(IntPtr boxset1, [In] ref Matrix trans1, IntPtr boxset2, [In] ref Matrix trans2, IntPtr collision_pairs);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactQuantizedBvh_get_node_pointer(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactQuantizedBvh_getEscapeNodeIndex(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactQuantizedBvh_getGlobalBox(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactQuantizedBvh_getLeftNode(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactQuantizedBvh_getNodeBound(IntPtr obj, int nodeindex, IntPtr bound);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactQuantizedBvh_getNodeCount(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactQuantizedBvh_getNodeData(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactQuantizedBvh_getNodeTriangle(IntPtr obj, int nodeindex, IntPtr triangle);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactQuantizedBvh_getPrimitiveManager(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactQuantizedBvh_getRightNode(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGImpactQuantizedBvh_hasHierarchy(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGImpactQuantizedBvh_isLeafNode(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGImpactQuantizedBvh_isTrimesh(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGImpactQuantizedBvh_rayQuery(IntPtr obj, [In] ref Vector3 ray_dir, [In] ref Vector3 ray_origin, IntPtr collided_results);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactQuantizedBvh_setNodeBound(IntPtr obj, int nodeindex, IntPtr bound);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactQuantizedBvh_setPrimitiveManager(IntPtr obj, IntPtr primitive_manager);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactQuantizedBvh_update(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactQuantizedBvh_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGImpactShapeInterface_childrenHasTransform(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactShapeInterface_getBoxSet(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactShapeInterface_getBulletTetrahedron(IntPtr obj, int prim_index, IntPtr tetrahedron);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactShapeInterface_getBulletTriangle(IntPtr obj, int prim_index, IntPtr triangle);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactShapeInterface_getChildAabb(IntPtr obj, int child_index, [In] ref Matrix t, out Vector3 aabbMin, out Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactShapeInterface_getChildTransform(IntPtr obj, int index, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactShapeInterface_getLocalBox(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactShapeInterface_getNumChildShapes(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactShapeInterface_getPrimitiveManager(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactShapeInterface_getPrimitiveTriangle(IntPtr obj, int index, IntPtr triangle);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGImpactShapeInterface_hasBoxSet(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactShapeInterface_lockChildShapes(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGImpactShapeInterface_needsRetrieveTetrahedrons(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGImpactShapeInterface_needsRetrieveTriangles(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactShapeInterface_postUpdate(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactShapeInterface_processAllTrianglesRay(IntPtr obj, IntPtr __unnamed0, [In] ref Vector3 __unnamed1, [In] ref Vector3 __unnamed2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactShapeInterface_rayTest(IntPtr obj, [In] ref Vector3 rayFrom, [In] ref Vector3 rayTo, IntPtr resultCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactShapeInterface_setChildTransform(IntPtr obj, int index, [In] ref Matrix transform);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactShapeInterface_unlockChildShapes(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactShapeInterface_updateBound(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGjkConvexCast_new(IntPtr convexA, IntPtr convexB, IntPtr simplexSolver);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGjkEpaPenetrationDepthSolver_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGjkPairDetector_new(IntPtr objectA, IntPtr objectB, IntPtr simplexSolver, IntPtr penetrationDepthSolver);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGjkPairDetector_new2(IntPtr objectA, IntPtr objectB, int shapeTypeA, int shapeTypeB, float marginA, float marginB, IntPtr simplexSolver, IntPtr penetrationDepthSolver);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGjkPairDetector_getCachedSeparatingAxis(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btGjkPairDetector_getCachedSeparatingDistance(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGjkPairDetector_getCatchDegeneracies(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGjkPairDetector_getClosestPointsNonVirtual(IntPtr obj, IntPtr input, IntPtr output, IntPtr debugDraw);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGjkPairDetector_getCurIter(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGjkPairDetector_getDegenerateSimplex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGjkPairDetector_getFixContactNormalDirection(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGjkPairDetector_getLastUsedMethod(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGjkPairDetector_setCachedSeparatingAxis(IntPtr obj, [In] ref Vector3 seperatingAxis);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGjkPairDetector_setCatchDegeneracies(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGjkPairDetector_setCurIter(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGjkPairDetector_setDegenerateSimplex(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGjkPairDetector_setFixContactNormalDirection(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGjkPairDetector_setIgnoreMargin(IntPtr obj, bool ignoreMargin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGjkPairDetector_setLastUsedMethod(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGjkPairDetector_setMinkowskiA(IntPtr obj, IntPtr minkA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGjkPairDetector_setMinkowskiB(IntPtr obj, IntPtr minkB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGjkPairDetector_setPenetrationDepthSolver(IntPtr obj, IntPtr penetrationDepthSolver);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btHashedOverlappingPairCache_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btHashedOverlappingPairCache_GetCount(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btHashedOverlappingPairCache_needsBroadphaseCollision(IntPtr obj, IntPtr proxy0, IntPtr proxy1);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btHeightfieldTerrainShape_new(int heightStickWidth, int heightStickLength, IntPtr heightfieldData, float heightScale, float minHeight, float maxHeight, int upAxis, PhyScalarType heightDataType, bool flipQuadEdges);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btHeightfieldTerrainShape_new2(int heightStickWidth, int heightStickLength, IntPtr heightfieldData, float maxHeight, int upAxis, bool useFloatData, bool flipQuadEdges);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHeightfieldTerrainShape_setUseDiamondSubdivision(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHeightfieldTerrainShape_setUseDiamondSubdivision2(IntPtr obj, bool useDiamondSubdivision);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHeightfieldTerrainShape_setUseZigzagSubdivision(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHeightfieldTerrainShape_setUseZigzagSubdivision2(IntPtr obj, bool useZigzagSubdivision);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btHinge2Constraint_new(IntPtr rbA, IntPtr rbB, [In] ref Vector3 anchor, [In] ref Vector3 axis1, [In] ref Vector3 axis2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHinge2Constraint_getAnchor(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHinge2Constraint_getAnchor2(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btHinge2Constraint_getAngle1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btHinge2Constraint_getAngle2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHinge2Constraint_getAxis1(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHinge2Constraint_getAxis2(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHinge2Constraint_setLowerLimit(IntPtr obj, float ang1min);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHinge2Constraint_setUpperLimit(IntPtr obj, float ang1max);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btHingeAccumulatedAngleConstraint_new(IntPtr rbA, IntPtr rbB, [In] ref Vector3 pivotInA, [In] ref Vector3 pivotInB, [In] ref Vector3 axisInA, [In] ref Vector3 axisInB, bool useReferenceFrameA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btHingeAccumulatedAngleConstraint_new2(IntPtr rbA, [In] ref Vector3 pivotInA, [In] ref Vector3 axisInA, bool useReferenceFrameA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btHingeAccumulatedAngleConstraint_new3(IntPtr rbA, IntPtr rbB, [In] ref Matrix rbAFrame, [In] ref Matrix rbBFrame, bool useReferenceFrameA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btHingeAccumulatedAngleConstraint_new4(IntPtr rbA, [In] ref Matrix rbAFrame, bool useReferenceFrameA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btHingeAccumulatedAngleConstraint_getAccumulatedHingeAngle(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHingeAccumulatedAngleConstraint_setAccumulatedHingeAngle(IntPtr obj, float accAngle);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btHingeConstraint_new(IntPtr rbA, IntPtr rbB, [In] ref Vector3 pivotInA, [In] ref Vector3 pivotInB, [In] ref Vector3 axisInA, [In] ref Vector3 axisInB, bool useReferenceFrameA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btHingeConstraint_new2(IntPtr rbA, [In] ref Vector3 pivotInA, [In] ref Vector3 axisInA, bool useReferenceFrameA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btHingeConstraint_new3(IntPtr rbA, IntPtr rbB, [In] ref Matrix rbAFrame, [In] ref Matrix rbBFrame, bool useReferenceFrameA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btHingeConstraint_new4(IntPtr rbA, [In] ref Matrix rbAFrame, bool useReferenceFrameA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHingeConstraint_enableAngularMotor(IntPtr obj, bool enableMotor, float targetVelocity, float maxMotorImpulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHingeConstraint_enableMotor(IntPtr obj, bool enableMotor);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHingeConstraint_getAFrame(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btHingeConstraint_getAngularOnly(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHingeConstraint_getBFrame(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btHingeConstraint_getEnableAngularMotor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern HingeFlags btHingeConstraint_getFlags(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHingeConstraint_getFrameOffsetA(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHingeConstraint_getFrameOffsetB(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btHingeConstraint_getHingeAngle(IntPtr obj, [In] ref Matrix transA, [In] ref Matrix transB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btHingeConstraint_getHingeAngle2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHingeConstraint_getInfo1NonVirtual(IntPtr obj, IntPtr info);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHingeConstraint_getInfo2Internal(IntPtr obj, IntPtr info, [In] ref Matrix transA, [In] ref Matrix transB, [In] ref Vector3 angVelA, [In] ref Vector3 angVelB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHingeConstraint_getInfo2InternalUsingFrameOffset(IntPtr obj, IntPtr info, [In] ref Matrix transA, [In] ref Matrix transB, [In] ref Vector3 angVelA, [In] ref Vector3 angVelB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHingeConstraint_getInfo2NonVirtual(IntPtr obj, IntPtr info, [In] ref Matrix transA, [In] ref Matrix transB, [In] ref Vector3 angVelA, [In] ref Vector3 angVelB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btHingeConstraint_getLimitBiasFactor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btHingeConstraint_getLimitRelaxationFactor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btHingeConstraint_getLimitSign(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btHingeConstraint_getLimitSoftness(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btHingeConstraint_getLowerLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btHingeConstraint_getMaxMotorImpulse(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btHingeConstraint_getMotorTargetVelocity(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btHingeConstraint_getSolveLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btHingeConstraint_getUpperLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btHingeConstraint_getUseFrameOffset(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btHingeConstraint_getUseReferenceFrameA(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btHingeConstraint_hasLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHingeConstraint_setAngularOnly(IntPtr obj, bool angularOnly);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHingeConstraint_setAxis(IntPtr obj, [In] ref Vector3 axisInA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHingeConstraint_setFrames(IntPtr obj, [In] ref Matrix frameA, [In] ref Matrix frameB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHingeConstraint_setLimit(IntPtr obj, float low, float high);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHingeConstraint_setLimit2(IntPtr obj, float low, float high, float _softness);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHingeConstraint_setLimit3(IntPtr obj, float low, float high, float _softness, float _biasFactor);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHingeConstraint_setLimit4(IntPtr obj, float low, float high, float _softness, float _biasFactor, float _relaxationFactor);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHingeConstraint_setMaxMotorImpulse(IntPtr obj, float maxMotorImpulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHingeConstraint_setMotorTarget(IntPtr obj, float targetAngle, float dt);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHingeConstraint_setMotorTarget2(IntPtr obj, [In] ref Quaternion qAinB, float dt);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHingeConstraint_setMotorTargetVelocity(IntPtr obj, float motorTargetVelocity);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHingeConstraint_setUseFrameOffset(IntPtr obj, bool frameOffsetOnOff);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHingeConstraint_setUseReferenceFrameA(IntPtr obj, bool useReferenceFrameA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHingeConstraint_testLimit(IntPtr obj, [In] ref Matrix transA, [In] ref Matrix transB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHingeConstraint_updateRHS(IntPtr obj, float timeStep);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btIndexedMesh_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern PhyScalarType btIndexedMesh_getIndexType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btIndexedMesh_getNumTriangles(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btIndexedMesh_getNumVertices(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btIndexedMesh_getTriangleIndexBase(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btIndexedMesh_getTriangleIndexStride(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btIndexedMesh_getVertexBase(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btIndexedMesh_getVertexStride(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern PhyScalarType btIndexedMesh_getVertexType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btIndexedMesh_setIndexType(IntPtr obj, PhyScalarType value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btIndexedMesh_setNumTriangles(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btIndexedMesh_setNumVertices(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btIndexedMesh_setTriangleIndexBase(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btIndexedMesh_setTriangleIndexStride(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btIndexedMesh_setVertexBase(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btIndexedMesh_setVertexStride(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btIndexedMesh_setVertexType(IntPtr obj, PhyScalarType value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btIndexedMesh_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btInternalTriangleIndexCallbackWrapper_new(IntPtr internalProcessTriangleIndexCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btInternalTriangleIndexCallback_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btITaskScheduler_getMaxNumThreads(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btITaskScheduler_getName(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btITaskScheduler_getNumThreads(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btITaskScheduler_setNumThreads(IntPtr obj, int numThreads);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btJointFeedback_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btJointFeedback_getAppliedForceBodyA(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btJointFeedback_getAppliedForceBodyB(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btJointFeedback_getAppliedTorqueBodyA(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btJointFeedback_getAppliedTorqueBodyB(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btJointFeedback_setAppliedForceBodyA(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btJointFeedback_setAppliedForceBodyB(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btJointFeedback_setAppliedTorqueBodyA(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btJointFeedback_setAppliedTorqueBodyB(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btJointFeedback_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btManifoldPoint_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btManifoldPoint_new2([In] ref Vector3 pointA, [In] ref Vector3 pointB, [In] ref Vector3 normal, float distance);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldPoint_getAppliedImpulse(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldPoint_getAppliedImpulseLateral1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldPoint_getAppliedImpulseLateral2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldPoint_getCombinedContactDamping1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldPoint_getCombinedContactStiffness1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldPoint_getCombinedFriction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldPoint_getCombinedRestitution(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldPoint_getCombinedRollingFriction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldPoint_getContactCFM(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldPoint_getContactERP(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldPoint_getContactMotion1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldPoint_getContactMotion2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern ContactPointFlags btManifoldPoint_getContactPointFlags(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldPoint_getDistance(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldPoint_getDistance1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldPoint_getFrictionCFM(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btManifoldPoint_getIndex0(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btManifoldPoint_getIndex1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_getLateralFrictionDir1(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_getLateralFrictionDir2(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btManifoldPoint_getLifeTime(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_getLocalPointA(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_getLocalPointB(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_getNormalWorldOnB(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btManifoldPoint_getPartId0(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btManifoldPoint_getPartId1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_getPositionWorldOnA(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_getPositionWorldOnB(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btManifoldPoint_getUserPersistentData(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setAppliedImpulse(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setAppliedImpulseLateral1(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setAppliedImpulseLateral2(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setCombinedContactDamping1(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setCombinedContactStiffness1(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setCombinedFriction(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setCombinedRestitution(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setCombinedRollingFriction(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setContactCFM(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setContactERP(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setContactMotion1(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setContactMotion2(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setContactPointFlags(IntPtr obj, ContactPointFlags value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setDistance(IntPtr obj, float dist);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setDistance1(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setFrictionCFM(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setIndex0(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setIndex1(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setLateralFrictionDir1(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setLateralFrictionDir2(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setLifeTime(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setLocalPointA(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setLocalPointB(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setNormalWorldOnB(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setPartId0(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setPartId1(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setPositionWorldOnA(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setPositionWorldOnB(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setUserPersistentData(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btManifoldResult_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btManifoldResult_new2(IntPtr body0Wrap, IntPtr body1Wrap);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldResult_calculateCombinedContactDamping(IntPtr body0, IntPtr body1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldResult_calculateCombinedContactStiffness(IntPtr body0, IntPtr body1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldResult_calculateCombinedFriction(IntPtr body0, IntPtr body1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldResult_calculateCombinedRestitution(IntPtr body0, IntPtr body1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldResult_calculateCombinedRollingFriction(IntPtr body0, IntPtr body1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btManifoldResult_getBody0Internal(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btManifoldResult_getBody0Wrap(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btManifoldResult_getBody1Internal(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btManifoldResult_getBody1Wrap(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldResult_getClosestPointDistanceThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btManifoldResult_getPersistentManifold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldResult_refreshContactPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldResult_setBody0Wrap(IntPtr obj, IntPtr obj0Wrap);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldResult_setBody1Wrap(IntPtr obj, IntPtr obj1Wrap);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldResult_setClosestPointDistanceThreshold(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldResult_setPersistentManifold(IntPtr obj, IntPtr manifoldPtr);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMaterialProperties_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMaterialProperties_getMaterialBase(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMaterialProperties_getMaterialStride(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern PhyScalarType btMaterialProperties_getMaterialType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMaterialProperties_getNumMaterials(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMaterialProperties_getNumTriangles(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMaterialProperties_getTriangleMaterialsBase(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMaterialProperties_getTriangleMaterialStride(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern PhyScalarType btMaterialProperties_getTriangleType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMaterialProperties_setMaterialBase(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMaterialProperties_setMaterialStride(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMaterialProperties_setMaterialType(IntPtr obj, PhyScalarType value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMaterialProperties_setNumMaterials(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMaterialProperties_setNumTriangles(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMaterialProperties_setTriangleMaterialsBase(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMaterialProperties_setTriangleMaterialStride(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMaterialProperties_setTriangleType(IntPtr obj, PhyScalarType value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMaterialProperties_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMinkowskiPenetrationDepthSolver_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMinkowskiSumShape_new(IntPtr shapeA, IntPtr shapeB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMinkowskiSumShape_getTransformA(IntPtr obj, out Matrix transA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMinkowskiSumShape_GetTransformB(IntPtr obj, out Matrix transB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMinkowskiSumShape_setTransformA(IntPtr obj, [In] ref Matrix transA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMinkowskiSumShape_setTransformB(IntPtr obj, [In] ref Matrix transB);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMLCPSolverInterface_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMLCPSolver_new(IntPtr solver);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMLCPSolver_getNumFallbacks(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMLCPSolver_setMLCPSolver(IntPtr obj, IntPtr solver);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMLCPSolver_setNumFallbacks(IntPtr obj, int num);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMotionState_getWorldTransform(IntPtr obj, [Out] out Matrix worldTrans);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMotionState_setWorldTransform(IntPtr obj, [In] ref Matrix worldTrans);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMotionState_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMotionStateWrapper_new(IntPtr getWorldTransformCallback, IntPtr setWorldTransformCallback);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiBodyConstraintSolver_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btMultiBodyConstraintSolver_solveGroupCacheFriendlyFinish(IntPtr obj, IntPtr bodies, int numBodies, IntPtr infoGlobal);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyConstraintSolver_solveMultiBodyGroup(IntPtr obj, IntPtr bodies, int numBodies, IntPtr manifold, int numManifolds, IntPtr constraints, int numConstraints, IntPtr multiBodyConstraints, int numMultiBodyConstraints, IntPtr info, IntPtr debugDrawer, IntPtr dispatcher);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyConstraint_allocateJacobiansMultiDof(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyConstraint_createConstraintRows(IntPtr obj, IntPtr constraintRows, IntPtr data, IntPtr infoGlobal);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyConstraint_debugDraw(IntPtr obj, IntPtr drawer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyConstraint_finalizeMultiDof(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btMultiBodyConstraint_getAppliedImpulse(IntPtr obj, int dof);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMultiBodyConstraint_getIslandIdA(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMultiBodyConstraint_getIslandIdB(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btMultiBodyConstraint_getMaxAppliedImpulse(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMultiBodyConstraint_getNumRows(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btMultiBodyConstraint_getPosition(IntPtr obj, int row);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyConstraint_internalSetAppliedImpulse(IntPtr obj, int dof, float appliedImpulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btMultiBodyConstraint_isUnilateral(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiBodyConstraint_jacobianA(IntPtr obj, int row);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiBodyConstraint_jacobianB(IntPtr obj, int row);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyConstraint_setMaxAppliedImpulse(IntPtr obj, float maxImp);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyConstraint_setPosition(IntPtr obj, int row, float pos);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyConstraint_updateJacobianSizes(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyConstraint_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiBodyDynamicsWorld_new(IntPtr dispatcher, IntPtr pairCache, IntPtr constraintSolver, IntPtr collisionConfiguration);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyDynamicsWorld_addMultiBody(IntPtr obj, IntPtr body, int group, int mask);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyDynamicsWorld_addMultiBodyConstraint(IntPtr obj, IntPtr constraint);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyDynamicsWorld_clearMultiBodyConstraintForces(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyDynamicsWorld_clearMultiBodyForces(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyDynamicsWorld_debugDrawMultiBodyConstraint(IntPtr obj, IntPtr constraint);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyDynamicsWorld_forwardKinematics(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMultiBodyDynamicsWorld_getNumMultiBodyConstraints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyDynamicsWorld_integrateTransforms(IntPtr obj, float timeStep);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyDynamicsWorld_removeMultiBody(IntPtr obj, IntPtr body);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyDynamicsWorld_removeMultiBodyConstraint(IntPtr obj, IntPtr constraint);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiBodyFixedConstraint_new(IntPtr body, int link, IntPtr bodyB, [In] ref Vector3 pivotInA, [In] ref Vector3 pivotInB, [In] ref Matrix frameInA, [In] ref Matrix frameInB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiBodyFixedConstraint_new2(IntPtr bodyA, int linkA, IntPtr bodyB, int linkB, [In] ref Vector3 pivotInA, [In] ref Vector3 pivotInB, [In] ref Matrix frameInA, [In] ref Matrix frameInB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyFixedConstraint_getFrameInA(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyFixedConstraint_getFrameInB(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyFixedConstraint_getPivotInA(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyFixedConstraint_getPivotInB(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyFixedConstraint_setFrameInA(IntPtr obj, [In] ref Matrix frameInA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyFixedConstraint_setFrameInB(IntPtr obj, [In] ref Matrix frameInB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyFixedConstraint_setPivotInA(IntPtr obj, [In] ref Vector3 pivotInA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyFixedConstraint_setPivotInB(IntPtr obj, [In] ref Vector3 pivotInB);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiBodyJointLimitConstraint_new(IntPtr body, int link, float lower, float upper);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiBodyJointMotor_new(IntPtr body, int link, float desiredVelocity, float maxMotorImpulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiBodyJointMotor_new2(IntPtr body, int link, int linkDoF, float desiredVelocity, float maxMotorImpulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyJointMotor_setVelocityTarget(IntPtr obj, float velTarget);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiBodyLinkCollider_new(IntPtr multiBody, int link);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMultiBodyLinkCollider_getLink(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiBodyLinkCollider_getMultiBody(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyLinkCollider_setLink(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyLinkCollider_setMultiBody(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiBodyLinkCollider_upcast(IntPtr colObj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_getAbsFrameLocVelocity(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_getAbsFrameTotVelocity(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_getAppliedConstraintForce(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_getAppliedConstraintTorque(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_getAppliedForce(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_getAppliedTorque(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultibodyLink_getAxes(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_getAxisBottom(IntPtr obj, int dof, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_getAxisTop(IntPtr obj, int dof, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_getCachedRotParentToThis(IntPtr obj, out Quaternion value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_getCachedRVector(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_getCachedWorldTransform(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMultibodyLink_getCfgOffset(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultibodyLink_getCollider(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMultibodyLink_getDofCount(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMultibodyLink_getDofOffset(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_getDVector(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_getEVector(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMultibodyLink_getFlags(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_getInertiaLocal(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btMultibodyLink_getJointDamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultibodyLink_getJointFeedback(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btMultibodyLink_getJointFriction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultibodyLink_getJointName(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultibodyLink_getJointPos(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultibodyLink_getJointTorque(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern FeatherstoneJointType btMultibodyLink_getJointType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultibodyLink_getLinkName(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btMultibodyLink_getMass(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMultibodyLink_getParent(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMultibodyLink_getPosVarCount(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_getZeroRotParentToThis(IntPtr obj, out Quaternion value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultibodyLink_getUserPtr(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_setAppliedConstraintForce(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_setAppliedConstraintTorque(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_setAppliedForce(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_setAppliedTorque(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_setAxisBottom(IntPtr obj, int dof, float x, float y, float z);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_setAxisBottom2(IntPtr obj, int dof, [In] ref Vector3 axis);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_setAxisTop(IntPtr obj, int dof, float x, float y, float z);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_setAxisTop2(IntPtr obj, int dof, [In] ref Vector3 axis);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_setCachedRotParentToThis(IntPtr obj, [In] ref Quaternion value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_setCachedRVector(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_setCachedWorldTransform(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_setCfgOffset(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_setCollider(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_setDofCount(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_setDofOffset(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_setDVector(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_setEVector(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_setFlags(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_setInertiaLocal(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_setJointDamping(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_setJointFeedback(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_setJointFriction(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_setJointName(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_setJointType(IntPtr obj, FeatherstoneJointType value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_setLinkName(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_setMass(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_setParent(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_setPosVarCount(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_setZeroRotParentToThis(IntPtr obj, [In] ref Quaternion value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_setUserPtr(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultibodyLink_updateCacheMultiDof(IntPtr obj, float[] pq);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiBodyPoint2Point_new(IntPtr body, int link, IntPtr bodyB, [In] ref Vector3 pivotInA, [In] ref Vector3 pivotInB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiBodyPoint2Point_new2(IntPtr bodyA, int linkA, IntPtr bodyB, int linkB, [In] ref Vector3 pivotInA, [In] ref Vector3 pivotInB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyPoint2Point_getPivotInB(IntPtr obj, out Vector3 pivotInB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyPoint2Point_setPivotInB(IntPtr obj, [In] ref Vector3 pivotInB);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiBodySliderConstraint_new(IntPtr body, int link, IntPtr bodyB, [In] ref Vector3 pivotInA, [In] ref Vector3 pivotInB, [In] ref Matrix frameInA, [In] ref Matrix frameInB, [In] ref Vector3 jointAxis);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiBodySliderConstraint_new2(IntPtr bodyA, int linkA, IntPtr bodyB, int linkB, [In] ref Vector3 pivotInA, [In] ref Vector3 pivotInB, [In] ref Matrix frameInA, [In] ref Matrix frameInB, [In] ref Vector3 jointAxis);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySliderConstraint_getFrameInA(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySliderConstraint_getFrameInB(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySliderConstraint_getJointAxis(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySliderConstraint_getPivotInA(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySliderConstraint_getPivotInB(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySliderConstraint_setFrameInA(IntPtr obj, [In] ref Matrix frameInA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySliderConstraint_setFrameInB(IntPtr obj, [In] ref Matrix frameInB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySliderConstraint_setJointAxis(IntPtr obj, [In] ref Vector3 jointAxis);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySliderConstraint_setPivotInA(IntPtr obj, [In] ref Vector3 pivotInA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySliderConstraint_setPivotInB(IntPtr obj, [In] ref Vector3 pivotInB);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiBodySolverConstraint_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySolverConstraint_getAngularComponentA(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySolverConstraint_getAngularComponentB(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btMultiBodySolverConstraint_getAppliedImpulse(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btMultiBodySolverConstraint_getAppliedPushImpulse(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btMultiBodySolverConstraint_getCfm(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySolverConstraint_getContactNormal1(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySolverConstraint_getContactNormal2(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMultiBodySolverConstraint_getDeltaVelAindex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMultiBodySolverConstraint_getDeltaVelBindex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btMultiBodySolverConstraint_getFriction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMultiBodySolverConstraint_getFrictionIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMultiBodySolverConstraint_getJacAindex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMultiBodySolverConstraint_getJacBindex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btMultiBodySolverConstraint_getJacDiagABInv(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMultiBodySolverConstraint_getLinkA(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMultiBodySolverConstraint_getLinkB(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btMultiBodySolverConstraint_getLowerLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiBodySolverConstraint_getMultiBodyA(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiBodySolverConstraint_getMultiBodyB(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiBodySolverConstraint_getOrgConstraint(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMultiBodySolverConstraint_getOrgDofIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiBodySolverConstraint_getOriginalContactPoint(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMultiBodySolverConstraint_getOverrideNumSolverIterations(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySolverConstraint_getRelpos1CrossNormal(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySolverConstraint_getRelpos2CrossNormal(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btMultiBodySolverConstraint_getRhs(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btMultiBodySolverConstraint_getRhsPenetration(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMultiBodySolverConstraint_getSolverBodyIdA(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMultiBodySolverConstraint_getSolverBodyIdB(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btMultiBodySolverConstraint_getUnusedPadding4(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btMultiBodySolverConstraint_getUpperLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySolverConstraint_setAngularComponentA(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySolverConstraint_setAngularComponentB(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySolverConstraint_setAppliedImpulse(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySolverConstraint_setAppliedPushImpulse(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySolverConstraint_setCfm(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySolverConstraint_setContactNormal1(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySolverConstraint_setContactNormal2(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySolverConstraint_setDeltaVelAindex(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySolverConstraint_setDeltaVelBindex(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySolverConstraint_setFriction(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySolverConstraint_setFrictionIndex(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySolverConstraint_setJacAindex(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySolverConstraint_setJacBindex(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySolverConstraint_setJacDiagABInv(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySolverConstraint_setLinkA(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySolverConstraint_setLinkB(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySolverConstraint_setLowerLimit(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySolverConstraint_setMultiBodyA(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySolverConstraint_setMultiBodyB(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySolverConstraint_setOrgConstraint(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySolverConstraint_setOrgDofIndex(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySolverConstraint_setOriginalContactPoint(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySolverConstraint_setOverrideNumSolverIterations(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySolverConstraint_setRelpos1CrossNormal(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySolverConstraint_setRelpos2CrossNormal(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySolverConstraint_setRhs(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySolverConstraint_setRhsPenetration(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySolverConstraint_setSolverBodyIdA(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySolverConstraint_setSolverBodyIdB(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySolverConstraint_setUnusedPadding4(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySolverConstraint_setUpperLimit(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodySolverConstraint_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiBody_new(int n_links, float mass, [In] ref Vector3 inertia, bool fixedBase, bool canSleep);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_addBaseConstraintForce(IntPtr obj, [In] ref Vector3 f);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_addBaseConstraintTorque(IntPtr obj, [In] ref Vector3 t);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_addBaseForce(IntPtr obj, [In] ref Vector3 f);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_addBaseTorque(IntPtr obj, [In] ref Vector3 t);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_addJointTorque(IntPtr obj, int i, float Q);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_addJointTorqueMultiDof(IntPtr obj, int i, float[] Q);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_addJointTorqueMultiDof2(IntPtr obj, int i, int dof, float Q);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_addLinkConstraintForce(IntPtr obj, int i, [In] ref Vector3 f);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_addLinkConstraintTorque(IntPtr obj, int i, [In] ref Vector3 t);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_addLinkForce(IntPtr obj, int i, [In] ref Vector3 f);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_addLinkTorque(IntPtr obj, int i, [In] ref Vector3 t);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_applyDeltaVeeMultiDof(IntPtr obj, float[] delta_vee, float multiplier);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_applyDeltaVeeMultiDof2(IntPtr obj, float[] delta_vee, float multiplier);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_calcAccelerationDeltasMultiDof(IntPtr obj, float[] force, float[] output, IntPtr scratch_r, IntPtr scratch_v);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMultiBody_calculateSerializeBufferSize(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_checkMotionAndSleepIfRequired(IntPtr obj, float timestep);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_clearConstraintForces(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_clearForcesAndTorques(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_clearVelocities(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_computeAccelerationsArticulatedBodyAlgorithmMultiDof(IntPtr obj, float dt, IntPtr scratch_r, IntPtr scratch_v, IntPtr scratch_m, bool isConstraintPass);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_fillConstraintJacobianMultiDof(IntPtr obj, int link, [In] ref Vector3 contact_point, [In] ref Vector3 normal_ang, [In] ref Vector3 normal_lin, float[] jac, IntPtr scratch_r, IntPtr scratch_v, IntPtr scratch_m);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_fillContactJacobianMultiDof(IntPtr obj, int link, [In] ref Vector3 contact_point, [In] ref Vector3 normal, float[] jac, IntPtr scratch_r, IntPtr scratch_v, IntPtr scratch_m);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_finalizeMultiDof(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_forwardKinematics(IntPtr obj, IntPtr scratch_q, IntPtr scratch_m);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btMultiBody_getAngularDamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_getAngularMomentum(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiBody_getBaseCollider(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_getBaseForce(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_getBaseInertia(IntPtr obj, out Vector3 inertia);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btMultiBody_getBaseMass(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiBody_getBaseName(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_getBaseOmega(IntPtr obj, out Vector3 omega);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_getBasePos(IntPtr obj, out Vector3 pos);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_getBaseTorque(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_getBaseVel(IntPtr obj, out Vector3 vel);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_getBaseWorldTransform(IntPtr obj, out Matrix tr);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btMultiBody_getCanSleep(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMultiBody_getCompanionId(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btMultiBody_getJointPos(IntPtr obj, int i);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiBody_getJointPosMultiDof(IntPtr obj, int i);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btMultiBody_getJointTorque(IntPtr obj, int i);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiBody_getJointTorqueMultiDof(IntPtr obj, int i);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btMultiBody_getJointVel(IntPtr obj, int i);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiBody_getJointVelMultiDof(IntPtr obj, int i);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btMultiBody_getKineticEnergy(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btMultiBody_getLinearDamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiBody_getLink(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_getLinkForce(IntPtr obj, int i, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_getLinkInertia(IntPtr obj, int i, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btMultiBody_getLinkMass(IntPtr obj, int i);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_getLinkTorque(IntPtr obj, int i, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btMultiBody_getMaxAppliedImpulse(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btMultiBody_getMaxCoordinateVelocity(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMultiBody_getNumDofs(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMultiBody_getNumLinks(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMultiBody_getNumPosVars(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMultiBody_getParent(IntPtr obj, int link_num);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_getParentToLocalRot(IntPtr obj, int i, out Quaternion value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_getRVector(IntPtr obj, int i, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btMultiBody_getUseGyroTerm(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMultiBody_getUserIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMultiBody_getUserIndex2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiBody_getUserPointer(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiBody_getVelocityVector(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_getWorldToBaseRot(IntPtr obj, out Quaternion rot);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_goToSleep(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btMultiBody_hasFixedBase(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btMultiBody_hasSelfCollision(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btMultiBody_internalNeedsJointFeedback(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btMultiBody_isAwake(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btMultiBody_isPosUpdated(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btMultiBody_isUsingGlobalVelocities(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btMultiBody_isUsingRK4Integration(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_localDirToWorld(IntPtr obj, int i, [In] ref Vector3 vec, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_localFrameToWorld(IntPtr obj, int i, [In] ref Matrix mat, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_localPosToWorld(IntPtr obj, int i, [In] ref Vector3 vec, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_processDeltaVeeMultiDof2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiBody_serialize(IntPtr obj, IntPtr dataBuffer, IntPtr serializer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_setAngularDamping(IntPtr obj, float damp);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_setBaseCollider(IntPtr obj, IntPtr collider);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_setBaseInertia(IntPtr obj, [In] ref Vector3 inertia);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_setBaseMass(IntPtr obj, float mass);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_setBaseName(IntPtr obj, IntPtr name);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_setBaseOmega(IntPtr obj, [In] ref Vector3 omega);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_setBasePos(IntPtr obj, [In] ref Vector3 pos);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_setBaseVel(IntPtr obj, [In] ref Vector3 vel);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_setBaseWorldTransform(IntPtr obj, [In] ref Matrix tr);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_setCanSleep(IntPtr obj, bool canSleep);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_setCompanionId(IntPtr obj, int id);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_setHasSelfCollision(IntPtr obj, bool hasSelfCollision);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_setJointPos(IntPtr obj, int i, float q);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_setJointPosMultiDof(IntPtr obj, int i, float[] q);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_setJointVel(IntPtr obj, int i, float qdot);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_setJointVelMultiDof(IntPtr obj, int i, float[] qdot);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_setLinearDamping(IntPtr obj, float damp);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_setMaxAppliedImpulse(IntPtr obj, float maxImp);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_setMaxCoordinateVelocity(IntPtr obj, float maxVel);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_setNumLinks(IntPtr obj, int numLinks);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_setPosUpdated(IntPtr obj, bool updated);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_setupFixed(IntPtr obj, int linkIndex, float mass, [In] ref Vector3 inertia, int parent, [In] ref Quaternion rotParentToThis, [In] ref Vector3 parentComToThisPivotOffset, [In] ref Vector3 thisPivotToThisComOffset, bool deprecatedDisableParentCollision);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_setupPlanar(IntPtr obj, int i, float mass, [In] ref Vector3 inertia, int parent, [In] ref Quaternion rotParentToThis, [In] ref Vector3 rotationAxis, [In] ref Vector3 parentComToThisComOffset, bool disableParentCollision);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_setupPrismatic(IntPtr obj, int i, float mass, [In] ref Vector3 inertia, int parent, [In] ref Quaternion rotParentToThis, [In] ref Vector3 jointAxis, [In] ref Vector3 parentComToThisPivotOffset, [In] ref Vector3 thisPivotToThisComOffset, bool disableParentCollision);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_setupRevolute(IntPtr obj, int linkIndex, float mass, [In] ref Vector3 inertia, int parentIndex, [In] ref Quaternion rotParentToThis, [In] ref Vector3 jointAxis, [In] ref Vector3 parentComToThisPivotOffset, [In] ref Vector3 thisPivotToThisComOffset, bool disableParentCollision);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_setupSpherical(IntPtr obj, int linkIndex, float mass, [In] ref Vector3 inertia, int parent, [In] ref Quaternion rotParentToThis, [In] ref Vector3 parentComToThisPivotOffset, [In] ref Vector3 thisPivotToThisComOffset, bool disableParentCollision);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_setUseGyroTerm(IntPtr obj, bool useGyro);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_setUserIndex(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_setUserIndex2(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_setUserPointer(IntPtr obj, IntPtr userPointer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_setWorldToBaseRot(IntPtr obj, [In] ref Quaternion rot);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_stepPositionsMultiDof(IntPtr obj, float dt, float[] pq, float[] pqd);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_stepVelocitiesMultiDof(IntPtr obj, float dt, IntPtr scratch_r, IntPtr scratch_v, IntPtr scratch_m, bool isConstraintPass);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_updateCollisionObjectWorldTransforms(IntPtr obj, IntPtr scratch_q, IntPtr scratch_m);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_useGlobalVelocities(IntPtr obj, bool use);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_useRK4Integration(IntPtr obj, bool use);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_wakeUp(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_worldDirToLocal(IntPtr obj, int i, [In] ref Vector3 vec, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_worldPosToLocal(IntPtr obj, int i, [In] ref Vector3 vec, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBody_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultimaterialTriangleMeshShape_new(IntPtr meshInterface, bool useQuantizedAabbCompression, bool buildBvh);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultimaterialTriangleMeshShape_new2(IntPtr meshInterface, bool useQuantizedAabbCompression, [In] ref Vector3 bvhAabbMin, [In] ref Vector3 bvhAabbMax, bool buildBvh);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiSphereShape_new(Vector3[] positions, float[] radi, int numSpheres);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiSphereShape_new2(IntPtr positions, float[] radi, int numSpheres);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMultiSphereShape_getSphereCount(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiSphereShape_getSpherePosition(IntPtr obj, int index, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btMultiSphereShape_getSphereRadius(IntPtr obj, int index);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btNNCGConstraintSolver_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btNNCGConstraintSolver_getOnlyForNoneContact(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btNNCGConstraintSolver_setOnlyForNoneContact(IntPtr obj, bool value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btNodeOverlapCallback_processNode(IntPtr obj, int subPart, int triangleIndex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btNodeOverlapCallback_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btNullPairCache_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btOptimizedBvh_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btOptimizedBvh_build(IntPtr obj, IntPtr triangles, bool useQuantizedAabbCompression, [In] ref Vector3 bvhAabbMin, [In] ref Vector3 bvhAabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btOptimizedBvh_deSerializeInPlace(IntPtr i_alignedDataBuffer, uint i_dataBufferSize, bool i_swapEndian);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btOptimizedBvh_refit(IntPtr obj, IntPtr triangles, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btOptimizedBvh_refitPartial(IntPtr obj, IntPtr triangles, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btOptimizedBvh_serializeInPlace(IntPtr obj, IntPtr o_alignedDataBuffer, uint i_dataBufferSize, bool i_swapEndian);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btOptimizedBvh_updateBvhNodes(IntPtr obj, IntPtr meshInterface, int firstNode, int endNode, int index);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btOptimizedBvhNode_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btOptimizedBvhNode_getAabbMaxOrg(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btOptimizedBvhNode_getAabbMinOrg(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btOptimizedBvhNode_getEscapeIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btOptimizedBvhNode_getSubPart(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btOptimizedBvhNode_getTriangleIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btOptimizedBvhNode_setAabbMaxOrg(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btOptimizedBvhNode_setAabbMinOrg(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btOptimizedBvhNode_setEscapeIndex(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btOptimizedBvhNode_setSubPart(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btOptimizedBvhNode_setTriangleIndex(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btOptimizedBvhNode_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btOverlapCallback_processOverlap(IntPtr obj, IntPtr pair);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btOverlapCallback_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btOverlapFilterCallback_needBroadphaseCollision(IntPtr obj, IntPtr proxy0, IntPtr proxy1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btOverlapFilterCallback_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btOverlapFilterCallbackWrapper_new(IntPtr needBroadphaseCollision);


		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btOverlappingPairCache_cleanOverlappingPair(IntPtr obj, IntPtr pair, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btOverlappingPairCache_cleanProxyFromPairs(IntPtr obj, IntPtr proxy, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btOverlappingPairCache_findPair(IntPtr obj, IntPtr proxy0, IntPtr proxy1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btOverlappingPairCache_getNumOverlappingPairs(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btOverlappingPairCache_getOverlappingPairArray(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btOverlappingPairCache_getOverlappingPairArrayPtr(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btOverlappingPairCache_hasDeferredRemoval(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btOverlappingPairCache_processAllOverlappingPairs(IntPtr obj, IntPtr __unnamed0, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btOverlappingPairCache_setInternalGhostPairCallback(IntPtr obj, IntPtr ghostPairCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btOverlappingPairCache_setOverlapFilterCallback(IntPtr obj, IntPtr callback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btOverlappingPairCache_sortOverlappingPairs(IntPtr obj, IntPtr dispatcher);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btOverlappingPairCallback_addOverlappingPair(IntPtr obj, IntPtr proxy0, IntPtr proxy1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btOverlappingPairCallback_removeOverlappingPair(IntPtr obj, IntPtr proxy0, IntPtr proxy1, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btOverlappingPairCallback_removeOverlappingPairsContainingProxy(IntPtr obj, IntPtr proxy0, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btOverlappingPairCallback_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btPairCachingGhostObject_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btPairCachingGhostObject_getOverlappingPairCache(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btPairSet_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPairSet_push_pair(IntPtr obj, int index1, int index2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPairSet_push_pair_inv(IntPtr obj, int index1, int index2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPairSet_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btPersistentManifold_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btPersistentManifold_new2(IntPtr body0, IntPtr body1, int __unnamed2, float contactBreakingThreshold, float contactProcessingThreshold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btPersistentManifold_addManifoldPoint(IntPtr obj, IntPtr newPoint, bool isPredictive);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPersistentManifold_clearManifold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPersistentManifold_clearUserCache(IntPtr obj, IntPtr pt);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btPersistentManifold_getBody0(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btPersistentManifold_getBody1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btPersistentManifold_getCacheEntry(IntPtr obj, IntPtr newPoint);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btPersistentManifold_getCompanionIdA(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btPersistentManifold_getCompanionIdB(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btPersistentManifold_getContactBreakingThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btPersistentManifold_getContactPoint(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btPersistentManifold_getContactProcessingThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btPersistentManifold_getIndex1a(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btPersistentManifold_getNumContacts(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPersistentManifold_refreshContactPoints(IntPtr obj, [In] ref Matrix trA, [In] ref Matrix trB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPersistentManifold_removeContactPoint(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPersistentManifold_replaceContactPoint(IntPtr obj, IntPtr newPoint, int insertIndex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPersistentManifold_setBodies(IntPtr obj, IntPtr body0, IntPtr body1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPersistentManifold_setCompanionIdA(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPersistentManifold_setCompanionIdB(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPersistentManifold_setContactBreakingThreshold(IntPtr obj, float contactBreakingThreshold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPersistentManifold_setContactProcessingThreshold(IntPtr obj, float contactProcessingThreshold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPersistentManifold_setIndex1a(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPersistentManifold_setNumContacts(IntPtr obj, int cachedPoints);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btPersistentManifold_validContactDistance(IntPtr obj, IntPtr pt);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPersistentManifold_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btPoint2PointConstraint_new(IntPtr rbA, IntPtr rbB, [In] ref Vector3 pivotInA, [In] ref Vector3 pivotInB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btPoint2PointConstraint_new2(IntPtr rbA, [In] ref Vector3 pivotInA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern Point2PointFlags btPoint2PointConstraint_getFlags(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPoint2PointConstraint_getInfo1NonVirtual(IntPtr obj, IntPtr info);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPoint2PointConstraint_getInfo2NonVirtual(IntPtr obj, IntPtr info, [In] ref Matrix body0_trans, [In] ref Matrix body1_trans);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPoint2PointConstraint_getPivotInA(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPoint2PointConstraint_getPivotInB(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btPoint2PointConstraint_getSetting(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btPoint2PointConstraint_getUseSolveConstraintObsolete(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPoint2PointConstraint_setPivotA(IntPtr obj, [In] ref Vector3 pivotA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPoint2PointConstraint_setPivotB(IntPtr obj, [In] ref Vector3 pivotB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPoint2PointConstraint_setUseSolveConstraintObsolete(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPoint2PointConstraint_updateRHS(IntPtr obj, float timeStep);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btPointCollector_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btPointCollector_getDistance(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btPointCollector_getHasResult(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPointCollector_getNormalOnBInWorld(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPointCollector_getPointInWorld(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPointCollector_setDistance(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPointCollector_setHasResult(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPointCollector_setNormalOnBInWorld(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPointCollector_setPointInWorld(IntPtr obj, [In] ref Vector3 value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btPolarDecomposition_new(float tolerance, uint maxIterations);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern uint btPolarDecomposition_decompose(IntPtr obj, [In] ref Matrix a, out Matrix u, out Matrix h);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern uint btPolarDecomposition_maxIterations(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPolarDecomposition_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPolyhedralConvexAabbCachingShape_getNonvirtualAabb(IntPtr obj, [In] ref Matrix trans, out Vector3 aabbMin, out Vector3 aabbMax, float margin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPolyhedralConvexAabbCachingShape_recalcLocalAabb(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btPolyhedralConvexShape_getConvexPolyhedron(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPolyhedralConvexShape_getEdge(IntPtr obj, int i, out Vector3 pa, out Vector3 pb);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btPolyhedralConvexShape_getNumEdges(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btPolyhedralConvexShape_getNumPlanes(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btPolyhedralConvexShape_getNumVertices(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPolyhedralConvexShape_getPlane(IntPtr obj, out Vector3 planeNormal, out Vector3 planeSupport, int i);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPolyhedralConvexShape_getVertex(IntPtr obj, int i, out Vector3 vtx);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btPolyhedralConvexShape_initializePolyhedralFeatures(IntPtr obj, int shiftVerticesByMargin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btPolyhedralConvexShape_isInside(IntPtr obj, [In] ref Vector3 pt, float tolerance);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btPoolAllocator_new(int elemSize, int maxElements);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btPoolAllocator_allocate(IntPtr obj, int size);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPoolAllocator_freeMemory(IntPtr obj, IntPtr ptr);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btPoolAllocator_getElementSize(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btPoolAllocator_getFreeCount(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btPoolAllocator_getMaxCount(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btPoolAllocator_getPoolAddress(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btPoolAllocator_getUsedCount(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btPoolAllocator_validPtr(IntPtr obj, IntPtr ptr);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPoolAllocator_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPrimitiveManagerBase_get_primitive_box(IntPtr obj, int prim_index, IntPtr primbox);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btPrimitiveManagerBase_get_primitive_count(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPrimitiveManagerBase_get_primitive_triangle(IntPtr obj, int prim_index, IntPtr triangle);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btPrimitiveManagerBase_is_trimesh(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPrimitiveManagerBase_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btPrimitiveTriangle_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPrimitiveTriangle_applyTransform(IntPtr obj, [In] ref Matrix t);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPrimitiveTriangle_buildTriPlane(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btPrimitiveTriangle_clip_triangle(IntPtr obj, IntPtr other, [Out] out Vector3 clipped_points);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btPrimitiveTriangle_find_triangle_collision_clip_method(IntPtr obj, IntPtr other, IntPtr contacts);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPrimitiveTriangle_get_edge_plane(IntPtr obj, int edge_index, [Out] out Vector4 plane);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btPrimitiveTriangle_getDummy(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btPrimitiveTriangle_getMargin(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPrimitiveTriangle_getPlane(IntPtr obj, [Out] out Vector4 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btPrimitiveTriangle_getVertices(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btPrimitiveTriangle_overlap_test_conservative(IntPtr obj, IntPtr other);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPrimitiveTriangle_setDummy(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPrimitiveTriangle_setMargin(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPrimitiveTriangle_setPlane(IntPtr obj, [In] ref Vector4 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPrimitiveTriangle_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btQuantizedBvh_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btQuantizedBvh_buildInternal(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern uint btQuantizedBvh_calculateSerializeBufferSize(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btQuantizedBvh_calculateSerializeBufferSizeNew(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btQuantizedBvh_deSerializeDouble(IntPtr obj, IntPtr quantizedBvhDoubleData);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btQuantizedBvh_deSerializeFloat(IntPtr obj, IntPtr quantizedBvhFloatData);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btQuantizedBvh_deSerializeInPlace(IntPtr i_alignedDataBuffer, uint i_dataBufferSize, bool i_swapEndian);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern uint btQuantizedBvh_getAlignmentSerializationPadding();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btQuantizedBvh_getLeafNodeArray(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btQuantizedBvh_getQuantizedNodeArray(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btQuantizedBvh_getSubtreeInfoArray(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btQuantizedBvh_isQuantized(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btQuantizedBvh_reportAabbOverlappingNodex(IntPtr obj, IntPtr nodeCallback, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btQuantizedBvh_reportBoxCastOverlappingNodex(IntPtr obj, IntPtr nodeCallback, [In] ref Vector3 raySource, [In] ref Vector3 rayTarget, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btQuantizedBvh_reportRayOverlappingNodex(IntPtr obj, IntPtr nodeCallback, [In] ref Vector3 raySource, [In] ref Vector3 rayTarget);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btQuantizedBvh_serialize(IntPtr obj, IntPtr o_alignedDataBuffer, uint i_dataBufferSize, bool i_swapEndian);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btQuantizedBvh_serialize2(IntPtr obj, IntPtr dataBuffer, IntPtr serializer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btQuantizedBvh_setQuantizationValues(IntPtr obj, [In] ref Vector3 bvhAabbMin, [In] ref Vector3 bvhAabbMax, float quantizationMargin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btQuantizedBvh_setTraversalMode(IntPtr obj, QuantizedBvh.TraversalMode traversalMode);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btQuantizedBvh_unQuantize(IntPtr obj, IntPtr vecIn, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btQuantizedBvh_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btQuantizedBvhNode_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btQuantizedBvhNode_getEscapeIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btQuantizedBvhNode_getEscapeIndexOrTriangleIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btQuantizedBvhNode_getPartId(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btQuantizedBvhNode_getQuantizedAabbMax(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btQuantizedBvhNode_getQuantizedAabbMin(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btQuantizedBvhNode_getTriangleIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btQuantizedBvhNode_isLeafNode(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btQuantizedBvhNode_setEscapeIndexOrTriangleIndex(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btQuantizedBvhNode_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btQuantizedBvhTree_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btQuantizedBvhTree_build_tree(IntPtr obj, IntPtr primitive_boxes);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btQuantizedBvhTree_clearNodes(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btQuantizedBvhTree_get_node_pointer(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btQuantizedBvhTree_getEscapeNodeIndex(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btQuantizedBvhTree_getLeftNode(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btQuantizedBvhTree_getNodeBound(IntPtr obj, int nodeindex, IntPtr bound);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btQuantizedBvhTree_getNodeCount(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btQuantizedBvhTree_getNodeData(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btQuantizedBvhTree_getRightNode(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btQuantizedBvhTree_isLeafNode(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btQuantizedBvhTree_quantizePoint(IntPtr obj, ushort[] quantizedpoint, [In] ref Vector3 point);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btQuantizedBvhTree_setNodeBound(IntPtr obj, int nodeindex, IntPtr bound);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btQuantizedBvhTree_testQuantizedBoxOverlapp(IntPtr obj, int node_index, ushort[] quantizedMin, ushort[] quantizedMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btQuantizedBvhTree_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btRigidBody_btRigidBodyConstructionInfo_new(float mass, IntPtr motionState, IntPtr collisionShape);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btRigidBody_btRigidBodyConstructionInfo_new2(float mass, IntPtr motionState, IntPtr collisionShape, [In] ref Vector3 localInertia);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRigidBody_btRigidBodyConstructionInfo_getAdditionalAngularDampingFactor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRigidBody_btRigidBodyConstructionInfo_getAdditionalAngularDampingThresholdSqr(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btRigidBody_btRigidBodyConstructionInfo_getAdditionalDamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRigidBody_btRigidBodyConstructionInfo_getAdditionalDampingFactor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRigidBody_btRigidBodyConstructionInfo_getAdditionalLinearDampingThresholdSqr(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRigidBody_btRigidBodyConstructionInfo_getAngularDamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRigidBody_btRigidBodyConstructionInfo_getAngularSleepingThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRigidBody_btRigidBodyConstructionInfo_getFriction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRigidBody_btRigidBodyConstructionInfo_getLinearDamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRigidBody_btRigidBodyConstructionInfo_getLinearSleepingThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_btRigidBodyConstructionInfo_getLocalInertia(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRigidBody_btRigidBodyConstructionInfo_getMass(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRigidBody_btRigidBodyConstructionInfo_getRestitution(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRigidBody_btRigidBodyConstructionInfo_getRollingFriction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_btRigidBodyConstructionInfo_getStartWorldTransform(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_btRigidBodyConstructionInfo_setAdditionalAngularDampingFactor(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_btRigidBodyConstructionInfo_setAdditionalAngularDampingThresholdSqr(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_btRigidBodyConstructionInfo_setAdditionalDamping(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_btRigidBodyConstructionInfo_setAdditionalDampingFactor(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_btRigidBodyConstructionInfo_setAdditionalLinearDampingThresholdSqr(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_btRigidBodyConstructionInfo_setAngularDamping(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_btRigidBodyConstructionInfo_setAngularSleepingThreshold(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_btRigidBodyConstructionInfo_setCollisionShape(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_btRigidBodyConstructionInfo_setFriction(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_btRigidBodyConstructionInfo_setLinearDamping(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_btRigidBodyConstructionInfo_setLinearSleepingThreshold(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_btRigidBodyConstructionInfo_setLocalInertia(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_btRigidBodyConstructionInfo_setMass(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_btRigidBodyConstructionInfo_setMotionState(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_btRigidBodyConstructionInfo_setRestitution(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_btRigidBodyConstructionInfo_setRollingFriction(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_btRigidBodyConstructionInfo_setStartWorldTransform(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_btRigidBodyConstructionInfo_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btRigidBody_new(IntPtr constructionInfo);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_addConstraintRef(IntPtr obj, IntPtr c);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_applyCentralForce(IntPtr obj, [In] ref Vector3 force);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_applyCentralImpulse(IntPtr obj, [In] ref Vector3 impulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_applyDamping(IntPtr obj, float timeStep);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_applyForce(IntPtr obj, [In] ref Vector3 force, [In] ref Vector3 rel_pos);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_applyGravity(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_applyImpulse(IntPtr obj, [In] ref Vector3 impulse, [In] ref Vector3 rel_pos);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_applyTorque(IntPtr obj, [In] ref Vector3 torque);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_applyTorqueImpulse(IntPtr obj, [In] ref Vector3 torque);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_clearForces(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRigidBody_computeAngularImpulseDenominator(IntPtr obj, [In] ref Vector3 axis);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_computeGyroscopicForceExplicit(IntPtr obj, float maxGyroscopicForce, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_computeGyroscopicImpulseImplicit_Body(IntPtr obj, float step, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_computeGyroscopicImpulseImplicit_World(IntPtr obj, float dt, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRigidBody_computeImpulseDenominator(IntPtr obj, [In] ref Vector3 pos, [In] ref Vector3 normal);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_getAabb(IntPtr obj, out Vector3 aabbMin, out Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRigidBody_getAngularDamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_getAngularFactor(IntPtr obj, out Vector3 angFac);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRigidBody_getAngularSleepingThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_getAngularVelocity(IntPtr obj, out Vector3 ang_vel);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btRigidBody_getBroadphaseProxy(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_getCenterOfMassPosition(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_getCenterOfMassTransform(IntPtr obj, out Matrix xform);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btRigidBody_getContactSolverType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern RigidBodyFlags btRigidBody_getFlags(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btRigidBody_getFrictionSolverType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_getGravity(IntPtr obj, out Vector3 acceleration);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_getInvInertiaDiagLocal(IntPtr obj, out Vector3 diagInvInertia);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_getInvInertiaTensorWorld(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRigidBody_getInvMass(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRigidBody_getLinearDamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_getLinearFactor(IntPtr obj, out Vector3 linearFactor);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRigidBody_getLinearSleepingThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_getLinearVelocity(IntPtr obj, out Vector3 lin_vel);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_getLocalInertia(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btRigidBody_getMotionState(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_getOrientation(IntPtr obj, out Quaternion value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_getTotalForce(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_getTotalTorque(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_getVelocityInLocalPoint(IntPtr obj, [In] ref Vector3 rel_pos, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_integrateVelocities(IntPtr obj, float step);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btRigidBody_isInWorld(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_predictIntegratedTransform(IntPtr obj, float step, out Matrix predictedTransform);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_proceedToTransform(IntPtr obj, [In] ref Matrix newTrans);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_removeConstraintRef(IntPtr obj, IntPtr c);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_saveKinematicState(IntPtr obj, float step);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_setAngularFactor(IntPtr obj, [In] ref Vector3 angFac);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_setAngularFactor2(IntPtr obj, float angFac);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_setAngularVelocity(IntPtr obj, [In] ref Vector3 ang_vel);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_setCenterOfMassTransform(IntPtr obj, [In] ref Matrix xform);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_setContactSolverType(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_setDamping(IntPtr obj, float lin_damping, float ang_damping);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_setFlags(IntPtr obj, RigidBodyFlags flags);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_setFrictionSolverType(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_setGravity(IntPtr obj, [In] ref Vector3 acceleration);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_setInvInertiaDiagLocal(IntPtr obj, [In] ref Vector3 diagInvInertia);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_setLinearFactor(IntPtr obj, [In] ref Vector3 linearFactor);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_setLinearVelocity(IntPtr obj, [In] ref Vector3 lin_vel);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_setMassProps(IntPtr obj, float mass, [In] ref Vector3 inertia);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_setMotionState(IntPtr obj, IntPtr motionState);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_setNewBroadphaseProxy(IntPtr obj, IntPtr broadphaseProxy);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_setSleepingThresholds(IntPtr obj, float linear, float angular);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_translate(IntPtr obj, [In] ref Vector3 v);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btRigidBody_upcast(IntPtr colObj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_updateDeactivation(IntPtr obj, float timeStep);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_updateInertiaTensor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btRigidBody_wantsSleeping(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btRotationalLimitMotor_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btRotationalLimitMotor_new2(IntPtr limot);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRotationalLimitMotor_getAccumulatedImpulse(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRotationalLimitMotor_getBounce(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btRotationalLimitMotor_getCurrentLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRotationalLimitMotor_getCurrentLimitError(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRotationalLimitMotor_getCurrentPosition(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRotationalLimitMotor_getDamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btRotationalLimitMotor_getEnableMotor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRotationalLimitMotor_getHiLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRotationalLimitMotor_getLimitSoftness(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRotationalLimitMotor_getLoLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRotationalLimitMotor_getMaxLimitForce(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRotationalLimitMotor_getMaxMotorForce(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRotationalLimitMotor_getNormalCFM(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRotationalLimitMotor_getStopCFM(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRotationalLimitMotor_getStopERP(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRotationalLimitMotor_getTargetVelocity(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btRotationalLimitMotor_isLimited(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btRotationalLimitMotor_needApplyTorques(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor_setAccumulatedImpulse(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor_setBounce(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor_setCurrentLimit(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor_setCurrentLimitError(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor_setCurrentPosition(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor_setDamping(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor_setEnableMotor(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor_setHiLimit(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor_setLimitSoftness(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor_setLoLimit(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor_setMaxLimitForce(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor_setMaxMotorForce(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor_setNormalCFM(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor_setStopCFM(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor_setStopERP(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor_setTargetVelocity(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRotationalLimitMotor_solveAngularLimits(IntPtr obj, float timeStep, [In] ref Vector3 axis, float jacDiagABInv, IntPtr body0, IntPtr body1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btRotationalLimitMotor_testLimitValue(IntPtr obj, float test_value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btRotationalLimitMotor2_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btRotationalLimitMotor2_new2(IntPtr limot);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRotationalLimitMotor2_getBounce(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btRotationalLimitMotor2_getCurrentLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRotationalLimitMotor2_getCurrentLimitError(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRotationalLimitMotor2_getCurrentLimitErrorHi(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRotationalLimitMotor2_getCurrentPosition(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btRotationalLimitMotor2_getEnableMotor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btRotationalLimitMotor2_getEnableSpring(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRotationalLimitMotor2_getEquilibriumPoint(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRotationalLimitMotor2_getHiLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRotationalLimitMotor2_getLoLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRotationalLimitMotor2_getMaxMotorForce(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRotationalLimitMotor2_getMotorCFM(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRotationalLimitMotor2_getMotorERP(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btRotationalLimitMotor2_getServoMotor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRotationalLimitMotor2_getServoTarget(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRotationalLimitMotor2_getSpringDamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btRotationalLimitMotor2_getSpringDampingLimited(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRotationalLimitMotor2_getSpringStiffness(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btRotationalLimitMotor2_getSpringStiffnessLimited(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRotationalLimitMotor2_getStopCFM(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRotationalLimitMotor2_getStopERP(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRotationalLimitMotor2_getTargetVelocity(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btRotationalLimitMotor2_isLimited(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor2_setBounce(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor2_setCurrentLimit(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor2_setCurrentLimitError(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor2_setCurrentLimitErrorHi(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor2_setCurrentPosition(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor2_setEnableMotor(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor2_setEnableSpring(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor2_setEquilibriumPoint(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor2_setHiLimit(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor2_setLoLimit(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor2_setMaxMotorForce(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor2_setMotorCFM(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor2_setMotorERP(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor2_setServoMotor(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor2_setServoTarget(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor2_setSpringDamping(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor2_setSpringDampingLimited(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor2_setSpringStiffness(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor2_setSpringStiffnessLimited(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor2_setStopCFM(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor2_setStopERP(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor2_setTargetVelocity(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor2_testLimitValue(IntPtr obj, float test_value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRotationalLimitMotor2_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btScaledBvhTriangleMeshShape_new(IntPtr childShape, [In] ref Vector3 localScaling);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSerializerWrapper_new(IntPtr allocateCallback, IntPtr finalizeChunkCallback,
			IntPtr findNameForPointerCallback, IntPtr findPointerCallback, IntPtr finishSerializationCallback,
			IntPtr getBufferPointerCallback, IntPtr getChunkCallback, IntPtr getCurrentBufferSizeCallback,
			IntPtr getNumChunksCallback, IntPtr getSerializationFlagsCallback, IntPtr getUniquePointerCallback,
			IntPtr registerNameForPointerCallback, IntPtr serializeNameCallback, IntPtr setSerializationFlagsCallback,
			IntPtr startSerializationCallback);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSerializer_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSequentialImpulseConstraintSolver_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern ulong btSequentialImpulseConstraintSolver_btRand2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSequentialImpulseConstraintSolver_btRandInt2(IntPtr obj, int n);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern ulong btSequentialImpulseConstraintSolver_getRandSeed(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSequentialImpulseConstraintSolver_setRandSeed(IntPtr obj, ulong seed);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btShapeHull_new(IntPtr shape);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btShapeHull_buildHull(IntPtr obj, float margin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btShapeHull_getIndexPointer(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btShapeHull_getVertexPointer(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btShapeHull_numIndices(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btShapeHull_numTriangles(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btShapeHull_numVertices(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btShapeHull_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSimulationIslandManager_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSimulationIslandManager_buildAndProcessIslands(IntPtr obj, IntPtr dispatcher, IntPtr collisionWorld, IntPtr callback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSimulationIslandManager_buildIslands(IntPtr obj, IntPtr dispatcher, IntPtr colWorld);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSimulationIslandManager_findUnions(IntPtr obj, IntPtr dispatcher, IntPtr colWorld);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSimulationIslandManager_getSplitIslands(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSimulationIslandManager_getUnionFind(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSimulationIslandManager_initUnionFind(IntPtr obj, int n);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSimulationIslandManager_setSplitIslands(IntPtr obj, bool doSplitIslands);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSimulationIslandManager_storeIslandActivationState(IntPtr obj, IntPtr world);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSimulationIslandManager_updateActivationState(IntPtr obj, IntPtr colWorld, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSimulationIslandManager_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSimulationIslandManager_IslandCallback_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSliderConstraint_new(IntPtr rbA, IntPtr rbB, [In] ref Matrix frameInA, [In] ref Matrix frameInB, bool useLinearReferenceFrameA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSliderConstraint_new2(IntPtr rbB, [In] ref Matrix frameInB, bool useLinearReferenceFrameA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_calculateTransforms(IntPtr obj, [In] ref Matrix transA, [In] ref Matrix transB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_getAncorInA(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_getAncorInB(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSliderConstraint_getAngDepth(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSliderConstraint_getAngularPos(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_getCalculatedTransformA(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_getCalculatedTransformB(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSliderConstraint_getDampingDirAng(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSliderConstraint_getDampingDirLin(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSliderConstraint_getDampingLimAng(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSliderConstraint_getDampingLimLin(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSliderConstraint_getDampingOrthoAng(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSliderConstraint_getDampingOrthoLin(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern SliderFlags btSliderConstraint_getFlags(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_getFrameOffsetA(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_getFrameOffsetB(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_getInfo1NonVirtual(IntPtr obj, IntPtr info);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_getInfo2NonVirtual(IntPtr obj, IntPtr info, [In] ref Matrix transA, [In] ref Matrix transB, [In] ref Vector3 linVelA, [In] ref Vector3 linVelB, float rbAinvMass, float rbBinvMass);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSliderConstraint_getLinDepth(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSliderConstraint_getLinearPos(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSliderConstraint_getLowerAngLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSliderConstraint_getLowerLinLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSliderConstraint_getMaxAngMotorForce(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSliderConstraint_getMaxLinMotorForce(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSliderConstraint_getPoweredAngMotor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSliderConstraint_getPoweredLinMotor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSliderConstraint_getRestitutionDirAng(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSliderConstraint_getRestitutionDirLin(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSliderConstraint_getRestitutionLimAng(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSliderConstraint_getRestitutionLimLin(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSliderConstraint_getRestitutionOrthoAng(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSliderConstraint_getRestitutionOrthoLin(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSliderConstraint_getSoftnessDirAng(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSliderConstraint_getSoftnessDirLin(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSliderConstraint_getSoftnessLimAng(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSliderConstraint_getSoftnessLimLin(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSliderConstraint_getSoftnessOrthoAng(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSliderConstraint_getSoftnessOrthoLin(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSliderConstraint_getSolveAngLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSliderConstraint_getSolveLinLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSliderConstraint_getTargetAngMotorVelocity(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSliderConstraint_getTargetLinMotorVelocity(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSliderConstraint_getUpperAngLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSliderConstraint_getUpperLinLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSliderConstraint_getUseFrameOffset(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSliderConstraint_getUseLinearReferenceFrameA(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_setDampingDirAng(IntPtr obj, float dampingDirAng);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_setDampingDirLin(IntPtr obj, float dampingDirLin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_setDampingLimAng(IntPtr obj, float dampingLimAng);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_setDampingLimLin(IntPtr obj, float dampingLimLin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_setDampingOrthoAng(IntPtr obj, float dampingOrthoAng);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_setDampingOrthoLin(IntPtr obj, float dampingOrthoLin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_setFrames(IntPtr obj, [In] ref Matrix frameA, [In] ref Matrix frameB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_setLowerAngLimit(IntPtr obj, float lowerLimit);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_setLowerLinLimit(IntPtr obj, float lowerLimit);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_setMaxAngMotorForce(IntPtr obj, float maxAngMotorForce);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_setMaxLinMotorForce(IntPtr obj, float maxLinMotorForce);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_setPoweredAngMotor(IntPtr obj, bool onOff);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_setPoweredLinMotor(IntPtr obj, bool onOff);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_setRestitutionDirAng(IntPtr obj, float restitutionDirAng);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_setRestitutionDirLin(IntPtr obj, float restitutionDirLin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_setRestitutionLimAng(IntPtr obj, float restitutionLimAng);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_setRestitutionLimLin(IntPtr obj, float restitutionLimLin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_setRestitutionOrthoAng(IntPtr obj, float restitutionOrthoAng);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_setRestitutionOrthoLin(IntPtr obj, float restitutionOrthoLin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_setSoftnessDirAng(IntPtr obj, float softnessDirAng);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_setSoftnessDirLin(IntPtr obj, float softnessDirLin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_setSoftnessLimAng(IntPtr obj, float softnessLimAng);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_setSoftnessLimLin(IntPtr obj, float softnessLimLin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_setSoftnessOrthoAng(IntPtr obj, float softnessOrthoAng);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_setSoftnessOrthoLin(IntPtr obj, float softnessOrthoLin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_setTargetAngMotorVelocity(IntPtr obj, float targetAngMotorVelocity);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_setTargetLinMotorVelocity(IntPtr obj, float targetLinMotorVelocity);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_setUpperAngLimit(IntPtr obj, float upperLimit);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_setUpperLinLimit(IntPtr obj, float upperLimit);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_setUseFrameOffset(IntPtr obj, bool frameOffsetOnOff);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_testAngLimits(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSliderConstraint_testLinLimits(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_new(IntPtr worldInfo, int node_count, [In] Vector3[] x, [In] float[] m);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_new2(IntPtr worldInfo);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_addAeroForceToFace(IntPtr obj, [In] ref Vector3 windVelocity, int faceIndex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_addAeroForceToNode(IntPtr obj, [In] ref Vector3 windVelocity, int nodeIndex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_addForce(IntPtr obj, [In] ref Vector3 force);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_addForce2(IntPtr obj, [In] ref Vector3 force, int node);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_addVelocity(IntPtr obj, [In] ref Vector3 velocity);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_addVelocity2(IntPtr obj, [In] ref Vector3 velocity, int node);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendAnchor(IntPtr obj, int node, IntPtr body, [In] ref Vector3 localPivot, bool disableCollisionBetweenLinkedBodies, float influence);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendAnchor2(IntPtr obj, int node, IntPtr body, bool disableCollisionBetweenLinkedBodies, float influence);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendAngularJoint(IntPtr obj, IntPtr specs);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendAngularJoint2(IntPtr obj, IntPtr specs, IntPtr body);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendAngularJoint3(IntPtr obj, IntPtr specs, IntPtr body);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendAngularJoint4(IntPtr obj, IntPtr specs, IntPtr body0, IntPtr body1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendFace(IntPtr obj, int model, IntPtr mat);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendFace2(IntPtr obj, int node0, int node1, int node2, IntPtr mat);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendLinearJoint(IntPtr obj, IntPtr specs, IntPtr body);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendLinearJoint2(IntPtr obj, IntPtr specs);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendLinearJoint3(IntPtr obj, IntPtr specs, IntPtr body);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendLinearJoint4(IntPtr obj, IntPtr specs, IntPtr body0, IntPtr body1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendLink(IntPtr obj, int node0, int node1, IntPtr mat, bool bcheckexist);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendLink2(IntPtr obj, int model, IntPtr mat);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendLink3(IntPtr obj, IntPtr node0, IntPtr node1, IntPtr mat, bool bcheckexist);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_appendMaterial(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendNode(IntPtr obj, [In] ref Vector3 x, float m);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendNote(IntPtr obj, IntPtr text, [In] ref Vector3 o, IntPtr feature);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendNote2(IntPtr obj, IntPtr text, [In] ref Vector3 o, IntPtr feature);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendNote3(IntPtr obj, IntPtr text, [In] ref Vector3 o, IntPtr feature);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendNote4(IntPtr obj, IntPtr text, [In] ref Vector3 o);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendNote5(IntPtr obj, IntPtr text, [In] ref Vector3 o, [In] ref Vector4 c, IntPtr n0, IntPtr n1, IntPtr n2, IntPtr n3);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendTetra(IntPtr obj, int model, IntPtr mat);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendTetra2(IntPtr obj, int node0, int node1, int node2, int node3, IntPtr mat);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_applyClusters(IntPtr obj, bool drift);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_applyForces(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSoftBody_checkContact(IntPtr obj, IntPtr colObjWrap, [In] ref Vector3 x, float margin, IntPtr cti);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSoftBody_checkFace(IntPtr obj, int node0, int node1, int node2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSoftBody_checkLink(IntPtr obj, IntPtr node0, IntPtr node1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSoftBody_checkLink2(IntPtr obj, int node0, int node1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_cleanupClusters(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_clusterAImpulse(IntPtr cluster, IntPtr impulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_clusterCom(IntPtr obj, int cluster, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_clusterCom2(IntPtr cluster, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_clusterCount(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_clusterDAImpulse(IntPtr cluster, [In] ref Vector3 impulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_clusterDCImpulse(IntPtr cluster, [In] ref Vector3 impulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_clusterDImpulse(IntPtr cluster, [In] ref Vector3 rpos, [In] ref Vector3 impulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_clusterImpulse(IntPtr cluster, [In] ref Vector3 rpos, IntPtr impulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_clusterVAImpulse(IntPtr cluster, [In] ref Vector3 impulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_clusterVelocity(IntPtr cluster, [In] ref Vector3 rpos, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_clusterVImpulse(IntPtr cluster, [In] ref Vector3 rpos, [In] ref Vector3 impulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSoftBody_cutLink(IntPtr obj, IntPtr node0, IntPtr node1, float position);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSoftBody_cutLink2(IntPtr obj, int node0, int node1, float position);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_dampClusters(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_defaultCollisionHandler(IntPtr obj, IntPtr pcoWrap);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_defaultCollisionHandler2(IntPtr obj, IntPtr psb);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_evaluateCom(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_generateBendingConstraints(IntPtr obj, int distance, IntPtr mat);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_generateClusters(IntPtr obj, int k);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_generateClusters2(IntPtr obj, int k, int maxiterations);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_getAabb(IntPtr obj, out Vector3 aabbMin, out Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getAnchors(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getBounds(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSoftBody_getBUpdateRtCst(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getCdbvt(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getCfg(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_getClusterConnectivity(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getClusters(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_getCollisionDisabledObjects(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getFaces(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getFdbvt(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_getInitialWorldTransform(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getJoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getLinks(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_getMass(IntPtr obj, int node);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getMaterials(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getNdbvt(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getNodes(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getNotes(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getPose(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getRcontacts(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_getRestLengthScale(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getScontacts(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getSst(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getTag(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getTetras(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_getTimeacc(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_getTotalMass(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getUserIndexMapping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_getWindVelocity(IntPtr obj, out Vector3 velocity);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_getVolume(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getWorldInfo(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_indicesToPointers(IntPtr obj, int[] map);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_initDefaults(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_initializeClusters(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_initializeFaceTree(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_integrateMotion(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_pointersToIndices(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_predictMotion(IntPtr obj, float dt);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_prepareClusters(IntPtr obj, int iterations);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_PSolve_Anchors(IntPtr psb, float kst, float ti);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_PSolve_Links(IntPtr psb, float kst, float ti);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_PSolve_RContacts(IntPtr psb, float kst, float ti);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_PSolve_SContacts(IntPtr psb, float __unnamed1, float ti);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_randomizeConstraints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSoftBody_rayTest(IntPtr obj, [In] ref Vector3 rayFrom, [In] ref Vector3 rayTo, IntPtr results);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_rayTest2(IntPtr obj, [In] ref Vector3 rayFrom, [In] ref Vector3 rayTo, ref float mint, out SoftBody.FeatureType feature, out int index, bool bcountonly);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_refine(IntPtr obj, IntPtr ifn, float accurary, bool cut);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_releaseCluster(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_releaseClusters(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_resetLinkRestLengths(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_rotate(IntPtr obj, [In] ref Quaternion rot);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_scale(IntPtr obj, [In] ref Vector3 scl);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_setBUpdateRtCst(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_setInitialWorldTransform(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_setMass(IntPtr obj, int node, float mass);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_setPose(IntPtr obj, bool bvolume, bool bframe);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_setRestLengthScale(IntPtr obj, float restLength);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_setSoftBodySolver(IntPtr obj, IntPtr softBodySolver);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_setTag(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_setTimeacc(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_setTotalDensity(IntPtr obj, float density);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_setTotalMass(IntPtr obj, float mass, bool fromfaces);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_setVelocity(IntPtr obj, [In] ref Vector3 velocity);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_setWindVelocity(IntPtr obj, [In] ref Vector3 velocity);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_setVolumeDensity(IntPtr obj, float density);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_setVolumeMass(IntPtr obj, float mass);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_setWorldInfo(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_solveClusters(IntPtr bodies);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_solveClusters2(IntPtr obj, float sor);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_solveCommonConstraints(IntPtr bodies, int count, int iterations);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_solveConstraints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_staticSolve(IntPtr obj, int iterations);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_transform(IntPtr obj, [In] ref Matrix trs);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_translate(IntPtr obj, [In] ref Vector3 trs);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_upcast(IntPtr colObj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_updateArea(IntPtr obj, bool averageArea);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_updateBounds(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_updateClusters(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_updateConstants(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_updateLinkConstants(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_updateNormals(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_updatePose(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_VSolve_Links(IntPtr psb, float kst);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_getFaceVertexData(IntPtr obj, [In, Out] float[] vertices);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_getFaceVertexData(IntPtr obj, [In, Out] Vector3[] vertices);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_getFaceVertexNormalData(IntPtr obj, [In, Out] float[] data);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_getFaceVertexNormalData2(IntPtr obj, [In, Out] float[] vertices, [In, Out] float[] normals);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_getFaceVertexNormalData(IntPtr obj, [In, Out] Vector3[] data);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_getFaceVertexNormalData2(IntPtr obj, [In, Out] Vector3[] vertices, [In, Out] Vector3[] normals);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_getLinkVertexData(IntPtr obj, [In, Out] float[] vertices);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_getLinkVertexData(IntPtr obj, [In, Out] Vector3[] vertices);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_getLinkVertexNormalData(IntPtr obj, [In, Out] float[] data);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_getLinkVertexNormalData(IntPtr obj, [In, Out] Vector3[] data);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_getTetraVertexData(IntPtr obj, [In, Out] float[] vertices);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_getTetraVertexData(IntPtr obj, Vector3[] vertices);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_getTetraVertexNormalData(IntPtr obj, [In, Out] float[] data);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_getTetraVertexNormalData2(IntPtr obj, [In, Out] float[] vectors, [In, Out] float[] normals);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_getTetraVertexNormalData(IntPtr obj, [In, Out] Vector3[] value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_getTetraVertexNormalData2(IntPtr obj, [In, Out] Vector3[] vectors, [In, Out] Vector3[] normals);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_AJoint_IControlWrapper_new(IntPtr prepareCallback, IntPtr speedCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_AJoint_IControlWrapper_getWrapperData(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_AJoint_IControlWrapper_setWrapperData(IntPtr obj, IntPtr data);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_AJoint_IControl_Default();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_AJoint_IControl_Prepare(IntPtr obj, IntPtr __unnamed0);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_AJoint_IControl_Speed(IntPtr obj, IntPtr __unnamed0, float current);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_AJoint_IControl_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_AJoint_Specs_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_AJoint_Specs_getAxis(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_AJoint_Specs_getIcontrol(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_AJoint_Specs_setAxis(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_AJoint_Specs_setIcontrol(IntPtr obj, IntPtr value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_AJoint_getAxis(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_AJoint_getIcontrol(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_AJoint_setIcontrol(IntPtr obj, IntPtr value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Anchor_getBody(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Anchor_getC0(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Anchor_getC1(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Anchor_getC2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Anchor_getInfluence(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Anchor_getLocal(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Anchor_getNode(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Anchor_setBody(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Anchor_setC0(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Anchor_setC1(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Anchor_setC2(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Anchor_setInfluence(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Anchor_setLocal(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Anchor_setNode(IntPtr obj, IntPtr value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Body_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Body_new2(IntPtr colObj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Body_new3(IntPtr p);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Body_activate(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Body_angularVelocity(IntPtr obj, [In] ref Vector3 rpos, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Body_angularVelocity2(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Body_applyAImpulse(IntPtr obj, IntPtr impulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Body_applyDAImpulse(IntPtr obj, [In] ref Vector3 impulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Body_applyDCImpulse(IntPtr obj, [In] ref Vector3 impulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Body_applyDImpulse(IntPtr obj, [In] ref Vector3 impulse, [In] ref Vector3 rpos);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Body_applyImpulse(IntPtr obj, IntPtr impulse, [In] ref Vector3 rpos);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Body_applyVAImpulse(IntPtr obj, [In] ref Vector3 impulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Body_applyVImpulse(IntPtr obj, [In] ref Vector3 impulse, [In] ref Vector3 rpos);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Body_getCollisionObject(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Body_getRigid(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Body_getSoft(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Body_invMass(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Body_invWorldInertia(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Body_linearVelocity(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Body_setCollisionObject(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Body_setRigid(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Body_setSoft(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Body_velocity(IntPtr obj, [In] ref Vector3 rpos, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Body_xform(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Body_delete(IntPtr obj);


		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_CJoint_getFriction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_CJoint_getLife(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_CJoint_getMaxlife(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_CJoint_getNormal(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_CJoint_getRpos(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_CJoint_setFriction(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_CJoint_setLife(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_CJoint_setMaxlife(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_CJoint_setNormal(IntPtr obj, [In] ref Vector3 value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Cluster_getAdamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_getAv(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_Cluster_getClusterIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSoftBody_Cluster_getCollide(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_getCom(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSoftBody_Cluster_getContainsAnchor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Cluster_getDimpulses(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Cluster_getFramerefs(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_getFramexform(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Cluster_getIdmass(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Cluster_getImass(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_getInvwi(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Cluster_getLdamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Cluster_getLeaf(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_getLocii(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_getLv(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Cluster_getMasses(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Cluster_getMatching(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Cluster_getMaxSelfCollisionImpulse(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Cluster_getNdamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_Cluster_getNdimpulses(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Cluster_getNodes(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_Cluster_getNvimpulses(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Cluster_getSelfCollisionImpulseFactor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Cluster_getVimpulses(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setAdamping(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setAv(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setClusterIndex(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setCollide(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setCom(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setContainsAnchor(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setFramexform(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setIdmass(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setImass(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setInvwi(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setLdamping(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setLeaf(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setLocii(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setLv(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setMatching(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setMaxSelfCollisionImpulse(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setNdamping(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setNdimpulses(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setNvimpulses(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setSelfCollisionImpulseFactor(IntPtr obj, float value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern SoftBody.AeroModel btSoftBody_Config_getAeromodel(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_Config_getCiterations(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern SoftBody.Collisions btSoftBody_Config_getCollisions(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_Config_getDiterations(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Config_getDsequence(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getKAHR(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getKCHR(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getKDF(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getKDG(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getKDP(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getKKHR(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getKLF(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getKMT(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getKPR(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getKSHR(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getKSK_SPLT_CL(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getKSKHR_CL(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getKSR_SPLT_CL(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getKSRHR_CL(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getKSS_SPLT_CL(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getKSSHR_CL(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getKVC(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getKVCF(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getMaxvolume(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_Config_getPiterations(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Config_getPsequence(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getTimescale(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_Config_getViterations(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Config_getVsequence(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setAeromodel(IntPtr obj, SoftBody.AeroModel value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setCiterations(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setCollisions(IntPtr obj, SoftBody.Collisions value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setDiterations(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setKAHR(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setKCHR(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setKDF(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setKDG(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setKDP(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setKKHR(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setKLF(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setKMT(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setKPR(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setKSHR(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setKSK_SPLT_CL(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setKSKHR_CL(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setKSR_SPLT_CL(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setKSRHR_CL(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setKSS_SPLT_CL(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setKSSHR_CL(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setKVC(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setKVCF(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setMaxvolume(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setPiterations(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setTimescale(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setViterations(IntPtr obj, int value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Element_getTag(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Element_setTag(IntPtr obj, IntPtr value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Face_getLeaf(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Face_getN(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Face_getNormal(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Face_getRa(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Face_setLeaf(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Face_setNormal(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Face_setRa(IntPtr obj, float value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Feature_getMaterial(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Feature_setMaterial(IntPtr obj, IntPtr value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_ImplicitFnWrapper_new(IntPtr evalCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_ImplicitFn_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Impulse_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_Impulse_getAsDrift(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_Impulse_getAsVelocity(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Impulse_getDrift(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Impulse_getVelocity(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Impulse_operator_n(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Impulse_operator_m(IntPtr obj, float x);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Impulse_setAsDrift(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Impulse_setAsVelocity(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Impulse_setDrift(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Impulse_setVelocity(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Impulse_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Joint_Specs_getCfm(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Joint_Specs_getErp(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Joint_Specs_getSplit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Joint_Specs_setCfm(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Joint_Specs_setErp(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Joint_Specs_setSplit(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Joint_Specs_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Joint_getBodies(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Joint_getCfm(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSoftBody_Joint_getDelete(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Joint_getDrift(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Joint_getErp(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Joint_getMassmatrix(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Joint_getRefs(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Joint_getSdrift(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Joint_getSplit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Joint_Prepare(IntPtr obj, float dt, int iterations);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Joint_setCfm(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Joint_setDelete(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Joint_setDrift(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Joint_setErp(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Joint_setMassmatrix(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Joint_setSdrift(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Joint_setSplit(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Joint_Solve(IntPtr obj, float dt, float sor);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Joint_Terminate(IntPtr obj, float dt);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern SoftBody.JointType btSoftBody_Joint_Type(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Link_new2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_Link_getBbending(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Link_getC0(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Link_getC1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Link_getC2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Link_getC3(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Link_getN(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Link_getRl(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Link_setBbending(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Link_setC0(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Link_setC1(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Link_setC2(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Link_setC3(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Link_setRl(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Link_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_LJoint_Specs_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_LJoint_Specs_getPosition(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_LJoint_Specs_setPosition(IntPtr obj, [In] ref Vector3 value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_LJoint_getRpos(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern SoftBody.MaterialFlags btSoftBody_Material_getFlags(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Material_getKAST(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Material_getKLST(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Material_getKVST(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Material_setFlags(IntPtr obj, SoftBody.MaterialFlags value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Material_setKAST(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Material_setKLST(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Material_setKVST(IntPtr obj, float value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Node_getArea(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_Node_getBattach(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Node_getF(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Node_getIm(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Node_getLeaf(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Node_getN(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Node_getQ(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Node_getV(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Node_getX(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Node_setArea(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Node_setBattach(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Node_setF(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Node_setIm(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Node_setLeaf(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Node_setN(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Node_setQ(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Node_setV(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Node_setX(IntPtr obj, [In] ref Vector3 value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Note_getCoords(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Note_getNodes(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Note_getOffset(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_Note_getRank(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.LPStr)]
		public static extern string btSoftBody_Note_getText(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Note_setOffset(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Note_setRank(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Note_setText(IntPtr obj, IntPtr value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Pose_getAqq(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSoftBody_Pose_getBframe(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSoftBody_Pose_getBvolume(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Pose_getCom(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Pose_getPos(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Pose_getRot(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Pose_getScl(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Pose_getWgh(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Pose_getVolume(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Pose_setAqq(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Pose_setBframe(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Pose_setBvolume(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Pose_setCom(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Pose_setRot(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Pose_setScl(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Pose_setVolume(IntPtr obj, float value);


		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_RayFromToCaster_new([In] ref Vector3 rayFrom, [In] ref Vector3 rayTo, float mxt);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_RayFromToCaster_getFace(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_RayFromToCaster_getMint(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_RayFromToCaster_getRayFrom(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_RayFromToCaster_getRayNormalizedDirection(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_RayFromToCaster_getRayTo(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_RayFromToCaster_getTests(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_RayFromToCaster_rayFromToTriangle([In] ref Vector3 rayFrom, [In] ref Vector3 rayTo, [In] ref Vector3 rayNormalizedDirection, [In] ref Vector3 a, [In] ref Vector3 b, [In] ref Vector3 c, float maxt);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_RayFromToCaster_setFace(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_RayFromToCaster_setMint(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_RayFromToCaster_setRayFrom(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_RayFromToCaster_setRayNormalizedDirection(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_RayFromToCaster_setRayTo(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_RayFromToCaster_setTests(IntPtr obj, int value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_SolverState_getIsdt(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_SolverState_getRadmrg(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_SolverState_getSdt(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_SolverState_getUpdmrg(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_SolverState_getVelmrg(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_SolverState_setIsdt(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_SolverState_setRadmrg(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_SolverState_setSdt(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_SolverState_setUpdmrg(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_SolverState_setVelmrg(IntPtr obj, float value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_sRayCast_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern SoftBody.FeatureType btSoftBody_sRayCast_getFeature(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_sRayCast_getFraction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_sRayCast_getIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_sRayCast_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Tetra_getC0(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Tetra_getC1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Tetra_getC2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Tetra_getLeaf(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Tetra_getN(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Tetra_getRv(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Tetra_setC1(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Tetra_setC2(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Tetra_setLeaf(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Tetra_setRv(IntPtr obj, float value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_SContact_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_SContact_getCfm(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_SContact_getFace(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_SContact_getFriction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_SContact_getMargin(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_SContact_getNode(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_SContact_getNormal(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_SContact_getWeights(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_SContact_setFace(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_SContact_setFriction(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_SContact_setMargin(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_SContact_setNode(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_SContact_setNormal(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_SContact_setWeights(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_SContact_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_sCti_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_sCti_getColObj(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_sCti_getNormal(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_sCti_getOffset(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_sCti_setColObj(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_sCti_setNormal(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_sCti_setOffset(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_sCti_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_RContact_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_RContact_getC0(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_RContact_getC1(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_RContact_getC2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_RContact_getC3(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_RContact_getC4(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_RContact_getCti(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_RContact_getNode(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_RContact_setC0(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_RContact_setC1(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_RContact_setC2(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_RContact_setC3(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_RContact_setC4(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_RContact_setNode(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_RContact_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBodyConcaveCollisionAlgorithm_CreateFunc_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBodyConcaveCollisionAlgorithm_SwappedCreateFunc_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBodyConcaveCollisionAlgorithm_new(IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap, bool isSwapped);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBodyConcaveCollisionAlgorithm_clearCache(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBodyHelpers_CreateFromConvexHull(IntPtr worldInfo, [In] Vector3[] vertices, int nvertices, bool randomizeConstraints);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBodyHelpers_CreatePatchUV(IntPtr worldInfo, [In] ref Vector3 corner00, [In] ref Vector3 corner10, [In] ref Vector3 corner01, [In] ref Vector3 corner11, int resx, int resy, int fixeds, bool gendiags, float[] tex_coords);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBodyHelpers_Draw(IntPtr psb, IntPtr idraw, SoftBody.DrawFlags drawflags);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBodyHelpers_DrawClusterTree(IntPtr psb, IntPtr idraw, int minDepth, int maxDepth);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBodyHelpers_DrawFaceTree(IntPtr psb, IntPtr idraw, int minDepth, int maxDepth);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBodyHelpers_DrawFrame(IntPtr psb, IntPtr idraw);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBodyHelpers_DrawInfos(IntPtr psb, IntPtr idraw, bool masses, bool areas, bool stress);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBodyHelpers_DrawNodeTree(IntPtr psb, IntPtr idraw, int minDepth, int maxDepth);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBodyRigidBodyCollisionConfiguration_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBodyRigidBodyCollisionConfiguration_new2(IntPtr constructionInfo);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSoftBodySolver_checkInitialized(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBodySolver_copyBackToSoftBodies(IntPtr obj, bool bMove);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBodySolver_getNumberOfPositionIterations(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBodySolver_getNumberOfVelocityIterations(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBodySolver_getTimeScale(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBodySolver_optimize(IntPtr obj, IntPtr softBodies, bool forceUpdate);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBodySolver_predictMotion(IntPtr obj, float solverdt);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBodySolver_setNumberOfPositionIterations(IntPtr obj, int iterations);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBodySolver_setNumberOfVelocityIterations(IntPtr obj, int iterations);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBodySolver_solveConstraints(IntPtr obj, float solverdt);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBodySolver_updateSoftBodies(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBodySolver_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBodyWorldInfo_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBodyWorldInfo_getAir_density(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBodyWorldInfo_getGravity(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBodyWorldInfo_getMaxDisplacement(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBodyWorldInfo_getSparsesdf(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBodyWorldInfo_getWater_density(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBodyWorldInfo_getWater_normal(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBodyWorldInfo_getWater_offset(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBodyWorldInfo_setAir_density(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBodyWorldInfo_setBroadphase(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBodyWorldInfo_setDispatcher(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBodyWorldInfo_setGravity(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBodyWorldInfo_setMaxDisplacement(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBodyWorldInfo_setWater_density(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBodyWorldInfo_setWater_normal(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBodyWorldInfo_setWater_offset(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBodyWorldInfo_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftRigidCollisionAlgorithm_CreateFunc_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftRigidCollisionAlgorithm_new(IntPtr mf, IntPtr ci, IntPtr col0, IntPtr col1Wrap, bool isSwapped);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftRigidDynamicsWorld_new(IntPtr dispatcher, IntPtr pairCache, IntPtr constraintSolver, IntPtr collisionConfiguration, IntPtr softBodySolver);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftRigidDynamicsWorld_addSoftBody(IntPtr obj, IntPtr body);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftRigidDynamicsWorld_addSoftBody3(IntPtr obj, IntPtr body, int collisionFilterGroup, int collisionFilterMask);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftRigidDynamicsWorld_getDrawFlags(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftRigidDynamicsWorld_getSoftBodyArray(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftRigidDynamicsWorld_getWorldInfo(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftRigidDynamicsWorld_removeSoftBody(IntPtr obj, IntPtr body);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftRigidDynamicsWorld_setDrawFlags(IntPtr obj, int f);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftSoftCollisionAlgorithm_CreateFunc_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftSoftCollisionAlgorithm_new(IntPtr ci);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftSoftCollisionAlgorithm_new2(IntPtr mf, IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSortedOverlappingPairCache_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSortedOverlappingPairCache_getOverlapFilterCallback(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSortedOverlappingPairCache_needsBroadphaseCollision(IntPtr obj, IntPtr proxy0, IntPtr proxy1);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSparseSdf_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSparseSdf3_GarbageCollect(IntPtr obj, int lifetime);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSparseSdf3_Initialize(IntPtr obj, int hashsize, int clampCells);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSparseSdf3_RemoveReferences(IntPtr obj, IntPtr pcs);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSparseSdf3_Reset(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSphereBoxCollisionAlgorithm_CreateFunc_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSphereBoxCollisionAlgorithm_new(IntPtr mf, IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap, bool isSwapped);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSphereBoxCollisionAlgorithm_getSphereDistance(IntPtr obj, IntPtr boxObjWrap, out Vector3 v3PointOnBox, out Vector3 normal, out float penetrationDepth, [In] ref Vector3 v3SphereCenter, float fRadius, float maxContactDistance);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSphereBoxCollisionAlgorithm_getSpherePenetration(IntPtr obj, [In] ref Vector3 boxHalfExtent, [In] ref Vector3 sphereRelPos, out Vector3 closestPoint, out Vector3 normal);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSphereShape_new(float radius);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSphereShape_getRadius(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSphereShape_setUnscaledRadius(IntPtr obj, float radius);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSphereSphereCollisionAlgorithm_CreateFunc_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSphereSphereCollisionAlgorithm_new(IntPtr mf, IntPtr ci, IntPtr col0Wrap, IntPtr col1Wrap);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSphereSphereCollisionAlgorithm_new2(IntPtr ci);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSphereTriangleCollisionAlgorithm_CreateFunc_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSphereTriangleCollisionAlgorithm_new(IntPtr mf, IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap, bool swapped);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSphereTriangleCollisionAlgorithm_new2(IntPtr ci);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btStaticPlaneShape_new([In] ref Vector3 planeNormal, float planeConstant);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btStaticPlaneShape_getPlaneConstant(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btStaticPlaneShape_getPlaneNormal(IntPtr obj, out Vector3 value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btStorageResult_getClosestPointInB(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btStorageResult_getDistance(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btStorageResult_getNormalOnSurfaceB(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btStorageResult_setClosestPointInB(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btStorageResult_setDistance(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btStorageResult_setNormalOnSurfaceB(IntPtr obj, [In] ref Vector3 value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btStridingMeshInterface_calculateAabbBruteForce(IntPtr obj, out Vector3 aabbMin, out Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btStridingMeshInterface_calculateSerializeBufferSize(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btStridingMeshInterface_getLockedReadOnlyVertexIndexBase(IntPtr obj, out IntPtr vertexbase, out int numverts, out PhyScalarType type, out int vertexStride, out IntPtr indexbase, out int indexstride, out int numfaces, out PhyScalarType indicestype, int subpart);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btStridingMeshInterface_getLockedVertexIndexBase(IntPtr obj, out IntPtr vertexbase, out int numverts, out PhyScalarType type, out int vertexStride, out IntPtr indexbase, out int indexstride, out int numfaces, out PhyScalarType indicestype, int subpart);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btStridingMeshInterface_getNumSubParts(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btStridingMeshInterface_getPremadeAabb(IntPtr obj, out Vector3 aabbMin, out Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btStridingMeshInterface_getScaling(IntPtr obj, out Vector3 scaling);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btStridingMeshInterface_hasPremadeAabb(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btStridingMeshInterface_InternalProcessAllTriangles(IntPtr obj, IntPtr callback, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btStridingMeshInterface_preallocateIndices(IntPtr obj, int numindices);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btStridingMeshInterface_preallocateVertices(IntPtr obj, int numverts);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btStridingMeshInterface_serialize(IntPtr obj, IntPtr dataBuffer, IntPtr serializer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btStridingMeshInterface_setPremadeAabb(IntPtr obj, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btStridingMeshInterface_setScaling(IntPtr obj, [In] ref Vector3 scaling);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btStridingMeshInterface_unLockReadOnlyVertexBase(IntPtr obj, int subpart);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btStridingMeshInterface_unLockVertexBase(IntPtr obj, int subpart);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btStridingMeshInterface_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSubSimplexClosestResult_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSubSimplexClosestResult_getBarycentricCoords(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSubSimplexClosestResult_getClosestPointOnSimplex(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSubSimplexClosestResult_getDegenerate(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSubSimplexClosestResult_getUsedVertices(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSubSimplexClosestResult_isValid(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSubSimplexClosestResult_reset(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSubSimplexClosestResult_setBarycentricCoordinates(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSubSimplexClosestResult_setBarycentricCoordinates2(IntPtr obj, float a);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSubSimplexClosestResult_setBarycentricCoordinates3(IntPtr obj, float a, float b);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSubSimplexClosestResult_setBarycentricCoordinates4(IntPtr obj, float a, float b, float c);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSubSimplexClosestResult_setBarycentricCoordinates5(IntPtr obj, float a, float b, float c, float d);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSubSimplexClosestResult_setClosestPointOnSimplex(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSubSimplexClosestResult_setDegenerate(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSubSimplexClosestResult_setUsedVertices(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSubSimplexClosestResult_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTetrahedronShapeEx_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTetrahedronShapeEx_setVertices(IntPtr obj, [In] ref Vector3 v0, [In] ref Vector3 v1, [In] ref Vector3 v2, [In] ref Vector3 v3);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btThreads_btGetOpenMPTaskScheduler();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btThreads_btGetPPLTaskScheduler();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btThreads_btGetSequentialTaskScheduler();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btThreads_btGetTBBTaskScheduler();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btThreads_btSetTaskScheduler(IntPtr taskScheduler);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTransformUtil_calculateDiffAxisAngle([In] ref Matrix transform0, [In] ref Matrix transform1, out Vector3 axis, out float angle);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTransformUtil_calculateDiffAxisAngleQuaternion([In] ref Quaternion orn0, [In] ref Quaternion orn1a, out Vector3 axis, out float angle);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTransformUtil_calculateVelocity([In] ref Matrix transform0, [In] ref Matrix transform1, float timeStep, out Vector3 linVel, out Vector3 angVel);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTransformUtil_calculateVelocityQuaternion([In] ref Vector3 pos0, [In] ref Vector3 pos1, [In] ref Quaternion orn0, [In] ref Quaternion orn1, float timeStep, out Vector3 linVel, out Vector3 angVel);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTransformUtil_integrateTransform([In] ref Matrix curTrans, [In] ref Vector3 linvel, [In] ref Vector3 angvel, float timeStep, out Matrix predictedTransform);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTranslationalLimitMotor_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTranslationalLimitMotor_new2(IntPtr other);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor_getAccumulatedImpulse(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTranslationalLimitMotor_getCurrentLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor_getCurrentLimitError(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor_getCurrentLinearDiff(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btTranslationalLimitMotor_getDamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTranslationalLimitMotor_getEnableMotor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btTranslationalLimitMotor_getLimitSoftness(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor_getLowerLimit(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor_getMaxMotorForce(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor_getNormalCFM(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btTranslationalLimitMotor_getRestitution(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor_getStopCFM(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor_getStopERP(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor_getTargetVelocity(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor_getUpperLimit(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btTranslationalLimitMotor_isLimited(IntPtr obj, int limitIndex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btTranslationalLimitMotor_needApplyForce(IntPtr obj, int limitIndex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor_setAccumulatedImpulse(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor_setCurrentLimitError(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor_setCurrentLinearDiff(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor_setDamping(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor_setLimitSoftness(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor_setLowerLimit(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor_setMaxMotorForce(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor_setNormalCFM(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor_setRestitution(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor_setStopCFM(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor_setStopERP(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor_setTargetVelocity(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor_setUpperLimit(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btTranslationalLimitMotor_solveLinearAxis(IntPtr obj, float timeStep, float jacDiagABInv, IntPtr body1, [In] ref Vector3 pointInA, IntPtr body2, [In] ref Vector3 pointInB, int limit_index, [In] ref Vector3 axis_normal_on_a, [In] ref Vector3 anchorPos);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btTranslationalLimitMotor_testLimitValue(IntPtr obj, int limitIndex, float test_value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTranslationalLimitMotor2_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTranslationalLimitMotor2_new2(IntPtr other);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor2_getBounce(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTranslationalLimitMotor2_getCurrentLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor2_getCurrentLimitError(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor2_getCurrentLimitErrorHi(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor2_getCurrentLinearDiff(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTranslationalLimitMotor2_getEnableMotor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTranslationalLimitMotor2_getEnableSpring(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor2_getEquilibriumPoint(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor2_getLowerLimit(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor2_getMaxMotorForce(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor2_getMotorCFM(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor2_getMotorERP(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTranslationalLimitMotor2_getServoMotor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor2_getServoTarget(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor2_getSpringDamping(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTranslationalLimitMotor2_getSpringDampingLimited(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor2_getSpringStiffness(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTranslationalLimitMotor2_getSpringStiffnessLimited(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor2_getStopCFM(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor2_getStopERP(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor2_getTargetVelocity(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor2_getUpperLimit(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btTranslationalLimitMotor2_isLimited(IntPtr obj, int limitIndex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor2_setBounce(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor2_setCurrentLimitError(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor2_setCurrentLimitErrorHi(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor2_setCurrentLinearDiff(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor2_setEquilibriumPoint(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor2_setLowerLimit(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor2_setMaxMotorForce(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor2_setMotorCFM(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor2_setMotorERP(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor2_setServoTarget(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor2_setSpringDamping(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor2_setSpringStiffness(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor2_setStopCFM(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor2_setStopERP(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor2_setTargetVelocity(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor2_setUpperLimit(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor2_testLimitValue(IntPtr obj, int limitIndex, float test_value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTranslationalLimitMotor2_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTriangleBuffer_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleBuffer_clearBuffer(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btTriangleBuffer_getNumTriangles(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTriangleBuffer_getTriangle(IntPtr obj, int index);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTriangleCallbackWrapper_new(IntPtr internalProcessTriangleIndexCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleCallback_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTriangleIndexVertexArray_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTriangleIndexVertexArray_new2(int numTriangles, IntPtr triangleIndexBase, int triangleIndexStride, int numVertices, IntPtr vertexBase, int vertexStride);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleIndexVertexArray_addIndexedMesh(IntPtr obj, IntPtr mesh, PhyScalarType indexType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTriangleIndexVertexArray_getIndexedMeshArray(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTriangleIndexVertexMaterialArray_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTriangleIndexVertexMaterialArray_new2(int numTriangles, IntPtr triangleIndexBase, int triangleIndexStride, int numVertices, IntPtr vertexBase, int vertexStride, int numMaterials, IntPtr materialBase, int materialStride, IntPtr triangleMaterialsBase, int materialIndexStride);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleIndexVertexMaterialArray_addMaterialProperties(IntPtr obj, IntPtr mat, PhyScalarType triangleType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleIndexVertexMaterialArray_getLockedMaterialBase(IntPtr obj, out IntPtr materialBase, out int numMaterials, out PhyScalarType materialType, out int materialStride, out IntPtr triangleMaterialBase, out int numTriangles, out int triangleMaterialStride, out PhyScalarType triangleType, int subpart);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleIndexVertexMaterialArray_getLockedReadOnlyMaterialBase(IntPtr obj, out IntPtr materialBase, out int numMaterials, out PhyScalarType materialType, out int materialStride, out IntPtr triangleMaterialBase, out int numTriangles, out int triangleMaterialStride, out PhyScalarType triangleType, int subpart);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTriangleInfoMap_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btTriangleInfoMap_calculateSerializeBufferSize(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btTriangleInfoMap_getConvexEpsilon(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btTriangleInfoMap_getEdgeDistanceThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btTriangleInfoMap_getEqualVertexThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btTriangleInfoMap_getMaxEdgeAngleThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btTriangleInfoMap_getPlanarEpsilon(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btTriangleInfoMap_getZeroAreaThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTriangleInfoMap_serialize(IntPtr obj, IntPtr dataBuffer, IntPtr serializer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleInfoMap_setConvexEpsilon(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleInfoMap_setEdgeDistanceThreshold(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleInfoMap_setEqualVertexThreshold(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleInfoMap_setMaxEdgeAngleThreshold(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleInfoMap_setPlanarEpsilon(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleInfoMap_setZeroAreaThreshold(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleInfoMap_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTriangleInfo_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btTriangleInfo_getEdgeV0V1Angle(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btTriangleInfo_getEdgeV1V2Angle(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btTriangleInfo_getEdgeV2V0Angle(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btTriangleInfo_getFlags(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleInfo_setEdgeV0V1Angle(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleInfo_setEdgeV1V2Angle(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleInfo_setEdgeV2V0Angle(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleInfo_setFlags(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleInfo_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleMeshShape_getLocalAabbMax(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleMeshShape_getLocalAabbMin(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTriangleMeshShape_getMeshInterface(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleMeshShape_localGetSupportingVertex(IntPtr obj, [In] ref Vector3 vec, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleMeshShape_localGetSupportingVertexWithoutMargin(IntPtr obj, [In] ref Vector3 vec, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleMeshShape_recalcLocalAabb(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTriangleMesh_new(bool use32bitIndices, bool use4componentVertices);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleMesh_addIndex(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleMesh_addTriangle(IntPtr obj, [In] ref Vector3 vertex0, [In] ref Vector3 vertex1, [In] ref Vector3 vertex2, bool removeDuplicateVertices);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleMesh_addTriangleIndices(IntPtr obj, int index1, int index2, int index3);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btTriangleMesh_findOrAddVertex(IntPtr obj, [In] ref Vector3 vertex, bool removeDuplicateVertices);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btTriangleMesh_getNumTriangles(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btTriangleMesh_getUse32bitIndices(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btTriangleMesh_getUse4componentVertices(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btTriangleMesh_getWeldingThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleMesh_setWeldingThreshold(IntPtr obj, float value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTriangleShape_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTriangleShape_new2([In] ref Vector3 p0, [In] ref Vector3 p1, [In] ref Vector3 p2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleShape_calcNormal(IntPtr obj, out Vector3 normal);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleShape_getPlaneEquation(IntPtr obj, int i, out Vector3 planeNormal, out Vector3 planeSupport);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTriangleShape_getVertexPtr(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTriangleShape_getVertices1(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTriangleShapeEx_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTriangleShapeEx_new2([In] ref Vector3 p0, [In] ref Vector3 p1, [In] ref Vector3 p2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTriangleShapeEx_new3(IntPtr other);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleShapeEx_applyTransform(IntPtr obj, [In] ref Matrix t);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleShapeEx_buildTriPlane(IntPtr obj, [Out] out Vector4 plane);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btTriangleShapeEx_overlap_test_conservative(IntPtr obj, IntPtr other);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTriangle_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btTriangle_getPartId(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btTriangle_getTriangleIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangle_getVertex0(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangle_getVertex1(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangle_getVertex2(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangle_setPartId(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangle_setTriangleIndex(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangle_setVertex0(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangle_setVertex1(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangle_setVertex2(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangle_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btChunk_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btChunk_getChunkCode(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btChunk_getDna_nr(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btChunk_getLength(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btChunk_getNumber(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btChunk_getOldPtr(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btChunk_setChunkCode(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btChunk_setDna_nr(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btChunk_setLength(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btChunk_setNumber(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btChunk_setOldPtr(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btChunk_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTypedConstraint_btConstraintInfo1_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btTypedConstraint_btConstraintInfo1_getNub(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btTypedConstraint_btConstraintInfo1_getNumConstraintRows(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTypedConstraint_btConstraintInfo1_setNub(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTypedConstraint_btConstraintInfo1_setNumConstraintRows(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTypedConstraint_btConstraintInfo1_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTypedConstraint_btConstraintInfo2_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTypedConstraint_btConstraintInfo2_getCfm(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTypedConstraint_btConstraintInfo2_getConstraintError(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btTypedConstraint_btConstraintInfo2_getDamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btTypedConstraint_btConstraintInfo2_getErp(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTypedConstraint_btConstraintInfo2_getFindex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btTypedConstraint_btConstraintInfo2_getFps(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTypedConstraint_btConstraintInfo2_getJ1angularAxis(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTypedConstraint_btConstraintInfo2_getJ1linearAxis(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTypedConstraint_btConstraintInfo2_getJ2angularAxis(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTypedConstraint_btConstraintInfo2_getJ2linearAxis(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTypedConstraint_btConstraintInfo2_getLowerLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btTypedConstraint_btConstraintInfo2_getNumIterations(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btTypedConstraint_btConstraintInfo2_getRowskip(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTypedConstraint_btConstraintInfo2_getUpperLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTypedConstraint_btConstraintInfo2_setCfm(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTypedConstraint_btConstraintInfo2_setConstraintError(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTypedConstraint_btConstraintInfo2_setDamping(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTypedConstraint_btConstraintInfo2_setErp(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTypedConstraint_btConstraintInfo2_setFindex(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTypedConstraint_btConstraintInfo2_setFps(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTypedConstraint_btConstraintInfo2_setJ1angularAxis(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTypedConstraint_btConstraintInfo2_setJ1linearAxis(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTypedConstraint_btConstraintInfo2_setJ2angularAxis(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTypedConstraint_btConstraintInfo2_setJ2linearAxis(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTypedConstraint_btConstraintInfo2_setLowerLimit(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTypedConstraint_btConstraintInfo2_setNumIterations(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTypedConstraint_btConstraintInfo2_setRowskip(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTypedConstraint_btConstraintInfo2_setUpperLimit(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTypedConstraint_btConstraintInfo2_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTypedConstraint_buildJacobian(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btTypedConstraint_calculateSerializeBufferSize(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTypedConstraint_enableFeedback(IntPtr obj, bool needsFeedback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btTypedConstraint_getAppliedImpulse(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btTypedConstraint_getBreakingImpulseThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern TypedConstraintType btTypedConstraint_getConstraintType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btTypedConstraint_getDbgDrawSize(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTypedConstraint_getInfo1(IntPtr obj, IntPtr info);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTypedConstraint_getInfo2(IntPtr obj, IntPtr info);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTypedConstraint_getJointFeedback(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btTypedConstraint_getOverrideNumSolverIterations(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btTypedConstraint_getParam(IntPtr obj, ConstraintParam num);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btTypedConstraint_getParam2(IntPtr obj, ConstraintParam num, int axis);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btTypedConstraint_getUid(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btTypedConstraint_getUserConstraintId(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTypedConstraint_getUserConstraintPtr(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btTypedConstraint_getUserConstraintType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btTypedConstraint_internalGetAppliedImpulse(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTypedConstraint_internalSetAppliedImpulse(IntPtr obj, float appliedImpulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btTypedConstraint_isEnabled(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btTypedConstraint_needsFeedback(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTypedConstraint_serialize(IntPtr obj, IntPtr dataBuffer, IntPtr serializer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTypedConstraint_setBreakingImpulseThreshold(IntPtr obj, float threshold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTypedConstraint_setDbgDrawSize(IntPtr obj, float dbgDrawSize);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTypedConstraint_setEnabled(IntPtr obj, bool enabled);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTypedConstraint_setJointFeedback(IntPtr obj, IntPtr jointFeedback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTypedConstraint_setOverrideNumSolverIterations(IntPtr obj, int overideNumIterations);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTypedConstraint_setParam(IntPtr obj, ConstraintParam num, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTypedConstraint_setParam2(IntPtr obj, ConstraintParam num, float value, int axis);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTypedConstraint_setupSolverConstraint(IntPtr obj, IntPtr ca, int solverBodyA, int solverBodyB, float timeStep);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTypedConstraint_setUserConstraintId(IntPtr obj, int uid);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTypedConstraint_setUserConstraintPtr(IntPtr obj, IntPtr ptr);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTypedConstraint_setUserConstraintType(IntPtr obj, int userConstraintType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTypedConstraint_solveConstraintObsolete(IntPtr obj, IntPtr __unnamed0, IntPtr __unnamed1, float __unnamed2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTypedConstraint_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btUniformScalingShape_new(IntPtr convexChildShape, float uniformScalingFactor);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btUniformScalingShape_getUniformScalingFactor(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btUnionFind_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btUnionFind_allocate(IntPtr obj, int N);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btUnionFind_find(IntPtr obj, int p, int q);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btUnionFind_find2(IntPtr obj, int x);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btUnionFind_Free(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btUnionFind_getElement(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btUnionFind_getNumElements(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btUnionFind_isRoot(IntPtr obj, int x);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btUnionFind_reset(IntPtr obj, int N);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btUnionFind_sortIslands(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btUnionFind_unite(IntPtr obj, int p, int q);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btUnionFind_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btUniversalConstraint_new(IntPtr rbA, IntPtr rbB, [In] ref Vector3 anchor, [In] ref Vector3 axis1, [In] ref Vector3 axis2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btUniversalConstraint_getAnchor(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btUniversalConstraint_getAnchor2(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btUniversalConstraint_getAngle1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btUniversalConstraint_getAngle2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btUniversalConstraint_getAxis1(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btUniversalConstraint_getAxis2(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btUniversalConstraint_setLowerLimit(IntPtr obj, float ang1min, float ang2min);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btUniversalConstraint_setUpperLimit(IntPtr obj, float ang1max, float ang2max);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btUsageBitfield_getUnused1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btUsageBitfield_getUnused2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btUsageBitfield_getUnused3(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btUsageBitfield_getUnused4(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btUsageBitfield_getUsedVertexA(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btUsageBitfield_getUsedVertexB(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btUsageBitfield_getUsedVertexC(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btUsageBitfield_getUsedVertexD(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btUsageBitfield_reset(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btUsageBitfield_setUnused1(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btUsageBitfield_setUnused2(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btUsageBitfield_setUnused3(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btUsageBitfield_setUnused4(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btUsageBitfield_setUsedVertexA(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btUsageBitfield_setUsedVertexB(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btUsageBitfield_setUsedVertexC(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btUsageBitfield_setUsedVertexD(IntPtr obj, bool value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btVector3_array_at(IntPtr obj, int n, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btVector3_array_set(IntPtr obj, int n, [In] ref Vector3 value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btVoronoiSimplexSolver_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btVoronoiSimplexSolver_addVertex(IntPtr obj, [In] ref Vector3 w, [In] ref Vector3 p, [In] ref Vector3 q);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btVoronoiSimplexSolver_backup_closest(IntPtr obj, out Vector3 v);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btVoronoiSimplexSolver_closest(IntPtr obj, out Vector3 v);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btVoronoiSimplexSolver_closestPtPointTetrahedron(IntPtr obj, [In] ref Vector3 p, [In] ref Vector3 a, [In] ref Vector3 b, [In] ref Vector3 c, [In] ref Vector3 d, IntPtr finalResult);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btVoronoiSimplexSolver_closestPtPointTriangle(IntPtr obj, [In] ref Vector3 p, [In] ref Vector3 a, [In] ref Vector3 b, [In] ref Vector3 c, IntPtr result);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btVoronoiSimplexSolver_compute_points(IntPtr obj, out Vector3 p1, out Vector3 p2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btVoronoiSimplexSolver_emptySimplex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btVoronoiSimplexSolver_fullSimplex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btVoronoiSimplexSolver_getCachedBC(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btVoronoiSimplexSolver_getCachedP1(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btVoronoiSimplexSolver_getCachedP2(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btVoronoiSimplexSolver_getCachedV(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btVoronoiSimplexSolver_getCachedValidClosest(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btVoronoiSimplexSolver_getEqualVertexThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btVoronoiSimplexSolver_getLastW(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btVoronoiSimplexSolver_getNeedsUpdate(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btVoronoiSimplexSolver_getNumVertices(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btVoronoiSimplexSolver_getSimplex(IntPtr obj, Vector3[] pBuf, Vector3[] qBuf, Vector3[] yBuf);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btVoronoiSimplexSolver_getSimplexPointsP(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btVoronoiSimplexSolver_getSimplexPointsQ(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btVoronoiSimplexSolver_getSimplexVectorW(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btVoronoiSimplexSolver_inSimplex(IntPtr obj, [In] ref Vector3 w);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btVoronoiSimplexSolver_maxVertex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btVoronoiSimplexSolver_numVertices(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btVoronoiSimplexSolver_pointOutsideOfPlane(IntPtr obj, [In] ref Vector3 p, [In] ref Vector3 a, [In] ref Vector3 b, [In] ref Vector3 c, [In] ref Vector3 d);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btVoronoiSimplexSolver_reduceVertices(IntPtr obj, IntPtr usedVerts);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btVoronoiSimplexSolver_removeVertex(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btVoronoiSimplexSolver_reset(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btVoronoiSimplexSolver_setCachedBC(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btVoronoiSimplexSolver_setCachedP1(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btVoronoiSimplexSolver_setCachedP2(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btVoronoiSimplexSolver_setCachedV(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btVoronoiSimplexSolver_setCachedValidClosest(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btVoronoiSimplexSolver_setEqualVertexThreshold(IntPtr obj, float threshold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btVoronoiSimplexSolver_setLastW(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btVoronoiSimplexSolver_setNeedsUpdate(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btVoronoiSimplexSolver_setNumVertices(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btVoronoiSimplexSolver_updateClosestVectorAndPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btVoronoiSimplexSolver_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr getBulletDNAstr();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int getBulletDNAlen();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr getBulletDNAstr64();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int getBulletDNAlen64();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr GIM_BVH_DATA_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr GIM_BVH_DATA_getBound(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int GIM_BVH_DATA_getData(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void GIM_BVH_DATA_setBound(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void GIM_BVH_DATA_setData(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void GIM_BVH_DATA_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr GIM_BVH_DATA_ARRAY_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr GIM_BVH_TREE_NODE_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr GIM_BVH_TREE_NODE_getBound(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int GIM_BVH_TREE_NODE_getDataIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int GIM_BVH_TREE_NODE_getEscapeIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool GIM_BVH_TREE_NODE_isLeafNode(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void GIM_BVH_TREE_NODE_setBound(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void GIM_BVH_TREE_NODE_setDataIndex(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void GIM_BVH_TREE_NODE_setEscapeIndex(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void GIM_BVH_TREE_NODE_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr GIM_BVH_TREE_NODE_ARRAY_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr GIM_PAIR_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr GIM_PAIR_new2(IntPtr p);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr GIM_PAIR_new3(int index1, int index2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int GIM_PAIR_getIndex1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int GIM_PAIR_getIndex2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void GIM_PAIR_setIndex1(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void GIM_PAIR_setIndex2(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void GIM_PAIR_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr GIM_QUANTIZED_BVH_NODE_ARRAY_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr GIM_TRIANGLE_CONTACT_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr GIM_TRIANGLE_CONTACT_new2(IntPtr other);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void GIM_TRIANGLE_CONTACT_copy_from(IntPtr obj, IntPtr other);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float GIM_TRIANGLE_CONTACT_getPenetration_depth(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int GIM_TRIANGLE_CONTACT_getPoint_count(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr GIM_TRIANGLE_CONTACT_getPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void GIM_TRIANGLE_CONTACT_getSeparating_normal(IntPtr obj, [Out] out Vector4 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void GIM_TRIANGLE_CONTACT_merge_points(IntPtr obj, [In] ref Vector4 plane, float margin, [In] ref Vector3 points, int point_count);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void GIM_TRIANGLE_CONTACT_setPenetration_depth(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void GIM_TRIANGLE_CONTACT_setPoint_count(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void GIM_TRIANGLE_CONTACT_setSeparating_normal(IntPtr obj, [In] ref Vector4 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void GIM_TRIANGLE_CONTACT_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr HACD_HACD_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool HACD_HACD_Compute(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool HACD_HACD_Compute2(IntPtr obj, bool fullCH);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool HACD_HACD_Compute3(IntPtr obj, bool fullCH, bool exportDistPoints);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void HACD_HACD_DenormalizeData(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool HACD_HACD_GetAddExtraDistPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool HACD_HACD_GetAddFacesPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool HACD_HACD_GetAddNeighboursDistPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr HACD_HACD_GetCallBack(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool HACD_HACD_GetCH(IntPtr obj, int numCH, IntPtr points, IntPtr triangles);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern double HACD_HACD_GetCompacityWeight(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern double HACD_HACD_GetConcavity(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern double HACD_HACD_GetConnectDist(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int HACD_HACD_GetNClusters(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int HACD_HACD_GetNPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int HACD_HACD_GetNPointsCH(IntPtr obj, int numCH);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int HACD_HACD_GetNTriangles(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int HACD_HACD_GetNTrianglesCH(IntPtr obj, int numCH);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int HACD_HACD_GetNVerticesPerCH(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr HACD_HACD_GetPartition(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr HACD_HACD_GetPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern double HACD_HACD_GetScaleFactor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr HACD_HACD_GetTriangles(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern double HACD_HACD_GetVolumeWeight(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void HACD_HACD_NormalizeData(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool HACD_HACD_Save(IntPtr obj, IntPtr fileName, bool uniColor);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool HACD_HACD_Save2(IntPtr obj, IntPtr fileName, bool uniColor, long numCluster);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void HACD_HACD_SetAddExtraDistPoints(IntPtr obj, bool addExtraDistPoints);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void HACD_HACD_SetAddFacesPoints(IntPtr obj, bool addFacesPoints);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void HACD_HACD_SetAddNeighboursDistPoints(IntPtr obj, bool addNeighboursDistPoints);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void HACD_HACD_SetCallBack(IntPtr obj, IntPtr callBack);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void HACD_HACD_SetCompacityWeight(IntPtr obj, double alpha);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void HACD_HACD_SetConcavity(IntPtr obj, double concavity);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void HACD_HACD_SetConnectDist(IntPtr obj, double ccConnectDist);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void HACD_HACD_SetNClusters(IntPtr obj, int nClusters);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void HACD_HACD_SetNPoints(IntPtr obj, int nPoints);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void HACD_HACD_SetNTriangles(IntPtr obj, int nTriangles);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void HACD_HACD_SetNVerticesPerCH(IntPtr obj, int nVerticesPerCH);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void HACD_HACD_SetPoints(IntPtr obj, IntPtr points);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void HACD_HACD_SetScaleFactor(IntPtr obj, double scale);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void HACD_HACD_SetTriangles(IntPtr obj, IntPtr triangles);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void HACD_HACD_SetVolumeWeight(IntPtr obj, double beta);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void HACD_HACD_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void setGContactAddedCallback(IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void setGContactDestroyedCallback(IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void setGContactProcessedCallback(IntPtr value);
	}
}
