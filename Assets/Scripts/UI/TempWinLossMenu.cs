using DefaultNamespace.Events;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempWinLossMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _winLoss;
    [SerializeField] private GameObject _content;

    private void LoadUI(string msg)
    {
        // load the string based on the bool of win or lose state
        _content.SetActive(true);
        _winLoss.text = msg;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void OnEnable()
    {
        // event subscription for when the player wins or loses
        GameEvents.OnGameLost += LoadUI;
        GameEvents.OnGameWon += LoadUI;
    }

    private void OnDisable()
    {
        GameEvents.OnGameLost -= LoadUI;
        GameEvents.OnGameWon -= LoadUI;
    }
}
