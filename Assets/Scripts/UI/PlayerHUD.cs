using DefaultNamespace.Events;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _bets;
    [SerializeField] private TextMeshProUGUI _trump;

    private int _bet;

    private void OnEnable()
    {
        GameEvents.OnWildCardSetSuit += UpdateTrump;
        GameEvents.OnBetMade += UpdateBet;
        GameEvents.OnTrickWon += UpdateTricksWon;
    }

    private void OnDisable()
    {
        GameEvents.OnWildCardSetSuit -= UpdateTrump;
        GameEvents.OnBetMade -= UpdateBet;
        GameEvents.OnTrickWon -= UpdateTricksWon;
    }

    private void UpdateBet(int bet)
    {
        _bet = bet;
        _bets.text = $"0/{bet} tricks";
    }

    private void UpdateTricksWon(int tricksWon)
    {
        _bets.text = $"{ tricksWon}/{_bet} tricks";
    }

    private void UpdateTrump(string suit)
    {
        _trump.text = $"{suit}";
    }
}
