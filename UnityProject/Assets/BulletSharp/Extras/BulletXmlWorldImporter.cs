using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using BulletSharp.Math;

namespace BulletSharp
{
	public class BulletXmlWorldImporter : WorldImporter
	{
        private int _fileVersion = -1;
        private bool _fileOK = false;

        private List<byte[]> _collisionShapeData = new List<byte[]>();
        private List<byte[]> _constraintData = new List<byte[]>();
        private List<byte[]> _rigidBodyData = new List<byte[]>();
        private Dictionary<long, byte[]> _pointerLookup = new Dictionary<long, byte[]>();

		public BulletXmlWorldImporter(DynamicsWorld world)
            : base(world)
		{
		}

        private void AutoSerializeRootLevelChildren(XmlElement parent)
        {
            foreach (XmlNode node in parent)
            {
                if (node.NodeType != XmlNodeType.Element)
                {
                    continue;
                }

                XmlElement element = node as XmlElement;
                switch (element.Name)
                {
                    case "btCompoundShapeChildData":
                        DeSerializeCompoundShapeChildData(element);
                        continue;
                    case "btCompoundShapeData":
                        DeSerializeCompoundShapeData(element);
                        continue;
                    case "btConvexHullShapeData":
                        DeSerializeConvexHullShapeData(element);
                        continue;
                    case "btConvexInternalShapeData":
                        DeSerializeConvexInternalShapeData(element);
                        continue;
                    case "btDynamicsWorldFloatData":
                        DeSerializeDynamicsWorldData(element);
                        continue;
                    case "btGeneric6DofConstraintData":
                        DeSerializeGeneric6DofConstraintData(element);
                        continue;
                    case "btRigidBodyFloatData":
                        DeSerializeRigidBodyFloatData(element);
                        continue;
                    case "btStaticPlaneShapeData":
                        DeSerializeStaticPlaneShapeData(element);
                        continue;
                    case "btVector3FloatData":
                        DeSerializeVector3FloatData(element);
                        continue;
                    default:
                        throw new NotImplementedException();
                }
            }

            foreach (byte[] shapeData in _collisionShapeData)
            {
                CollisionShape shape = ConvertCollisionShape(shapeData, _pointerLookup);
                if (shape != null)
                {
                    foreach (KeyValuePair<long, byte[]> lib in _pointerLookup)
                    {
                        if (lib.Value == shapeData)
                        {
                            _shapeMap.Add(lib.Key, shape);
                            break;
                        }
                    }

                    using (MemoryStream stream = new MemoryStream(shapeData, false))
                    {
                        using (BulletReader reader = new BulletReader(stream))
                        {
                            long namePtr = reader.ReadPtr(CollisionShapeFloatData.Offset("Name"));
                            if (namePtr != 0)
                            {
                                byte[] nameData = _pointerLookup[namePtr];
                                int length = Array.IndexOf(nameData, (byte)0);
                                string name = System.Text.Encoding.ASCII.GetString(nameData, 0, length);
                                _objectNameMap.Add(shape, name);
                                _nameShapeMap.Add(name, shape);
                            }
                        }
                    }
                }
            }

            foreach (byte[] rbData in _rigidBodyData)
            {
                ConvertRigidBodyFloat(rbData, _pointerLookup);
            }

            foreach (byte[] constraintData in _constraintData)
            {
                //throw new NotImplementedException();
                //ConvertConstraint(constraintData);
            }
        }

        private void DeSerializeCollisionShapeData(XmlElement parent, BulletWriter writer)
        {
            SetIntValue(writer, parent["m_shapeType"], CollisionShapeFloatData.Offset("ShapeType"));
            writer.Write(0, CollisionShapeFloatData.Offset("Name"));
        }

        private void DeSerializeCompoundShapeChildData(XmlElement element)
        {
            int ptr = int.Parse(element.GetAttribute("pointer"));

            XmlNodeList transforms = element.SelectNodes("m_transform");
            XmlNodeList childShapes = element.SelectNodes("m_childShape");
            XmlNodeList childShapeTypes = element.SelectNodes("m_childShapeType");
            XmlNodeList childMargins = element.SelectNodes("m_childMargin");

            int numChildren = transforms.Count;
            int dataSize = Marshal.SizeOf(typeof(CompoundShapeChildFloatData));
            byte[] compoundChild = new byte[dataSize * numChildren];

            using (MemoryStream stream = new MemoryStream(compoundChild))
            {
                using (BulletWriter writer = new BulletWriter(stream))
                {
                    int offset = 0;
                    for (int i = 0; i < numChildren; i++)
                    {
                        SetTransformValue(writer, transforms[i], offset + CompoundShapeChildFloatData.Offset("Transform"));
                        SetPointerValue(writer, childShapes[i], offset + CompoundShapeChildFloatData.Offset("ChildShape"));
                        SetIntValue(writer, childShapeTypes[i], offset + CompoundShapeChildFloatData.Offset("ChildShapeType"));
                        SetFloatValue(writer, childMargins[i], offset + CompoundShapeChildFloatData.Offset("ChildMargin"));

                        offset += dataSize;
                    }
                }
            }

            _pointerLookup.Add(ptr, compoundChild);
        }

        private void DeSerializeCompoundShapeData(XmlElement element)
        {
            int ptr = int.Parse(element.GetAttribute("pointer"));
            byte[] convexShape = new byte[Marshal.SizeOf(typeof(CompoundShapeFloatData))];

            using (MemoryStream stream = new MemoryStream(convexShape))
            {
                using (BulletWriter writer = new BulletWriter(stream))
                {
                    XmlNode node = element["m_collisionShapeData"];
                    if (node == null)
                    {
                        return;
                    }
                    DeSerializeCollisionShapeData(node as XmlElement, writer);

                    SetIntValue(writer, element["m_numChildShapes"], CompoundShapeFloatData.Offset("NumChildShapes"));
                    SetPointerValue(writer, element["m_childShapePtr"], CompoundShapeFloatData.Offset("ChildShapePtr"));
                    SetFloatValue(writer, element["m_collisionMargin"], CompoundShapeFloatData.Offset("CollisionMargin"));
                }
            }

            _collisionShapeData.Add(convexShape);
            _pointerLookup.Add(ptr, convexShape);
        }

        private void DeSerializeConvexHullShapeData(XmlElement element)
        {
            int ptr = int.Parse(element.GetAttribute("pointer"));
            byte[] convexHullData = new byte[Marshal.SizeOf(typeof(ConvexHullShapeFloatData))];

            using (MemoryStream stream = new MemoryStream(convexHullData))
            {
                using (BulletWriter writer = new BulletWriter(stream))
                {
                    XmlNode node = element["m_convexInternalShapeData"];
                    if (node == null)
                    {
                        return;
                    }
                    DeSerializeConvexInternalShapeData(node as XmlElement, writer);

                    SetPointerValue(writer, element["m_unscaledPointsFloatPtr"], ConvexHullShapeFloatData.Offset("UnscaledPointsFloatPtr"));
                    SetPointerValue(writer, element["m_unscaledPointsFloatPtr"], ConvexHullShapeFloatData.Offset("UnscaledPointsFloatPtr"));
                    SetIntValue(writer, element["m_numUnscaledPoints"], ConvexHullShapeFloatData.Offset("NumUnscaledPoints"));
                }
            }

            _collisionShapeData.Add(convexHullData);
            _pointerLookup.Add(ptr, convexHullData);
        }

        private void DeSerializeConvexInternalShapeData(XmlElement element)
        {
            int ptr = int.Parse(element.GetAttribute("pointer"));
            byte[] convexShapeData = new byte[Marshal.SizeOf(typeof(ConvexInternalShapeFloatData))];

            using (MemoryStream stream = new MemoryStream(convexShapeData))
            {
                using (BulletWriter writer = new BulletWriter(stream))
                {
                    DeSerializeConvexInternalShapeData(element, writer);
                }
            }

            _collisionShapeData.Add(convexShapeData);
            _pointerLookup.Add(ptr, convexShapeData);
        }

        private void DeSerializeConvexInternalShapeData(XmlElement element, BulletWriter writer)
        {
            XmlNode node = element["m_collisionShapeData"];
            if (node == null)
            {
                return;
            }
            DeSerializeCollisionShapeData(node as XmlElement, writer);

            SetFloatValue(writer, element["m_collisionMargin"], ConvexInternalShapeFloatData.Offset("CollisionMargin"));
            SetVector4Value(writer, element["m_localScaling"], ConvexInternalShapeFloatData.Offset("LocalScaling"));
            SetVector4Value(writer, element["m_implicitShapeDimensions"], ConvexInternalShapeFloatData.Offset("ImplicitShapeDimensions"));
        }

        private void DeSerializeDynamicsWorldData(XmlElement element)
        {
        }

        private void DeSerializeGeneric6DofConstraintData(XmlElement element)
        {
            int ptr = int.Parse(element.GetAttribute("pointer"));
            byte[] dof6Data = new byte[Marshal.SizeOf(typeof(Generic6DofConstraintFloatData))];

            using (MemoryStream stream = new MemoryStream(dof6Data))
            {
                using (BulletWriter writer = new BulletWriter(stream))
                {
                    XmlNode node = element["m_typeConstraintData"];
                    if (node == null)
                    {
                        return;
                    }
                    SetPointerValue(writer, node["m_rbA"], TypedConstraintFloatData.Offset("RigidBodyA"));
                    SetPointerValue(writer, node["m_rbB"], TypedConstraintFloatData.Offset("RigidBodyB"));
                    writer.Write(0, TypedConstraintFloatData.Offset("Name"));
                    SetIntValue(writer, node["m_objectType"], TypedConstraintFloatData.Offset("ObjectType"));
                    SetIntValue(writer, node["m_userConstraintType"], TypedConstraintFloatData.Offset("UserConstraintType"));
                    SetIntValue(writer, node["m_userConstraintId"], TypedConstraintFloatData.Offset("UserConstraintId"));
                    SetIntValue(writer, node["m_needsFeedback"], TypedConstraintFloatData.Offset("NeedsFeedback"));
                    SetFloatValue(writer, node["m_appliedImpulse"], TypedConstraintFloatData.Offset("AppliedImpulse"));
                    SetFloatValue(writer, node["m_dbgDrawSize"], TypedConstraintFloatData.Offset("DebugDrawSize"));
                    SetIntValue(writer, node["m_disableCollisionsBetweenLinkedBodies"], TypedConstraintFloatData.Offset("DisableCollisionsBetweenLinkedBodies"));
                    SetIntValue(writer, node["m_overrideNumSolverIterations"], TypedConstraintFloatData.Offset("OverrideNumSolverIterations"));
                    SetFloatValue(writer, node["m_breakingImpulseThreshold"], TypedConstraintFloatData.Offset("BreakingImpulseThreshold"));
                    SetIntValue(writer, node["m_isEnabled"], TypedConstraintFloatData.Offset("IsEnabled"));

                    SetTransformValue(writer, element["m_rbAFrame"], Generic6DofConstraintFloatData.Offset("RigidBodyAFrame"));
                    SetTransformValue(writer, element["m_rbBFrame"], Generic6DofConstraintFloatData.Offset("RigidBodyBFrame"));
                    SetVector4Value(writer, element["m_linearUpperLimit"], Generic6DofConstraintFloatData.Offset("LinearUpperLimit"));
                    SetVector4Value(writer, element["m_linearLowerLimit"], Generic6DofConstraintFloatData.Offset("LinearLowerLimit"));
                    SetVector4Value(writer, element["m_angularUpperLimit"], Generic6DofConstraintFloatData.Offset("AngularUpperLimit"));
                    SetVector4Value(writer, element["m_angularLowerLimit"], Generic6DofConstraintFloatData.Offset("AngularLowerLimit"));
                    SetIntValue(writer, element["m_useLinearReferenceFrameA"], Generic6DofConstraintFloatData.Offset("UseLinearReferenceFrameA"));
                    SetIntValue(writer, element["m_useOffsetForConstraintFrame"], Generic6DofConstraintFloatData.Offset("UseOffsetForConstraintFrame"));
                }
            }

            _constraintData.Add(dof6Data);
        }

        private void DeSerializeRigidBodyFloatData(XmlElement element)
        {
            int ptr;
            if (!int.TryParse(element.GetAttribute("pointer"), out ptr))
            {
                _fileOK = false;
                return;
            }

            byte[] rbData = new byte[Marshal.SizeOf(typeof(RigidBodyFloatData))];

            using (MemoryStream stream = new MemoryStream(rbData))
            {
                using (BulletWriter writer = new BulletWriter(stream))
                {
                    XmlNode node = element["m_collisionObjectData"];
                    if (node == null)
                    {
                        return;
                    }
                    SetPointerValue(writer, node["m_collisionShape"], CollisionObjectFloatData.Offset("CollisionShape"));
                    SetTransformValue(writer, node["m_worldTransform"], CollisionObjectFloatData.Offset("WorldTransform"));
                    SetTransformValue(writer, node["m_interpolationWorldTransform"], CollisionObjectFloatData.Offset("InterpolationWorldTransform"));
                    SetVector4Value(writer, node["m_interpolationLinearVelocity"], CollisionObjectFloatData.Offset("InterpolationLinearVelocity"));
                    SetVector4Value(writer, node["m_interpolationAngularVelocity"], CollisionObjectFloatData.Offset("InterpolationAngularVelocity"));
                    SetVector4Value(writer, node["m_anisotropicFriction"], CollisionObjectFloatData.Offset("AnisotropicFriction"));
                    SetFloatValue(writer, node["m_contactProcessingThreshold"], CollisionObjectFloatData.Offset("ContactProcessingThreshold"));
                    SetFloatValue(writer, node["m_deactivationTime"], CollisionObjectFloatData.Offset("DeactivationTime"));
                    SetFloatValue(writer, node["m_friction"], CollisionObjectFloatData.Offset("Friction"));
                    SetFloatValue(writer, node["m_restitution"], CollisionObjectFloatData.Offset("Restitution"));
                    SetFloatValue(writer, node["m_hitFraction"], CollisionObjectFloatData.Offset("HitFraction"));
                    SetFloatValue(writer, node["m_ccdSweptSphereRadius"], CollisionObjectFloatData.Offset("CcdSweptSphereRadius"));
                    SetFloatValue(writer, node["m_ccdMotionThreshold"], CollisionObjectFloatData.Offset("CcdMotionThreshold"));
                    SetIntValue(writer, node["m_hasAnisotropicFriction"], CollisionObjectFloatData.Offset("HasAnisotropicFriction"));
                    SetIntValue(writer, node["m_collisionFlags"], CollisionObjectFloatData.Offset("CollisionFlags"));
                    SetIntValue(writer, node["m_islandTag1"], CollisionObjectFloatData.Offset("IslandTag1"));
                    SetIntValue(writer, node["m_companionId"], CollisionObjectFloatData.Offset("CompanionId"));
                    SetIntValue(writer, node["m_activationState1"], CollisionObjectFloatData.Offset("ActivationState1"));
                    SetIntValue(writer, node["m_internalType"], CollisionObjectFloatData.Offset("InternalType"));
                    SetIntValue(writer, node["m_checkCollideWith"], CollisionObjectFloatData.Offset("CheckCollideWith"));

                    SetMatrix3x3Value(writer, element["m_invInertiaTensorWorld"], RigidBodyFloatData.Offset("InvInertiaTensorWorld"));
                    SetVector4Value(writer, element["m_linearVelocity"], RigidBodyFloatData.Offset("LinearVelocity"));
                    SetVector4Value(writer, element["m_angularVelocity"], RigidBodyFloatData.Offset("AngularVelocity"));
                    SetVector4Value(writer, element["m_angularFactor"], RigidBodyFloatData.Offset("AngularFactor"));
                    SetVector4Value(writer, element["m_linearFactor"], RigidBodyFloatData.Offset("LinearFactor"));
                    SetVector4Value(writer, element["m_gravity"], RigidBodyFloatData.Offset("Gravity"));
                    SetVector4Value(writer, element["m_gravity_acceleration"], RigidBodyFloatData.Offset("GravityAcceleration"));
                    SetVector4Value(writer, element["m_invInertiaLocal"], RigidBodyFloatData.Offset("InvInertiaLocal"));
                    SetVector4Value(writer, element["m_totalTorque"], RigidBodyFloatData.Offset("TotalTorque"));
                    SetVector4Value(writer, element["m_totalForce"], RigidBodyFloatData.Offset("TotalForce"));
                    SetFloatValue(writer, element["m_inverseMass"], RigidBodyFloatData.Offset("InverseMass"));
                    SetFloatValue(writer, element["m_linearDamping"], RigidBodyFloatData.Offset("LinearDamping"));
                    SetFloatValue(writer, element["m_angularDamping"], RigidBodyFloatData.Offset("AngularDamping"));
                    SetFloatValue(writer, element["m_additionalDampingFactor"], RigidBodyFloatData.Offset("AdditionalDampingFactor"));
                    SetFloatValue(writer, element["m_additionalLinearDampingThresholdSqr"], RigidBodyFloatData.Offset("AdditionalLinearDampingThresholdSqr"));
                    SetFloatValue(writer, element["m_additionalAngularDampingThresholdSqr"], RigidBodyFloatData.Offset("AdditionalAngularDampingThresholdSqr"));
                    SetFloatValue(writer, element["m_additionalAngularDampingFactor"], RigidBodyFloatData.Offset("AdditionalAngularDampingFactor"));
                    SetFloatValue(writer, element["m_angularSleepingThreshold"], RigidBodyFloatData.Offset("AngularSleepingThreshold"));
                    SetFloatValue(writer, element["m_linearSleepingThreshold"], RigidBodyFloatData.Offset("LinearSleepingThreshold"));
                    SetIntValue(writer, element["m_additionalDamping"], RigidBodyFloatData.Offset("AdditionalDamping"));
                }
            }

            _rigidBodyData.Add(rbData);
        }

        private void DeSerializeStaticPlaneShapeData(XmlElement element)
        {
            int ptr = int.Parse(element.GetAttribute("pointer"));
            byte[] convexShape = new byte[Marshal.SizeOf(typeof(StaticPlaneShapeFloatData))];

            using (MemoryStream stream = new MemoryStream(convexShape))
            {
                using (BulletWriter writer = new BulletWriter(stream))
                {
                    XmlNode node = element["m_collisionShapeData"];
                    if (node == null)
                    {
                        return;
                    }
                    DeSerializeCollisionShapeData(node as XmlElement, writer);

                    SetVector4Value(writer, element["m_localScaling"], StaticPlaneShapeFloatData.Offset("LocalScaling"));
                    SetVector4Value(writer, element["m_planeNormal"], StaticPlaneShapeFloatData.Offset("PlaneNormal"));
                    SetFloatValue(writer, element["m_planeConstant"], StaticPlaneShapeFloatData.Offset("PlaneConstant"));
                }
            }

            _collisionShapeData.Add(convexShape);
            _pointerLookup.Add(ptr, convexShape);
        }

        private void DeSerializeVector3FloatData(XmlElement element)
        {
            int ptr = int.Parse(element.GetAttribute("pointer"));

            XmlNodeList vectors = element.SelectNodes("m_floats");
            int numVectors = vectors.Count;

            int vectorSize = Marshal.SizeOf(typeof(Vector3FloatData));
            byte[] v = new byte[numVectors * vectorSize];

            using (MemoryStream stream = new MemoryStream(v))
            {
                using (BulletWriter writer = new BulletWriter(stream))
                {
                    int offset = 0;
                    for (int i = 0; i < numVectors; i++)
                    {
                        SetVector4Value(writer, vectors[i], offset);
                        offset += vectorSize;
                    }
                }
            }

            _pointerLookup.Add(ptr, v);
        }

		public bool LoadFile(string fileName)
		{
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);

            XmlElement bulletPhysics = doc.DocumentElement;
            if (!bulletPhysics.Name.Equals("bullet_physics"))
            {
                Console.WriteLine("ERROR: no bullet_physics element");
                return false;
            }

            if (!int.TryParse(bulletPhysics.GetAttribute("version"), out _fileVersion))
            {
                return false;
            }

            if (_fileVersion == 281)
            {
                _fileOK = true;
                AutoSerializeRootLevelChildren(bulletPhysics);
                return _fileOK;
            }

            return false;
		}

        private void SetFloatValue(BulletWriter writer, XmlNode valueNode, int offset)
        {
            writer.Write(float.Parse(valueNode.InnerText, CultureInfo.InvariantCulture), offset);
        }

        private void SetIntValue(BulletWriter writer, XmlNode valueNode, int offset)
        {
            writer.Write(int.Parse(valueNode.InnerText), offset);
        }

        private void SetPointerValue(BulletWriter writer, XmlNode valueNode, int offset)
        {
            writer.Write(new IntPtr(long.Parse(valueNode.InnerText)), offset);
        }

        private void SetMatrix3x3Value(BulletWriter writer, XmlElement valueNode, int offset)
        {
            XmlNode floats = valueNode["m_el"]["m_floats"];
            SetVector4Value(writer, floats, offset);
            floats = floats.NextSibling;
            SetVector4Value(writer, floats, offset + 16);
            floats = floats.NextSibling;
            SetVector4Value(writer, floats, offset + 32);
        }

        private void SetTransformValue(BulletWriter writer, XmlNode valueNode, int offset)
        {
            SetMatrix3x3Value(writer, valueNode["m_basis"], offset);
            SetVector4Value(writer, valueNode["m_origin"], offset + TransformFloatData.OriginOffset);
        }

        private void SetVector4Value(BulletWriter writer, XmlNode valueNode, int offset)
        {
            int i = 0;
            foreach (string value in valueNode.InnerText.Split(' '))
            {
                if (!string.IsNullOrEmpty(value))
                {
                    writer.Write(float.Parse(value, CultureInfo.InvariantCulture), offset + i * sizeof(float));
                    i++;
                    if (i == 4)
                    {
                        break;
                    }
                }
            }
        }
	}
}
