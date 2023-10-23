#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(S_PlayerBulletSpawner))]
public class BasicBulletSpawnerEditor : Editor
{
    SerializedProperty angleCenterProp;
    SerializedProperty angleWidthProp;

    void OnEnable()
    {
        angleCenterProp = serializedObject.FindProperty("angleCenter");
        angleWidthProp = serializedObject.FindProperty("angleWidth");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawPropertiesExcluding(serializedObject, "angleCenter", "angleWidth");

        EditorGUILayout.PropertyField(angleCenterProp);
        EditorGUILayout.PropertyField(angleWidthProp);

        float minAngle = angleCenterProp.floatValue - angleWidthProp.floatValue / 2;
        float maxAngle = angleCenterProp.floatValue + angleWidthProp.floatValue / 2;
        EditorGUILayout.MinMaxSlider(new GUIContent("Angle Range"), ref minAngle, ref maxAngle, -180f, 180f);
        angleCenterProp.floatValue = (minAngle + maxAngle) / 2;
        angleWidthProp.floatValue = maxAngle - minAngle;

        serializedObject.ApplyModifiedProperties();
    }
}
#endif