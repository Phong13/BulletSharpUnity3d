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
}
