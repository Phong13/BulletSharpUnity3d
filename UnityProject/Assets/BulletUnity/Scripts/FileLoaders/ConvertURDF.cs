using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BulletSharp;
using BulletSharp.Math;
using BulletUnity.Debugging;
using BulletUnity;
using System;

namespace DemoFramework.FileLoaders
{
    [System.Flags]
    enum btMultiBodyLinkFlags
    {
        BT_MULTIBODYLINKFLAGS_DISABLE_PARENT_COLLISION = 1,
        BT_MULTIBODYLINKFLAGS_DISABLE_ALL_PARENT_COLLISION = 2,
    };

    [System.Flags]
    public enum ConvertURDFFlags
    {
        NONE = 0,
        CUF_USE_SDF = 1,
        // Use inertia values in URDF instead of recomputing them from collision shape.
        CUF_USE_URDF_INERTIA = 2,
        CUF_USE_MJCF = 4,
        CUF_USE_SELF_COLLISION = 8,
        CUF_USE_SELF_COLLISION_EXCLUDE_PARENT = 16,
        CUF_USE_SELF_COLLISION_EXCLUDE_ALL_PARENTS = 32,
        CUF_RESERVED = 64,
    };

    public enum UrdfJointTypes
    {
        URDFRevoluteJoint = 1,
        URDFPrismaticJoint,
        URDFContinuousJoint,
        URDFFloatingJoint,
        URDFPlanarJoint,
        URDFFixedJoint,
    };

    [System.Flags]
    public enum URDF_LinkContactFlags
    {
        NONE = 0,
        URDF_CONTACT_HAS_LATERAL_FRICTION = 1,
        URDF_CONTACT_HAS_INERTIA_SCALING = 2,
        URDF_CONTACT_HAS_CONTACT_CFM = 4,
        URDF_CONTACT_HAS_CONTACT_ERP = 8,
        URDF_CONTACT_HAS_STIFFNESS_DAMPING = 16,
        URDF_CONTACT_HAS_ROLLING_FRICTION = 32,
        URDF_CONTACT_HAS_SPINNING_FRICTION = 64,
        URDF_CONTACT_HAS_RESTITUTION = 128,
        URDF_CONTACT_HAS_FRICTION_ANCHOR = 256,

    };

    public class URDFLinkContactInfo
    {
        public float m_lateralFriction;
        public float m_rollingFriction;
        public float m_spinningFriction;
        public float m_restitution;
        public float m_inertiaScaling;
        public float m_contactCfm;
        public float m_contactErp;
        public float m_contactStiffness;
        public float m_contactDamping;

        public URDF_LinkContactFlags m_flags;

        public URDFLinkContactInfo()
        {
            m_lateralFriction = (0.5f);

            m_rollingFriction = (0);
            m_spinningFriction = (0);

            m_restitution = (0);
            m_inertiaScaling = (1);

            m_contactCfm = (0);

            m_contactErp = (0);
            m_contactStiffness = (1e4f);
            m_contactDamping = (1);

            m_flags = URDF_LinkContactFlags.URDF_CONTACT_HAS_LATERAL_FRICTION;
        }
    };

    public class UrdfModel
    {
        public string m_name;
        public string m_sourceFile;
        public Matrix m_rootTransformInWorld;
        public Dictionary<string, UrdfMaterial> m_materials;
        public Dictionary<string, UrdfLink> m_links;
        public Dictionary<string, UrdfJoint> m_joints;

        public List<UrdfLink> m_rootLinks;
        public bool m_overrideFixedBase = false;

        public UrdfModel(bool overrideFixedBase = false)
        {
            m_rootTransformInWorld = Matrix.Identity;
        }
    };

    public enum UrdfCollisionFlags
    {
        NONE = 0,
        URDF_FORCE_CONCAVE_TRIMESH = 1,
        URDF_HAS_COLLISION_GROUP = 2,
        URDF_HAS_COLLISION_MASK = 4,
    };

    public class UrdfMaterialColor
    {
        public Color m_rgbaColor;
        public Color m_specularColor;
        public UrdfMaterialColor()
        {
            m_rgbaColor = new Color(0.8f, 0.8f, 0.8f, 1);

            m_specularColor = new Color(0.4f, 0.4f, 0.4f);
        }
    };

    public class URDF2BulletCachedData
    {
        public List<int> m_urdfLinkParentIndices = new List<int>();
        public List<int> m_urdfLinkIndices2BulletLinkIndices = new List<int>();
        public List<RigidBody> m_urdfLink2rigidBodies = new List<RigidBody>();
        public List<Matrix> m_urdfLinkLocalInertialFrames = new List<Matrix>();

        public int m_currentMultiBodyLinkIndex;

        public BMultiBody m_bulletMultiBody;

        //this will be initialized in the constructor
        public int m_totalNumJoints1;

        public URDF2BulletCachedData()
        {

            m_currentMultiBodyLinkIndex = -1;

            m_bulletMultiBody = null;

            m_totalNumJoints1 = (0);


        }
        //these arrays will be initialized in the 'InitURDF2BulletCache'


        public int getParentUrdfIndex(int linkIndex)
        {
            return m_urdfLinkParentIndices[linkIndex];
        }

        public int getMbIndexFromUrdfIndex(int urdfIndex)
        {
            if (urdfIndex == -2)
                return -2;
            return m_urdfLinkIndices2BulletLinkIndices[urdfIndex];
        }


        public void registerMultiBody(int urdfLinkIndex, MultiBody body, Matrix worldTransform, float mass, BulletSharp.Math.Vector3 localInertiaDiagonal, CollisionShape compound, ref Matrix localInertialFrame)
        {
            m_urdfLinkLocalInertialFrames[urdfLinkIndex] = localInertialFrame;
        }

        public RigidBody getRigidBodyFromLink(int urdfLinkIndex)
        {
            return m_urdfLink2rigidBodies[urdfLinkIndex];
        }

        public void registerRigidBody(int urdfLinkIndex, RigidBody body, Matrix worldTransform, float mass, BulletSharp.Math.Vector3 localInertiaDiagonal, CollisionShape compound, ref Matrix localInertialFrame)
        {
            UnityEngine.Debug.Assert(m_urdfLink2rigidBodies[urdfLinkIndex] == null);

            m_urdfLink2rigidBodies[urdfLinkIndex] = body;
            m_urdfLinkLocalInertialFrames[urdfLinkIndex] = localInertialFrame;
        }

    }


    public interface URDFImporterInterface
    {

        bool loadURDF(string fileName, bool forceFixedBase = false);

        bool loadSDF(string fileName, bool forceFixedBase = false);

        string getPathPrefix();

        ///return >=0 for the root link index, -1 if there is no root link
        int getRootLinkIndex();

        ///pure  interfaces, precondition is a valid linkIndex (you can assert/terminate if the linkIndex is out of range)
        string getLinkName(int linkIndex);

        //various derived class in internal source code break with new pure  methods, so provide some default implementation
        string getBodyName();
        /// optional method to provide the link color. return true if the color is available and copied into colorRGBA, return false otherwise
        bool getLinkColor(int linkIndex, out Color colorRGBA);

        bool getLinkColor2(int linkIndex, out UrdfMaterialColor matCol);


        UrdfCollisionFlags getCollisionGroupAndMask(int linkIndex, out BulletSharp.CollisionFilterGroups colGroup, out CollisionFilterGroups colMask);
        ///this API will likely change, don't override it!
        bool getLinkContactInfo(int linkIndex, out URDFLinkContactInfo contactInfo);

        //bool getLinkAudioSource(int linkIndex, out SDFAudioSource audioSource);

        string getJointName(int linkIndex);

        //fill mass and inertial data. If inertial data is missing, please initialize mass, inertia to sensitive values, and inertialFrame to identity.
        void getMassAndInertia(int urdfLinkIndex, out float mass, out BulletSharp.Math.Vector3 localInertiaDiagonal, out Matrix inertialFrame);

        ///fill an array of child link indices for this link, btAlignedObjectArray behaves like a std::vector so just use push_back and resize(0) if needed
        void getLinkChildIndices(int urdfLinkIndex, List<int> childLinkIndices);

        bool getJointInfo(int urdfLinkIndex, out Matrix parent2joint, out Matrix linkTransformInWorld, out BulletSharp.Math.Vector3 jointAxisInJointSpace, out UrdfJointTypes jointType, out float jointLowerLimit, out float jointUpperLimit, out float jointDamping, out float jointFriction);

        bool getJointInfo2(int urdfLinkIndex, out Matrix parent2joint, out Matrix linkTransformInWorld, out BulletSharp.Math.Vector3 jointAxisInJointSpace, out UrdfJointTypes jointType, out float jointLowerLimit, out float jointUpperLimit, out float jointDamping, out float jointFriction, out float jointMaxForce, out float jointMaxVelocity);
        /*
        {
            //backwards compatibility for custom file importers
            jointMaxForce = 0;
            jointMaxVelocity = 0;
            return getJointInfo(urdfLinkIndex, parent2joint, linkTransformInWorld, jointAxisInJointSpace, jointType, jointLowerLimit, jointUpperLimit, jointDamping, jointFriction);
        };
        */

        bool getRootTransformInWorld(out Matrix rootTransformInWorld);
        void setRootTransformInWorld(ref Matrix rootTransformInWorld);

        ///quick hack: need to rethink the API/dependencies of this
        int convertLinkVisualShapes(int linkIndex, string pathPrefix, ref Matrix inertialFrame);

        void convertLinkVisualShapes2(int linkIndex, int urdfIndex, string pathPrefix, ref Matrix inertialFrame, CollisionObject colObj, int objectIndex);
        void setBodyUniqueId(int bodyId);
        int getBodyUniqueId();

        //default implementation for backward compatibility 
        BCollisionShape convertLinkCollisionShapes(int linkIndex, string pathPrefix, ref Matrix localInertiaFrame, GameObject go);

        int getNumAllocatedCollisionShapes();
        //CollisionShape getAllocatedCollisionShape(int /*index*/ );
        int getNumModels();
        //void activateModel(int /*modelIndex*/);
        int getNumAllocatedMeshInterfaces();
        StridingMeshInterface getAllocatedMeshInterface(int index);

        void activateModel(int modelIndex);
    }

    public interface MultiBodyCreationInterface
    {



        void createRigidBodyGraphicsInstance(int linkIndex, RigidBody body, Color colorRgba, int graphicsIndex);
        void createRigidBodyGraphicsInstance2(int linkIndex, RigidBody body, Color colorRgba, Color specularColor, int graphicsIndex);

        ///optionally create some graphical representation from a collision object, usually for visual debugging purposes.
        void createCollisionObjectGraphicsInstance(int linkIndex, CollisionObject col, Color colorRgba);
        void createCollisionObjectGraphicsInstance2(int linkIndex, CollisionObject col, Color colorRgba, Color specularColor);

        MultiBody allocateMultiBody(int urdfLinkIndex, int totalNumJoints, float mass, BulletSharp.Math.Vector3 localInertiaDiagonal, bool isFixedBase, bool canSleep);

        RigidBody allocateRigidBody(int urdfLinkIndex, float mass, BulletSharp.Math.Vector3 localInertiaDiagonal, Matrix initialWorldTrans, CollisionShape colShape);

        Generic6DofSpring2Constraint allocateGeneric6DofSpring2Constraint(int urdfLinkIndex, RigidBody rbA /*parent*/, RigidBody rbB, Matrix offsetInA, Matrix offsetInB, int rotateOrder = 0);

        Generic6DofSpring2Constraint createPrismaticJoint(int urdfLinkIndex, RigidBody rbA /*parent*/, RigidBody rbB, Matrix offsetInA, Matrix offsetInB,
                                                            BulletSharp.Math.Vector3 jointAxisInJointSpace, float jointLowerLimit, float jointUpperLimit);
        Generic6DofSpring2Constraint createRevoluteJoint(int urdfLinkIndex, RigidBody rbA /*parent*/, RigidBody rbB, Matrix offsetInA, Matrix offsetInB,
                                                            BulletSharp.Math.Vector3 jointAxisInJointSpace, float jointLowerLimit, float jointUpperLimit);

        Generic6DofSpring2Constraint createFixedJoint(int urdfLinkIndex, RigidBody rbA /*parent*/, RigidBody rbB, Matrix offsetInA, Matrix offsetInB);

        MultiBodyLinkCollider allocateMultiBodyLinkCollider(int urdfLinkIndex, int mbLinkIndex, MultiBody body);

        void addLinkMapping(int urdfLinkIndex, int mbLinkIndex);

    }


     
    public class ConvertURDF
    {

        BDebug.DebugType debugLevel = BDebug.DebugType.Info;

        private void Resize(List<int> list, int n)
        {
            for (int i = list.Count; i < n; i++) {  list.Add(0); }
        }

        private void Resize(List<RigidBody> list, int n)
        {
            for (int i = list.Count; i < n; i++) { list.Add(null); }
        }

        private void Resize(List<Matrix> list, int n)
        {
            for (int i = list.Count; i < n; i++) { list.Add(new Matrix()); }
        }

        public void InitURDF2BulletCache(URDFImporterInterface u2b, URDF2BulletCachedData cache)
        {
            //compute the number of links, and compute parent indices array (and possibly other cached data?)
            cache.m_totalNumJoints1 = 0;

            int rootLinkIndex = u2b.getRootLinkIndex();
            if (rootLinkIndex >= 0)
            {
                ComputeTotalNumberOfJoints(u2b, cache, rootLinkIndex);
                int numTotalLinksIncludingBase = 1 + cache.m_totalNumJoints1;

                Resize(cache.m_urdfLinkParentIndices, numTotalLinksIncludingBase);
                Resize(cache.m_urdfLinkIndices2BulletLinkIndices,numTotalLinksIncludingBase);
                Resize(cache.m_urdfLink2rigidBodies,numTotalLinksIncludingBase);
                Resize(cache.m_urdfLinkLocalInertialFrames, numTotalLinksIncludingBase);

                cache.m_currentMultiBodyLinkIndex = -1;//multi body base has 'link' index -1
                ComputeParentIndices(u2b, cache, rootLinkIndex, -2);
            }
        }

        void ComputeTotalNumberOfJoints(URDFImporterInterface u2b, URDF2BulletCachedData cache, int linkIndex)
        {
            List<int> childIndices = new List<int>();
            u2b.getLinkChildIndices(linkIndex, childIndices);
            //b3Printf("link %s has %d children\n", u2b.getLinkName(linkIndex).c_str(),childIndices.size());
            //for (int i=0;i<childIndices.size();i++)
            //{
            //    b3Printf("child %d has childIndex%d=%s\n",i,childIndices[i],u2b.getLinkName(childIndices[i]).c_str());
            //}
            cache.m_totalNumJoints1 += childIndices.Count;
            for (int i = 0; i < childIndices.Count; i++)
            {
                int childIndex = childIndices[i];
                ComputeTotalNumberOfJoints(u2b, cache, childIndex);
            }
        }

        void ComputeParentIndices(URDFImporterInterface u2b, URDF2BulletCachedData cache, int urdfLinkIndex, int urdfParentIndex)
        {
            cache.m_urdfLinkParentIndices[urdfLinkIndex] = urdfParentIndex;
            cache.m_urdfLinkIndices2BulletLinkIndices[urdfLinkIndex] = cache.m_currentMultiBodyLinkIndex++;

            List<int> childIndices = new List<int>();
            u2b.getLinkChildIndices(urdfLinkIndex, childIndices);
            for (int i = 0; i < childIndices.Count; i++)
            {
                ComputeParentIndices(u2b, cache, childIndices[i], urdfLinkIndex);
            }
        }

        public void ConvertURDF2BulletInternal(
                        URDFImporterInterface u2b,
                        MultiBodyCreationInterface creation,
                        URDF2BulletCachedData cache, int urdfLinkIndex,
                        Matrix parentTransformInWorldSpace,
                        GameObject parentGameObject,
                        MultiBodyDynamicsWorld world1,
                        bool createMultiBody, string pathPrefix,
                        bool enableConstraints,
                        ConvertURDFFlags flags = 0)
        {
            Matrix linkTransformInWorldSpace = Matrix.Identity;
            int mbLinkIndex = cache.getMbIndexFromUrdfIndex(urdfLinkIndex);
            int urdfParentIndex = cache.getParentUrdfIndex(urdfLinkIndex);
            int mbParentIndex = cache.getMbIndexFromUrdfIndex(urdfParentIndex);
            RigidBody parentRigidBody = null;

            //b3Printf();
            if (debugLevel >= BDebug.DebugType.Debug) { Debug.LogFormat("mb link index = {0}\n", mbLinkIndex); }

            Matrix parentLocalInertialFrame = Matrix.Identity;
            float parentMass = (1);
            BulletSharp.Math.Vector3 parentLocalInertiaDiagonal = new BulletSharp.Math.Vector3(1, 1, 1);

            if (urdfParentIndex == -2)
            {
                if (debugLevel >= BDebug.DebugType.Debug)
                {
                    Debug.LogFormat("root link has no parent\n");
                }
            }
            else
            {
                if (debugLevel >= BDebug.DebugType.Debug)
                {
                    Debug.LogFormat("urdf parent index = {0}",urdfParentIndex);
                    Debug.LogFormat("mb parent index = {0}",mbParentIndex);
                }
                parentRigidBody = cache.getRigidBodyFromLink(urdfParentIndex);
                u2b.getMassAndInertia(urdfParentIndex, out parentMass, out parentLocalInertiaDiagonal, out parentLocalInertialFrame);
            }

            float mass = 0;
            Matrix localInertialFrame = Matrix.Identity;

            BulletSharp.Math.Vector3 localInertiaDiagonal = new BulletSharp.Math.Vector3(0, 0, 0);
            u2b.getMassAndInertia(urdfLinkIndex, out mass, out localInertiaDiagonal, out localInertialFrame);
            Matrix parent2joint = Matrix.Identity;

            UrdfJointTypes jointType;
            BulletSharp.Math.Vector3 jointAxisInJointSpace;
            float jointLowerLimit;
            float jointUpperLimit;
            float jointDamping;
            float jointFriction;
            float jointMaxForce;
            float jointMaxVelocity;

            bool hasParentJoint = u2b.getJointInfo2(urdfLinkIndex, out parent2joint, out linkTransformInWorldSpace, out jointAxisInJointSpace, out jointType, out jointLowerLimit, out jointUpperLimit, out jointDamping, out jointFriction, out jointMaxForce, out jointMaxVelocity);
            string linkName = u2b.getLinkName(urdfLinkIndex);

            if ((flags & ConvertURDFFlags.CUF_USE_SDF) != 0)
            {
                Matrix tmp = parentTransformInWorldSpace.Inverse();
                Matrix.Multiply(ref tmp, ref linkTransformInWorldSpace, out parent2joint);
            }
            else
            {
                if ((flags & ConvertURDFFlags.CUF_USE_MJCF) != 0)
                {
                    linkTransformInWorldSpace = parentTransformInWorldSpace * linkTransformInWorldSpace;
                }
                else
                {
                    linkTransformInWorldSpace = parentTransformInWorldSpace * parent2joint;
                }
            }

            GameObject gameObject = new GameObject(linkName);
            if (parentGameObject != null)
            {
                gameObject.transform.parent = parentGameObject.transform;
            }
            //--------------------
            /*
            bool hasParentJoint = loader.getJointInfo2(linkIndex, out parent2joint, out linkTransformInWorldSpace, out jointAxisInJointSpace, out jointType, out jointLowerLimit, out jointUpperLimit, out jointDamping, out jointFriction, out jointMaxForce, out jointMaxVelocity);
            string linkName = loader.getLinkName(linkIndex);

            if ((flags & ConvertURDFFlags.CUF_USE_SDF) != 0)
            {
                Matrix tmp = parentTransformInWorldSpace.Inverse();
                Matrix.Multiply(ref tmp, ref linkTransformInWorldSpace, out parent2joint);
            }
            else
            {
                if ((flags & ConvertURDFFlags.CUF_USE_MJCF) != 0)
                {
                    linkTransformInWorldSpace = parentTransformInWorldSpace * linkTransformInWorldSpace;
                }
                else
                {
                    linkTransformInWorldSpace = parentTransformInWorldSpace * parent2joint;
                }
            }

            lgo.transform.position = linkTransformInWorldSpace.Origin.ToUnity();
            */
            //--------------------
            gameObject.transform.position = linkTransformInWorldSpace.Origin.ToUnity();
            gameObject.transform.rotation = linkTransformInWorldSpace.Rotation.ToUnity();

            BCollisionShape compoundShape = u2b.convertLinkCollisionShapes(urdfLinkIndex, pathPrefix, ref localInertialFrame, gameObject);


            int graphicsIndex;
            {
                graphicsIndex = u2b.convertLinkVisualShapes(urdfLinkIndex, pathPrefix, ref localInertialFrame);
            }

            if (compoundShape != null)
            {
                UrdfMaterialColor matColor;
                Color color2 = Color.red;
                Color specular = new Color(0.5f, 0.5f, 0.5f);
                if (u2b.getLinkColor2(urdfLinkIndex, out matColor))
                {
                    color2 = matColor.m_rgbaColor;
                    specular = matColor.m_specularColor;
                }

                if (mass != 0)
                {
                    if ((flags & ConvertURDFFlags.CUF_USE_URDF_INERTIA) == 0)
                    {
                        compoundShape.GetCollisionShape().CalculateLocalInertia(mass, out localInertiaDiagonal);
                        Debug.Assert(localInertiaDiagonal[0] < 1e10);
                        Debug.Assert(localInertiaDiagonal[1] < 1e10);
                        Debug.Assert(localInertiaDiagonal[2] < 1e10);
                    }
                    URDFLinkContactInfo contactInfo;
                    u2b.getLinkContactInfo(urdfLinkIndex, out contactInfo);
                    //temporary inertia scaling until we load inertia from URDF
                    if ((contactInfo.m_flags & URDF_LinkContactFlags.URDF_CONTACT_HAS_INERTIA_SCALING) != 0)
                    {
                        localInertiaDiagonal *= contactInfo.m_inertiaScaling;
                    }
                }

                RigidBody linkRigidBody = null;
                Matrix inertialFrameInWorldSpace = linkTransformInWorldSpace * localInertialFrame;

                if (!createMultiBody)
                {
                    RigidBody body = creation.allocateRigidBody(urdfLinkIndex, mass, localInertiaDiagonal, inertialFrameInWorldSpace, compoundShape.GetCollisionShape());
                    linkRigidBody = body;
                    world1.AddRigidBody(body);
                    compoundShape.GetCollisionShape().UserIndex = (graphicsIndex);
                    URDFLinkContactInfo contactInfo;
                    u2b.getLinkContactInfo(urdfLinkIndex, out contactInfo);
                    ProcessContactParameters(contactInfo, body);
                    creation.createRigidBodyGraphicsInstance2(urdfLinkIndex, body, color2, specular, graphicsIndex);
                    cache.registerRigidBody(urdfLinkIndex, body, inertialFrameInWorldSpace, mass, localInertiaDiagonal, compoundShape.GetCollisionShape(), ref localInertialFrame);
                    //untested: u2b.convertLinkVisualShapes2(linkIndex,urdfLinkIndex,pathPrefix,localInertialFrame,body);
                }
                else
                {
                    if (cache.m_bulletMultiBody == null)
                    {
                        // creating base
                        bool canSleep = false;
                        bool isFixedBase = (mass == 0);//todo: figure out when base is fixed
                        int totalNumJoints = cache.m_totalNumJoints1;
                        //cache.m_bulletMultiBody = creation.allocateMultiBody(urdfLinkIndex, totalNumJoints, mass, localInertiaDiagonal, isFixedBase, canSleep);
                        BMultiBody bmm = cache.m_bulletMultiBody = gameObject.AddComponent<BMultiBody>();
                        bmm.fixedBase = isFixedBase;
                        bmm.canSleep = canSleep;
                        bmm.baseMass = mass;
                        //new btMultiBody(totalNumJoints, mass, localInertiaDiagonal, isFixedBase, canSleep);
                        //if ((flags & ConvertURDFFlags.CUF_USE_MJCF) != 0)
                        //{
                        //    cache.m_bulletMultiBody.BaseWorldTransform = (linkTransformInWorldSpace);
                        //}
                        //cache.registerMultiBody(urdfLinkIndex, cache.m_bulletMultiBody, inertialFrameInWorldSpace, mass, localInertiaDiagonal, compoundShape.GetCollisionShape(), ref localInertialFrame);
                        
                    }
                }

                //create a joint if necessary
                BMultiBodyLink bmbl = null;
                if (hasParentJoint)
                {
                    //====================
                    //btTransform offsetInA, offsetInB;
                    //offsetInA = parentLocalInertialFrame.inverse() * parent2joint;
                    //offsetInB = localInertialFrame.inverse();
                    //btQuaternion parentRotToThis = offsetInB.getRotation() * offsetInA.inverse().getRotation();
                    //=====================
                    Matrix offsetInA, offsetInB;
                    offsetInA = parentLocalInertialFrame.Inverse() * parent2joint;
                    offsetInB = localInertialFrame.Inverse();
                    BulletSharp.Math.Quaternion parentRotToThis = offsetInB.GetRotation() * offsetInA.Inverse().GetRotation();

                    Matrix tmp = parentTransformInWorldSpace.Inverse();
                    Matrix tmp2 = parent2joint.Inverse();
                    Matrix tmp3 = new Matrix();
                    Matrix tmp4 = linkTransformInWorldSpace;
                    Matrix link2joint = new Matrix();
                    Matrix.Multiply(ref tmp, ref tmp2, out tmp3);
                    Matrix.Multiply(ref tmp3, ref tmp4, out link2joint);

                    if (debugLevel >= BDebug.DebugType.Debug)
                    {
                        Debug.Log("Creating link " + linkName + " offsetInA=" + offsetInA.Origin + " offsetInB=" + offsetInB.Origin + " parentLocalInertialFrame=" + parentLocalInertialFrame.Origin + " localInertialFrame=" + localInertialFrame.Origin + " localTrnaform=" +
                                 " linkTransInWorldSpace=" + linkTransformInWorldSpace.Origin +
                                 " linkTransInWorldSpaceInv=" + linkTransformInWorldSpace.Inverse().Origin +
                                 " parentTransInWorldSpace=" + parentTransformInWorldSpace.Origin +
                                 " parentTransInWorldSpaceInv=" + parentTransformInWorldSpace.Inverse().Origin +
                                 " link2joint=" + link2joint.Origin +
                                 " jointType=" + jointType);
                    }

                    bool disableParentCollision = true;
                    
                    switch (jointType)
                    {
                        case UrdfJointTypes.URDFFloatingJoint:
                        case UrdfJointTypes.URDFPlanarJoint:
                        case UrdfJointTypes.URDFFixedJoint:
                            {
                                
                                
                                if ((jointType == UrdfJointTypes.URDFFloatingJoint) || (jointType == UrdfJointTypes.URDFPlanarJoint))
                                {

                                    Debug.Log("Warning: joint unsupported, creating a fixed joint instead.");
                                }
                                //creation.addLinkMapping(urdfLinkIndex, mbLinkIndex);

                                if (createMultiBody)
                                {
                                    //todo: adjust the center of mass transform and pivot axis properly
                                    /*
                                    cache.m_bulletMultiBody.SetupFixed(mbLinkIndex, mass, localInertiaDiagonal, mbParentIndex,
                                                                        parentRotToThis, offsetInA.Origin, -offsetInB.Origin);
                                    */
                                    bmbl = gameObject.AddComponent<BMultiBodyLink>();
                                    bmbl.jointType = FeatherstoneJointType.Fixed;
                                    bmbl.mass = mass;
                                    bmbl.localPivotPosition = offsetInB.Origin.ToUnity();
                                }
                                else
                                {
                                    //b3Printf("Fixed joint\n");
                                    Debug.LogError("TODO Setup 6dof ");
                                    /*
                                    Generic6DofSpring2Constraint dof6 = null;

                                    //backward compatibility
                                    if ((flags & ConvertURDFFlags.CUF_RESERVED) != 0)
                                    {
                                        dof6 = creation.createFixedJoint(urdfLinkIndex, parentRigidBody, linkRigidBody, offsetInA, offsetInB);
                                    }
                                    else
                                    {
                                        dof6 = creation.createFixedJoint(urdfLinkIndex, linkRigidBody, parentRigidBody, offsetInB, offsetInA);
                                    }
                                    if (enableConstraints)
                                        world1.AddConstraint(dof6, true);
                                    */
                                }
                                break;
                            }
                        case UrdfJointTypes.URDFContinuousJoint:
                        case UrdfJointTypes.URDFRevoluteJoint:
                            {
                                
                                
                                //creation.addLinkMapping(urdfLinkIndex, mbLinkIndex);
                                if (createMultiBody)
                                {
                                    //cache.m_bulletMultiBody.SetupRevolute(mbLinkIndex, mass, localInertiaDiagonal, mbParentIndex,
                                    //                                          parentRotToThis, (offsetInB.GetRotation().Rotate(jointAxisInJointSpace)), offsetInA.Origin,//parent2joint.getOrigin(),
                                    //                                          -offsetInB.Origin,
                                    //                                          disableParentCollision);
                                    bmbl = gameObject.AddComponent<BMultiBodyLink>();
                                    bmbl.jointType = FeatherstoneJointType.Revolute;
                                    bmbl.mass = mass;
                                    bmbl.localPivotPosition = offsetInB.Origin.ToUnity();
                                    bmbl.rotationAxis = offsetInB.Rotation.Rotate(jointAxisInJointSpace).ToUnity();
                                    if (jointType == UrdfJointTypes.URDFRevoluteJoint && jointLowerLimit <= jointUpperLimit)
                                    {
                                        //string name = u2b.getLinkName(urdfLinkIndex);
                                        //printf("create btMultiBodyJointLimitConstraint for revolute link name=%s urdf link index=%d (low=%f, up=%f)\n", name.c_str(), urdfLinkIndex, jointLowerLimit, jointUpperLimit);
                                        BMultiBodyJointLimitConstraint mbc = gameObject.AddComponent<BMultiBodyJointLimitConstraint>();
                                        mbc.m_jointLowerLimit = jointLowerLimit;
                                        mbc.m_jointUpperLimit = jointUpperLimit;
                                    }
                                    /*
                                    Debug.Log("=========== Creating joint for: " + gameObject.name);
                                    Debug.Log("parentRotateToThis: " + parentRotToThis.ToUnity().eulerAngles);
                                    Debug.Log("rotationAxis: " + bmbl.rotationAxis);
                                    Debug.Log("offsetInA: " + offsetInA.Origin);
                                    Debug.Log("negOffsetInB: " + -offsetInB.Origin);
                                    */
                                }
                                else
                                {

                                    Generic6DofSpring2Constraint dof6 = null;
                                    //backwards compatibility
                                    if ((flags & ConvertURDFFlags.CUF_RESERVED) != 0)
                                    {
                                        dof6 = creation.createRevoluteJoint(urdfLinkIndex, parentRigidBody, linkRigidBody, offsetInA, offsetInB, jointAxisInJointSpace, jointLowerLimit, jointUpperLimit);
                                    }
                                    else
                                    {
                                        dof6 = creation.createRevoluteJoint(urdfLinkIndex, linkRigidBody, parentRigidBody, offsetInB, offsetInA, jointAxisInJointSpace, jointLowerLimit, jointUpperLimit);
                                    }
                                    if (enableConstraints)
                                        world1.AddConstraint(dof6, true);
                                    //b3Printf("Revolute/Continuous joint\n");
                                }
                                
                                break;

                            }
                        case UrdfJointTypes.URDFPrismaticJoint:
                            {
                                
                                
                                //creation.addLinkMapping(urdfLinkIndex, mbLinkIndex);

                                if (createMultiBody)
                                {
                                    //cache.m_bulletMultiBody.SetupPrismatic(mbLinkIndex, mass, localInertiaDiagonal, mbParentIndex,
                                    //                                           parentRotToThis, (offsetInB.GetRotation().Rotate(jointAxisInJointSpace)), offsetInA.Origin,//parent2joint.getOrigin(),
                                    //                                           -offsetInB.Origin,
                                    //                                           disableParentCollision);
                                    bmbl = gameObject.AddComponent<BMultiBodyLink>();
                                    bmbl.jointType = FeatherstoneJointType.Prismatic;
                                    bmbl.mass = mass;
                                    bmbl.localPivotPosition = offsetInB.Origin.ToUnity();
                                    bmbl.rotationAxis = offsetInB.Rotation.Rotate(jointAxisInJointSpace).ToUnity();

                                    if (jointLowerLimit <= jointUpperLimit)
                                    {
                                        //string name = u2b.getLinkName(urdfLinkIndex);
                                        //printf("create btMultiBodyJointLimitConstraint for prismatic link name=%s urdf link index=%d (low=%f, up=%f)\n", name.c_str(), urdfLinkIndex, jointLowerLimit,jointUpperLimit);
                                        BMultiBodyJointLimitConstraint mbc = gameObject.AddComponent<BMultiBodyJointLimitConstraint>();
                                        mbc.m_jointLowerLimit = jointLowerLimit;
                                        mbc.m_jointUpperLimit = jointUpperLimit;
                                    }
                                    //printf("joint lower limit=%d, upper limit = %f\n", jointLowerLimit, jointUpperLimit);

                                }
                                else
                                {

                                    Generic6DofSpring2Constraint dof6 = creation.createPrismaticJoint(urdfLinkIndex, parentRigidBody, linkRigidBody, offsetInA, offsetInB, jointAxisInJointSpace, jointLowerLimit, jointUpperLimit);

                                    if (enableConstraints)
                                        world1.AddConstraint(dof6, true);

                                    //b3Printf("Prismatic\n");
                                }
                                
                                break;
                            }
                        default:
                            {
                                //b3Printf("Error: unsupported joint type in URDF (%d)\n", jointType);
                                Debug.Assert(false);
                                break;
                            }
                    }

                }

                if (createMultiBody)
                {

                    if (bmbl != null)
                    {
                        bmbl.jointDamping = jointDamping;
                        bmbl.jointFriction = jointFriction;
                        //bmbl.jointLowerLimit = jointLowerLimit;
                        //bmbl.jointUpperLimit = jointUpperLimit;
                        //bmbl.jointMaxForce = jointMaxForce;
                        //bmbl.jointMaxVelocity = jointMaxVelocity;
                    }
                    {
                        /*
                        MultiBodyLinkCollider col = creation.allocateMultiBodyLinkCollider(urdfLinkIndex, mbLinkIndex, cache.m_bulletMultiBody);

                        compoundShape.GetCollisionShape().UserIndex = (graphicsIndex);

                        col.CollisionShape = (compoundShape.GetCollisionShape());

                        Matrix tr = Matrix.Identity;

                        tr = linkTransformInWorldSpace;
                        //if we don't set the initial pose of the btCollisionObject, the simulator will do this
                        //when syncing the btMultiBody link transforms to the btMultiBodyLinkCollider

                        col.WorldTransform = (tr);

                        //base and fixed? . static, otherwise flag as dynamic
                        bool isDynamic = (mbLinkIndex < 0 && cache.m_bulletMultiBody.HasFixedBase) ? false : true;
                        CollisionFilterGroups collisionFilterGroup = isDynamic ? (CollisionFilterGroups.DefaultFilter) : (CollisionFilterGroups.StaticFilter);
                        CollisionFilterGroups collisionFilterMask = isDynamic ? (CollisionFilterGroups.AllFilter) : (CollisionFilterGroups.AllFilter ^ CollisionFilterGroups.StaticFilter);

                        CollisionFilterGroups colGroup = 0, colMask = 0;
                        UrdfCollisionFlags collisionFlags = u2b.getCollisionGroupAndMask(urdfLinkIndex, out colGroup, out colMask);
                        if ((collisionFlags & UrdfCollisionFlags.URDF_HAS_COLLISION_GROUP) != 0)
                        {
                            collisionFilterGroup = colGroup;
                        }
                        if ((collisionFlags & UrdfCollisionFlags.URDF_HAS_COLLISION_MASK) != 0)
                        {
                            collisionFilterMask = colMask;
                        }
                        world1.AddCollisionObject(col, collisionFilterGroup, collisionFilterMask);

                        color2 = Color.red;//(0.0,0.0,0.5);
                        Color specularColor = new Color(1, 1, 1);
                        UrdfMaterialColor matCol;
                        if (u2b.getLinkColor2(urdfLinkIndex, out matCol))
                        {
                            color2 = matCol.m_rgbaColor;
                            specularColor = matCol.m_specularColor;
                        }
                        {


                            creation.createCollisionObjectGraphicsInstance2(urdfLinkIndex, col, color2, specularColor);
                        }
                        {


                            u2b.convertLinkVisualShapes2(mbLinkIndex, urdfLinkIndex, pathPrefix, ref localInertialFrame, col, u2b.getBodyUniqueId());
                        }
                        URDFLinkContactInfo contactInfo;
                        u2b.getLinkContactInfo(urdfLinkIndex, out contactInfo);


                        ProcessContactParameters(contactInfo, col);

                        if (mbLinkIndex >= 0) //???? double-check +/- 1
                        {
                            cache.m_bulletMultiBody.GetLink(mbLinkIndex).Collider = col;
                            if ((flags & ConvertURDFFlags.CUF_USE_SELF_COLLISION_EXCLUDE_PARENT) != 0)
                            {
                                cache.m_bulletMultiBody.GetLink(mbLinkIndex).Flags |= (int)btMultiBodyLinkFlags.BT_MULTIBODYLINKFLAGS_DISABLE_PARENT_COLLISION;
                            }
                            if ((flags & ConvertURDFFlags.CUF_USE_SELF_COLLISION_EXCLUDE_ALL_PARENTS) != 0)
                            {
                                cache.m_bulletMultiBody.GetLink(mbLinkIndex).Flags |= (int)btMultiBodyLinkFlags.BT_MULTIBODYLINKFLAGS_DISABLE_ALL_PARENT_COLLISION;
                            }
                        }
                        else
                        {
                            cache.m_bulletMultiBody.BaseCollider = (col);
                        }
                        */
                    }
                }
                else
                {
                    //u2b.convertLinkVisualShapes2(urdfLinkIndex,urdfIndex,pathPrefix,localInertialFrame,compoundShape);
                }
            }


            List<int> urdfChildIndices = new List<int>();
            u2b.getLinkChildIndices(urdfLinkIndex, urdfChildIndices);

            int numChildren = urdfChildIndices.Count;

            for (int i = 0; i < numChildren; i++)
            {
                int urdfChildLinkIndex = urdfChildIndices[i];

                ConvertURDF2BulletInternal(u2b, creation, cache, urdfChildLinkIndex, linkTransformInWorldSpace, gameObject, world1, createMultiBody, pathPrefix, enableConstraints, flags);
            }

        }

        void ProcessContactParameters(URDFLinkContactInfo contactInfo, CollisionObject col)
        {
            if ((contactInfo.m_flags & URDF_LinkContactFlags.URDF_CONTACT_HAS_LATERAL_FRICTION) != 0)
            {
                col.Friction = (contactInfo.m_lateralFriction);
            }
            if ((contactInfo.m_flags & URDF_LinkContactFlags.URDF_CONTACT_HAS_RESTITUTION) != 0)
            {
                col.Restitution = (contactInfo.m_restitution);
            }

            if ((contactInfo.m_flags & URDF_LinkContactFlags.URDF_CONTACT_HAS_ROLLING_FRICTION) != 0)
            {
                col.RollingFriction = (contactInfo.m_rollingFriction);
            }
            if ((contactInfo.m_flags & URDF_LinkContactFlags.URDF_CONTACT_HAS_SPINNING_FRICTION) != 0)
            {
                col.SpinningFriction = (contactInfo.m_spinningFriction);
            }
            if ((contactInfo.m_flags & URDF_LinkContactFlags.URDF_CONTACT_HAS_STIFFNESS_DAMPING) != 0)
            {
                col.SetContactStiffnessAndDamping(contactInfo.m_contactStiffness, contactInfo.m_contactDamping);
            }
            if ((contactInfo.m_flags & URDF_LinkContactFlags.URDF_CONTACT_HAS_FRICTION_ANCHOR) != 0)
            {
                col.CollisionFlags = (col.CollisionFlags | BulletSharp.CollisionFlags.HasFrictionAnchor);
            }
        }

        /*
        void ConvertURDF2Bullet(
            const URDFImporterInterface& u2b, MultiBodyCreationInterface& creation,
            const Matrix& rootTransformInWorldSpace,
            btMultiBodyDynamicsWorld* world1,
            bool createMultiBody, string pathPrefix, ConvertURDFFlags flags)
        {
            URDF2BulletCachedData cache;

            InitURDF2BulletCache(u2b, cache);
            int urdfLinkIndex = u2b.getRootLinkIndex();
            B3_PROFILE("ConvertURDF2Bullet");
            ConvertURDF2BulletInternal(u2b, creation, cache, urdfLinkIndex, rootTransformInWorldSpace, world1, createMultiBody, pathPrefix, flags);

            if (world1 && cache.m_bulletMultiBody)
            {
                B3_PROFILE("Post process");
                btMultiBody* mb = cache.m_bulletMultiBody;

                mb.setHasSelfCollision((flags & ConvertURDFFlags.CUF_USE_SELF_COLLISION) != 0);

                mb.finalizeMultiDof();

                Matrix localInertialFrameRoot = cache.m_urdfLinkLocalInertialFrames[urdfLinkIndex];

                if (flags & ConvertURDFFlags.CUF_USE_MJCF)
                {
                }
                else
                {
                    mb.setBaseWorldTransform(rootTransformInWorldSpace * localInertialFrameRoot);
                }
                btAlignedObjectArray<BulletSharp.Math.Quaternion> scratch_q;
                btAlignedObjectArray<BulletSharp.Math.Vector3> scratch_m;
                mb.forwardKinematics(scratch_q, scratch_m);
                mb.updateCollisionObjectWorldTransforms(scratch_q, scratch_m);

                world1.addMultiBody(mb);
            }
        }
        */
    }
}
