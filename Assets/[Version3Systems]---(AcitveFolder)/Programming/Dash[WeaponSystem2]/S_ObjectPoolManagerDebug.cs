using UnityEngine;
using UnityEditor;


public class S_ObjectPoolManagerDebug : EditorWindow
{
    [MenuItem("Window/Object Pool Manager Debug")]
    public static void ShowWindow()
    {
        GetWindow<S_ObjectPoolManagerDebug>("Object Pool Manager Debug");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Object Pool Manager Debug", EditorStyles.boldLabel);

        EditorGUILayout.Space();

        if (GUILayout.Button("Refresh"))
        {
            Repaint();
        }

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Pooled Objects:", EditorStyles.boldLabel);

        var poolInfoList = ObjectPoolManager.PoolInfoList;
        foreach (var poolInfo in poolInfoList)
        {
            EditorGUILayout.LabelField("Key: " + poolInfo.Key);
            EditorGUILayout.LabelField("Max Spawned Objects: " + poolInfo.MaxSpawnedObjects);
            EditorGUILayout.LabelField("Object Count: " + poolInfo.ObjectCount);
            EditorGUILayout.Space();
        }
    }
}