using UnityEditor;
using UnityEngine;


namespace BulletUnity
{

    [CustomEditor(typeof(BSoftBody), true)]
    public class BSoftBodyEditor : Editor
    {
        public BSoftBody bSoftBodyTarget;

        SerializedProperty softBodySettings;

        //BulletSharp.SoftBody.Collisions collisions;  //bitmask field for collisions

        GUIContent gcSoftBodySettings = new GUIContent("SoftBodySettings");
        //GUIContent gcSoftBodyMeshSettings = new GUIContent("SoftBodyMeshSettings");

        //used to hide specific serialized properties in the editor
        //Hide softBody Settings until we want to display it, also script name
        private static readonly string[] hideMe = new string[] { "_softBodySettings", "m_Script" };

        const string collisionTooltip = "Collisions flags\n" +
       "SDF_RS Rigid versus soft mask.\n" +
       "CL_RS: SDF based rigid vs soft.\n" +
       "SVSmask: Cluster vs convex rigid vs soft.\n" +
       "VF_SS: Rigid versus soft mask.\n" +
       "CL_SS:Vertex vs face soft vs soft handling.\n" +
       "CL_SELF: Cluster vs cluster soft vs soft handling.\n" +
       "Default: Cluster soft body self collision.";

        GUIContent gcCollisionTooltip = new GUIContent("Soft vs. Rigid Mask", collisionTooltip);

        Color GUIBlue = new Color32(82, 140, 255, 255);

        BAnyMeshSettingsForEditor inspectorMeshSettings;

        public void OnEnable()
        {
            bSoftBodyTarget = (BSoftBody)target;
            softBodySettings = serializedObject.FindProperty("_softBodySettings");
            if (inspectorMeshSettings == null)
            {
                inspectorMeshSettings = new BAnyMeshSettingsForEditor();
            }
        }



        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            //Color GUIBlue = new Color32(82, 140, 255, 255);

            EditorHelpers.DrawLogoAndVersion();

            //BSoftBody sb = (BSoftBody)target;
            //sb.m_collisionFlags = BCollisionObjectEditor.RenderEnumMaskCollisionFlagsField(BCollisionObjectEditor.gcCollisionFlags, sb.m_collisionFlags);
            //sb.m_groupsIBelongTo = BCollisionObjectEditor.RenderEnumMaskCollisionFilterGroupsField(BCollisionObjectEditor.gcGroupsIBelongTo, sb.m_groupsIBelongTo);
            //sb.m_collisionMask = BCollisionObjectEditor.RenderEnumMaskCollisionFilterGroupsField(BCollisionObjectEditor.gcCollisionMask, sb.m_collisionMask);

            if (bSoftBodyTarget is BSoftBodyWMesh)
            {
                DrawCustomMeshSettingsOptions();
            }

            DrawPropertiesExcluding(serializedObject, hideMe); //Draw settings after the default inspector
            
            if (target is BSoftBodyPartOnSkinnedMesh)
            {
                BSoftBodyPartOnSkinnedMesh sb = (BSoftBodyPartOnSkinnedMesh)target;
                if (EditorHelpers.InspectorButton("Bind Bones To Soft Body & Nodes To Anchors", 300, 15, GUIBlue))
                {
                    sb.BindBonesToSoftBodyAndNodesToAnchors();
                }
                EditorGUILayout.HelpBox(sb.DescribeBonesAndAnchors(), MessageType.Info);
                if (sb.SoftBodySettings.sBpresetSelect != SBSettingsPresets.ShapeMatching)
                {
                    EditorGUILayout.HelpBox("For a soft body mesh the preset should probably be 'shape matching' or 'Volume'", MessageType.Warning);
                }
            }

            EditorGUILayout.BeginHorizontal();
            if (EditorHelpers.InspectorButton("Apply Preset", 100, 15, GUIBlue))
            {
                //SBSettingsPresets saveMe = bSoftBodyTarget.SoftBodySettings.sBpresetSelect;
                bSoftBodyTarget.SoftBodySettings.ResetToSoftBodyPresets(bSoftBodyTarget.SoftBodySettings.sBpresetSelect);
                //bSoftBodyTarget.SoftBodySettings.sBpresetSelect = saveMe;
            }

            bSoftBodyTarget.SoftBodySettings.sBpresetSelect = (SBSettingsPresets)EditorGUILayout.EnumPopup(bSoftBodyTarget.SoftBodySettings.sBpresetSelect);

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            //bitmask field for collisions
            bSoftBodyTarget.SoftBodySettings.config.Collisions = (BulletSharp.SoftBody.CollisionFlags) EditorGUILayout.EnumMaskField(gcCollisionTooltip, bSoftBodyTarget.SoftBodySettings.config.Collisions);

            EditorGUILayout.PropertyField(softBodySettings, gcSoftBodySettings, true);

            serializedObject.ApplyModifiedProperties();

            if (GUI.changed) //Can apply settings on editor change
            {
                bSoftBodyTarget.BuildSoftBody();
            }

            serializedObject.ApplyModifiedProperties();

        }

        //Menu items here
        #region MenuBulletForUnity

        [MenuItem("BulletForUnity/BSoftBody/Rope")]
        [MenuItem("GameObject/Create Other/BulletForUnity/BSoftBody/Rope")]  //right click menu
        static void CreateBSoftBodyRope()
        {
            Selection.activeObject = BSoftBodyRope.CreateNew(EditorHelpers.GetCameraRaycastPosition(), Quaternion.identity, false);
            PostCreateObject();
        }

        [MenuItem("BulletForUnity/BSoftBody/BSoftBodyWMesh")]
        [MenuItem("GameObject/Create Other/BulletForUnity/BSoftBody/BSoftBodyWMesh")]  //right click menu
        static void CreateBSoftWithMesh()
        {
            BAnyMeshSettings settings = new BAnyMeshSettings();
            settings.meshType = PrimitiveMeshOptions.Bunny;
            Selection.activeObject = BSoftBodyWMesh.CreateNew(EditorHelpers.GetCameraRaycastPosition(), Quaternion.identity, settings.Build(), true, SBSettingsPresets.ShapeMatching);
            PostCreateObject();
        }

        /// <summary>
        /// Stuff to do after creation like sort script order
        /// </summary>
        static void PostCreateObject()
        {
            BSoftBodyComponentOrderSorter.SortComponents((GameObject)Selection.activeObject);  //order the scripts, looks nicer
        }

        #endregion

        
        //Hackish method to get past Unity serialization
        void DrawCustomMeshSettingsOptions()
        {

            EditorGUILayout.LabelField("Generate Custom Mesh?",EditorStyles.boldLabel);

            //Get Instance
            BAnyMeshSettingsForEditor bAny = inspectorMeshSettings; //BAnyMeshSettingsForEditor.Instance;

            //Build it!
            if (EditorHelpers.InspectorButton("Update Mesh", 100, 15, GUIBlue, "New/Change mesh"))
            {
                ((BSoftBodyWMesh)bSoftBodyTarget).meshSettings.UserMesh = bAny.Build();
                bSoftBodyTarget.BuildSoftBody();
            }

            bAny.imediateUpdate = EditorGUILayout.Toggle("Imediate Update", bAny.imediateUpdate);

            //Select a mesh type
            bAny.meshType = (PrimitiveMeshOptions)EditorGUILayout.EnumPopup("Mesh Type", bAny.meshType);

            switch (bAny.meshType)
            {
                case PrimitiveMeshOptions.UserDefinedMesh:
                    bAny.userMesh = (Mesh) EditorGUILayout.ObjectField(bAny.userMesh, typeof(Mesh),false);
                    break;
                case PrimitiveMeshOptions.Box:
                    bAny.extents = EditorGUILayout.Vector3Field("Extents", bAny.extents);
                    break;
                case PrimitiveMeshOptions.Sphere:
                    bAny.radius = EditorGUILayout.FloatField("radius", bAny.radius);
                    bAny.numLongitudeLines = EditorGUILayout.IntField("numLongitudeLines", bAny.numLongitudeLines);
                    bAny.numLatitudeLines = EditorGUILayout.IntField("numLatitudeLines", bAny.numLatitudeLines);

                    break;
                case PrimitiveMeshOptions.Cylinder:
                    bAny.height = EditorGUILayout.FloatField("height", bAny.height);
                    bAny.radius = EditorGUILayout.FloatField("radius", bAny.radius);
                    bAny.nbSides = EditorGUILayout.IntField("nbSides", bAny.nbSides);
                    break;
                case PrimitiveMeshOptions.Cone:
                    bAny.height = EditorGUILayout.FloatField("height", bAny.height);
                    bAny.radius = EditorGUILayout.FloatField("radius", bAny.radius);
                    bAny.nbSides = EditorGUILayout.IntField("nbSides", bAny.nbSides);
                    break;
                case PrimitiveMeshOptions.Pyramid:
                    bAny.height = EditorGUILayout.FloatField("height", bAny.height);
                    bAny.radius = EditorGUILayout.FloatField("radius", bAny.radius);
                    break;
                case PrimitiveMeshOptions.Bunny:
                    break;
                case PrimitiveMeshOptions.Plane:
                    bAny.length = EditorGUILayout.FloatField("length", bAny.length);
                    bAny.width = EditorGUILayout.FloatField("width", bAny.width);
                    bAny.resX = EditorGUILayout.IntField("resX", bAny.resX);
                    bAny.resZ = EditorGUILayout.IntField("resZ", bAny.resZ);
                    break;
                default:
                    break;
            }

            //limit the fields [Range()] doesnt work
            bAny.extents.x = Mathf.Clamp(bAny.extents.x, 0f, 10000f);
            bAny.extents.y = Mathf.Clamp(bAny.extents.y, 0f, 10000f);
            bAny.extents.z = Mathf.Clamp(bAny.extents.z, 0f, 10000f);
            bAny.radius = Mathf.Clamp(bAny.radius, 0f, 10000f);
            bAny.numLatitudeLines = Mathf.Clamp(bAny.numLatitudeLines, 2, 100);
            bAny.numLongitudeLines = Mathf.Clamp(bAny.numLongitudeLines, 2, 100);
            bAny.height = Mathf.Clamp(bAny.height, 0, 100);
            bAny.nbSides = Mathf.Clamp(bAny.nbSides, 2, 100);

            bAny.length = Mathf.Clamp(bAny.length, 0, 1000);
            bAny.width = Mathf.Clamp(bAny.width, 0, 1000);
            bAny.resX = Mathf.Clamp(bAny.resX, 2, 100);
            bAny.resZ = Mathf.Clamp(bAny.resZ, 2, 100);

            //AutoMagickally change settings is edited
            if (GUI.changed && bAny.imediateUpdate) //Can apply settings on editor change
            {
                ((BSoftBodyWMesh)bSoftBodyTarget).meshSettings.UserMesh = bAny.Build();
                bSoftBodyTarget.BuildSoftBody();
            }
            EditorGUILayout.LabelField("Mesh Settings", EditorStyles.boldLabel);
            EditorGUILayout.Space();
        }

    }
}