using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(S_CirclePlacement))]
public class CirclePlacementEditor : Editor
{
    public override void OnInspectorGUI()
    {
        S_CirclePlacement circlePlacement = (S_CirclePlacement)target;

        circlePlacement.radius = EditorGUILayout.FloatField("Radius", circlePlacement.radius);
        circlePlacement.numberOfObjects = EditorGUILayout.IntField("Number Of Objects", circlePlacement.numberOfObjects);
        circlePlacement.prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", circlePlacement.prefab, typeof(GameObject), false);

        if(GUILayout.Button("Place Objects"))
        {
            circlePlacement.PlaceObjects();
        }
    }
}