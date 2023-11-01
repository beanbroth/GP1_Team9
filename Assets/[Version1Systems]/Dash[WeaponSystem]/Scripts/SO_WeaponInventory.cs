using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "WeaponInventory", menuName = "WeaponSystem/WeaponInventory", order = 0)]
public class SO_WeaponInventory : ScriptableObject
{
    [SerializeField] private bool resetInventoryOnEnable = false;

    [Header("Weapon Info")] [SerializeField]
    public List<UnlockedWeaponInfo> unlockedWeapons;

    [SerializeField] public List<SO_SingleWeaponClass> initialAvailableWeaponClasses;
    [SerializeField] public List<SO_SingleWeaponClass> availableWeaponClasses;
    [SerializeField] public List<SO_SingleWeaponClass> weaponClassDatabase;

    private void OnEnable()
    {
        availableWeaponClasses.Clear();
        foreach (SO_SingleWeaponClass weaponClass in initialAvailableWeaponClasses)
        {
            availableWeaponClasses.Add(weaponClass);
        }
        
        if (resetInventoryOnEnable)
        {
            ResetUnlockedWeapons();
        }
    }
    
    private void Awake() //Also reset from the S_GameSceneReset script
    {
        availableWeaponClasses.Clear();
        foreach (SO_SingleWeaponClass weaponClass in initialAvailableWeaponClasses)
        {
            availableWeaponClasses.Add(weaponClass);
        }

        if (resetInventoryOnEnable)
        {
            ResetUnlockedWeapons();
        }
    }

    private void FillAvailableWeaponList()
    {
        availableWeaponClasses.Clear();
        foreach (SO_SingleWeaponClass weaponClass in weaponClassDatabase)
        {
            if (!IsWeaponMaxLevel(weaponClass))
            {
                availableWeaponClasses.Add(weaponClass);
            }
        }
    }

    public void ResetUnlockedWeapons()
    {
        Debug.Log("resetting weapon inventory");
        unlockedWeapons.Clear();
    }



    //event for weapon info change
    public delegate void WeaponInfoChange();

    public static event WeaponInfoChange OnWeaponInfoChange;

    public void LevelUpWeapon(SO_SingleWeaponClass weapon, int levelIncrease)
    {
        if (levelIncrease <= 0)
        {
            return;
        }

        int weaponIndex = unlockedWeapons.FindIndex(x => x.weaponData == weapon);
        FillAvailableWeaponList();
        if (weaponIndex != -1)
        {
            UnlockedWeaponInfo weaponInfo = unlockedWeapons[weaponIndex];
            weaponInfo.currentLevel += levelIncrease;
            unlockedWeapons[weaponIndex] = weaponInfo; // Update the weapon info in the list
            if (weaponInfo.currentLevel >= weaponInfo.maxLevel)
            {
                // Remove the weapon from avalibleWeapons list
                availableWeaponClasses.Remove(weapon);
            }
        }
        else
        {
            AddWeapon(weapon.weaponName);
            weaponIndex = unlockedWeapons.FindIndex(x => x.weaponData == weapon);
            UnlockedWeaponInfo weaponInfo = unlockedWeapons[weaponIndex];
            weaponInfo.currentLevel += levelIncrease - 1;
            //Debug.Log($"Added and leveled up weapon: {weapon.weaponName} to level {weaponInfo.level}");
            unlockedWeapons[weaponIndex] = weaponInfo; // Update the weapon info in the list
        }

        // Check if the weapon has been upgraded to max level
        ValidateWeaponLevels();
        OnWeaponInfoChange?.Invoke();
    }

    public UnlockedWeaponInfo GetUnlockedWeaponInfoForWeapon(SO_SingleWeaponClass weapon)
    {
        foreach (UnlockedWeaponInfo unlockedWeaponInfo in unlockedWeapons)
        {
            if (unlockedWeaponInfo.weaponData == weapon)
            {
                return unlockedWeaponInfo;
            }
        }

        UnlockedWeaponInfo newWeapon = new UnlockedWeaponInfo();
        newWeapon.weaponData = weapon;
        newWeapon.currentLevel = -1;
        newWeapon.maxLevel = weapon.WeaponPrefabs.Count;
        return newWeapon;
    }

    public SO_SingleWeaponClass GetWeaponByName(string weaponName)
    {
        return weaponClassDatabase.Find(x => x.weaponName == weaponName);
    }

    public bool IsWeaponUnlocked(SO_SingleWeaponClass weapon)
    {
        return unlockedWeapons.Exists(x => x.weaponData == weapon);
    }

    public bool IsWeaponMaxLevel(SO_SingleWeaponClass weaponClass)
    {
        if (GetUnlockedWeaponInfoForWeapon(weaponClass).currentLevel >=
            GetUnlockedWeaponInfoForWeapon(weaponClass).maxLevel - 1)
        {
            return true;
        }

        return false;
    }
    
    public bool IsWeaponOneBeforeMaxLevel(SO_SingleWeaponClass weaponClass)
    {
        if (GetUnlockedWeaponInfoForWeapon(weaponClass).currentLevel >=
            GetUnlockedWeaponInfoForWeapon(weaponClass).maxLevel - 2)
        {
            return true;
        }

        return false;
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
        newWeapon.currentLevel = 0;
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
            if (weapon.currentLevel > weapon.maxLevel - 1)
            {
                if (Application.isPlaying)
                    Debug.Log("Weapon level is greater than max level");
                weapon.currentLevel = weapon.maxLevel - 1;
            }

            if (weapon.currentLevel < 0)
            {
                if (Application.isPlaying)
                    Debug.Log("Weapon level is less than 0");
                weapon.currentLevel = 0;
            }

            unlockedWeapons[i] = weapon;
        }
    }
}

[System.Serializable]
public struct UnlockedWeaponInfo
{
    public SO_SingleWeaponClass weaponData;
    public int currentLevel;
    public int maxLevel;
}