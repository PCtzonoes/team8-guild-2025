using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using DefaultNamespace.Events;
using DefaultNamespace.Powers;
using JetBrains.Annotations;
using UnityEngine;

public class TrickManager : MonoBehaviour
{
    [SerializeField] private PlayerHand playerHand;
    [SerializeField] private OpponentHand opponentHand;
    [SerializeField] private Card playedCard;
    //[SerializeField] private GameManager gameManager;

    private string wildCardSuit;
    public int CurrentTrick { get; set; } = 0;

    [CanBeNull] private ICardTransformer cardTransformerInPlay = null;

    public readonly Vector3 _graveyard = new Vector3(10f, 10f, 10f);

    public void InitializeTrick(
        List<Card> oppoenentCards,
        string drawnWildCardSuit)
    {
        wildCardSuit = drawnWildCardSuit;
        opponentHand.RenderCards(oppoenentCards);
    }

    public void StartTrick(List<Card> oppoenentCards)
    {
        CurrentTrick++;
        StartCoroutine(StartTrickWithDelay(oppoenentCards));
    }

    public void DiscardCards(List<Card> cards)
    {
        foreach (Card card in cards)
        {
            if (playerHand.IsCardInHand(card))
            {
                playerHand.RemoveCard(card);
            }
            else if (opponentHand.IsCardInHand(card))
            {
                opponentHand.RemoveCard(card);
            }
        }
    }

    private void OnUsePower(IPower power)
    {
        power.Use(this);

        if (power is ICardTransformer cardTransformer)
        {
            this.cardTransformerInPlay = cardTransformer;
            this.opponentHand.SetCardInteraction(true);
        }
    }
    
    private void OnSelectCard(Card card)
    {
        playerHand.CheckSelectedCard(card);
        opponentHand.CheckSelectedCard(card);
    }
    
     
    private void OnPlayedCard(Card card)
    {
        if (cardTransformerInPlay != null)
        {
            cardTransformerInPlay.TransformCard(card);
            opponentHand.SetCardInteraction(false);
            cardTransformerInPlay = null;
            card.Deselect();
            card.isSelected = false;

            if (playerHand.cardsInHand.Count <= 0)
            {
                TrickEnd();
            }
            
            return;
        }
        
        card.isInHand = false;
        playedCard = card;
        playedCard.transform.SetParent(transform);
        // playedCard.transform.localPosition = new Vector3(-0.0f,0.1f,-6.5f);
        // playedCard.transform.localRotation = Quaternion.Euler(90, 0, 0);
        
        playedCard.AnimOnMoveAndRotate(
            new Vector3(1.32f, 0.83f, -7.367f),
            Quaternion.Euler(0, 0, 0),0f);
        playerHand.RemoveCard(card);
        //Debug.Log("Player played card.");
        
        card.isInteractible = false;

        TrickEnd();
    }

    void TrickEnd()
    {
        opponentHand.RevealCard();
        bool isPlayerWinner = DidPlayerWinTrick();
        GameEvents.EndTrick(isPlayerWinner);
    }

    private bool DidPlayerWinTrick()
    {   
        if (playedCard.cardSuit == "devil")
        {
            return opponentHand.GetInitialShownCards().All(card => card.cardSuit != "devil");
        }
        
        if (opponentHand.cardsInHand.Any(card => card.cardSuit == "devil"))
        {
            return false;
        }

        List<Card> opponentWildCards = opponentHand.GetWildCards(wildCardSuit);
        
        if (playedCard.cardSuit == wildCardSuit)
        {
            return !opponentWildCards.Any(card => card.cardRank >= playedCard.cardRank);
        }

        if (opponentWildCards.Count > 0)
        {
            return false;
        }
        
        return !opponentHand.cardsInHand.Any(card => card.cardRank >= playedCard.cardRank);
    }
    
    private void OnEnable()
    {
        GameEvents.OnSelectCard += OnSelectCard;
        GameEvents.OnPlayedCard += OnPlayedCard;
        GameEvents.OnUsePower += OnUsePower;
    }

    private void OnDisable()
    {
        GameEvents.OnSelectCard -= OnSelectCard;
        GameEvents.OnPlayedCard -= OnPlayedCard;
        GameEvents.OnUsePower -= OnUsePower;
    }

    private IEnumerator StartTrickWithDelay(List<Card> oppoenentCards)
    {
        yield return new WaitForSeconds(2);
        
        foreach (var card in opponentHand.cardsInHand)
        {
            card.transform.position = _graveyard;
        }

        playedCard.gameObject.SetActive(false);

        opponentHand.cardsInHand.Clear();

        opponentHand.RenderCards(oppoenentCards);
    }
}
