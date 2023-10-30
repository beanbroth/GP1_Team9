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

    private UpgradeCardInfo cardInfo;

    public UpgradeCardInfo GetCardInfo() { return cardInfo; }

    public void SetCardInfo(UpgradeCardInfo cardInfo)
    {
        this.cardInfo = cardInfo;
        nameField.text = cardInfo.name;
        descriptionField.text = cardInfo.description;
        lvlField.text = "lvl " + cardInfo.level.ToString();

        if (cardInfo.image != null)
        {
            preview.material.mainTexture = cardInfo.image;
        }
        if(cardInfo.prefab != null)
        {
            //spawn prefab in front of camera or something
        }

    }
}