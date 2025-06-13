using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMainMenu : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    public void PlayGame()
    {
        gameManager.StartRound();
        gameObject.SetActive(false);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
