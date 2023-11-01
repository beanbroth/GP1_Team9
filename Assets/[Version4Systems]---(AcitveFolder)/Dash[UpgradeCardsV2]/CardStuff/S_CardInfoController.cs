using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_CardInfoController : MonoBehaviour
{
    [SerializeField] TextMeshPro nameField;
    [SerializeField] TextMeshPro descriptionField;
    [SerializeField] TextMeshPro lvlField;
    [SerializeField] Renderer preview;
    private Texture texture;
    private UpgradeCardInfo cardInfo;

    public UpgradeCardInfo GetCardInfo()
    {
        return cardInfo;
    }

    public void SetCardInfo(UpgradeCardInfo cardInfo)
    {
        this.cardInfo = cardInfo;
        preview.material.mainTexture = cardInfo.image;
        nameField.text = cardInfo.name;
        descriptionField.text = cardInfo.description;
        lvlField.text = "lvl " + cardInfo.level.ToString();
        if (FindFirstObjectByType<S_UpgradeManager>().weaponInventory.IsWeaponOneBeforeMaxLevel(cardInfo.weaponClass))
        {
            lvlField.text = "MAX";
        }

        if (cardInfo.image != null)
        {
            preview.material.mainTexture = cardInfo.image;
        }
    }
}