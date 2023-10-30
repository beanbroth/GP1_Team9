#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;

public class WeaponInventoryEditor : Editor
{
    [MenuItem("Assets/Populate Weapon Inventory")]
    public static void PopulateWeaponInventory()
    {
        SO_WeaponInventory weaponInventory = Selection.activeObject as SO_WeaponInventory;
        if (weaponInventory == null)
        {
            Debug.LogError("Please select a SO_WeaponInventory asset.");
            return;
        }

        string weaponInventoryPath = AssetDatabase.GetAssetPath(weaponInventory);
        string weaponInventoryDirectory = Path.GetDirectoryName(weaponInventoryPath);
        string[] guids = AssetDatabase.FindAssets("t:SO_SingleWeaponClass", new[] { weaponInventoryDirectory });

        weaponInventory.weaponClassDatabase.Clear();
        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            SO_SingleWeaponClass weaponData = AssetDatabase.LoadAssetAtPath<SO_SingleWeaponClass>(assetPath);
            if (weaponData != null)
            {
                weaponInventory.weaponClassDatabase.Add(weaponData);
            }
        }

        EditorUtility.SetDirty(weaponInventory);
        AssetDatabase.SaveAssets();
        Debug.Log("Populated weapon inventory with S_WeaponData scriptable objects.");
    }
}
#endif