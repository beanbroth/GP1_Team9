using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuarkManager", menuName = "Quarks/QuarkManager", order = 0)]
public class SO_QuarkManager : ScriptableObject
{
    public int quarkCount;

    private void Awake()
    {
        quarkCount = 0;
    }
    public void ResetQuarks()
    {
        quarkCount = 0;
    }

    public int QuarkCount
    {
        get => quarkCount;
        set => quarkCount = value;
    }

    private void OnEnable()
    {
        quarkCount = 0;
    }

    public void AddQuarks(int amount)
    {
        quarkCount += amount;
    }
}