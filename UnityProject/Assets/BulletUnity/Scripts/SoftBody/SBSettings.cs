using UnityEngine;
using System.Collections;
using BulletSharp.SoftBody;
using System;
using BulletSharp;
using System.Collections.Generic;
//using BulletSharp.Math;

namespace BulletUnity
{
    /// <summary>
    /// Settings for configuring a Bullet SoftBody in Unity Editor
    /// </summary>
    [Serializable]
    public class SBSettings
    {
        [Tooltip("Total SB Mass")]
        [Range(0f, 1000f)]
        public float totalMass = 10f;

        [Tooltip("Dont use scale here?")]
        public Vector3 scale = Vector3.one;

        [Tooltip("???")]
        public bool fromFaces = true;

        [Tooltip("bframe set at true mean the you enable kMT (shape matching), based on the current body shape(current orientation/translation doesn't matter, just node positions relative to each others).")]
        public bool bframe = true;

        [Tooltip("bvolume set to TRUE when you want to use pressure forces(kPR) and/or volume conservation forces(kVC) based on the current volume of the body. Keep in mind that volume means closed mesh, so check your meshes. ")]
        public bool bvolume = false;

        [Tooltip("")]
        public bool generateClusters = false;

        [Tooltip("int; ???")]
        public int bendingConstraintDistance = 2;

        [Tooltip("???")]
        public bool randomizeConstraints = true;

        public SBConfig config = new SBConfig();

        public SBMaterial sBMaterial = new SBMaterial();

        [HideInInspector]
        public SBSettingsPresets sBpresetSelect = SBSettingsPresets.Default;

        /// <summary>
        /// Apply these SoftBody settings to this SoftBody
        /// </summary>
        /// <param name="softBody"></param>
        public void ConfigureSoftBody(SoftBody softBody)
        {
            BulletSharp.SoftBody.Material pm = sBMaterial.SetSBMaterial(softBody);

            softBody.Scale(scale.ToBullet());

            softBody.SetTotalMass(totalMass, fromFaces);

            config.SetConfig(softBody.Cfg);

            softBody.GenerateBendingConstraints(bendingConstraintDistance, pm);

            if (generateClusters)
                softBody.GenerateClusters(0);

        }



        //TODO: lots! These presets need work.

        /// <summary>
        /// Reset and Configure SoftBody settings for some general preset values.  Can use as a starting point before optimization.
        /// </summary>
        /// <param name="preset"></param>
        public void ResetToSoftBodyPresets(SBSettingsPresets preset)
        {
            sBpresetSelect = preset;  //save last applied preset
            totalMass = 10f;
            scale = Vector3.one;
            fromFaces = true;
            bframe = true;
            bvolume = false;
            generateClusters = false;
            bendingConstraintDistance = 2;
            randomizeConstraints = true;
            config = new SBConfig();
            sBMaterial = new SBMaterial();

            switch (preset)
            {
                case SBSettingsPresets.Default:
                    //reset itself
                    break;
                case SBSettingsPresets.Cloth:
                    fromFaces = false;
                    bvolume = false;
                    bframe = false;
                    sBMaterial.LST = 0.5f;

                    break;
                case SBSettingsPresets.Rope:
                    config.PIterations = 20;
                    sBMaterial.LST = 0.5f;
                    bendingConstraintDistance = 0;
                    randomizeConstraints = false;
                    fromFaces = false;
                    bframe = false;
                    bvolume = false;

                    break;
                case SBSettingsPresets.ConvexHull:

                    break;

                case SBSettingsPresets.Pressurized:
                    sBMaterial.LST = 0.1f;
                    config.DF = 1;
                    config.DP = 0.001f; // fun factor...
                    config.PR = 2500;
                    fromFaces = true;
                    break;

                case SBSettingsPresets.Aerodynamic:
                    config.LF = 0.004f;
                    config.DG = 0.0003f;
                    config.aeroModel = AeroModel.VTwoSided;
                    break;
                case SBSettingsPresets.Volume:

                    sBMaterial.LST = 0.45f;
                    config.VC = 20;
                    totalMass = 50f;
                    fromFaces = true;
                    bvolume = true;
                    bframe = false;



                    break;
                case SBSettingsPresets.ShapeMatching:
                    sBMaterial.LST = 0.5f;
                    config.DF = 0.5f;
                    config.MT = 0.05f;
                    config.PIterations = 10;

                    randomizeConstraints = true;
                    totalMass = 100f;
                    fromFaces = true;
                    bvolume = false;
                    bframe = true;

                    break;

                default:
                    break;
            }

        }


    }







    /// <summary>
    /// Provides convienent presets for SBSettings
    /// Warning; Will overwrite settings with preset defaults!
    /// </summary>
    public enum SBSettingsPresets
    {
        Default,
        Cloth,
        Rope,
        ConvexHull,
        Pressurized,
        Aerodynamic,
        Volume,
        ShapeMatching,

    }

    #region SBConfig

    //TODO: apply reasonable min/max values
    /// <summary>
    /// SoftBody Config wrapper
    /// </summary>
    [Serializable]
    public class SBConfig
    {
        [Header("Common Settings")]

        [Tooltip("Dynamic friction coefficient; Same as rigid body friction. 0 = slides, 1 = sticks. ")]
        [Range(0f, 1f)]
        public float DF = 0.2f;

        [Tooltip("Damping coefficient; (Velocity?) damping.")]
        [Range(0f, 1f)]
        public float DP = 0f;

        [Tooltip("Volume conservation coefficient; Volume conservation. Also, when setPose(true, ...)*** has been called, defines magnitude of the force used to conserve volume. (?)")]
        [Range(0f, float.PositiveInfinity)]
        public float VC = 0f;

        [Tooltip("Pressure coefficient; Affects aerodynamics computations. Also, when setPose(true, ...)*** has been called, defines pressure used to conserve volume.")]
        //[Range(float.NegativeInfinity, float.PositiveInfinity)]
        public float PR = 0f;

        [Tooltip("Anchors hardness; Defines how “soft” anchor constraints (joints) are. 0 = no drift correction, 1 = full correction.")]
        [Range(0f, 1f)]
        public float Ahr = 0.7f;


        const string collisionTooltip = "Collisions flags\n\n" +
             "SDF_RS Rigid versus soft mask.\n\n" +
             "CL_RS: SDF based rigid vs soft.\n\n" +
             "SVSmask: Cluster vs convex rigid vs soft.\n\n" +
             "VF_SS: Rigid versus soft mask.\n\n" +
             "CL_SS:Vertex vs face soft vs soft handling.\n\n" +
             "CL_SELF: Cluster vs cluster soft vs soft handling.\n\n" +
             "Default: Cluster soft body self collision.\n\n";
        [HideInInspector]
        [Tooltip(collisionTooltip)]
        public Collisions Collisions = Collisions.Default;


        [Tooltip("Maximum volume ratio for pose.")]
        [Range(0f, float.PositiveInfinity)]
        public float MaxVolume = 1.0f;

        [Tooltip("Pose matching coefficient; When setPose(..., true)*** has been called, defines the factor used for pose matching. (enforcing relative vertex positions)")]
        [Range(0f, 1f)]
        public float MT = 0f;

        [Tooltip("Velocities correction factor (Baumgarte); Define the amount of correction per time step for drift solver (sometimes referred to as ERP in rigid bodies solvers).")]
        [Range(0f, 1f)]
        public float Vcf = 1.0f;

        [Tooltip("Time scale; Factor of time step. Can be used to speed up or slow down simulation of a specific soft body.")]
        [Range(0f, float.PositiveInfinity)]
        public float Timescale = 1.0f;

        [Header("Contact Hardness Settings")]

        [Tooltip("Rigid contacts hardness; Defines how “soft” contact with rigid bodies are. 0 = no penetration correction, 1 = full correction.")]
        [Range(0f, 1f)]
        public float Chr = 1.0f;

        [Tooltip("Kinetic contacts hardness; Defines how “soft” contact with kinetic/static bodies are. 0 = no penetration correction, 1 = full correction.")]
        [Range(0f, 1f)]
        public float Khr = 0.1f;

        [Tooltip("Soft contacts hardness; Defines how “soft” contact with other soft bodies are. 0 = no penetration correction, 1 = full correction.")]
        [Range(0f, 1f)]
        public float Shr = 1.0f;

        [Header("Solver Settings")]

        [Tooltip("Positions solver iterations")]
        [Range(0, 1000)] //int.MaxValue)]
        public int PIterations = 1;

        [Tooltip("Drift solver iterations; Number of iterations for drift solvers (if any, can be 0).")]
        [Range(0, 1000)]
        public int DIterations = 0;

        [Tooltip("Velocities solver iterations; Number of iterations for velocities solvers (if any).")]
        [Range(0, 1000)]
        public int Viterations = 0;

        [Tooltip("Cluster solver iterations; Number of iterations for cluster solvers (if any).")]
        [Range(0, 1000)]
        public int CIterations = 0;

        [Header("Aerodynamics Settings")]

        [Tooltip("Aerodynamic model; Define what kind of feature is used to compute aerodynamic forces (specifies orientation of vertex or face normals for aeurodynamics).")]
        public AeroModel aeroModel = AeroModel.VPoint;

        // V_Point,			///Vertex normals are oriented toward velocity
        //V_TwoSided,			///Vertex normals are flipped to match velocity	
        //V_TwoSidedLiftDrag, ///Vertex normals are flipped to match velocity and lift and drag forces are applied
        //V_OneSided,			///Vertex normals are taken as it is	
        //F_TwoSided,			///Face normals are flipped to match velocity
        //F_TwoSidedLiftDrag,	///Face normals are flipped to match velocity and lift and drag forces are applied 
        //F_OneSided,			///Face normals are taken as it is	

        [Tooltip("Drag coefficient; For aerodynamics computations. See wikipedia “Drag coefficient”. 0 = no drag.")]
        [Range(0f, 10f)]
        public float DG = 0f;

        [Tooltip("Lift coefficient; For aerodynamics computations. See wikipedia “Lift (force)”. 0 = no lift.")]
        [Range(0f, 1f)]
        public float LF = 0f;

        [Header("Cluster Settings")]

        [Tooltip("Soft vs rigid hardness; Used with clusters only. Presumably similar function as kCHR.")]
        [Range(0f, 1f)]
        public float SrhrCL = 0.1f;

        [Tooltip("Soft vs kinetic hardness, Used with clusters only. Presumably similar function as kKHR.")]
        [Range(0f, 1f)]
        public float SkhrCL = 1.0f;

        [Tooltip("Soft vs soft hardness; Used with clusters only. Presumably similar function as kSHR.")]
        [Range(0f, 1f)]
        public float SshrCL = 0.5f;

        [Tooltip("Soft vs rigid impulse split; Used with clusters only. What proportion to split impulse with a rigid body after collision.")]
        [Range(0f, 1f)]
        public float SRSplitCL = 0.5f;

        [Tooltip("Soft vs kinetic impulse split; Used with clusters only. What proportion to split impulse with a kinetic/static body after collision.")]
        [Range(0f, 1f)]
        public float SKSplitCL = 0.5f;

        [Tooltip("Soft vs soft impulse split; Used with clusters only. What proportion to split impulse with another soft body after collision.")]
        [Range(0f, 1f)]
        public float SSSplitCL = 0.5f;

        public void SetConfig(BulletSharp.SoftBody.Config sBConfig)
        {
            sBConfig.DF = DF;
            sBConfig.DP = DP;
            sBConfig.VC = VC;
            sBConfig.PR = PR;
            sBConfig.Ahr = Ahr;
            sBConfig.Collisions = Collisions;
            sBConfig.MaxVolume = MaxVolume;
            sBConfig.MT = MT;
            sBConfig.Vcf = Vcf;
            sBConfig.Timescale = Timescale;
            sBConfig.Chr = Chr;
            sBConfig.Khr = Khr;
            sBConfig.PIterations = PIterations;
            sBConfig.DIterations = DIterations;
            sBConfig.Viterations = Viterations;
            sBConfig.CIterations = CIterations;
            sBConfig.AeroModel = aeroModel;
            sBConfig.DG = DG;
            sBConfig.LF = LF;
            sBConfig.SrhrCL = SrhrCL;
            sBConfig.SkhrCL = SkhrCL;
            sBConfig.SshrCL = SshrCL;
            sBConfig.SRSplitCL = SRSplitCL;
            sBConfig.SKSplitCL = SKSplitCL;
            sBConfig.SSSplitCL = SSSplitCL;



        }
    }

    #endregion

    #region SBMaterial

    /// <summary>
    /// SoftBody Material wrapper
    /// </summary>
    [Serializable]
    public class SBMaterial
    {
        [Tooltip("Linear stiffness coefficient")]
        [Range(0f, 1f)]
        public float LST = 1f;

        [Tooltip("Angular stiffness coefficient")]
        [Range(0f, 1f)]
        public float AST = 1f;

        [Tooltip("Volume stiffness coefficient")]
        [Range(0f, 1f)]
        public float VST = 1f;

        [Tooltip("See: fMaterial::Default")]
        public FMaterial Flags = FMaterial.Default;

        /// <summary>
        /// Set SoftBody material properties
        /// </summary>
        /// <param name="softBody"></param>
        /// <returns></returns>
        public BulletSharp.SoftBody.Material SetSBMaterial(SoftBody softBody)
        {
            BulletSharp.SoftBody.Material pm = softBody.AppendMaterial();
            pm.Ast = AST;
            pm.Lst = LST;
            pm.Vst = VST;
            pm.Flags = Flags;

            return pm;
        }

    }

    #endregion


}

//Notes:
//http://bulletphysics.org/Bullet/phpBB3/viewtopic.php?p=24280

/*setPose(bvolume,bframe):
- setPose take the current state of the body(usually after creation, scaling, setting mass, etc...), and do two things depending of the value(true/false) of 'bvolume' and 'bframe'.
- bvolume set at true mean that you enable kVC and/or kPR based on the current volume of the body.keep in mind that volume mean closed, so check your meshes.
- bframe set at true mean the you enable kMT (shape matching), based on the current body shape(current orientation/translation doesn't matter, just node positions relative to each others).
- you need to call setPose(...,true) only when you want to use shape matching(kMT), and setPose(true,...) when you want to use pressure forces(kPR) and/or volume conservation forces(kVC).
*/
