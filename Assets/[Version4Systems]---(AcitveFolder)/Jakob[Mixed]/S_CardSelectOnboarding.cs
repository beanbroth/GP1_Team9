using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CardSelectOnboarding : MonoBehaviour
{
    [SerializeField] GameObject cardUI;
    [SerializeField] int timesToShow = 1;
    int timesShown = 0;
    private void OnEnable()
    {
        S_UpgradeCardManager.OpenUpgrade += EnableUI;
        S_UpgradeCardManager.CloseUpgrade += DisableUI;
    }

    private void OnDisable()
    {
        S_UpgradeCardManager.OpenUpgrade -= EnableUI;
        S_UpgradeCardManager.CloseUpgrade -= DisableUI;
    }

    void EnableUI()
    {
        timesShown++;
        if(timesToShow==0 || timesShown <= timesToShow)
            cardUI.SetActive(true);
    }
    void DisableUI()
    {
        cardUI.SetActive(false);
    }
}
