using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

[CustomEditor(typeof(SO_SingleWeaponClass))]
public class SingleWeaponClassEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SO_SingleWeaponClass weaponData = (SO_SingleWeaponClass)target;

        if (GUILayout.Button("Add All Prefabs in Folder"))
        {
            string path = AssetDatabase.GetAssetPath(weaponData);
            string folderPath = Path.GetDirectoryName(path);

            var prefabGUIDs = AssetDatabase.FindAssets("t:Prefab", new[] { folderPath });
            var prefabs = prefabGUIDs.Select(guid => AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(guid))).ToList();

            weaponData.weaponPrefabs = prefabs;
            EditorUtility.SetDirty(weaponData);
        }
    }
}