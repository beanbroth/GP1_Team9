using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(S_UpgradeCardManager))]
public class UpgradeCardPlacerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        S_UpgradeCardManager myScript = (S_UpgradeCardManager)target;

        if (GUILayout.Button("Refresh Cards"))
        {
            myScript.DisplayCards();
        }
    }
}