#if UNITY_EDITOR

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

            // Clear the list of weapon prefabs
            weaponData.weaponPrefabs.Clear();

            for (int i = 0; i < prefabs.Count; i++)
            {
                // Add each prefab to the list
                weaponData.weaponPrefabs.Add(prefabs[i]);

                // Check if there's an existing description, otherwise add the default
                if (weaponData.weaponDescriptions.Count <= i || string.IsNullOrEmpty(weaponData.weaponDescriptions[i]))
                {
                    string defaultDescription = $"Default Weapon Upgrade Description For: {weaponData.weaponPrefabs[i]}";
                    if (weaponData.weaponDescriptions.Count > i) // If the index exists
                    {
                        //weaponData.weaponDescriptions[i] = defaultDescription;
                    }
                    else
                    {
                        weaponData.weaponDescriptions.Add(defaultDescription);
                    }
                }
            }

            EditorUtility.SetDirty(weaponData);
        }
    }
}
#endif