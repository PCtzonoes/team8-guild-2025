using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using DefaultNamespace.Events;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] public Vector3 WildCardPosition = new Vector3(-0f, 1f, -7f);
    public readonly Quaternion WildCardRotation = Quaternion.Euler(40f, 0.0f, 0.0f);
    public readonly Vector3 DiscardPosition = new Vector3(10.0f, 10.0f, 10.0f);

    [SerializeField] private DeckManager deckManager;
    [SerializeField] private PlayerHand playerHand;
    [SerializeField] private TrickManager trickManager;
    [SerializeField] private DialogueEvent dialogueEvent;
    
    public string wildCardSuit;
    private int _targetTrickWins;
    public int tricksWon = 0;

    public void ShuffleCards()
    {
        deckManager.ShuffleCards();
    }
    
    public void SetTrumpCard()
    {
        while (wildCardSuit != "hearts" && wildCardSuit != "clubs" && wildCardSuit != "diamonds" &&
               wildCardSuit != "spades")
        {
            Card wildCard = deckManager.DrawCardsFromDeck(1)[0];
            wildCardSuit = wildCard.cardSuit;

            wildCard.transform.SetParent(transform);
            wildCard.AnimOnMoveAndRotate(WildCardPosition, WildCardRotation, 0.1f);

            GameEvents.SetWildCardSuit(wildCardSuit);

            wildCard.AnimOnMoveAndRotate(DiscardPosition, Quaternion.identity, 2.0f);

        }
    }
    
    public void DrawPlayerHand(int numberOfCards)
    {
        List<Card> drawnCards = deckManager.DrawCardsFromDeck(numberOfCards);
        playerHand.DrawCards(drawnCards);
    }

    public void StartBetting()
    {
        GameEvents.StartBetting();
    }
    
    private void PlaceBet(int bet)
    {
        _targetTrickWins = bet;
    }

    public void InitializeTrick()
    {
        trickManager.InitializeTrick(
            deckManager.DrawCardsFromDeck(3),
            wildCardSuit);
    }
 
    private void OnTrickEnd(bool isPlayerWinner)
    {
        if (isPlayerWinner)
        {
            tricksWon++;
            GameEvents.TrickWon(tricksWon);

            if (tricksWon > _targetTrickWins)
            {
                dialogueEvent.TriggerDialogueByName("lose_round");
                return;
            }
        }

        // check once player hand is empty
        if (playerHand.cardsInHand.Count <= 0)
        {
            if (tricksWon != _targetTrickWins)
            {
                dialogueEvent.TriggerDialogueByName("lose_round");
            }
            else
            {
                dialogueEvent.TriggerDialogueByName("win_round");
            }
        }
        else
        {
            trickManager.StartTrick(deckManager.DrawCardsFromDeck(3));
        }
    }

    // REMOVED: CheckRoundOverState() method
    // Game ending logic is now handled directly in OnTrickEnd() method
    // This eliminates the redundant and premature round-over checks

    public void EndGame()
    {
        _targetTrickWins = 0;
        tricksWon = 0;
        playerHand.cardsInHand.Clear();
        wildCardSuit = "";
    }

    private void OnEnable()
    {
        GameEvents.OnTrickEnd += OnTrickEnd;
        GameEvents.OnBetMade += PlaceBet;
    }

    private void OnDisable()
    {
        GameEvents.OnTrickEnd -= OnTrickEnd;
        GameEvents.OnBetMade -= PlaceBet;
    }
}
