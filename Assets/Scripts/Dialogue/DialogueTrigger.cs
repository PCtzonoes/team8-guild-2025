using DefaultNamespace.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueEvent dialogueEvent;
    
    public Dialogue[] dialoguePhases; // Keep original name to preserve Unity Inspector data
    private Dictionary<string, Dialogue> _dialogueLookup = new Dictionary<string, Dialogue>();
    
    private DialogueManager _dialogueManager;

    private void Start()
    {
        _dialogueManager = FindObjectOfType<DialogueManager>();
        
        // Convert array to dictionary and set default delays
        InitializeDialogues();
    }

    private void InitializeDialogues()
    {
        _dialogueLookup.Clear();
        
        if (dialoguePhases == null) return;
        
        foreach (Dialogue dialogue in dialoguePhases)
        {
            _dialogueLookup[dialogue.name] = dialogue;
        }
    }

    public void TriggerDialogueByName(string dialogueName)
    {
        if (_dialogueLookup.TryGetValue(dialogueName, out Dialogue dialogue))
        {
            _dialogueManager.StartDialogue(dialogue);
        }
        else
        {
            Debug.LogWarning($"DialogueTrigger: Could not find dialogue with name '{dialogueName}'. Available dialogues: {string.Join(", ", _dialogueLookup.Keys)}");
        }
    }

    /// <summary>
    /// event Subscriptions - DialogueTrigger handles all event listening
    /// </summary>
    private void OnEnable()
    {
        dialogueEvent.OnTriggeredDialogueByName += TriggerDialogueByName;
    }

    private void OnDisable()
    {
        dialogueEvent.OnTriggeredDialogueByName -= TriggerDialogueByName;
    }
}
