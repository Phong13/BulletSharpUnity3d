using BulletUnity;
using UnityEditor;

[CustomEditor(typeof(BRigidBody))]
[CanEditMultipleObjects]
public class BRigidBodyEditor : Editor
{
    BRigidBody rb;
    private SerializedProperty m_collisionFlagsProp;
    private SerializedProperty m_groupsIBelongToProp;
    private SerializedProperty m_collisionMaskProp;
    private SerializedProperty _massProp;
    private SerializedProperty _linearFactorProp;
    private SerializedProperty _angularFactorProp;
    private SerializedProperty _frictionProp;
    private SerializedProperty _rollingFrictionProp;
    private SerializedProperty _linearDampingProp;
    private SerializedProperty _angularDampingProp;
    private SerializedProperty _additionalDampingProp;
    private SerializedProperty _additionalDampingFactorProp;
    private SerializedProperty _additionalLinearDampingThresholdSqrProp;
    private SerializedProperty _additionalAngularDampingThresholdSqrProp;
    private SerializedProperty _additionalAngularDampingFactorProp;
    private SerializedProperty _restitutionProp;
    private SerializedProperty _linearSleepingThresholdProp;
    private SerializedProperty _angularSleepingThresholdProp;
    private SerializedProperty debugTypeProp;

    void OnEnable()
    {
        rb = (BRigidBody)target;
        m_collisionFlagsProp = serializedObject.FindProperty("m_collisionFlags");
        m_groupsIBelongToProp = serializedObject.FindProperty("m_groupsIBelongTo");
        m_collisionMaskProp = serializedObject.FindProperty("m_collisionMask");
        _massProp = serializedObject.FindProperty("_mass");
        _linearFactorProp = serializedObject.FindProperty("_linearFactor");
        _angularFactorProp = serializedObject.FindProperty("_angularFactor");
        _frictionProp = serializedObject.FindProperty("_friction");
        _rollingFrictionProp = serializedObject.FindProperty("_rollingFriction");
        _linearDampingProp = serializedObject.FindProperty("_linearDamping");
        _angularDampingProp = serializedObject.FindProperty("_angularDamping");
        _additionalDampingProp = serializedObject.FindProperty("_additionalDamping");
        _additionalDampingFactorProp = serializedObject.FindProperty("_additionalDampingFactor");
        _additionalLinearDampingThresholdSqrProp =
            serializedObject.FindProperty("_additionalLinearDampingThresholdSqr");
        _additionalAngularDampingThresholdSqrProp = serializedObject.FindProperty("_additionalAngularDampingThresholdSqr");
        _additionalAngularDampingFactorProp = serializedObject.FindProperty("_additionalAngularDampingFactor");
        _restitutionProp = serializedObject.FindProperty("_restitution");
        _linearSleepingThresholdProp = serializedObject.FindProperty("_linearSleepingThreshold");
        _angularSleepingThresholdProp = serializedObject.FindProperty("_angularSleepingThreshold");
        debugTypeProp = serializedObject.FindProperty("debugType");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.LabelField("Collision", EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(m_collisionFlagsProp);
        EditorGUILayout.PropertyField(m_groupsIBelongToProp);
        EditorGUILayout.PropertyField(m_collisionMaskProp);

        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Object", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_massProp);

        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Limits", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_linearFactorProp);
        EditorGUILayout.PropertyField(_angularFactorProp);

        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Friction", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_frictionProp);
        EditorGUILayout.PropertyField(_rollingFrictionProp);

        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Damping", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_linearDampingProp);
        EditorGUILayout.PropertyField(_angularDampingProp);
        EditorGUILayout.PropertyField(_additionalDampingProp);

        if (rb.additionalDamping)
        {
            EditorGUILayout.PropertyField(_additionalDampingFactorProp);
            EditorGUILayout.PropertyField(_additionalLinearDampingThresholdSqrProp);
            EditorGUILayout.PropertyField(_additionalAngularDampingThresholdSqrProp);
            EditorGUILayout.PropertyField(_additionalAngularDampingFactorProp);
        }

        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Other Settings", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_restitutionProp);
        EditorGUILayout.PropertyField(_linearSleepingThresholdProp);
        EditorGUILayout.PropertyField(_angularSleepingThresholdProp);

        EditorGUILayout.PropertyField(debugTypeProp);

        rb.debugType = EditorInterface.DrawDebug(rb.debugType, rb);

        if (rb.debugType != 0)
        {
            EditorGUILayout.LabelField(string.Format("Velocity {0}", rb.velocity));
            EditorGUILayout.LabelField(string.Format("Angular Velocity {0}", rb.angularVelocity));
        }

        serializedObject.ApplyModifiedProperties();
    }
}
