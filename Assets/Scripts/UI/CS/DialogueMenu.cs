using DefaultNamespace.Events;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueMenu : UIRoot
{
    private VisualElement _background;
    private Label _line;

    public bool scrolling;

    [SerializeField] private float _commaDelay;
    [SerializeField] private float _periodQuestionExclamationDelay;
    [SerializeField] private float _letterDelay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private new void Start()
    {
        base.Start();
        // declare visual elements
        _background = _uiDoc.rootVisualElement.Q("background");
        _line = _uiDoc.rootVisualElement.Q<Label>("line");
    }

    public void UpdateLine(string line)
    {
        _line.text = line;
    }
}
