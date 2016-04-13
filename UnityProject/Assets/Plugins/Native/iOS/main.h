#pragma once

#include <cstring> // size_t

//#define BULLETC_DISABLE_HACD
#define BULLETC_DISABLE_IACTION_CLASSES // omits classes inheriting from btActionInterface
#define BULLETC_DISABLE_SOFTBODY_HELPERS
#define BULLETC_DISABLE_WORLD_IMPORTERS

#ifdef _MSC_VER
#define EXPORT __declspec(dllexport)
#else
#if __GNUC__ >= 4
	#define EXPORT __attribute__ ((visibility("default")))
#else
	#define EXPORT
#endif
#endif

#ifndef BT_SCALAR_H
#ifdef BT_USE_DOUBLE_PRECISION
#define btScalar double
#else
#define btScalar float
#endif
#endif

#define ALIGNED_NEW_FORCE(targetClass) new (btAlignedAlloc(sizeof(targetClass), 16)) targetClass
#define ALIGNED_FREE_FORCE(target) btAlignedFree(target)

//#if defined(BT_USE_SIMD_VECTOR3) && defined(BT_USE_SSE_IN_API) && defined(BT_USE_SSE)
#if defined(BT_USE_SIMD_VECTOR3) && defined(BT_USE_SSE)
#define ALIGNED_NEW(targetClass) ALIGNED_NEW_FORCE(targetClass)
#define ALIGNED_FREE(target) ALIGNED_FREE_FORCE(target)
#else
#define ALIGNED_NEW(targetClass) new targetClass
#define ALIGNED_FREE(target) delete target
#endif

#ifndef _BT_ACTION_INTERFACE_H
#define btActionInterface void
#endif

#ifndef BT_AXIS_SWEEP_3_H
#define bt32BitAxisSweep3 void
#define bt32BitAxisSweep3_Handle void
#define btAxisSweep3 void
#define btAxisSweep3_Handle void
#else
#define bt32BitAxisSweep3_Handle bt32BitAxisSweep3::Handle
#define btAxisSweep3_Handle btAxisSweep3::Handle
#endif

#ifndef BT_BOX_2D_BOX_2D__COLLISION_ALGORITHM_H
#define btBox2dBox2dCollisionAlgorithm void
#define btBox2dBox2dCollisionAlgorithm_CreateFunc void
#else
#define btBox2dBox2dCollisionAlgorithm_CreateFunc btBox2dBox2dCollisionAlgorithm::CreateFunc
#endif

#ifndef BT_BOX_BOX__COLLISION_ALGORITHM_H
#define btBoxBoxCollisionAlgorithm void
#define btBoxBoxCollisionAlgorithm_CreateFunc void
#else
#define btBoxBoxCollisionAlgorithm_CreateFunc btBoxBoxCollisionAlgorithm::CreateFunc
#endif

#ifndef BT_BOX_BOX_DETECTOR_H
#define btBoxBoxDetector void
#endif

#ifndef BT_BOX_COLLISION_H_INCLUDED
#define BT_BOX_BOX_TRANSFORM_CACHE void
#define btAABB void
#define eBT_PLANE_INTERSECTION_TYPE int
#endif

#ifndef BT_BROADPHASE_INTERFACE_H
#define btBroadphaseAabbCallback void
#define btBroadphaseInterface void
#define btBroadphaseRayCallback void
#endif

#ifndef BT_BROADPHASE_PROXY_H
#define btAlignedBroadphasePairArray void
#define btBroadphasePair void
#define btBroadphasePairSortPredicate void
#define btBroadphaseProxy void
#else
#define btAlignedBroadphasePairArray btAlignedObjectArray<btBroadphasePair>
#endif

#ifndef BT_BULLET_FILE_H
#define btAligendCharPtrArray void
#define btAlignedStructHandleArray void
#define bParse_btBulletFile void
#else
#define btAligendCharPtrArray btAlignedObjectArray<char*>
#define btAlignedStructHandleArray btAlignedObjectArray<bParse::bStructHandle*>
#define bParse_btBulletFile bParse::btBulletFile
#endif

#ifndef BT_BVH_TRIANGLE_MATERIAL_MESH_SHAPE_H
#define btMultimaterialTriangleMeshShape void
#endif

#ifndef BT_BVH_TRIANGLE_MESH_SHAPE_H
#define btBvhTriangleMeshShape void
#endif

#ifndef BT_CAPSULE_SHAPE_H
#define btCapsuleShape void
#define btCapsuleShapeX void
#define btCapsuleShapeZ void
#define btCapsuleShapeData void
#endif

#ifndef BT_CHARACTER_CONTROLLER_INTERFACE_H
#define btCharacterControllerInterface void
#endif

#ifndef BT_COLLISION_ALGORITHM_H
#define btCollisionAlgorithm void
#define btCollisionAlgorithmConstructionInfo void
#endif

#ifndef BT_COLLISION_CONFIGURATION
#ifndef btCollisionConfiguration
#define btCollisionConfiguration void
#endif
#endif

#ifndef BT_COLLISION_CREATE_FUNC
#define btCollisionAlgorithmCreateFunc void
#endif

#ifndef BT_COLLISION__DISPATCHER_H
#define btCollisionDispatcher void
#define btNearCallback void*
#endif

#ifndef BT_COLLISION_OBJECT_H
#define btAlignedCollisionObjectArray void
#define btAlignedConstCollisionObjectArray void
#define btCollisionObject void
#define btCollisionObjectArray void
#else
#define btAlignedCollisionObjectArray btAlignedObjectArray<btCollisionObject*>
#define btAlignedConstCollisionObjectArray btAlignedObjectArray<const btCollisionObject*>
#endif

#ifndef BT_COLLISION_OBJECT_WRAPPER_H
#define btCollisionObjectWrapper void
#endif

#ifndef BT_COLLISION_SHAPE_H
#define btCollisionShape void
#endif

#ifndef BT_COLLISION_WORLD_H
#define btCollisionWorld void
#define btCollisionWorld_AllHitsRayResultCallback void
#define btCollisionWorld_ClosestConvexResultCallback void
#define btCollisionWorld_ClosestRayResultCallback void
#define btCollisionWorld_ContactResultCallback void
#define btCollisionWorld_ConvexResultCallback void
#define btCollisionWorld_LocalConvexResult void
#define btCollisionWorld_LocalRayResult void
#define btCollisionWorld_LocalShapeInfo void
#define btCollisionWorld_RayResultCallback void
#else
#define btCollisionWorld_AllHitsRayResultCallback btCollisionWorld::AllHitsRayResultCallback
#define btCollisionWorld_ClosestConvexResultCallback btCollisionWorld::ClosestConvexResultCallback
#define btCollisionWorld_ClosestRayResultCallback btCollisionWorld::ClosestRayResultCallback
#define btCollisionWorld_ContactResultCallback btCollisionWorld::ContactResultCallback
#define btCollisionWorld_ConvexResultCallback btCollisionWorld::ConvexResultCallback
#define btCollisionWorld_LocalConvexResult btCollisionWorld::LocalConvexResult
#define btCollisionWorld_LocalRayResult btCollisionWorld::LocalRayResult
#define btCollisionWorld_LocalShapeInfo btCollisionWorld::LocalShapeInfo
#define btCollisionWorld_RayResultCallback btCollisionWorld::RayResultCallback
#endif

#ifndef BT_COMPOUND_COLLISION_ALGORITHM_H
#define btCompoundCollisionAlgorithm void
#define btCompoundCollisionAlgorithm_CreateFunc void
#define btCompoundCollisionAlgorithm_SwappedCreateFunc void
#else
#define btCompoundCollisionAlgorithm_CreateFunc btCompoundCollisionAlgorithm::CreateFunc
#define btCompoundCollisionAlgorithm_SwappedCreateFunc btCompoundCollisionAlgorithm::SwappedCreateFunc
#endif

#ifndef BT_COMPOUND_COMPOUND_COLLISION_ALGORITHM_H
#define btCompoundCompoundCollisionAlgorithm void
#define btCompoundCompoundCollisionAlgorithm_CreateFunc void
#define btCompoundCompoundCollisionAlgorithm_SwappedCreateFunc void
#else
#define btCompoundCompoundCollisionAlgorithm_CreateFunc btCompoundCompoundCollisionAlgorithm::CreateFunc
#define btCompoundCompoundCollisionAlgorithm_SwappedCreateFunc btCompoundCompoundCollisionAlgorithm::SwappedCreateFunc
#endif

#ifndef BT_COMPOUND_SHAPE_H
#define btCompoundShape void
#define btCompoundShapeChild void
#endif

#ifndef BT_CONCAVE_SHAPE_H
#define btConcaveShape void
#define PHY_ScalarType int
#endif

#ifndef BT_CONE_MINKOWSKI_H
#define btConeShape void
#define btConeShapeX void
#define btConeShapeZ void
#endif

#ifndef BT_CONETWISTCONSTRAINT_H
#define btConeTwistConstraint void
#endif

#ifndef BT_CONSTRAINT_SOLVER_H
#define btConstraintSolver void
#define btConstraintSolverType int
#endif

#ifndef BT_CONTACT_CONSTRAINT_H
#define btContactConstraint void
#endif

#ifndef BT_CONTACT_SOLVER_INFO
#define btContactSolverInfo void
#define btContactSolverInfoData void
#endif

#ifndef BT_CONTINUOUS_COLLISION_CONVEX_CAST_H
#define btContinuousConvexCollision void
#endif

#ifndef BT_CONVEX_2D_SHAPE_H
#define btConvex2dShape void
#endif

#ifndef BT_CONVEX_2D_CONVEX_2D_ALGORITHM_H
#define btConvex2dConvex2dAlgorithm void
#define btConvex2dConvex2dAlgorithm_CreateFunc void
#else
#define btConvex2dConvex2dAlgorithm_CreateFunc btConvex2dConvex2dAlgorithm::CreateFunc
#endif

#ifndef BT_CONVEX_CAST_H
#define btConvexCast void
#define btConvexCast_CastResult void
#else
#define btConvexCast_CastResult btConvexCast::CastResult
#endif

#ifndef BT_CONVEX_CONCAVE_COLLISION_ALGORITHM_H
#define btConvexConcaveCollisionAlgorithm void
#define btConvexConcaveCollisionAlgorithm_CreateFunc void
#define btConvexConcaveCollisionAlgorithm_SwappedCreateFunc void
#define btConvexTriangleCallback void
#else
#define btConvexConcaveCollisionAlgorithm_CreateFunc btConvexConcaveCollisionAlgorithm::CreateFunc
#define btConvexConcaveCollisionAlgorithm_SwappedCreateFunc btConvexConcaveCollisionAlgorithm::SwappedCreateFunc
#endif

#ifndef BT_CONVEX_CONVEX_ALGORITHM_H
#define btConvexConvexAlgorithm void
#define btConvexConvexAlgorithm_CreateFunc void
#else
#define btConvexConvexAlgorithm_CreateFunc btConvexConvexAlgorithm::CreateFunc
#endif

#ifndef BT_CONVEX_HULL_SHAPE_H
#define btConvexHullShape void
#endif

#ifndef BT_CONVEX_INTERNAL_SHAPE_H
#define btConvexInternalShape void
#define btConvexInternalAabbCachingShape void
#endif

#ifndef BT_CONVEX_PENETRATION_DEPTH_H
#define btConvexPenetrationDepthSolver void
#endif

#ifndef BT_CONVEX_PLANE_COLLISION_ALGORITHM_H
#define btConvexPlaneCollisionAlgorithm void
#define btConvexPlaneCollisionAlgorithm_CreateFunc void
#else
#define btConvexPlaneCollisionAlgorithm_CreateFunc btConvexPlaneCollisionAlgorithm::CreateFunc
#endif

#ifndef BT_CONVEX_POINT_CLOUD_SHAPE_H
#define btConvexPointCloudShape void
#endif

#ifndef BT_CONVEX_SHAPE_INTERFACE1
#define btConvexShape void
#endif

#ifndef BT_CONVEX_TRIANGLEMESH_SHAPE_H
#define btConvexTriangleMeshShape void
#endif

#ifndef BT_CYLINDER_MINKOWSKI_H
#define btCylinderShape void
#define btCylinderShapeX void
#define btCylinderShapeZ void
#endif

#ifndef BT_DANTZIG_SOLVER_H
#define btDantzigSolver void
#endif

#ifndef BT_DEFAULT_COLLISION_CONFIGURATION
#define btDefaultCollisionConfiguration void
#define btDefaultCollisionConstructionInfo void
#endif

#ifndef BT_DBVT_BROADPHASE_H
#define btDbvtBroadphase void
#define btDbvtProxy void
#endif

#ifndef BT_DEFAULT_MOTION_STATE_H
#define btDefaultMotionState void
#endif

#ifndef BT_DISCRETE_COLLISION_DETECTOR1_INTERFACE_H
#define btDiscreteCollisionDetectorInterface void
#define btDiscreteCollisionDetectorInterface_ClosestPointInput void
#define btDiscreteCollisionDetectorInterface_Result void
#define btStorageResult void
#else
#define btDiscreteCollisionDetectorInterface_ClosestPointInput btDiscreteCollisionDetectorInterface::ClosestPointInput
#define btDiscreteCollisionDetectorInterface_Result btDiscreteCollisionDetectorInterface::Result
#endif

#ifndef BT_DISCRETE_DYNAMICS_WORLD_H
#define btDiscreteDynamicsWorld void
#endif

#ifndef BT_DISPATCHER_H
#define btDispatcher void
#define btDispatcherInfo void
#endif

#ifndef BT_DYNAMIC_BOUNDING_VOLUME_TREE_H
#define btDbvt void
#define btDbvtAabbMm void
#define btDbvtNode void
#define btDbvtVolume void
#define btDbvt_IClone void
#define btDbvt_ICollide void
#define btDbvt_IWriter void
#define btDbvt_sStkCLN void
#define btDbvt_sStkNN void
#define btDbvt_sStkNP void
#define btDbvt_sStkNPS void
#define btAlignedDbvtNodeArray void
#define btAlignedStkNNArray void
#define btAlignedStkNpsArray void
#else
#define btDbvt_IClone btDbvt::IClone
#define btDbvt_ICollide btDbvt::ICollide
#define btDbvt_IWriter btDbvt::IWriter
#define btDbvt_sStkCLN btDbvt::sStkCLN
#define btDbvt_sStkNN btDbvt::sStkNN
#define btDbvt_sStkNP btDbvt::sStkNP
#define btDbvt_sStkNPS btDbvt::sStkNPS
#define btAlignedDbvtNodeArray btAlignedObjectArray<const btDbvtNode*>
#define btAlignedStkNNArray btAlignedObjectArray<btDbvt_sStkNN>
#define btAlignedStkNpsArray btAlignedObjectArray<btDbvt_sStkNPS>
#endif

#ifndef BT_DYNAMICS_WORLD_H
#define btDynamicsWorld void
#define btDynamicsWorldType int
#define btInternalTickCallback void*
#endif

#ifndef BT_EMPTY_ALGORITH
#define btEmptyAlgorithm void
#define btEmptyAlgorithm_CreateFunc void
#else
#define btEmptyAlgorithm_CreateFunc btEmptyAlgorithm::CreateFunc
#endif

#ifndef BT_EMPTY_SHAPE_H
#define btEmptyShape void
#endif

#ifndef BT_FEATHERSTONE_LINK_COLLIDER_H
#define btMultiBodyLinkCollider void
#endif

#ifndef BT_FIXED_CONSTRAINT_H
#define btFixedConstraint void
#endif

#ifndef BT_GEAR_CONSTRAINT_H
#define btGearConstraint void
#endif

#ifndef BT_GENERIC_6DOF_CONSTRAINT_H
#define btGeneric6DofConstraint void
#define btRotationalLimitMotor void
#define btTranslationalLimitMotor void
#endif

#ifndef BT_GENERIC_6DOF_CONSTRAINT2_H
#define btGeneric6DofSpring2Constraint void
#define btRotationalLimitMotor2 void
#define btTranslationalLimitMotor2 void
#define RotateOrder int
#endif

#ifndef BT_GENERIC_6DOF_SPRING_CONSTRAINT_H
#define btGeneric6DofSpringConstraint void
#endif

#ifndef BT_GHOST_OBJECT_H
#define btGhostObject void
#define btGhostPairCallback void
#define btPairCachingGhostObject void
#endif

#ifndef BT_GIMPACT_BVH_CONCAVE_COLLISION_ALGORITHM_H
#define btGImpactCollisionAlgorithm void
#define btGImpactCollisionAlgorithm_CreateFunc void
#else
#define btGImpactCollisionAlgorithm_CreateFunc btGImpactCollisionAlgorithm::CreateFunc
#endif

#ifndef BT_GJK_CONVEX_CAST_H
#define btGjkConvexCast void
#endif

#ifndef BT_GJP_EPA_PENETRATION_DEPTH_H
#define btGjkEpaPenetrationDepthSolver void
#endif

#ifndef BT_GJK_PAIR_DETECTOR_H
#define btGjkPairDetector void
#endif

#ifndef BT_HEIGHTFIELD_TERRAIN_SHAPE_H
#define btHeightfieldTerrainShape void
#endif

#ifndef BT_HINGE2_CONSTRAINT_H
#define btHinge2Constraint void
#endif

#ifndef BT_HINGECONSTRAINT_H
#define btHingeConstraint void
#define btHingeAccumulatedAngleConstraint void
#endif

#ifndef BT_IDEBUG_DRAW__H
#define btIDebugDraw void
#endif

#ifndef BT_KINEMATIC_CHARACTER_CONTROLLER_H
#define btKinematicCharacterController void
#endif

#ifndef BT_MANIFOLD_CONTACT_POINT_H
#define btConstraintRow void
#define btManifoldPoint void
#endif

#ifndef BT_MANIFOLD_RESULT_H
#define btManifoldResult void
#define ContactAddedCallback void*
#endif

#ifndef BT_MATERIAL_H
#define btMaterial void
#endif

#ifndef	BT_MATRIX3x3_H
#define btMatrix3x3 void
#define btAlignedMatrix3x3Array void
#else
#define btAlignedMatrix3x3Array btAlignedObjectArray<btMatrix3x3>
#endif

#ifndef BT_MINKOWSKI_PENETRATION_DEPTH_SOLVER_H
#define btMinkowskiPenetrationDepthSolver void
#endif

#ifndef BT_MLCP_SOLVER_H
#define btMLCPSolver void
#endif

#ifndef BT_MLCP_SOLVER_INTERFACE_H
#define btMLCPSolverInterface void
#endif

#ifndef BT_MINKOWSKI_SUM_SHAPE_H
#define btMinkowskiSumShape void
#endif

#ifndef BT_MOTIONSTATE_H
#define btMotionState void
#endif

#ifndef BT_MULTI_SPHERE_MINKOWSKI_H
#define btMultiSphereShape void
#endif

#ifndef BT_MULTIBODY_H
#define btMultiBody void
#endif

#ifndef BT_MULTIBODY_CONSTRAINT_H
#define btMultiBodyConstraint void
#define btMultiBodyJacobianData void
#endif

#ifndef BT_MULTIBODY_CONSTRAINT_SOLVER_H
#define btMultiBodyConstraintSolver void
#endif

#ifndef BT_MULTIBODY_DYNAMICS_WORLD_H
#define btMultiBodyDynamicsWorld void
#endif

#ifndef BT_MULTIBODY_JOINT_LIMIT_CONSTRAINT_H
#define btMultiBodyJointLimitConstraint void
#endif

#ifndef BT_MULTIBODY_JOINT_MOTOR_H
#define btMultiBodyJointMotor void
#endif

#ifndef BT_MULTIBODY_LINK_H
#define btMultiBodyJointFeedback void
#define btMultibodyLink void
#define btSpatialMotionVector void
#define btMultibodyLink_eFeatherstoneJointType int
#else
#define btMultibodyLink_eFeatherstoneJointType btMultibodyLink::eFeatherstoneJointType
#endif

#ifndef BT_MULTIBODY_POINT2POINT_H
#define btMultiBodyPoint2Point void
#endif

#ifndef BT_MULTIBODY_SOLVER_CONSTRAINT_H
#define btMultiBodyConstraintArray void
#define btMultiBodySolverConstraint void
#endif

#ifndef BT_MULTIMATERIAL_TRIANGLE_INDEX_VERTEX_ARRAY_H
#define btMaterialProperties void
#define btTriangleIndexVertexMaterialArray void
#endif

#ifndef BT_NNCG_CONSTRAINT_SOLVER_H
#define btNNCGConstraintSolver void
#endif

#ifndef BT_OBB_BOX_2D_SHAPE_H
#define btBox2dShape void
#endif

#ifndef BT_OBB_BOX_MINKOWSKI_H
#define btBoxShape void
#endif

#ifndef BT_OBB_TRIANGLE_MINKOWSKI_H
#define btTriangleShape void
#endif

#ifndef BT_OBJECT_ARRAY__
#define btAlignedBoolArray void
#define btAlignedIntArray void
#define btAlignedScalarArray void
#else
#define btAlignedBoolArray btAlignedObjectArray<bool>
#define btAlignedIntArray btAlignedObjectArray<int>
#define btAlignedScalarArray btAlignedObjectArray<btScalar>
#endif

#ifndef BT_OPTIMIZED_BVH_H
#define btOptimizedBvh void
#endif

#ifndef BT_OVERLAPPING_PAIR_CACHE_H
#define btHashedOverlappingPairCache void
#define btNullPairCache void
#define btOverlapCallback void
#define btOverlapFilterCallback void
#define btOverlappingPairCache void
#define btSortedOverlappingPairCache void
#endif

#ifndef OVERLAPPING_PAIR_CALLBACK_H
#define btOverlappingPairCallback void
#endif

#ifndef __BT_PARALLEL_CONSTRAINT_SOLVER_H
#define btParallelConstraintSolver void
#endif

#ifndef BT_PERSISTENT_MANIFOLD_H
#define btAlignedManifoldArray void
#define btPersistentManifold void
#define ContactDestroyedCallback void*
#define ContactProcessedCallback void*
#else
#define btAlignedManifoldArray btAlignedObjectArray<btPersistentManifold*>
#endif

#ifndef BT_POINT_COLLECTOR_H
#define btPointCollector void
#endif

#ifndef BT_POINT2POINTCONSTRAINT_H
#define btConstraintSetting void
#define btPoint2PointConstraint void
#endif

#ifndef _BT_POOL_ALLOCATOR_H
#define btPoolAllocator void
#endif

#ifndef BT_POLYHEDRAL_CONVEX_SHAPE_H
#define btPolyhedralConvexAabbCachingShape void
#define btPolyhedralConvexShape void
#endif

#ifndef _BT_POLYHEDRAL_FEATURES_H
#define btConvexPolyhedron void
#define btFace void
#define btAlignedFaceArray void
#else
#define btAlignedFaceArray btAlignedObjectArray<btFace>
#endif

#ifndef BT_QUANTIZED_BVH_H
#define btNodeOverlapCallback void
#define btOptimizedBvhNode void
#define btQuantizedBvh void
#define btQuantizedBvh_btTraversalMode int
#define btQuantizedBvhDoubleData void
#define btQuantizedBvhFloatData void
#define btQuantizedBvhNode void
#define BvhSubtreeInfoArray void
#define QuantizedNodeArray void
#else
#define btQuantizedBvh_btTraversalMode btQuantizedBvh::btTraversalMode
#endif

#ifndef BT_SIMD__QUATERNION_H_
#define btQuaternion void
#define btAlignedQuaternionArray void
#else
#define btAlignedQuaternionArray btAlignedObjectArray<btQuaternion>
#endif

#ifndef BT_RAYCASTVEHICLE_H
#define btDefaultVehicleRaycaster void
#define btRaycastVehicle void
#define btRaycastVehicle_btVehicleTuning void
#else
#define btRaycastVehicle_btVehicleTuning btRaycastVehicle::btVehicleTuning
#endif

#ifndef BT_RIGIDBODY_H
#define btRigidBody void
#define btRigidBody_btRigidBodyConstructionInfo void
#else
#define btRigidBody_btRigidBodyConstructionInfo btRigidBody::btRigidBodyConstructionInfo
#endif

#ifndef BT_SCALED_BVH_TRIANGLE_MESH_SHAPE_H
#define btScaledBvhTriangleMeshShape void
#endif

#ifndef BT_SEQUENTIAL_IMPULSE_CONSTRAINT_SOLVER_H
#define btSequentialImpulseConstraintSolver void
#endif

#ifndef BT_SERIALIZER_H
#define btChunk void
#define btDefaultSerializer void
#define btPointerUid void
#define btSerializer void
#endif

#ifndef BT_SHAPE_HULL_H
#define btShapeHull void
#endif

#ifndef BT_SIMPLEX_1TO4_SHAPE
#define btBU_Simplex1to4 void
#endif

#ifndef BT_SLIDER_CONSTRAINT_H
#define btSliderConstraint void
#endif

#ifndef BT_SIMULATION_ISLAND_MANAGER_H
#define btSimulationIslandManager void
#define btSimulationIslandManager_IslandCallback void
#else
#define btSimulationIslandManager_IslandCallback btSimulationIslandManager::IslandCallback
#endif

#ifndef _BT_SOFT_BODY_H
#define btAlignedSoftBodyArray void
#define btAlignedSoftBodyAnchorArray void
#define btAlignedSoftBodyClusterArray void
#define btAlignedSoftBodyFaceArray void
#define btAlignedSoftBodyJointArray void
#define btAlignedSoftBodyLinkArray void
#define btAlignedSoftBodyMaterialArray void
#define btAlignedSoftBodyNodeArray void
#define btAlignedSoftBodyNodePtrArray void
#define btAlignedSoftBodyNoteArray void
#define btAlignedSoftBodyPSolverArray void
#define btAlignedSoftBodyRContactArray void
#define btAlignedSoftBodySContactArray void
#define btAlignedSoftBodyTetraArray void
#define btAlignedSoftBodyVSolverArray void
#define btSoftBodyNodePtrArray void
#define btSoftBody void
#define btSoftBodyWorldInfo void
#define btSoftBody_eAeroModel int
#define btSoftBody_eFeature int
#define btSoftBody_ePSolver int
#define btSoftBody_eSolverPresets int
#define btSoftBody_eVSolver int
#define btSoftBody_fCollision int
#define btSoftBody_fMaterial int
#define btSoftBody_AJoint void
#define btSoftBody_AJoint_IControl void
#define btSoftBody_AJoint_Specs void
#define btSoftBody_Anchor void
#define btSoftBody_Body void
#define btSoftBody_CJoint void
#define btSoftBody_Cluster void
#define btSoftBody_Config void
#define btSoftBody_Element void
#define btSoftBody_Face void
#define btSoftBody_Feature void
#define btSoftBody_ImplicitFn void
#define btSoftBody_Impulse void
#define btSoftBody_Joint void
#define btSoftBody_Joint_eType void
#define btSoftBody_Joint_Specs void
#define btSoftBody_Link void
#define btSoftBody_LJoint void
#define btSoftBody_LJoint_Specs void
#define btSoftBody_Material void
#define btSoftBody_Node void
#define btSoftBody_Note void
#define btSoftBody_Pose void
#define btSoftBody_RayFromToCaster void
#define btSoftBody_RContact void
#define btSoftBody_SContact void
#define btSoftBody_sCti void
#define btSoftBody_sMedium void
#define btSoftBody_SolverState void
#define btSoftBody_sRayCast void
#define btSoftBody_Tetra void
#else
#define btAlignedSoftBodyArray btSoftBody::tSoftBodyArray
#define btAlignedSoftBodyAnchorArray btSoftBody::tAnchorArray
#define btAlignedSoftBodyClusterArray btSoftBody::tClusterArray
#define btAlignedSoftBodyRContactArray btSoftBody::tRContactArray
#define btAlignedSoftBodyFaceArray btSoftBody::tFaceArray
#define btAlignedSoftBodyJointArray btSoftBody::tJointArray
#define btAlignedSoftBodyLinkArray btSoftBody::tLinkArray
#define btAlignedSoftBodyMaterialArray btSoftBody::tMaterialArray
#define btAlignedSoftBodyNodeArray btSoftBody::tNodeArray
#define btAlignedSoftBodyNodePtrArray btAlignedObjectArray<btSoftBody::Node*>
#define btAlignedSoftBodyNoteArray btSoftBody::tNoteArray
#define btAlignedSoftBodyPSolverArray btSoftBody::tPSolverArray
#define btAlignedSoftBodyRContactArray btSoftBody::tRContactArray
#define btAlignedSoftBodySContactArray btSoftBody::tSContactArray
#define btAlignedSoftBodyTetraArray btSoftBody::tTetraArray
#define btAlignedSoftBodyVSolverArray btSoftBody::tVSolverArray
#define btSoftBodyNodePtrArray btSoftBody::Node*
#define btSoftBody_eAeroModel btSoftBody::eAeroModel::_
#define btSoftBody_eFeature btSoftBody::eFeature::_
#define btSoftBody_ePSolver btSoftBody::ePSolver::_
#define btSoftBody_eSolverPresets btSoftBody::eSolverPresets::_
#define btSoftBody_eVSolver btSoftBody::eVSolver::_
#define btSoftBody_fCollision btSoftBody::fCollision
#define btSoftBody_fMaterial btSoftBody::fMaterial
#define btSoftBody_AJoint btSoftBody::AJoint
#define btSoftBody_AJoint_IControl btSoftBody::AJoint::IControl
#define btSoftBody_AJoint_Specs btSoftBody::AJoint::Specs
#define btSoftBody_Anchor btSoftBody::Anchor
#define btSoftBody_Body btSoftBody::Body
#define btSoftBody_CJoint btSoftBody::CJoint
#define btSoftBody_Cluster btSoftBody::Cluster
#define btSoftBody_Config btSoftBody::Config
#define btSoftBody_Element btSoftBody::Element
#define btSoftBody_Face btSoftBody::Face
#define btSoftBody_Feature btSoftBody::Feature
#define btSoftBody_ImplicitFn btSoftBody::ImplicitFn
#define btSoftBody_Impulse btSoftBody::Impulse
#define btSoftBody_Joint btSoftBody::Joint
#define btSoftBody_Joint_eType btSoftBody::Joint::eType::_
#define btSoftBody_Joint_Specs btSoftBody::Joint::Specs
#define btSoftBody_Link btSoftBody::Link
#define btSoftBody_LJoint btSoftBody::LJoint
#define btSoftBody_LJoint_Specs btSoftBody::LJoint::Specs
#define btSoftBody_Material btSoftBody::Material
#define btSoftBody_Node btSoftBody::Node
#define btSoftBody_Note btSoftBody::Note
#define btSoftBody_Pose btSoftBody::Pose
#define btSoftBody_RayFromToCaster btSoftBody::RayFromToCaster
#define btSoftBody_RContact btSoftBody::RContact
#define btSoftBody_SContact btSoftBody::SContact
#define btSoftBody_sCti btSoftBody::sCti
#define btSoftBody_sMedium btSoftBody::sMedium
#define btSoftBody_SolverState btSoftBody::SolverState
#define btSoftBody_sRayCast btSoftBody::sRayCast
#define btSoftBody_Tetra btSoftBody::Tetra
#endif

#ifndef BT_SOFT_BODY_CONCAVE_COLLISION_ALGORITHM_H
#define btSoftBodyConcaveCollisionAlgorithm void
#define btSoftBodyConcaveCollisionAlgorithm_CreateFunc void
#define btSoftBodyConcaveCollisionAlgorithm_SwappedCreateFunc void
#define btSoftBodyTriangleCallback void
#define btTriIndex void
#else
#define btSoftBodyConcaveCollisionAlgorithm_CreateFunc btSoftBodyConcaveCollisionAlgorithm::CreateFunc
#define btSoftBodyConcaveCollisionAlgorithm_SwappedCreateFunc btSoftBodyConcaveCollisionAlgorithm::SwappedCreateFunc
#endif

#ifndef BT_SOFT_BODY_DEFAULT_SOLVER_H
#define btDefaultSoftBodySolver void
#endif

#ifndef BT_SOFT_BODY_HELPERS_H
#define fDrawFlags void
#endif

#ifndef BT_SOFT_BODY_SOLVERS_H
#define btSoftBodySolver void
#define btSoftBodySolverOutput void
#endif

#ifndef BT_SOFT_RIGID_COLLISION_ALGORITHM_H
#define btSoftRigidCollisionAlgorithm void
#define btSoftRigidCollisionAlgorithm_CreateFunc void
#else
#define btSoftRigidCollisionAlgorithm_CreateFunc btSoftRigidCollisionAlgorithm::CreateFunc
#endif

#ifndef BT_SOFT_SOFT_COLLISION_ALGORITHM_H
#define btSoftSoftCollisionAlgorithm void
#define btSoftSoftCollisionAlgorithm_CreateFunc void
#else
#define btSoftSoftCollisionAlgorithm_CreateFunc btSoftSoftCollisionAlgorithm::CreateFunc
#endif

#ifndef BT_SOFT_RIGID_DYNAMICS_WORLD_H
#define btSoftBodyArray void
#define btSoftRigidDynamicsWorld void
#endif

#ifndef BT_SOFTBODY_RIGIDBODY_COLLISION_CONFIGURATION
#define btSoftBodyRigidBodyCollisionConfiguration void
#endif

#ifndef BT_SOFT_BODY_SOLVER_VERTEX_BUFFER_H
#define btVertexBufferDescriptor void
#endif

#ifndef BT_SOLVER_BODY_H
#define btSolverBody void
#endif

#ifndef BT_SOLVER_CONSTRAINT_H
#define btConstraintArray void
#endif

#ifndef BT_SPARSE_SDF_H
#define btSparseSdf3 void
#else
#define btSparseSdf3 btSparseSdf<3>
#endif

#ifndef BT_SPHERE_BOX_COLLISION_ALGORITHM_H
#define btSphereBoxCollisionAlgorithm void
#define btSphereBoxCollisionAlgorithm_CreateFunc void
#else
#define btSphereBoxCollisionAlgorithm_CreateFunc btSphereBoxCollisionAlgorithm::CreateFunc
#endif

#ifndef BT_SPHERE_MINKOWSKI_H
#define btSphereShape void
#endif

#ifndef BT_SPHERE_SPHERE_COLLISION_ALGORITHM_H
#define btSphereSphereCollisionAlgorithm void
#define btSphereSphereCollisionAlgorithm_CreateFunc void
#else
#define btSphereSphereCollisionAlgorithm_CreateFunc btSphereSphereCollisionAlgorithm::CreateFunc
#endif

#ifndef BT_SPHERE_TRIANGLE_COLLISION_ALGORITHM_H
#define btSphereTriangleCollisionAlgorithm void
#define btSphereTriangleCollisionAlgorithm_CreateFunc void
#else
#define btSphereTriangleCollisionAlgorithm_CreateFunc btSphereTriangleCollisionAlgorithm::CreateFunc
#endif

#ifndef BT_SPU_COLLISION_TASK_PROCESS_H
#define SpuCollisionTaskProcess void
#define SpuGatherAndProcessWorkUnitInput void
#endif

#ifndef BT_SPU_GATHERING_COLLISION__DISPATCHER_H
#define SpuGatheringCollisionDispatcher void
#endif

#ifndef BT_STATIC_PLANE_SHAPE_H
#define btStaticPlaneShape void
#endif

#ifndef BT_STRIDING_MESHINTERFACE_H
#define btStridingMeshInterface void
#define btStridingMeshInterfaceData void
#endif

#ifndef BT_THREAD_SUPPORT_INTERFACE_H
#define btBarrier void
#define btCriticalSection void
#define btThreadSupportInterface void
#endif

#ifndef BT_TRANSFORM_H
#define btTransform void
#endif

#ifndef BT_TRANSFORM_UTIL_H
#define btConvexSeparatingDistanceUtil void
#endif

#ifndef BT_TRIANGLE_BUFFER_H
#define btTriangle void
#define btTriangleBuffer void
#endif

#ifndef BT_TRIANGLE_CALLBACK_H
#define btInternalTriangleIndexCallback void
#define btTriangleCallback void
#endif

#ifndef BT_TRIANGLE_INDEX_VERTEX_ARRAY_H
#define btAlignedIndexedMeshArray void
#define btIndexedMesh void
#define btTriangleIndexVertexArray void
#define IndexedMeshArray void
#else
#define btAlignedIndexedMeshArray btAlignedObjectArray<btIndexedMesh>
#endif

#ifndef _BT_TRIANGLE_INFO_MAP_H
#define btTriangleInfo void
#define btTriangleInfoMap void
#endif

#ifndef BT_TRIANGLE_MESH_H
#define btTriangleMesh void
#endif

#ifndef BT_TRIANGLE_MESH_SHAPE_H
#define btTriangleMeshShape void
#endif

#ifndef BT_TYPED_CONSTRAINT_H
#define btAngularLimit void
#define btJointFeedback void
#define btTypedConstraint void
#define btTypedConstraint_btConstraintInfo1 void
#define btTypedConstraint_btConstraintInfo2 void
#define btTypedConstraintType int
#else
#define btTypedConstraint_btConstraintInfo1 btTypedConstraint::btConstraintInfo1
#define btTypedConstraint_btConstraintInfo2 btTypedConstraint::btConstraintInfo2
#endif

#ifndef BT_UNIFORM_SCALING_SHAPE_H
#define btUniformScalingShape void
#endif

#ifndef BT_UNION_FIND_H
#define btElement void
#define btUnionFind void
#endif

#ifndef BT_UNIVERSAL_CONSTRAINT_H
#define btUniversalConstraint void
#endif

#ifndef BT_VECTOR3_H
#define btAlignedVector3Array void
#define btVector3 void
#define btVector4 void
#else
#define btAlignedVector3Array btAlignedObjectArray<btVector3>
#endif

#ifndef BT_VEHICLE_RAYCASTER_H
#define btVehicleRaycaster void
#define btVehicleRaycaster_btVehicleRaycasterResult void
#else
#define btVehicleRaycaster_btVehicleRaycasterResult btVehicleRaycaster::btVehicleRaycasterResult
#endif

#ifndef BT_VORONOI_SIMPLEX_SOLVER_H
#define btSubSimplexClosestResult void
#define btUsageBitfield void
#define btVoronoiSimplexSolver void
#endif

#ifndef BT_WHEEL_INFO_H
#define btAlignedWheelInfoArray void
#define btWheelInfo void
#define btWheelInfo_RaycastInfo void
#define btWheelInfoConstructionInfo void
#else
#define btAlignedWheelInfoArray btAlignedObjectArray<btWheelInfo>
#define btWheelInfo_RaycastInfo btWheelInfo::RaycastInfo
#endif

#ifndef BT_WIN32_THREAD_SUPPORT_H
#define Win32ThreadSupport void
#define Win32ThreadSupport_Win32ThreadConstructionInfo void
typedef void (*Win32ThreadFunc)(void* userPtr,void* lsMemory);
typedef void* (*Win32lsMemorySetupFunc)();
#else
#define Win32ThreadSupport_Win32ThreadConstructionInfo Win32ThreadSupport::Win32ThreadConstructionInfo
#endif

#ifndef BT_WORLD_IMPORTER_H
#define btWorldImporter void
#endif

#ifndef BULLET_WORLD_IMPORTER_H
#define btBulletWorldImporter void
#endif

#ifndef BT_BULLET_XML_WORLD_IMPORTER_H
#define btBulletXmlWorldImporter void
#endif

#ifndef GIM_BOX_SET_H_INCLUDED
#define btBvhTree void
#define btPairSet void
#define GIM_BVH_DATA void
#define GIM_BVH_DATA_ARRAY void
#define GIM_BVH_TREE_NODE void
#define GIM_BVH_TREE_NODE_ARRAY void
#define GIM_PAIR void
#define btGImpactBvh void
#endif

#ifndef GIM_QUANTIZED_SET_H_INCLUDED
#define BT_QUANTIZED_BVH_NODE void
#define GIM_QUANTIZED_BVH_NODE_ARRAY void
#define btGImpactQuantizedBvh void
#define btQuantizedBvhTree void
#endif

#ifndef GIMPACT_SHAPE_H
#define btGImpactBoxSet void
#define btGImpactCompoundShape void
#define btGImpactCompoundShape_CompoundPrimitiveManager void
#define btGImpactMeshShapePart_TrimeshPrimitiveManager void
#define btGImpactMeshShape void
#define btGImpactMeshShapePart void
#define btGImpactShapeInterface void
#define btTetrahedronShapeEx void
#define eGIMPACT_SHAPE_TYPE int
#else
#define btGImpactCompoundShape_CompoundPrimitiveManager btGImpactCompoundShape::CompoundPrimitiveManager
#define btGImpactMeshShapePart_TrimeshPrimitiveManager btGImpactMeshShapePart::TrimeshPrimitiveManager
#endif

#ifndef GIMPACT_TRIANGLE_SHAPE_EX_H
#define btPrimitiveTriangle void
#define btTriangleShapeEx void
#define GIM_TRIANGLE_CONTACT void
#endif

#ifndef GIM_BOX_SET_H_INCLUDED
#define btPrimitiveManagerBase void
#endif

#ifndef POLARDECOMPOSITION_H
#define btPolarDecomposition void
#endif

#ifndef HACD_HACD_H
#define HACD_HACD void
#define HACD_Vec3_long long
#define HACD_Vec3_Real void
typedef bool (*HACD_CallBackFunction)(const char *, double, double, size_t);
#else
#define HACD_HACD HACD::HACD
#define HACD_Vec3_long HACD::Vec3<long>
#define HACD_Vec3_Real HACD::Vec3<HACD::Real>
#define HACD_CallBackFunction HACD::CallBackFunction
#endif

#ifndef SPU_GATHERING_COLLISION_TASK_H
#if defined(_WIN64)
	typedef unsigned __int64 ppu_address_t;
#elif defined(__LP64__) || defined(__x86_64__)
	typedef unsigned long int ppu_address_t;
#else
	typedef unsigned int ppu_address_t;
#endif
#define CollisionTask_LocalStoreMemory void
#define SpuGatherAndProcessPairsTaskDesc void
#endif
