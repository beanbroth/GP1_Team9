using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class S_UpgradeManager : MonoBehaviour
{
    [SerializeField] public SO_WeaponInventory weaponInventory;
    [SerializeField] S_UpgradeCardManager upgradeCardManager;
    [SerializeField] int upgradeCost = 3;
    [SerializeField] int inititalUpgradeChoices = 3;
    [SerializeField] int deafaultlUpgradeChoices = 2;
    int upgradeChoices = 3;
    [SerializeField] float inputDelayTime = 0.05f;
    private bool isUpgrading;
    private S_PlayerControls playerControls;
    private bool isHoldingLeft = false;
    private bool isHoldingRight = false;
    private bool firstSelectionComplete = false;
    [SerializeField] private float upgradeCostIncrease = 0.2f;

    private void Awake()
    {
        weaponInventory.ResetUnlockedWeapons();
        QuarkManager.upgradeCost = upgradeCost;
        playerControls = new S_PlayerControls();
        playerControls.Enable();
        QuarkManager.OnQuarkCountChanged += QuarkCountChanged;
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void OnDestroy()
    {
        QuarkManager.OnQuarkCountChanged -= QuarkCountChanged;
    }

    private void QuarkCountChanged(int newCount)
    {
        if (newCount >= upgradeCost)
        {
            QuarkManager.quarkCount = 0;
            PauseManager.Pause();
            isUpgrading = true;
            InitCards();
        }
    }

    private bool delayInProgress = false;

    private IEnumerator MoveSelection(int value)
    {
        if (!delayInProgress)
        {
            delayInProgress = true;
            // Add a delay of 0.1f seconds (or any other desired amount)
            yield return new WaitForSecondsRealtime(inputDelayTime);
            upgradeCardManager.MoveSelectedCard(value);
            delayInProgress = false;
        }
    }

    private void Update()
    {
        if (!isUpgrading)
            return;
        if (S_PauseMenu.IsPauseMenuActive)
            return;
        float turnLeftValue = playerControls.Player.TurnLeft.ReadValue<float>();
        float turnRightValue = playerControls.Player.TurnRight.ReadValue<float>();
        bool wasTurnLeftPressed = playerControls.Player.TurnLeft.WasPressedThisFrame();
        bool wasTurnRightPressed = playerControls.Player.TurnRight.WasPressedThisFrame();
        if (turnLeftValue < -0.5f)
        {
            if (wasTurnLeftPressed)
            {
                StartCoroutine(MoveSelection(-1));
            }
        }

        if (turnRightValue > 0.5f)
        {
            if (wasTurnRightPressed)
            {
                StartCoroutine(MoveSelection(1));
            }
        }

        if (turnLeftValue < -0.5f && turnRightValue > 0.5f)
        {
            PerformUpgrade();
        }
    }

    private void PerformUpgrade()
    {
        weaponInventory.LevelUpWeapon(upgradeCardManager.GetSelectedWeapon(), 1);
        upgradeCost += Mathf.Max(1, (int)(upgradeCost * upgradeCostIncrease));
        QuarkManager.upgradeCost = upgradeCost;
        QuarkManager.ResetQuarks();
        isUpgrading = false;
        upgradeCardManager.ClearCards();
        PauseManager.Unpause();
    }

    private void InitCards()
    {
        if (!firstSelectionComplete)
        {
            upgradeChoices = inititalUpgradeChoices;
            firstSelectionComplete = true;
        }
        else
        {
            upgradeChoices = deafaultlUpgradeChoices;
        }
        List<UpgradeCardInfo> upgradeCardInfos = new List<UpgradeCardInfo>();
        List<SO_SingleWeaponClass> tempAvilibleWeapons = new List<SO_SingleWeaponClass>();
        foreach (SO_SingleWeaponClass weaponClass in weaponInventory.availableWeaponClasses)
        {
            if (!weaponInventory.IsWeaponMaxLevel(weaponClass))
            {
                tempAvilibleWeapons.Add(weaponClass);
            }
        }

        for (int i = 0; i < upgradeChoices; i++)
        {
            if (tempAvilibleWeapons.Count == 0)
            {
                break;
            }

            UpgradeCardInfo upgradeCardInfo = new UpgradeCardInfo();
            SO_SingleWeaponClass weaponClass =
                tempAvilibleWeapons[UnityEngine.Random.Range(0, tempAvilibleWeapons.Count)];
            tempAvilibleWeapons.Remove(weaponClass);
            UnlockedWeaponInfo unlockedWeaponInfo = weaponInventory.GetUnlockedWeaponInfoForWeapon(weaponClass);
            upgradeCardInfo.weaponClass = weaponClass;
            upgradeCardInfo.name = weaponClass.weaponName;
            if (unlockedWeaponInfo.currentLevel < 0)
            {
                //Debug.Log("weapon level is less than 0");
                upgradeCardInfo.level = 0;
            }
            else
            {
                upgradeCardInfo.level = unlockedWeaponInfo.currentLevel + 1;
            }

            upgradeCardInfo.description = weaponClass.weaponDescriptions[upgradeCardInfo.level];
            upgradeCardInfo.prefab = weaponClass.weaponPrefabs[upgradeCardInfo.level];
            upgradeCardInfo.image = null;
            upgradeCardInfos.Add(upgradeCardInfo);
        }

        if (upgradeCardInfos.Count == 0)
        {
            Debug.Log("Player has unlocked every possible weapon!");
            isUpgrading = false;
            QuarkManager.upgradeCost = 999999;
            upgradeCost = 999999;
            PauseManager.Unpause();
            return;
        }

        upgradeCardManager.DisplayCards(upgradeCardInfos.ToArray());
    }
}

public struct UpgradeCardInfo
{
    public SO_SingleWeaponClass weaponClass;
    public string name;
    public int level;
    public string description;
    public GameObject prefab;
    public Texture image;
}