using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_UpgradeCardManager: MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    private List<S_CardMovementController> cards = new List<S_CardMovementController>();
    [SerializeField] int testNumCards = 3;
    [SerializeField] float totalWidth = 20f;
    private int cardIndex = 0;

    private void SetSelectedCard(int ci)
    {
        cardIndex = Mathf.Clamp(ci, 0, cards.Count-1);
        for (int i = 0; i < cards.Count; i++)
        {
            if (i == cardIndex)
            {
                cards[i].SetSelected(true);
            }
            else
            {
                cards[i].SetSelected(false);
            }
        }
    }

    public void MoveSelectedCard(int moveDirection)
    {
        SetSelectedCard(cardIndex + moveDirection);
    }

    public SO_SingleWeaponClass GetSelectedWeapon()
    {
        return cards[cardIndex].GetComponent<S_CardInfoController>().GetCardInfo().weaponClass;
    }

    public void DisplayCards()
    {
        DisplayCardsEditor(testNumCards, totalWidth);
    }


    public void DisplayCards(UpgradeCardInfo[] cardInfos)
    {
        float numberOfCards = cardInfos.Length;

        float cardSpacing = totalWidth / (numberOfCards + 1);
        float cardWidth = totalWidth / numberOfCards;


        for (int i = 0; i < numberOfCards; i++)
        {
            float positionX = ((i + 1) * cardSpacing) - (totalWidth / 2);
            Vector3 cardPosition = new Vector3(positionX, 0, 0);
            GameObject card = Instantiate(cardPrefab, transform);
            card.GetComponent<S_CardInfoController>().SetCardInfo(cardInfos[i]);
            card.transform.localPosition = cardPosition;
            cards.Add(card.GetComponent<S_CardMovementController>());
        }

    }


    public void DisplayCardsEditor(int numberOfCards, float totalWidth)
    {
        ClearCards();

        float cardSpacing = totalWidth / (numberOfCards + 1);
        float cardWidth = totalWidth / numberOfCards;

        for (int i = 0; i < numberOfCards; i++)
        {
            float positionX = ((i + 1) * cardSpacing) - (totalWidth / 2);
            Vector3 cardPosition = new Vector3(positionX, 0, 0);
            GameObject card = Instantiate(cardPrefab, transform);
            card.transform.localPosition = cardPosition;
        }
    }

    public void ClearCards()
    {
      
        cards.Clear();
        for(int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

    }
}