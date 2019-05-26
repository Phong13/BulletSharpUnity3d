using UnityEditor;
using UnityEngine;

namespace BulletUnity
{
    public static class GraphDisplay
    {
        static Material material;
        static Material Material
        {
            get
            {
                if (material == null)
                {
                    material = new Material(Shader.Find("Hidden/Internal-Colored"));
                }
                return material;
            }
        }

        public static void DisplayGraph(string graphName, double[] data, float limit, Color inLimitColor, Color outLimitColor)
        {

            EditorGUILayout.LabelField(graphName);
            // Begin to draw a horizontal layout, using the helpBox EditorStyle
            GUILayout.BeginHorizontal(EditorStyles.helpBox);

            // Reserve GUI space with a width from 10 to 10000, and a fixed height of 100, and 
            // cache it as a rectangle.
            Rect layoutRectangle = GUILayoutUtility.GetRect(10, 10000, 100, 100);

            if (Event.current.type == EventType.Repaint)
            {

                GUI.BeginClip(layoutRectangle);
                GL.PushMatrix();

                GL.Clear(true, false, Color.black);
                Material.SetPass(0);

                GL.Begin(GL.LINES);

                float height = layoutRectangle.height;

                float xdivisions = layoutRectangle.width / data.Length;
                float ydivisions = height / (limit * 5);

                GL.Color(Color.grey);

                GL.Vertex3(0, height - ydivisions * limit, 0);
                GL.Vertex3(layoutRectangle.width, height - ydivisions * limit, 0);

                // step time
                for (int h = 0; h < data.Length; h++)
                {

                    GL.Color(data[h] > limit ? outLimitColor : inLimitColor);
                    float y = Mathf.Min((float)data[h] * ydivisions, height);
                    float x = h * xdivisions;

                    // first line of X
                    GL.Vertex3(x, height, 0);
                    GL.Vertex3(x, height - y, 0);
                }

                GL.End();
                GL.PopMatrix();
                GUI.EndClip();
            }
            GUILayout.EndHorizontal();
        }
    }
}