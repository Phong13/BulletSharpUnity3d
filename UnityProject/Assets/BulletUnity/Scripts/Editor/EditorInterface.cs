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
	public static BDebug.DebugType DrawDebug(BDebug.DebugType debug, MonoBehaviour script) 
	{
		EditorGUILayout.LabelField("Debug", EditorStyles.boldLabel);

		//Check if the interface changed for perform an undo record.
		EditorGUI.BeginChangeCheck();
		debug = (BDebug.DebugType)EditorGUILayout.EnumMaskField(debug);
		if(EditorGUI.EndChangeCheck()) 
		{
			Undo.RecordObject(script, "Debug Mode");
		}

		return debug;
	}
}
