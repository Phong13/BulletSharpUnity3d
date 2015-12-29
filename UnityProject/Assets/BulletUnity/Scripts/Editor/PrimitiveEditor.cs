using UnityEditor;
using UnityEngine;
using BulletSharp;

namespace BulletUnity.Primitives
{
    [CustomEditor(typeof(BPrimitive), true)]
    public class BPrimitiveEditor : Editor
    {

        public BPrimitive primitiveTarget;
        
        static GUIStyle versionStyle = new GUIStyle();
        static string version = "0.1";
        
        private static Texture2D editorLogo;
        public static Texture2D EditorLogo
        {
            get { return editorLogo = (Texture2D)LoadAsset(editorLogo, "bulletLogo"); }
        }

        SerializedProperty meshSettings;
        GUIContent gcSize = new GUIContent("MeshSettings");

        public void OnEnable()
        {
            primitiveTarget = (BPrimitive)target;
            versionStyle.fontSize = 10;
            meshSettings = serializedObject.FindProperty("meshSettings");
        }

        //Custom inspector
        public override void OnInspectorGUI()
        {

            serializedObject.Update();

            EditorGUILayout.BeginHorizontal();
            //Logo
            GUIStyle logoGUIStyle = new GUIStyle();
            logoGUIStyle.border = new RectOffset(0, 0, 0, 0);
            GUILayout.Box(EditorLogo, logoGUIStyle);

            Color GUIBlue = new Color32(82, 140, 255, 255);
            //Color GUIGreen = new Color32(0, 160, 0, 255);

            //Title/Version
            GUILayout.Label("Bullet For Unity: " + version, versionStyle);
            EditorGUILayout.Space();
            EditorGUILayout.EndHorizontal();

            //EditorGUILayout.BeginHorizontal();
            //GUI.backgroundColor = GUIBlue;
            //if (InspectorButton("BuildMesh", 100, 40, GUIBlue, "Update size or something\n\n"))
            //{
            //    primitiveTarget.BuildMesh(); //build mesh and resize
            //}
            //EditorGUILayout.EndHorizontal();
            EditorGUILayout.PropertyField(meshSettings, gcSize, true);

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
            Selection.activeObject = BBox.CreateNew(GetCameraRaycastPosition(), Quaternion.identity);
            PostCreateObject();

        }

        [MenuItem("BulletForUnity/BSphere")]
        [MenuItem("GameObject/Create Other/BulletForUnity/BSphere")]  //right click menu
        static void CreateBSphere()
        {
            Selection.activeObject = BSphere.CreateNew(GetCameraRaycastPosition(), Quaternion.identity);
            PostCreateObject();
        }

        [MenuItem("BulletForUnity/BCylinder")]
        [MenuItem("GameObject/Create Other/BulletForUnity/BCylinder")]  //right click menu
        static void CreateBCylinder()
        {
            Selection.activeObject = BCylinder.CreateNew(GetCameraRaycastPosition(), Quaternion.identity);
            PostCreateObject();
        }

        [MenuItem("BulletForUnity/BCone")]
        [MenuItem("GameObject/Create Other/BulletForUnity/BCone")]  //right click menu
        static void CreateBCone()
        {
            Selection.activeObject = BCone.CreateNew(GetCameraRaycastPosition(), Quaternion.identity);
            PostCreateObject();
        }

        [MenuItem("BulletForUnity/BSoft")]
        [MenuItem("GameObject/Create Other/BulletForUnity/BSoft")]  //right click menu
        static void CreateBSoft()
        {
            Selection.activeObject = BSoft.CreateNew(GetCameraRaycastPosition(), Quaternion.identity);
            PostCreateObject();
        }

        static void PostCreateObject()
        {
            BPrimitiveComponentOrderSorter.SortComponents((GameObject)Selection.activeObject);  //order the scripts, looks nicer

        }


        #endregion

        static Vector3 GetCameraRaycastPosition()
        {
            Ray ray = GetCenterRay();
            RaycastHit hitInfo;
            Vector3 position = Vector3.zero;
            float rayDistance = 0f;

            //ray-plane intersection
            if (new Plane(Vector3.up, Vector3.zero).Raycast(ray, out rayDistance))
            {
                position = ray.GetPoint(rayDistance);
            }
            if (rayDistance <= 0f || rayDistance > 20f)
            {
                if (Physics.Raycast(ray, out hitInfo))  //raycast on existing geometry
                {
                    position = hitInfo.point;
                }
                else //place it x units from the camera
                {
                    position = ray.origin + ray.direction * 10.0f;
                }
            }
            return position;
        }


        //Get a ray in the world from editor camera to middle of the screen
        public static Ray GetCenterRay()
        {
            Camera camera = SceneView.lastActiveSceneView.camera;
            return GetScreenRay(camera, new Vector2(camera.pixelWidth / 2, camera.pixelHeight / 2));
        }

        public static Ray GetScreenRay(Camera camera, Vector2 pos)
        {
            return camera.ScreenPointToRay(new Vector2(pos.x, camera.pixelHeight - pos.y));
        }


        private bool InspectorButton(string label, int width, int height, Color color, string text)
        {
            bool clicked = false;

            EditorGUILayout.BeginHorizontal();
            GUI.backgroundColor = color;
            if (GUILayout.Button(label, GUILayout.Width(width), GUILayout.Height(height)))
            {
                clicked = true;
            }
            GUI.backgroundColor = Color.white;
            EditorGUILayout.HelpBox(text, MessageType.None, true);
            EditorGUILayout.EndHorizontal();

            return clicked;
        }

        private static Object LoadAsset(Object asset, string path)
        {

            if (asset == null)
            {
                asset = Resources.Load(path);
            }
            return asset;
        }


    }
}
