using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Core.StateManagement;
using DefaultNamespace.Events;
using GameRound;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] public Vector3 WildCardPosition = new Vector3(-0f, 1f, -7f);
    public readonly Quaternion WildCardRotation = Quaternion.Euler(40f, 0.0f, 0.0f);
    public readonly Vector3 DiscardPosition = new Vector3(10.0f, 10.0f, 10.0f);

    [SerializeField] private DeckManager deckManager;
    [SerializeField] private PlayerHand playerHand;
    [SerializeField] private TrickManager trickManager;
    [SerializeField] private DialogueEvents dialogueEvents;
    
    [SerializeField] private GameActionEvent gameActionEvent;
    
    public string wildCardSuit;
    private int _targetTrickWins;
    public int tricksWon = 0;

    public void ShuffleCards(GameStateProperties properties)
    {
        deckManager.ShuffleCards();

        gameActionEvent.ActionEnd();
    }
    
    public void SetTrumpCard(GameStateProperties properties)
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
        
        properties.WildCardSuit = wildCardSuit;
        gameActionEvent.ActionEnd();
    }
    
    public void DrawPlayerHand(GameStateProperties properties)
    {
        List<Card> drawnCards = deckManager.DrawCardsFromDeck(properties.InitialPlayerHandSize);
        playerHand.DrawCards(drawnCards);
        
        gameActionEvent.ActionEnd();
    }

    public void StartBetting(GameStateProperties properties)
    {
        GameEvents.StartBetting();
    }
    
    private void PlaceBet(int bet)
    {
        _targetTrickWins = bet;
        gameActionEvent.ActionEnd() ;
    }

    public void InitializeTrick(GameStateProperties properties)
    {
        trickManager.InitializeTrick(
            deckManager.DrawCardsFromDeck(3),
            wildCardSuit);
    }

    public void PlayTrick(GameStateProperties properties)
    {
        trickManager.StartTrick(deckManager.DrawCardsFromDeck(3));
    }
 
    private void OnTrickEnd(bool isPlayerWinner)
    {
        if (isPlayerWinner)
        {
            tricksWon++;
            GameEvents.TrickWon(tricksWon);

            if (tricksWon > _targetTrickWins)
            {
                dialogueEvents.TriggerDialogueByName("lose_round");
                return;
            }
        }

        // check once player hand is empty
        if (playerHand.cardsInHand.Count <= 0)
        {
            if (tricksWon != _targetTrickWins)
            {
                dialogueEvents.TriggerDialogueByName("lose_round");
            }
            else
            {
                dialogueEvents.TriggerDialogueByName("win_round");
            }
        }
        else
        {
            trickManager.StartTrick(deckManager.DrawCardsFromDeck(3));
        }
    }

    public void EndGame(GameStateProperties properties)
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
