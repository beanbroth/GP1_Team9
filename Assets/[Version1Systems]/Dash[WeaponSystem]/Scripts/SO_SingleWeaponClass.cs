using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "WeaponSystem/WeaponData", order = 0)]
public class SO_SingleWeaponClass : ScriptableObject
{
    public string weaponName;
    public List<GameObject> weaponPrefabs = new List<GameObject>();
    public List<string> weaponDescriptions = new List<string>();

    public List<GameObject> WeaponPrefabs
    {
        get
        {
            if (weaponPrefabs.Count == 0)
            {
                Debug.LogError("Weapon Prefabs not set. Put them in the weapon scriptable object");
            }

            return weaponPrefabs;
        }
    }
}