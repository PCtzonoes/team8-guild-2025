using DefaultNamespace.Events;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueMenu : UIRoot
{
    private Label _npcLine;
    private Label _protagonistLine;
    public bool scrolling;

    [SerializeField] private float _commaDelay;
    [SerializeField] private float _periodQuestionExclamationDelay;
    [SerializeField] private float _letterDelay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private new void Start()
    {
        base.Start();
        // declare visual elements
        _npcLine = _uiDoc.rootVisualElement.Q<Label>("npc");
        _protagonistLine = _uiDoc.rootVisualElement.Q<Label>("protagonist");
    }

    // prints the next letter to the line in question
    public void UpdateLine(string line, bool isProtagonist)
    {
        Label thisLabel;
        Label notThisLabel;
        switch (isProtagonist)
        {
            case true:
                thisLabel = _protagonistLine;
                notThisLabel = _npcLine;
                break;
            case false:
                thisLabel = _npcLine;
                notThisLabel = _protagonistLine;
                break;
        
        }

        if (thisLabel.ClassListContains("line-inactive"))
        {
            notThisLabel.AddToClassList("line-inactive");
            thisLabel.RemoveFromClassList("line-inactive");
        }
        thisLabel.text = line;
    }
}
