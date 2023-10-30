using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardInventory", menuName = "CardSystem/CardInventory", order = 0)]
public class SO_CardInventory : ScriptableObject
{
    [SerializeField] private bool resetInventoryOnEnable = false;
    [Header("Card Info")] [SerializeField] public List<UnlockedCardInfo> unlockedCards;
    [SerializeField] public List<SO_BaseCardData> availableCardClasses;
    [SerializeField] public List<SO_BaseCardData> cardClassDatabase;

    //event for card info change
    public delegate void CardInfoChange();
    public static event CardInfoChange OnCardInfoChange;

    private void OnEnable()
    {
        if (resetInventoryOnEnable)
        {
            ResetUnlockedCards();
        }
    }

    public void ResetUnlockedCards()
    {
        Debug.Log("resetting card inventory");
        unlockedCards = new List<UnlockedCardInfo>();
        availableCardClasses.Clear();
        foreach (SO_BaseCardData card in cardClassDatabase)
        {
            availableCardClasses.Add(card);
        }
    }

    private void Awake() 
    {
        if (resetInventoryOnEnable)
        {
            ResetUnlockedCards();
        }
    }

    public void LevelUpCard(SO_BaseCardData card, int levelIncrease)
    {
        if (levelIncrease <= 0)
        {
            return;
        }

        int cardIndex = unlockedCards.FindIndex(x => x.cardData == card);
        if (cardIndex != -1)
        {
            UnlockedCardInfo cardInfo = unlockedCards[cardIndex];
            cardInfo.currentLevel += levelIncrease;
            unlockedCards[cardIndex] = cardInfo; 
            if (cardInfo.currentLevel >= cardInfo.maxLevel - 1)
            {
                availableCardClasses.Remove(card);
            }
        }
        else
        {
            AddCard(card.cardName);
            cardIndex = unlockedCards.FindIndex(x => x.cardData == card);
            UnlockedCardInfo cardInfo = unlockedCards[cardIndex];
            cardInfo.currentLevel += levelIncrease - 1;
            unlockedCards[cardIndex] = cardInfo;
        }

        ValidateCardLevels();
        OnCardInfoChange?.Invoke();
    }

    public UnlockedCardInfo GetUnlockedCardInfoForCard(SO_BaseCardData card)
    {
        foreach (UnlockedCardInfo unlockedCardInfo in unlockedCards)
        {
            if (unlockedCardInfo.cardData == card)
            {
                return unlockedCardInfo;
            }
        }

        UnlockedCardInfo newCard = new UnlockedCardInfo();
        newCard.cardData = card;
        newCard.currentLevel = -1;
        newCard.maxLevel = card.CardPrefabs.Count;
        return newCard;
    }

    public SO_BaseCardData GetCardByName(string cardName)
    {
        return cardClassDatabase.Find(x => x.cardName == cardName);
    }

    public bool IsCardUnlocked(SO_BaseCardData card)
    {
        return unlockedCards.Exists(x => x.cardData == card);
    }

    public bool IsCardMaxLevel(SO_BaseCardData cardClass)
    {
        if (GetUnlockedCardInfoForCard(cardClass).currentLevel >=
            GetUnlockedCardInfoForCard(cardClass).maxLevel - 1)
        {
            return true;
        }

        return false;
    }

    public void AddCard(string cardName)
    {
        SO_BaseCardData card = GetCardByName(cardName);
        if (card == null)
        {
            Debug.LogError("Card not found");
            return;
        }

        UnlockedCardInfo newCard = new UnlockedCardInfo();
        newCard.cardData = card;
        newCard.currentLevel = 0;
        newCard.maxLevel = card.CardPrefabs.Count;
        unlockedCards.Add(newCard);
        OnCardInfoChange?.Invoke();
    }

    private void OnValidate()
    {
        ValidateCardLevels();
        OnCardInfoChange?.Invoke();
    }

    private void ValidateCardLevels()
    {
        for (int i = 0; i < unlockedCards.Count; i++)
        {
            UnlockedCardInfo card = unlockedCards[i];
            if (card.cardData == null)
            {
                return;
            }

            card.maxLevel = card.cardData.CardPrefabs.Count;
            if (card.currentLevel > card.maxLevel - 1)
            {
                if (Application.isPlaying)
                    Debug.Log("Card level is greater than max level");
                card.currentLevel = card.maxLevel - 1;
            }

            if (card.currentLevel < 0)
            {
                if (Application.isPlaying)
                    Debug.Log("Card level is less than 0");
                card.currentLevel = 0;
            }

            unlockedCards[i] = card;
        }
    }
}

[System.Serializable]
public struct UnlockedCardInfo
{
    public SO_BaseCardData cardData;
    public int currentLevel;
    public int maxLevel;
}