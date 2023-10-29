using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuarkManager", menuName = "Quarks/QuarkManager", order = 0)]
public class QuarkManager : ScriptableObject
{
    //onquark count changed
    public static Action<int> OnQuarkCountChanged;
    public static Action UpgradeCostMet;

    public static int quarkCount;
    public static int upgradeCost;

    public static int QuarkCount
    {
        get => quarkCount;
        set { quarkCount = value; OnQuarkCountChanged?.Invoke(quarkCount); }

    }

    private void Awake()
    {
        ResetQuarks();
        ResetUpgradeCost();
    }


    private void OnEnable()
    {
        ResetQuarks();
        ResetUpgradeCost();
    }

    private void ResetUpgradeCost()
    {
        upgradeCost = 0;
    }

    public static void ResetQuarks()
    {
        quarkCount = 0;
        OnQuarkCountChanged?.Invoke(quarkCount);
    }

    public static void AddQuarks(int amount)
    {
        quarkCount += amount;
        OnQuarkCountChanged?.Invoke(quarkCount);

        if (quarkCount >= upgradeCost)
        {
            UpgradeCostMet?.Invoke();
        }

    }
}