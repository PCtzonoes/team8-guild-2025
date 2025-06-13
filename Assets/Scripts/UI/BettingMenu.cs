using DefaultNamespace.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BettingMenu : MonoBehaviour
{
    [SerializeField] private GameObject content;

    private int _bet;

    private void EnableUI()
    {
        content.SetActive(true);
    }

    public void ChangeBet(int bet)
    {
        _bet = bet;
    }

    public void ConfirmBet()
    {
        GameEvents.MadeBet(_bet);
        content.SetActive(false);
    }

    private void OnEnable()
    {
        GameEvents.OnStartBetting += EnableUI;
    }

    private void OnDisable()
    {
        GameEvents.OnStartBetting -= EnableUI;
    }
}
