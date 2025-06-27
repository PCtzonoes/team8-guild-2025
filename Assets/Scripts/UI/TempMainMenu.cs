using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TempMainMenu : MonoBehaviour
{
    [FormerlySerializedAs("gameManager")] [SerializeField] private RoundManager roundManager;
    
    public void PlayGame()
    {
        gameObject.SetActive(false);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
