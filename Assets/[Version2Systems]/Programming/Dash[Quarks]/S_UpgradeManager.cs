using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class S_UpgradeManager : MonoBehaviour
{
    [SerializeField] SO_WeaponInventory weaponInventory;
    [SerializeField] SO_QuarkManager quarkManager;
    [SerializeField] private int upgradeCost = 20;
    [SerializeField] private TextMeshProUGUI quarkCounterText;
    private bool isUpgrading;
    private bool isWaitingToUpgrade;
    public S_PauseMenu pauseMenu;

    private S_PlayerControls playerControls;
    [Header("UI References")]
    [SerializeField] GameObject upgradeUIObject;
    Transform[] _cards;
    SO_SingleWeaponClass[] _weapons = new SO_SingleWeaponClass[2];

    [SerializeField] AudioClip cardSelectSound;
    AudioSource audioSource;

    private void Awake()
    {
        playerControls = new S_PlayerControls();
        audioSource = GetComponent<AudioSource>();
        if(pauseMenu == null)
            pauseMenu = FindFirstObjectByType<S_PauseMenu>();
        playerControls.Player.Turn.performed += context =>
        {
            if (isUpgrading && !pauseMenu.GetIsPaused())
            {
                float turnDirection = context.ReadValue<float>();
                if (turnDirection < 0)
                {
                    UpgradeLeft();
                }
                else if (turnDirection > 0)
                {
                    UpgradeRight();
                }
                audioSource.PlayOneShot(cardSelectSound);
            }
        };
        Transform _rightCard = upgradeUIObject.transform.Find("UpgradeCards/RightCard");
        Transform _leftCard = upgradeUIObject.transform.Find("UpgradeCards/LeftCard");
        _cards = new Transform[] { _leftCard, _rightCard};
        upgradeUIObject.SetActive(false);
        quarkManager.ResetQuarks();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        if (!isUpgrading)
        {
            if (quarkManager.quarkCount >= upgradeCost && !isWaitingToUpgrade)
            {
                StartCoroutine(AllowUpgradeAfterDelay(1f));
                Time.timeScale = 0;
                int idx = 0;
                foreach (Transform _card in _cards)
                {
                    SO_SingleWeaponClass weapon =
                        weaponInventory.allWeapons[UnityEngine.Random.Range(0, weaponInventory.allWeapons.Count)];

                    //Update card UI
                    _weapons[idx] = weapon;
                    _card.Find("WeaponName").GetComponent<TextMeshProUGUI>().text = weapon.weaponCardInfo.weaponCardName;
                    int currentWeaponLevel = -1;
                    if (weaponInventory.unlockedWeapons != null && weaponInventory.IsWeaponUnlocked(weapon))
                    {
                        currentWeaponLevel = weaponInventory.GetUnlockedWeaponInfoForWeapon(weapon).level;
                    }
                    currentWeaponLevel++;
                    try
                    {
                        _card.Find("Level").GetComponent<TextMeshProUGUI>().text = "LVL " + weapon.weaponCardInfo.cardInfoPerLevel[currentWeaponLevel].level.ToString();
                        _card.Find("Description").GetComponent<TextMeshProUGUI>().text = weapon.weaponCardInfo.cardInfoPerLevel[currentWeaponLevel].description;
                        _card.Find("Icon").GetComponent<Image>().sprite = weapon.weaponCardInfo.cardInfoPerLevel[currentWeaponLevel].image;
                    }
                    catch
                    {
                        print("Couldn't find weapon info for weapon: " + weapon.weaponName);
                    }
                    idx++;
                }
                upgradeUIObject.SetActive(true);
            }
            quarkCounterText.text = $"{quarkManager.quarkCount} / {upgradeCost} quarks";

        }

    }

    private void UpgradeLeft()
    {
        weaponInventory.LevelUpWeapon(weaponInventory.GetWeaponByName(_weapons[0].weaponName), 1);
        DisableText();
        quarkManager.quarkCount -= upgradeCost;
        upgradeCost += Mathf.Max(1, (int)(upgradeCost * 0.2f));

    }

    private void UpgradeRight()
    {
        weaponInventory.LevelUpWeapon(weaponInventory.GetWeaponByName(_weapons[1].weaponName), 1);
        DisableText();
        quarkManager.quarkCount -= upgradeCost;
        upgradeCost += Mathf.Max(1, (int)(upgradeCost * 0.2f));
    }

    private IEnumerator AllowUpgradeAfterDelay(float delay)
    {
        isWaitingToUpgrade = true;
        yield return new WaitForSecondsRealtime(delay);
        isUpgrading = true;
        isWaitingToUpgrade = false;

    }


    private void DisableText()
    {
        upgradeUIObject.SetActive(false);
        isUpgrading = false;
        Time.timeScale = 1;
    }
}