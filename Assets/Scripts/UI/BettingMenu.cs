using DefaultNamespace.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BettingMenu : MonoBehaviour
{
    [SerializeField] private GameObject _content;
    [SerializeField] private Button _confirm;

    private int _bet;

    private void EnableUI()
    {
        _content.SetActive(true);
    }

    public void ChangeBet(int bet)
    {
        _bet = bet;
        _confirm.interactable = true;
    }

    public void ConfirmBet()
    {
        GameEvents.MadeBet(_bet);
        _confirm.interactable = false;
        _content.SetActive(false);
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
