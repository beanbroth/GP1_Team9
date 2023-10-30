using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatUpgradeClass", menuName = "PlayerCards/StatUpgradeClass", order = 0)]
public class SO_StatUpgradeClass : SO_BaseCardData
{
    [SerializeField] List<
    [SerializeField] List<Texture> cardTextures = new List<Texture>();
    
    public List<Texture> CardTextures
    {
        get
        {
            if (cardTextures.Count == 0)
            {
                Debug.LogError("Card texture not set. Put them in the card scriptable object");
            }

            return cardTextures;
        }
    }
}

struct StatUpgradeInfo
{
    public  statReferance;
    public float statMultiplier;
    public float statAddition;
    
    
}

struct Stat
{
    
}


