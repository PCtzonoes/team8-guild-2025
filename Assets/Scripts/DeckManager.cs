using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private Card[] _totalDeck;
    [SerializeField] private List<Card> _currentDeck;
    [SerializeField] private PlayerHand _playerHand;

    // private void Start()
    // {
    //     _totalDeck = FindObjectsOfType<Card>();
    //
    //     ShuffleCards();
    // }

    // Manual Debugs
    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.S))
    //     {
    //         ShuffleCards();
    //     }
    //
    //     if (Input.GetKeyDown(KeyCode.D))
    //     {
    //         DrawPlayerHand(5);
    //     }
    // }

    public void ShuffleCards()
    {
        _totalDeck = FindObjectsOfType<Card>();

        Debug.Log("Shuffling cards...");
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

        Debug.Log("shuffled the deck");
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
