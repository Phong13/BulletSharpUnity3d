using UnityEditor;
using UnityEngine;

namespace BulletUnity
{
    public static class EditorHelpers
    {

        //BulletUnity version
        public static GUIStyle versionStyle = new GUIStyle();
        public static string version = "0.1";
        
        private static Texture2D editorLogo;
        public static Texture2D EditorLogo
        {
            get { return editorLogo = (Texture2D)EditorHelpers.LoadAsset(editorLogo, "bulletLogo"); }
        }


        //Draw the UnityBullet Logo and version on scripts that need it
        public static void DrawLogoAndVersion()
        {
			EditorGUILayout.LabelField(new GUIContent(EditorLogo), GUILayout.MinHeight(64.0f), GUILayout.ExpandWidth(false));
			//EditorGUILayout.LabelField(string.Format("Bullet Version: {0}", version));
			EditorGUILayout.Separator();
        }


        public static Vector3 GetCameraRaycastPosition()
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

        public static Object LoadAsset(Object asset, string path)
        {

            if (asset == null)
            {
                asset = Resources.Load(path);
            }
            return asset;
        }

        public static bool InspectorButton(string label, int width, int height, Color color, string text)
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

        public static bool InspectorButton(string label, int width, int height, Color color)
        {
            bool clicked = false;

            //EditorGUILayout.BeginHorizontal();
            GUI.backgroundColor = color;
            if (GUILayout.Button(label, GUILayout.Width(width), GUILayout.Height(height)))
            {
                clicked = true;
            }
            GUI.backgroundColor = Color.white;
            //EditorGUILayout.HelpBox(text, MessageType.None, true);
            //EditorGUILayout.EndHorizontal();

            return clicked;
        }


    }
}
