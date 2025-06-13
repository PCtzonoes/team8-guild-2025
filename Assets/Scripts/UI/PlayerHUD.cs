using DefaultNamespace.Events;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _bets;
    [SerializeField] private TextMeshProUGUI _trump;

    private void OnEnable()
    {
        GameEvents.OnWildCardSetSuit += UpdateTrump;
        GameEvents.OnBetMade += UpdateBet;
    }

    private void OnDisable()
    {
        GameEvents.OnWildCardSetSuit -= UpdateTrump;
        GameEvents.OnBetMade -= UpdateBet;
    }

    private void UpdateBet(int bet)
    {
        _bets.text = $"0/{bet} tricks";
    }

    private void UpdateTrump(string suit)
    {
        _trump.text = $"{suit}";
    }
}
