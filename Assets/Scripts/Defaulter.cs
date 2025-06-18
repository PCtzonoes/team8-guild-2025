using DefaultNamespace.Events;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defaulter : MonoBehaviour
{
    [SerializeField] private DeckManager deck;
    private Card[] cards;
    private Transform[] cardTransforms;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TrickManager trickManager;
    [SerializeField] private TempPowerButton powerMenu;
    [SerializeField] private DialogueTrigger dialogueTrigger;

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

        dialogueTrigger.currentBark = 1;
        TrickManager.currentTrick = 0;
    }
}
