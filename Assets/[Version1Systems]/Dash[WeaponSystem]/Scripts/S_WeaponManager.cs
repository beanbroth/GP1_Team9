using UnityEngine;

#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public class S_WeaponManager : MonoBehaviour
{
    [SerializeField] SO_WeaponInventory weaponInventory;
    [SerializeField] Transform weaponSpawnPoint;


    private void Awake()
    {
        // weaponInventory.ResetUnlockedWeapons();
    }
    void OnEnable()
    {
        SO_WeaponInventory.OnWeaponInfoChange += UpdateWeapons;
        PauseManager.OnPauseStateChange += OnPauseChange;
    }

    private void OnDisable()
    {
        SO_WeaponInventory.OnWeaponInfoChange -= UpdateWeapons;
        PauseManager.OnPauseStateChange -= OnPauseChange;
    }

    void OnPauseChange(bool gamePaused)
    {
        if (gamePaused)
        {
            foreach (Transform child in weaponSpawnPoint)
            {
                Destroy(child.gameObject);
            }
        }
        else
        {
            UpdateWeapons();

        }
    }

    private void Start()
    {
        UpdateWeapons();
    }

    private void OnDestroy()
    {
        SO_WeaponInventory.OnWeaponInfoChange -= UpdateWeapons;
    }

    void UpdateWeapons()
    {
        if (Application.isEditor && !Application.isPlaying)
        {
            UpdateWeaponsWithDelay();
        }
        else
        {
            foreach (Transform child in weaponSpawnPoint)
            {
                Destroy(child.gameObject);
            }

            foreach (UnlockedWeaponInfo unlockedWeapon in weaponInventory.unlockedWeapons)
            {
                if (unlockedWeapon.currentLevel >= 0)
                {
                    Instantiate(unlockedWeapon.weaponData.WeaponPrefabs[unlockedWeapon.currentLevel], gameObject.transform)
                        .SetActive(true);
                }
            }
        }
    }

    void UpdateWeaponsEditMode()
    {
        if (Application.isEditor && !Application.isPlaying && this.transform.childCount != 0)
        {
            for (int i = this.transform.childCount; i > 0; --i)
                DestroyImmediate(this.transform.GetChild(0).gameObject);
            foreach (UnlockedWeaponInfo unlockedWeapon in weaponInventory.unlockedWeapons)
            {
                if (unlockedWeapon.weaponData != null && unlockedWeapon.currentLevel >= 0)
                {
                    Instantiate(unlockedWeapon.weaponData.WeaponPrefabs[unlockedWeapon.currentLevel], transform)
                        .SetActive(true);
                }
            }
        }
    }

    private void OnValidate()
    {
        UpdateWeapons();
    }

    private void UpdateWeaponsWithDelay()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.delayCall += () => { UpdateWeaponsEditMode(); };
#endif
    }
}