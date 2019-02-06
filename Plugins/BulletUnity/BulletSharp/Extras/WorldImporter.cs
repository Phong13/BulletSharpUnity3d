using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using BulletSharp.Math;

namespace BulletSharp
{
	public class WorldImporter
	{
        private DynamicsWorld _dynamicsWorld;

        protected List<OptimizedBvh> _allocatedBvhs = new List<OptimizedBvh>();
        protected List<CollisionShape> _allocatedCollisionShapes = new List<CollisionShape>();
        protected List<TypedConstraint> _allocatedConstraints = new List<TypedConstraint>();
        protected List<RigidBody> _allocatedRigidBodies = new List<RigidBody>();
        protected List<TriangleIndexVertexArray> _allocatedTriangleIndexArrays = new List<TriangleIndexVertexArray>();
        protected List<TriangleInfoMap> _allocatedTriangleInfoMaps = new List<TriangleInfoMap>();

        protected Dictionary<byte[], CollisionObject> _bodyMap = new Dictionary<byte[], CollisionObject>();
        protected Dictionary<long, OptimizedBvh> _bvhMap = new Dictionary<long, OptimizedBvh>();
        protected Dictionary<string, RigidBody> _nameBodyMap = new Dictionary<string, RigidBody>();
        protected Dictionary<string, TypedConstraint> _nameConstraintMap = new Dictionary<string, TypedConstraint>();
        protected Dictionary<string, CollisionShape> _nameShapeMap = new Dictionary<string, CollisionShape>();
        protected Dictionary<Object, string> _objectNameMap = new Dictionary<Object, string>();
        protected Dictionary<long, CollisionShape> _shapeMap = new Dictionary<long, CollisionShape>();
        protected FileVerboseMode _verboseMode;

		public WorldImporter(DynamicsWorld world)
		{
            _dynamicsWorld = world;
		}

        protected CollisionShape ConvertCollisionShape(byte[] shapeData, Dictionary<long, byte[]> libPointers)
        {
            CollisionShape shape = null;
            MemoryStream stream = new MemoryStream(shapeData, false);
            BulletReader reader = new BulletReader(stream);

            BroadphaseNativeType type = (BroadphaseNativeType) reader.ReadInt32(CollisionShapeFloatData.Offset("ShapeType"));
            stream.Position = Marshal.SizeOf(typeof(CollisionShapeFloatData));
            switch (type)
            {
                case BroadphaseNativeType.StaticPlaneShape:
                {
                    Vector3 localScaling = reader.ReadVector3();
                    Vector3 planeNormal = reader.ReadVector3();
                    float planeConstant = reader.ReadSingle();
                    shape = CreatePlaneShape(ref planeNormal, planeConstant);
                    shape.LocalScaling = localScaling;
                    break;
                }
                case BroadphaseNativeType.GImpactShape:
                {
                    //StridingMeshInterfaceData* interfaceData = CreateStridingMeshInterfaceData(&gimpactData->m_meshInterface)
                    TriangleIndexVertexArray meshInterface = CreateMeshInterface(shapeData,
                        GImpactMeshShapeData.Offset("MeshInterface"), libPointers);

                    GImpactShapeType gImpactType = (GImpactShapeType)reader.ReadInt32(GImpactMeshShapeData.Offset("GImpactSubType"));
                    if (gImpactType == GImpactShapeType.TrimeshShape)
                    {
                        GImpactMeshShape gimpactShape = CreateGimpactShape(meshInterface);
                        gimpactShape.LocalScaling = reader.ReadVector3(GImpactMeshShapeData.Offset("LocalScaling"));
                        gimpactShape.Margin = reader.ReadSingle(GImpactMeshShapeData.Offset("CollisionMargin"));
                        gimpactShape.UpdateBound();
                        shape = gimpactShape;
                    }
                    else
                    {
#if DEBUG
                        Console.WriteLine("Unsupported GImpact subtype");
#endif
                    }
                    break;
                }
                case BroadphaseNativeType.CompoundShape:
                {
                    long childShapesPtr = reader.ReadPtr();
                    byte[] childShapes = libPointers[childShapesPtr];
                    int numChildShapes = reader.ReadInt32();
                    //float collisionMargin = reader.ReadInt32();
                    CompoundShape compoundShape = CreateCompoundShape();
                    using (MemoryStream shapeStream = new MemoryStream(childShapes, false))
                    {
                        using (BulletReader shapeReader = new BulletReader(shapeStream))
                        {
                            for (int i = 0; i < numChildShapes; i++)
                            {
                                Matrix localTransform = shapeReader.ReadMatrix();
                                long childShapePtr = shapeReader.ReadPtr();
                                int childShapeType = shapeReader.ReadInt32();
                                float childMargin = shapeReader.ReadSingle();
                                CollisionShape childShape = ConvertCollisionShape(libPointers[childShapePtr], libPointers);
                                compoundShape.AddChildShape(localTransform, childShape);
                            }
                        }
                    }
                    shape = compoundShape;
                    break;
                }
                case BroadphaseNativeType.BoxShape:
                case BroadphaseNativeType.CapsuleShape:
                case BroadphaseNativeType.ConeShape:
                case BroadphaseNativeType.ConvexHullShape:
                case BroadphaseNativeType.CylinderShape:
                case BroadphaseNativeType.MultiSphereShape:
                case BroadphaseNativeType.SphereShape:
                {
                    Vector3 localScaling = reader.ReadVector3();
                    Vector3 implicitShapeDimensions = reader.ReadVector3();
                    float collisionMargin = reader.ReadSingle();
                    stream.Position += 4; // m_padding
                    switch (type)
                    {
                        case BroadphaseNativeType.BoxShape:
                        {
                            Vector3 boxExtents = implicitShapeDimensions/localScaling + new Vector3(collisionMargin);
                            BoxShape box = CreateBoxShape(ref boxExtents) as BoxShape;
                            //box.InitializePolyhedralFeatures();
                            shape = box;
                            break;
                        }
                        case BroadphaseNativeType.CapsuleShape:
                        {
                            Vector3 halfExtents = implicitShapeDimensions + new Vector3(collisionMargin);
                            int upAxis = reader.ReadInt32();
                            switch (upAxis)
                            {
                                case 0:
                                    shape = CreateCapsuleShapeX(halfExtents.Y, halfExtents.X);
                                    break;
                                case 1:
                                    shape = CreateCapsuleShapeY(halfExtents.X, halfExtents.Y);
                                    break;
                                case 2:
                                    shape = CreateCapsuleShapeZ(halfExtents.X, halfExtents.Z);
                                    break;
                                default:
                                    Console.WriteLine("error: wrong up axis for btCapsuleShape");
                                    break;
                            }
                            break;
                        }
                        case BroadphaseNativeType.ConeShape:
                        {
                            Vector3 halfExtents = implicitShapeDimensions; // + new Vector3(collisionMargin);
                            int upAxis = reader.ReadInt32();
                            switch (upAxis)
                            {
                                case 0:
                                    shape = CreateConeShapeX(halfExtents.Y, halfExtents.X);
                                    break;
                                case 1:
                                    shape = CreateConeShapeY(halfExtents.X, halfExtents.Y);
                                    break;
                                case 2:
                                    shape = CreateConeShapeZ(halfExtents.X, halfExtents.Z);
                                    break;
                                default:
                                    Console.WriteLine("unknown Cone up axis");
                                    break;
                            }
                            break;
                        }
                        case BroadphaseNativeType.ConvexHullShape:
                        {
                            long unscaledPointsFloatPtr = reader.ReadPtr();
                            long unscaledPointsDoublePtr = reader.ReadPtr();
                            int numPoints = reader.ReadInt32();

                            byte[] points = libPointers[unscaledPointsFloatPtr];
                            ConvexHullShape hullShape = CreateConvexHullShape();
                            using (MemoryStream pointStream = new MemoryStream(points, false))
                            {
                                using (BulletReader pointReader = new BulletReader(pointStream))
                                {
                                    for (int i = 0; i < numPoints; i++)
                                    {
                                        hullShape.AddPoint(pointReader.ReadVector3());
                                    }
                                }
                            }
                            hullShape.Margin = collisionMargin;
                            //hullShape.InitializePolyhedralFeatures();
                            shape = hullShape;
                            break;
                        }
                        case BroadphaseNativeType.CylinderShape:
                        {
                            Vector3 halfExtents = implicitShapeDimensions + new Vector3(collisionMargin);
                            int upAxis = reader.ReadInt32();
                            switch (upAxis)
                            {
                                case 0:
                                    shape = CreateCylinderShapeX(halfExtents.Y, halfExtents.X);
                                    break;
                                case 1:
                                    shape = CreateCylinderShapeY(halfExtents.X, halfExtents.Y);
                                    break;
                                case 2:
                                    shape = CreateCylinderShapeZ(halfExtents.X, halfExtents.Z);
                                    break;
                                default:
                                    Console.WriteLine("unknown Cylinder up axis");
                                    break;
                            }
                            break;
                        }
                        case BroadphaseNativeType.MultiSphereShape:
                        {
                            long localPositionArrayPtr = reader.ReadPtr();
                            byte[] localPositionArray = libPointers[localPositionArrayPtr];
                            int localPositionArraySize = reader.ReadInt32();
                            Vector3[] positions = new Vector3[localPositionArraySize];
                            float[] radi = new float[localPositionArraySize];
                            using (MemoryStream localPositionArrayStream = new MemoryStream(localPositionArray, false))
                            {
                                using (BulletReader localPositionArrayReader = new BulletReader(localPositionArrayStream))
                                {
                                    for (int i = 0; i < localPositionArraySize; i++)
                                    {
                                        positions[i] = localPositionArrayReader.ReadVector3();
                                        radi[i] = localPositionArrayReader.ReadSingle();
                                    }
                                }
                            }
                            shape = CreateMultiSphereShape(positions, radi);
                            break;
                        }
                        case BroadphaseNativeType.SphereShape:
                        {
                            shape = CreateSphereShape(implicitShapeDimensions.X);
                            break;
                        }
                    }
                    if (shape != null)
                    {
                        shape.LocalScaling = localScaling;
                    }
                    break;
                }
                case BroadphaseNativeType.TriangleMeshShape:
                {
                    TriangleIndexVertexArray meshInterface = CreateMeshInterface(shapeData,
                        TriangleMeshShapeData.Offset("MeshInterface"), libPointers);
                    if (meshInterface.NumSubParts == 0)
                    {
                        return null;
                    }
                    OptimizedBvh bvh = null;
                    long bvhPtr = reader.ReadPtr(TriangleMeshShapeData.Offset("QuantizedFloatBvh"));
                    if (bvhPtr != 0)
                    {
                        if (_bvhMap.ContainsKey(bvhPtr))
                        {
                            bvh = _bvhMap[bvhPtr];
                        }
                        else
                        {
                            bvh = CreateOptimizedBvh();
                            throw new NotImplementedException();
                            //bvh.DeserializeFloat(bvhPtr);
                        }
                    }
                    bvhPtr = reader.ReadPtr(TriangleMeshShapeData.Offset("QuantizedDoubleBvh"));
                    if (bvhPtr != 0)
                    {
                        throw new NotImplementedException();
                    }
                    BvhTriangleMeshShape trimeshShape = CreateBvhTriangleMeshShape(meshInterface, bvh);
                    trimeshShape.Margin = reader.ReadSingle(TriangleMeshShapeData.Offset("CollisionMargin"));
                    shape = trimeshShape;
                    break;
                }
                case BroadphaseNativeType.SoftBodyShape:
                    return null;
                default:
#if DEBUG
                    Console.WriteLine("Unsupported shape type ({0})\n", type);
#endif
                    throw new NotImplementedException();
            }

            reader.Dispose();
            stream.Dispose();
            return shape;
        }

        protected void ConvertConstraintFloat(RigidBody rigidBodyA, RigidBody rigidBodyB, byte[] constraintData, int fileVersion, Dictionary<long, byte[]> libPointers)
        {
            TypedConstraint constraint = null;
            MemoryStream stream = new MemoryStream(constraintData, false);
            BulletReader reader = new BulletReader(stream);

            TypedConstraintType type = (TypedConstraintType)reader.ReadInt32(TypedConstraintFloatData.Offset("ObjectType"));
            switch (type)
            {
                case TypedConstraintType.Point2Point:
                {
                    Vector3 pivotInA = reader.ReadVector3(Point2PointConstraintFloatData.Offset("PivotInA"));
                    if (rigidBodyA != null && rigidBodyB != null)
                    {
                        Vector3 pivotInB = reader.ReadVector3(Point2PointConstraintFloatData.Offset("PivotInB"));
                        constraint = CreatePoint2PointConstraint(rigidBodyA, rigidBodyB, ref pivotInA, ref pivotInB);
                    }
                    else
                    {
                        constraint = CreatePoint2PointConstraint(rigidBodyA, ref pivotInA);
                    }
                    break;
                }
                case TypedConstraintType.ConeTwist:
                {
                    ConeTwistConstraint coneTwist;
                    Matrix rbaFrame = reader.ReadMatrix(ConeTwistConstraintFloatData.Offset("RigidBodyAFrame"));
                    if (rigidBodyA != null && rigidBodyB != null)
                    {
                        Matrix rbbFrame = reader.ReadMatrix(ConeTwistConstraintFloatData.Offset("RigidBodyBFrame"));
                        coneTwist = CreateConeTwistConstraint(rigidBodyA, rigidBodyB, ref rbaFrame, ref rbbFrame);
                    }
                    else
                    {
                        coneTwist = CreateConeTwistConstraint(rigidBodyA, ref rbaFrame);
                    }
                    coneTwist.SetLimit(
                        reader.ReadSingle(ConeTwistConstraintFloatData.Offset("SwingSpan1")),
                        reader.ReadSingle(ConeTwistConstraintFloatData.Offset("SwingSpan2")),
                        reader.ReadSingle(ConeTwistConstraintFloatData.Offset("TwistSpan")),
                        reader.ReadSingle(ConeTwistConstraintFloatData.Offset("LimitSoftness")),
                        reader.ReadSingle(ConeTwistConstraintFloatData.Offset("BiasFactor")),
                        reader.ReadSingle(ConeTwistConstraintFloatData.Offset("RelaxationFactor")));
                    coneTwist.Damping = reader.ReadSingle(ConeTwistConstraintFloatData.Offset("Damping"));

                    constraint = coneTwist;
                    break;
                }
                case TypedConstraintType.D6:
                {
                    Generic6DofConstraint dof = null;
                    if (rigidBodyA != null && rigidBodyB != null)
                    {
                        Matrix rbaFrame = reader.ReadMatrix(Generic6DofConstraintFloatData.Offset("RigidBodyAFrame"));
                        Matrix rbbFrame = reader.ReadMatrix(Generic6DofConstraintFloatData.Offset("RigidBodyBFrame"));
                        int useLinearReferenceFrameA =
                            reader.ReadInt32(Generic6DofConstraintFloatData.Offset("UseLinearReferenceFrameA"));
                        dof = CreateGeneric6DofConstraint(rigidBodyA, rigidBodyB, ref rbaFrame, ref rbbFrame,
                            useLinearReferenceFrameA != 0);
                    }
                    else
                    {
                        if (rigidBodyB != null)
                        {
                            Matrix rbbFrame = reader.ReadMatrix(Generic6DofConstraintFloatData.Offset("RigidBodyBFrame"));
                            int useLinearReferenceFrameA =
                                reader.ReadInt32(Generic6DofConstraintFloatData.Offset("UseLinearReferenceFrameA"));
                            dof = CreateGeneric6DofConstraint(rigidBodyB, ref rbbFrame, useLinearReferenceFrameA != 0);
                        }
                        else
                        {
                            Console.WriteLine("Error in WorldImporter.CreateGeneric6DofConstraint: missing rigidBodyB");
                        }
                    }

                    if (dof != null)
                    {
                        dof.AngularLowerLimit =
                            reader.ReadVector3(Generic6DofConstraintFloatData.Offset("AngularLowerLimit"));
                        dof.AngularUpperLimit =
                            reader.ReadVector3(Generic6DofConstraintFloatData.Offset("AngularUpperLimit"));
                        dof.LinearLowerLimit =
                            reader.ReadVector3(Generic6DofConstraintFloatData.Offset("LinearLowerLimit"));
                        dof.LinearUpperLimit =
                            reader.ReadVector3(Generic6DofConstraintFloatData.Offset("LinearUpperLimit"));
                    }
                    constraint = dof;
                    break;
                }
                case TypedConstraintType.D6Spring:
                {
                    Generic6DofSpringConstraint dof = null;
                    int sixDofData = Generic6DofSpringConstraintFloatData.Offset("SixDofData");
                    if (rigidBodyA != null && rigidBodyB != null)
                    {
                        Matrix rbaFrame = reader.ReadMatrix(sixDofData + Generic6DofConstraintFloatData.Offset("RigidBodyAFrame"));
                        Matrix rbbFrame = reader.ReadMatrix(sixDofData + Generic6DofConstraintFloatData.Offset("RigidBodyBFrame"));
                        int useLinearReferenceFrameA = reader.ReadInt32(sixDofData +
                            Generic6DofConstraintFloatData.Offset("UseLinearReferenceFrameA"));
                        dof = CreateGeneric6DofSpringConstraint(rigidBodyA, rigidBodyB, ref rbaFrame, ref rbbFrame,
                            useLinearReferenceFrameA != 0);
                    }
                    else
                    {
                        Console.WriteLine(
                            "Error in WorldImporter.CreateGeneric6DofSpringConstraint: requires rigidBodyA && rigidBodyB");
                    }

                    if (dof != null)
                    {
                        dof.AngularLowerLimit = reader.ReadVector3(sixDofData + Generic6DofConstraintFloatData.Offset("AngularLowerLimit"));
                        dof.AngularUpperLimit = reader.ReadVector3(sixDofData + Generic6DofConstraintFloatData.Offset("AngularUpperLimit"));
                        dof.LinearLowerLimit = reader.ReadVector3(sixDofData + Generic6DofConstraintFloatData.Offset("LinearLowerLimit"));
                        dof.LinearUpperLimit = reader.ReadVector3(sixDofData + Generic6DofConstraintFloatData.Offset("LinearUpperLimit"));

                        int i;
                        if (fileVersion > 280)
                        {
                            int springEnabledOffset = Generic6DofSpringConstraintFloatData.Offset("SpringEnabled");
                            int equilibriumPointOffset = Generic6DofSpringConstraintFloatData.Offset("EquilibriumPoint");
                            int springStiffnessOffset = Generic6DofSpringConstraintFloatData.Offset("SpringStiffness");
                            int springDampingOffset = Generic6DofSpringConstraintFloatData.Offset("SpringDamping");
                            for (i = 0; i < 6; i++)
                            {
                                dof.SetStiffness(i, reader.ReadSingle(springStiffnessOffset + sizeof (float)*i));
                                dof.SetEquilibriumPoint(i, reader.ReadSingle(equilibriumPointOffset + sizeof (float)*i));
                                dof.EnableSpring(i, reader.ReadInt32(springEnabledOffset + sizeof (int)*i) != 0);
                                dof.SetDamping(i, reader.ReadSingle(springDampingOffset + sizeof (float)*i));
                            }
                        }
                    }
                    constraint = dof;
                    break;
                }
                case TypedConstraintType.D6Spring2:
                {
                    Generic6DofSpring2Constraint dof = null;
                    if (rigidBodyA != null && rigidBodyB != null)
                    {
                        Matrix rbaFrame = reader.ReadMatrix(Generic6DofSpring2ConstraintFloatData.Offset("RigidBodyAFrame"));
                        Matrix rbbFrame = reader.ReadMatrix(Generic6DofSpring2ConstraintFloatData.Offset("RigidBodyBFrame"));
                        RotateOrder rotateOrder = (RotateOrder)reader.ReadInt32(Generic6DofSpring2ConstraintFloatData.Offset("RotateOrder"));
                        dof = CreateGeneric6DofSpring2Constraint(rigidBodyA, rigidBodyB, ref rbaFrame, ref rbbFrame, rotateOrder);
                    }
                    else
                    {
                        Console.WriteLine(
                            "Error in WorldImporter.CreateGeneric6DofSpring2Constraint: requires rigidBodyA && rigidBodyB");
                    }

                    if (dof != null)
                    {
                        dof.AngularLowerLimit = reader.ReadVector3(Generic6DofSpring2ConstraintFloatData.Offset("AngularLowerLimit"));
                        dof.AngularUpperLimit = reader.ReadVector3(Generic6DofSpring2ConstraintFloatData.Offset("AngularUpperLimit"));
                        dof.LinearLowerLimit = reader.ReadVector3(Generic6DofSpring2ConstraintFloatData.Offset("LinearLowerLimit"));
                        dof.LinearUpperLimit = reader.ReadVector3(Generic6DofSpring2ConstraintFloatData.Offset("LinearUpperLimit"));

                        int i;
                        if (fileVersion > 280)
                        {
                            int linearSpringStiffnessOffset = Generic6DofSpring2ConstraintFloatData.Offset("LinearSpringStiffness");
                            int linearSpringStiffnessLimitedOffset = Generic6DofSpring2ConstraintFloatData.Offset("LinearSpringStiffnessLimited");
                            int linearEnableSpringdOffset = Generic6DofSpring2ConstraintFloatData.Offset("LinearEnableSpring");
                            int linearEquilibriumPointOffset = Generic6DofSpring2ConstraintFloatData.Offset("LinearEquilibriumPoint");
                            int linearSpringDampingOffset = Generic6DofSpring2ConstraintFloatData.Offset("LinearSpringDamping");
                            int linearSpringDampingLimitedOffset = Generic6DofSpring2ConstraintFloatData.Offset("LinearSpringDampingLimited");
                            for (i = 0; i < 3; i++)
                            {
                                dof.SetStiffness(i, reader.ReadSingle(linearSpringStiffnessOffset + sizeof (float)*i),
                                    reader.ReadByte(linearSpringStiffnessLimitedOffset + sizeof (byte)*i) != 0);
                                dof.SetEquilibriumPoint(i, reader.ReadSingle(linearEquilibriumPointOffset + sizeof (float)*i));
                                dof.EnableSpring(i, reader.ReadInt32(linearEnableSpringdOffset + sizeof (byte)*i) != 0);
                                dof.SetDamping(i, reader.ReadSingle(linearSpringDampingOffset + sizeof (float)*i),
                                    reader.ReadByte(linearSpringDampingLimitedOffset + sizeof (float)*i) != 0);
                            }

                            int angularSpringStiffnessOffset = Generic6DofSpring2ConstraintFloatData.Offset("AngularSpringStiffness");
                            int angularSpringStiffnessLimitedOffset = Generic6DofSpring2ConstraintFloatData.Offset("AngularSpringStiffnessLimited");
                            int angularEnableSpringdOffset = Generic6DofSpring2ConstraintFloatData.Offset("AngularEnableSpring");
                            int angularEquilibriumPointOffset = Generic6DofSpring2ConstraintFloatData.Offset("AngularEquilibriumPoint");
                            int angularSpringDampingOffset = Generic6DofSpring2ConstraintFloatData.Offset("AngularSpringDamping");
                            int angularSpringDampingLimitedOffset = Generic6DofSpring2ConstraintFloatData.Offset("AngularSpringDampingLimited");
                            for (i = 0; i < 3; i++)
                            {
                                dof.SetStiffness(i + 3,
                                    reader.ReadSingle(angularSpringStiffnessOffset + sizeof (float)*i),
                                    reader.ReadByte(angularSpringStiffnessLimitedOffset + sizeof (byte)*i) != 0);
                                dof.SetEquilibriumPoint(i + 3,
                                    reader.ReadSingle(angularEquilibriumPointOffset + sizeof (float)*i));
                                dof.EnableSpring(i + 3,
                                    reader.ReadInt32(angularEnableSpringdOffset + sizeof (byte)*i) != 0);
                                dof.SetDamping(i + 3, reader.ReadSingle(angularSpringDampingOffset + sizeof (float)*i),
                                    reader.ReadByte(angularSpringDampingLimitedOffset + sizeof (float)*i) != 0);
                            }
                        }
                    }
                    constraint = dof;
                    break;
                }
                case TypedConstraintType.Gear:
                {
                    GearConstraint gear;
                    if (rigidBodyA != null && rigidBodyB != null)
                    {
                        Vector3 axisInA = reader.ReadVector3(GearConstraintFloatData.Offset("AxisInA"));
                        Vector3 axisInB = reader.ReadVector3(GearConstraintFloatData.Offset("AxisInB"));
                        float ratio = reader.ReadSingle(GearConstraintFloatData.Offset("Ratio"));
                        gear = CreateGearConstraint(rigidBodyA, rigidBodyB, ref axisInA, ref axisInB, ratio);
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                    constraint = gear;
                    break;
                }
                case TypedConstraintType.Hinge:
                {
                    HingeConstraint hinge;
                    Matrix rbaFrame = reader.ReadMatrix(HingeConstraintFloatData.Offset("RigidBodyAFrame"));
                    int useReferenceFrameA = reader.ReadInt32(HingeConstraintFloatData.Offset("UseReferenceFrameA"));
                    if (rigidBodyA != null && rigidBodyB != null)
                    {
                        Matrix rbbFrame = reader.ReadMatrix(HingeConstraintFloatData.Offset("RigidBodyBFrame"));
                        hinge = CreateHingeConstraint(rigidBodyA, rigidBodyB, ref rbaFrame, ref rbbFrame,
                            useReferenceFrameA != 0);
                    }
                    else
                    {
                        hinge = CreateHingeConstraint(rigidBodyA, ref rbaFrame, useReferenceFrameA != 0);
                    }
                    if (reader.ReadInt32(HingeConstraintFloatData.Offset("EnableAngularMotor")) != 0)
                    {
                        hinge.EnableAngularMotor(true,
                            reader.ReadSingle(HingeConstraintFloatData.Offset("MotorTargetVelocity")),
                            reader.ReadSingle(HingeConstraintFloatData.Offset("MaxMotorImpulse")));
                    }
                    hinge.AngularOnly = reader.ReadInt32(HingeConstraintFloatData.Offset("AngularOnly")) != 0;
                    hinge.SetLimit(
                        reader.ReadSingle(HingeConstraintFloatData.Offset("LowerLimit")),
                        reader.ReadSingle(HingeConstraintFloatData.Offset("UpperLimit")),
                        reader.ReadSingle(HingeConstraintFloatData.Offset("LimitSoftness")),
                        reader.ReadSingle(HingeConstraintFloatData.Offset("BiasFactor")),
                        reader.ReadSingle(HingeConstraintFloatData.Offset("RelaxationFactor")));
                    constraint = hinge;
                    break;
                }
                case TypedConstraintType.Slider:
                {
                    SliderConstraint slider;
                    Matrix rbbFrame = reader.ReadMatrix(SliderConstraintFloatData.Offset("RigidBodyBFrame"));
                    int useLinearReferenceFrameA =
                        reader.ReadInt32(SliderConstraintFloatData.Offset("UseLinearReferenceFrameA"));
                    if (rigidBodyA != null && rigidBodyB != null)
                    {
                        Matrix rbaFrame = reader.ReadMatrix(SliderConstraintFloatData.Offset("RigidBodyAFrame"));
                        slider = CreateSliderConstraint(rigidBodyA, rigidBodyB, ref rbaFrame, ref rbbFrame,
                            useLinearReferenceFrameA != 0);
                    }
                    else
                    {
                        slider = CreateSliderConstraint(rigidBodyB, ref rbbFrame, useLinearReferenceFrameA != 0);
                    }
                    slider.LowerLinearLimit = reader.ReadSingle(SliderConstraintFloatData.Offset("LinearLowerLimit"));
                    slider.UpperLinearLimit = reader.ReadSingle(SliderConstraintFloatData.Offset("LinearUpperLimit"));
                    slider.LowerAngularLimit = reader.ReadSingle(SliderConstraintFloatData.Offset("AngularLowerLimit"));
                    slider.UpperAngularLimit = reader.ReadSingle(SliderConstraintFloatData.Offset("AngularUpperLimit"));
                    slider.UseFrameOffset =
                        reader.ReadInt32(SliderConstraintFloatData.Offset("UseOffsetForConstraintFrame")) != 0;
                    break;
                }
                default:
                    throw new NotImplementedException();
            }

            if (constraint != null)
            {
                constraint.DebugDrawSize = reader.ReadSingle(TypedConstraintFloatData.Offset("DebugDrawSize"));
                // those fields didn't exist and set to zero for pre-280 versions, so do a check here
                if (fileVersion >= 280)
                {
                    constraint.BreakingImpulseThreshold = reader.ReadSingle(TypedConstraintFloatData.Offset("BreakingImpulseThreshold"));
                    constraint.IsEnabled = reader.ReadInt32(TypedConstraintFloatData.Offset("IsEnabled")) != 0;
                    constraint.OverrideNumSolverIterations = reader.ReadInt32(TypedConstraintFloatData.Offset("OverrideNumSolverIterations"));
                }

                long namePtr = reader.ReadPtr(TypedConstraintFloatData.Offset("Name"));
                if (namePtr != 0)
                {
                    byte[] nameData = libPointers[namePtr];
                    int length = Array.IndexOf(nameData, (byte)0);
                    string name = System.Text.Encoding.ASCII.GetString(nameData, 0, length);
                    _nameConstraintMap.Add(name, constraint);
                    _objectNameMap.Add(constraint, name);
                }

                if (_dynamicsWorld != null)
                {
                    _dynamicsWorld.AddConstraint(constraint);
                }
            }

            reader.Dispose();
            stream.Dispose();
        }

        protected void ConvertRigidBodyFloat(byte[] bodyData, Dictionary<long, byte[]> libPointers)
        {
            MemoryStream stream = new MemoryStream(bodyData, false);
            BulletReader reader = new BulletReader(stream);

            int cod = RigidBodyFloatData.Offset("CollisionObjectData");
            long collisionShapePtr = reader.ReadPtr(cod + CollisionObjectFloatData.Offset("CollisionShape"));
            Matrix startTransform = reader.ReadMatrix(cod + CollisionObjectFloatData.Offset("WorldTransform"));
            float inverseMass = reader.ReadSingle(RigidBodyFloatData.Offset("InverseMass"));
            long namePtr = reader.ReadPtr(CollisionObjectFloatData.Offset("Name"));

            float mass = inverseMass.Equals(0) ? 0 : 1.0f / inverseMass;
            CollisionShape shape = _shapeMap[collisionShapePtr];

            if (shape.IsNonMoving)
            {
                mass = 0.0f;
            }
            bool isDynamic;
            Vector3 localInertia;
            if (mass != 0.0f)
            {
                isDynamic = true;
                shape.CalculateLocalInertia(mass, out localInertia);
            }
            else
            {
                isDynamic = false;
                localInertia = Vector3.Zero;
            }
            string name = null;
            if (namePtr != 0)
            {
                byte[] nameData = libPointers[namePtr];
                int length = Array.IndexOf(nameData, (byte)0);
                name = System.Text.Encoding.ASCII.GetString(nameData, 0, length);
            }
            RigidBody body = CreateRigidBody(isDynamic, mass, ref startTransform, shape, name);
            _bodyMap.Add(bodyData, body);

            reader.Dispose();
            stream.Dispose();
        }

		public CollisionShape CreateBoxShape(ref Vector3 halfExtents)
		{
            BoxShape shape = new BoxShape(halfExtents);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public BvhTriangleMeshShape CreateBvhTriangleMeshShape(StridingMeshInterface trimesh, OptimizedBvh bvh)
		{
            BvhTriangleMeshShape shape;
            if (bvh != null)
            {
                shape = new BvhTriangleMeshShape(trimesh, bvh.IsQuantized, false);
                shape.OptimizedBvh = bvh;
            }
            else
            {
                shape = new BvhTriangleMeshShape(trimesh, true);
            }
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public CollisionShape CreateCapsuleShapeZ(float radius, float height)
		{
            CapsuleShapeZ shape = new CapsuleShapeZ(radius, height);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public CollisionShape CreateCapsuleShapeX(float radius, float height)
		{
            CapsuleShapeX shape = new CapsuleShapeX(radius, height);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public CollisionShape CreateCapsuleShapeY(float radius, float height)
		{
            CapsuleShape shape = new CapsuleShape(radius, height);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public CollisionObject CreateCollisionObject(ref Matrix startTransform, CollisionShape shape, string bodyName)
		{
            return CreateRigidBody(false, 0, ref startTransform, shape, bodyName);
		}

		public CompoundShape CreateCompoundShape()
		{
            CompoundShape shape = new CompoundShape();
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public CollisionShape CreateConeShapeZ(float radius, float height)
		{
			ConeShape shape = new ConeShapeZ(radius, height);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public CollisionShape CreateConeShapeX(float radius, float height)
		{
			ConeShape shape = new ConeShapeX(radius, height);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public CollisionShape CreateConeShapeY(float radius, float height)
		{
			ConeShape shape = new ConeShape(radius, height);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public ConeTwistConstraint CreateConeTwistConstraint(RigidBody rbA, ref Matrix rbAFrame)
		{
            ConeTwistConstraint constraint = new ConeTwistConstraint(rbA, rbAFrame);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

		public ConeTwistConstraint CreateConeTwistConstraint(RigidBody rbA, RigidBody rbB, ref Matrix rbAFrame, ref Matrix rbBFrame)
		{
            ConeTwistConstraint constraint = new ConeTwistConstraint(rbA, rbB, rbAFrame, rbBFrame);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

		public ConvexHullShape CreateConvexHullShape()
		{
            ConvexHullShape shape = new ConvexHullShape();
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}
        /*
		public CollisionShape CreateConvexTriangleMeshShape(StridingMeshInterface trimesh)
		{
			return btWorldImporter_createConvexTriangleMeshShape(_native, trimesh._native);
		}
        */
		public CollisionShape CreateCylinderShapeZ(float radius, float height)
		{
            CylinderShapeZ shape = new CylinderShapeZ(radius, radius, height);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public CollisionShape CreateCylinderShapeX(float radius, float height)
		{
            CylinderShapeX shape = new CylinderShapeX(height, radius, radius);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public CollisionShape CreateCylinderShapeY(float radius, float height)
		{
            CylinderShape shape = new CylinderShape(radius, height, radius);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public GearConstraint CreateGearConstraint(RigidBody rbA, RigidBody rbB, ref Vector3 axisInA, ref Vector3 axisInB, float ratio)
		{
			GearConstraint constraint = new GearConstraint(rbA, rbB, axisInA, axisInB, ratio);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

		public Generic6DofConstraint CreateGeneric6DofConstraint(RigidBody rbB, ref Matrix frameInB, bool useLinearReferenceFrameB)
		{
			Generic6DofConstraint constraint = new Generic6DofConstraint(rbB, frameInB, useLinearReferenceFrameB);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

		public Generic6DofConstraint CreateGeneric6DofConstraint(RigidBody rbA, RigidBody rbB, ref Matrix frameInA, ref Matrix frameInB, bool useLinearReferenceFrameA)
		{
			Generic6DofConstraint constraint = new Generic6DofConstraint(rbA, rbB, frameInA, frameInB, useLinearReferenceFrameA);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

		public Generic6DofSpringConstraint CreateGeneric6DofSpringConstraint(RigidBody rbA, RigidBody rbB, ref Matrix frameInA, ref Matrix frameInB, bool useLinearReferenceFrameA)
		{
			Generic6DofSpringConstraint constraint = new Generic6DofSpringConstraint(rbA, rbB, frameInA, frameInB, useLinearReferenceFrameA);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

        public Generic6DofSpring2Constraint CreateGeneric6DofSpring2Constraint(RigidBody rbA, RigidBody rbB, ref Matrix frameInA, ref Matrix frameInB, RotateOrder rotateOrder)
        {
            Generic6DofSpring2Constraint constraint = new Generic6DofSpring2Constraint(rbA, rbB, frameInA, frameInB, rotateOrder);
            _allocatedConstraints.Add(constraint);
            return constraint;
        }

		public GImpactMeshShape CreateGimpactShape(StridingMeshInterface trimesh)
		{
            GImpactMeshShape shape = new GImpactMeshShape(trimesh._native);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public HingeConstraint CreateHingeConstraint(RigidBody rbA, RigidBody rbB, ref Matrix rbAFrame, ref Matrix rbBFrame, bool useReferenceFrameA)
		{
            HingeConstraint constraint = new HingeConstraint(rbA, rbB, rbAFrame, rbBFrame, useReferenceFrameA);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

        public HingeConstraint CreateHingeConstraint(RigidBody rbA, RigidBody rbB, ref Matrix rbAFrame, ref Matrix rbBFrame)
		{
            HingeConstraint constraint = new HingeConstraint(rbA, rbB, rbAFrame, rbBFrame);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

        public HingeConstraint CreateHingeConstraint(RigidBody rbA, ref Matrix rbAFrame, bool useReferenceFrameA)
		{
            HingeConstraint constraint = new HingeConstraint(rbA, rbAFrame, useReferenceFrameA);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

        public HingeConstraint CreateHingeConstraint(RigidBody rbA, ref Matrix rbAFrame)
		{
            HingeConstraint constraint = new HingeConstraint(rbA, rbAFrame);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

        public TriangleIndexVertexArray CreateMeshInterface(byte[] meshData, int offset, Dictionary<long, byte[]> libPointers)
		{
            TriangleIndexVertexArray meshInterface = CreateTriangleMeshContainer();
            byte[] meshParts;
            Vector3 scaling;
            int numMeshParts;
            using (MemoryStream shapeStream = new MemoryStream(meshData, false))
            {
                using (BulletReader shapeReader = new BulletReader(shapeStream))
                {
                    shapeStream.Position += offset;
                    long meshPartsPtr = shapeReader.ReadPtr();
                    meshParts = libPointers[meshPartsPtr];
                    scaling = shapeReader.ReadVector3();
                    numMeshParts = shapeReader.ReadInt32();
                }
            }
            using (MemoryStream meshStream = new MemoryStream(meshParts, false))
            {
                using (BulletReader meshReader = new BulletReader(meshStream))
                {
                    for (int i = 0; i < numMeshParts; i++)
                    {
                        int meshOffset = i * Marshal.SizeOf(typeof(MeshPartData));

                        IndexedMesh meshPart = new IndexedMesh();
                        long vertices3f = meshReader.ReadPtr(meshOffset + MeshPartData.Offset("Vertices3F"));
                        long vertices3d = meshReader.ReadPtr(meshOffset + MeshPartData.Offset("Vertices3D"));
                        long indices32 = meshReader.ReadPtr(meshOffset + MeshPartData.Offset("Indices32"));
                        meshPart.NumTriangles = meshReader.ReadInt32(meshOffset + MeshPartData.Offset("NumTriangles"));
                        meshPart.NumVertices = meshReader.ReadInt32(meshOffset + MeshPartData.Offset("NumVertices"));
                        meshPart.Allocate(meshPart.NumTriangles, meshPart.NumVertices, sizeof(int) * 3, sizeof(float) * 4);

                        if (indices32 != 0)
                        {
                            using (Stream triangleStream = meshPart.GetTriangleStream())
                            {
                                byte[] indices = libPointers[indices32];
                                triangleStream.Write(indices, 0, indices.Length);
                            }
                        }
                        else
                        {
                            throw new NotImplementedException();
                            //long indices16 = meshReader.ReadPtr(meshOffset + MeshPartData.Offset("Indices16"));
                        }

                        if (vertices3f != 0)
                        {
                            using (Stream vertexStream = meshPart.GetVertexStream())
                            {
                                byte[] vertices = libPointers[vertices3f];
                                vertexStream.Write(vertices, 0, vertices.Length);
                            }
                        }
                        else
                        {
                            throw new NotImplementedException();
                        }
                        if (meshPart.TriangleIndexBase != IntPtr.Zero && meshPart.VertexBase != IntPtr.Zero)
                        {
                            meshInterface.AddIndexedMesh(meshPart, meshPart.IndexType);
                        }
                        //meshPart.Dispose();
                    }
                }
            }
            return meshInterface;
		}

        public MultiSphereShape CreateMultiSphereShape(Vector3[] positions, float[] radi)
		{
			MultiSphereShape shape = new MultiSphereShape(positions, radi);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}
        
		public OptimizedBvh CreateOptimizedBvh()
		{
			OptimizedBvh bvh = new OptimizedBvh();
            _allocatedBvhs.Add(bvh);
            return bvh;
		}
        
		public CollisionShape CreatePlaneShape(ref Vector3 planeNormal, float planeConstant)
		{
            StaticPlaneShape shape = new StaticPlaneShape(planeNormal, planeConstant);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public Point2PointConstraint CreatePoint2PointConstraint(RigidBody rbA, RigidBody rbB, ref Vector3 pivotInA, ref Vector3 pivotInB)
		{
            Point2PointConstraint constraint = new Point2PointConstraint(rbA, rbB, pivotInA, pivotInB);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

		public Point2PointConstraint CreatePoint2PointConstraint(RigidBody rbA, ref Vector3 pivotInA)
		{
			Point2PointConstraint constraint = new Point2PointConstraint(rbA, pivotInA);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

		public virtual RigidBody CreateRigidBody(bool isDynamic, float mass, ref Matrix startTransform, CollisionShape shape, string bodyName)
		{
            Vector3 localInertia;
            if (mass != 0.0f)
            {
                shape.CalculateLocalInertia(mass, out localInertia);
            }
            else
            {
                localInertia = Vector3.Zero;
            }

            RigidBodyConstructionInfo info = new RigidBodyConstructionInfo(mass, null, shape, localInertia);
            RigidBody body = new RigidBody(info);
            info.Dispose();
            body.WorldTransform = startTransform;

            if (_dynamicsWorld != null)
            {
                _dynamicsWorld.AddRigidBody(body);
            }

            if (bodyName != null)
            {
                _objectNameMap.Add(body, bodyName);
                _nameBodyMap.Add(bodyName, body);
            }
            _allocatedRigidBodies.Add(body);
            return body;
		}

		public ScaledBvhTriangleMeshShape CreateScaledTriangleMeshShape(BvhTriangleMeshShape meshShape, ref Vector3 localScalingbtBvhTriangleMeshShape)
		{
            ScaledBvhTriangleMeshShape shape = new ScaledBvhTriangleMeshShape(meshShape, localScalingbtBvhTriangleMeshShape);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public SliderConstraint CreateSliderConstraint(RigidBody rbB, ref Matrix frameInB, bool useLinearReferenceFrameA)
		{
			SliderConstraint constraint = new SliderConstraint(rbB, frameInB, useLinearReferenceFrameA);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

		public SliderConstraint CreateSliderConstraint(RigidBody rbA, RigidBody rbB, ref Matrix frameInA, ref Matrix frameInB, bool useLinearReferenceFrameA)
		{
            SliderConstraint constraint = new SliderConstraint(rbA, rbB, frameInA, frameInB, useLinearReferenceFrameA);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

		public CollisionShape CreateSphereShape(float radius)
		{
			SphereShape shape = new SphereShape(radius);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public byte[] CreateStridingMeshInterfaceData(byte[] interfaceData, int offset = 0)
		{
            return null;
		}

		public TriangleInfoMap CreateTriangleInfoMap()
		{
            TriangleInfoMap tim = new TriangleInfoMap();
            _allocatedTriangleInfoMaps.Add(tim);
            return tim;
		}

        public TriangleIndexVertexArray CreateTriangleMeshContainer()
		{
            TriangleIndexVertexArray tiva = new TriangleIndexVertexArray();
            _allocatedTriangleIndexArrays.Add(tiva);
            return tiva;
		}

		public void DeleteAllData()
		{
            foreach (TypedConstraint constraint in _allocatedConstraints)
            {
                if (_dynamicsWorld != null)
                {
                    _dynamicsWorld.RemoveConstraint(constraint);
                }
                constraint.Dispose();
            }
            _allocatedConstraints.Clear();

            foreach (RigidBody rigidBody in _allocatedRigidBodies)
            {
                if (_dynamicsWorld != null)
                {
                    _dynamicsWorld.RemoveRigidBody(rigidBody);
                }
                rigidBody.Dispose();
            }
            _allocatedRigidBodies.Clear();

            foreach (CollisionShape shape in _allocatedCollisionShapes)
            {
                shape.Dispose();
            }
            _allocatedCollisionShapes.Clear();

            foreach (OptimizedBvh bvh in _allocatedBvhs)
            {
                bvh.Dispose();
            }
            _allocatedBvhs.Clear();
        
            foreach (TriangleInfoMap tim in _allocatedTriangleInfoMaps)
            {
                tim.Dispose();
            }
            _allocatedTriangleInfoMaps.Clear();

            foreach (TriangleIndexVertexArray tiva in _allocatedTriangleIndexArrays)
            {
                tiva.Dispose();
            }
            _allocatedTriangleIndexArrays.Clear();

            //TODO: _allocatedbtStridingMeshInterfaceDatas
		}
        
		public OptimizedBvh GetBvhByIndex(int index)
		{
            return _allocatedBvhs[index];
		}
        
		public CollisionShape GetCollisionShapeByIndex(int index)
		{
            return _allocatedCollisionShapes[index];
		}

		public CollisionShape GetCollisionShapeByName(string name)
		{
            CollisionShape shape;
            _nameShapeMap.TryGetValue(name, out shape);
            return shape;
		}

		public TypedConstraint GetConstraintByIndex(int index)
		{
            return _allocatedConstraints[index];
		}

		public TypedConstraint GetConstraintByName(string name)
		{
            TypedConstraint constraint;
            _nameConstraintMap.TryGetValue(name, out constraint);
            return constraint;
		}

		public string GetNameForObject(Object obj)
		{
            string name;
            _objectNameMap.TryGetValue(obj, out name);
            return name;
		}

		public CollisionObject GetRigidBodyByIndex(int index)
		{
            return _allocatedRigidBodies[index];
		}

		public RigidBody GetRigidBodyByName(string name)
		{
            RigidBody body;
            _nameBodyMap.TryGetValue(name, out body);
            return body;
		}
        
		public TriangleInfoMap GetTriangleInfoMapByIndex(int index)
		{
            return _allocatedTriangleInfoMaps[index];
		}
        
		public void SetDynamicsWorldInfo(ref Vector3 gravity, ContactSolverInfo solverInfo)
		{
            if (_dynamicsWorld != null)
            {
                _dynamicsWorld.SetGravity(ref gravity);
            }
		}

		public int NumBvhs
		{
            get { return _allocatedBvhs.Count; }
		}

		public int NumCollisionShapes
		{
            get { return _allocatedCollisionShapes.Count; }
		}

		public int NumConstraints
		{
			get { return _allocatedConstraints.Count; }
		}

		public int NumRigidBodies
		{
            get { return _allocatedRigidBodies.Count; }
		}

		public int NumTriangleInfoMaps
		{
            get { return _allocatedTriangleInfoMaps.Count; }
		}

        public FileVerboseMode VerboseMode
        {
            get { return _verboseMode; }
            set { _verboseMode = value; }
        }
	}
}
