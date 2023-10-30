

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
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
            ResetUnlockedWeapons();
        }
    }
    public void ResetUnlockedWeapons()
    {
        unlockedWeapons = new List<UnlockedWeaponInfo>();
    }
    private void Awake() //Also reset from the S_GameSceneReset script
    {
        if (resetInventoryOnEnable)
        {
            ResetUnlockedWeapons();
        }
    }

    //event for weapon info change
    public delegate void WeaponInfoChange();

    public static event WeaponInfoChange OnWeaponInfoChange;

    public void LevelUpWeapon(SO_SingleWeaponClass weapon, int levelIncrease)
    {
        if (levelIncrease <= 0)
        {
            //Debug.LogError("Level increase must be greater than 0");
            return;
        }

        //Debug.Log($"Attempting to level up weapon: {weapon.weaponName} by {levelIncrease} levels.");

        int weaponIndex = unlockedWeapons.FindIndex(x => x.weaponData == weapon);

        if (weaponIndex != -1)
        {
            UnlockedWeaponInfo weaponInfo = unlockedWeapons[weaponIndex];
            //Debug.Log($"Weapon level: {weaponInfo.level}");
            weaponInfo.level += levelIncrease;
            //Debug.Log($"Leveled up weapon: {weapon.weaponName} to level {weaponInfo.level}");
            unlockedWeapons[weaponIndex] = weaponInfo; // Update the weapon info in the list
        }
        else
        {
            AddWeapon(weapon.weaponName);
            weaponIndex = unlockedWeapons.FindIndex(x => x.weaponData == weapon);
            UnlockedWeaponInfo weaponInfo = unlockedWeapons[weaponIndex];
            weaponInfo.level += levelIncrease - 1;
            //Debug.Log($"Added and leveled up weapon: {weapon.weaponName} to level {weaponInfo.level}");
            unlockedWeapons[weaponIndex] = weaponInfo; // Update the weapon info in the list
        }

        ValidateWeaponLevels();
        OnWeaponInfoChange?.Invoke();
    }

    public UnlockedWeaponInfo GetUnlockedWeaponInfoForWeapon(SO_SingleWeaponClass weapon)
    {
        foreach(UnlockedWeaponInfo unlockedWeaponInfo in unlockedWeapons)
        {
            if(unlockedWeaponInfo.weaponData == weapon)
            {
                return unlockedWeaponInfo;
            }
        }
        return new UnlockedWeaponInfo();
    }

    public SO_SingleWeaponClass GetWeaponByName(string weaponName)
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
            if (weapon.weaponData == null)
            {
                return;
            }
            weapon.maxLevel = weapon.weaponData.WeaponPrefabs.Count;
            if (weapon.level > weapon.maxLevel-1)
            {
                if (Application.isPlaying)
                    Debug.Log("Weapon level is greater than max level");
                weapon.level = weapon.maxLevel-1;
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