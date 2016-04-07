using UnityEngine;
using System.Collections;
using UnityEditor;
using BulletUnity.Debugging;

public class EditorInterface : Editor 
{
	/// <summary>
	/// Draw a box for select the debug mode of this object.
	/// </summary>
	/// <param name="debug">DebugType</param>
	public static BDebug.DebugType DrawDebug(BDebug.DebugType debugType, Object undoObject) 
	{
		EditorGUILayout.LabelField("Debug", EditorStyles.boldLabel);

		//Check if the interface changed for perform an undo record.
		EditorGUI.BeginChangeCheck();
		debugType = (BDebug.DebugType)EditorGUILayout.EnumMaskField(debugType);
		if(EditorGUI.EndChangeCheck()) 
		{
			Undo.RecordObject(undoObject, "Debug Mode");
		}

		return debugType;
	}

    public static class Layout
    {
        public static Vector3 DrawVector3(string label, Vector3 value, Object undoObject)
        {
            EditorGUI.BeginChangeCheck();
            value = EditorGUILayout.Vector3Field(label, value);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(undoObject, label);
            }
            return value;
        }

        public static Vector3 DrawVector3(string label, Vector3 value, string undoName, Object undoObject)
        {
            EditorGUI.BeginChangeCheck();
            value = EditorGUILayout.Vector3Field(label, value);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(undoObject, undoName);
            }
            return value;
        }

        public static float DrawFloat(string label, float value, Object undoObject)
        {
            EditorGUI.BeginChangeCheck();
            value = EditorGUILayout.FloatField(label, value);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(undoObject, label);
            }
            return value;
        }

        public static float DrawFloat(string label, float value, string undoName, Object undoObject)
        {
            EditorGUI.BeginChangeCheck();
            value = EditorGUILayout.FloatField(label, value);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(undoObject, undoName);
            }
            return value;
        }

        public static bool DrawToggle(string label, bool value, Object undoObject)
        {
            EditorGUI.BeginChangeCheck();
            value = EditorGUILayout.Toggle(label, value);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(undoObject, label);
            }
            return value;
        }

        public static bool DrawToggle(string label, bool value, string undoName, Object undoObject)
        {
            EditorGUI.BeginChangeCheck();
            value = EditorGUILayout.Toggle(label, value);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(undoObject, undoName);
            }
            return value;
        }
    }
}
