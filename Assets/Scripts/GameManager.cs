using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Events;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private readonly Vector3 WildCardPosition = new Vector3(-0f, 1f, -7f);
    private readonly Quaternion WildCardRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    private readonly Vector3 DiscardPosition = new Vector3(10.0f, 10.0f, 10.0f);
    
    [SerializeField] private DeckManager deckManager;
    [SerializeField] private PlayerHand playerHand;
    [SerializeField] private TrickManager trickManager;

    private string _wildCardSuit;
    private int _targetTrickWins;
    private int _tricksWon = 0;
    
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
        
        SetWildCard();
        while (_wildCardSuit != "hearts" && _wildCardSuit != "clubs" && _wildCardSuit != "diamonds" && _wildCardSuit != "spades")
        {
            //Debug.Log(_wildCardSuit);
            yield return new WaitForSeconds(1.0f);
            SetWildCard();
        }
        
        yield return new WaitForSeconds(1.0f);
        //Debug.Log("Wild card set.");

        DrawPlayerHand();
        
        yield return new WaitForSeconds(1.0f);
        //Debug.Log("Player hand drawn.");

        //PlaceBet();

        //Debug.Log("Player placed bet");

        //trickManager.InitializeTrick(
        //    deckManager.DrawCardsFromDeck(3),
        //    _wildCardSuit);

        GameEvents.StartBetting();
    }

    private void OnTrickEnd(bool isPlayerWinner)
    {
        if (isPlayerWinner)
        {
            _tricksWon++;
            GameEvents.TrickWon(_tricksWon);

            if (_tricksWon > _targetTrickWins)
            {
                GameEvents.GameLost("You went over your bet, and LOST!!");
                Debug.Log("YOU LOSE!");
            }

            
        }

        // check once player hand is empty
        if (playerHand.cardsInHand.Count <= 0)
        {
            //Debug.LogWarning("check passed");
            if(_tricksWon != _targetTrickWins)
            {
                GameEvents.GameLost("You didn't achieve your bet, and LOST!!");
            }
            else
            {
                GameEvents.GameWon("You got exactly what you bet. Congratulations!!");
            }
        }

        trickManager.StartTrick(deckManager.DrawCardsFromDeck(3));
    }

    private void SetWildCard()
    {
        //TODO: Handler redraw if is Wizard or Jester
         Card wildCard = deckManager.DrawCardsFromDeck(1)[0];
         _wildCardSuit = wildCard.cardSuit;
         
         wildCard.transform.SetParent(transform);
         wildCard.AnimOnMoveAndRotate(WildCardPosition, WildCardRotation, 0.1f);
         
         GameEvents.SetWildCardSuit(_wildCardSuit);
         
         wildCard.AnimOnMoveAndRotate(DiscardPosition, Quaternion.identity, 2.0f);
    }

    private void DrawPlayerHand()
    {
        List<Card> drawnCards = deckManager.DrawCardsFromDeck(5);
        playerHand.DrawCards(drawnCards);
    }

    private void PlaceBet(int bet)
    {
        //TODO handle bet placing
        _targetTrickWins = bet;
        //GameEvents.MadeBet(_targetTrickWins);

        Debug.Log("Player placed bet");

        trickManager.InitializeTrick(
            deckManager.DrawCardsFromDeck(3),
            _wildCardSuit);
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
