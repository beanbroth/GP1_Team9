using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SO_WeaponClassV2 : SO_BaseCardData
{
    public List<GameObject> weaponPrefabs = new List<GameObject>();
    public List<GameObject> WeaponPrefabs
    {
        get
        {
            if (weaponPrefabs.Count == 0)
            {
                Debug.LogError("Card Prefabs not set. Put them in the card scriptable object");
            }

            return weaponPrefabs;
        }
    }
}
