using System;
using UnityEngine;
using UnityEngine.UIElements;
using FMODUnity;

public class RecipeBook : UIRoot
{

    private VisualElement[] _pages;

    private Button _backButton;
    private Button _leftTurn;
    private Button _rightTurn;

    private int _currentPage = 0;

    private new void Start()
    {

        for (int j = 0; j < _pages.Length; j++)
        {
            _pages[j] = _uiDoc.rootVisualElement.Q($"p{j+1}");
        }
        
        _backButton = _uiDoc.rootVisualElement.Q<Button>("back");
        _leftTurn = _uiDoc.rootVisualElement.Q<Button>("left");
        _rightTurn = _uiDoc.rootVisualElement.Q<Button>("right");


        // declare all click events
        _backButton.clicked += () => OnBackClick();
        _leftTurn.clicked += () => OnLeftClick();
        _rightTurn.clicked += () => OnRightClick();
    }

    // turn 1 page to the right
    private void OnRightClick()
    {
        if(_currentPage >= _pages.Length - 1) return;
        _currentPage += 1;

        for (int j = 0; j < _pages.Length; j++)
        {
            _pages[j].RemoveFromClassList("page-current");
        }

        _pages[_currentPage].AddToClassList("page-current");
        
    }

    // turn 1 page left
    private void OnLeftClick()
    {
        if (_currentPage <= 0) return;

        _currentPage -= 1;

        for (int j = 0; j < _pages.Length; j++)
        {
            _pages[j].RemoveFromClassList("page-current");
        }

        _pages[_currentPage].AddToClassList("page-current");
    }

    // make the book dissapear
    private void OnBackClick()
    {
        _currentPage = 0;
        for (int j = 0; j < _pages.Length; j++)
        {
            _pages[j].RemoveFromClassList("page-current");
        }
        _pages[_currentPage].AddToClassList("page-current");
    }
}
