using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GameSceneReset : MonoBehaviour
{
    public static void ResetScene()
    {
        ObjectPoolManager.ClearPools();
    }

    private void Awake()
    {
        ResetScene();
        //FindFirstObjectByType<SO_QuarkManager>().ResetQuarks();
        FindFirstObjectByType<S_UpgradeManager>().GetWeaponInventory().ResetUnlockedWeapons();
        Time.timeScale = 1.0f;
    }
}
