using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using DefaultNamespace.Events;
using DefaultNamespace.Powers;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public class TrickManager : MonoBehaviour
{
    [SerializeField] private PlayerHand playerHand;
    [SerializeField] private OpponentHand opponentHand;
    [SerializeField] private Card playedCard;

    private string wildCardSuit;

    [CanBeNull] private ICardTransformer cardTransformerInPlay = null;

    public readonly Vector3 _graveyard = new Vector3(10f, 10f, 10f);

    public static int currentTrick = 0;
    
    public void InitializeTrick(
        List<Card> oppoenentCards,
        string drawnWildCardSuit)
    {
        wildCardSuit = drawnWildCardSuit;
        opponentHand.RenderCards(oppoenentCards);
    }

    public void StartTrick(List<Card> oppoenentCards)
    {
        currentTrick++;
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
            new Vector3(0.0f,0.2f,-6.5f),
            Quaternion.Euler(90, 0, 0),0f);
        playerHand.RemoveCard(card);
        //Debug.Log("Player played card.");
        
        TrickEnd();
    }

    void TrickEnd()
    {
        opponentHand.RevealCard();
        GameEvents.EndTrick(DidPlayerWinTrick());
    }

    private bool DidPlayerWinTrick()
    {   
        if (playedCard.cardSuit == "wizard")
        {
            return opponentHand.GetInitialShownCards().All(card => card.cardSuit != "wizard");
        }
        
        if (opponentHand.cardsInHand.Any(card => card.cardSuit == "wizard"))
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
        GameEvents.OnSelectCard += OnSelectCard;
        GameEvents.OnPlayedCard -= OnPlayedCard;
        GameEvents.OnUsePower += OnUsePower;
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
