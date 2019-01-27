using System;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using BulletUnity;

[CustomEditor(typeof(BCollisionObject))]
public class BCollisionObjectEditor : Editor
{
    //BulletSharp has a class for CollisionFilterGroups but the Editor EnumMaskField doesn't like the None=0 
    [Flags]
    public enum GUICollisionFlags
    {
        StaticObject = 1,
        KinematicObject = 2,
        NoContactResponse = 4,
        CustomMaterialCallback = 8,
        CharacterObject = 16,
        DisableVisualizeObject = 32,
        DisableSpuCollisionProcessing = 64
    }

    //BulletSharp has a class for CollisionFilterGroups but the Editor EnumMaskField doesn't like the None=0 and AllFilter = -1 entries
    //The EnumMaskField does not like enums that define 0
    //Also it doesn't like an enum with value -1
    [Flags]
    public enum GUICollisionFilterGroups
    {
        Everything = -1,
        DefaultFilter = 1,
        StaticFilter = 2,
        KinematicFilter = 4,
        DebrisFilter = 8,
        SensorTrigger = 16,
        CharacterFilter = 32,
        BallFilter = 64,
        BallCollisionFilter = 128,
        ItemBoxCollisionFilter = 256,
        ObstacleCollisionFilter = 512,
    }

    static string PrintBits(int num)
    {
        byte[] bytes = BitConverter.GetBytes(num);
        string s = "";
        int bitPos = 0;
        while (bitPos < 8 * bytes.Length)
        {
            int byteIndex = bitPos / 8;
            int offset = bitPos % 8;
            bool isSet = (bytes[byteIndex] & (1 << offset)) != 0;

            if (isSet) s += "1";
            else s += "0";
            // isSet = [True] if the bit at bitPos is set, false otherwise

            bitPos++;
        }
        return s;
    }

    public static BulletSharp.CollisionFlags RenderEnumMaskCollisionFlagsField(GUIContent guiContent, BulletSharp.CollisionFlags enumVal)
    {
        GUICollisionFlags g = (GUICollisionFlags) enumVal;
        g = (GUICollisionFlags)EditorGUILayout.EnumMaskField(guiContent, g);
        return (BulletSharp.CollisionFlags) g;
    }

    public static BulletSharp.CollisionFilterGroups RenderEnumMaskCollisionFilterGroupsField(GUIContent guiContent, BulletSharp.CollisionFilterGroups enumVal)
    {
        GUICollisionFilterGroups g = (GUICollisionFilterGroups) enumVal;
        g = (GUICollisionFilterGroups)EditorGUILayout.EnumMaskField(guiContent, g);
        return (BulletSharp.CollisionFilterGroups)g;
    }

    public static GUIContent gcCollisionFlags = new GUIContent("Collision Flags");
    public static GUIContent gcGroupsIBelongTo = new GUIContent("Collision Groups", "These are the collision groups this object belongs to. This object will only collide with another object " +
                                                  "if one of these groups matches the collision mask on the other object AND the one of the groups on the other object matches the " +
                                                  " collision mask on this object.");
    public static GUIContent gcCollisionMask = new GUIContent("Collision Mask", "Another object will only collide with this object if a group of the other object matches this mask AND a group on this object " +
                                                  " matches the mask on the other object.");

    public override void OnInspectorGUI()
    {
        BCollisionObject obj = (BCollisionObject) target;

        obj.collisionFlags = BCollisionObjectEditor.RenderEnumMaskCollisionFlagsField(gcCollisionFlags, obj.collisionFlags);
        obj.groupsIBelongTo = BCollisionObjectEditor.RenderEnumMaskCollisionFilterGroupsField(gcGroupsIBelongTo, obj.groupsIBelongTo);
        obj.collisionMask = BCollisionObjectEditor.RenderEnumMaskCollisionFilterGroupsField(gcCollisionMask, obj.collisionMask);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(obj);
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            Undo.RecordObject(obj, "Undo Rigid Body");
        }
    }
}
