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
        DefaultFilter = 1,
        StaticFilter = 2,
        KinematicFilter = 4,
        DebrisFilter = 8,
        SensorTrigger = 16,
        CharacterFilter = 32,
        AllFilter = 64,
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

    public static BulletSharp.CollisionFilterGroups ConvertGUIEnumToBulletSharpEnum_CollisionFilterGroups(GUICollisionFilterGroups v)
    {
        BulletSharp.CollisionFilterGroups vout = BulletSharp.CollisionFilterGroups.None;
        if ((v & GUICollisionFilterGroups.DefaultFilter) != 0) vout = vout | BulletSharp.CollisionFilterGroups.DefaultFilter;
        if ((v & GUICollisionFilterGroups.StaticFilter) != 0) vout = vout | BulletSharp.CollisionFilterGroups.StaticFilter;
        if ((v & GUICollisionFilterGroups.KinematicFilter) != 0) vout = vout | BulletSharp.CollisionFilterGroups.KinematicFilter;
        if ((v & GUICollisionFilterGroups.DebrisFilter) != 0) vout = vout | BulletSharp.CollisionFilterGroups.DebrisFilter;
        if ((v & GUICollisionFilterGroups.SensorTrigger) != 0) vout = vout | BulletSharp.CollisionFilterGroups.SensorTrigger;
        if ((v & GUICollisionFilterGroups.CharacterFilter) != 0) vout = vout | BulletSharp.CollisionFilterGroups.CharacterFilter;
        if ((v & GUICollisionFilterGroups.AllFilter) != 0)
        {
            vout = BulletSharp.CollisionFilterGroups.AllFilter;
        }
        return vout;
    }

    public static GUICollisionFilterGroups ConvertBulletSharpEnumToGUIEnum_CollisionFilterGroups(BulletSharp.CollisionFilterGroups v)
    {
        int vout = 0;
        if (v == BulletSharp.CollisionFilterGroups.AllFilter)
        {
            vout = (int)GUICollisionFilterGroups.AllFilter;
        }
        else {
            if ((v & BulletSharp.CollisionFilterGroups.DefaultFilter) != 0) vout = vout | (int)GUICollisionFilterGroups.DefaultFilter;
            if ((v & BulletSharp.CollisionFilterGroups.StaticFilter) != 0) vout = vout | (int)GUICollisionFilterGroups.StaticFilter;
            if ((v & BulletSharp.CollisionFilterGroups.KinematicFilter) != 0) vout = vout | (int)GUICollisionFilterGroups.KinematicFilter;
            if ((v & BulletSharp.CollisionFilterGroups.DebrisFilter) != 0) vout = vout | (int)GUICollisionFilterGroups.DebrisFilter;
            if ((v & BulletSharp.CollisionFilterGroups.SensorTrigger) != 0) vout = vout | (int)GUICollisionFilterGroups.SensorTrigger;
            if ((v & BulletSharp.CollisionFilterGroups.CharacterFilter) != 0) vout = vout | (int)GUICollisionFilterGroups.CharacterFilter;
        }
        if (vout == 0)
        {
            vout = (int)GUICollisionFilterGroups.DefaultFilter;
        }
        
        return (GUICollisionFilterGroups) vout;
    }

    public static BulletSharp.CollisionFlags RenderEnumMaskCollisionFlagsField(GUIContent guiContent, BulletSharp.CollisionFlags enumVal)
    {
        GUICollisionFlags g = (GUICollisionFlags) enumVal;
        g = (GUICollisionFlags)EditorGUILayout.EnumMaskField(guiContent, g);
        return (BulletSharp.CollisionFlags) g;
    }

    public static BulletSharp.CollisionFilterGroups RenderEnumMaskCollisionFilterGroupsField(GUIContent guiContent, BulletSharp.CollisionFilterGroups enumVal)
    {
        GUICollisionFilterGroups g = ConvertBulletSharpEnumToGUIEnum_CollisionFilterGroups(enumVal);
        g = (GUICollisionFilterGroups)EditorGUILayout.EnumMaskField(guiContent, g);
        return ConvertGUIEnumToBulletSharpEnum_CollisionFilterGroups(g);
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
