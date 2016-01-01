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

        //used to hide specific serialized properties in the editor
        //Hide softBody Settings until we want to display it, also script name
        private static readonly string[] hideMe = new string[] { "_softBodySettings", "m_Script" };

        const string collisionTooltip = "Collisions flags\n\n" +
       "SDF_RS Rigid versus soft mask.\n\n" +
       "CL_RS: SDF based rigid vs soft.\n\n" +
       "SVSmask: Cluster vs convex rigid vs soft.\n\n" +
       "VF_SS: Rigid versus soft mask.\n\n" +
       "CL_SS:Vertex vs face soft vs soft handling.\n\n" +
       "CL_SELF: Cluster vs cluster soft vs soft handling.\n\n" +
       "Default: Cluster soft body self collision.\n\n";

        GUIContent gcCollisionTooltip = new GUIContent("Collision Mask", collisionTooltip);

        public void OnEnable()
        {
            bSoftBodyTarget = (BSoftBody)target;
            softBodySettings = serializedObject.FindProperty("_softBodySettings");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            Color GUIBlue = new Color32(82, 140, 255, 255);

            EditorHelpers.DrawLogoAndVersion();

            DrawPropertiesExcluding(serializedObject, hideMe); //Draw settings after the default inspector
            //DrawDefaultInspector();//Items custom to this type of SoftBody

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
            bSoftBodyTarget.SoftBodySettings.config.Collisions = (BulletSharp.SoftBody.Collisions)EditorGUILayout.EnumMaskField(gcCollisionTooltip, bSoftBodyTarget.SoftBodySettings.config.Collisions);

            EditorGUILayout.PropertyField(softBodySettings, gcSoftBodySettings, true);

            serializedObject.ApplyModifiedProperties();

            if (GUI.changed) //Can apply settings on editor change
            {
                bSoftBodyTarget.BuildSoftBody();
            }



        }

        //Menu items here
        #region MenuBulletForUnity

        [MenuItem("BulletForUnity/BSoftBody/Rope")]
        [MenuItem("GameObject/Create Other/BulletForUnity/BSoftBody/Rope")]  //right click menu
        static void CreateBCube()
        {
            Selection.activeObject = BSoftBodyRope.CreateNew(EditorHelpers.GetCameraRaycastPosition(), Quaternion.identity);
            PostCreateObject();

        }


        [MenuItem("BulletForUnity/BSoftBody/BAnySoftObject")]
        [MenuItem("GameObject/Create Other/BulletForUnity/BSoftBody/BAnySoftObject")]  //right click menu
        static void CreateBAnySoftObject()
        {
            Selection.activeObject = BAnySoftObject.CreateNew(EditorHelpers.GetCameraRaycastPosition(), Quaternion.identity);
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



    }
}