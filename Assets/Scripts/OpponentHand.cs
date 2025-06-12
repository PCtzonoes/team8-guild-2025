using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class OpponentHand : MonoBehaviour
{
    private const float GAP_BETWEEN_CARDS = 0.2f;

    [SerializeField] private int numberOfHiddenCards = 1;
    
    public Card[] handOfCards;
    
    public void RenderCards(Card[] cards)
    {
        handOfCards = cards;
        float cardWidth = handOfCards.First().transform.localScale.x;
        float initialTransform = (handOfCards.Length * cardWidth + (handOfCards.Length - 1) * GAP_BETWEEN_CARDS)/2;
        
        for (int i = 0; i < handOfCards.Length; i++)
        {
            Card card = handOfCards[i];
            card.transform.SetParent(transform);
            card.transform.localPosition = new Vector3(initialTransform - (cardWidth + GAP_BETWEEN_CARDS) * i, 0, 0);
        }

        Card[] hiddenCards = GetInitialHiddenCards();

        foreach (Card card in hiddenCards)
        {
            card.transform.rotation = Quaternion.Euler(-90, 0, 0);
        }
            
    }

    public void RevealCard()
    {
        foreach (Card card in GetInitialHiddenCards())
        {
            card.transform.rotation = Quaternion.Euler(90, 0, 0);
        }
    }

    public Card[] GetOpponentWildCards(string wildCardSuite)
    {
        return handOfCards
            .Where(card => card.cardSuit == wildCardSuite)
            .ToArray();
    }

    public Card[] GetInitialShownCards()
    {
        return handOfCards.Skip(numberOfHiddenCards).ToArray();
    }

    public Card[] GetInitialHiddenCards()
    {
        return handOfCards.Take(numberOfHiddenCards).ToArray();
    }
}
