using System;
using UnityEngine;

public class BossBlind : MonoBehaviour
{
    protected GameManager _gameManager;
    protected DeckManager _deck;
    protected PlayerHand _playerHand;
    protected OpponentHand _opponentHand;
    public string rule;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _deck = FindObjectOfType<DeckManager>();
        _playerHand = FindObjectOfType<PlayerHand>();
        _opponentHand = FindObjectOfType<OpponentHand>();
    }
}
