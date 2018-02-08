using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BulletSharp;
using BulletSharp.Math;
using System.Xml;
using System;
using BulletUnity;

namespace DemoFramework.FileLoaders
{
    

    public enum ePARENT_LINK_ENUMS
    {
        BASE_LINK_INDEX = -1,
        INVALID_LINK_INDEX = -2
    }

    public class UrdfMaterial
    {
        public string m_name;
        public string m_textureFilename;
        public UrdfMaterialColor m_matColor;
        public UrdfMaterial()
        {
            m_matColor = new UrdfMaterialColor();
        }
    };

    public class UrdfInertia
    {
        public Matrix m_linkLocalFrame;
        public bool m_hasLinkLocalFrame;

        public float m_mass;
        public float m_ixx, m_ixy, m_ixz, m_iyy, m_iyz, m_izz;

        public UrdfInertia()
        {
            m_hasLinkLocalFrame = false;
            m_linkLocalFrame = Matrix.Identity;
            m_mass = 0.0f;
            m_ixx = m_ixy = m_ixz = m_iyy = m_iyz = m_izz = 0.0f;
        }
    };

    public enum UrdfGeomTypes
    {
        URDF_GEOM_SPHERE = 2,
        URDF_GEOM_BOX,
        URDF_GEOM_CYLINDER,
        URDF_GEOM_MESH,
        URDF_GEOM_PLANE,
        URDF_GEOM_CAPSULE, //non-standard URDF?
        URDF_GEOM_UNKNOWN,
    };

    public class UrdfModel2
    {
        public string m_name;
        public string m_sourceFile;
        public Matrix m_rootTransformInWorld;
        public System.Collections.Specialized.OrderedDictionary m_materials = new System.Collections.Specialized.OrderedDictionary();
        public System.Collections.Specialized.OrderedDictionary m_links = new System.Collections.Specialized.OrderedDictionary();
        public System.Collections.Specialized.OrderedDictionary m_joints = new System.Collections.Specialized.OrderedDictionary();

        public List<UrdfLink2> m_rootLinks = new List<UrdfLink2>();
        public bool m_overrideFixedBase = false;

        public UrdfModel2(bool overrideFixedBase = false)
        {
            m_rootTransformInWorld = Matrix.Identity;
        }
    };

    public enum MeshFileFormat {
        FILE_STL = 1,
        FILE_COLLADA = 2,
        FILE_OBJ = 3,
    }

    public class UrdfGeometry2
    {

        public UrdfGeomTypes m_type;

        public float m_sphereRadius;

        public BulletSharp.Math.Vector3 m_boxSize;

        public float m_capsuleRadius;
        public float m_capsuleHeight;
        public bool m_hasFromTo;
        public BulletSharp.Math.Vector3 m_capsuleFrom;
        public BulletSharp.Math.Vector3 m_capsuleTo;

        public BulletSharp.Math.Vector3 m_planeNormal;


        public int m_meshFileType;
        public string m_meshFileName;
        public BulletSharp.Math.Vector3 m_meshScale;

        public UrdfMaterial m_localMaterial;
        public bool m_hasLocalMaterial;

        public UrdfGeometry2()
        {
            m_type = (UrdfGeomTypes.URDF_GEOM_UNKNOWN);

            m_sphereRadius = (1);

            m_boxSize = new BulletSharp.Math.Vector3(1, 1, 1);

            m_capsuleRadius = (1);

            m_capsuleHeight = (1);

            m_hasFromTo = false;

        m_capsuleFrom = new BulletSharp.Math.Vector3(0, 1, 0);

        m_capsuleTo = new BulletSharp.Math.Vector3(1, 0, 0);

        m_planeNormal = new BulletSharp.Math.Vector3(0, 0, 1);

            m_meshFileType = (0);

            m_meshScale = new BulletSharp.Math.Vector3(1, 1, 1);

            m_localMaterial = new UrdfMaterial();
            m_hasLocalMaterial = (false);
        }

    };

    public class UrdfShape
    {
        public string m_sourceFileLocation;
        public Matrix m_linkLocalFrame;
        public UrdfGeometry2 m_geometry;
        public string m_name;
    };

    public class UrdfVisual : UrdfShape
    {
        public string m_materialName;
    };

    public class UrdfCollision2 : UrdfShape
    {
        public int m_flags;
        public int m_collisionGroup;
        public int m_collisionMask;
        public UrdfCollision2()
        {
            m_flags = 0;
        }
    };

    public class UrdfLink2
    {
        public string m_name;
        public UrdfInertia m_inertia = new UrdfInertia();
        public Matrix m_linkTransformInWorld;
        public List<UrdfVisual> m_visualArray = new List<UrdfVisual>();
        public List<UrdfCollision2> m_collisionArray = new List<UrdfCollision2>();
        public UrdfLink2 m_parentLink;
        public UrdfJoint2 m_parentJoint;
        public bool m_isJointLink = false;
        public List<UrdfJoint2> m_childJoints = new List<UrdfJoint2>();
        public List<UrdfLink2> m_childLinks = new List<UrdfLink2>();

        public int m_linkIndex;

        public URDFLinkContactInfo m_contactInfo = new URDFLinkContactInfo();


        public UrdfLink2()
        {
            m_parentLink = null;

            m_parentJoint = null;

            m_linkIndex = (-2);
        }

    };

    public class UrdfJoint2
    {
        public string m_name;
        public UrdfJointTypes m_type;
        public Matrix m_parentLinkToJointTransform;
        public string m_parentLinkName = "";
        public string m_childLinkName = "";
        public BulletSharp.Math.Vector3 m_localJointAxis;

        public float m_lowerLimit;
        public float m_upperLimit;

        public float m_effortLimit;
        public float m_velocityLimit;

        public float m_jointDamping;
        public float m_jointFriction;
        public UrdfJoint2()
        {
            m_lowerLimit = (0);

            m_upperLimit = (-1);

            m_effortLimit = (0);

            m_velocityLimit = (0);

            m_jointDamping = (0);

            m_jointFriction = (0);
        }
    };

    public class BulletMJCFImporter : URDFImporterInterface
    {
        const float SIM_EPSILON = 1.192092896e-07f;

        class MyMJCFAsset
        {
            public string m_fileName;
        }

        class MyMJCFDefaults
        {
            public int m_defaultCollisionGroup;
            public int m_defaultCollisionMask;
            public float m_defaultCollisionMargin;

            // joint defaults
            public string m_defaultJointLimited;

            // geom defaults
            public string m_defaultGeomRgba;
            public int m_defaultConDim;
            public float m_defaultLateralFriction;
            public float m_defaultSpinningFriction;
            public float m_defaultRollingFriction;

            public MyMJCFDefaults()
            {
                m_defaultCollisionGroup = (1);

                m_defaultCollisionMask = (1);

                m_defaultCollisionMargin = (0.001f);//assume unit meters, margin is 1mm

                m_defaultConDim = (3);

                m_defaultLateralFriction = (0.5f);

                m_defaultSpinningFriction = (0);

                m_defaultRollingFriction = (0);

            }

        }

        public class MJCFErrorLogger
        {
            public void reportWarning(string str)
            {
                Debug.LogWarning(str);
            }

            public void reportError(string str)
            {
                Debug.LogError(str);
            }
        }

        static int gUid = 0;
        public Material material;

        static bool parseVector4(out BulletSharp.Math.Vector4 vec4, string vector_str)
        {
            vec4 = new BulletSharp.Math.Vector4(0, 0, 0, 0);
            List<float> rgba = new List<float>();
            string[] pieces = vector_str.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < pieces.Length; ++i)
            {
                if (!pieces[i].Equals(""))
                {
                    rgba.Add(float.Parse(pieces[i]));
                }
            }
            if (rgba.Count != 4)
            {
                return false;
            }
            vec4 = new BulletSharp.Math.Vector4(rgba[0], rgba[1], rgba[2], rgba[3]);
            return true;
        }


        static bool parseVector3(out BulletSharp.Math.Vector3 vec3, string vector_str, MJCFErrorLogger logger, bool lastThree = false)
        {
            vec3 = BulletSharp.Math.Vector3.Zero;
            List<float> rgba = new List<float>();
            string[] pieces = vector_str.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < pieces.Length; ++i)
            {
                if (pieces[i].Length != 0)
                {
                    rgba.Add(float.Parse(pieces[i]));
                }
            }
            if (rgba.Count < 3)
            {
                logger.reportWarning(("Couldn't parse vector3 '" + vector_str + "'"));
                return false;
            }
            if (lastThree)
            {
                vec3.SetValue(rgba[rgba.Count - 3], rgba[rgba.Count - 2], rgba[rgba.Count - 1]);
            }
            else
            {
                vec3.SetValue(rgba[0], rgba[1], rgba[2]);

            }
            return true;
        }


        static bool parseVector6(out BulletSharp.Math.Vector3 v0, out BulletSharp.Math.Vector3 v1, string vector_str, MJCFErrorLogger logger)
        {
            v0 = BulletSharp.Math.Vector3.Zero;
            v1 = BulletSharp.Math.Vector3.Zero;
            List<float> values = new List<float>();
            string[] pieces = vector_str.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < pieces.Length; ++i)
            {
                if (pieces[i].Length != 0)
                {
                    values.Add(float.Parse(pieces[i]));
                }
            }
            if (values.Count < 6)
            {
                logger.reportWarning(("Couldn't parse 6 floats '" + vector_str + "'"));

                return false;
            }
            v0.SetValue(values[0], values[1], values[2]);
            v1.SetValue(values[3], values[4], values[5]);

            return true;
        }

        class BulletMJCFImporterInternalData
        {
            //public GUIHelperInterface m_guiHelper;
            //public LinkVisualShapesConverter m_customVisualShapesConverter;
            public string m_pathPrefix;

            public string m_sourceFileName; // with path
            public string m_fileModelName;  // without path
            public Dictionary<string, MyMJCFAsset> m_assets = new Dictionary<string, MyMJCFAsset>();

            public List<UrdfModel2> m_models = new List<UrdfModel2>();

            //<compiler angle="radian" meshdir="mesh/" texturedir="texture/"/>
            public string m_meshDir;
            public string m_textureDir;
            public string m_angleUnits;


            public int m_activeModel;
            public int m_activeBodyUniqueId;

            //todo: for better MJCF compatibility, we would need a stack of default values
            public MyMJCFDefaults m_globalDefaults;
            public Dictionary<string, MyMJCFDefaults> m_classDefaults = new Dictionary<string, MyMJCFDefaults>();

            //those collision shapes are deleted by caller (todo: make sure this happens!)
            public List<BCollisionShape> m_allocatedCollisionShapes = new List<BCollisionShape>();
            public List<TriangleMesh> m_allocatedMeshInterfaces = new List<TriangleMesh>();

            public BulletMJCFImporterInternalData()
            {
                m_activeModel = (-1);
                m_activeBodyUniqueId = (-1);
                m_pathPrefix = null;
            }

            public string sourceFileLocation(XmlElement e)
            {
#if FALSE
	//no C++11 snprintf etc
		char buf[1024];
		snprintf(buf, sizeof(buf), "%s:%i", m_sourceFileName, e.Row());
		return buf;
#else
                string row = string.Format("{0}", e.Value);
                string str = m_sourceFileName + (":") + row.ToString();
                return str;
#endif
            }

            /*
            public  UrdfLink2 getLink(int modelIndex, int linkIndex)
            {
                if (modelIndex >= 0 && modelIndex < m_models.Count)
                {
                    UrdfLink2 linkPtrPtr = m_models[modelIndex].m_links.getAtIndex(linkIndex);
                    if (linkPtrPtr != null && linkPtrPtr != null)
                    {
                        UrdfLink2 linkPtr = linkPtrPtr;
                        return linkPtr;
                    }
                }
                return null;
            }
            */

            public void parseCompiler(XmlElement root_xml, MJCFErrorLogger logger)
            {

                string meshDirStr = root_xml.Attributes[("meshdir")].Value;
                if (meshDirStr != null)
                {
                    m_meshDir = meshDirStr;
                }
                string textureDirStr = root_xml.Attributes[("texturedir")].Value;
                if (textureDirStr != null)
                {
                    m_textureDir = textureDirStr;
                }
                string angle = root_xml.Attributes[("angle")].Value;
                m_angleUnits = angle != null ? angle : "degree";  // degrees by default, http://www.mujoco.org/book/modeling.html#compiler
#if FALSE
		for (XmlElement child_xml = root_xml.FirstChildElement() ; child_xml ; child_xml = child_xml.NextSiblingElement())
		{
			string n = child_xml.Value();
		}
#endif
            }

            public void parseAssets(XmlElement root_xml, MJCFErrorLogger logger)
            {
                //		<mesh name="index0" 	file="index0.stl"/>
                foreach (XmlElement child_xml in root_xml.ChildNodes)
                {
                    string n = child_xml.Value;
                    if (n == "mesh")
                    {
                        string assetNameStr = child_xml.Attributes[("name")].Value;
                        string fileNameStr = child_xml.Attributes[("file")].Value;
                        if (assetNameStr != null && fileNameStr != null)
                        {
                            string assetName = assetNameStr;
                            MyMJCFAsset asset = new MyMJCFAsset();
                            asset.m_fileName = m_meshDir + fileNameStr;
                            m_assets.Add(assetName, asset);
                        }
                    }

                }
            }


            public bool parseDefaults(out MyMJCFDefaults defaults, XmlElement root_xml, MJCFErrorLogger logger)
            {
                bool handled = false;
                defaults = new MyMJCFDefaults();
                //rudimentary 'default' support, would need more work for better feature coverage
                foreach (XmlElement child_xml in root_xml.ChildNodes)
                {
                    string n = child_xml.Name;

                    if (n.IndexOf("default") != -1)
                    {
                        string className = child_xml.Attributes[("class")].Value;

                        if (className != null)
                        {
                            MyMJCFDefaults curDefaultsPtr = m_classDefaults[className];
                            if (curDefaultsPtr == null)
                            {
                                MyMJCFDefaults def = new MyMJCFDefaults();
                                m_classDefaults.Add(className, def);
                                curDefaultsPtr = m_classDefaults[className];
                            }
                            if (curDefaultsPtr != null)
                            {
                                MyMJCFDefaults curDefaults = curDefaultsPtr;
                                parseDefaults(out curDefaults, child_xml, logger);
                            }
                        }
                    }

                    if (n == "inertial")
                    {
                    }
                    if (n == "asset")
                    {
                        parseAssets(child_xml, logger);
                    }
                    if (n == "joint")
                    {
                        // Other attributes here:
                        // armature="1"
                        // damping="1"
                        // limited="true"
                        
                        if (child_xml.Attributes[("limited")] != null)
                        {
                            string conTypeStr = child_xml.Attributes[("limited")].Value;
                            defaults.m_defaultJointLimited = child_xml.Attributes[("limited")].Value;
                        }
                    }
                    if (n == "geom")
                    {
                        //contype, conaffinity 
                        
                        if (child_xml.Attributes[("contype")] != null)
                        {
                            string conTypeStr = child_xml.Attributes[("contype")].Value;
                            defaults.m_defaultCollisionGroup = int.Parse(conTypeStr);
                        }
                        
                        if (child_xml.Attributes[("conaffinity")] != null)
                        {
                            string conAffinityStr = child_xml.Attributes[("conaffinity")].Value;
                            defaults.m_defaultCollisionMask = int.Parse(conAffinityStr);
                        }
                        
                        if (child_xml.Attributes[("rgba")] != null)
                        {
                            string rgba = child_xml.Attributes[("rgba")].Value;
                            defaults.m_defaultGeomRgba = rgba;
                        }

                        
                        if (child_xml.Attributes[("condim")] != null)
                        {
                            string conDimS = child_xml.Attributes[("condim")].Value;
                            defaults.m_defaultConDim = int.Parse(conDimS);
                        }
                        int conDim = defaults.m_defaultConDim;

                        
                        if (child_xml.Attributes[("friction")] != null)
                        {
                            string frictionS = child_xml.Attributes[("friction")].Value;
                            List<float> frictions = new List<float>();
                            List<float> values = new List<float>();
                            string[] pieces = frictionS.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                            for (int i = 0; i < pieces.Length; ++i)
                            {
                                if (pieces[i].Length != 0)
                                {
                                    frictions.Add(float.Parse(pieces[i]));
                                }
                            }
                            if (frictions.Count > 0)
                            {
                                defaults.m_defaultLateralFriction = frictions[0];
                            }
                            if (frictions.Count > 1)
                            {
                                defaults.m_defaultSpinningFriction = frictions[1];
                            }
                            if (frictions.Count > 2)
                            {
                                defaults.m_defaultRollingFriction = frictions[2];
                            }
                        }
                    }
                }
                handled = true;
                return handled;
            }

            public bool parseRootLevel(MyMJCFDefaults defaults, XmlElement rootxml, MJCFErrorLogger logger)
            {
                Debug.Log("rootxm " + rootxml + " " + rootxml.Value + " nm " + rootxml.Name);

                    bool handled = false;
                    string n = rootxml.Name;

                    Debug.Log(n);

                    if (n == "body")
                    {
                        int modelIndex = m_models.Count;
                        UrdfModel2 model = new UrdfModel2(false);
                        m_models.Add(model);
                        parseBody(defaults, rootxml, modelIndex, (int)ePARENT_LINK_ENUMS.INVALID_LINK_INDEX, logger);
                        initTreeAndRoot(model, logger);
                        handled = true;
                    }

                    if (n == "geom")
                    {
                        int modelIndex = m_models.Count;
                        UrdfModel2 modelPtr = new UrdfModel2(false);
                        m_models.Add(modelPtr);

                        UrdfLink2 linkPtr = new UrdfLink2();
                        linkPtr.m_name = "anonymous";
                        XmlAttribute namePtr = rootxml.Attributes[("name")];
                        if (namePtr != null)
                        {
                            linkPtr.m_name = namePtr.Value;
                        }
                        int linkIndex = modelPtr.m_links.Count;
                        linkPtr.m_linkIndex = linkIndex;
                        if (modelPtr.m_links.Contains(linkPtr.m_name)) Debug.LogError("CONTAINS!!!");
                        modelPtr.m_links.Add(linkPtr.m_name, linkPtr);

                        //don't parse geom transform here, it will be inside 'parseGeom'
                        linkPtr.m_linkTransformInWorld = Matrix.Identity;

                        //				modelPtr.m_rootLinks.Add(linkPtr);

                        BulletSharp.Math.Vector3 inertialShift = new BulletSharp.Math.Vector3(0, 0, 0);
                        parseGeom(defaults, rootxml, modelIndex, linkIndex, logger, inertialShift);
                        initTreeAndRoot(modelPtr, logger);

                        handled = true;
                    }


                    if (n == "site")
                    {
                        handled = true;
                    }
                    if (!handled)
                    {
                        logger.reportWarning((sourceFileLocation(rootxml) + ": unhandled root element '" + n + "' nm" + rootxml.Name));
                    }
                return true;
            }

            public bool parseJoint(MyMJCFDefaults defaults, XmlElement link_xml, int modelIndex, int parentLinkIndex, int linkIndex, MJCFErrorLogger logger, Matrix parentToLinkTrans, out Matrix jointTransOut)
            {
                bool jointHandled = false;
                XmlAttribute jType = link_xml.Attributes["type"];
                XmlAttribute limitedStr = link_xml.Attributes["limited"];
                XmlAttribute axisStr = link_xml.Attributes["axis"];
                XmlAttribute posStr = link_xml.Attributes["pos"];
                XmlAttribute ornStr = link_xml.Attributes["quat"];
                XmlAttribute nameStr = link_xml.Attributes["name"];
                XmlAttribute rangeStr = link_xml.Attributes["range"];
                
                Matrix jointTrans = Matrix.Identity;
                if (posStr != null)
                {
                    BulletSharp.Math.Vector3 pos;
                    string p = posStr.Value;
                    if (parseVector3(out pos, p, logger))
                    {
                        jointTrans.Origin = (pos);
                    }
                }
                if (ornStr != null)
                {
                    string o = ornStr.Value;
                    BulletSharp.Math.Vector4 o4;
                    if (parseVector4(out o4, o))
                    {
                        BulletSharp.Math.Quaternion orn = new BulletSharp.Math.Quaternion(o4[3], o4[0], o4[1], o4[2]);
                        jointTrans.Rotation = (orn);
                    }
                }

                BulletSharp.Math.Vector3 jointAxis = new BulletSharp.Math.Vector3(1, 0, 0);
                if (axisStr != null)
                {
                    string ax = axisStr.Value;
                    parseVector3(out jointAxis, ax, logger);
                }
                else
                {
                    logger.reportWarning((sourceFileLocation(link_xml) + ": joint without axis attribute"));
                }

                float[] range = new float[2] { 1, 0 };
                string lim = m_globalDefaults.m_defaultJointLimited;
                if (limitedStr != null)
                {
                    lim = limitedStr.Value;
                }
                bool isLimited = lim == "true";

                UrdfJointTypes ejtype = UrdfJointTypes.URDFFixedJoint;
                if (jType != null)
                {
                    string jointType = jType.Value;
                    if (jointType == "fixed")
                    {
                        ejtype = UrdfJointTypes.URDFFixedJoint;
                        jointHandled = true;
                    }
                    if (jointType == "slide")
                    {
                        ejtype = UrdfJointTypes.URDFPrismaticJoint;
                        jointHandled = true;
                    }
                    if (jointType == "hinge")
                    {
                        if (isLimited)
                        {
                            ejtype = UrdfJointTypes.URDFRevoluteJoint;
                        }
                        else
                        {
                            ejtype = UrdfJointTypes.URDFContinuousJoint;
                        }
                        jointHandled = true;
                    }
                }
                else
                {
                    logger.reportWarning((sourceFileLocation(link_xml) + ": expected 'type' attribute for joint"));
                }

                if (isLimited)
                {
                    //parse the 'range' field
                    List<float> limits = new List<float>();
                    string[] pieces = rangeStr.Value.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < pieces.Length; ++i)
                    {
                        if (pieces[i].Length != 0)
                        {
                            limits.Add(float.Parse(pieces[i]));
                        }
                    }
                    if (limits.Count == 2)
                    {
                        range[0] = limits[0];
                        range[1] = limits[1];
                        if (m_angleUnits == "degree" && ejtype == UrdfJointTypes.URDFRevoluteJoint)
                        {
                            range[0] = limits[0] * Mathf.PI / 180;
                            range[1] = limits[1] * Mathf.PI / 180;
                        }
                    }
                    else
                    {
                        logger.reportWarning((sourceFileLocation(link_xml) + ": cannot parse 'range' attribute (units='" + m_angleUnits + "'')"));
                    }
                }

                // TODO armature : real, "0" Armature inertia (or rotor inertia) of all
                // degrees of freedom created by this joint. These are constants added to the
                // diagonal of the inertia matrix in generalized coordinates. They make the
                // simulation more stable, and often increase physical realism. This is because
                // when a motor is attached to the system with a transmission that amplifies
                // the motor force by c, the inertia of the rotor (i.e. the moving part of the
                // motor) is amplified by c*c. The same holds for gears in the early stages of
                // planetary gear boxes. These extra inertias often dominate the inertias of
                // the robot parts that are represented explicitly in the model, and the
                // armature attribute is the way to model them.

                // TODO damping : real, "0" Damping applied to all degrees of
                // freedom created by this joint. Unlike friction loss
                // which is computed by the constraint solver, damping is
                // simply a force linear in velocity. It is included in
                // the passive forces. Despite this simplicity, larger
                // damping values can make numerical integrators unstable,
                // which is why our Euler integrator handles damping
                // implicitly. See Integration in the Computation chapter.

                UrdfLink2 linkPtr = getLink(modelIndex, linkIndex);

                Matrix parentLinkToJointTransform = Matrix.Identity;
                parentLinkToJointTransform = parentToLinkTrans * jointTrans;
                jointTransOut = jointTrans;

                if (jointHandled)
                {
                    UrdfJoint2 jointPtr = new UrdfJoint2();
                    jointPtr.m_childLinkName = linkPtr.m_name;
                    UrdfLink2 parentLink = getLink(modelIndex, parentLinkIndex);
                    jointPtr.m_parentLinkName = parentLink.m_name;
                    jointPtr.m_localJointAxis = jointAxis;
                    jointPtr.m_parentLinkToJointTransform = parentLinkToJointTransform;
                    jointPtr.m_type = ejtype;
                    int numJoints = m_models[modelIndex].m_joints.Count;

                    //range
                    jointPtr.m_lowerLimit = range[0];
                    jointPtr.m_upperLimit = range[1];

                    if (nameStr != null)
                    {
                        Debug.Log("Parsed joint " + nameStr.Value);
                        jointPtr.m_name = nameStr.Value;
                    }
                    else
                    {
                        string jointName = string.Format("joint{0}_{1}_{2}", gUid++, linkIndex, numJoints);
                        jointPtr.m_name = jointName;
                    }
                    m_models[modelIndex].m_joints.Add(jointPtr.m_name, jointPtr);
                    return true;
                }

                //URDFRevoluteJoint=1,
                //URDFPrismaticJoint,
                //URDFContinuousJoint,
                //URDFFloatingJoint,
                //URDFPlanarJoint,
                //URDFFixedJoint,

                return false;
            }

           public bool parseGeom(MyMJCFDefaults defaults, XmlElement link_xml, int modelIndex, int linkIndex, MJCFErrorLogger logger, BulletSharp.Math.Vector3 inertialShift)
            {
                UrdfLink2 linkPtrPtr = (UrdfLink2) m_models[modelIndex].m_links[linkIndex];
                if (linkPtrPtr == null)
                {
                    // XXX: should it be assert?
                    logger.reportWarning("Invalide linkindex");
                    return false;
                }
                UrdfLink2 linkPtr = linkPtrPtr;

                Matrix linkLocalFrame = Matrix.Identity;


                bool handledGeomType = false;
                UrdfGeometry2 geom = new UrdfGeometry2();

                string sz = link_xml.Attributes["size"].Value;
                int conDim = defaults.m_defaultConDim;
                XmlAttribute conDimS = link_xml.Attributes[("condim")];
                {
                    if (conDimS != null)
                    {
                        conDim = int.Parse(conDimS.Value);
                    }
                }

                float lateralFriction = defaults.m_defaultLateralFriction;
                float spinningFriction = defaults.m_defaultSpinningFriction;
                float rollingFriction = defaults.m_defaultRollingFriction;

                XmlAttribute frictionS = link_xml.Attributes[("friction")];
                if (frictionS != null)
                {
                    List<float> frictions = new List<float>();
                    string[] pieces = frictionS.Value.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < pieces.Length; ++i)
                    {
                        if (pieces[i].Length != 0)
                        {
                            frictions.Add(float.Parse(pieces[i]));
                        }
                    }
                    if (frictions.Count > 0)
                    {
                        lateralFriction = frictions[0];
                    }
                    if (frictions.Count > 1 && conDim > 3)
                    {
                        spinningFriction = frictions[1];
                    }
                    if (frictions.Count > 2 && conDim > 4)
                    {
                        rollingFriction = frictions[2];
                    }

                }

                linkPtr.m_contactInfo.m_lateralFriction = lateralFriction;
                linkPtr.m_contactInfo.m_spinningFriction = spinningFriction;
                linkPtr.m_contactInfo.m_rollingFriction = rollingFriction;

                if (conDim > 3)
                {
                    linkPtr.m_contactInfo.m_spinningFriction = defaults.m_defaultSpinningFriction;
                    linkPtr.m_contactInfo.m_flags |= URDF_LinkContactFlags.URDF_CONTACT_HAS_SPINNING_FRICTION;
                }
                if (conDim > 4)
                {
                    linkPtr.m_contactInfo.m_rollingFriction = defaults.m_defaultRollingFriction;
                    linkPtr.m_contactInfo.m_flags |= URDF_LinkContactFlags.URDF_CONTACT_HAS_ROLLING_FRICTION;
                }

                string rgba = defaults.m_defaultGeomRgba;
                XmlAttribute rgbattr = link_xml.Attributes[("rgba")];
                if (rgbattr != null)
                {
                    rgba = rgbattr.Value;
                }
                if (rgba != null && rgba.Length != 0)
                {
                    // "0 0.7 0.7 1"
                    BulletSharp.Math.Vector4 tmp;
                    parseVector4(out tmp, rgba);
                    geom.m_localMaterial.m_matColor.m_rgbaColor = new Color(tmp[0], tmp[1], tmp[2], tmp[3]);
                    geom.m_hasLocalMaterial = true;
                    geom.m_localMaterial.m_name = rgba;
                }

                XmlAttribute posS = link_xml.Attributes[("pos")];
                if (posS != null)
                {
                    BulletSharp.Math.Vector3 pos = new BulletSharp.Math.Vector3(0, 0, 0);
                    string p = posS.Value;
                    if (parseVector3(out pos, p, logger))
                    {
                        linkLocalFrame.Origin = (pos);
                    }
                }

                XmlAttribute ornS = link_xml.Attributes[("quat")];
                if (ornS != null)
                {
                    BulletSharp.Math.Quaternion orn = new BulletSharp.Math.Quaternion(0, 0, 0, 1);
                    BulletSharp.Math.Vector4 o4;
                    if (parseVector4(out o4, ornS.Value))
                    {
                        orn = new BulletSharp.Math.Quaternion(o4[1], o4[2], o4[3], o4[0]);
                        linkLocalFrame.Rotation = (orn);
                    }
                }

                XmlAttribute axis_and_angle = link_xml.Attributes[("axisangle")];
                if (axis_and_angle != null)
                {
                    BulletSharp.Math.Quaternion orn = new BulletSharp.Math.Quaternion(0, 0, 0, 1);
                    BulletSharp.Math.Vector4 o4;
                    if (parseVector4(out o4, axis_and_angle.Value))
                    {
                        orn = new BulletSharp.Math.Quaternion(o4[0], o4[1], o4[2], o4[3]);
                        linkLocalFrame.Rotation = (orn);
                    }
                }

                XmlAttribute gType = link_xml.Attributes[("type")];
                if (gType != null)
                {
                    string geomType = gType.Value;


                    if (geomType == "plane")
                    {
                        geom.m_type = UrdfGeomTypes.URDF_GEOM_PLANE;
                        geom.m_planeNormal.SetValue(0, 0, 1);
                        BulletSharp.Math.Vector3 size = new BulletSharp.Math.Vector3(1, 1, 1);
                        if (sz != null)
                        {
                            string sizeStr = sz;
                            bool lastThree = false;
                            parseVector3(out size, sizeStr, logger, lastThree);
                        }
                        geom.m_boxSize = size;
                        handledGeomType = true;
                    }
                    if (geomType == "box")
                    {
                        BulletSharp.Math.Vector3 size = new BulletSharp.Math.Vector3(1, 1, 1);
                        if (sz != null)
                        {
                            string sizeStr = sz;
                            bool lastThree = false;
                            parseVector3(out size, sizeStr, logger, lastThree);
                        }
                        geom.m_type = UrdfGeomTypes.URDF_GEOM_BOX;
                        geom.m_boxSize = size;
                        handledGeomType = true;
                    }

                    if (geomType == "sphere")
                    {
                        geom.m_type = UrdfGeomTypes.URDF_GEOM_SPHERE;
                        if (sz != null)
                        {
                            geom.m_sphereRadius = float.Parse(sz);
                        }
                        else
                        {
                            logger.reportWarning((sourceFileLocation(link_xml) + ": no size field (scalar) in sphere geom"));
                        }
                        handledGeomType = true;
                    }

                    if (geomType == "capsule" || geomType == "cylinder")
                    {
                        // <geom conaffinity="0" contype="0" fromto="0 0 0 0 0 0.02" name="root" rgba="0.9 0.4 0.6 1" size=".011" type="cylinder"/>
                        geom.m_type = geomType == "cylinder" ? UrdfGeomTypes.URDF_GEOM_CYLINDER : UrdfGeomTypes.URDF_GEOM_CAPSULE;

                        List<float> sizes = new List<float>();
                        string[] pieces = sz.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < pieces.Length; ++i)
                        {
                            if (pieces[i].Length != 0)
                            {
                                sizes.Add(float.Parse(pieces[i]));
                            }
                        }

                        geom.m_capsuleRadius = 2.00f; // 2 to make it visible if something is wrong
                        geom.m_capsuleHeight = 2.00f;

                        if (sizes.Count > 0)
                        {
                            geom.m_capsuleRadius = sizes[0];
                            if (sizes.Count > 1)
                            {
                                geom.m_capsuleHeight = 2 * sizes[1];
                            }
                        }
                        else
                        {
                            logger.reportWarning((sourceFileLocation(link_xml) + ": couldn't convert 'size' attribute of capsule geom"));
                        }
                        XmlAttribute fromtoStr = link_xml.Attributes[("fromto")];
                        geom.m_hasFromTo = false;

                        if (fromtoStr != null)
                        {
                            geom.m_hasFromTo = true;
                            string fromto = fromtoStr.Value;
                            parseVector6(out geom.m_capsuleFrom, out geom.m_capsuleTo, fromto, logger);
                            inertialShift = 0.5f * (geom.m_capsuleFrom + geom.m_capsuleTo);
                            handledGeomType = true;
                        }
                        else
                        {
                            if (sizes.Count < 2)
                            {
                                logger.reportWarning((sourceFileLocation(link_xml) + ": capsule without fromto attribute requires 2 sizes (radius and halfheight)"));
                            }
                            else
                            {
                                handledGeomType = true;
                            }
                        }
                    }
                    if (geomType == "mesh")
                    {
                        string meshStr = link_xml.Attributes[("mesh")].Value;
                        if (meshStr != null)
                        {
                            MyMJCFAsset assetPtr = m_assets[meshStr];
                            if (assetPtr != null)
                            {
                                geom.m_type = UrdfGeomTypes.URDF_GEOM_MESH;
                                geom.m_meshFileName = assetPtr.m_fileName;
                                bool exists = findExistingMeshFile(
                                    m_sourceFileName, assetPtr.m_fileName, sourceFileLocation(link_xml),
                                    geom.m_meshFileName,
                                    geom.m_meshFileType);
                                handledGeomType = exists;

                                geom.m_meshScale.SetValue(1, 1, 1);
                                //todo: parse mesh scale
                                if (sz != null)
                                {
                                    
                                }
                            }
                        }
                    }
                    if (handledGeomType)
                    {

                        UrdfCollision2 col = new UrdfCollision2();
                        col.m_flags |= (int)UrdfCollisionFlags.URDF_HAS_COLLISION_GROUP;
                        col.m_collisionGroup = defaults.m_defaultCollisionGroup;

                        col.m_flags |= (int)UrdfCollisionFlags.URDF_HAS_COLLISION_MASK;
                        col.m_collisionMask = defaults.m_defaultCollisionMask;

                        //contype, conaffinity 
                        XmlAttribute conTypeStr = link_xml.Attributes[("contype")];
                        if (conTypeStr != null)
                        {
                            col.m_flags |= (int)UrdfCollisionFlags.URDF_HAS_COLLISION_GROUP;
                            col.m_collisionGroup = int.Parse(conTypeStr.Value);
                        }
                        XmlAttribute conAffinityStr = link_xml.Attributes[("conaffinity")];
                        if (conAffinityStr != null)
                        {
                            col.m_flags |= (int)UrdfCollisionFlags.URDF_HAS_COLLISION_MASK;
                            col.m_collisionMask = int.Parse(conAffinityStr.Value);
                        }

                        col.m_geometry = geom;
                        col.m_linkLocalFrame = linkLocalFrame;
                        col.m_sourceFileLocation = sourceFileLocation(link_xml);
                        linkPtr.m_collisionArray.Add(col);

                    }
                    else
                    {
                        logger.reportWarning((sourceFileLocation(link_xml) + ": unhandled geom type '" + geomType + "'"));
                    }
                }
                else
                {
                    logger.reportWarning((sourceFileLocation(link_xml) + ": geom requires type"));
                }

                return handledGeomType;
            }

            Matrix parseTransform(XmlElement link_xml, MJCFErrorLogger logger)
            {
                Matrix tr = Matrix.Identity;

                XmlAttribute p = link_xml.Attributes[("pos")];
                if (p != null)
                {
                    BulletSharp.Math.Vector3 pos = new BulletSharp.Math.Vector3(0, 0, 0);
                    string pstr = p.Value;
                    if (parseVector3(out pos, pstr, logger))
                    {
                        tr.Origin = (pos);
                    }

                }
                else
                {
                    //			logger.reportWarning("body should have pos attribute");
                }
                XmlAttribute o = link_xml.Attributes[("quat")];
                if (o != null)
                {
                    string ornstr = o.Value;
                    BulletSharp.Math.Vector4 o4;
                    BulletSharp.Math.Quaternion orn = new BulletSharp.Math.Quaternion(0, 0, 0, 1);
                    if (parseVector4(out o4, ornstr))
                    {
                        orn = new BulletSharp.Math.Quaternion(o4[1], o4[2], o4[3], o4[0]);//MuJoCo quats are [w,x,y,z], Bullet uses [x,y,z,w]
                        tr.Rotation = (orn);
                    }
                }
                else
                {
                    //			logger.reportWarning("body doesn't have quat (orientation) attribute");
                }
                return tr;
            }

            float computeVolume(UrdfLink2 linkPtr, MJCFErrorLogger logger)
            {

                float totalVolume = 0;

                for (int i = 0; i < linkPtr.m_collisionArray.Count; i++)
                {
                    UrdfCollision2 col = linkPtr.m_collisionArray[i];
                    switch (col.m_geometry.m_type)
                    {
                        case UrdfGeomTypes.URDF_GEOM_SPHERE:
                            {
                                float r = col.m_geometry.m_sphereRadius;
                                totalVolume += 4.0f / 3.0f * Mathf.PI * r * r * r;
                                break;
                            }
                        case UrdfGeomTypes.URDF_GEOM_BOX:
                            {
                                totalVolume += 8.0f * col.m_geometry.m_boxSize[0] *

                                    col.m_geometry.m_boxSize[1] *

                                    col.m_geometry.m_boxSize[2];
                                break;
                            }
                        case UrdfGeomTypes.URDF_GEOM_MESH:
                            {
                                //todo (based on mesh bounding box?)
                                break;
                            }
                        case UrdfGeomTypes.URDF_GEOM_PLANE:
                            {
                                //todo
                                break;
                            }
                        case UrdfGeomTypes.URDF_GEOM_CYLINDER:
                        case UrdfGeomTypes.URDF_GEOM_CAPSULE:
                            {
                                //one sphere 
                                float r = col.m_geometry.m_capsuleRadius;
                                if (col.m_geometry.m_type == UrdfGeomTypes.URDF_GEOM_CAPSULE)
                                {
                                    totalVolume += 4.0f / 3.0f * Mathf.PI * r * r * r;
                                }
                                float h = (0);
                                if (col.m_geometry.m_hasFromTo)
                                {
                                    //and one cylinder of 'height'
                                    h = (col.m_geometry.m_capsuleFrom - col.m_geometry.m_capsuleTo).Length;
                                }
                                else
                                {
                                    h = col.m_geometry.m_capsuleHeight;
                                }
                                totalVolume += Mathf.PI * r * r * h;
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }

                return totalVolume;
            }

            public UrdfLink2 getLink(int modelIndex, int linkIndex)
            {
                UrdfLink2 linkPtrPtr = (UrdfLink2) m_models[modelIndex].m_links[linkIndex];
                if (linkPtrPtr != null && linkPtrPtr != null)
                {
                    return linkPtrPtr;
                }
                Debug.Assert(false);
                return null;
            }

            int createBody(int modelIndex, string namePtr, bool isJointBody = false)
            {
                
                UrdfModel2 modelPtr = m_models[modelIndex];
                int orgChildLinkIndex = modelPtr.m_links.Count;
                UrdfLink2 linkPtr = new UrdfLink2();
                string linkn = string.Format("link{0}_{1}", modelIndex, orgChildLinkIndex);
                
                linkPtr.m_name = linkn;
                if (namePtr != null)
                {
                    linkPtr.m_name = namePtr;
                }
                if (isJointBody) linkn = "JOINT_" + linkn;
                linkPtr.m_isJointLink = isJointBody;
                Debug.Log("Creating body " + linkPtr.m_name);
                linkPtr.m_linkIndex = orgChildLinkIndex;
                if (modelPtr.m_links.Contains(linkPtr.m_name)) Debug.LogError("CONTAINS!!!");
                modelPtr.m_links.Add(linkPtr.m_name, linkPtr);

                return orgChildLinkIndex;
            }

            bool parseBody(MyMJCFDefaults defaults, XmlElement link_xml, int modelIndex, int orgParentLinkIndex, MJCFErrorLogger logger)
            {
                MyMJCFDefaults curDefaults = defaults;

                int newParentLinkIndex = orgParentLinkIndex;

                XmlAttribute childClassName = link_xml.Attributes[("childclass")];
                if (childClassName != null)
                {
                    MyMJCFDefaults classDefaults = m_classDefaults[childClassName.Value];
                    if (classDefaults != null)
                    {
                        curDefaults = classDefaults;
                    }
                }
                string bodyName = link_xml.Attributes[("name")].Value;
                int orgChildLinkIndex = createBody(modelIndex, bodyName);
                Matrix localInertialFrame = Matrix.Identity;


                //		int curChildLinkIndex = orgChildLinkIndex;
                string bodyN;

                if (bodyName != null)
                {
                    bodyN = bodyName;
                }
                else
                {
                    string anon = string.Format("anon{0}", gUid++);
                    bodyN = anon;
                }


                //		Matrix orgLinkTransform = parseTransform(link_xml,logger);

                Matrix linkTransform = parseTransform(link_xml, logger);
                UrdfLink2 linkPtr = getLink(modelIndex, orgChildLinkIndex);



                bool massDefined = false;


                float mass = 0.0f;
                BulletSharp.Math.Vector3 localInertiaDiag = new BulletSharp.Math.Vector3(0, 0, 0);
                //	int thisLinkIndex = -2;
                bool hasJoint = false;
                Matrix jointTrans = Matrix.Identity;
                bool skipFixedJoint = false;

                foreach (XmlElement xml in link_xml)
                {
                    bool handled = false;
                    string n = xml.Name;
                    if (n == "inertial")
                    {
                        //   <inertial pos="0 0 0" quat="0.5 0.5 -0.5 0.5" mass="297.821" diaginertia="109.36 69.9714 69.9714" />

                        string p = xml.Attributes[("pos")].Value;
                        if (p != null)
                        {
                            string posStr = p;
                            BulletSharp.Math.Vector3 inertialPos = new BulletSharp.Math.Vector3(0, 0, 0);
                            if (parseVector3(out inertialPos, posStr, logger))
                            {
                                localInertialFrame.Origin = (inertialPos);
                            }
                        }
                        string o = xml.Attributes[("quat")].Value;
                        if (o != null)
                        {
                            string ornStr = o;
                            BulletSharp.Math.Quaternion orn = new BulletSharp.Math.Quaternion(0, 0, 0, 1);
                            BulletSharp.Math.Vector4 o4;
                            if (parseVector4(out o4, ornStr))
                            {
                                orn = new BulletSharp.Math.Quaternion(o4[1], o4[2], o4[3], o4[0]);
                                localInertialFrame.Rotation = (orn);
                            }
                        }
                        string m = xml.Attributes[("mass")].Value;
                        if (m != null)
                        {
                            mass = float.Parse(m);
                        }
                        string i = xml.Attributes[("diaginertia")].Value;
                        if (i != null)
                        {
                            string istr = i;

                            parseVector3(out localInertiaDiag, istr, logger);
                        }

                        massDefined = true;

                        handled = true;
                    }

                    if (n == "joint")
                    {
                        XmlAttribute jointNameAtr = xml.Attributes["name"];
                        string jointNameStr = null;
                        if (jointNameAtr != null)
                        {
                            jointNameStr = jointNameAtr.Value;
                        }
                        if (!hasJoint)
                        {
                            string jType = xml.Attributes[("type")].Value;
                            string jointType = jType != null ? jType : "";
                            if (newParentLinkIndex != (int)ePARENT_LINK_ENUMS.INVALID_LINK_INDEX || jointType != "free")
                            {
                                if (newParentLinkIndex == (int)ePARENT_LINK_ENUMS.INVALID_LINK_INDEX)
                                {
                                    int newRootLinkIndex = createBody(modelIndex, jointNameStr,true);
                                    UrdfLink2 rootLink = getLink(modelIndex, newRootLinkIndex);
                                    rootLink.m_inertia.m_mass = 0;
                                    rootLink.m_linkTransformInWorld = Matrix.Identity;
                                    newParentLinkIndex = newRootLinkIndex;
                                }

                                int newLinkIndex = createBody(modelIndex, jointNameStr, true);

                                parseJoint(curDefaults, xml, modelIndex, newParentLinkIndex, newLinkIndex, logger, linkTransform, out jointTrans);

                                //getLink(modelIndex,newLinkIndex).m_linkTransformInWorld = jointTrans*linkTransform;

                                linkTransform = jointTrans.Inverse();
                                newParentLinkIndex = newLinkIndex;
                                //newParentLinkIndex, curChildLinkIndex
                                hasJoint = true;
                                handled = true;
                            }
                        }
                        else
                        {
                            int newLinkIndex = createBody(modelIndex, jointNameStr, true);
                            Matrix joint2nextjoint = jointTrans.Inverse();
                            Matrix unused = new Matrix();

                            parseJoint(curDefaults, xml, modelIndex, newParentLinkIndex, newLinkIndex, logger, joint2nextjoint, out unused);
                            newParentLinkIndex = newLinkIndex;
                            //todo: compute relative joint transforms (if any) and append to linkTransform
                            hasJoint = true;
                            handled = true;
                        }

                    }
                    if (n == "geom")
                    {
                        BulletSharp.Math.Vector3 inertialShift = new BulletSharp.Math.Vector3(0, 0, 0);

                        parseGeom(curDefaults, xml, modelIndex, orgChildLinkIndex, logger, inertialShift);
                        if (!massDefined)
                        {
                            localInertialFrame.Origin = (inertialShift);
                        }
                        handled = true;
                    }
                     
                    //recursive
                    if (n == "body")
                    {

                        parseBody(curDefaults, xml, modelIndex, orgChildLinkIndex, logger);
                        handled = true;
                    }

                    if (n == "light")
                    {
                        handled = true;
                    }
                    if (n == "site")
                    {
                        handled = true;
                    }

                    if (!handled)
                    {
                        logger.reportWarning((sourceFileLocation(xml) + ": unknown field '" + n + "'"));
                    }
                }

                linkPtr.m_linkTransformInWorld = linkTransform;

                if ((newParentLinkIndex != (int)ePARENT_LINK_ENUMS.INVALID_LINK_INDEX) && !skipFixedJoint)
                {
                    //linkPtr.m_linkTransformInWorld.setIdentity();
                    //default to 'fixed' joint
                    UrdfJoint2 jointPtr = new UrdfJoint2();
                    jointPtr.m_childLinkName = linkPtr.m_name;
                    UrdfLink2 parentLink = getLink(modelIndex, newParentLinkIndex);
                    jointPtr.m_parentLinkName = parentLink.m_name;
                    jointPtr.m_localJointAxis.SetValue(1, 0, 0);
                    jointPtr.m_parentLinkToJointTransform = linkTransform;
                    jointPtr.m_type = UrdfJointTypes.URDFFixedJoint;

                    string jointName = string.Format("jointfix_{0}_{1}", gUid++, newParentLinkIndex);
                    jointPtr.m_name = jointName;
                    m_models[modelIndex].m_joints.Add(jointPtr.m_name, jointPtr);
                }

                //check mass/inertia
                if (!massDefined)
                {
                    float density = 1000;
                    float volume = computeVolume(linkPtr, logger);
                    mass = density * volume;
                }
                linkPtr.m_inertia.m_linkLocalFrame = localInertialFrame;// = jointTrans.inverse();
                linkPtr.m_inertia.m_mass = mass;
                return true;
            }

            void recurseAddChildLinks(UrdfModel2 model, UrdfLink2 link)
            {
                for (int i = 0; i < link.m_childLinks.Count; i++)
                {
                    int linkIndex = model.m_links.Count;
                    link.m_childLinks[i].m_linkIndex = linkIndex;
                    string linkName = link.m_childLinks[i].m_name;
                    if (model.m_links.Contains(linkName)) Debug.LogError("CONTAINS!!!");
                    model.m_links.Add(linkName, link.m_childLinks[i]);
                }
                for (int i = 0; i < link.m_childLinks.Count; i++)
                {
                    recurseAddChildLinks(model, link.m_childLinks[i]);
                }
            }

            bool initTreeAndRoot(UrdfModel2 model, MJCFErrorLogger logger)
            {
                // every link has children links and joints, but no parents, so we create a
                // local convenience data structure for keeping child.parent relations
                Dictionary<string, string> parentLinkTree = new Dictionary<string, string>();

                // loop through all joints, for every link, assign children links and children joints
                for (int i = 0; i < model.m_joints.Count; i++)
                {
                    UrdfJoint2 jointPtr = (UrdfJoint2) model.m_joints[i];
                    if (jointPtr != null)
                    {
                        UrdfJoint2 joint = jointPtr;
                        string parent_link_name = joint.m_parentLinkName;
                        string child_link_name = joint.m_childLinkName;
                        if (parent_link_name.Length == 0 || child_link_name.Length == 0)
                        {
                            logger.reportError("parent link or child link is empty for joint");
                            logger.reportError(joint.m_name);
                            return false;
                        }

                        UrdfLink2 childLinkPtr = (UrdfLink2) model.m_links[joint.m_childLinkName];
                        if (childLinkPtr == null)
                        {
                            logger.reportError("Cannot find child link for joint ");
                            logger.reportError(joint.m_name);

                            return false;
                        }
                        UrdfLink2 childLink = childLinkPtr;

                        UrdfLink2 parentLinkPtr = (UrdfLink2) model.m_links[joint.m_parentLinkName];
                        if (parentLinkPtr == null)
                        {
                            logger.reportError("Cannot find parent link for a joint");
                            logger.reportError(joint.m_name);
                            return false;
                        }
                        UrdfLink2 parentLink = parentLinkPtr;

                        childLink.m_parentLink = parentLink;

                        childLink.m_parentJoint = joint;
                        parentLink.m_childJoints.Add(joint);
                        parentLink.m_childLinks.Add(childLink);
                        parentLinkTree.Add(childLink.m_name, parentLink.m_name);
                    }
                }

                //search for children that have no parent, those are 'root'
                for (int i = 0; i < model.m_links.Count; i++)
                {
                    UrdfLink2 linkPtr = (UrdfLink2) model.m_links[i];
                    Debug.Assert(linkPtr != null);
                    if (linkPtr != null)
                    {
                        UrdfLink2 link = linkPtr;
                        link.m_linkIndex = i;

                        if (link.m_parentLink == null)
                        {
                            model.m_rootLinks.Add(link);
                        }
                    }

                }

                if (model.m_rootLinks.Count > 1)
                {
                    logger.reportWarning("URDF file with multiple root links found");
                }

                if (model.m_rootLinks.Count == 0)
                {
                    logger.reportError("URDF without root link found");
                    return false;
                }

                //re-index the link indices so parent indices are always smaller than child indices
                UrdfLink2[] links = new UrdfLink2[model.m_links.Count];
                for (int i = 0; i < model.m_links.Count; i++)
                {
                    links[i] = (UrdfLink2) model.m_links[i];
                }
                model.m_links.Clear();
                for (int i = 0; i < model.m_rootLinks.Count; i++)
                {
                    UrdfLink2 rootLink = model.m_rootLinks[i];
                    int linkIndex = model.m_links.Count;
                    rootLink.m_linkIndex = linkIndex;
                    if (model.m_links.Contains(rootLink.m_name)) Debug.LogError("CONTAINS!!!");
                    model.m_links.Add(rootLink.m_name, rootLink);
                    recurseAddChildLinks(model, rootLink);
                }
                return true;

            }

        };

        BulletMJCFImporterInternalData m_data;

        public BulletMJCFImporter(Material mat)
        {
            material = mat;
            m_data = new BulletMJCFImporterInternalData();
            //m_data.m_guiHelper = helper;
            //m_data.m_customVisualShapesConverter = customConverter;
        }

        public bool loadURDF(string fileName, bool forceFixedBase = false)
        {
            Debug.LogError("Not Implemented");
            return false;
        }

        public bool loadSDF(string fileName, bool forceFixedBase = false)
        {
            Debug.LogError("Not Implemented");
            return false;
        }

        public bool loadMJCF(string fileName, MJCFErrorLogger logger, bool forceFixedBase)
        {
            if (fileName.Length == 0)
                return false;

            if (!System.IO.File.Exists(fileName))
            {
                Debug.LogError("MJCF file not found");
                return false;
            }
            else
            {

                string xml_string = System.IO.File.ReadAllText(fileName);
                if (parseMJCFString(xml_string, logger))
                {
                    return true;
                }
            }

            return false;
        }

        bool parseMJCFString(string xmlText, MJCFErrorLogger logger)
        {
            XmlDocument xml_doc = new XmlDocument();
            xml_doc.LoadXml(xmlText);
            //xml_doc.Parse(xmlText);
            /*
            if (xml_doc.Error())
            {
                //logger.reportError(xml_doc.ErrorDesc());
                //xml_doc.ClearError();
                return false;
            }
            */

            XmlElement mujoco_xml = xml_doc["mujoco"];
            if (mujoco_xml == null)
            {
                logger.reportWarning("Cannot find <mujoco> root element");
                return false;
            }

            Debug.Log("found root element " + mujoco_xml.Value);
            string modelName = mujoco_xml.Attributes[("model")].Value;
            if (modelName != null)
            {
                m_data.m_fileModelName = modelName;
            }

            //<compiler>,<option>,<size>,<default>,<body>,<keyframe>,<contactpair>,
            //<light>, <camera>,<constraint>,<tendon>,<actuator>,<customfield>,<textfield>

            if (mujoco_xml[("default")] != null)
            {
                m_data.parseDefaults(out m_data.m_globalDefaults, mujoco_xml[("default")], logger);
            }

            foreach (XmlElement link_xml in mujoco_xml[("compiler")])
            {
                m_data.parseCompiler(link_xml, logger);
            }
            foreach (XmlElement link_xml in mujoco_xml[("asset")])
            {
                m_data.parseAssets(link_xml, logger);
            }

            if (mujoco_xml["body"] != null)
            {
                foreach (XmlElement link_xml in mujoco_xml[("body")])
                {
                    m_data.parseRootLevel(m_data.m_globalDefaults, link_xml, logger);
                }
            }
            if (mujoco_xml["worldbody"] != null)
            {
                foreach (XmlElement link_xml in mujoco_xml[("worldbody")])
                {
                    m_data.parseRootLevel(m_data.m_globalDefaults, link_xml, logger);
                }
            }



            return true;
        }

        public string getPathPrefix()
        {
            return m_data.m_pathPrefix;
        }

        public int getRootLinkIndex()
        {
            if (m_data.m_activeModel >= 0 && m_data.m_activeModel < m_data.m_models.Count)
            {
                if (m_data.m_models[m_data.m_activeModel].m_rootLinks.Count != 0)
                {
                    return 0;
                }
            }
            return -1;
        }


        public string getLinkName(int linkIndex)
        {

            UrdfLink2 link = m_data.getLink(m_data.m_activeModel, linkIndex);
            if (link != null)
            {
                return link.m_name;
            }
            return "";
        }

        public string getBodyName()
        {
            return m_data.m_fileModelName;
        }

        public bool getLinkColor(int linkIndex, out Color colorRGBA)
        {
            //	UrdfLink2* link = m_data.getLink(linkIndex);
            colorRGBA = Color.gray;
            return true;
        }

        public bool getLinkColor2(int linkIndex, out UrdfMaterialColor matCol)
        {
            matCol = new UrdfMaterialColor();
            matCol.m_rgbaColor = Color.gray;
            matCol.m_specularColor = Color.gray;
            return true;
        }

        //todo: placeholder implementation
        //MuJoCo type/affinity is different from Bullet group/mask, so we should implement a custom collision filter instead
        //(contype1 & conaffinity2) || (contype2 & conaffinity1)
        public UrdfCollisionFlags getCollisionGroupAndMask(int linkIndex, out CollisionFilterGroups colGroup, out CollisionFilterGroups colMask)
        {
            UrdfCollisionFlags flags = 0;
            colGroup = CollisionFilterGroups.DefaultFilter;
            colMask = CollisionFilterGroups.Everything;
            UrdfLink2 link = m_data.getLink(m_data.m_activeModel, linkIndex);
            if (link != null)
            {
                for (int i = 0; i < link.m_collisionArray.Count; i++)
                {
                    UrdfCollision2 col = link.m_collisionArray[i];
                    colGroup = (CollisionFilterGroups) col.m_collisionGroup;
                    flags |= UrdfCollisionFlags.URDF_HAS_COLLISION_GROUP;
                    colMask = (CollisionFilterGroups) col.m_collisionMask;
                    flags |= UrdfCollisionFlags.URDF_HAS_COLLISION_MASK;

                }
            }

            return flags;
        }


        public string getJointName(int linkIndex)
        {

            UrdfLink2 link = m_data.getLink(m_data.m_activeModel, linkIndex);
            if (link != null)
            {
                if (link.m_parentJoint != null)
                {
                    return link.m_parentJoint.m_name;
                }
                return link.m_name;
            }
            return "";
        }

        //fill mass and inertial data. If inertial data is missing, please initialize mass, inertia to sensitive values, and inertialFrame to identity.
        public void getMassAndInertia(int urdfLinkIndex, out float mass, out BulletSharp.Math.Vector3 localInertiaDiagonal, out Matrix inertialFrame)
        {
            UrdfLink2 link = m_data.getLink(m_data.m_activeModel, urdfLinkIndex);

            if (link != null)
            {
                mass = link.m_inertia.m_mass;
                localInertiaDiagonal = new BulletSharp.Math.Vector3(link.m_inertia.m_ixx,
                    link.m_inertia.m_iyy,
                    link.m_inertia.m_izz);
                inertialFrame = Matrix.Identity;
                inertialFrame = link.m_inertia.m_linkLocalFrame;
            }
            else
            {
                mass = 0;
                localInertiaDiagonal = BulletSharp.Math.Vector3.Zero;
                inertialFrame = Matrix.Identity;
            }
        }

        ///fill an array of child link indices for this link, btAlignedObjectArray behaves like a std.vector so just use Add and resize(0) if needed
        public void getLinkChildIndices(int urdfLinkIndex, List<int> childLinkIndices)
        {
            UrdfLink2 link = m_data.getLink(m_data.m_activeModel, urdfLinkIndex);
            if (link != null)
            {
                for (int i = 0; i < link.m_childLinks.Count; i++)
                {
                    int childIndex = link.m_childLinks[i].m_linkIndex;
                    childLinkIndices.Add(childIndex);
                }
            }
        }

        public bool getJointInfo2(int urdfLinkIndex, out Matrix parent2joint, out Matrix linkTransformInWorld, out BulletSharp.Math.Vector3 jointAxisInJointSpace, out UrdfJointTypes jointType, out float jointLowerLimit, out float jointUpperLimit, out float jointDamping, out float jointFriction, out float jointMaxForce, out float jointMaxVelocity)
        {
            jointLowerLimit = 0.0f;
            jointUpperLimit = 0.0f;
            jointDamping = 0.0f;
            jointFriction = 0.0f;
            jointMaxForce = 0f;
            jointMaxVelocity = 0f;
            jointType = UrdfJointTypes.URDFFixedJoint;
            jointAxisInJointSpace = BulletSharp.Math.Vector3.UnitX;
            parent2joint = Matrix.Identity;
            linkTransformInWorld = Matrix.Identity;
            UrdfLink2 link = m_data.getLink(m_data.m_activeModel, urdfLinkIndex);
            if (link != null)
            {

                linkTransformInWorld = link.m_linkTransformInWorld;

                if (link.m_parentJoint != null)
                {
                    UrdfJoint2 pj = link.m_parentJoint;
                    parent2joint = pj.m_parentLinkToJointTransform;
                    jointType = pj.m_type;
                    jointAxisInJointSpace = pj.m_localJointAxis;
                    jointLowerLimit = pj.m_lowerLimit;
                    jointUpperLimit = pj.m_upperLimit;
                    jointDamping = pj.m_jointDamping;
                    jointFriction = pj.m_jointFriction;
                    jointMaxForce = pj.m_effortLimit;
                    jointMaxVelocity = pj.m_velocityLimit;

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

        public bool getJointInfo(int urdfLinkIndex, out Matrix parent2joint, out Matrix linkTransformInWorld, out BulletSharp.Math.Vector3 jointAxisInJointSpace, out UrdfJointTypes jointType, out float jointLowerLimit, out float jointUpperLimit, out float jointDamping, out float jointFriction)
        {
            //backwards compatibility for custom file importers
            float jointMaxForce = 0;
            float jointMaxVelocity = 0;
            return getJointInfo2(urdfLinkIndex, out parent2joint, out linkTransformInWorld, out jointAxisInJointSpace, out jointType, out jointLowerLimit, out jointUpperLimit, out jointDamping, out jointFriction, out jointMaxForce, out jointMaxVelocity);
        }

        public bool getRootTransformInWorld(out Matrix rootTransformInWorld)
        {
            rootTransformInWorld = Matrix.Identity;

            return true;
        }

        public void setRootTransformInWorld(ref Matrix rootTransformInWorld)
        {
            Debug.LogError("Not implemented");
        }

        public int convertLinkVisualShapes(int linkIndex, string pathPrefix, ref Matrix inertialFrame, GameObject go)
        {
            return -1;
        }

        public bool getLinkContactInfo(int linkIndex, out URDFLinkContactInfo contactInfo)
        {
            UrdfLink2 link = m_data.getLink(m_data.m_activeModel, linkIndex);
            if (link != null)
            {
                contactInfo = link.m_contactInfo;
                return true;
            }
            contactInfo = new URDFLinkContactInfo();
            return false;
        }

        public int convertLinkVisualShapes(int linkIndex, string pathPrefix, ref Matrix inertialFrame)
        {
            return -1;
        }

        public void convertLinkVisualShapes2(int linkIndex, int urdfIndex, string pathPrefix, ref Matrix inertialFrame, CollisionObject colObj, int objectIndex)
        {
            /*
            if (m_data.m_customVisualShapesConverter)
            {
                UrdfLink2 link = m_data.getLink(m_data.m_activeModel, urdfIndex);
                m_data.m_customVisualShapesConverter.convertVisualShapes(linkIndex, pathPrefix, inertialFrame, link, 0, colObj, objectIndex);
            }
            */
        }

        public void setBodyUniqueId(int bodyId)
        {
            m_data.m_activeBodyUniqueId = bodyId;
        }

        public int getBodyUniqueId()
        {

            Debug.Assert(m_data.m_activeBodyUniqueId != -1);
            return m_data.m_activeBodyUniqueId;
        }

        /*
        static CollisionShape MjcfCreateConvexHullFromShapes(List<tinyobj.shape_t> shapes, BulletSharp.Math.Vector3 geomScale, float collisionMargin)
        {
            CompoundShape compound = new CompoundShape();
            compound.Margin = (collisionMargin);

            Matrix identity = Matrix.Identity;

            for (int s = 0; s < (int)shapes.Count; s++)
            {
                ConvexHullShape convexHull = new ConvexHullShape();
                convexHull.Margin = (collisionMargin);
                tinyobj.shape_t & shape = shapes[s];
                int faceCount = shape.mesh.indices.Count;

                for (int f = 0; f < faceCount; f += 3)
                {

                    BulletSharp.Math.Vector3 pt;
                    pt.SetValue(shape.mesh.positions[shape.mesh.indices[f] * 3 + 0],
                        shape.mesh.positions[shape.mesh.indices[f] * 3 + 1],
                        shape.mesh.positions[shape.mesh.indices[f] * 3 + 2]);

                    convexHull.addPoint(pt * geomScale, false);

                    pt.SetValue(shape.mesh.positions[shape.mesh.indices[f + 1] * 3 + 0],
                                shape.mesh.positions[shape.mesh.indices[f + 1] * 3 + 1],
                                shape.mesh.positions[shape.mesh.indices[f + 1] * 3 + 2]);
                    convexHull.addPoint(pt * geomScale, false);

                    pt.SetValue(shape.mesh.positions[shape.mesh.indices[f + 2] * 3 + 0],
                                shape.mesh.positions[shape.mesh.indices[f + 2] * 3 + 1],
                                shape.mesh.positions[shape.mesh.indices[f + 2] * 3 + 2]);
                    convexHull.addPoint(pt * geomScale, false);
                }

                convexHull.recalcLocalAabb();
                convexHull.optimizeConvexHull();
                compound.AddChildShape(identity, convexHull);
            }

            return compound;
        }
        */


        public BCollisionShape convertLinkCollisionShapes(int linkIndex, string pathPrefix,ref Matrix localInertiaFrame, GameObject go)
        {
            bool addVisualShape = true;
            BCompoundShape compoundShape = null;
            UrdfLink2 link = m_data.getLink(m_data.m_activeModel, linkIndex);
            if (link != null)
            {
                compoundShape = go.AddComponent<BCompoundShape>();
                for (int i = 0; i < link.m_collisionArray.Count; i++)
                {
                    BCollisionShape childShape = null;
                    UrdfCollision2 col = link.m_collisionArray[i];
                    GameObject colGo = new GameObject("collisionGO");
                    colGo.transform.parent = go.transform;
                    colGo.transform.localPosition = col.m_linkLocalFrame.Origin.ToUnity();
                    colGo.transform.localRotation = col.m_linkLocalFrame.Rotation.ToUnity();
                    switch (col.m_geometry.m_type)
                    {
                        case UrdfGeomTypes.URDF_GEOM_PLANE:
                            {
                                Debug.LogError("Not implemented");
                                //childShape = new StaticPlaneShape(col.m_geometry.m_planeNormal, 0);
                                break;
                            }
                        case UrdfGeomTypes.URDF_GEOM_SPHERE:
                            {
                                
                                BSphereShape bs = colGo.AddComponent<BSphereShape>();
                                bs.Radius = col.m_geometry.m_sphereRadius;
                                childShape = bs;
                                if (addVisualShape)
                                {
                                    BulletUnity.Primitives.BSphere bss = colGo.AddComponent<BulletUnity.Primitives.BSphere>();
                                    bss.meshSettings.radius = bs.Radius;
                                    bss.BuildMesh();
                                    bss.GetComponent<MeshRenderer>().sharedMaterial = material;
                                }
                                break;
                            }
                        case UrdfGeomTypes.URDF_GEOM_BOX:
                            {
                                BBoxShape bs = colGo.AddComponent<BBoxShape>();
                                bs.Extents = col.m_geometry.m_boxSize.ToUnity();
                                childShape = bs;
                                if (addVisualShape)
                                {
                                    BulletUnity.Primitives.BBox bss = colGo.AddComponent<BulletUnity.Primitives.BBox>();
                                    bss.meshSettings.extents = bs.Extents;
                                    bss.BuildMesh();
                                    bss.GetComponent<MeshRenderer>().sharedMaterial = material;
                                }
                                break;
                            }
                        case UrdfGeomTypes.URDF_GEOM_CYLINDER:
                            {
                                if (col.m_geometry.m_hasFromTo)
                                {
                                    BulletSharp.Math.Vector3 f = col.m_geometry.m_capsuleFrom;
                                    BulletSharp.Math.Vector3 t = col.m_geometry.m_capsuleTo;

                                    //compute the local 'fromto' transform
                                    BulletSharp.Math.Vector3 localPosition = 0.5f * (t + f);
                                    BulletSharp.Math.Quaternion localOrn;
                                    localOrn = BulletSharp.Math.Quaternion.Identity;

                                    BulletSharp.Math.Vector3 diff = t - f;
                                    float lenSqr = diff.LengthSquared;
                                    float height = 0.0f;

                                    if (lenSqr > SIM_EPSILON)
                                    {
                                        height = Mathf.Sqrt(lenSqr);
                                        BulletSharp.Math.Vector3 ax = diff / height;

                                        BulletSharp.Math.Vector3 zAxis = new BulletSharp.Math.Vector3(0, 0, 1);
                                        localOrn = UnityEngine.Quaternion.FromToRotation(zAxis.ToUnity(), ax.ToUnity()).ToBullet(); // shortestArcQuat(zAxis, ax);
                                    }
                                    CylinderShapeZ cyl = new CylinderShapeZ(new BulletSharp.Math.Vector3(col.m_geometry.m_capsuleRadius, col.m_geometry.m_capsuleRadius, 0.5f * height));

                                    //compound = new CompoundShape();
                                    //Matrix localTransform = Matrix.AffineTransformation(1f, localOrn, localPosition);
                                    //compound.AddChildShape(localTransform, cyl);
                                    //childShape = compound;
                                    //---------------
                                    BCylinderShape cs = colGo.AddComponent<BCylinderShape>();
                                    cs.HalfExtent = new BulletSharp.Math.Vector3(col.m_geometry.m_capsuleRadius, col.m_geometry.m_capsuleRadius, 0.5f * height).ToUnity();
                                    childShape = cs;
                                    Debug.LogError("TODO not finished. Need to use a compound shape with and offset child.");
                                    if (addVisualShape)
                                    {
                                        Debug.LogError("TODO visual shape for cylinder");
                                    }
                                }
                                else
                                {
                                    CylinderShapeZ cap = new CylinderShapeZ(new BulletSharp.Math.Vector3(col.m_geometry.m_capsuleRadius,
                                                                col.m_geometry.m_capsuleRadius, 0.5f * col.m_geometry.m_capsuleHeight));
                                    BCylinderShape cs = new BCylinderShape();
                                    cs.HalfExtent = new BulletSharp.Math.Vector3(col.m_geometry.m_capsuleRadius,
                                                                col.m_geometry.m_capsuleRadius, 0.5f * col.m_geometry.m_capsuleHeight).ToUnity();
                                    if (addVisualShape)
                                    {
                                        BulletUnity.Primitives.BCylinder bss = colGo.AddComponent<BulletUnity.Primitives.BCylinder>();
                                        bss.meshSettings.height = cs.HalfExtent.z;
                                        bss.meshSettings.radius = cs.HalfExtent.x;
                                        bss.BuildMesh();
                                        bss.GetComponent<MeshRenderer>().sharedMaterial = material;
                                    }
                                    childShape = cs;
                                }
                                break;
                            }
                        case UrdfGeomTypes.URDF_GEOM_MESH:
                            {
                                Debug.LogError("NotImplemented");
                                /*
                                GLInstanceGraphicsShape glmesh = null;
                                switch ((MeshFileFormat) col.m_geometry.m_meshFileType)
                                {
                                    case MeshFileFormat.FILE_OBJ:
                                        {
                                            if (col.m_flags & UrdfCollisionFlags.URDF_FORCE_CONCAVE_TRIMESH)
                                            {
                                                glmesh = LoadMeshFromObj(col.m_geometry.m_meshFileName, 0);
                                            }
                                            else
                                            {
                                                
                                                
                                                List<tinyobj.shape_t> shapes = new List<tinyobj.shape_t>();
                                                string err = tinyobj.LoadObj(shapes, col.m_geometry.m_meshFileName);
                                                //create a convex hull for each shape, and store it in a btCompoundShape

                                                childShape = MjcfCreateConvexHullFromShapes(shapes, col.m_geometry.m_meshScale, m_data.m_globalDefaults.m_defaultCollisionMargin);
                                                
                                            }
                                            break;
                                        }
                                    case MeshFileFormat.FILE_STL:
                                        {
                                            glmesh = LoadMeshFromSTL(col.m_geometry.m_meshFileName);
                                            break;
                                        }
                                    default:
                                        {

                                            Debug.LogWarningFormat("{0}: Unsupported file type in Collision: {1} (maybe .dae?)\n", col.m_sourceFileLocation, col.m_geometry.m_meshFileType);
                                            break;
                                        }
                                }
                                

                                if (childShape != null)
                                {
                                    // okay!
                                }
                                else if (glmesh == null || glmesh.m_numvertices <= 0)
                                {

                                    Debug.LogWarningFormat("{0}: cannot extract anything useful from mesh '{1}'\n", col.m_sourceFileLocation, col.m_geometry.m_meshFileName);
                                }
                                else
                                {
                                    //b3Printf("extracted %d verticed from STL file %s\n", glmesh.m_numvertices,fullPath);
                                    //int shapeId = m_glApp.m_instancingRenderer.registerShape(&gvertices[0].pos[0],gvertices.Count,&indices[0],indices.Count);
                                    //convex.setUserIndex(shapeId);
                                    List<BulletSharp.Math.Vector3> convertedVerts = new List<BulletSharp.Math.Vector3>();
                                    //convertedVerts.reserve(glmesh.m_numvertices);
                                    for (i = 0; i < glmesh.m_numvertices; i++)
                                    {
                                        convertedVerts.Add(new BulletSharp.Math.Vector3(
                                            glmesh.m_vertices.at(i).xyzw[0] * col.m_geometry.m_meshScale[0],
                                            glmesh.m_vertices.at(i).xyzw[1] * col.m_geometry.m_meshScale[1],
                                            glmesh.m_vertices.at(i).xyzw[2] * col.m_geometry.m_meshScale[2]));
                                    }

                                    if (col.m_flags & (int) UrdfCollisionFlags.URDF_FORCE_CONCAVE_TRIMESH)
                                    {

                                        TriangleMesh meshInterface = new TriangleMesh();
                                        m_data.m_allocatedMeshInterfaces.Add(meshInterface);

                                        for (int i = 0; i < glmesh.m_numIndices / 3; i++)
                                        {
                                            float[] v0 = glmesh.m_vertices.at(glmesh.m_indices.at(i * 3)).xyzw;
                                            float[] v1 = glmesh.m_vertices.at(glmesh.m_indices.at(i * 3 + 1)).xyzw;
                                            float[] v2 = glmesh.m_vertices.at(glmesh.m_indices.at(i * 3 + 2)).xyzw;
                                            meshInterface.AddTriangle(new BulletSharp.Math.Vector3(v0[0], v0[1], v0[2]),

                                                                                                    new BulletSharp.Math.Vector3(v1[0], v1[1], v1[2]),

                                                                                                new BulletSharp.Math.Vector3(v2[0], v2[1], v2[2]));
                                        }

                                        BvhTriangleMeshShape trimesh = new BvhTriangleMeshShape(meshInterface, true, true);
                                        childShape = trimesh;
                                    }
                                    else
                                    {
                                        ConvexHullShape convexHull = new ConvexHullShape(convertedVerts[0].X, convertedVerts.Count, sizeof(float) * 3);
                                        convexHull.OptimizeConvexHull();
                                        //convexHull.initializePolyhedralFeatures();
                                        convexHull.Margin = (m_data.m_globalDefaults.m_defaultCollisionMargin);
                                        childShape = convexHull;
                                    }
                                }
                                */
                                childShape = null;
                                break;
                            }
                        case UrdfGeomTypes.URDF_GEOM_CAPSULE:
                            {
                                if (col.m_geometry.m_hasFromTo)
                                {

                                    BulletSharp.Math.Vector3 f = col.m_geometry.m_capsuleFrom;
                                    BulletSharp.Math.Vector3 t = col.m_geometry.m_capsuleTo;
                                    BulletSharp.Math.Vector3[] fromto = new BulletSharp.Math.Vector3[] { f, t };
                                    float[] radii = new float[]{col.m_geometry.m_capsuleRadius
                                        ,col.m_geometry.m_capsuleRadius};
                                    Debug.LogFormat("Creating capsule {0} {1} {2} {3}", link.m_name, f, t, col.m_geometry.m_capsuleRadius);
                                    BMultiSphereShape ms = colGo.AddComponent<BMultiSphereShape>();
                                    BMultiSphereShape.Sphere sphereA = new BMultiSphereShape.Sphere();
                                    sphereA.position = f.ToUnity();
                                    sphereA.radius = col.m_geometry.m_capsuleRadius;
                                    BMultiSphereShape.Sphere sphereB = new BMultiSphereShape.Sphere();
                                    sphereB.position = t.ToUnity();
                                    sphereB.radius = col.m_geometry.m_capsuleRadius;
                                    ((BMultiSphereShape)ms).spheres = new BMultiSphereShape.Sphere[] { sphereA, sphereB };
                                    childShape = ms;
                                    if (addVisualShape)
                                    {
                                        GameObject visGo = new GameObject("CapsuleRenderer");
                                        visGo.transform.parent = colGo.transform;
                                        visGo.transform.localPosition = ((f + t) / 2f).ToUnity();
                                        visGo.transform.localRotation = UnityEngine.Quaternion.LookRotation((t - f).ToUnity());
                                        
                                        BulletUnity.Primitives.BCapsule bss = visGo.AddComponent<BulletUnity.Primitives.BCapsule>();
                                        bss.meshSettings.height = (f - t).Length;
                                        bss.meshSettings.radius = col.m_geometry.m_capsuleRadius;
                                        bss.meshSettings.upAxis = BCapsuleShape.CapsuleAxis.z;
                                        bss.BuildMesh();
                                        visGo.GetComponent<MeshRenderer>().sharedMaterial = material;
                                    }
                                }
                                else
                                {
                                    BCapsuleShape cap = colGo.AddComponent<BCapsuleShape>();
                                    cap.Height = col.m_geometry.m_capsuleHeight;
                                    cap.Radius = col.m_geometry.m_capsuleRadius;
                                    cap.UpAxis = BCapsuleShape.CapsuleAxis.z;
                                    childShape = cap;
                                    if (addVisualShape)
                                    {
                                        BulletUnity.Primitives.BCapsule bss = colGo.AddComponent<BulletUnity.Primitives.BCapsule>();
                                        bss.meshSettings.height = cap.Height;
                                        bss.meshSettings.radius = cap.Radius;
                                        bss.meshSettings.upAxis = BCapsuleShape.CapsuleAxis.z;
                                        bss.BuildMesh();
                                        bss.GetComponent<MeshRenderer>().sharedMaterial = material;
                                    }
                                }
                                break;
                            }
                        case UrdfGeomTypes.URDF_GEOM_UNKNOWN:
                            {
                                break;
                            }

                    } // switch geom
                    if (childShape != null)
                    {
                        m_data.m_allocatedCollisionShapes.Add(childShape);
                        compoundShape.AddBCollisionShape(childShape);
                    }
                }
            }
            return compoundShape;
        }

        public int getNumAllocatedCollisionShapes()
        {
            return m_data.m_allocatedCollisionShapes.Count;
        }

        public CollisionShape getAllocatedCollisionShape(int index)
        {
            return m_data.m_allocatedCollisionShapes[index].GetCollisionShape();
        }

        public int getNumAllocatedMeshInterfaces()
        {
            return m_data.m_allocatedMeshInterfaces.Count;
        }

        public StridingMeshInterface getAllocatedMeshInterface(int index)
        {
            return m_data.m_allocatedMeshInterfaces[index];
        }

        public int getNumModels()
        {
            return m_data.m_models.Count;
        }

        public void activateModel(int modelIndex)
        {
            if ((modelIndex >= 0) && (modelIndex < m_data.m_models.Count))
            {
                m_data.m_activeModel = modelIndex;
            } else
            {
                Debug.LogError("bad model index for activateModel");
            }
        }

        static bool findExistingMeshFile(string s1, string s2, string s3, string s4, int idx)
        {
            Debug.LogError("Not implmented.");
            return false;
        }
    }
}
