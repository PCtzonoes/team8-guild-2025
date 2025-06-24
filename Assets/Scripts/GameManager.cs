using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Events;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public Vector3 WildCardPosition = new Vector3(-0f, 1f, -7f);
    public readonly Quaternion WildCardRotation = Quaternion.Euler(40f, 0.0f, 0.0f);
    public readonly Vector3 DiscardPosition = new Vector3(10.0f, 10.0f, 10.0f);

    [SerializeField] private DeckManager deckManager;
    [SerializeField] private PlayerHand playerHand;
    [SerializeField] private TrickManager trickManager;

    public string wildCardSuit;
    private int _targetTrickWins;
    public int tricksWon = 0;

    //private void Start()
    //{
    //    StartCoroutine(StartGameRoutine());
    //}

    public void StartRound()
    {
        StartCoroutine(StartGameRoutine());
    }

    private IEnumerator StartGameRoutine()
    {
        deckManager.ShuffleCards();
        //Debug.Log("Shuffled the deck.");

        yield return new WaitForSeconds(1);

        SetTrumpCard();
        while (wildCardSuit != "hearts" && wildCardSuit != "clubs" && wildCardSuit != "diamonds" && wildCardSuit != "spades")
        {
            yield return new WaitForSeconds(1.0f);
            SetTrumpCard();
        }

        yield return new WaitForSeconds(1.0f);

        DrawPlayerHand();

        yield return new WaitForSeconds(1.0f);

        GameEvents.StartBetting();
    }

    private void OnTrickEnd(bool isPlayerWinner)
    {
        if (isPlayerWinner)
        {
            tricksWon++;
            GameEvents.TrickWon(tricksWon);

            if (tricksWon > _targetTrickWins)
            {
                GameEvents.GameLost();
                Debug.Log("YOU LOSE!");
            }
        }

        // check once player hand is empty
        if (playerHand.cardsInHand.Count <= 0)
        {
            //Debug.LogWarning("check passed");
            if (tricksWon != _targetTrickWins)
            {
                GameEvents.GameLost();
                return;
            }
            else
            {
                GameEvents.GameWon();
                return;
            }
        }
        else
        {
            trickManager.StartTrick(deckManager.DrawCardsFromDeck(3));
        }

        //trickManager.StartTrick(deckManager.DrawCardsFromDeck(3));
    }

    public void CheckRoundOverState()
    {
        if (playerHand.cardsInHand.Count > 0 || TrickManager.currentTrick <= 0)
        {
            return;
        }
            
        if (tricksWon != _targetTrickWins)
        {
            DialogueEvents.LoseDialogue();
        }
        else
        {
            DialogueEvents.WinDialogue();
        }
    }

    public void SetTrumpCard()
    {
        //TODO: Handler redraw if is Wizard or Jester
        Card wildCard = deckManager.DrawCardsFromDeck(1)[0];
        wildCardSuit = wildCard.cardSuit;

        wildCard.transform.SetParent(transform);
        wildCard.AnimOnMoveAndRotate(WildCardPosition, WildCardRotation, 0.1f);

        GameEvents.SetWildCardSuit(wildCardSuit);

        wildCard.AnimOnMoveAndRotate(DiscardPosition, Quaternion.identity, 2.0f);
    }

    public void DrawPlayerHand()
    {
        List<Card> drawnCards = deckManager.DrawCardsFromDeck(5);
        playerHand.DrawCards(drawnCards);
    }

    public void PlaceBet(int bet)
    {
        //TODO handle bet placing
        _targetTrickWins = bet;
        //GameEvents.MadeBet(_targetTrickWins);

        trickManager.InitializeTrick(
            deckManager.DrawCardsFromDeck(3),
            wildCardSuit);
    }

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
