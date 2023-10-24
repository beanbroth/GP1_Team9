#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

//[CustomEditor(typeof(S_ObjectPoolManager))]
//public class PoolManagerEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI();

//        S_ObjectPoolManager poolManager = (S_ObjectPoolManager)target;

//        EditorGUILayout.LabelField("Pools", EditorStyles.boldLabel);
        
//        foreach (S_ObjectPoolManager.PoolInfo poolInfo in poolManager.PoolInfoList)
//        {
//            EditorGUILayout.BeginHorizontal();
//            EditorGUILayout.LabelField(poolInfo.Key, GUILayout.Width(300));
//            EditorGUILayout.LabelField("Max Spawned:", GUILayout.Width(80));
//            EditorGUILayout.LabelField(poolInfo.MaxSpawnedObjects.ToString(), GUILayout.Width(50));
//            EditorGUILayout.LabelField("Remaining:", GUILayout.Width(70));
//            EditorGUILayout.LabelField(poolInfo.ObjectCount.ToString(), GUILayout.Width(50));
//            EditorGUILayout.EndHorizontal();
//        }
//    }
//}

#endif
