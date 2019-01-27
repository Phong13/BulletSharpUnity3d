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

        private static Texture2D editorLogo;
        public static Texture2D EditorLogo
        {
            get
            {
                if (editorLogo == null)
                {
                    editorLogo = (Texture2D)LoadAsset(editorLogo, "bulletLogo");
                }
                return editorLogo;
            }
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

            //EditorGUILayout.BeginHorizontal();
            //Logo
            GUIStyle logoGUIStyle = new GUIStyle();
            logoGUIStyle.border = new RectOffset(0, 0, 0, 0);
            //GUILayout.Box(EditorLogo, GUILayout.Height(64), GUILayout.ExpandWidth(true));
            EditorGUILayout.LabelField(new GUIContent(EditorLogo), GUILayout.MinHeight(64.0f), GUILayout.ExpandWidth(true));

            Color GUIBlue = new Color32(192, 219, 255, 255);

            //Title/Version
            /*
            GUILayout.Label("Bullet For Unity: " + version, versionStyle);
            EditorGUILayout.Space();
            */
            //EditorGUILayout.EndHorizontal();
            MonoBehaviour mb = (MonoBehaviour)target;
            if (mb.transform.localScale != Vector3.one)
            {
                EditorGUILayout.HelpBox("Transform Scale must be 1,1,1. Use the Mesh Settings to scale the object.",MessageType.Error);
            }
            EditorGUILayout.BeginHorizontal();
            if (InspectorButton("Build Mesh", 100, 40, GUIBlue, "Update mesh and Bullet Collision shape settings\n\n"))
            {
                primitiveTarget.BuildMesh(); //build mesh and resize
            }
            EditorGUILayout.EndHorizontal();
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

        [AddComponentMenu("BulletForUnity/Primitives/BCube")]
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

        [MenuItem("BulletForUnity/BCapsule")]
        [MenuItem("GameObject/Create Other/BulletForUnity/BCapsule")]  //right click menu
        static void CreateBCapsule()
        {
            Selection.activeObject = BCapsule.CreateNew(GetCameraRaycastPosition(), Quaternion.identity);
            PostCreateObject();
        }

        [MenuItem("BulletForUnity/BCone")]
        [MenuItem("GameObject/Create Other/BulletForUnity/BCone")]  //right click menu
        static void CreateBCone()
        {
            Selection.activeObject = BCone.CreateNew(GetCameraRaycastPosition(), Quaternion.identity);
            PostCreateObject();
        }

        [MenuItem("BulletForUnity/BConvexHull")]
        [MenuItem("GameObject/Create Other/BulletForUnity/BConvexHull")]  //right click menu
        static void CreateBConvexHull()
        {
            Selection.activeObject = BConvexHull.CreateNew(GetCameraRaycastPosition(), Quaternion.identity);
            PostCreateObject();
        }

        [MenuItem("BulletForUnity/BConvexTriMesh")]
        [MenuItem("GameObject/Create Other/BulletForUnity/BConvexTriMesh")]  //right click menu
        static void CreateBConvexTriMesh()
        {
            Selection.activeObject = BConvexTriMesh.CreateNew(GetCameraRaycastPosition(), Quaternion.identity);
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
            if (SceneView.lastActiveSceneView != null && SceneView.lastActiveSceneView.camera != null)
            {
                Camera camera = SceneView.lastActiveSceneView.camera;
                return GetScreenRay(camera, new Vector2(camera.pixelWidth / 2, camera.pixelHeight / 2));
            } else
            {
                return new Ray(Vector3.zero, -Vector3.forward);
            }
        }

        public static Ray GetScreenRay(Camera camera, Vector2 pos)
        {
            return camera.ScreenPointToRay(new Vector2(pos.x, camera.pixelHeight - pos.y));
        }


        private bool InspectorButton(string label, int width, int height, Color c, string text)
        {
            bool clicked = false;

            EditorGUILayout.BeginHorizontal();
            GUI.backgroundColor = c;
            if (GUILayout.Button(label, GUILayout.Width(width), GUILayout.Height(height)))
            {
                clicked = true;
            }
            GUI.backgroundColor = Color.white;
            EditorGUILayout.HelpBox(text, MessageType.Info, true);
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
