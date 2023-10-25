using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_UpgradeManager : MonoBehaviour
{
    [SerializeField] SO_WeaponInventory weaponInventory;
    [SerializeField] SO_QuarkManager quarkManager;
    [SerializeField] List<TextMeshProUGUI> cardText;
    [SerializeField] private int upgradeCost = 20;
    [SerializeField] private TextMeshProUGUI quarkCounterText;
    private bool isUpgrading;
    private bool isWaitingToUpgrade;
    public S_PauseMenu pauseMenu;

    private S_PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new S_PlayerControls();
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
            }
        };
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
                foreach (TextMeshProUGUI text in cardText)
                {
                    SO_SingleWeaponClass weapon =
                        weaponInventory.allWeapons[UnityEngine.Random.Range(0, weaponInventory.allWeapons.Count)];

                    text.transform.parent.transform.gameObject.SetActive(true);

                    text.text = weapon.weaponName;
                }
            }
            quarkCounterText.text = $"{quarkManager.quarkCount} / {upgradeCost} quarks";

        }

    }

    private void UpgradeLeft()
    {
        weaponInventory.LevelUpWeapon(weaponInventory.GetWeaponByName(cardText[1].text), 1);
        DisableText();
        quarkManager.quarkCount -= upgradeCost;
        upgradeCost += Mathf.Max(1, (int)(upgradeCost * 0.2f));

    }

    private void UpgradeRight()
    {
        weaponInventory.LevelUpWeapon(weaponInventory.GetWeaponByName(cardText[0].text), 1);
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
        foreach (TextMeshProUGUI text in cardText)
        {
            text.transform.parent.transform.gameObject.SetActive(false);
        }

        isUpgrading = false;
        Time.timeScale = 1;
    }
}