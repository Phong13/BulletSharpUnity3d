using UnityEditor;
using UnityEngine;
using BulletSharp;

namespace BulletUnity.Primitives
{
    [CustomEditor(typeof(BPrimitive), true)]
    public class BPrimitiveEditor : Editor
    {

        public BPrimitive primitiveTarget;    

        SerializedProperty meshSettings;
        GUIContent gcMeshSettings = new GUIContent("MeshSettings");

        public void OnEnable()
        {
            primitiveTarget = (BPrimitive)target;
            meshSettings = serializedObject.FindProperty("meshSettings");
        }

        //Custom inspector
        public override void OnInspectorGUI()
        {

            serializedObject.Update();

            //EditorGUILayout.BeginHorizontal();
            ////Logo
            //GUIStyle logoGUIStyle = new GUIStyle();
            //logoGUIStyle.border = new RectOffset(0, 0, 0, 0);
            //GUILayout.Box(EditorHelpers.EditorLogo, logoGUIStyle);

            //Color GUIBlue = new Color32(82, 140, 255, 255);
            ////Color GUIGreen = new Color32(0, 160, 0, 255);

            ////Title/Version
            //GUILayout.Label("Bullet For Unity: " + EditorHelpers.version, EditorHelpers.versionStyle);
            //EditorGUILayout.Space();
            //EditorGUILayout.EndHorizontal();

            EditorHelpers.DrawLogoAndVersion();

            //EditorGUILayout.BeginHorizontal();
            //GUI.backgroundColor = GUIBlue;
            //if (InspectorButton("BuildMesh", 100, 40, GUIBlue, "Update size or something\n\n"))
            //{
            //    primitiveTarget.BuildMesh(); //build mesh and resize
            //}
            //EditorGUILayout.EndHorizontal();
            EditorGUILayout.PropertyField(meshSettings, gcMeshSettings, true);

            serializedObject.ApplyModifiedProperties();
            
            if (GUI.changed)
            {
                primitiveTarget.BuildMesh();
            }

            //draw default view
            //DrawDefaultInspector();
        }



        #region MenuBulletForUnity

        //[AddComponentMenu("BulletForUnity/Primitives/BCube")]
        [MenuItem("BulletForUnity/BBox")]
        [MenuItem("GameObject/Create Other/BulletForUnity/BBox")]  //right click menu
        static void CreateBCube()
        {
            Selection.activeObject = BBox.CreateNew(EditorHelpers.GetCameraRaycastPosition(), Quaternion.identity);
            PostCreateObject();

        }

        [MenuItem("BulletForUnity/BSphere")]
        [MenuItem("GameObject/Create Other/BulletForUnity/BSphere")]  //right click menu
        static void CreateBSphere()
        {
            Selection.activeObject = BSphere.CreateNew(EditorHelpers.GetCameraRaycastPosition(), Quaternion.identity);
            PostCreateObject();
        }

        [MenuItem("BulletForUnity/BCylinder")]
        [MenuItem("GameObject/Create Other/BulletForUnity/BCylinder")]  //right click menu
        static void CreateBCylinder()
        {
            Selection.activeObject = BCylinder.CreateNew(EditorHelpers.GetCameraRaycastPosition(), Quaternion.identity);
            PostCreateObject();
        }

        [MenuItem("BulletForUnity/BCone")]
        [MenuItem("GameObject/Create Other/BulletForUnity/BCone")]  //right click menu
        static void CreateBCone()
        {
            Selection.activeObject = BCone.CreateNew(EditorHelpers.GetCameraRaycastPosition(), Quaternion.identity);
            PostCreateObject();
        }

        //[MenuItem("BulletForUnity/BSoft")]
        //[MenuItem("GameObject/Create Other/BulletForUnity/BSoft")]  //right click menu
        //static void CreateBSoft()
        //{
        //    Selection.activeObject = BSoft.CreateNew(EditorHelpers.GetCameraRaycastPosition(), Quaternion.identity);
        //    PostCreateObject();
        //}

        static void PostCreateObject()
        {
            BPrimitiveComponentOrderSorter.SortComponents((GameObject)Selection.activeObject);  //order the scripts, looks nicer

        }


        #endregion

    

    }
}
