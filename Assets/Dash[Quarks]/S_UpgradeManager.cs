using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class S_UpgradeManager : MonoBehaviour
{
    [SerializeField] SO_WeaponInventory weaponInventory;
    [SerializeField] SO_QuarkManager quarkManager;
    [SerializeField] List<TextMeshProUGUI> cardText;
    [SerializeField] private int upgradeCost = 20;
    private bool isUpgrading;

    private void Update()
    {
        if (quarkManager.quarkCount >= upgradeCost && !isUpgrading)
        {
            isUpgrading = true;
            Time.timeScale = 0;
            foreach (TextMeshProUGUI text in cardText)
            {
                SO_SingleWeaponClass weapon =
                    weaponInventory.allWeapons[UnityEngine.Random.Range(0, weaponInventory.allWeapons.Count)];
               
                
                text.transform.parent.transform.gameObject.SetActive(true);
                
                text.text = weapon.weaponName;
            }
        }

        if (isUpgrading)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                weaponInventory.LevelUpWeapon(weaponInventory.GetWeaponByName(cardText[1].text), 1);
                DisableText();
                quarkManager.quarkCount -= upgradeCost;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                weaponInventory.LevelUpWeapon(weaponInventory.GetWeaponByName(cardText[0].text), 1);
                DisableText();
                quarkManager.quarkCount -= upgradeCost;
            }
            
            
            
        }
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