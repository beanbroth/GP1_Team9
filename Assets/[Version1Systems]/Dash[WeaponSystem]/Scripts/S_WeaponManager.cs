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
        weaponInventory.ResetUnlockedWeapons();
    }
    void OnEnable()
    {
        SO_WeaponInventory.OnWeaponInfoChange += UpdateWeapons;
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
                if (unlockedWeapon.level >= 0)
                {
                    Instantiate(unlockedWeapon.weaponData.WeaponPrefabs[unlockedWeapon.level], transform)
                        .SetActive(true);
                }
            }
        }
    }

    private void Update()
    {
    }

    void UpdateWeaponsEditMode()
    {
        if (Application.isEditor && !Application.isPlaying &&  this.transform.childCount != 0)
        {
            for (int i = this.transform.childCount; i > 0; --i)
                DestroyImmediate(this.transform.GetChild(0).gameObject);
            foreach (UnlockedWeaponInfo unlockedWeapon in weaponInventory.unlockedWeapons)
            {
                if (unlockedWeapon.weaponData != null && unlockedWeapon.level >= 0)
                {
                    Instantiate(unlockedWeapon.weaponData.WeaponPrefabs[unlockedWeapon.level], transform)
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