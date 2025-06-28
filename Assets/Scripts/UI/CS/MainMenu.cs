using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : UIRoot
{
    [SerializeField] private Button _tutorial;
    [SerializeField] private Button _grimReaper;
    [SerializeField] private Button _dolumon;
    [SerializeField] private Button _quit;

    private LoadingScreen _loadingScreen;

    private new void Start()
    {
        base.Start();
        // declare scene handler
        _loadingScreen = FindObjectOfType<LoadingScreen>();

        // Declare All Buttons
        _tutorial = _uiDoc.rootVisualElement.Q<Button>("Tutorial");
        _grimReaper = _uiDoc.rootVisualElement.Q<Button>("GrimReaper");
        _dolumon = _uiDoc.rootVisualElement.Q<Button>("Dolumon");
        _quit = _uiDoc.rootVisualElement.Q<Button>("Quit");

        _tutorial.clicked += () => OnTutorialClicked();
        _quit.clicked += () => OnGrimReaperClicked();
        _quit.clicked += () => OnDolumonClicked();
        _quit.clicked += () => OnQuitClicked();

        
    }

    private void DisableAllButtons()
    {
        _tutorial.AddToClassList("button-inactive");
        _tutorial.focusable = false;
        _grimReaper.AddToClassList("button-inactive");
        _grimReaper.focusable = false;
        _dolumon.AddToClassList("button-inactive");
        _dolumon.focusable = false;
        _quit.AddToClassList("button-inactive");
        _quit.focusable = false;
    }

    private void OnTutorialClicked()
    {
        DisableAllButtons();
        SceneHandler.LoadScene("grimTutorial");
    }

    private void OnGrimReaperClicked()
    {
        DisableAllButtons();
        SceneHandler.LoadScene("grimLevel");
    }

    private void OnDolumonClicked()
    {
        DisableAllButtons();
        throw new NotImplementedException();
    }

    private void OnQuitClicked()
    {
        DisableAllButtons();
        Application.Quit();
    }


}
  