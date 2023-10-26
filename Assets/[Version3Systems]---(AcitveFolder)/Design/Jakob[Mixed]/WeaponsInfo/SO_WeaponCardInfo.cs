using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponCardInfo", menuName = "WeaponSystem/WeaponCardInfo")]
public class SO_WeaponCardInfo : ScriptableObject
{
    public string weaponCardName;
    public SO_WeaponLevelCardInfo[] cardInfoPerLevel;
}
