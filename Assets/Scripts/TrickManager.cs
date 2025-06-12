using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public class TrickManager : MonoBehaviour
{
    [SerializeField] private PlayerHand playerHand;
    [SerializeField] private OpponentHand opponentHand;
    [SerializeField] private Card playedCard;
    [SerializeField] private string wildCardSuite;
    
    
    [SerializeField] private Card[] tempOpponentCardInput;
    [SerializeField] private Card tempPlayedCard;
    
    // Start is called before the first frame update
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            OnTrickStart(tempOpponentCardInput);
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            OnPlayCard(tempPlayedCard);
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            TrickEnd();
        }
    }

    void OnTrickStart(Card[] opponentCards)
    {
        opponentHand.RenderCards(opponentCards);
    }

    void OnPlayCard(Card card)
    {
        // TODO: Remove card from PlayerHand
        
        playedCard = card;
        playedCard.transform.SetParent(transform);
        playedCard.transform.localPosition = new Vector3(-0.0f,0.1f,-6.5f);
        playedCard.transform.localRotation = Quaternion.Euler(90, 0, 0);
        
        opponentHand.RevealCard();
    }

    void TrickEnd()
    {
        playedCard.cardSuit = wildCardSuite;
        if (DidPlayerWinTrick())
        {
            Debug.Log("YOU WIN!");
        }
        else
        {
            Debug.Log("YOU LOSE!");
        }
        
    }

    private bool DidPlayerWinTrick()
    {
        if (playedCard.cardSuit == "jester")
        {
            return opponentHand.GetInitialShownCards().Any(card => card.cardSuit != "jester");
        }
        
        if (playedCard.cardSuit == "wizard")
        {
            return opponentHand.GetInitialShownCards().All(card => card.cardSuit != "wizard");
        }
        
        if (opponentHand.handOfCards.Any(card => card.cardSuit == "wizard"))
        {
            return false;
        }

        Card[] opponentWildCards = opponentHand.GetOpponentWildCards(wildCardSuite);
        
        if (playedCard.cardSuit == wildCardSuite)
        {
            return !opponentWildCards.Any(card => card.cardRank >= playedCard.cardRank);
        }

        if (opponentWildCards.Length > 0)
        {
            return false;
        }
        
        return !opponentHand.handOfCards.Any(card => card.cardRank >= playedCard.cardRank);
    }
}
