using System;
using UnityEngine;

public class BossBlind : MonoBehaviour
{
    protected RoundManager _roundManager;
    protected DeckManager _deck;
    protected PlayerHand _playerHand;
    protected OpponentHand _opponentHand;
    public string rule;

    private void Start()
    {
        _roundManager = FindObjectOfType<RoundManager>();
        _deck = FindObjectOfType<DeckManager>();
        _playerHand = FindObjectOfType<PlayerHand>();
        _opponentHand = FindObjectOfType<OpponentHand>();
    }
}
