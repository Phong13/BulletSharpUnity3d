using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using BulletSharp;
using BulletSharp.Math;
using UnityEngine;
using BulletUnity;
using BulletUnity.Primitives;

namespace DemoFramework.FileLoaders
{
    
    public class UrdfLoader : URDFImporterInterface
    {
        public Material material;
        public UrdfRobot robot;
        string filename;

        public UrdfLoader(Material m)
        {
            material = m;
        }

        public bool loadURDF(string fileName, bool forceFixedBase = false)
        {
            robot = FromFile(fileName);
            filename = fileName;
            return true;
        }

        public bool loadSDF(string fileName, bool forceFixedBase = false)
        {
            throw new NotImplementedException();
            return false;
        }

        public string getPathPrefix()
        {
            return System.IO.Path.GetDirectoryName(filename);
        }

        public int getRootLinkIndex()
        {
            return robot.rootLink.index;
        }

        public string getLinkName(int linkIndex)
        {
            return robot.Links[linkIndex].Name;
        }

        public string getBodyName()
        {
            return robot.Name;
        }

        public bool getLinkColor(int linkIndex, out Color colorRGBA)
        {
            colorRGBA = Color.gray;
            return true;
        }

        public bool getLinkColor2(int linkIndex, out UrdfMaterialColor matCol)
        {
            matCol = new UrdfMaterialColor();
            matCol.m_rgbaColor = Color.gray;
            matCol.m_specularColor = Color.white;
            return true;
        }

        public UrdfCollisionFlags getCollisionGroupAndMask(int linkIndex, out CollisionFilterGroups colGroup, out CollisionFilterGroups colMask)
        {
            colGroup = CollisionFilterGroups.DefaultFilter;
            colMask = CollisionFilterGroups.Everything;
            UrdfCollisionFlags colFlags = UrdfCollisionFlags.URDF_HAS_COLLISION_GROUP;
            colFlags |= UrdfCollisionFlags.URDF_HAS_COLLISION_MASK;
            return colFlags;
        }

        public bool getLinkContactInfo(int linkIndex, out URDFLinkContactInfo contactInfo)
        {
            UrdfCollision col = robot.Links[linkIndex].Collision;
            URDFLinkContactInfo linkContactInfo = new URDFLinkContactInfo();

            
            //linkContactInfo.m_contactCfm =
            //linkContactInfo.m_contactDamping
            //linkContactInfo.m_contactErp
            //linkContactInfo.m_contactStiffness
            //linkContactInfo.m_flags
            //linkContactInfo.m_inertiaScaling
            //linkContactInfo.m_lateralFriction
            //linkContactInfo.m_restitution
            //linkContactInfo.m_rollingFriction
            //linkContactInfo.m_spinningFriction
            

            contactInfo = linkContactInfo;
            return false;
        }

        public string getJointName(int linkIndex)
        {
            return robot.Links[linkIndex].joint.name;
        }

        public void getMassAndInertia(int urdfLinkIndex, out float mass, out BulletSharp.Math.Vector3 localInertiaDiagonal, out Matrix inertialFrame)
        {
            UrdfLink linkPtr = robot.Links[urdfLinkIndex];
            if (linkPtr != null)
            {
                UrdfLink link = linkPtr;
                Matrix linkInertiaBasis = Matrix.Identity;
                float linkMass, principalInertiaX, principalInertiaY, principalInertiaZ;
                if (link.joint == null && robot.overrideFixedBase)
                {
                    linkMass = 0.0f;
                    principalInertiaX = 0.0f;
                    principalInertiaY = 0.0f;
                    principalInertiaZ = 0.0f;
                    linkInertiaBasis = Matrix.Identity;
                }
                else
                {
                    linkMass = link.Inertial.Mass;
                    if (link.Inertial.ixy == 0.0 &&
                        link.Inertial.ixz == 0.0 &&
                        link.Inertial.iyz == 0.0)
                    {
                        principalInertiaX = link.Inertial.ixx;
                        principalInertiaY = link.Inertial.iyy;
                        principalInertiaZ = link.Inertial.izz;
                        linkInertiaBasis = Matrix.Identity;
                    }
                    else
                    {
                        UnityEngine.Debug.LogError("TODO not implemented diagonalize");
                        principalInertiaX = link.Inertial.ixx;
                        Matrix inertiaTensor = new Matrix(link.Inertial.ixx, link.Inertial.ixy, link.Inertial.ixz, 0,
                                      link.Inertial.ixy, link.Inertial.iyy, link.Inertial.iyz, 0,
                                      link.Inertial.ixz, link.Inertial.iyz, link.Inertial.izz, 0,
                                      0, 0, 0, 0);
                        float threshold = 1.0e-6f;
                        int numIterations = 30;
                        //inertiaTensor.diagonalize(linkInertiaBasis, threshold, numIterations);
                        principalInertiaX = inertiaTensor[0, 0];
                        principalInertiaY = inertiaTensor[1, 1];
                        principalInertiaZ = inertiaTensor[2, 2];
                    }
                }
                mass = linkMass;
                if (principalInertiaX < 0 ||
                    principalInertiaX > (principalInertiaY + principalInertiaZ) ||
                    principalInertiaY < 0 ||
                    principalInertiaY > (principalInertiaX + principalInertiaZ) ||
                    principalInertiaZ < 0 ||
                    principalInertiaZ > (principalInertiaX + principalInertiaY))
                {
                    UnityEngine.Debug.LogWarningFormat("Bad inertia tensor properties, setting inertia to zero for link: {0}\n", link.Name);
                    principalInertiaX = 0.0f;
                    principalInertiaY = 0.0f;
                    principalInertiaZ = 0.0f;
                    linkInertiaBasis = Matrix.Identity;
                }
                localInertiaDiagonal = new BulletSharp.Math.Vector3(principalInertiaX, principalInertiaY, principalInertiaZ);
                inertialFrame = new Matrix();
                inertialFrame.Origin = (link.Inertial.origin.Position);

                //inertialFrame.Basis(link.Inertial.m_linkLocalFrame.getBasis() * linkInertiaBasis);
                Matrix basis = new Matrix();
                Matrix inertialM = link.Inertial.origin.ToMatrix();
                Matrix.Multiply(ref inertialM, ref linkInertiaBasis, out basis);
                inertialFrame.Column1 = basis.Column1;
                inertialFrame.Column2 = basis.Column2;
                inertialFrame.Column3 = basis.Column3;
            }
            else
            {
                mass = 1.0f;
                localInertiaDiagonal = new BulletSharp.Math.Vector3(1, 1, 1);
                inertialFrame = Matrix.Identity;
            }
        }

        public void getLinkChildIndices(int urdfLinkIndex, List<int> childLinkIndices)
        {
            UrdfLink lk = robot.Links[urdfLinkIndex];
            childLinkIndices.Clear();
            for (int i = 0; i < lk.joints.Count; i++)
            {
                childLinkIndices.Add(lk.joints[i].UrdfChildLink.index);
            }
        }

        public bool getJointInfo(int urdfLinkIndex, out Matrix parent2joint, out Matrix linkTransformInWorld, out BulletSharp.Math.Vector3 jointAxisInJointSpace, out UrdfJointTypes jointType, out float jointLowerLimit, out float jointUpperLimit, out float jointDamping, out float jointFriction)
        {
            throw new NotImplementedException();
        }

        public bool getJointInfo2(int urdfLinkIndex, out Matrix parent2joint, out Matrix linkTransformInWorld, out BulletSharp.Math.Vector3 jointAxisInJointSpace, out UrdfJointTypes jointType, out float jointLowerLimit, out float jointUpperLimit, out float jointDamping, out float jointFriction, out float jointMaxForce, out float jointMaxVelocity)
        {
            jointLowerLimit = 0.0f;
            jointUpperLimit = 0.0f;
            jointDamping = 0.0f;
            jointFriction = 0.0f;
            jointMaxForce = 0.0f;
            jointMaxVelocity = 0.0f;
            jointAxisInJointSpace = BulletSharp.Math.Vector3.UnitX;
            jointType = UrdfJointTypes.URDFFixedJoint;
            parent2joint = Matrix.Identity;
            linkTransformInWorld = Matrix.Identity;
            UrdfLink linkPtr = robot.Links[urdfLinkIndex];
            if (linkPtr != null)
            {
                UrdfLink link = linkPtr;

                link.GetlinkTransformInWorld(out linkTransformInWorld);

                if (link.joint != null)
                {
                    UrdfJoint pj = link.joint;
                    Matrix p2j = new Matrix();
                    link.GetParentLinkToJointTransform(ref p2j);
                    parent2joint = p2j;
                    jointType = pj.GetJointType();
                    if (pj.axis != null)
                    {
                        jointAxisInJointSpace = pj.axis.xyz;
                    }
                    if (pj.limit != null)
                    {
                        jointLowerLimit = pj.limit.lower;
                        jointUpperLimit = pj.limit.upper;
                        jointMaxForce = pj.limit.effort;
                        jointMaxVelocity = pj.limit.velocity;
                    }
                    if (pj.dynamics != null)
                    {
                        jointDamping = pj.dynamics.damping;
                        jointFriction = pj.dynamics.friction;
                    }
                    return true;
                }
                else
                {
                    parent2joint = Matrix.Identity;
                    return false;
                }
            }
            return false;
        }

        public void activateModel(int modelIndex)
        {
            //model is robot
        }

        public bool getRootTransformInWorld(out Matrix rootTransformInWorld)
        {
            throw new NotImplementedException();
        }

        public void setRootTransformInWorld(ref Matrix rootTransformInWorld)
        {
            throw new NotImplementedException();
        }

        public int convertLinkVisualShapes(int linkIndex, string pathPrefix, ref Matrix inertialFrame)
        {
            throw new NotImplementedException();
        }

        public void convertLinkVisualShapes2(int linkIndex, int urdfIndex, string pathPrefix, ref Matrix inertialFrame, CollisionObject colObj, int objectIndex)
        {
            throw new NotImplementedException();
        }

        public void setBodyUniqueId(int bodyId)
        {
            throw new NotImplementedException();
        }

        public int getBodyUniqueId()
        {
            throw new NotImplementedException();
        }

        public BCollisionShape convertLinkCollisionShapes(int linkIndex, string pathPrefix, ref Matrix localInertiaFrame, GameObject go)
        {
            BCollisionShape retValue;
            UrdfLink l = robot.Links[linkIndex];
            if (l.Collision != null)
            {
                if (l.Collision.Geometry.Type == UrdfGeometryType.Box)
                {
                    BBox bb = go.AddComponent<BBox>();
                    UrdfBox box = (UrdfBox)l.Collision.Geometry;
                    bb.meshSettings.extents = box.size.ToUnity();
                    bb.BuildMesh();
                    go.GetComponent<MeshRenderer>().sharedMaterial = material;
                    retValue = go.GetComponent<BCollisionShape>();
                }
                else if (l.Collision.Geometry.Type == UrdfGeometryType.Cylinder)
                {
                    BCylinder bc = go.AddComponent<BCylinder>();
                    UrdfCylinder cylinder = (UrdfCylinder)l.Collision.Geometry;
                    bc.meshSettings.height = cylinder.Length;
                    bc.meshSettings.radius = cylinder.Radius;
                    bc.meshSettings.nbSides = 12;
                    bc.BuildMesh();
                    go.GetComponent<MeshRenderer>().sharedMaterial = material;
                    retValue = go.GetComponent<BCollisionShape>();
                }
                else if (l.Collision.Geometry.Type == UrdfGeometryType.Sphere)
                {
                    BSphere bs = go.AddComponent<BSphere>();
                    UrdfSphere sphere = (UrdfSphere)l.Collision.Geometry;
                    bs.meshSettings.numLatitudeLines = 8;
                    bs.meshSettings.numLongitudeLines = 8;
                    bs.meshSettings.radius = sphere.Radius;
                    bs.BuildMesh();
                    go.GetComponent<MeshRenderer>().sharedMaterial = material;
                    retValue = go.GetComponent<BCollisionShape>();
                }
                else if (l.Collision.Geometry.Type == UrdfGeometryType.Mesh)
                {
                    UrdfMesh umesh = (UrdfMesh)l.Collision.Geometry;
                    //todo correct directory separators for other platforms
                    string mpath = pathPrefix + "\\" + umesh.FileName.Replace('/', '\\');
                    Mesh mesh = LoadMeshFromSTL.LoadMesh(mpath, umesh.Scale);
                    BConvexHull bch = go.AddComponent<BConvexHull>();
                    bch.meshSettings.UserMesh = mesh;
                    bch.BuildMesh();
                    go.GetComponent<MeshRenderer>().sharedMaterial = material;
                    retValue = go.GetComponent<BCollisionShape>();
                }
                else
                {
                    Debug.LogError("Unknown collsion shape");

                }
            }
            else
            {
                Debug.LogError("Link has no collision " + l.Name);
            }
            return null;
        }

        public int getNumAllocatedCollisionShapes()
        {
            throw new NotImplementedException();
        }

        public int getNumModels()
        {
            if (robot == null) return 0;
            else return 1;
        }

        public int getNumAllocatedMeshInterfaces()
        {
            throw new NotImplementedException();
        }

        public StridingMeshInterface getAllocatedMeshInterface(int index)
        {
            throw new NotImplementedException();
        }

        private static UrdfRobot FromFile(string filename)
        {
            var document = new XmlDocument();
            document.Load(filename);
            return ParseRobot(document["robot"]);
        }

        private static UrdfRobot ParseRobot(XmlElement element)
        {
            UrdfRobot robot = new UrdfRobot
            {
                Name = element.GetAttribute("name")
            };

            List<UrdfLink> tmpLinks = new List<UrdfLink>();
            List<UrdfJoint> tmpJoints = new List<UrdfJoint>();
            foreach (XmlElement linkElement in element.SelectNodes("link"))
            {
                UrdfLink link = ParseLink(linkElement);
                Debug.Log("Parsed Link " + link.Name + " " + link.Inertial);
                if (link.Name.Equals("base_link"))
                {
                    robot.rootLink = link;
                }
                tmpLinks.Add(link);
            }

            foreach (XmlElement jointElement in element.SelectNodes("joint"))
            {
                UrdfJoint joint = ParseJoint(jointElement);
                tmpJoints.Add(joint);
            }
            for (int i = 0; i < tmpJoints.Count; i++)
            {
                tmpJoints[i].UrdfChildLink = tmpLinks.Find(x => x.Name == tmpJoints[i].child);
                tmpJoints[i].UrdfParentLink = tmpLinks.Find(x => x.Name == tmpJoints[i].parent);
                tmpJoints[i].UrdfChildLink.joint = tmpJoints[i];
                tmpJoints[i].UrdfParentLink.joints.Add(tmpJoints[i]);
            }
            //now build links list in correct order
            AddLinkToList(robot.rootLink, robot);

            return robot;
        }

        static void AddLinkToList(UrdfLink link, UrdfRobot robot)
        {
            robot.Links.Add(link);
            robot.Links[robot.Links.Count - 1].index = robot.Links.Count - 1;
            for (int i = 0; i < link.joints.Count; i++)
            {
                AddLinkToList(link.joints[i].UrdfChildLink, robot);
            }
        }

        private static UrdfLink ParseLink(XmlElement element)
        {
            return new UrdfLink
            {
                Name = element.GetAttribute("name"),
                Collision = ParseCollision(element["collision"]),
                Inertial = ParseInertial(element["inertial"]),
            };
        }

        private static UrdfJoint ParseJoint(XmlElement element)
        {
            return new UrdfJoint(element);
        }

        private static UrdfCollision ParseCollision(XmlElement element)
        {
            if (element == null)
            {
                return null;
            }
            return new UrdfCollision
            {
                Geometry = ParseGeometry(element["geometry"]),
                Origin = ParseUrdfPose(element["origin"])
            };
        }

        private static UrdfInertial ParseInertial(XmlElement element)
        {
            if (element == null)
            {
                return new UrdfInertial();
            }
            UrdfInertial inertia = new UrdfInertial();
            XmlElement inertiaElement = element["intertia"];
            if (inertiaElement != null)
            {
                inertia.ixx = float.Parse(element.Attributes["ixx"].Value, CultureInfo.InvariantCulture);
                inertia.ixy = float.Parse(element.Attributes["ixy"].Value, CultureInfo.InvariantCulture);
                inertia.ixz = float.Parse(element.Attributes["ixz"].Value, CultureInfo.InvariantCulture);
                inertia.iyy = float.Parse(element.Attributes["iyy"].Value, CultureInfo.InvariantCulture);
                inertia.iyz = float.Parse(element.Attributes["iyz"].Value, CultureInfo.InvariantCulture);
                inertia.izz = float.Parse(element.Attributes["izz"].Value, CultureInfo.InvariantCulture);
            }
            inertia.Mass = ParseMass(element["mass"]);
            inertia.origin = ParseUrdfPose(element["origin"]);
            if (inertia.origin == null) inertia.origin = new UrdfPose();
            return inertia;
        }

        private static float ParseMass(XmlElement element)
        {
            return float.Parse(element.Attributes["value"].Value, CultureInfo.InvariantCulture);
        }

        private static UrdfGeometry ParseGeometry(XmlElement element)
        {
            var shapeElement = element.SelectSingleNode("box|cylinder|mesh|sphere");
            switch (shapeElement.Name)
            {
                case "box":
                    UrdfBox b = new UrdfBox();
                    b.size = new BulletSharp.Math.Vector3(StringToFloats(shapeElement.Attributes["size"].Value));
                    return b;
                case "cylinder":
                    UrdfCylinder c = new UrdfCylinder();

                    c.Radius = float.Parse(
                            shapeElement.Attributes["radius"].Value,
                            CultureInfo.InvariantCulture);
                    c.Length = float.Parse(
                            shapeElement.Attributes["length"].Value,
                            CultureInfo.InvariantCulture);
                    return c;
                case "mesh":
                    UrdfMesh m = new UrdfMesh();
                    m.FileName = shapeElement.Attributes["filename"].Value;
                    m.Scale = shapeElement.Attributes["scale"] != null ? new BulletSharp.Math.Vector3(StringToFloats(shapeElement.Attributes["scale"].Value)) : new BulletSharp.Math.Vector3(1f,1f,1f);
                    return m;
                case "sphere":
                    UrdfSphere sp = new UrdfSphere();

                    sp.Radius = float.Parse(
                            shapeElement.Attributes["radius"].Value,
                            CultureInfo.InvariantCulture);
                    return sp;
            }
            return null;
        }

        public static float[] StringToFloats(string s)
        {
            try
            {
                string[] strs = s.Split(new char[] { ' ', ',', }, StringSplitOptions.RemoveEmptyEntries);
                float[] fs = new float[strs.Length];
                for (int i = 0; i < fs.Length; i++)
                {
                    fs[i] = float.Parse(strs[i]);
                }
                return fs;

            }
            catch
            {
                return new float[0];
            }
        }

        public static UrdfPose ParseUrdfPose(XmlElement element)
        {
            if (element == null)
            {
                return null;
            }
            UrdfPose pose = new UrdfPose();
            pose.Position = new BulletSharp.Math.Vector3(StringToFloats(element.Attributes["xyz"].Value));
            XmlAttribute rpy = element.Attributes["rpy"];
            if (rpy != null)
            {
                pose.RollPitchYaw = new BulletSharp.Math.Vector3(StringToFloats(element.Attributes["rpy"].Value));
            } else
            {
                pose.RollPitchYaw = new BulletSharp.Math.Vector3(0, 0, 0);
            }
            return pose;
        }
    }

    public class UrdfRobot
    {
        public string Name { get; set; }

        public UrdfLink rootLink;

        public List<UrdfLink> Links = new List<UrdfLink>();

        public List<UrdfJoint> Joints = new List<UrdfJoint>();

        public bool overrideFixedBase;

        public override string ToString()
        {
            return Name;
        }

        public static float ReadOptionalFloat(string d)
        {
            if (d == null)
            {
                return 0;
            }
            else
            {
                try
                {
                    return float.Parse(d);
                }
                catch (Exception e)
                {
                    return 0;
                }
            }
        }
    }

    public class UrdfJoint
    {
        public UrdfLink UrdfParentLink;
        public UrdfLink UrdfChildLink;
        public string name;
        public string type;
        public UrdfPose origin;
        public string parent;
        public string child;
        public Axis axis;
        public Calibration calibration;
        public Dynamics dynamics;
        public Limit limit;
        public Mimic mimic;
        public SafetyController safetyController;

        public UrdfJoint(XmlElement node)
        {
            name = (string)node.Attributes[("name")].Value; // required
            type = (string)node.Attributes["type"].Value; // required
            origin = (node["origin"] != null) ? UrdfLoader.ParseUrdfPose(node["origin"]) : new UrdfPose(); // optional  
            parent = (string)node["parent"].Attributes[("link")].Value; // required
            child = (string)node["child"].Attributes["link"].Value; // required
            axis = (node["axis"] != null) ? new Axis(node["axis"]) : null;  // optional 
            calibration = (node["calibration"] != null) ? new Calibration(node["calibration"]) : null;  // optional 
            dynamics = (node["dynamics"] != null) ? new Dynamics(node["dynamics"]) : null;  // optional 
            limit = (node["limit"] != null) ? new Limit(node["limit"]) : null;  // required only for revolute and prismatic joints
            mimic = (node["mimic"] != null) ? new Mimic(node["mimic"]) : null;  // optional
            safetyController = (node["safety_controller"] != null) ? new SafetyController(node["safety_controller"]) : null;  // optional
        }

        public bool toTransformMatrix(out Matrix tr)
        {
            tr = Matrix.AffineTransformation(1, 
                UnityEngine.Quaternion.Euler(origin.RollPitchYaw.ToUnity() * Mathf.Rad2Deg).ToBullet(), 
                origin.Position);

            return true;
        }

        public UrdfJointTypes GetJointType()
        {
            
            if (type.Equals("revolute"))
            {
                return UrdfJointTypes.URDFRevoluteJoint;
            }
            else if (type.Equals("fixed"))
            {
                return UrdfJointTypes.URDFFixedJoint;
            }
            else if (type.Equals("continuous"))
            {
                return UrdfJointTypes.URDFContinuousJoint;
            }
            else if (type.Equals("prismatic"))
            {
                return UrdfJointTypes.URDFPrismaticJoint;
            }
            else if (type.Equals("floating"))
            {
                return UrdfJointTypes.URDFFloatingJoint;
            }
            else if (type.Equals("planar"))
            {
                return UrdfJointTypes.URDFPlanarJoint;
            }
            UnityEngine.Debug.LogError("Unknown joint type");
            
            return UrdfJointTypes.URDFFixedJoint;
        }
    }

    public class Axis
    {
        public BulletSharp.Math.Vector3 xyz;

        public Axis(XmlElement node)
        {
            xyz = new BulletSharp.Math.Vector3(UrdfLoader.StringToFloats(node.Attributes.GetNamedItem("xyz").Value));
        }
    }

    public class Calibration
    {
        public float rising;
        public float falling;

        public Calibration(XmlElement node)
        {
            rising = UrdfRobot.ReadOptionalFloat(node.Attributes["rising"].Value);  // optional
            falling = UrdfRobot.ReadOptionalFloat(node.Attributes["falling"].Value);  // optional
        }
    }

    public class Dynamics
    {
        public float damping;
        public float friction;

        public Dynamics(XmlElement node)
        {
            damping = UrdfRobot.ReadOptionalFloat(node.Attributes["damping"].Value); // optional
            friction = UrdfRobot.ReadOptionalFloat(node.Attributes["friction"].Value); // optional
        }
    }

    public class Limit
    {
        public float lower;
        public float upper;
        public float effort;
        public float velocity;

        public Limit(XmlElement node)
        {
            lower = UrdfRobot.ReadOptionalFloat(node.Attributes[("lower")].Value); // optional
            upper = UrdfRobot.ReadOptionalFloat(node.Attributes[("upper")].Value); // optional
            effort = (float)UrdfRobot.ReadOptionalFloat(node.Attributes[("effort")].Value); // required
            velocity = (float)UrdfRobot.ReadOptionalFloat(node.Attributes[("velocity")].Value); // required
        }
    }

    public class Mimic
    {
        public string joint;
        public float multiplier;
        public float offset;

        public Mimic(XmlElement node)
        {
            joint = (string)node.Attributes["joint"].Value; // required
            multiplier = UrdfRobot.ReadOptionalFloat(node.Attributes["multiplier"].Value); // optional
            offset = UrdfRobot.ReadOptionalFloat(node.Attributes["offset"].Value); // optional   
        }
    }

    public class SafetyController
    {
        public float softLowerLimit;
        public float softUpperLimit;
        public float kPosition;
        public float kVelocity;

        public SafetyController(XmlElement node)
        {
            softLowerLimit = UrdfRobot.ReadOptionalFloat(node.Attributes[("soft_lower_limit")].Value); // optional
            softUpperLimit = UrdfRobot.ReadOptionalFloat(node.Attributes[("soft_upper_limit")].Value); // optional
            kPosition = UrdfRobot.ReadOptionalFloat(node.Attributes[("k_position")].Value); // optional
            kVelocity = UrdfRobot.ReadOptionalFloat(node.Attributes[("k_velocity")].Value); // required   
        }
    }

    public class UrdfLink
    {
        public int index;
        public string Name { get; set; }
        public UrdfCollision Collision { get; set; }
        public UrdfInertial Inertial { get; set; }
        public UrdfJoint joint;
        public List<UrdfJoint> joints = new List<UrdfJoint>();
        public UrdfPose pose = null;

        public override string ToString()
        {
            return Name;
        }

        public void GetParentLinkToJointTransform(ref Matrix m)
        {
            joint.toTransformMatrix(out m);
        }

        public void GetlinkTransformInWorld(out Matrix m)
        {
            if (pose == null)
            {
                m = Matrix.Identity;
            }
            else
            {
                UnityEngine.Debug.LogError("Not implemented");
                m = Matrix.Identity;
                //m = Matrix.AffineTransformation(1, UnityEngine.Quaternion.Euler(origin.RollPitchYaw.ToUnity() * Mathf.Rad2Deg).ToBullet(), origin.Postion);
            }
        }
    }

    public class UrdfCollision
    {
        public UrdfGeometry Geometry { get; set; }
        public UrdfPose Origin { get; set; }
    }

    public class UrdfInertial
    {
        public float Mass = 0f;
        public float ixx = 1, ixy = 0, ixz = 0, iyy = 1, iyz = 0, izz = 1;
        public UrdfPose origin = new UrdfPose();
    }

    public enum UrdfGeometryType
    {
        Box,
        Cylinder,
        Mesh,
        Sphere
    }

    public abstract class UrdfGeometry
    {
        public abstract UrdfGeometryType Type { get; set; }
    }

    public class UrdfBox : UrdfGeometry
    {
        UrdfGeometryType _type = UrdfGeometryType.Box;
        public override UrdfGeometryType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public BulletSharp.Math.Vector3 size;
    }

    public class UrdfCylinder : UrdfGeometry
    {
        UrdfGeometryType _type = UrdfGeometryType.Cylinder;
        public override UrdfGeometryType Type
        {
            get { return _type; }
            set { _type = value; }
        }
        public float Length { get; set; }
        public float Radius { get; set; }
    }

    public class UrdfMesh : UrdfGeometry
    {
        UrdfGeometryType _type = UrdfGeometryType.Mesh;
        public override UrdfGeometryType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string FileName { get; set; }
        public BulletSharp.Math.Vector3 Scale { get; set; }
    }

    public class UrdfSphere : UrdfGeometry
    {
        UrdfGeometryType _type = UrdfGeometryType.Sphere;
        public override UrdfGeometryType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public float Radius { get; set; }
    }

    public class UrdfPose
    {
        public BulletSharp.Math.Vector3 Position;
        public BulletSharp.Math.Vector3 RollPitchYaw;
        public Matrix ToMatrix()
        {
            Matrix m = Matrix.AffineTransformation(1, UnityEngine.Quaternion.Euler(RollPitchYaw.ToUnity() * Mathf.Rad2Deg).ToBullet(), Position);
            return m;
        }
    }
}
