using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using DefaultNamespace.Events;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public class TrickManager : MonoBehaviour
{
    [SerializeField] private PlayerHand playerHand;
    [SerializeField] private OpponentHand opponentHand;
    [SerializeField] private Card playedCard;

    private string wildCardSuit;

    private readonly Vector3 _graveyard = new Vector3(10f, 10f, 10f);
    
    public void InitializeTrick(
        List<Card> oppoenentCards,
        string drawnWildCardSuit)
    {
        wildCardSuit = drawnWildCardSuit;
        opponentHand.RenderCards(oppoenentCards);
    }

    public void StartTrick(List<Card> oppoenentCards)
    {
        StartCoroutine(StartTrickWithDelay(oppoenentCards));
    }
    
    void OnPlayedCard(Card card)
    {
        // TODO: Remove card from PlayerHand
        card.isInHand = false;
        playedCard = card;
        playedCard.transform.SetParent(transform);
        // playedCard.transform.localPosition = new Vector3(-0.0f,0.1f,-6.5f);
        // playedCard.transform.localRotation = Quaternion.Euler(90, 0, 0);
        
        playedCard.AnimOnMoveAndRotate(
            new Vector3(0.0f,0.2f,-6.5f),
            Quaternion.Euler(90, 0, 0),0f);
        
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
        //if (playedCard.cardSuit == "jester")
        //{
        //    return opponentHand.GetInitialShownCards().Any(card => card.cardSuit != "jester");
        //}
        
        if (playedCard.cardSuit == "wizard")
        {
            return opponentHand.GetInitialShownCards().All(card => card.cardSuit != "wizard");
        }
        
        if (opponentHand.handOfCards.Any(card => card.cardSuit == "wizard"))
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
        
        return !opponentHand.handOfCards.Any(card => card.cardRank >= playedCard.cardRank);
    }
    
    private void OnEnable()
    {
        GameEvents.OnPlayedCard += OnPlayedCard;
    }

    private void OnDisable()
    {
        GameEvents.OnPlayedCard -= OnPlayedCard;
    }

    private IEnumerator StartTrickWithDelay(List<Card> oppoenentCards)
    {
        yield return new WaitForSeconds(2);
        foreach (var card in opponentHand.handOfCards)
        {
            card.transform.position = _graveyard;
        }

        playedCard.gameObject.SetActive(false);

        opponentHand.handOfCards.Clear();

        opponentHand.RenderCards(oppoenentCards);
    }
}
