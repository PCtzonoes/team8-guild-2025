using DefaultNamespace.Events;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Core.StateManagement;
using UnityEngine;
using UnityEngine.Serialization;

public class Defaulter : MonoBehaviour
{
    [SerializeField] private DeckManager deck;
    private Card[] cards;
    private Transform[] cardTransforms;
    [FormerlySerializedAs("gameManager")] [SerializeField] private RoundManager roundManager;
    [SerializeField] private TrickManager trickManager;
    [SerializeField] private TempPowerButton powerMenu;

    private void Start()
    {
        cards = FindObjectsOfType<Card>();
        cardTransforms = new Transform[cards.Length];
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].transform.SetParent(deck.transform);
            cardTransforms[i] = cards[i].transform;
        }
    }

    public void FuckGoBack()
    {
        // place all cards back in the deck
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].transform.SetParent(deck.transform);
            cards[i].gameObject.SetActive(true);
            
            cards[i].transform.position = deck.transform.position;
            cards[i].transform.DORotate(new Vector3(270, 0, 0), 0f);
            //Debug.LogWarning(cards[i].transform.rotation);
        }

        trickManager.CurrentTrick = 0;
        roundManager.EndGame(GameState.Properties);
    }
}
