using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class S_WeaponManager : MonoBehaviour
{
    [SerializeField] SO_WeaponInventory weaponInventory;
    [SerializeField] Transform weaponSpawnPoint;

    void Start()
    {
        UpdateWeapons();
        SO_WeaponInventory.OnWeaponInfoChange += UpdateWeapons;
    }

    private void OnDestroy()
    {
        SO_WeaponInventory.OnWeaponInfoChange -= UpdateWeapons;
    }

    void UpdateWeapons()
    {
        foreach (Transform child in weaponSpawnPoint)
        {
            Destroy(child.gameObject);
        }

        foreach (UnlockedWeaponInfo unlockedWeapon in weaponInventory.unlockedWeapons)
        {
            if (unlockedWeapon.level >= 0)
            {
                Instantiate(unlockedWeapon.weaponData.WeaponPrefabs[unlockedWeapon.level], transform).SetActive(true);
            }
        }
    }

    void UpdateWeaponsEditMode()
    {
        if (Application.isEditor && !Application.isPlaying)
        {
            for (int i = this.transform.childCount; i > 0; --i)
                DestroyImmediate(this.transform.GetChild(0).gameObject);
            foreach (UnlockedWeaponInfo unlockedWeapon in weaponInventory.unlockedWeapons)
            {
                if (unlockedWeapon.level >= 0)
                {
                    Instantiate(unlockedWeapon.weaponData.WeaponPrefabs[unlockedWeapon.level], transform)
                        .SetActive(true);
                }
            }
        }
    }

    private void OnValidate()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.delayCall += () => { UpdateWeaponsEditMode(); };
#endif
    }
}