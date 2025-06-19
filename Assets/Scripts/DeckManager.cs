using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private Card[] _totalDeck;
    [SerializeField] private List<Card> _currentDeck;
    [SerializeField] private PlayerHand _playerHand;

    private void Start()
    {
        ApplySuitsAndRanks();
    }

    private void ApplySuitsAndRanks()
    {
        Card[] cards = FindObjectsOfType<Card>();

        string[] suits = {"clubs", "diamonds", "spades", "hearts", "imp", "devil"};

        for(int i = 0;  i < cards.Length ; i++)
        {
            Card card = cards[i];

            switch (i) 
            {
                case < 13:
                    card.cardSuit = suits[0];
                    card.cardRank = i+2;
                    break;
                case <26:
                    card.cardSuit = suits[1];
                    card.cardRank = i-11;
                    break;
                case < 39:
                    card.cardSuit = suits[2];
                    card.cardRank = i - 24;
                    break;
                case < 52:
                    card.cardSuit = suits[3];
                    card.cardRank = i - 37;
                    break;
                case < 56:
                    card.cardSuit = suits[4];
                    card.cardRank = 0;
                    break;
                case < 60:
                    card.cardSuit = suits[5];
                    card.cardRank = 99;
                    break;
            }
            card.RenderCard();
            
        }
    }

    public void ShuffleCards()
    {
        //Debug.Log("SHUFFLING");
        _totalDeck = FindObjectsOfType<Card>();
        _currentDeck.Clear();

        // copy the template deck
        for (int i = 0; i < _totalDeck.Length; i++)
        {
            _currentDeck.Add(_totalDeck[i]);
        }

        // kuthe's shuffling algorith
        for (int i = 0;  i < _currentDeck.Count; i++)
        {
            _currentDeck.Append(_totalDeck[i]);
            int newIndex = Random.Range(0, _totalDeck.Length);

            Card oldCard = _currentDeck[newIndex];
            _currentDeck[newIndex] = _currentDeck[i];
            _currentDeck[i] = oldCard;
        }

        SpaceCards();
    }

    // space the cards out by their width.
    private void SpaceCards()
    {
        for (int i =  0; i < _currentDeck.Count; i++)
        {
            Card thisCard = _currentDeck[i];
            float cardWidth = thisCard.transform.localScale.z;
            thisCard.transform.localPosition = new Vector3(0, cardWidth * (_currentDeck.Count - i), 0);
        }
    }

    // send the top cards to the player hand
    // remove the top cards from the deck
    public void DrawPlayerHand(int handSize)
    {
        for (int i = 0; i < handSize; i++)
        {
            _playerHand.cardsInHand.Add(_currentDeck[i]);
            _currentDeck[i].isInHand = true;
            _currentDeck[i] = null;
        }

        for (int i = handSize - 1; i >= 0; i--)
        {
            _currentDeck.Remove(_currentDeck[i]);
        }

        // fix the spacing => MAKE A TWEEN LATER
        SpaceCards();

        // flourish cards
        _playerHand.ArrangeHand();
        Debug.Log("Drew Player Cards");
    }

    public List<Card> DrawCardsFromDeck(int numberOfCards)
    {
        List<Card> cards = _currentDeck.Take(numberOfCards).ToList();
        _currentDeck.RemoveRange(0, numberOfCards);
        SpaceCards();
        return cards;
    }

}
