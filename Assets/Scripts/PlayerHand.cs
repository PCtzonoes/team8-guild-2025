using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Events;
using UnityEngine;
using System;

public class PlayerHand : MonoBehaviour
{
    public List<Card> cardsInHand = new List<Card>();

    [SerializeField] private float cardSpacingX = 1f;
    [SerializeField] private float cardSpacingZ = .01f;

    public void DrawCards(List<Card> cards)
    {
        cards.ForEach(card => card.isInHand = true);
        cardsInHand.AddRange(cards);
        ArrangeHand();
    }
    
    public void ArrangeHand()
    {
        int count = cardsInHand.Count;
        if (count == 0) return;


        float spacingX = cardSpacingX/count;

        float totalWidth = (count - 1) * spacingX;
        float startX = -totalWidth / 2f; // Center the hand

        float totalDepth = (count - 1) * cardSpacingZ;
        float startZ = totalDepth / 2f; // Centre of hand

        for (int i = 0; i < count; i++)
        {
            Card thisCard = cardsInHand[i];
            thisCard.transform.SetParent(transform);

            // change position based on order
            Vector3 newPos = new Vector3(startX + i * spacingX, 0f, startZ + i * - cardSpacingZ);
            thisCard.AnimOnMoveAndRotate(newPos, transform.rotation, .15f*i);

            // rotate based on position
            //thisCard.transform.rotation = transform.rotation;
        }
    }

    public void CheckSelectedCard()
    {
        foreach(Card card in cardsInHand)
        {
            if (card.isSelected == true)
            {
                card.isSelected = false;
                card.AnimHoverDown();
            }
        }
    }

    public void PlaySelectedCard(Card card)
    {
        GameEvents.PlayedCard(card);
        cardsInHand.Remove(card);
        card.isInteractible = false;
        // card.AnimPlayToTable();
        ArrangeHand();
    }

    private void EnableCardInteraction(int bet)
    {
        foreach (Card card in cardsInHand)
        {
            card.isInteractible = true;
        }
    }

    private void OnEnable()
    {
        GameEvents.OnBetMade += EnableCardInteraction;
    }

    private void OnDisable()
    {
        GameEvents.OnBetMade -= EnableCardInteraction;
    }


}
