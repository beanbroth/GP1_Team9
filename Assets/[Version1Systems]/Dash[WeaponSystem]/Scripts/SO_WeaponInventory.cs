using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "WeaponInventory", menuName = "WeaponSystem/WeaponInventory", order = 0)]
public class SO_WeaponInventory : ScriptableObject
{
    [SerializeField] bool resetInventoryOnEnable = false;
    [Header("Weapon Info")]
    [SerializeField] public List<UnlockedWeaponInfo> unlockedWeapons;
    [SerializeField] public List<SO_SingleWeaponClass> allWeapons;
    
    private void OnEnable()
    {
        if (resetInventoryOnEnable)
        {
            unlockedWeapons = new List<UnlockedWeaponInfo>();
        }
    }

    //event for weapon info change
    public delegate void WeaponInfoChange();

    public static event WeaponInfoChange OnWeaponInfoChange;

    public void LevelUpWeapon(SO_SingleWeaponClass weapon, int levelIncrease)
    {
        if (levelIncrease <= 0)
            Debug.LogError("Level increase must greater than 0");
        if (IsWeaponUnlocked(weapon))
        {
            UnlockedWeaponInfo weaponInfo = unlockedWeapons.Find(x => x.weaponData == weapon);
            weaponInfo.level += levelIncrease;
        }
        else
        {
            AddWeapon(weapon.weaponName);
            UnlockedWeaponInfo weaponInfo = unlockedWeapons.Find(x => x.weaponData == weapon);
            weaponInfo.level += levelIncrease - 1;
        }

        ValidateWeaponLevels();
        OnWeaponInfoChange?.Invoke();
    }

    SO_SingleWeaponClass GetWeaponByName(string weaponName)
    {
        return allWeapons.Find(x => x.weaponName == weaponName);
    }

    public bool IsWeaponUnlocked(SO_SingleWeaponClass weapon)
    {
        return unlockedWeapons.Exists(x => x.weaponData == weapon);
    }

    public void AddWeapon(string weaponName)
    {
        SO_SingleWeaponClass weapon = GetWeaponByName(weaponName);
        if (weapon == null)
        {
            Debug.LogError("Weapon not found");
            return;
        }

        UnlockedWeaponInfo newWeapon = new UnlockedWeaponInfo();
        newWeapon.weaponData = weapon;
        newWeapon.level = 0;
        newWeapon.maxLevel = weapon.WeaponPrefabs.Count;
        unlockedWeapons.Add(newWeapon);
        OnWeaponInfoChange?.Invoke();
    }

    private void OnValidate()
    {
        ValidateWeaponLevels();
        OnWeaponInfoChange?.Invoke();
    }

    private void ValidateWeaponLevels()
    {
        for (int i = 0; i < unlockedWeapons.Count; i++)
        {
            UnlockedWeaponInfo weapon = unlockedWeapons[i];
            weapon.maxLevel = weapon.weaponData.WeaponPrefabs.Count;
            if (weapon.level > weapon.maxLevel)
            {
                if (Application.isPlaying)
                    Debug.Log("Weapon level is greater than max level");
                weapon.level = weapon.maxLevel;
            }

            if (weapon.level < 0)
            {
                if (Application.isPlaying)
                    Debug.Log("Weapon level is less than 0");
                weapon.level = 0;
            }

            unlockedWeapons[i] = weapon;
        }
    }
}

[System.Serializable]
public struct UnlockedWeaponInfo
{
    public SO_SingleWeaponClass weaponData;
    public int level;
    public int maxLevel;
}