using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class OpponentHand : MonoBehaviour
{
    private const float GAP_BETWEEN_CARDS = 0.2f;

    [SerializeField] private int numberOfHiddenCards = 1;
    
    public List<Card> handOfCards;
    
    public void RenderCards(List<Card> cards)
    {
        handOfCards = cards;
        
        List<Card> shownCards = GetInitialShownCards();

        foreach (Card card in shownCards)
        {
            //Debug.Log(card);
            card.transform.rotation = Quaternion.Euler(90, 0, 0);
        }
        
        float cardWidth = handOfCards.First().transform.localScale.x;
        float initialTransform = (handOfCards.Count * cardWidth + (handOfCards.Count - 1) * GAP_BETWEEN_CARDS)/2;
        
        for (int i = 0; i < handOfCards.Count; i++)
        {
            Card card = handOfCards[i];
            card.transform.SetParent(transform);
            // card.transform.localPosition = new Vector3(initialTransform - (cardWidth + GAP_BETWEEN_CARDS) * i, 0, 0);
            card.AnimOnMove(new Vector3(initialTransform - (cardWidth + GAP_BETWEEN_CARDS) * i, 0, 0), .25f * i);
        }
    }

    public void RevealCard()
    {
        foreach (Card card in GetInitialHiddenCards())
        {
            card.AnimOnRotate(Quaternion.Euler(90, 0, 0), 0f);
        }
    }

    public List<Card> GetWildCards(string wildCardSuite)
    {
        return handOfCards
            .Where(card => card.cardSuit == wildCardSuite)
            .ToList();
    }

    public List<Card> GetInitialShownCards()
    {
        return handOfCards.Skip(numberOfHiddenCards).ToList();
    }

    public List<Card> GetInitialHiddenCards()
    {
        return handOfCards.Take(numberOfHiddenCards).ToList();
    }
}
