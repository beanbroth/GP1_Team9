using System.Collections.Generic;
using UnityEngine;

public class SO_PlayerStats : ScriptableObject
{
    public enum PlayerStatKey
    {
        Health,
        Speed,
    }
    
    public Dictionary<PlayerStatKey, float> playerStats;

    private void OnEnable()
    {
        if (playerStats == null)
            playerStats = new Dictionary<PlayerStatKey, float>();
    }

    public void SetStat(PlayerStatKey key, float value)
    {
        playerStats[key] = value;
    }

    public float GetStat(PlayerStatKey key)
    {
        if (playerStats.ContainsKey(key))
            return playerStats[key];
        else
            return 0f; // Or return a default value...
    }
}
.
