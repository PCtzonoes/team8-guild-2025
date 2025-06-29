using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class OpponentHand : MonoBehaviour
{
    private const float GAP_BETWEEN_CARDS = 0.2f;

    [SerializeField] private int numberOfHiddenCards = 1;
    
    public List<Card> cardsInHand;
    
    public void RenderCards(List<Card> cards)
    {
        cardsInHand = cards;
        
        List<Card> shownCards = GetInitialShownCards();

        foreach (Card card in cardsInHand)
        {
            //Debug.Log(card);
            card.transform.rotation = Quaternion.Euler(180, 0, 0);
        }

        foreach (Card card in shownCards)
        {
            //Debug.Log(card);
            card.transform.rotation = Quaternion.Euler(360, 0, 180);
        }

        ArrangeHand();
    }

    private void ArrangeHand()
    {
        float cardWidth = cardsInHand.First().transform.localScale.x;
        float initialTransform = (cardsInHand.Count * cardWidth + (cardsInHand.Count - 1) * GAP_BETWEEN_CARDS)/2;
        
        for (int i = 0; i < cardsInHand.Count; i++)
        {
            Card card = cardsInHand[i];
            card.transform.SetParent(transform);
            // card.transform.localPosition = new Vector3(initialTransform - (cardWidth + GAP_BETWEEN_CARDS) * i, 0, 0);
            card.AnimOnMove(new Vector3(initialTransform - (cardWidth + GAP_BETWEEN_CARDS) * i, 0, 0), .25f * i);
        }
    }

    public void RevealCard()
    {
        foreach (Card card in GetInitialHiddenCards())
        {
            card.AnimOnRotate(Quaternion.Euler(0, 0, 0), 0f);
        }
    }

    public List<Card> GetWildCards(string wildCardSuite)
    {
        return cardsInHand
            .Where(card => card.cardSuit == wildCardSuite)
            .ToList();
    }

    public List<Card> GetInitialShownCards()
    {
        return cardsInHand.Skip(numberOfHiddenCards).ToList();
    }

    public List<Card> GetInitialHiddenCards()
    {
        return cardsInHand.Take(numberOfHiddenCards).ToList();
    }
    
    public void CheckSelectedCard(Card selectedCard)
    {
        foreach(Card card in cardsInHand)
        {
            if (card.isSelected && card != selectedCard)
            {
                card.Deselect();
            }
        }
    }

    public void RemoveCard(Card card)
    {
        cardsInHand.Remove(card);
        ArrangeHand();
    }
    
    public void SetCardInteraction(bool interactable)
    {
        foreach (Card card in cardsInHand)
        {
            card.isInteractible = interactable;
        }
    }
    
    public bool IsCardInHand(Card card)
    {
        return cardsInHand.Contains(card);
    }

}
